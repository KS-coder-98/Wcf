using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string[] GetMessages();

        [OperationContract]
        void sendString(string name);

        [OperationContract]
        List<List<AirConnection>> SearchByLocation(String portA, String portB);

        [OperationContract]
        List<List<AirConnection>> SearchByLocationAndDate(String portA, String portB, DateTime departure, DateTime arrival);

    }
}
