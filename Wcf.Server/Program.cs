using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class Program
    {

        //[Serializba]
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        public class MessageService : IMessageService
        {
            public string[] GetMessages()
            {
                return new string[] { "server1", "server2", "server3" };
            }

            public List<List<AirConnection>> SearchByLocation(string portA, string portB)
            {
                return flightSearchEngine.Start(portA, portB, DateTime.Now.AddDays(-300), DateTime.Now.AddDays(300));
            }

            public List<List<AirConnection>> SearchByLocationAndDate(string portA, string portB, DateTime departure, DateTime arrival)
            {
                return flightSearchEngine.Start(portA, portB, departure, arrival);
            }

            public void sendString(string name)
            {
                Console.WriteLine(name);
            }
        }

        public static FlightSearchEngine flightSearchEngine;
        static void Main(string[] args)
        {
            List<AirConnection> airConnections = AirConnection.ReadCsvFile(@"C:\Users\Krzysztof\source\repos\Wcf\Wcf.Server\flight_database.csv");
            flightSearchEngine = new FlightSearchEngine
            {
                baseFlights = airConnections
            };
            var uris = new Uri[1];
            string address = "net.tcp://localhost:6565/MessageService";
            uris[0] = new Uri(address);
            IMessageService message = new MessageService();
            ServiceHost host = new ServiceHost(message, uris);
            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(IMessageService), binding, "");
            host.Opened += Host_Opened;
            host.Open();
            Console.ReadLine();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("message service started");
        }
    }
}
