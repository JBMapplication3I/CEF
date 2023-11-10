using System;
using System.IO;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Comm;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Filer;
using JPMC.MSDK.Configurator;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Summary description for IDispatcherFactory.
    /// </summary>
    public interface IDispatcherFactory
    {
        /// <summary>
        /// 
        /// </summary>
        string HomeDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsConfiguratorInitialized { get; }
        /// <summary>
        /// 
        /// </summary>
        void InitializeConfigurator();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MSDKHome"></param>
        void InitializeConfigurator( string MSDKHome );
        /// <summary>
        /// 
        /// </summary>
        ILog Logger { get; }
        ILog DetailLogger { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        IRequest MakeRequest( string requestType, ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="isFormat"></param>
        /// <returns></returns>
        IRequest MakeRequest( string requestType, ConfigurationData configData, bool isFormat );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="isFormat"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IRequest MakeRequest( string requestType, bool isFormat, IRequest parent, ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="rawData"></param>
        /// <returns></returns>
        IRequest MakeRequest( string xml, byte[] rawData, ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFileManager MakeFileManager();
        /// <summary>
        /// 
        /// </summary>
        IConfigurator Config { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commMode"></param>
        /// <returns></returns>
        IOnlineProcessor MakeOnlineProcessor();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITCPBatchProcessor MakeTCPBatchProcessor();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISFTPBatchProcessor MakeSFTPBatchProcessor();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        ISubmissionDescriptor MakeSubmission( string filename, string password, ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        ISubmissionDescriptor OpenSubmission( string filename, string password, ConfigurationData configData );
        /// <summary>
        /// This creates the static CommManager for online processing only. 
        /// This must be called at startup only.
        /// </summary>
        /// <returns></returns>
        void MakeCommManager();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <param name="sftpFilename"></param>
        /// <returns></returns>
        IResponseDescriptorImpl MakeResponseDescriptor( string filename, string password, ConfigurationData configData, string sftpFilename );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        IResponseDescriptor MakeResponseDescriptor( string filename, string password, ConfigurationData configData );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorResp"></param>
        /// <param name="sftpFilename"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        IResponseDescriptor MakeResponseDescriptor( IResponse errorResp, string sftpFilename, ConfigurationData configData );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        XmlDocument LoadXmlFile( string filename );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IRequestImpl RequestToImpl( IRequest request );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ISubmissionDescriptor GetSubmission( string name, string password );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submission"></param>
        void AddSubmission( ISubmissionDescriptor submission );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void RemoveSubmission( string name );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        IFiler MakeAESFiler( FileHeader header );
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFiler MakeAESFiler();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commMode"></param>
        /// <returns></returns>
        IFileWriter MakeFileWriter( ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        IFileWriter MakeTextFileWriter( string fileName, string responseType );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="submissionName"></param>
        /// <returns></returns>
        IFileReader MakeFileReader( string filename, string password, string submissionName );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        IFileReader MakeTextFileReader( string filename );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="create"></param>
        /// <param name="forWriting"></param>
        /// <returns></returns>
        IStreamWrapper MakeStreamWrapper( string filename, bool create, bool forWriting );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        bool IsOrderResponse( IResponse response );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="val"></param>
        void SetOrderResponse( IResponse response, bool val );

        /// <summary>
        /// Gets the XmlDocument for the specified template.
        /// </summary>
        /// <remarks>
        /// The factory maintains a static cache of all the templates that 
        /// have been loaded. It will return the template document from 
        /// the cache if it is there, and will load it from the file if
        /// it is not. Once a template is loaded from the file, it will
        /// be added to the cache.
        /// 
        /// Also, GetTemplate will return a clone of the cached template
        /// so that Request can modify it without invalidating the original
        /// template.
        /// </remarks>
        /// <param name="templateName">The name of the template to get.</param>
        /// <returns>A clone of the template.</returns>
        XmlDocument GetTemplate( string templateName, bool isFormat );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        IBatchConverter MakeBatchConverter( ConfigurationData configData );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commMode"></param>
        /// <returns></returns>
        IOnlineConverter MakeOnlineConverter( ConfigurationData configData, MessageFormat messageType );

        /// <summary>
        /// 
        /// </summary>
        ICommManager Comm { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommManager BatchCommManager { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        IFileConverter MakePGPFileConverter( ConfigurationData configData );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="response"></param>
        void SetResponseHost( string hostName, IResponse response );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="response"></param>
        void SetResponsePort( string port, IResponse response );
        /// <summary>
        /// Creates a new RuleEngine and returns it.
        /// </summary>
        /// <param name="name">The name of the engine to load.</param>
        /// <returns></returns>
        IRuleEngine MakeRuleEngine( string name );
        /// <summary>
        /// Wraps the FileManager static method.
        /// </summary>
        /// <param name="filename">The name of the file to find.</param>
        /// <param name="directory">The base directory path to look in.</param>
        /// <returns></returns>
        string GetFilePath( string filename, string directory );

        /// <summary>
        /// Gets the singleton instance of the TemplateInfo.
        /// </summary>
        TemplateInfo TemplateInfo { get; }

        /// <summary>
        /// Gets an environment variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns></returns>
        string GetEnvironmentVariable( string name );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string[] ReadTextFile( string fileName );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OrbitalSchema GetOrbitalSchema();

        RequestTemplate GetRequestTemplate( RequestType requestType );

        Format GetFormat( string formatName, RequestType requestType );

        Definitions Definitions { get; }

        IOnlineConverter MakeOrbitalConverter( ConfigurationData configData );
    }
}
