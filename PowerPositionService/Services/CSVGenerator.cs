using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using PowerPositionService.Interfaces;
using PowerPositionService.Model;

namespace PowerPositionService.Services
{
    public class CSVGenerator : IFileGenerator
    {
        IConfigSettings _configSettings;
        public CSVGenerator(IConfigSettings configSettings)
        {
            this._configSettings = configSettings;
        }

        public string GenerateFile(string strHeader, IEnumerable<PowerPosition> data)
        {
            try
            {
                if (!Directory.Exists(this._configSettings.FileLocation)) Directory.CreateDirectory(this._configSettings.FileLocation);

                string fileName = $"{this._configSettings.FilePrefix}{DateTime.Now.ToString(this._configSettings.FileSuffixFormat)}.csv";
                string strFilePath = Path.Combine(this._configSettings.FileLocation, fileName);

                using (var file = new StreamWriter(strFilePath))
                {
                    file.WriteLine(strHeader);
                    foreach (var line in data)
                    {
                       file.WriteLine($"{line.PositionDateTime.ToString(this._configSettings.PowerPositionDateFormat)}, { line.Volume}");
                    }
                }

                return strFilePath;
            }
            catch 
            {
                throw;
            }
        }
    }
}
