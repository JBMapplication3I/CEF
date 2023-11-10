#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// <p>
// Title: Name-Value Pair Adapter
// </p>
//
// <p>
// Description: This class provides an NVP data driven
// interface to the MSDK. This class can be used directly or as a base
// class for other adapters that provide other data formats that can be
// converted to NVP
// </p>
//
// <p>
// Copyright (c)2018, Paymentech, LLC. All rights reserved
// </p>
//
// @version 1.0
namespace JPMC.MSDK.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using JPMC.MSDK.Common;
    using JPMC.MSDK.Configurator;
    using JPMC.MSDK.Framework;
    using log4net;

    public class NVPAdapter
    {
        protected static volatile IDispatcher dispatcher;

        // Submission Descriptor cache
        protected static KeySafeDictionary<string, ISubmissionDescriptor> submissionCache = new KeySafeDictionary<string, ISubmissionDescriptor>();

        // Response Descriptor cache
        protected static KeySafeDictionary<string, IResponseDescriptor> responseCache = new KeySafeDictionary<string, IResponseDescriptor>();

        // PNS Upload config cache
        protected static KeySafeDictionary<string, ConfigurationData> configCache = new KeySafeDictionary<string, ConfigurationData>();


        // The common eCommerce log for the MSDK
        public static ILog logger;

        public static ILog Logger
        {
            get
            {
                if ( logger == null )
                {
                    var lw = new LoggingWrapper();
                    logger = lw.EngineLogger;
                }
                return logger;
            }
            set => logger = value;
        }

        protected static string NEWLINE = Environment.NewLine;
        public static string EMPTY_STRING = string.Empty;
        public static bool REQUIRED = true;
        public static bool NOT_REQUIRED = false;

        private static List<CommModule> onlineCommModules = new List<CommModule>(){
            CommModule.TCPConnect,
            CommModule.HTTPSConnect,
            CommModule.PNSConnect
        };

        public static IEnumerable<CommModule> OnlineCommModules
        {
            get => onlineCommModules.AsReadOnly();
            private set { }
        }

        private static List<CommModule> batchCommModules = new List<CommModule>(){
            CommModule.TCPBatch,
            CommModule.HTTPSUpload,
            CommModule.PNSUpload,
            CommModule.SFTPBatch
        };

        public static IEnumerable<CommModule> BatchCommModules
        {
            get => batchCommModules.AsReadOnly();
            private set { }
        }


        private static List<string> responseTypes = new List<string>(){
            ResponseType.Response,
            ResponseType.HBCBIN,
            ResponseType.CHNBIN,
            //ResponseType.DELIMITED_HBC_BIN,
            //ResponseType.DELIMITED_CHN_BIN,
            ResponseType.USMCAccountUpdater,
            ResponseType.USVIAccountUpdater,
            ResponseType.EUMCAccountUpdater,
            ResponseType.EUVIAccountUpdater,
            ResponseType.USDIAccountUpdater,
            ResponseType.DelimitedFileReport,
            ResponseType.CommercialBIN,
            ResponseType.PinlessDebitBIN,
            ResponseType.PinDebitBIN
        };

        public static IEnumerable<string> ResponseTypes
        {
            get => responseTypes;
            private set { }
        }


        // Turn the list above into a comma delimited string, statically
        private static string responseTypesString;
        public static string ResponseTypesString
        {
            get
            {
                if ( responseTypesString == null )
                {
                    var sb = new StringBuilder();
                    foreach ( var str in ResponseTypes.ToList<string>() )
                    {
                        sb.Append( str ).Append( ", " );
                    }
                    sb.Remove( sb.Length - 2, 2 );
                    responseTypesString = sb.ToString();
                }
                return responseTypesString;
            }
            private set { }
        }




        public NVPAdapter()
        {
            initialize( null );
        }


        public NVPAdapter( IDispatcher disp )
        {
            initialize( disp );
        }

        private void initialize( IDispatcher disp )
        {
            var log = NVPAdapter.Logger;
            dispatcher = disp;
            if ( dispatcher == null )
                lock ( this )
                {
                    if ( dispatcher == null )
                        dispatcher = new Dispatcher();
                }

            logger.Info($"{GetThreadID()} Creating instance of {this.GetType()}");
        }


        /// <summary>
        /// This is the overloaded version of the mapToCore method that merchant clients call when
        /// using the MapAdapter interface directly.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <returns>KeySafeDictionary{string, string}.</returns>
        /// <throws> InterruptedException </throws>
        /// <throws> AdapterException </throws>
        ///
        public KeySafeDictionary<string, string> SendToCore( KeySafeDictionary<string, string> args )
        {
            KeySafeDictionary<string, string> retVal;

            try
            {
                retVal = NVPToCore( args );
                retVal.Add( "TransactionCompleted", "true" );
            }
            catch ( Exception ex )
            {
                logger.Error($"{GetThreadID()} {ex.ToString()}");
                logger.Debug($"{GetThreadID()} {ex.StackTrace}");

                retVal = new KeySafeDictionary<string, string>();
                retVal.Add( "error", ex.ToString() );
                retVal.Add( "TransactionCompleted", "false" );
            }

            return retVal;
        }


        public KeySafeDictionary<string, string> NVPToCore( KeySafeDictionary<string, string> input )
        {
            return NVPToCore( input, new SDKMetrics( SDKMetrics.Service.NVPAdapter, SDKMetrics.ServiceFormat.NVP ) );
        }


        /// <summary>
        /// This method is the main interface to the Core SDK using name value
        /// pairs. Values are always String, so binary values must be encoded into
        /// base64. Directives to the method that are not passed to the SDK all have
        /// a name with the "control." prefix. This overloaded method is only called
        /// from within Umpire and tracks transaction metrics. Merchants should call
        /// the method above when using the Map Adapter directly in their
        /// applications.
        ///
        /// </summary>
        /// <param name="input">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <returns>KeySafeDictionary{string, string}.</returns>
        ///
        public KeySafeDictionary<string, string> NVPToCore( KeySafeDictionary<string, string> input, SDKMetrics metrics )
        {

            string transID = null;
            var action = AdapterAction.None;
            var retVal = new KeySafeDictionary<string, string>();

             // Need to clone the input arguments because the args map gets modified during processing
            var args = new KeySafeDictionary<string, string>( input );

            // ResetBatch and ReleaseBatchID are special control parameters intended to make testing easier
            // thus are not documented
            if ( GetBooleanParam( args, "control.ResetBatch" ) )
                ResetBatch();
            if ( args.ContainsKey( "control.ReleaseBatchID" ) )
                ReleaseBatchID( GetParam( args, "control.ReleaseBatchID" ) );

            transID = GetParam( args, "control.TransactionID" );
            if ( transID != null )
                logger.Info($"{GetThreadID()} NVPAdapter processing TransactionID {transID}");
            else
                logger.Info( string.Format( "{0} NVPAdapter processing transaction", GetThreadID(), transID ) );

            //// Set security provider
            //var providerClass = null;
            //providerClass = Org.BouncyCastle.Crypto.          jce.provider.BouncyCastleProvider
            //// Add the provider to the list
            //java.security.Security.addProvider( (java.security.Provider) providerClass.newInstance() );

            // Get Action
            var actionStr = GetParam( args, "control.Action" );
            if ( actionStr == null )
            {
                throw new AdapterException( "control.Action is required" );
            }
            try
            {
                action = (AdapterAction)Enum.Parse( typeof( AdapterAction ), actionStr, true );
            }
            catch ( ArgumentException )
            {
                throw new AdapterException($"Action {actionStr} not supported");
            }

            // Get configuration data
            ConfigurationData config = null;
            var configName = args[ "control.ConfigName" ];  // don't remove it from args
            if ( configName != null )
                config = dispatcher.GetConfig( configName );
            else
                config = new ConfigurationData();

            // We only need protocol here if this is a PNS batch transaction
            var protocol = config.Protocol;
            if ( protocol == CommModule.Unknown && args[ "MessageHeader.MessageType" ] != null )
                protocol = CommModule.PNSUpload;

            // Get TransactionType
            var transType = TransactionType.NONE;
            var transTypeStr = GetParam( args, "control.TransactionType" );
            if ( transTypeStr != null )
            {
                try
                {
                    transType = (TransactionType)Enum.Parse( typeof( TransactionType ), transTypeStr, true );
                }
                catch ( ArgumentException )
                {
                    throw new AdapterException($"TransactionType {transTypeStr} not supported");
                }
            }

            // Get MessageFormat from input or config file
            var messageFormat = MessageFormat.None;
            var msgFormatStr = args[ "control.MessageFormat" ];  // don't remove it from args
            if ( msgFormatStr == null )
            {
                msgFormatStr = config.MessageFormat;
            }
            if ( msgFormatStr != null )
            {
                try
                {
                    messageFormat = (MessageFormat)Enum.Parse( typeof( MessageFormat ), msgFormatStr, true );
                }
                catch ( ArgumentException )
                {
                    throw new AdapterException($"MessageFormat {msgFormatStr} not supported");
                }
            }
            else
            {
                // batch Next and End transactions don't require ConfigName so we can't get the message format
                // from the config file, but if the message contains MessageHeader.MessageType we know
                // it's PNS.  This is needed in the processBatch method later.
                if ( args[ "MessageHeader.MessageType" ] != null
                        || args[ "Bit41.CardAcquirerTerminalId" ] != null
                        || args[ "Bit42.CardAcquirerId" ] != null )
                    messageFormat = MessageFormat.PNS;
            }

            if ( action == AdapterAction.ProcessTransaction && transType == TransactionType.Passthru )
                // only NetConnect messages have a TransactionType of "Passthru"
                ProcessNetConnect( args, retVal, metrics );
            else
            {
                switch ( action )
                {
                    case AdapterAction.ProcessTransaction:
                        ProcessOnline( action, transType, messageFormat, args, retVal, metrics );
                        break;

                    case AdapterAction.ProcessUpload:
                    case AdapterAction.ProcessDownload:
                        ProcessBatch( action, transType, messageFormat, protocol, args, retVal, metrics );
                        break;

                    case AdapterAction.None:
                        throw new AdapterException( "Invalid action" );
                }
            }

            return retVal;
        }


        /// <summary>
        /// This method implements the online action handler.  SLM and PNS messages do not require
        /// TransactionType, ORB messages do.
        /// </summary>
        /// <param name="action">The AdapterAction type</param>
        /// <param name="transType">The TransactionType</param>
        /// <param name="messageFormat">The MessageFormat Type</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <throws> DispatcherException</throws>
        /// <throws> RequestException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> AdapterException</throws>
        ///
        protected void ProcessOnline( AdapterAction action, TransactionType transType, MessageFormat messageFormat,
                KeySafeDictionary<string, string> args, KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            IRequest request = null;

            // debugging option
            var dumpFields = GetBooleanParam( args, "control.DumpFieldValues" );

            var tracenum = GetParam( args, "control.TraceNumber" );

            var configName = GetParam( args, "control.ConfigName" );
            if ( configName == null )
                throw new AdapterException( "control.ConfigName is required" );

            // Get configuration data from the config file, then override with any control values
            // in the input data
            var config = dispatcher.GetConfig( configName );
            SetConfigOverrideOptions( args, config );

            // Get the name of the SDK template to use for this transaction
            string templateName = null;
            if (transType == TransactionType.HeartBeat)
                templateName = "HeartBeat";
            else if ( messageFormat == MessageFormat.SLM )
                templateName = "NewTransaction";
            else if ( messageFormat == MessageFormat.PNS )
                templateName = "PNSNewTransaction";
            else if ( messageFormat == MessageFormat.ORB )
            {
                if ( transType == TransactionType.NONE )
                    throw new AdapterException( "control.TransactionType is required for Orbital transactions" );
                else
                    // Orbital template names are the same as their TransactionType
                    templateName = transType.ToString();
            }

            // Create the request object
            request = (IRequest)dispatcher.CreateRequest( templateName, config );

            // Copy control values from the input data to the request's config data
            CopyRequestData( args, request );

            // Orbital TraceNumber needs special handling
            if ( tracenum != null && messageFormat == MessageFormat.ORB )
                ((IRequestImpl)request).TransactionControlValues[ "TraceNumber" ] = tracenum;

            // update the metrics
            //metrics.setPointOfEntry(messageFormat);
            //metrics.setCommMode(config.Protocol);
            //request.Metrics = metrics ;

            // Verify that the protocol is valid for online transactions
            if ( !onlineCommModules.Contains( config.Protocol ) )
                throw new AdapterException($"Protocol {config.Protocol} not valid for action ProcessTransaction");

            if ( dumpFields )
                DumpFieldValues( request );

            IResponse resp = null;

            // call the SDK to process the request and get the response
            if (transType == TransactionType.HeartBeat)
                resp = dispatcher.SendHeartbeat( request );
            else
                resp = dispatcher.ProcessRequest( request );

            // Copy values from the response in to the response data map
            CopyResponseFields( resp, retVal, null );
            if ( messageFormat != MessageFormat.ORB )
                CopyResponseProperties( resp, retVal, null );
            if ( messageFormat == MessageFormat.SLM && config.Protocol == CommModule.TCPConnect )
                CopyRetryHostPort( resp, retVal );

            if ( dumpFields )
                DumpFieldValues( resp );
        }


        /// <summary>
        /// Handles the processing of SLM batch and PNS upload transactions
        /// </summary>
        /// <param name="action">TheAdapterAction type</param>
        /// <param name="transType">The TransactionType</param>
        /// <param name="messageFormat">The MessageFormat Type</param>
        /// <param name="protocol">A mid</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <throws> AdapterException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> DispatcherException</throws>
        /// <throws> SubmissionException</throws>
        /// <throws> RequestException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> InterruptedException</throws>
        ///
        protected void ProcessBatch( AdapterAction action, TransactionType transType, MessageFormat messageFormat,
                CommModule protocol, KeySafeDictionary<string, string> args, KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            var configName = GetParam( args, "control.ConfigName" );
            switch ( transType )
            {
                case TransactionType.Start:
                case TransactionType.Open:
                case TransactionType.Delete:
                    if ( configName == null )
                        throw new AdapterException( "control.ConfigName required for Start, Open, and Delete transactions" );
                    break;
                default:
                    break;
            }

            var batchID = GetParam( args, "control.BatchID" );
            if ( batchID == null && messageFormat != MessageFormat.PNS && transType != TransactionType.Delete )
                // PNS uploads don't use batchID, they use MID/TID
                throw new AdapterException( "control.BatchID is required" );

            // For Next, End, and Close transactions we need to get the message format from the cached descriptor
            // Note: message format of PNS is determined from an educated guess in the mapToCore method
            switch ( transType )
            {
                case TransactionType.Start:
                case TransactionType.Open:
                case TransactionType.Delete:
                    break;

                default:
                    if ( messageFormat != MessageFormat.PNS )
                    {
                        string mf = null;
                        switch ( action )
                        {
                            case AdapterAction.ProcessUpload:
                                mf = GetSubmissionDescriptor( batchID, REQUIRED ).Config.MessageFormat;
                                break;

                            case AdapterAction.ProcessDownload:
                                mf = ( (ResponseDescriptor)GetResponseDescriptor( batchID, REQUIRED ) ).ConfigData.MessageFormat;
                                break;

                            default:
                                break;
                        }

                        messageFormat = (MessageFormat)Enum.Parse( typeof( MessageFormat ), mf, true );
                    }
                    break;
            }

            switch ( messageFormat )
            {
                case MessageFormat.SLM:
                    ProcessSLMBatch( action, transType, messageFormat, batchID, configName, args, retVal, metrics );
                    break;

                case MessageFormat.PNS:
                    ProcessPNSBatch( transType, configName, args, retVal, metrics );
                    break;

                case MessageFormat.ORB:
                    throw new AdapterException($"Action {action} not supported for Orbital");

                default:
                    break;
            }

        }


        /// <summary>
        /// This method implements the SLM batch handler
        /// </summary>
        /// <param name="action">TheAdapterAction type</param>
        /// <param name="transType">The TransactionType</param>
        /// <param name="messageFormat">The MessageFormat Type</param>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="configName">The configuration name</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="metrics">The metrics collection class</param>
        ///
        protected void ProcessSLMBatch( AdapterAction action, TransactionType transType,
                MessageFormat messageFormat, string batchID, string configName, KeySafeDictionary<string, string> args,
                KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            // Get the ConfigName if required for this transaction type
            if ( transType == TransactionType.Start )
            {
                if ( configName == null )
                    throw new AdapterException( "control.ConfigName is required for transaction type \"start\"" );
            }
            else
            {
                if ( configName != null )
                    logger.Warn($"control.ConfigName not required for transaction type {transType}, ignored");
            }

            switch ( action )
            {
                case AdapterAction.ProcessUpload:
                    {
                        switch ( transType )
                        {
                            case TransactionType.Start:
                                DoCreateSubmission( args, batchID, configName, retVal, metrics );
                                break;
                            case TransactionType.Next:
                                DoAddOrder( args, batchID, configName );
                                break;
                            case TransactionType.End:
                                DoSendSubmission( args, batchID, retVal );
                                break;
                            case TransactionType.Cancel:
                                DoCancelUpload( batchID );
                                break;
                            default:
                                throw new AdapterException(
                                    $"TransactionType {transType} not supported for action ProcessUpload");
                        }
                        break;
                    }

                case AdapterAction.ProcessDownload:
                    {
                        switch ( transType )
                        {
                            case TransactionType.Start:
                                DoReceiveResponse( args, batchID, configName, retVal );
                                break;
                            case TransactionType.Next:
                                DoGetNext( args, batchID, retVal );
                                break;
                            case TransactionType.End:
                            case TransactionType.Cancel:
                                DoClose( batchID );
                                break;
                            case TransactionType.Open:
                                DoOpenFileForRead( args, batchID, retVal, configName );
                                break;
                            case TransactionType.Delete:
                                DoDeleteServerFile( args, configName, retVal );
                                break;
                            default:
                                throw new AdapterException(
                                    $"control.TransactionType {transType} not supported for action ProcessDownload");
                        }
                        break;
                    }

                default:
                    // should never get here, but handle it anyway just in case (but mostly to keep the IDE happy)
                    throw new AdapterException(
                        $"Deprecated action \"{action}\" got through to new API handler, something's fubar.");
            }
        }


        /// <summary>
        /// This method implements the NetConnect handler
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <returns> KeySafeDictionary{string,string} a List of the values created.</returns>
        protected KeySafeDictionary<string, string> ProcessNetConnect( KeySafeDictionary<string, string> args, KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            //metrics.setPointOfEntry(Metrics.PointOfEntry.NetConnect);
            //metrics.setCommMode(CommModule.HTTPSConnect);

            var configName = GetParam( args, "control.ConfigName" );
            if ( configName == null )
                throw new AdapterException( "control.ConfigName is required" );

            // Message is the ONLY data for NCM
            var message = GetParam( args, "Message" );
            if ( message == null || message.Trim().Length == 0 )
                throw new AdapterException( "The Message element is required" );

            // Get configuration data from the config file
            var configData = dispatcher.GetConfig( configName );

            // Overriding the HPS gotten from the config file is required.
            var hps = args[ "control.HostProcessingSystem" ];

            hps = hps == null ? configData.GetField( "HostProcessingSystem" ) : hps;
            if ( hps == null )
            {
                throw new AdapterException( "control.HostProcessingSystem is required for all NCM transactions if it is not set in the config file or to override the config." );
            }
            if ( !hps.Equals( "SLM" ) && !hps.Equals( "TCS" ) && !hps.Equals( "HCS" ) )
                throw new AdapterException( "valid values for control.HostProcesingSystem: SLM, HCS, or TCS" );

            // Metrics for NCM are not currently implemented on the gateways, but we include it in hopes of a
            // brighter future.
            // Even though not used they will be included in the MIME headers the SDK sends to the gateway.
            var mp = new MessageProcessor( new DispatcherFactory() );
            //mp.setMetrics(metrics);

            // Override the configData with any control values in the input
            SetNCConfigOptions( args, configData, mp );

            var respEncoded = mp.Process( message, configData );
            retVal[ "Response" ] = respEncoded;

            return retVal;
        }


        /// <summary>
        /// This method handles the processing of the ProcessUpload/Start command
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="configName">A mid</param>
        /// <param name="metrics">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> DispatcherException</throws>
        /// <throws> SubmissionException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> RequestException </throws>
        /// <throws> AdapterException </throws>
        ///
        public void DoCreateSubmission( KeySafeDictionary<string, string> args, string batchID, string configName,
                KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            VerifyUniqueBatchID( batchID );

            var passwd = GetParam( args, "control.SubmissionFilePassword" );
            if ( passwd == null )
            {
                throw new AdapterException( "control.SubmissionFilePassword is required" );
            }

            var submName = GetParam( args, "control.SubmissionName" );
            if ( submName == null )
            {
                // SubmissionNameRoot is not documented, but is supported to make testing easier
                var root = GetParam( args, "control.SubmissionNameRoot" );
                if ( root != null )
                {
                    var config = Configurator.GetInstance().GetProtocolConfig( configName );
                    var fm = new FileManager();
                    submName = fm.CreateTempName( root, FileType.Outgoing, config );

                }
                else
                {
                    throw new AdapterException( "control.SubmissionName is required" );
                }
            }

            var subm = (ISubmissionDescriptor)dispatcher.GetSubmission( submName, passwd, configName );
            var configData = subm.Config;
            SetConfigOverrideOptions( args, configData );

            if ( !batchCommModules.Contains( configData.Protocol ) )
                throw new AdapterException($"Protocol {configData.Protocol} not valid for this transaction");

            var headRequest = dispatcher.CreateRequest( "SubmissionHeader", configData );
            CopyRequestData( args, headRequest );
            subm.CreateBatch( headRequest );

            //metrics.SetPointOfEntry(configData.GetField("MessageFormat"));
            // metrics.setCommMode(configData.Protocol);

            //subm.SetMetrics( metrics );

            submissionCache[ batchID ] = subm;

            retVal[ "SubmissionName" ] = subm.Name;
            retVal[ "FileName" ] = subm.FileName;
        }


        /// <summary>
        /// This method handles the processing of the ProcessUpload/Next command
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="batchID">A mid</param>
        /// <param name="configName">A mid</param>
        ///
        public void DoAddOrder( KeySafeDictionary<string, string> args, string batchID, string configName )
        {
            var batchRequest = GetSubmissionDescriptor( batchID, REQUIRED );

            var dumpFields = GetBooleanParam( args, "control.DumpFieldValues" );

            var configData = batchRequest.Config;
            SetConfigOverrideOptions( args, configData );
            if ( !batchCommModules.Contains( configData.Protocol ) )
                throw new AdapterException($"Protocol {configData.Protocol} not valid for this transaction");

            var order = dispatcher.CreateRequest( "SubmissionOrder", batchRequest.Config );
            CopyRequestData( args, order );
            batchRequest.Add( order );

            if ( dumpFields )
                DumpFieldValues( order );
        }


        /// <summary>
        /// This method handles the processing of the ProcessUpload/End command.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="retVal">The return value</param>
        /// <throws> DispatcherException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> AdapterException </throws>
        /// <throws> SubmissionException </throws>
        ///
        public void DoSendSubmission( KeySafeDictionary<string, string> args, string batchID, KeySafeDictionary<string, string> retVal )
        {
            if ( args[ "control.ConfigName" ] != null )
            {
                throw new AdapterException( "control.ConfigName is not valid for transaction type End.  "
                        + "ConfigName can only be set during Start." );
            }

            var subDesc = GetSubmissionDescriptor( batchID, REQUIRED );
            var configData = subDesc.Config;
            SetConfigOverrideOptions( args, configData );
            if ( !batchCommModules.Contains( configData.Protocol ) )
                throw new AdapterException($"Protocol {configData.Protocol} not valid for this transaction");

            // Explicitly close the batch so that we can add the totals to the response
            subDesc.CloseBatch();
            retVal[ "OrderCount" ] = subDesc.OrderCount.ToString();
            retVal[ "TotalAmounts" ] = subDesc.TotalAmounts.ToString();
            retVal[ "TotalRecords" ] = subDesc.TotalRecords.ToString();
            retVal[ "TotalRefundAmounts" ] = subDesc.TotalRefundAmounts.ToString();
            retVal[ "TotalSalesAmounts" ] = subDesc.TotalSalesAmounts.ToString();

            dispatcher.SendSubmission( subDesc );
            submissionCache.Remove( batchID );
        }


        /// <summary>
        /// Handles the ProcessUpload/Cancel command.
        /// </summary>
        /// <param name="batchID">The Batch ID</param>
        /// <throws> AdapterException</throws>
        /// <throws> ConfiguratorException</throws>
        ///
        public void DoCancelUpload( string batchID )
        {
            var subm = GetSubmissionDescriptor( batchID, REQUIRED );
            subm.Close();

            var fac = new DispatcherFactory();
            fac.RemoveSubmission( subm.Name );

            submissionCache.Remove( batchID );
            try
            {
                File.Delete( subm.FileName );
            }
            catch ( Exception ) { }
        }


        /// <summary>
        /// This method handles the processing of the ProcessDownload/Start command.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="configName">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> DispatcherException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> RequestException</throws>
        /// <throws> AdapterException</throws>
        ///
        public void DoReceiveResponse( KeySafeDictionary<string, string> args, string batchID, string configName, KeySafeDictionary<string, string> retVal )
        {
            VerifyUniqueBatchID( batchID );

            var config = dispatcher.GetConfig( configName );

            switch ( config.Protocol )
            {
                case CommModule.SFTPBatch:
                    ReceiveSFTPBatch( batchID, args, config, retVal );
                    break;
                case CommModule.TCPBatch:
                    ReceiveTCPBatch( batchID, args, configName, retVal );
                    break;
                default:
                    throw new AdapterException($"{config.Protocol} protocol not applicable for this transaction");
            }

        }


        /// <summary>
        /// Receives a batch response file via SFTP.
        /// </summary>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="config">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> ConfiguratorException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        /// <throws> DispatcherException</throws>
        ///
        public void ReceiveSFTPBatch( string batchID, KeySafeDictionary<string, string> args,
                ConfigurationData config, KeySafeDictionary<string, string> retVal )
        {
            var respType = GetParam( args, "control.ResponseType" );
            if ( respType == null )
                throw new AdapterException( "control.ResponseType is required for the ProcessDownload/Start action" );
            else if ( !responseTypes.Contains( respType ) )
                throw new AdapterException(
                    $"control.ResponseType valid values for SFTPBatch download: {responseTypesString}");

            SetConfigOverrideOptions( args, config );

            var resp = dispatcher.ReceiveResponse( respType, config );

            // If a response was received check if it's valid or if it's a NetConnect error file.
            if ( resp != null )
            {
                retVal[ "ResponseReceived" ] = "true";

                var err = resp.ErrorResponse;
                if ( err != null )  // it's an error
                {
                    CopyNetConnectErrorFields( err, retVal );
                    return;
                }

                if ( resp.ResponseFileType.Equals( "Response" ) )
                {
                    CopyResponseDescriptorFields( resp, retVal );
                }

                retVal[ "SFTPFileName" ] = resp.SFTPFilename;
                retVal[ "FileName" ] = resp.FileName;
                retVal[ "SubmissionName" ] = resp.Name;
                retVal[ "ResponseType" ] = resp.ResponseFileType;
                retVal[ "HasNext" ] = resp.HasNext.ToString();

                responseCache[ batchID ] = resp;
            }
            else
                retVal[ "ResponseReceived" ] = "false";
        }


        /// <summary>
        /// Receives a batch response file via TCP.
        /// </summary>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="configName">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> DispatcherException</throws>
        /// <throws> RequestException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        ///
        public void ReceiveTCPBatch( string batchID, KeySafeDictionary<string, string> args,
                string configName, KeySafeDictionary<string, string> retVal )
        {
            IRequest rfrRequest = null;

            // Note regarding DFR's: when downloading a DFR the ResponseType is just "Response", which DFR gets
            // downloaded is determined by the PID and SID used

            var respType = GetParam( args, "control.ResponseType" );
            if ( respType == null )
                throw new AdapterException( "control.ResponseType is required for the ProcessDownload/Start action" );
            else if ( !respType.Equals( "Response" ) && !respType.Equals( "RFS" ) )
                throw new AdapterException( "control.ResponseType valid values for TCPBatch download: Response, RFS" );
            if ( respType.Equals( "RFS" ) )
                rfrRequest = dispatcher.CreateRequest( "SubmissionStatus", configName );
            else
                rfrRequest = dispatcher.CreateRequest( "SubmissionResponse", configName );
            CopyRequestData( args, rfrRequest );
            SetConfigOverrideOptions( args, rfrRequest.Config );

            var resp = dispatcher.ReceiveResponse( rfrRequest );

            if ( resp != null )
            {
                retVal[ "ResponseReceived" ] = "true";

                var batchRespType = resp.ResponseFileType;
                if ( batchRespType.Equals( "Response" ) )
                {
                    CopyResponseDescriptorFields( resp, retVal );
                    retVal[ "SubmissionName" ] = resp.Name;
                }

                retVal[ "FileName" ] = resp.FileName;
                retVal[ "ResponseType" ] = resp.ResponseFileType;
                retVal[ "HasNext" ] = resp.HasNext.ToString();
                responseCache[ batchID ] = resp;
            }
            else
                retVal[ "ResponseReceived" ] = "false";
        }


        /// <summary>
        /// This method handles the ProcessDownload/Next command.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="batchID">The Batch ID</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> SubmissionException</throws>
        /// <throws> ResponseException</throws>
        /// <throws> AdapterException</throws>
        ///
        public void DoGetNext( KeySafeDictionary<string, string> args, string batchID, KeySafeDictionary<string, string> retVal )
        {
            var response = GetResponseDescriptor( batchID, REQUIRED );
            var resp = response.GetNext();

            if ( GetBooleanParam( args, "control.DumpFieldValues" ) )
                DumpFieldValues( resp );

            CopyResponseFields( resp, retVal, null );
            CopyResponseProperties( resp, retVal, null );

            retVal[ "HasNext" ] = response.HasNext.ToString();
        }


        /// <summary>
        /// This method handles the ProcessDownload/End.
        /// </summary>
        /// <param name="batchID">A mid</param>
        /// <throws> SubmissionException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> AdapterException</throws>
        ///
        public void DoClose( string batchID )
        {
            var batchRequest = GetSubmissionDescriptor( batchID, NOT_REQUIRED );
            if ( batchRequest != null )
            {
                batchRequest.Close();
                submissionCache.Remove( batchID );
            }

            var batchResponse = GetResponseDescriptor( batchID, NOT_REQUIRED );
            if ( batchResponse != null )
            {
                batchResponse.Close();
                responseCache.Remove( batchID );
            }
        }


        public void DoOpenFileForRead( KeySafeDictionary<string, string> args, string batchID, KeySafeDictionary<string, string> retVal, string configName )
        {
            VerifyUniqueBatchID( batchID );

            IResponseDescriptor respDesc = null;
            var filename = GetParam( args, "control.FileName" );
            if ( filename == null )
                throw new AdapterException( "control.FileName is required for ProcessDownload/Open action." );

            var password = GetParam( args, "control.SubmissionFilePassword" );
            if ( password == null )
                throw new AdapterException( "control.SubmissionFilePassword is required for ProcessDownload/Open action." );

            respDesc = dispatcher.OpenDescriptor( filename, password, configName );
            SetConfigOverrideOptions( args, respDesc.ConfigData );
            responseCache[ batchID ] = respDesc;

            CopyResponseDescriptorFields( respDesc, retVal );
            retVal[ "SubmissionName" ] = respDesc.Name;
            retVal[ "FileName" ] = respDesc.FileName;
        }


        /**
         * This method handles the processing of the DeleteServerFile batch command.
         * @param args
         * @param retVal
         * @throws DispatcherException
         * @throws ConfiguratorException
         * @throws AdapterException
         */
        public void DoDeleteServerFile( KeySafeDictionary<string, string> args, string configName, KeySafeDictionary<string, string> retVal )
        {
            var fname = GetParam( args, "control.SFTPFileName" );
            if ( fname == null )
                throw new AdapterException( "control.SFTPFileName is required for Delete transaction" );

            var config = dispatcher.GetConfig( configName );
            SetConfigOverrideOptions( args, config );

            if ( config[ "RSAMerchantPassPhrase" ] == null )
                throw new AdapterException( "control.RSAMerchantPassPhrase is required for Delete transaction" );

            var success = dispatcher.DeleteServerFile( fname, config );

            retVal[ "FileDeleted" ] = success.ToString().ToLower();
        }



        /// <summary>
        /// Handles the initial processing of startPNSUpload, processPNSUpload, and
        /// endPNSUpload commands. This method makes a call to
        /// processPNSUploadAction() which handles the main processing and is
        /// overridden in MapAdapterMultithreaded which ensures all the commands run
        /// in the same thread.
        ///</summary>
        /// <param name="transType">The TransactionType</param>
        /// <param name="configName">The configuration name</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <throws>DispatcherException</throws>
        /// <throws>RequestException</throws>
        /// <throws>ConfiguratorException</throws>
        /// <throws>ResponseException</throws>
        /// <throws>InterruptedException</throws>
        /// <throws>AdapterException</throws>
        public void ProcessPNSBatch( TransactionType transType, string configName, KeySafeDictionary<string, string> args, KeySafeDictionary<string, string> retVal, SDKMetrics metrics )
        {
            if ( configName == null && transType == TransactionType.Start )
                throw new AdapterException( "control.ConfigName is required for transaction type Start" );

            // Unlike SLM uploads that use BatchID, the PNS cache key is a combination of MID and TID
            var mid = args[ "Bit42.CardAcquirerId" ];
            if ( mid == null )
                throw new AdapterException( "Bit42.CardAcquirerId (MID) is required for PNS uploads" );

            var tid = args[ "Bit41.CardAcquirerTerminalId" ];
            if ( tid == null )
                throw new AdapterException( "Bit41.CardAcquirerTerminalId (TID) is required for PNS uploads" );

            var uploadKey = mid + tid;

            var dumpFields = GetBooleanParam( args, "control.DumpFieldValues" );

            IRequest request = null;
            if ( transType != TransactionType.Cancel )
            {
                request = CreatePNSUploadRequest( transType, uploadKey, args, configName, metrics );

                if ( dumpFields )
                    DumpFieldValues( request );

                var response = ProcessPNSUpload( request, transType, uploadKey );

                CopyResponseFields( response, retVal, null );

                if ( dumpFields )
                    DumpFieldValues( response );
            }
            else
            {
                DoCancelPNSUpload( mid, tid );
            }

            if ( transType == TransactionType.End )
            {
                configCache.Remove( uploadKey );
            }
        }


        /// <summary>
        /// Handles the processing of PNSUpload actions in a single-threaded
        /// environment. In multi-threaded environments this method is overridden in
        /// the MapAdapterMultithreaded class.
        ///</summary>
        /// <param name="request">The IRequest instance</param>
        /// <param name="transType">The TransactionType</param>
        /// <param name="uploadKey">A mid</param>
        /// <returns>The Response that is created.</returns>
        /// <throws> DispatcherException</throws>
        /// <throws> InterruptedException</throws>
        /// <throws> AdapterException</throws>
        ///
        public IResponse ProcessPNSUpload( IRequest request, TransactionType transType, string uploadKey )
        {
            IResponse response = null;

            switch ( transType )
            {
                case TransactionType.Start:
                    response = dispatcher.StartPNSUpload( request );
                    break;

                case TransactionType.Next:
                    response = dispatcher.ProcessPNSUpload( request );
                    break;

                case TransactionType.End:
                    response = dispatcher.EndPNSUpload( request );
                    break;

                default:
                    break;
            }

            return response;
        }


        /// <summary>
        /// Creates a populated PNS upload request.  If it's for a Start request then the configuration
        /// data gets cached so it can be used in subsequent ProcessPNSUpload and EndPNSUpload requests for the
        /// same mid/tid combo.
        /// </summary>
        /// <param name="transType">The TransactionType</param>
        /// <param name="uploadKey">A mid</param>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="configName">A mid</param>
        /// <param name="metrics">The metrics collection class</param>
        /// <returns>The IRequest that is created.</returns>
        /// <throws> DispatcherException</throws>
        /// <throws> RequestException</throws>
        /// <throws> ConfiguratorException</throws>
        /// <throws> AdapterException</throws>
        ///
        public IRequest CreatePNSUploadRequest( TransactionType transType, string uploadKey, KeySafeDictionary<string, string> args,
                string configName, SDKMetrics metrics )
        {
            IRequest request = null;
            ConfigurationData configData = null;

            var messageType = GetParam( args, "MessageHeader.MessageType" );
            if ( messageType == null )
                throw new AdapterException( "MessageHeader.MessageType is required for PNS uploads" );

            // Create a new transaction request object
            if ( transType == TransactionType.Start )
            {
                // All config options must be set during Start, then the config gets cached
                // and reused in subsequent requests for this mid/tid
                configData = dispatcher.GetConfig( configName );
                SetConfigOverrideOptions( args, configData );
                configCache[ uploadKey ] = configData;

                if ( configData.GetField( "HostProcessingSystem" ) == null )
                    throw new AdapterException( "HostProcessingSystem must be set in the configuration file, or control.HostProcessingSystem is required in the transaction" );

                request = (IRequestImpl)dispatcher.CreateRequest( "PNSNewTransaction", configData );
            }
            else if ( transType == TransactionType.Next || transType == TransactionType.End )
            {
                // Next and End actions both reuse the configData set during StartPNSUpload
                configData = configCache[ uploadKey ];
                if ( configData == null )
                    throw new AdapterException( "Couldn't find cached config data for mid/tid " + uploadKey );
                request = (IRequestImpl)dispatcher.CreateRequest( "PNSNewTransaction", configData );
            }
            else
            {
                throw new AdapterException($"TransactionType {transType} not supported for PNS Upload");
            }

            CopyRequestData( args, request );
            request.SetField( "MessageHeader.MessageType", messageType );

            // The request already contains a metrics object created in the Request constructor, but it does not
            // include the metrics data passed in to this method from Umpire, so we replace the metrics in the
            // request with this one.
            metrics.SetCommMode( configData.Protocol );
            metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Tandem;
            ((IRequestImpl)request).Metrics = metrics;

            return request;
        }


        public void DoCancelPNSUpload( string mid, string tid )
        {
            var uploadKey = mid + tid;
            ConfigurationData configData = null;
            configData = configCache[ uploadKey ];
            if ( configData == null )
                throw new AdapterException($"Upload cache for mid={mid}, tid={tid} not found");

            dispatcher.CancelPNSUpload( mid, tid, configData );
        }



        /// <summary>
        /// Creates and returns an Orbital request of the type specified by transType.
        /// </summary>
        /// <param name="transType">The TransactionType</param>
        /// <param name="configName">A mid</param>
        /// <returns>The IRequestImpl of the new Request.</returns>
        /// <throws> AdapterException</throws>
        ///
        public IRequestImpl CreateOrbitalRequest( string transType, string configName )
        {
            IRequest request = null;

            try
            {
                request = dispatcher.CreateRequest( transType, configName );
            }
            catch ( DispatcherException ex )
            {
                throw new AdapterException(
                    $"Unable to create Orbital transaction. Verify that the TransactionType '{transType}' is correct.", ex );
            }

            return (IRequestImpl)request;
        }


        /// <summary>
        /// Copy all the fields in the response into a name/value response KeySafeDictionary.
        /// </summary>
        /// <param name="resp">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="prefix">A mid</param>
        /// <throws> ResponseException</throws>
        ///
        protected static void CopyResponseFields( IResponse resp, KeySafeDictionary<string, string> retVal, string prefix )
        {
            if ( resp != null )
            {
                if ( prefix != null )
                    prefix += ".";
                else
                    prefix = "";

                var elements = resp.ResponseFieldIDs;

                // copy response name/value pairs into the return KeySafeDictionary
                foreach ( var name in elements )
                {
                    var key = prefix + name;
                    if ( !retVal.ContainsKey( key ) )
                        retVal[ key ] = resp.GetField( name );
                }
            }
        }


        /// <summary>
        /// Copies the retry host and port to the retVal map, if they are present in the response.
        /// </summary>
        /// <param name="resp">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        ///
        public static void CopyRetryHostPort( IResponse resp, KeySafeDictionary<string, string> retVal )
        {
            // Calls to getHost and getPort are only valid for TCPConnect to Salem, all
            // others will throw an exception that can be ignored.

            var host = resp.Host;
            if ( host != null && host.Length > 0 )
                retVal[ "RetryHost" ] = host;

            var port = resp.Port;
            if ( port != null && port.Length > 0 )
            {
                retVal[ "RetryPort" ] = port;
            }

        }


        /// <summary>
        /// Copies batch fields and properties for the Header, BatchTotals, Totals, and Trailer of the response to
        /// the retVal map.
        /// </summary>
        /// <param name="resp">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        ///
        protected static void CopyResponseDescriptorFields( IResponseDescriptor resp, KeySafeDictionary<string, string> retVal )
        {
            if ( !resp.ResponseFileType.Equals( "Response" ) && !resp.ResponseFileType.Equals( "Batch" ) )
                return;

            retVal[ "IsNetConnectError" ] = "false";

            var str = resp.SFTPFilename;
            if ( str != null )
            {
                retVal[ "SFTPFileName" ] = str;
            }

            IResponse holder = null;
            holder = resp.Header;
            if ( holder != null )
            {
                CopyResponseFields( holder, retVal, "Header" );
            }

            holder = resp.BatchTotals;
            if ( holder != null )
            {
                CopyResponseFields( holder, retVal, "BatchTotals" );
            }

            holder = resp.Totals;
            if ( holder != null )
            {
                CopyResponseFields( holder, retVal, "Totals" );
            }

            holder = resp.Trailer;
            if ( holder != null )
            {
                CopyResponseFields( holder, retVal, "Trailer" );
            }
        }


        /// <summary>
        /// Copy properties from the batch response to the retVal map.
        /// </summary>
        /// <param name="resp">A mid</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <param name="prefix">Wither "Header", "BatchTotals", "Totals", or "Trailer".</param>
        protected static void CopyResponseProperties( IResponse resp, KeySafeDictionary<string, string> retVal, string prefix )
        {
            if ( resp != null )
            {
                if ( prefix != null )
                    prefix += ".";
                else
                    prefix = "";

                retVal[ prefix + "NumExtraFields" ] = resp.NumExtraFields.ToString();
                retVal[ prefix + "IsConversionError" ] = resp.IsConversionError.ToString();
                retVal[ prefix + "LeftoverData" ] = resp.LeftoverData;
                retVal[ prefix + "ErrorDescription" ] = resp.ErrorDescription;
                retVal[ prefix + "HasExtraFields" ] = resp.HasExtraFields.ToString();
            }
        }


        /// <summary>
        /// copies all field values from a NetConnect error response in to the retVal map.
        /// </summary>
        /// <param name="ncError">A IResponse instance with errors</param>
        /// <param name="retVal">A KeySafeDictionary{string,string} with the return values</param>
        /// <throws> ResponseException</throws>
        /// <throws> SubmissionException</throws>
        ///
        protected static void CopyNetConnectErrorFields( IResponse ncError, KeySafeDictionary<string, string> retVal )
        {
            retVal[ "IsNetConnectError" ] = "true";

            foreach ( var name in ncError.ResponseFieldIDs )
            {
                retVal[ name ] = ncError.GetField( name );
            }
        }



        /// <summary>
        /// Control parameters in the args map override settings from the config file.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="configData">The ConfigurationData</param>
        /// <throws> ConfiguratorException</throws>
        ///
        protected static void SetConfigOverrideOptions( KeySafeDictionary<string, string> args, ConfigurationData configData )
        {
            // Performs an iteration over the args hashmap while safely removing args via the iterator
            IEnumerator<KeyValuePair<string, string>> i = args.GetEnumerator();
            while ( i.MoveNext() )
            {
                var arg = i.Current;
                var key = arg.Key;
                if ( key != null && key.StartsWith( "control." ) )
                {
                    // these are special control directives that are set in the request, not the config data
                    if ( key.Equals( "control.DestinationHost" ) || key.Equals( "control.DestinationPort" ) )
                        continue;

                    var fieldName = key.Substring( 8 );
                    var value = arg.Value;
                    if ( fieldName != null )
                    {
                        configData.SetField( fieldName, value, true );
                    }
                }
            }
        }


        /// <summary>
        /// Copy configuration option from the input KeySafeDictionary to the NC message processor.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="configData">The ConfigurationData</param>
        /// <param name="mp">A MessageProcessor</param>
        /// <throws> ConfiguratorException</throws>
        ///
        protected static void SetNCConfigOptions( KeySafeDictionary<string, string> args, ConfigurationData configData, MessageProcessor mp )
        {
            // Performs an iteration over the args hashmap while safely removing args via the iterator
            IEnumerator<KeyValuePair<string, string>> i = args.GetEnumerator();
            while ( i.MoveNext() )
            {
                var arg = i.Current;
                var key = arg.Key;
                if ( key != null && key.StartsWith( "control." ) )
                {
                    var fieldName = key.Substring( 8 );
                    var value = arg.Value;
                    if ( fieldName != null )
                    {
                        if ( fieldName.Equals( "CaptureMode" ) )
                        {
                            mp.CaptureMode = value;
                        }
                        else if ( fieldName.Equals( "MerchantID" ) )
                        {
                            mp.MerchantID = value;
                        }
                        else if ( fieldName.Equals( "MessageType" ) )
                        {
                            mp.MessageType = value;
                        }
                        else if ( fieldName.Equals( "TerminalID" ) )
                        {
                            mp.TerminalID = value;
                        }
                        else
                        {
                            configData.SetField( fieldName, value, true );
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Copy name/value data pairs from input map to request.  Control parameters are ignored.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="request">The IRequest instance</param>
        ///
        protected static void CopyRequestData( KeySafeDictionary<string, string> args, IRequest request )
        {
            // DestinationHost and Port are special control directives that are set in the request, not the config data
            var dhost = args[ "control.DestinationHost" ];
            if ( dhost != null )
            {
                request.Host = dhost;
                args.Remove( "control.DestinationHost" );
            }

            var dport = args[ "control.DestinationPort" ];
            if ( dport != null )
            {
                request.Port = dport;
                args.Remove( "control.DestinationPort" );
            }
            IEnumerator<KeyValuePair<string, string>> i = args.GetEnumerator();

            while ( i.MoveNext() )
            {
                var arg = i.Current;
                var key = arg.Key;

                if ( !key.StartsWith( "control." ) )
                    request.SetField( key, arg.Value );
            }
        }


        /// <summary>
        /// Gets the cached SubmissionDescriptor associated with the given BatchID.
        /// </summary>
        /// Throws a MapAdapterException if not found in the cache and 'isRequired' is true.
        /// <param name="batchID">The batch Id</param>
        /// <param name="isRequired">Required request</param>
        /// <returns>The named SubmissionDescriptor.</returns>
        /// <throws>AdapterException</throws>
        ///
        protected static ISubmissionDescriptor GetSubmissionDescriptor( string batchID, bool isRequired )
        {
            var subDesc = submissionCache[ batchID ];
            if ( subDesc == null && isRequired )
                throw new AdapterException($"control.BatchID: {batchID} not found in cache");

            return subDesc;
        }


        /// <summary>
        /// Gets the cached ResponseDescriptor associated with the given BatchID.
        /// </summary>
        /// Throws a AdapterException if not found in the cache and 'isRequired' is true.
        /// <param name="batchID">The Batch ID</param>
        /// <param name="isRequired">Required request</param>
        /// <returns>The named ResponseDescriptor .</returns>
        /// <throws>AdapterException</throws>
        ///
        protected static IResponseDescriptor GetResponseDescriptor( string batchID, bool isRequired )
        {
            var respDesc = responseCache[ batchID ];
            if ( respDesc == null && isRequired )
                throw new AdapterException($"control.BatchID: {batchID} not found in cache");
            return respDesc;
        }


        /// <summary>
        /// Verifies that the batchId is Unique and throws an AdapterException if the
        /// given BatchID already exists in the submissionMap or responseMap.
        /// </summary>
        /// <param name="batchID">The Batch ID</param>
        /// <throws>AdapterException</throws>
        ///
        protected static void VerifyUniqueBatchID( string batchID )
        {
            if ( submissionCache[ batchID ] != null || responseCache[ batchID ] != null )
                throw new AdapterException($"control.BatchID: {batchID} already in use");
        }


        /// <summary>
        ///  Returns the thread ID in brackets, to be used to prefix log messages.
        ///  </summary>
        ///  <returns>The thread ID .</returns>
        ///
        public static string GetThreadID()
        {
            return "[" + Thread.CurrentThread.ManagedThreadId + "]";
        }


        /// <summary>
        /// </summary>
        /// Dumps all field values of a request
        /// <param name="request">The IRequest to ouput to debug</param>
        ///
        public static void DumpFieldValues( IRequest request )
        {
            logger.Debug( "--- Dump of Request Fields:" );
            logger.Debug( request.DumpFieldValues() );
            logger.Debug( "--- End of Dump ---" );
        }


        /// <summary>
        /// Dumps all field values of a response
        /// </summary>
        /// <param name="response">The IResponse to ouput to debug</param>
        ///
        public static void DumpFieldValues( IResponse response )
        {
            logger.Debug( "--- Dump of Response Fields:" );
            logger.Debug( response.DumpFieldValues() );
            logger.Debug( "--- End of Dump ---" );
        }


        /// <summary>
        /// Gets the string value of the named parameter in the args map, then removes it from the map.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="paramName">A mid</param>
        /// <returns>The string value of the named parameter, or null if it's not in the map.</returns>
        ///
        public string GetParam( KeySafeDictionary<string, string> args, string paramName )
        {
            var value = args[ paramName ];
            args.Remove( paramName );
            return value;
        }


        /// <summary>
        /// Gets the boolean value of the named parameter in the args map, then removes it from the map.
        /// </summary>
        /// <param name="args">List arguments as a KeySafeDictionary{string,string}</param>
        /// <param name="name">A mid</param>
        /// <returns> The boolean value of the named parameter, or null if it's not in the map.</returns>
        ///
        public bool GetBooleanParam( KeySafeDictionary<string, string> args, string name )
        {
            var str = GetParam( args, name );
            return "true".Equals( str, StringComparison.OrdinalIgnoreCase );
        }


        /// <summary>
        /// This method is called when a transaction includes a "control.ResetBatch" parameter.
        /// It loops through the submissionMap and responseMap, closes each descriptor and
        /// deletes the file it referred to, then deletes the PNS upload cache in the dispatcher.
        /// </summary>
        ///
        public void ResetBatch()
        {
            var msdkHome = dispatcher.HomeDirectory;
            ConfigurationData configData = null;
            IEnumerator<KeyValuePair<string, ISubmissionDescriptor>> i = submissionCache.GetEnumerator();

            while ( i.MoveNext() )
            {
                var arg = i.Current;
                var key = arg.Key;

                var subm = arg.Value;
                if ( configData == null )
                {
                    configData = subm.Config;
                }
                subm.Close();
                dispatcher.GetSubmission( subm.FileName,subm.Password,subm.Config );
                try
                {
                    File.Delete( subm.FileName );
                }
                catch ( Exception ) { }
            }
            submissionCache.Clear();


            IEnumerator<KeyValuePair<string, IResponseDescriptor>> respC = responseCache.GetEnumerator();

            while ( respC.MoveNext() )
            {
                var arg = respC.Current;
                var key = arg.Key;

                var resp = arg.Value;
                if ( resp != null )
                {
                    try
                    {
                        if ( configData == null )
                        {
                            configData = resp.ConfigData;
                        }
                        resp.Close();

                        File.Delete( resp.FileName );
                    }
                    catch ( Exception ) { }
                }
            }
            responseCache.Clear();

            var outgoing = configData != null ? configData.GetField( "OutgoingBatchDirectory" ) : "outgoing";
            var incoming = configData != null ? configData.GetField( "IncomingBatchDirectory" ) : "incoming";

            // delete all remaining files in the outgoing directory
            try
            {
                if ( outgoing != null )
                {
                    if ( !Utils.IsAbsolutePath( outgoing ) )
                        outgoing = msdkHome + "\\" +outgoing;
                    foreach ( var file in Directory.GetFiles( outgoing ) )
                        File.Delete( file );
                }
            }
            catch ( Exception ) { }

            // delete all remaining files in the incoming directory
            try
            {
                if ( incoming != null )
                {
                    if ( !Utils.IsAbsolutePath( incoming ) )
                        incoming = msdkHome + "\\" + incoming;
                    foreach ( var file in Directory.GetFiles( incoming ) )
                        File.Delete( file );
                }
            }
            catch ( Exception ) { }

            dispatcher.Dispose();  // clears out the PNSUploads cache
            configCache.Clear();
        }


        /**
        * This method is called when a transaction includes a "control.ReleaseBatchID" parameter.
        * It removes the specified batch ID from the submission and response caches
        */
        public static void ReleaseBatchID(string id )
        {
            submissionCache.Remove( id );
            responseCache.Remove( id );
            configCache.Remove( id );
        }

    }
}
