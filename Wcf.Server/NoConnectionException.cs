using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class NoConnectionException : Exception
    {
        public NoConnectionException()
        {
            Console.WriteLine("Nie odnaleziono połaczenia");
        }
    }
}
