using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using PowerPositionService.Interfaces;

namespace PowerPositionService.Services
{
    public class ConfigSettings : IConfigSettings
    {

        public string ServiceName
        {
            get { return ConfigurationManager.AppSettings["ServiceName"]; }
        }

        public int PowerPositionInterval
        {
         get {return Convert.ToInt32(ConfigurationManager.AppSettings["PowerPositionInterval"]);}
        }

        public string PowerPositionDateFormat
        {
            get { return ConfigurationManager.AppSettings["PowerPositionDateFormat"]; }
        }
        

        public string FileLocation
        {
            get { return ConfigurationManager.AppSettings["FileLocation"]; }
        }

        public string FilePrefix
        {
            get { return  ConfigurationManager.AppSettings["FilePrefix"]; }
        }

        public string FileSuffixFormat
        {
            get { return  ConfigurationManager.AppSettings["FileSuffixFormat"]; }
        }
    }
}
