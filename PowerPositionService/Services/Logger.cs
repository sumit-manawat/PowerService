using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PowerPositionService.Interfaces;
using System.IO;
using System.Reflection;

namespace PowerPositionService.Services
{
    public class Logger : ILogger
    {
        private string _LogFolderPath;
        private static readonly object _locker = new object();
        private string _LogFullFilePath;
        public string LogFolderPath
        {
            get { return _LogFolderPath; }
        }
        
        public string LogFullFilePath
        {
            get { return _LogFullFilePath; }
        }
        
        public Logger()
        {
            Assembly oAssembly = Assembly.GetExecutingAssembly();
            string strName = oAssembly.GetName().Name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            string strFileURIPath = Path.Combine(Path.GetDirectoryName(oAssembly.CodeBase), "Logs");
            string strFilePath = (new Uri(strFileURIPath)).LocalPath;
            string strFileName = Path.Combine(strFilePath, strName);
             
            lock (_locker)
            {
                if (!Directory.Exists(strFilePath))
                    Directory.CreateDirectory(strFilePath);

            }
            this._LogFolderPath = strFilePath;
            this._LogFullFilePath = strFileName;
        }

        public void Log(string strMessage, bool isError = false)
        {
            lock (_locker)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.Append(isError ? " ERROR==>" : " Info==>");
                sb.AppendLine(strMessage);
                File.AppendAllText(this.LogFullFilePath, sb.ToString());
            }
        }
    }
}


    