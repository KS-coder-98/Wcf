using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class WrongDateexception : Exception 
    {
        public WrongDateexception()
        {
            Console.WriteLine("Zle wprowadznone daty");
        }
    }
}
