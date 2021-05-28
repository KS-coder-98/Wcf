using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class NoExistCityException : Exception
    {
        String city;
        public NoExistCityException(string msg) : base(msg)
        {
            city = msg;
        }

        public String getMsg()
        {
            return $"Nie ma takiego miasta {city}";
        }
    }
}
