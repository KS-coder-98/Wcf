using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Wcf.Server;

namespace Wcf.Client
{

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "net.tcp://localhost:6565/MessageService";
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            var chanel = new ChannelFactory<IMessageService>(binding);
            var endpoint = new EndpointAddress(uri);
            IMessageService proxy = chanel.CreateChannel(endpoint);
            Console.ReadLine();
            var ui = new UI(proxy);
            ui.RunUI();
        }
    }
}
