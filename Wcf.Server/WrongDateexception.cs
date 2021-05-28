using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcf.Server
{
    class WrongDateexception : Exception 
    {
        public String msg;
        public WrongDateexception()
        {
            msg = "Zle wprowadznone daty";
            Console.WriteLine(msg);
        }
        public String getMsg()
        {
            return msg;
        }
    }
}
