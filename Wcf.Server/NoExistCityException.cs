using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class NoExistCityException : Exception
    {
        public NoExistCityException(string msg) : base(msg)
        {
            Console.WriteLine($"Nie ma takiego miasta jak {msg}");
        }
    }
}
