using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPositionService.Model;

namespace PowerPositionService.Interfaces
{
    public interface IFileGenerator
    {
        string GenerateFile(string strHeader, IEnumerable<PowerPosition> data);
    }
}
