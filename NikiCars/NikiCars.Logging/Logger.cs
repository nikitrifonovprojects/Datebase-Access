using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace NikiCars.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog log;

        public Logger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date{ABSOLUTE} [%thread] %level - %message%newline%exception";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"C:\Users\Nikki\Desktop\Niki Programming\Logging\Logs.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "10MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            ConsoleAppender console = new ConsoleAppender();
            console.Layout = patternLayout;
            hierarchy.Root.AddAppender(console);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            this.log = GetLogger();
        }

        public void LogError(string message)
        {
            this.log.Error(message);
        }

        public void LogDebug(string message)
        {
            this.log.Debug(message);
        }

        private ILog GetLogger([CallerFilePath]string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}
