using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using PowerPositionService.Interfaces;
using System.Configuration;

using System.Timers;

namespace PowerPositionService
{
    partial class srvPowerPosition : ServiceBase
    {
        #region Global Variables

        ILogger _logger;
        IConfigSettings _configSettings;
        IPositonService _positionService;
        Timer powerPositionTimer;

        #endregion

        #region Constructor

        public srvPowerPosition(ILogger logger,
                                IConfigSettings configSettings,
                                IPositonService positionService)
        {
            this._logger = logger;
            this._configSettings = configSettings;
            this._positionService = positionService;

            InitializeComponent();
            this.ServiceName = this._configSettings.ServiceName;
            LogConfigInfo();
        }

        #endregion

        #region Service Events
        protected override void OnStart(string[] args)
        {
            try
            {
                Log(string.Format("{0} is starting......", this.ServiceName));
                //Generating CSV at the start and then will be generated at each interval
                GeneratePowerPosition();

                this.powerPositionTimer = new Timer(this._configSettings.PowerPositionInterval * 60 * 1000);
                this.powerPositionTimer.Elapsed += powerPositionTimer_Elapsed;
                Log("Starting PowerPosition timer...");
                this.powerPositionTimer.Start();
                Log(string.Format("PowerPosition timer started with interval {0} milliseconds.", this.powerPositionTimer.Interval));

                Log(string.Format("{0} successfully started.", this.ServiceName));
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                Log(ex.StackTrace, true);
            }
        }

      

        protected override void OnStop()
        {
             try
            {
            Log(string.Format("{0} is stopping......", this.ServiceName));
            // TODO: Add code here to perform any tear-down necessary to stop your service.

            Log(string.Format("{0} successfully stopped.", this.ServiceName));
            }
             catch (Exception ex)
             {
                 Log(ex.Message, true);
                 Log(ex.StackTrace, true);
             }
        }

        #endregion

        #region Timer Event
        void powerPositionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log("PowerPosition timer elapsed...");
            GeneratePowerPosition();
        }

        public void GeneratePowerPosition()
        {
            try
            {
                Log("Generating PowerPosition CSV...");
                var dayAhead = DateTime.Today.AddDays(1);
                this._positionService.GeneratePowerPosition(dayAhead);
                
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                Log(ex.StackTrace, true);
            }
        }

        #endregion


        #region Logging Functions

       
        private void Log(string strMessage, bool isError = false)
        {
            this._logger.Log(strMessage, isError);
        }

        private void LogConfigInfo()
        {
            Log("========================================================================================");
            Log("Configuration Settings");
            Log("========================================================================================");
            foreach (string key in ConfigurationManager.AppSettings)
            {
                Log(string.Format("{0}==>{1}", key, ConfigurationManager.AppSettings[key]));
            }
            Log("========================================================================================");
            Log("End Configuration Settings");
            Log("========================================================================================");
        }

        #endregion
    }
}
