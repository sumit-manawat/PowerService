using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPositionService.Interfaces
{
    public interface IPositonService
    {
        void GeneratePowerPosition(DateTime dtDate);
    }
}
