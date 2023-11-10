using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Converter;
using log4net;
using log4net.Config;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Configurator
{
    /// <summary>
    /// Abstracts access to external resources to facilitate unit testing.
    /// </summary>
    public class ConfiguratorFactory : IConfiguratorFactory
    {
        public string GetEnvironmentVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName);
        }

        public string GetFilePath(string fileName, string directory, string homeDir)
        {
            return Utils.GetFilePath(fileName, directory, homeDir);
        }

        public string FormatDirectoryPath(string fileName)
        {
            var file = new FileInfo(fileName);
            return Utils.FormatDirectoryPath(file.Directory.Parent.FullName);
        }

        public XmlDocument LoadXml(string fileName)
        {
            var document = new XmlDocument();
            document.Load(fileName);
            return document;
        }

        public ILog EngineLogger => LogManager.GetLogger("MSDKLogger");

        public bool DirectoryExists(string dirName)
        {
            return Directory.Exists(dirName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void LoggerConfigureAndWatch(FileInfo info)
        {
            XmlConfigurator.ConfigureAndWatch(info);
        }

        public void LoggerConfigure(FileInfo info)
        {
            XmlConfigurator.Configure(info);
        }

        public List<ILogAppender> GetAppenders()
        {
            var appenders = new List<ILogAppender>();
            foreach (var app in LogManager.GetRepository().GetAppenders())
            {
                appenders.Add(new LogAppender(app));
            }

            return appenders;
        }

        public ILoggingWrapper MakeLoggerWrapper()
        {
            return new LoggingWrapper();
        }

        public RequestTemplate GetRequestTemplate(RequestType requestType)
        {
            return RequestTemplate.GetInstance(requestType);
        }

        public OrbitalSchema GetOrbitalSchema()
        {
            return OrbitalSchema.GetInstance();
        }

        public ResponseTemplate GetResponseTemplate(RequestType responseType)
        {
            return ResponseTemplate.GetInstance(responseType);
        }

        public Definitions GetDefinitions(string homeDir)
        {
            return new DefinitionsFile(homeDir + "\\lib\\definitions.jar", this);
        }
    }
}
