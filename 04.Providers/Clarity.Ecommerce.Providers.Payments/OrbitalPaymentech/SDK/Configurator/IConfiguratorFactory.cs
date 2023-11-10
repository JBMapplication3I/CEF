// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Configurator
{
    using System.Xml;
    using Common;
    using Converter;
    using log4net;

    public interface IConfiguratorFactory
    {
        ILog EngineLogger { get; }

        string GetEnvironmentVariable(string variableName);

        string GetFilePath(string fileName, string directory, string homeDir);

        string FormatDirectoryPath(string fileName);

        XmlDocument LoadXml(string fileName);

        bool DirectoryExists(string dirName);

        bool FileExists(string fileName);

        ILoggingWrapper MakeLoggerWrapper();

        RequestTemplate GetRequestTemplate(RequestType requestType);

        OrbitalSchema GetOrbitalSchema();

        ResponseTemplate GetResponseTemplate(RequestType responseType);

        Definitions GetDefinitions(string homeDir);
    }
}
