using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name
{
     public class Singletone
    {
        private static Singletone singletone;
        public Singletone ()
        {

        }

        public static Singletone instance = GetInstance();
        private static Singletone GetInstance()
        {
            if (singletone == null)
                return new Singletone();
            return singletone;
        }
    }
}
