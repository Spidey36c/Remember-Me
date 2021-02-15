using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember_Me
{
    [Serializable]
    public class SettingsSave //needed to save settings
    {
        private string importFolder = null;
        private string exportFolder = null;

        public string ImportF
        {
            get { return importFolder; }
            set { importFolder = value; }
        }

        public string ExportF
        {
            get { return exportFolder; }
            set { exportFolder = value; }
        }
    }
}
