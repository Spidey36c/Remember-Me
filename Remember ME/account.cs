using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    [Serializable]
    public class account
    {
        private string name;
        private string pass;

        public string User
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        public void Clear()
        {
            name = null;
            pass = null;
        }
    }
}
