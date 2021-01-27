using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    public static class EntryClass //really bad way of keeping entry data between windows
    {
        private static int id;
        private static string name;
        private static string group;
        private static string desc;
        private static byte[] picture;

        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        public static string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static string Group
        {
            get { return group; }
            set { group = value; }
        }

        public static string Description
        {
            get { return desc; }
            set { desc = value; }
        }

        public static byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

    }
}
