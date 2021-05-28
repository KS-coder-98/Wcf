using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    [Serializable]
    public class AirConnection
    {
        public string portA { get; set; }
        public DateTime deperture { get; set; }
        public string portB { get; set; }
        public DateTime arrival { get; set; }

        public AirConnection(string portA, DateTime deperture, string portB, DateTime arrival)
        {
            this.portA = portA;
            this.deperture = deperture;
            this.portB = portB;
            this.arrival = arrival;
        }

        public AirConnection()
        {
        }

        public static AirConnection FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            var airConnection = new AirConnection();
            airConnection.portA = Convert.ToString(values[0]);
            airConnection.deperture = Convert.ToDateTime(values[1]);
            airConnection.portB = Convert.ToString(values[2]);
            airConnection.arrival = Convert.ToDateTime(values[3]);
            return airConnection;
        }

        public static List<AirConnection> ReadCsvFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Select(v => AirConnection.FromCsv(v))
                .ToList();
        }

        public override string ToString()
        {
            return $"{portA} {deperture} {portB} {arrival}";
        }

        internal AirConnection Clone()
        {
            return new AirConnection(this.portA, this.deperture, this.portB, this.arrival);
        }
    }
}
