using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPositionService.Interfaces
{
    public interface IConfigSettings
    {
        string ServiceName { get; }

        int PowerPositionInterval { get; }
        string PowerPositionDateFormat { get; }
        string FileLocation { get; }
        string FilePrefix { get; }
        string FileSuffixFormat { get; }
    }
}
