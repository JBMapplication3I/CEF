#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace JPMC.MSDK.Common
{
    using System.Linq;

    public class LoggingWrapper : ILoggingWrapper
    {
        private readonly string defLoggingConfigFile = "logging.xml";

        /// <summary>Initialize log from logging.xml.</summary>
        /// <param name="loadAndWatch">True to load and watch.</param>
        /// <param name="homeDir">     The home dir.</param>
        public void ConfigureLogging(bool loadAndWatch, string homeDir)
        {
            var tempFolderPath = Path.GetTempPath();
            var logPath = Environment.GetEnvironmentVariable("MSDK_LOGDIR");
            if (logPath == null)
            {
                logPath = Path.Combine(homeDir, "logs");
                Environment.SetEnvironmentVariable("MSDK_LOGDIR", logPath);
            }
            var logConfigFileName = GetLogConfigFileName(homeDir);
            if (loadAndWatch)
            {
                LoggerConfigureAndWatch(new FileInfo(logConfigFileName));
            }
            else
            {
                LoggerConfigure(new FileInfo(logConfigFileName));
            }
            // Unable to get the log location - default to system temp dir
            // This should only occur if the configured log location cannot be
            // written to, because XmlConfigurator.Configure will create the
            // dir if it doesn't exist.
            if (Directory.Exists(logPath))
            {
                return;
            }
            logPath = tempFolderPath;
            foreach (var app in GetAppenders())
            {
                if (!app.IsFileAppender || !app.Name.Equals("Engine"))
                {
                    continue;
                }
                var file = new FileInfo(app.File);
                if (Directory.Exists(file.DirectoryName))
                {
                    continue;
                }
                app.File = Path.Combine(logPath, file.Name);
                app.ActivateOptions();
            }
        }

        private string GetLogConfigFileName(string homeDir)
        {
            string logConfigFileName;
            //Support live logging changes
            var envVar = Environment.GetEnvironmentVariable("MSDK_LOGCONFIGFILENAME");
            if (envVar != null)
            {
                logConfigFileName = envVar;
            }
            else
            {
                //logConfigFileName = logPath;
                logConfigFileName = Path.Combine(Path.Combine(homeDir, "config"), defLoggingConfigFile);
                if (!File.Exists(logConfigFileName))
                {
                    logConfigFileName = Path.Combine(homeDir, defLoggingConfigFile);
                    if (!File.Exists(logConfigFileName))
                    {
                        logConfigFileName = null;
                    }
                }
            }
            if (logConfigFileName == null)
            {
                throw new Exception("Exception while loading configuration  file - couldn't locate logging.xml");
            }
            return logConfigFileName;
        }

        private static void LoggerConfigureAndWatch(FileInfo info)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(info);
        }

        private static void LoggerConfigure(FileInfo info)
        {
            log4net.Config.XmlConfigurator.Configure(info);
        }

        private static List<ILogAppender> GetAppenders()
        {
            return LogManager.GetRepository().GetAppenders().Select(app => new LogAppender(app)).Cast<ILogAppender>().ToList();
        }

        public bool IsConfigured => LogManager.GetRepository().Configured;

        public ILog EngineLogger
        {
            get
            {
                if (IsConfigured)
                {
                    return LogManager.GetLogger("MSDKLogger");
                }
                ConfigureLogging(true, null);
                if (!IsConfigured)
                {
                    throw new ObjectDisposedException("Logging failed to configure.");
                }
                return LogManager.GetLogger("MSDKLogger");
            }
        }

        public ILog DetailLogger
        {
            get
            {
                if (IsConfigured)
                {
                    return LogManager.GetLogger("MSDKDetailLogger");
                }
                ConfigureLogging(true, null);
                if (!IsConfigured)
                {
                    throw new ObjectDisposedException("Logging failed to configure.");
                }
                return LogManager.GetLogger("MSDKDetailLogger");
            }
        }

        /// <summary>
        /// Change the log level
        /// </summary>
        /// <param name="level"></param>
        public void SetEngineLogLevel(string level)
        {
            Level tempLevel = null;
            if (level != null)
            {
                tempLevel = ((Logger)EngineLogger.Logger).Repository.LevelMap[level];
            }
            if (tempLevel == null)
            {
                return;
            }
            ((Logger)EngineLogger.Logger).Level = tempLevel;
            EngineLogger.InfoFormat("Engine log level changed to [{0}]", level);
        }
    }

    public interface ILogAppender
    {
        bool IsFileAppender { get; }
        string Name { get; }
        string File { get; set; }
        void ActivateOptions();
    }

    public class LogAppender : ILogAppender
    {
        private readonly IAppender appender;

        public LogAppender(IAppender appender)
        {
            this.appender = appender;
        }

        #region ILogAppender Members
        public bool IsFileAppender => appender is FileAppender;

        public string Name => appender.Name;

        public string File
        {
            get => IsFileAppender ? ((FileAppender)appender).File : null;
            set
            {
                if (IsFileAppender)
                {
                    ((FileAppender)appender).File = value;
                }
            }
        }

        public void ActivateOptions()
        {
            if (IsFileAppender)
            {
                ((FileAppender)appender).ActivateOptions();
            }
        }
        #endregion
    }
}
