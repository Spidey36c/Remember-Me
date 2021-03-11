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
        private string name = null;
        private string pass = null;

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
