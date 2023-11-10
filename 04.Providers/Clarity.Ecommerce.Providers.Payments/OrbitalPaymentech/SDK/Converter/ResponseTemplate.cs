#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK.Converter
{
    /**
     *
     * <p>Title: </p>
     *
     * <p>Description: </p>
     *
     * <p>Copyright: Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
     * reserved</p>
     *
     * @author Rameshkumar Bhaskharanan
     * @version 1.0
     */
    public class ResponseTemplate : ConverterTemplate
    {
        //Singleton instance
        private static ResponseTemplate onlineTemplateInstance;
        private static ResponseTemplate batchTemplateInstance;

        private static string onlineResponseXML = "definitions/SLMOnlineRes.xml";
        private static string batchResponseXML = "definitions/SLMBatchRes.xml";

        // Data
        private List<string> invalidTokens = new List<string>();
        public List<FormatFinder> FormatFinders { get; private set; } = new List<FormatFinder>();

        //for PNS
        private static ResponseTemplate onlinePNSTemplateInstance;
        private static string onlinePNSResponseXML = "definitions/PNSOnlineRes.xml";


        /**
         * Default constructor for singleton object
         */
        public ResponseTemplate( IDispatcherFactory factory )
        {
            this.factory = factory;
            if ( this.factory == null )
            {
                this.factory = new DispatcherFactory();
            }
            this.configurator = this.factory.Config;
            MaxFormatLength = 6;
            MinBytesToRead = 121;
        }

        /**
         * Reset the object
         *
         */
        public void Reset()
        {
            if ( onlineTemplateInstance != null )
            {
                if ( engineLogger != null )
                    engineLogger.Info( "Initiated a Online request reset" );
                onlineTemplateInstance = null;
            }
            if ( batchTemplateInstance != null )
            {
                if ( engineLogger != null )
                    engineLogger.Info( "Initiated a Batch request reset" );
                batchTemplateInstance = null;
            }
            // for PNS
            if ( onlinePNSTemplateInstance != null )
            {
                if ( engineLogger != null )
                    engineLogger.Info( "Initiated a Online PNS request reset" );
                onlinePNSTemplateInstance = null;
            }
        }

        /**
         * return a static object for the request type
         * @param requestType string
         * @return RequestTemplate
         */
        [MethodImpl( MethodImplOptions.Synchronized )]
        public static ResponseTemplate GetInstance( RequestType requestType )
        {
            // For online
            try
            {
                switch ( requestType )
                {
                    case RequestType.Online:
                        if ( onlineTemplateInstance == null )
                        {
                            onlineTemplateInstance = new ResponseTemplate( null );
                            onlineTemplateInstance.Initialize( RequestType.Online );
                        }

                        return onlineTemplateInstance;
                    case RequestType.Batch:
                        if ( batchTemplateInstance == null )
                        {
                            batchTemplateInstance = new ResponseTemplate( null );
                            batchTemplateInstance.Initialize( RequestType.Batch );
                        }

                        return batchTemplateInstance;
                    case RequestType.PNSOnline:
                        if ( onlinePNSTemplateInstance == null )
                        {
                            onlinePNSTemplateInstance = new ResponseTemplate( null );
                            onlinePNSTemplateInstance.Initialize( RequestType.PNSOnline );
                        }

                        return onlinePNSTemplateInstance;
                }
            }
            catch ( ConverterException e )
            {
                throw new ConverterException( Error.InitializationFailure, "Failed to get instance of ResponseTemplate", e );
            }

            return null;
        }

        /**
         * Reset  the static object back to null
         * @param requestType string
         */
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

        /**
         *  Initialize the object data elements
         * @param requestType string
         * @throws ConverterException
         */
        public void Initialize( RequestType requestType )
        {
            // Initialize based on the flag.
            if ( !initialized )
            {
        //        Stopwatch stopwatch = new Stopwatch();

                this.DefaultFormatLength = requestType == RequestType.Batch ? 120 : DefaultFormatLength;
                string srcFilename = null;
                try
                {
                    this.configurator = this.factory.Config;
                    this.engineLogger = factory.Logger;

                    // Determine the source filename.
                    switch ( requestType )
                    {
                        case RequestType.Online:
                            srcFilename = onlineResponseXML;
                            break;
                        case RequestType.Batch:
                            srcFilename = batchResponseXML;
                            break;
                        case RequestType.PNSOnline:
                            srcFilename = onlinePNSResponseXML;
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

                    engineLogger.Info( "Successfully loaded the request source["
                            + srcFilename + "]" );

                    initialized = true;
                }
                catch ( ConverterException )
                {
                    throw;
                }
                catch ( Exception e )
                {
                    var msg = $"Failed to load the response template [{srcFilename}]";
                    if ( engineLogger != null )
                    {
                        engineLogger.Info( msg );
                    }

                    ResetObject( requestType );

                    throw new ConverterException( Error.InitializationFailure, msg, e );
                }

             //   Console.WriteLine( "Loading {0} took {1} ms.", requestType.ToString(), stopwatch.ElapsedMilliseconds );
            }
        }

        /**
         * FUnction load the template on demand
         * @param formatName
         * @throws XMLUtilityException
         */
        [MethodImpl( MethodImplOptions.Synchronized )]
        public void LoadTemplate( string fileName )
        {
            try
            {
                XmlDocument document = null;
                using ( var defs = factory.Definitions )
                {
                    document = defs.GetXmlDocument( fileName, false );
                }

                this.FormatIndicatorLength = Utils.StringToInt( FindNodeValue( "FormatIndicatorLength", document ), 0 );
                MessageDelimiter = FindNodeAttribute( "MessageDelimiter", "Value", Utils.HexAsciiToString( "0D" ), document );
                FormatDelimiter = FindNodeAttribute( "FormatDelimiter", "Value", document );
                this.XMLRootElementName = Utils.FindNodeValue( "XMLRootElementName", "xml", document.DocumentElement, true );
                this.DefaultFormatLength =
                    Utils.StringToInt( FindNodeValue( "DefaultFormatLength", document ), DefaultFormatLength );

                // TODO: Different default for response.
                FileTerminator = FindNodeAttribute( "TCPFileTerminator", "Value", Utils.HexAsciiToString( "0D0D" ), document );
                this.TCPRecordDelimiter = FindNodeAttribute( "TCPRecordDelimiter", "Value", Utils.HexAsciiToString( "0D" ), document );
                this.SFTPRecordDelimiter = FindNodeAttribute( "SFTPRecordDelimiter", "Value", Utils.HexAsciiToString( "0A" ), document );
                this.MaxFormatLength = Utils.StringToInt( FindNodeValue( "MaxFormatLength", document ), 6 );
                this.DFRDelimiter = FindNodeValue( "DFRDelimiter", document );
                this.MinBytesToRead = Utils.StringToInt( FindNodeValue( "MinDataForAnalysis", document ), 121 );
                this.DFRStrToRemove = FindNodeValue( "DFRStrToRemove", document );
                this.FormatPrefix = FindNodeValue( "FormatPrefix", document );

                ParseMessageFormats( document );
                ParsePrimaryBitMap( document );
                ParseOrder( document );
                ParseFormats( document.GetElementsByTagName( "Response" ).Item( 0 ).ChildNodes, "Response", null, null );
                ParseFormatFinders( document );
            }
            catch ( Exception ex )
            {
                var msg = "Exception caught while loading the response template " + fileName;
                engineLogger.Error( msg, ex );
                throw new ConverterException( Error.InitializationFailure, msg, ex );
            }
        }

        /**
         * Iterates through the top-level elements in the FormatFinder,
         * adding each one to a list.
         * The order of FormatFinders in the list is important.
         * @param document - The XML document of the converter template.
         */
        private void ParseFormatFinders( XmlDocument document )
        {
            var finderRoot = document.GetElementsByTagName( "FormatFinder" );
            if ( finderRoot == null || finderRoot.Count == 0 )
            {
                return;
            }
            var finders = finderRoot.Item( 0 ).ChildNodes;
            for ( var i = 0; i < finders.Count; i++ )
            {
                if ( finders.Item( i ).NodeType == XmlNodeType.Element )
                {
                    FormatFinders.Add( new FormatFinder( finders.Item( i ), null, this ) );
                }
            }
        }

        private void ParsePrimaryBitMap( XmlDocument document )
        {
            var bitmapNode = document.GetElementsByTagName( "PrimaryBitMap" );
            if ( bitmapNode == null || bitmapNode.Count == 0 )
                return;
            bitmapNode = bitmapNode.Item( 0 ).ChildNodes;
            for ( var i = 0; i < bitmapNode.Count; i++ )
            {
                var node = bitmapNode.Item( i );
                if ( node.NodeType != XmlNodeType.Element)
                    continue;
                if ( node.Name == "Size" )
                    this.BitmapSize = Utils.StringToInt( Utils.GetNodeValue( node ), 121 );
                else if ( node.Name == "Position" )
                    this.BitmapPosition = Utils.StringToInt( Utils.GetNodeValue( node ), 121 );
                else if ( node.Name == "Length" )
                    this.BitmapLength = Utils.StringToInt( Utils.GetNodeValue( node ), 121 );
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int BitmapPosition { get; private set; } = 121;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int BitmapLength { get; private set; } = 121;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int BitmapSize { get; private set; } = 121;

        /// <summary>
        ///
        /// </summary>
        public string DFRDelimiter { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string FormatPrefix { get; private set; }

        /// <summary>
        /// Returns true if the message ends with a valid record delimiter.
        /// </summary>
        /// <param name="message">The message to test.</param>
        /// <returns></returns>
        public bool IsMessageDelimiter( string message )
        {
            return message.StartsWith( this.MessageDelimiter ) || message.StartsWith( this.TCPRecordDelimiter ) || message.StartsWith( this.SFTPRecordDelimiter );
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string TCPRecordDelimiter { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string SFTPRecordDelimiter { get; private set; }

        /// <summary>
        /// Return optional root element name.
        /// </summary>
        /// <returns></returns>
        public string XMLRootElementName { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int MaxFormatLength { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string DFRStrToRemove { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int MinBytesToRead { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        /// <param name="commMode"></param>
        /// <returns></returns>
        public string GetRecordDelimiter( Format format, CommModule module )
        {
            if ( module == CommModule.TCPBatch )
            {
                if ( format.TCPRecordDelimiter != null )
                {
                    return format.TCPRecordDelimiter;
                }
                return this.TCPRecordDelimiter;
            }

            if ( module == CommModule.SFTPBatch )
            {
                if ( format.SFTPRecordDelimiter != null )
                {
                    return format.SFTPRecordDelimiter;
                }
                return this.SFTPRecordDelimiter;
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatData"></param>
        /// <param name="format"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public string GetFieldDelimiter( string formatData, Format format, ConfigurationData configData )
        {
            if (!format.IsDelmitedFormat)
            {
                return null;
            }

            if (configData.Protocol != CommModule.SFTPBatch && configData.Protocol != CommModule.TCPBatch)
            {
                return null;
            }
            else if ( configData.Protocol == CommModule.SFTPBatch && format.SFTPRecordDelimiter != null )
            {
                return SFTPRecordDelimiter;
            }

            string configDelim = null;
            try
            {
                configDelim = configData[ "DFRDelimiter" ];
            }
            catch
            {
            }

            if ( configDelim != null && configDelim.Length > 0 )
            {
                DFRDelimiter = configDelim;
            }

            if ( DFRDelimiter == null )
            {
                return null;
            }

            var delim = DFRDelimiter.ToCharArray();

            for ( var ctr = 0; ctr < delim.Length; ctr++ )
            {
                if ( formatData.IndexOf( delim[ctr] ) >= 0 )
                {
                    return char.ToString( delim[ctr] );
                }
            }

            return null;
        }
    }
}
