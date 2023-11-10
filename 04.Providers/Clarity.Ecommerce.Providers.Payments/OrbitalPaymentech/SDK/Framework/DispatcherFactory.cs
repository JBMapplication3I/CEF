using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using JPMC.MSDK;
using JPMC.MSDK.Common;
using JPMC.MSDK.Comm;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Filer;

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
    /// Summary description for DispatcherFactory.
    /// </summary>
    public class DispatcherFactory : IDispatcherFactory
    {
        /// <summary>
        /// 
        /// </summary>
        protected IConfigurator config;
        /// <summary>
        /// 
        /// </summary>
        protected string templatesLock = "lock";
        /// <summary>
        /// 
        /// </summary>
        protected SafeDictionary<string, XmlDocument> templates = new SafeDictionary<string, XmlDocument>();
        /// <summary>
        /// 
        /// </summary>
        protected string submissionsLock = "lock";
        /// <summary>
        /// 
        /// </summary>
        static protected SafeDictionary<string, ISubmissionDescriptor> submissions = new SafeDictionary<string, ISubmissionDescriptor>();
        /// <summary>
        /// 
        /// </summary>
        protected string commLock = "lock";
        /// <summary>
        /// 
        /// </summary>
        static protected ICommManager commManager;
        /// <summary>
        /// Batch needs to have a new CommManager be per thread.
        /// </summary>
        protected ICommManager batchCommManager;
        /// <summary>
        /// 
        /// </summary>
        protected SDKVersion sdkVersion = new SDKVersion();

        /// <summary>
        /// 
        /// </summary>
        public class SDKVersion
        {
            /// <summary>
            /// 
            /// </summary>
            public bool loaded = false;
            /// <summary>
            /// 
            /// </summary>
            public int os;
            /// <summary>
            /// 
            /// </summary>
            public int container;
            /// <summary>
            /// 
            /// </summary>
            public int framework;
            /// <summary>
            /// 
            /// </summary>
            public int version;
            /// <summary>
            /// 
            /// </summary>
            public int internetMode;
            /// <summary>
            /// 
            /// </summary>
            public int intranetMode;
            /// <summary>
            /// 
            /// </summary>
            public int intranetSSLMode;
            /// <summary>
            /// 
            /// </summary>
            public bool batchEncryption;
            /// <summary>
            /// 
            /// </summary>
            public bool onlineEncryption;

        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DispatcherFactory()
        {
        }

        /// <summary>
        /// Returns the MSDKHome.
        /// </summary>
        public string HomeDirectory
        {
            get => Config.HomeDirectory;
            set => Config.HomeDirectory = value;
        }

        /// <summary>
        /// Returns true if the Configurator has already been initialized and referenced.
        /// </summary>
        public bool IsConfiguratorInitialized => JPMC.MSDK.Configurator.Configurator.Initialized;

        /// <summary>
        /// Returns the logger reference.
        /// </summary>
        public ILog Logger => new LoggingWrapper().EngineLogger;

        /// <summary>
        /// Returns the logger reference.
        /// </summary>
        public ILog DetailLogger => new LoggingWrapper().DetailLogger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool IsOrderResponse( IResponse response )
        {
            return ((Response) response).IsOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="val"></param>
        public void SetOrderResponse( IResponse response, bool val )
        {
            ((Response) response).IsOrder = val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public IRequest MakeRequest( string requestType, ConfigurationData configData )
        {
            return MakeRequest( requestType, configData, false );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="isFormat"></param>
        /// <returns></returns>
        public IRequest MakeRequest( string requestType, ConfigurationData configData, bool isFormat )
        {
            return MakeRequest( requestType, isFormat, null, configData );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="isFormat"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public IRequest MakeRequest( string requestType, bool isFormat, IRequest parent, ConfigurationData configData )
        {
            try
            {
                if ( configData.MessageFormat == "ORB" )
                {
                    var schema = GetOrbitalSchema();
                    if ( schema.HasFormat( requestType ) )
                    {
                        return new OrbitalRequest( requestType, configData, this );
                    }
                }
                return new Request( requestType, configData, this );
            }
            catch ( RequestException ex )
            {
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public IRequest MakeRequest( string xml, byte[] rawData, ConfigurationData configData )
        {
            try
            {
                return null;// new Request( xml, rawData, this, null, false, configData );
            }
            catch ( RequestException ex )
            {
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IFileManager MakeFileManager()
        {
            return new FileManager( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public IOnlineProcessor MakeOnlineProcessor()
        {
            return new OnlineProcessor( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITCPBatchProcessor MakeTCPBatchProcessor()
        {
            return new TCPBatchProcessor( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISFTPBatchProcessor MakeSFTPBatchProcessor()
        {
            return new SFTPBatchProcessor( this );
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfigurator Config
        {
            get
            {
                if ( config == null )
                {
                    config = JPMC.MSDK.Configurator.Configurator.GetInstance();
                }
                return config;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public ISubmissionDescriptor MakeSubmission( string filename, string password, ConfigurationData configData )
        {
            return new SubmissionDescriptor( filename, password, configData, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public ISubmissionDescriptor OpenSubmission( string filename, string password, ConfigurationData configData )
        {
            var sub = (SubmissionDescriptor) MakeSubmission( filename, password, configData );
            sub.OpenBatch();
            return sub;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <param name="sftpFilename"></param>
        /// <returns></returns>
        public IResponseDescriptorImpl MakeResponseDescriptor( string filename, string password,
            ConfigurationData configData, string sftpFilename )
        {
            return new ResponseDescriptor( filename, password, configData, sftpFilename, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorResp"></param>
        /// <param name="sftpFilename"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        public IResponseDescriptor MakeResponseDescriptor( IResponse errorResp, string sftpFilename, ConfigurationData configData )
        {
            return new ResponseDescriptor( errorResp, configData, sftpFilename, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public IResponseDescriptor MakeResponseDescriptor( string filename, string password, ConfigurationData configData )
        {
            return new ResponseDescriptor( filename, password, configData, null, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRequestImpl RequestToImpl( IRequest request )
        {
            return (IRequestImpl) request;
        }

        /// <summary>
        /// Gets the template from the templates table. If it is not in the 
        /// table, it will load the template from the file, store it in the
        /// table, and then return a clone of it to the client. If it finds 
        /// the template in the table, it returns a clone of it.
        /// </summary>
        /// <remarks>
        /// The template name is the filename of the template file without
        /// the ".xml" extension. It will also be the key in the table.
        /// </remarks>
        /// <param name="templateName">The name of the template to load.</param>
        /// <returns></returns>
        public XmlDocument GetTemplate( string templateName, bool isFormat )
        {
            lock ( templatesLock )
            {
                var template = (XmlDocument) templates[ templateName ];
                if ( template != null )
                {
                    return (XmlDocument) template.Clone();
                }

                using ( var defs = Definitions )
                {
                    var name = templateName + ".xml";
                    template = defs.GetXmlDocument( name, true );
                    if ( template == null && isFormat)
                    {
                        template = defs.GetXmlDocument( "include/" + name, true );
                    }
                    if ( template == null && isFormat )
                    {
                        template = defs.GetXmlDocument( "pnsinclude/" + name, true );
                    }
                }
                
                if ( template == null )
                {
                    var msg = $"The template \"{templateName}\" could not be found.";
                    throw new DispatcherException( Error.InvalidRequestType, msg );
                }

                templates.Add( templateName, template );
                return (XmlDocument) template.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public XmlDocument LoadXmlFile( string filename )
        {
            var doc = new XmlDocument();
            doc.Load( filename );
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IBatchConverter MakeBatchConverter( ConfigurationData configData )
        {
            return new BatchConverter( configData );
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IOnlineConverter MakeOnlineConverter( ConfigurationData configData, MessageFormat messageType )
        {
            if ( messageType == MessageFormat.ORB )
            {
                return new OrbitalConverter();
            }
            return new OnlineConverter( new ConverterFactory(), configData );
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IOnlineConverter MakeOrbitalConverter( ConfigurationData configData )
        {
            return new OrbitalConverter( new ConverterFactory() );
        }

        /// <summary>
        /// This creates the static CommManager for online processing only. 
        /// This must be called at startup only.
        /// </summary>
        /// <returns></returns>
        public void MakeCommManager()
        {
            if ( commManager == null )
            {
                lock ( commLock )
                {
                    if ( commManager == null )
                        commManager = new CommManager();
                }
            }
        }

        /// <summary>
        /// Provides read access to the Online CommManager.
        /// </summary>
        public ICommManager Comm => commManager;

        /// <summary>
        /// Initializes the Configurator.
        /// </summary>
        public void InitializeConfigurator()
        {
            JPMC.MSDK.Configurator.Configurator.GetInstance();
        }

        /// <summary>
        /// Initializes the Configurator.
        /// </summary>
        public void InitializeConfigurator( string MSDKHome )
        {
            JPMC.MSDK.Configurator.Configurator.GetInstance( MSDKHome );
            Config.HomeDirectory = MSDKHome;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommManager BatchCommManager
        {
            get
            {
                if ( batchCommManager == null )
                    batchCommManager = new CommManager();
                return batchCommManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ISubmissionDescriptor GetSubmission( string name, string password )
        {
            lock ( submissionsLock )
            {
                var submission = submissions[ name ];

                if ( submission != null && password != null && submission.Password != password )
                {
                    var msg = "Submission file by this name already exists with a different password.";
                    var ex = new DispatcherException( Error.FileExists, msg );
                    Logger.Error( msg, ex );
                    throw ex;
                }

                return submission;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submission"></param>
        public void AddSubmission( ISubmissionDescriptor submission )
        {
            var chkSub = submissions[ submission.Name ];
            if ( chkSub != null )
                return;

            submissions.Add( submission.Name, submission );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void RemoveSubmission( string name )
        {
            lock ( submissionsLock )
            {
                submissions.Remove( name );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearAllSubmissions()
        {
            submissions.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IFiler MakeAESFiler()
        {
            return MakeAESFiler( null );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public IFiler MakeAESFiler( FileHeader header )
        {
            return new AESFiler( header, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public IFileWriter MakeFileWriter( ConfigurationData configData )
        {
            return new TCPFileWriter( configData, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        public IFileWriter MakeTextFileWriter( string fileName, string responseType )
        {
            return new TextFileWriter( fileName, responseType, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="submissionName"></param>
        /// <returns></returns>
        public IFileReader MakeFileReader( string filename, string password, string submissionName )
        {
            return new FileReader( filename, password, submissionName, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="create"></param>
        /// <param name="forWriting"></param>
        /// <returns></returns>
        public IStreamWrapper MakeStreamWrapper( string filename, bool create, bool forWriting )
        {
            return new StreamWrapper( filename, create, forWriting );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public IFileConverter MakePGPFileConverter( ConfigurationData configData )
        {
            return new PGPFileConverter( configData, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IFileReader MakeTextFileReader( string filename )
        {
            return new TextFileReader( filename, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="response"></param>
        public void SetResponseHost( string hostName, IResponse response )
        {
            ((Response) response).Host = hostName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="response"></param>
        public void SetResponsePort( string port, IResponse response )
        {
            ((Response) response).Port = port;
        }


        /// <summary>
        /// Creates a new RuleEngine and returns it.
        /// </summary>
        /// <param name="name">The name of the engine to load.</param>
        /// <returns></returns>
        public IRuleEngine MakeRuleEngine( string name )
        {
            return new RuleEngine( name, HomeDirectory, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        public string GetFilePath( string filename, string directory )
        {
            return Utils.GetFilePath( filename, directory, Config.HomeDirectory );
        }

        /// <summary>
        /// Gets the singleton instance of the TemplateInfo.
        /// </summary>
        public TemplateInfo TemplateInfo => TemplateInfo.Instance;

        /// <summary>
        /// Gets an environment variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns></returns>
        public string GetEnvironmentVariable( string name )
        {
            return Environment.GetEnvironmentVariable( name );
        }

        public string[] ReadTextFile( string fileName )
        {
            return File.ReadAllLines( fileName );
        }

        public OrbitalSchema GetOrbitalSchema()
        {
            return OrbitalSchema.GetInstance();
        }

        public RequestTemplate GetRequestTemplate( RequestType requestType )
        {
            return RequestTemplate.GetInstance( requestType );
        }

        public Format GetFormat( string formatName, RequestType requestType )
        {
            try
            {
                var test = RequestTemplate.GetInstance( requestType );
                return test.GetFormat( formatName );
            }
            catch
            {
                return null;
            }
        }

        public Definitions Definitions => new DefinitionsFile( Config.HomeDirectory + "\\lib\\definitions.jar", new ConfiguratorFactory() );
    }
}
