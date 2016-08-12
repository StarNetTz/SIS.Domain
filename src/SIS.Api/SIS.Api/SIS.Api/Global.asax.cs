using NServiceBus.Logging;
using System;
using System.IO;

namespace SIS.Api
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            InitializeNServiceBus();
            new AppHost().Init();
        }

        private static void InitializeNServiceBus()
        {
            InitializeNServiceBusLogging();
            ServiceBus.Init();
        }

        private static void InitializeNServiceBusLogging()
        {
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            var nserviceBusLogPath = "D:\\NServiceBusLogs";
            CreateDirIfNotExists(nserviceBusLogPath);
            defaultFactory.Directory(nserviceBusLogPath);
            defaultFactory.Level(LogLevel.Debug);
        }

        private static void CreateDirIfNotExists(string nserviceBusLogPath)
        {
            if (!Directory.Exists(nserviceBusLogPath))
                Directory.CreateDirectory(nserviceBusLogPath);
        }
    }
}