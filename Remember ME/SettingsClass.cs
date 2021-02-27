using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    public static class SettingsClass
    {
        private static string importFolder = null;
        private static string exportFolder = null;
        

        public static string ImportF
        {
            get { return importFolder; }
            set { importFolder = value; }
        }

        public static string ExportF
        {
            get { return exportFolder; }
            set { exportFolder = value; }
        }
    }
}
