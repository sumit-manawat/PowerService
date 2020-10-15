using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Services;
using System.IO;

using PowerPositionService.Interfaces;
using PowerPositionService.Model;

namespace PowerPositionService.Services
{
    public class PositonService : IPositonService
    {

        ILogger _logger;
        
        IPowerService _powerService;
        IFileGenerator _fileGenerator;
        public PositonService(ILogger logger,
                             IPowerService powerService,
                             IFileGenerator fileGenerator)
        {
            this._logger = logger;
            this._fileGenerator = fileGenerator;
            this._powerService = powerService;
        }

        public void GeneratePowerPosition(DateTime dtDate)
        {
            try
            {
                this._logger.Log("Calling GetTrades");
                var taskGetTrades = this._powerService.GetTradesAsync(dtDate);
                

                taskGetTrades.ContinueWith((taskResult) =>
                {
                    IEnumerable<PowerTrade> iePowerTrades = taskResult.Result;
                    this._logger.Log(string.Format("{0} no of PowerTrades returned", iePowerTrades.Count()));

                    //flatten all the PowerPeriods collection from PowerTrades collecion into one single collection of PowerPeriods with SelectMany
                    //then Group all the PowerPeriods by the Period and sum up the volume for each period
                    //Offset the Hour by the period to get the correct Output in hours

                    var data = from p in iePowerTrades.SelectMany(t => t.Periods)
                               group p by p.Period into g
                               select new PowerPosition()
                               {
                                   PositionDateTime = DateTime.Today.AddHours(-1).AddHours(g.Key - 1),
                                   Volume = g.Sum(t => t.Volume)
                               };


                    this._logger.Log("Generating CSV file...");
                    var strHeader = "DateTime,Volume";
                    string strFile = this._fileGenerator.GenerateFile(strHeader, data);
                    this._logger.Log(string.Format("{0} has been generated.", strFile));
                });

            }
            catch 
            {
                throw;
            }
        }
    }
}
