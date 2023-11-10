#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Net;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK
{
    public class MessageProcessor
    {
        private IDispatcherFactory factory;
        private WebHeaderCollection responseMimeHeaders = new WebHeaderCollection();
        private SDKMetrics Metrics { get; set; }

        public MessageProcessor( IDispatcherFactory factory )
        {
            this.factory = factory;
            MakeMetrics();
        }

        public MessageProcessor() : this( new DispatcherFactory() )
        {
        }

        /**
         * Takes a Base64 encoded string and processes it. The resulting response is returned Base64 encoded.
         * @param data - A Base-64 encoded string containing the message to be sent to the server.
         * @param config - Configuration values set for this transaction.
         * @return A Base-64 encoded string containing the response from the server.
         * @throws DispatcherException
         */
        public string Process( string data, ConfigurationData config )
        {
            return Convert.ToBase64String( Process( Convert.FromBase64String( data ), config ) );
        }

        /**
         * Takes a Base64 encoded string and processes it. The resulting response is returned Base64 encoded.
         * @param data - A byte array containing the message to be sent to the server.
         * @param config - Configuration values set for this transaction.
         * @return A byte array containing the response from the server.
         * @throws DispatcherException
         */
        public byte[] Process( byte[] data, ConfigurationData config )
        {
            var mime = new TransactionControlValues();
            SetDefaultValues( mime, config );

            var capture = CaptureMode != null ? CaptureMode : config[ "HostProcessingSystem" ];

            MessageType = MessageType == null ? config[ "MessageType" ] : MessageType;

            if ( MessageType == null )
            {
                var msg = "The required field MessageType is not set.";
                factory.Logger.Error( msg );
                throw new DispatcherException( Error.RequiredFieldNotSet, msg );
            }

            if ( capture == null )
            {
                var msg = "The HostProcessingSystem is not set for a passthrough operation.";
                factory.Logger.Error( msg );
                throw new DispatcherException( Error.RequiredFieldNotSet, msg );
            }

            mime[ "ContentType" ] = MessageType + "/" + capture;
            mime[ "StatelessTransaction" ] = "true";
            mime[ "AuthTid" ] = TerminalID;
            mime[ "AuthMid" ] = MerchantID;
            mime[ "Interface-Version" ] = Metrics.ToBase64();

            //System.out.println( mime.dumpFieldValues() );

            var processor = factory.MakeOnlineProcessor();

            var args = processor.CompleteTransaction( data, config, mime, null );
            responseMimeHeaders = args.MimeHeaders;
            return args.Data;
        }

        private void MakeMetrics()
        {
            Metrics = new SDKMetrics(SDKMetrics.Service.Dispatcher, SDKMetrics.ServiceFormat.API);
            Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.NetConnect;
            Metrics.CommModeMetric = SDKMetrics.CommMode.Internet;
            Metrics.MessageOriginMetric = SDKMetrics.MessageOrigin.Local;
        }

        private void SetDefaultValues( TransactionControlValues controlValues, ConfigurationData configData )
        {
            var defaults = factory.Config.GetDefaultValues( configData );

            if ( defaults == null )
            {
                return;
            }

            for ( var i = 0; i < defaults.Length; i++ )
            {
                if ( defaults[ i ].destination.Equals( "TransactionControlValues" ) )
                {
                    controlValues[ defaults[ i ].name ] = defaults[ i ].value;
                }
                if ( defaults[ i ].destination.Equals( "ConfigurationData" ) &&
                    !configData.ModifiedFields.Contains( defaults[ i ].name ) )
                {
                    configData[ defaults[ i ].name ] = defaults[ i ].value;
                }
            }
        }



        /// <summary>
        /// Gets or sets the Terminal ID.
        /// </summary>
        public string TerminalID { get; set; }

        /// <summary>
        /// Gets or sets the Merchant ID.
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// Gets or sets the Capture Mode. Valid values are "HCS", "TCS", or "SLM".
        /// </summary>
        public string CaptureMode { get; set; }

        /// <summary>
        /// This must be set either to PNSISO or UTF197
        /// </summary>
        public string MessageType { get; set; }

        // Response Methods
        public WebHeaderCollection ResponseMimeHeaders => this.responseMimeHeaders;
    }
}