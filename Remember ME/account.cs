using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    public static class account
    {
        private static string name;
        private static string pass;

        public static string User
        {
            get { return name; }
            set { name = value; }
        }

        public static string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        public static void Clear()
        {
            name = null;
            pass = null;
        }
    }
}
