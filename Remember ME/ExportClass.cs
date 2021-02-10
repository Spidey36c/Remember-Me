using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    [Serializable]
    public class ExportClass //Can't Serialize a Static Class, this one is a reflection of EntryClass for serialization
    {
        //Does not need FilePath nor ID
        private string name;
        private string group;
        private string desc;
        private byte[] picture;

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
