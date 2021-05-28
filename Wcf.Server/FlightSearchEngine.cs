using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class FlightSearchEngine
    {
        #region Properties
        public List<AirConnection> baseFlights { get; set; }
        public List<List<AirConnection>> resultConnections;
        #endregion



        private int _processDirTrack = 0;

        private void CheckInput(String portA, String portB, DateTime departure, DateTime arrival)
        {
            if ( 
                !(from connection in baseFlights
                where connection.portA == portA
                select connection).ToList().Any())
            {
                throw new NoExistCityException(portA);
            }
            if (
                !(from connection in baseFlights
                where connection.portB == portB
                select connection).ToList().Any())
            {
                throw new NoExistCityException(portB);
            }
            if (departure.CompareTo(arrival) > 0)
            {
                throw new WrongDateexception();
            }
 

        }

        public List<List<AirConnection>> Start(String portA, String portB, DateTime departure, DateTime arrival)
        {
            if ( _processDirTrack++ == 0)
            {
                resultConnections = new List<List<AirConnection>>();
                try
                {
                    CheckInput(portA, portB, departure, arrival);
                }
                catch (NoExistCityException e)
                {
                    _processDirTrack = 0;
                    List<AirConnection> t = new List<AirConnection>();
                    t.Add(new AirConnection("ERROR", departure, e.getMsg(), arrival));
                    resultConnections.Add(t);
                    return resultConnections;
                }
                catch (WrongDateexception e)
                {
                    _processDirTrack = 0;
                    List<AirConnection> t = new List<AirConnection>();
                    t.Add(new AirConnection("ERROR", departure, e.getMsg(), arrival));
                    resultConnections.Add(t);
                    return resultConnections;
                }

            }
            AirConnection beforeVisited = baseFlights.Last();
            (from connection in baseFlights
                where connection.portA == portA 
                && connection.deperture.CompareTo(departure) >= 0 
                && connection.arrival.CompareTo(arrival) <= 0
                select connection).ToList().ForEach(connection =>
                {
                    if ( !resultConnections.Any() || resultConnections.Last().Last().portB != connection.portA)
                        resultConnections.Add(new List<AirConnection>());
                    resultConnections.Last().Add(connection);
                    if (connection.portB == portB || (connection.portB == beforeVisited.portB && connection.portA == beforeVisited.portA))
                        return;
                    Start(connection.portB, portB, departure, arrival);
                });
            if (--_processDirTrack == 0)
                return (from connection in resultConnections
                             where connection.Last().portB == portB
                             && connection.First().portA == portA
                             select connection).ToList();
            return null;
        }
    }
}
