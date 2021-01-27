using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    class EntryClass
    {
        private int id;
        private string name;
        private string group;
        private string desc;
        private byte[] picture;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Description
        {
            get { return desc; }
            set { desc = value; }
        }

        public byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

    }
}
