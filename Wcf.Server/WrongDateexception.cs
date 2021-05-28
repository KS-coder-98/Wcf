using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class WrongDateexception : Exception 
    {
        String msg;
        public WrongDateexception()
        {
            Console.WriteLine("Zle wprowadznone daty");
        }
        public String getMsg()
        {
            return "Zle wprowadznone daty";
        }
    }
}
