using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PowerPositionService.Interfaces;
using PowerPositionService.Services;

namespace PowerPositionService.UnitTest
{
    [TestClass]
    public class ConfigSettingsTest
    {
        IConfigSettings _configSettings;

        public ConfigSettingsTest()
        {
            this._configSettings = new ConfigSettings();
        }

        [TestMethod]
        public void ServiceNameNotNull()
        {
            Assert.IsNotNull(this._configSettings.ServiceName);
        }

        [TestMethod]
        public void ServiceName()
        {
            Assert.AreEqual(this._configSettings.ServiceName, "PowerPositonService");
        }

        [TestMethod]
        public void PowerPositionIntervalNotNull()
        {
            Assert.IsNotNull(this._configSettings.PowerPositionInterval);
        }

        [TestMethod]
        public void PowerPositionInterval()
        {
            Assert.AreEqual(this._configSettings.PowerPositionInterval, 1);
        }

        [TestMethod]
        public void FileLocationNotNull()
        {
            Assert.IsNotNull(this._configSettings.FileLocation);
        }

        [TestMethod]
        public void FileLocation()
        {
            Assert.AreEqual(this._configSettings.FileLocation, @"C:\PowerPositions");
        }

        [TestMethod]
        public void FilePrefixNotNull()
        {
            Assert.IsNotNull(this._configSettings.FilePrefix);
        }

        [TestMethod]
        public void FilePrefix()
        {
            Assert.AreEqual(this._configSettings.FilePrefix, "PowerPosition_");
        }

        [TestMethod]
        public void FileSuffixFormatNotNull()
        {
            Assert.IsNotNull(this._configSettings.FileSuffixFormat);
        }

        [TestMethod]
        public void FileSuffixFormat()
        {
            Assert.AreEqual(this._configSettings.FileSuffixFormat, "yyyyMMdd_HHmm");
        }
    }
}
