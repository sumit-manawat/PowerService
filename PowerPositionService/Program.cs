using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using PowerPositionService.Interfaces;
using PowerPositionService.Services;
using Services;

namespace PowerPositionService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            ILogger logger = new Logger();
            IConfigSettings configSettings = new ConfigSettings();
            IPowerService powerService = new PowerService();
            IFileGenerator fileGenerator = new CSVGenerator(configSettings);
            IPositonService positionService = new PositonService(logger, powerService, fileGenerator);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new srvPowerPosition(logger,
                                     configSettings,
                                     positionService)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
