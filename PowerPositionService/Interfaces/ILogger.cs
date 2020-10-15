using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPositionService.Interfaces
{
    public interface ILogger
    {
        void Log(string strMessage, bool isError = false);
    }
}
