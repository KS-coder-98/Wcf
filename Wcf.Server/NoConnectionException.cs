using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class NoConnectionException : Exception
    {
        public String msg;
        public NoConnectionException()
        {
            msg = "Nie ma takiego połaczenia";
            Console.WriteLine("Nie odnaleziono połaczenia");
        }
        public String getMsg()
        {
            return msg;
        }
    }
}
