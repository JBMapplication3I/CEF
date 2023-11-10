using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;
using System.Diagnostics;

namespace JPMC.MSDK.Converter
{
    /// <summary>
    /// Wraps the Request converter templates for Salem (online and batch) and PNS.
    /// </summary>
    /// <remarks>
    /// This singleton class that stores three instances: Salem Online, Salem Batch, and PNS. 
    /// The GetInstance() method takes request type string that specifies which instance to return.
    /// </remarks>
    public class RequestTemplate : ConverterTemplate
    {
        // Singleton instance
        private static RequestTemplate onlineTemplateInstance;
        private static RequestTemplate batchTemplateInstance;
        private static RequestTemplate onlinePNSTemplateInstance;

        private static string onlineRequestXML = "definitions/SLMOnlineReq.xml";
        private static string batchRequestXML = "definitions/SLMBatchReq.xml";
        private static string onlinePNSRequestXML = "definitions/PNSOnlineReq.xml";

        /// <summary>
        /// Default constructor for singleton object
        /// </summary>
        /// <param name="factory">A dispatcher factory for getting all external classes</param>
        public RequestTemplate( IDispatcherFactory factory )
        {
            this.factory = factory;
            if ( factory == null )
            {
                this.factory = new DispatcherFactory();
            }
        }

        /// <summary>
        /// Resets all of the static instances to null.
        /// </summary>
        public static void Reset()
        {
            onlineTemplateInstance = null;
            batchTemplateInstance = null;
            onlinePNSTemplateInstance = null;
        }

        /// <summary>
        /// Gets a static object for the request type
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        [MethodImpl( MethodImplOptions.Synchronized )]
        public static RequestTemplate GetInstance( RequestType requestType )
        {
            // For online
            try
            {
                switch ( requestType )
                {
                    case RequestType.Online:
                        if ( onlineTemplateInstance == null )
                        {
                            onlineTemplateInstance = new RequestTemplate( null );
                            onlineTemplateInstance.Initialize( requestType );
                        }

                        return onlineTemplateInstance;
                    case RequestType.Batch:
                        if ( batchTemplateInstance == null )
                        {
                            batchTemplateInstance = new RequestTemplate( null );
                            batchTemplateInstance.Initialize( requestType );
                        }

                        return batchTemplateInstance;
                    case RequestType.PNSOnline:
                        if ( onlinePNSTemplateInstance == null )
                        {
                            onlinePNSTemplateInstance = new RequestTemplate( null );
                            onlinePNSTemplateInstance.Initialize( requestType );
                        }

                        return onlinePNSTemplateInstance;
                }
            }
            catch ( ConverterException )
            {
                throw;
            }

            return null;
        }

        /// <summary>
        /// Reset a specific instance object back to null.
        /// </summary>
        /// <param name="requestType"></param>
        private void ResetObject( RequestType requestType )
        {
            switch ( requestType )
            {
                case RequestType.Online:
                    onlineTemplateInstance = null;
                    break;
                case RequestType.Batch:
                    batchTemplateInstance = null;
                    break;
                case RequestType.PNSOnline:
                    onlinePNSTemplateInstance = null;
                    break;
            }

            initialized = false;
        }

        /// <summary>
        /// Initialize the object data elements.
        /// </summary>
        /// <param name="requestType"></param>
        public void Initialize( RequestType requestType )
        {
            // Initialize based on the flag.
            if ( !initialized )
            {
//                Stopwatch stopwatch = new Stopwatch();

                DefaultFormatLength = requestType == RequestType.Batch ? 120 : 1000;
                string srcFilename = null;
                try
                {
                    this.configurator = this.factory.Config;
                    this.engineLogger = factory.Logger;

                    // Determine the source filename.
                    switch ( requestType )
                    {
                        case RequestType.Online:
                            srcFilename = onlineRequestXML;
                            break;
                        case RequestType.Batch:
                            srcFilename = batchRequestXML;
                            break;
                        case RequestType.PNSOnline:
                            srcFilename = onlinePNSRequestXML;
                            break;
                    }

                    if ( srcFilename == null )
                    {
                        // Couldn't locate the file - reset the singleton instance to null
                        ResetObject( requestType );

                        throw new ConverterException( Error.InitializationFailure,
                            "Exception while loading the request source, "
                            + "could't locate the XML file" );
                    }

                    LoadTemplate( srcFilename );

                    engineLogger.InfoFormat( "Successfully loaded the request source[{0}]",
                            srcFilename );

                    initialized = true;

                }
                catch ( ConverterException )
                {
                    throw;
                }
                catch ( Exception e )
                {
                    if ( engineLogger != null )
                    {
                        engineLogger.ErrorFormat(
                            "Failed to load the request template [{0}]",
                            srcFilename );
                    }

                    ResetObject( requestType );

                    throw new ConverterException( Error.InitializationFailure,
                        "Exception while loading the request source ["
                        + srcFilename + "]" + e.ToString(), e );
                }

        //        Console.WriteLine( "Loading {0} took {1} ms.", requestType.ToString(), stopwatch.ElapsedMilliseconds );
            }
        }

        /// <summary>
        /// Loads and parses the specified template file.
        /// </summary>
        /// <param name="fileName"></param>
        [MethodImpl( MethodImplOptions.Synchronized )]
        private void LoadTemplate( string fileName )
        {
            try
            {
                XmlDocument document = null;
                using ( var defs = factory.Definitions )
                {
					document = defs.GetXmlDocument( fileName, false );
                }

                this.FormatIndicatorLength = Utils.StringToInt( FindNodeValue( "FormatIndicatorLength", document ), 0 );
                MessageDelimiter = FindNodeAttribute( "MessageDelimiter", "Value", document );
                FormatDelimiter = FindNodeAttribute( "FormatDelimiter", "Value", document );
                this.DefaultFormatLength =
                    Utils.StringToInt( FindNodeValue( "DefaultFormatLength", document ), DefaultFormatLength );
                FileTerminator = FindNodeAttribute( "TCPFileTerminator", "Value", document );
                if ( FileTerminator == null )
                    FileTerminator = "EOFEOFEOF" + Utils.HexAsciiToString( "0D0D" );

                ParseMessageFormats( document );
                ParseOrder( document );
                ParseFormats( document.GetElementsByTagName( "Request" ).Item( 0 ).ChildNodes, "Request", null, null );
            }
            catch ( Exception ex )
            {
                var msg = "Exception caught while loading the request template " + fileName;
                engineLogger.Error( msg, ex );
                throw new ConverterException( Error.InitializationFailure, msg, ex );
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        public string GetMessageFormat( string name )
        {
            return this.messageFormats[name];
        }
    }
}
