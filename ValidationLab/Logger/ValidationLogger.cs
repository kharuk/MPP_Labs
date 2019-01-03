using Logger.Interfaces;
using System.Configuration;

namespace Logger
{
    /// <summary>
    /// ValidationLogger class implements logging errors received from
    /// Validation Service in logfile located in Logger directory.
    /// </summary>
    public class ValidationLogger: ILogger
    {
        private readonly NLog.Logger logger;

        public ValidationLogger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"{ConfigurationManager.AppSettings["LogfileDir"]}\\{ConfigurationManager.AppSettings["LogfileName"]}" };

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

            NLog.LogManager.Configuration = config;
        }

        public void Debug(string message) => logger.Debug(message);

        public void Error(string message) => logger.Error(message);

        public void Fatal(string message) => logger.Fatal(message);

        public void Info(string message) => logger.Info(message);

        public void Warn(string message) => logger.Warn(message);
  }
}
