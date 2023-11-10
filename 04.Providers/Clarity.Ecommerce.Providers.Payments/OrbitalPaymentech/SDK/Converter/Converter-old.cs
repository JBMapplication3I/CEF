using System;
using System.Collections.Generic;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;
using log4net;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570


namespace JPMC.MSDK.Converter
{
    /// <summary>
    ///
    /// </summary>
    public class Converter
    {
        protected RequestTemplate requestTemplate = null;
        protected ResponseTemplate responseTemplate = null;
        protected ILog logger = null;
        protected IConverterFactory factory = null;
        protected ConverterArgs recordInfo = null;
        protected ConfigurationData configData = null;

        protected IFullResponse response = null;
        protected string responseData = null;
//        protected IRequest maskedRequest = null;
        protected ConverterArgs responseArgs = null;
        private bool isPNS = false;

        // For handling MOP Based format mapping.
        protected string methodOfPayment = null;
        protected string record = null;

        /// <summary>
        ///
        /// </summary>
        public Converter()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        public Converter( IConverterFactory factory )
        {
            this.factory = factory;
            logger = factory.EngineLogger;
        }

        public ConverterArgs RecordInfo
        {
            get { return recordInfo; }
            set { recordInfo = value; }
        }

        private ConverterArgs ConvertRequest( IRequestImpl request, bool isPNS, RequestTemplate onlineRequest )
        {
            this.requestTemplate = onlineRequest;
            //this.maskedRequest = factory.MakeRequest( request );
            this.isPNS = isPNS;

            List<byte> data = new List<byte>();
            List<byte> maskedData = new List<byte>();
            for ( int i = 0; i < request.Formats.Count; i++ )
            {
                IRequestImpl formatRequest = (IRequestImpl) request.Formats[ i ];
                if ( formatRequest == null )
                {
                    logger.Error( "A format is null" );
                    throw new ConverterException( Error.FormatNotFoundError, "A format is null" );
                }
                ConverterArgs convArgs = ConvertRequestFormat( formatRequest, "", null );
                data.AddRange( convArgs.ReqByteArray );
                maskedData.AddRange( convArgs.MaskedReqByteArray );
                if ( !isPNS && request.IsBatch )
                {
                    byte[] bytes = onlineRequest.FormatDelimiter.GetBytes();
                    data.AddRange( bytes );
                    maskedData.AddRange( bytes );
                }
            }

            if ( !request.IsBatch && onlineRequest.MessageDelimiter != null )
            {
                data.AddRange( onlineRequest.MessageDelimiter.GetBytes() );
                maskedData.AddRange( onlineRequest.MessageDelimiter.GetBytes() );
            }

            ConverterArgs args = new ConverterArgs();
            args.ReqByteArray = data.ToArray();
            args.Request = Utils.ByteArrayToString( args.ReqByteArray );
            args.MaskedReqByteArray = maskedData.ToArray();
            args.MaskedRequest = Utils.ByteArrayToString( args.MaskedReqByteArray );
            return args;
        }



        private ConverterArgs ConvertRequestFormat( IRequestImpl request, string baseName, PNSBitSet prevBitmap )
        {
            ConverterArgs retArgs = new ConverterArgs();
            PNSBitSet bitmap = prevBitmap;
            bool newBitmap = false;
            Format format = null;
            format = requestTemplate.GetFormat( baseName + request.FormatIndicator );
            if ( format.IsEmpty )
            {
                string msg = "The format [" + baseName + request.FormatIndicator + "] could not be found.";
                logger.Error( msg );
                throw new ConverterException( Error.FormatNotFoundError, msg );
            }
            if ( format.Name != request.FormatIndicator && format[ request.FormatIndicator ] != null )
            {
                retArgs.ReqByteArray = new byte[ 0 ];
                retArgs.MaskedReqByteArray = new byte[ 0 ];
                return retArgs;
            }

            List<byte> convertedData = new List<byte>();
            List<byte> maskedConvertedData = new List<byte>();
            List<byte> data = new List<byte>();
            List<byte> maskedData = new List<byte>();

            if ( format.FormatType == FormatType.Bitmap )
            {
                bitmap = new PNSBitSet();
                newBitmap = true;
            }

            // Handle arrays. Array formats will have the format indicator
            // plus a number, such as PromptDataArray1
            if ( format.Name != request.FormatIndicator )
            {
                if ( format.ArrayFormat != null && request.FormatIndicator.StartsWith( format.ArrayFormat.Name ) )
                {
                    format = format.ArrayFormat;
                }
                else
                {
                    string msg = "The format [" + request.FormatIndicator + "] could not be found.";
                    logger.Error( msg );
                    throw new ConverterException( Error.FormatNotFoundError, msg );
                }
            }

            ValidateFormat( format );

            // Set the appropriate bit in the bitmap, if applicable.
            if ( format.Bit != -1 && bitmap != null )
            {
                bitmap.Set( format.Bit );
            }

            // Convert fields
            byte[] previousFieldValue = null;
            foreach ( Field field in format.Fields )
            {
                RequestField reqField = request.GetLocalField( field.Name );
                if ( reqField == null )
                {
                    reqField = new RequestField( field.Name, null );
                }
                string maskedValue;
                previousFieldValue = ConvertRequestField( reqField, field, format, previousFieldValue, out maskedValue );

                 data.AddRange( previousFieldValue );
                if ( reqField.MaskedValue != null )
                {
                    if ( reqField.MaskedPrefix != null )
                    {
                        maskedData.AddRange( reqField.MaskedPrefix.GetBytes() );
                    }
                    maskedData.AddRange( reqField.MaskedValue.GetBytes() );
                }
            }

            // Convert sub-formats
            List<byte> convertedFormats = new List<byte>();
            List<byte> maskedConvertedFormats = new List<byte>();
            if ( format.Formats.Count > 0 )
            {
                List<byte> maskedChildData;
                convertedFormats.AddRange( ProcessChildFormats( request, baseName, format, bitmap, out maskedChildData ) );
                maskedConvertedFormats.AddRange( maskedChildData );
            }

            if ( newBitmap )
            {
                if ( format.BitMapType == BitMapType.Hex )
                {
                    byte[] bitSet = bitmap.BitSetToIsoHex().GetBytes();
                    convertedData.AddRange( bitSet );
                    maskedConvertedData.AddRange( bitSet );
                }
                else
                {
                    byte[] bitSet = bitmap.BitSetToBytes();
                    convertedData.AddRange( bitSet );
                    maskedConvertedData.AddRange( bitSet );
                }
            }


            convertedData.AddRange( data );
            convertedData.AddRange( convertedFormats );
            maskedConvertedData.AddRange( maskedData );
            maskedConvertedData.AddRange( maskedConvertedFormats );

            string prefix = GetPrefix( format, convertedData.Count );
            byte[] prefixBytes = prefix.GetBytes();
            convertedData.InsertRange( 0, prefixBytes );
            maskedConvertedData.InsertRange( 0, prefixBytes );

            retArgs.ReqByteArray = convertedData.ToArray();
            retArgs.MaskedReqByteArray = maskedConvertedData.ToArray();
            return retArgs;
        }

        private byte[] ConvertRequestField( RequestField reqField, Field convField, Format format, byte[] previousFieldValue, out string maskedValue )
        {
            maskedValue = null;

            if ( convField == null )
            {
                string msg = String.Format( "Could not find the field [{0}] in the converter template.", reqField.name );
                logger.Error( msg );
                throw new ConverterException( Error.FieldDoesNotExist, msg );
            }

            if ( reqField == null )
            {
                string msg = String.Format( "Could not find the field [{0}] in the request template.", convField.Name );
                logger.Error( msg );
                throw new ConverterException( Error.FieldDoesNotExist, msg );
            }

            if ( convField.FieldType == DataType.Binary && reqField.BinaryValue == null )
            {
                try
                {
                    byte[] binaryData = Convert.FromBase64String( reqField.Value );
                    reqField.BinaryValue = binaryData;
                }
                catch ( Exception ex )
                {
                    String msg = "The value of field " + reqField.elementID + " is not a valid Base64 String."; ;
                    logger.Error( msg, ex );
                    throw new ConverterException( Error.BadDataError, msg, ex );
                }
            }

            if ( reqField.BinaryValue != null )
            {
                if ( reqField.BinaryValue.Length != convField.Length )
                {
                    string msg = String.Format( "The field [{0}] is {1} bytes long, when it must be {2} bytes long.",
                        convField.Name, reqField.BinaryValue.Length, convField.Length );
                    logger.Error( msg );
                    throw new ConverterException( Error.LengthError, msg );
                }
                return reqField.BinaryValue;
            }

            PrepMultiLengthField( ref convField, previousFieldValue );

            string value = reqField.Value;
            if ( value == null )
            {
                value = "";
            }

            int fieldLength = GetFieldLength( convField, value );

            if ( IsEmpty( value ) && convField.DefaultValue != null )
            {
                if ( true)
                {
                    value = Utils.PadLeft( value, convField.DefaultValue[ 0 ], convField.Length );
                }
                else
                {
                    value = convField.DefaultValue;
                }
            }

            if ( reqField.required && IsEmpty( value ) )
            {
                string formatName = ( format.Parent != null ) ? format.Alias : "";
                string msg = String.Format( "The required field \"{0}\" in format \"{1}\" is not set.", reqField.name, formatName );
                logger.Error( msg );
                throw new ConverterException( Error.RequiredFieldNotSet, msg );
            }

            ValidateNumericField( value, convField );

            value = FillWith( value, convField, fieldLength, format.FormatType );

            if ( convField.CaseType == Case.Upper )
            {
                value = value.ToUpper();
            }
            else if ( convField.CaseType == Case.Lower )
            {
                value = value.ToLower();
            }

            maskedValue = MaskValue( value, convField, fieldLength );

            maskedValue = ( maskedValue == null ) ? value : maskedValue;

            if ( convField.PrefixLength > 0 )
            {
                string prefix = Convert.ToString( value.Length );
                if ( prefix.Length > convField.PrefixLength )
                {
                    string msg = String.Format( "The field [{0}] is too long. The prefix length is [{1}], but must be [{2}].",
                        convField.Name, prefix.Length, convField.PrefixLength );
                    logger.Error( msg );
                    throw new ConverterException( Error.LengthError, msg );
                }
                prefix = Utils.PadLeft( prefix, '0', convField.PrefixLength );
                value = String.Concat( prefix, value );
                reqField.MaskedPrefix = ( convField.HasMask ) ? new string( '#', prefix.Length ) : prefix;
                if ( convField.HasMask )
                {
                    String str = Utils.PadLeft( "", '#', prefix.Length );
                    reqField.MaskedPrefix = str;
                }
            }

            reqField.MaskedValue = maskedValue;

            return value.GetBytes();
        }

        private string FillWith( string value, Field convField, int fieldLength, FormatType formatType )
        {
            if ( fieldLength == 0 )
            {
                return value;
            }

            if ( convField.FillWith != null )
            {
                if ( convField.Justification == TextJustification.Right )
                {
                    value = Utils.PadLeft( value, convField.FillWith[ 0 ], fieldLength );
                }
                else
                {
                    value = Utils.PadRight( value, convField.FillWith[ 0 ], fieldLength );
                }
            }
            else if ( formatType != FormatType.Variable && formatType != FormatType.VariableArray )
            {
                value = Utils.PadRight( value, ' ', fieldLength );
            }

            return value;
        }

        /// <summary>
        /// Converts a Salem-spec response string to an XML document.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The response string is converted into a tree of DataElement
        /// objects that define the structure of the XML. Once it is built,
        /// the root DataElements are passed to the ResponseData object to be
        /// turned into XML.
        /// </para>
        /// <para>
        /// DataElements are quick to process and easier to
        /// work with than XmlNodes, so the Converter works with them. The
        /// ResponseData class manages building the XmlDocument from the
        /// list of DataElements supplied to it.
        /// </para>
        /// </remarks>
        /// <param name="respData"></param>
        /// <param name="isMask"></param>
        /// <returns>An XML document for the response.</returns>
        private ConverterArgs ConvertSalemResponse( string respData )
        {
            string formatName = null;

            try
            {
                logger.Debug( "Converting a Salem Spec Response" );
                LoadTemplates( false );
                response = factory.MakeResponse();
                response.ResponseType = "SLM";
                responseData = respData;
                int formatIndLen = responseTemplate.FormatIndicatorLength;
                responseArgs = new ConverterArgs();
                responseArgs.MaskedResponse = respData;
                responseArgs.Response = respData;

                if ( responseData.Length < formatIndLen )
                {
                    string msg = "The response data is shorter than the format indicator length." +
                        "The data returned from the server appears to be corrupt";
                    logger.Error( msg );
                    throw new ConverterException( Error.BadDataError, msg );
                }

                string formatIndicator = responseData.Substring( 0, formatIndLen );

                // Is there a base Batch report container?
                Format containerFormat = GetContainerFormat();
                KeySafeDictionary<string, DataElement> elements = new KeySafeDictionary<string, DataElement>();

                DataElement parentElement = null;
                Format parentFormat = null;
                while ( responseData.Length > 0 && !responseTemplate.IsMessageDelimiter( responseData ) )
                {
                    formatName = responseData.Substring( 0, formatIndLen );

                    logger.DebugFormat( "Getting format [{0}]", formatName );
                    Format format = responseTemplate.GetFormat( formatName );
                    if ( format.IsEmpty )
                    {
                        format = GetFormatFromContainer( formatName, containerFormat, format );
                    }

                    if ( format.IsEmpty )
                    {
                        ConverterArgs info = new ConverterArgs();
                        if ( GetPositionBasedFormatName( info, responseData, responseTemplate.formatMaps.positionBased.items ) )
                        {
                            formatName = info.Format;
                            format = responseTemplate.GetFormat( formatName );
                        }
                    }

                    if ( format.IsEmpty )
                    {
                        ConverterArgs info = new ConverterArgs();
                        if ( GetMOPBasedFormatName( info, responseData, responseTemplate.formatMaps.mopBased.items ) )
                        {
                            formatName = info.Format;
                            format = responseTemplate.GetFormat( formatName );
                        }
                    }

                    if ( format.IsEmpty )
                    {
                        format = GetVariableLengthNamedFormat( responseData );
                    }

                    if ( format.IsEmpty )
                    {
                        response.IsConversionError = true;
                        response.LeftoverData = TrimRecordDelimiter( format, responseData );
                        response.ErrorDescription = "The remaining data does not belong to a known format.";
                        logger.ErrorFormat( "Failed to convert the format [{0}], Details: [{1}]", formatName, response.ErrorDescription );
                        break;
                    }

                    DataElement element = new DataElement( format.Aliases[ 0 ], null, response );
                    int aliasLength = ( format.Aliases.Length > 0 ) ? format.Aliases.Length : 1;
                    element.NumAliases = aliasLength;
                    element.HideFromFieldID = format.HideFromFieldID;
                    ConvertResponseFormat( format, element );
                    element.Name = format.Aliases[ 0 ];
                    DealWithArray( elements, element );

                    bool isFormatRef = false;

                    if ( parentElement != null && parentFormat.IsFormatRef( element.Name ) )
                    {
                        AddFormatRefElement( parentElement, element );
                        //parentElement.getElements().add( element );
                        isFormatRef = true;
                    }
                    else
                    {
                        isFormatRef = false;
                        parentElement = null;
                        parentFormat = null;
                        elements.Add( element.FieldID, element );
                    }

                    if ( format.HasFormatRefs )
                    {
                        parentFormat = format;
                        parentElement = element;
                    }

                    // Create a new DataElement for each alias.
                    for ( int i = 1; i < format.Aliases.Length; i++ )
                    {
                        string oldName = element.Name;
                        oldName = ( oldName.Contains( "[" ) ) ? oldName.Substring( 0, oldName.IndexOf( "[" ) + 1 ) : oldName;
                        string newName = format.Aliases[ i ];
                        DataElement clone = new DataElement( element );
                        clone.Name = newName;
                        for ( int j = 0; j < clone.Elements.Count; j++ )
                        {
                            String id = clone.Elements[ j ].FieldID;
                            id = id.Replace( oldName, newName );
                            clone.Elements[ j ].FieldID = id;
                        }
                        DealWithArray( elements, clone );
                        if ( isFormatRef )
                        {
                            parentElement.Elements.Add( clone );
                        }
                        else
                        {
                            elements.Add( clone.FieldID, clone );
                        }
                    }

                    if ( responseData.StartsWith( responseTemplate.MessageDelimiter ) )
                    {
                        responseData = responseData.Remove( 0, 1 );
                    }

                    // SFTPBatch response files sometimes have an extra newline at the end of each
                    // record. We must remove it, if it exists.
                    if ( configData.Protocol == CommModule.SFTPBatch && responseData.StartsWith( "\n" ) )
                    {
                        responseData = responseData.Remove( 0, 1 );
                    }
                }

                if ( !respData.EndsWith( responseTemplate.MessageDelimiter )
                    && !respData.EndsWith( responseTemplate.TCPRecordDelimiter )
                    && !respData.EndsWith( responseTemplate.SFTPRecordDelimiter ) )
                {
                    response.IsConversionError = true;
                    response.ErrorDescription = "The record does not end with the record terminator";
                    response.LeftoverData = "";
                }

                response.RawData = respData.GetBytes();
                response.DataElements = new List<DataElement>( elements.Values );
                responseArgs.Format = formatIndicator;
                responseArgs.Response = respData;
                responseArgs.ResponseData = response;
                return responseArgs;
            }
            catch ( Exception ex )
            {
                string msg = null;
                if ( formatName == null )
                {
                    msg = "Exception caught while creating the response.";
                }
                else
                {
                    msg = "Exception caught while creating the response. Failed on format \"" + formatName + "\".";
                }
                logger.Error( msg, ex );
                throw new ConverterException( Error.ResponseFailure, msg, ex );
            }
        }

        private void AddFormatRefElement( DataElement parentElement, DataElement newElement )
        {
            int prevIndex = -1;
            newElement.FieldID = parentElement.FieldID + "." + newElement.FieldID;
            if ( newElement.Elements.Count > 0 )
            {
                string name = newElement.Elements[ 0 ].FieldID;
                newElement.Elements[ 0 ].FieldID = parentElement.FieldID + "." + name;
            }

            for ( int i = 0; i < parentElement.Elements.Count; i++ )
            {
                DataElement elem = parentElement.Elements[ i ];

                if ( elem.Elements.Count == 0 )
                {
                    continue;
                }

                string prevFieldID = elem.Elements[ 0 ].FieldID;
                string testPrev = StripArrayIndex( prevFieldID );
                string currFieldID = newElement.Elements[ 0 ].FieldID;

                if ( testPrev != currFieldID )
                {
                    continue;
                }

                prevIndex = GetArrayIndex( prevFieldID );

                // Add index to array elem that had none before.
                if ( prevIndex == -1 )
                {
                    prevIndex = 0;
                    string id = String.Format( "{0}[{1}].{2}", elem.FieldID, prevIndex, elem.Elements[ 0 ].Name );
                    elem.Elements[ 0 ].FieldID = id;
                }
            }

            if ( prevIndex == -1 )
            {
                parentElement.Elements.Add( newElement );
            }
            else
            {
                string currFieldID = String.Format( "{0}[{1}].{2}", newElement.FieldID, prevIndex + 1,
                        newElement.Elements[ 0 ].Name );

                newElement.Elements[ 0 ].FieldID = currFieldID;
                parentElement.Elements.Add( newElement );
            }
        }

        private int GetArrayIndex( string array )
        {
            int start = array.IndexOf( '[' );
            int end = array.IndexOf( ']' );
            if ( start == -1 || end == -1 )
            {
                return -1;
            }

            start++;

            String num = array.Substring( start, end - start );
            return Int32.Parse( num.Trim() );
        }

        private string StripArrayIndex( string array )
        {
            int start = array.IndexOf( '[' );
            int end = array.IndexOf( ']' );
            if ( start == -1 || end == -1 )
            {
                return array;
            }

            string ret = array.Substring( 0, start ) + array.Substring( end + 1 );
            return ret;
        }

        private void DealWithArray( KeySafeDictionary<string, DataElement> elements, DataElement element )
        {
            String fieldID = element.FieldID;
            if ( !elements.ContainsKey( fieldID ) && !elements.ContainsKey( fieldID + "[0]" ) )
            {
                return;
            }

            if ( elements.ContainsKey( fieldID ) )
            {
                DataElement elem = elements[ element.FieldID ];
                elem.SetArrayIndex( 0 );
                elements.Remove( fieldID );
                elements.Add( elem.FieldID, elem );

                element.SetArrayIndex( 1 );
                return;
            }

            // Count the number of elements.
            int count = 0;
            for ( count = 0; elements.ContainsKey( String.Format( "{0}[{1}]", fieldID, count ) ); count++ )
            {
            }

            element.SetArrayIndex( count );
        }

        private string TrimRecordDelimiter( Format format, string data )
        {
            string delim = responseTemplate.GetRecordDelimiter( format, configData.Protocol );
            delim = ( delim == null ) ? responseTemplate.MessageDelimiter : delim;
            string resp = data;
            if ( delim != null && data.EndsWith( delim ) )
            {
                resp = data.Substring( 0, data.Length - delim.Length );
            }

            return resp;
        }


        private Format GetVariableLengthNamedFormat( string data )
        {
            for ( int i = responseTemplate.FormatIndicatorLength + 1; i < 7 && i < data.Length; i++ )
            {
                Format format = GetFormat( data, i );
                if ( !format.IsEmpty )
                {
                    return format;
                }
            }

            return new Format();
        }

        private Format GetFormat( string name, int length )
        {
            return responseTemplate.GetFormat( name.Substring( 0, length ) );
        }

        protected Format GetContainerFormat()
        {
            Format containerFormat = new Format();
            if ( recordInfo != null && recordInfo.Format != null )
            {
                containerFormat = responseTemplate.GetFormat( recordInfo.Format );
            }
            return containerFormat;
        }

        /// <summary>
        /// Gets the format from a container format, if necessary.
        /// </summary>
        /// <remarks>
        /// Some batch formats are grouped together inside a container format.
        /// For instance, report files have a header, detail, and trailer
        /// record that belongs to one specific report. So, there is a format
        /// for the report (like HBCBIN), and it has a format for each of its
        /// records inside it, as child formats.
        ///
        /// This method will get the format from the container format, if one
        /// exists.
        /// </remarks>
        /// <param name="formatName"></param>
        /// <param name="baseBatchFormat"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        protected Format GetFormatFromContainer( string formatName, Format containerFormat, Format format )
        {
            if ( containerFormat.IsEmpty )
            {
                return format;
            }

            if ( containerFormat.Formats.ContainsKey( formatName ) )
            {
                return containerFormat.Formats[ formatName ];
            }
            else
            {
                return containerFormat.GetFormatByData( responseData );
            }
        }


        /// <summary>
        /// Converts a single format into DataElements.
        /// </summary>
        /// <param name="format">The Format to be converted.</param>
        /// <param name="element">The DataElement to insert the new format's DataElement into.</param>
        private void ConvertResponseFormat( Format format, DataElement element )
        {
            logger.DebugFormat( "Converting format \"{0}\"", format.Name );
            element.Name = ( format.Alias != null ) ? format.Aliases[ 0 ] : element.Name;

            if ( format.PrefixLength > 0 )
            {
                format.DataLength = GetIntValueFromData( format, format.PrefixLength );
                logger.DebugFormat( "Setting DataLength for format \"{0}\" to \"{1}\".", format.Name, format.DataLength );
            }

            ProcessResponseFields( format.Fields, format, element );

            if ( response.IsConversionError )
            {
                return;
            }

            ConvertResponseSubFormats( format, element );
        }

        private void ProcessResponseFields( List<Field> fields, Format format, DataElement element )
        {
            int dataLength = 0;
            foreach ( Field field in fields )
            {
                DataElement fieldElement = ConvertResponseField( field, field.Aliases[ 0 ], format, element );
                if ( response.IsConversionError )
                {
                    break;
                }

                dataLength = ( dataLength == 0 ) ? format.DataLength : dataLength;

                if ( field.ArrayIndicator )
                {
                    int itemCount = Utils.StringToInt( fieldElement.Value );
                    int count = 0;
                    // format.DataLength will be -1 in Salem responses, so we must rely on the item count.
                    // format.DataLength will be > 0 in PNS responses, so use that and then compare to item count afterward.
                    while ( ( format.DataLength == -1 && count < itemCount ) || format.DataLength > 0 )
                    {
                        string name = String.Format( "{0}[{1}]", field.ArrayNodeName, count );
                        DataElement arrayNode = new DataElement( name, null, element );
                        element.Elements.Add( arrayNode );
                        ProcessResponseFields( field.Fields, format, arrayNode );
                        count++;
                    }

                    if ( itemCount > 0 && itemCount != count )
                    {
                        response.IsConversionError = true;
                        response.LeftoverData = TrimRecordDelimiter( format, responseData );
                        response.ErrorDescription = "The format [" + format.Name + "] must have " + itemCount.ToString() + " array items, but has " + count.ToString() + " instead.";
                        logger.ErrorFormat( "Failed to convert the format [{0}], Details: [{1}]", format.Name, response.ErrorDescription );
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// Some Formats ins a Response have child formats. This method
        /// iterates through them and converts them.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="element"></param>
        private void ConvertResponseSubFormats( Format format, DataElement element )
        {
            if ( format.FormatType != FormatType.MultiFormat )
            {
                return;
            }

            int bytesConverted = 0;
            while ( bytesConverted < format.DataLength )
            {
                if ( response.IsConversionError )
                {
                    return;
                }

                // Get the Format.
                string formatIndicator = responseData.Substring( 0, responseTemplate.FormatIndicatorLength );
                Format subFormat = format.Formats[ formatIndicator ];
                if ( subFormat == null || subFormat.IsEmpty )
                {
                    // Sometimes the format name is longer than the format indicator.
                    subFormat = format.GetFormatByData( responseData );
                    if ( subFormat == null || subFormat.IsEmpty )
                    {
                        // Is there a Default SubFormat?
                        subFormat = format.Formats[ "Default" ];
                        if ( subFormat == null || subFormat.IsEmpty )
                        {
                            string msg = String.Format( "The format data does match a valid format, Format [{0}]", formatIndicator );
                            logger.Error( msg );
                            throw new ConverterException( Error.BadDataError, msg );
                        }
                    }
                    logger.DebugFormat( "A default format will be used for the FormatIndicator \"{0}\"", formatIndicator );
                }

                int lengthBefore = responseData.Length;
                DataElement childElement = new DataElement( subFormat.Alias, null, element );
                element.HideFromFieldID = subFormat.HideFromFieldID;
                ConvertResponseFormat( subFormat, childElement );
                element.Elements.Add( childElement );
                for ( int i = 1; i < subFormat.Aliases.Length; i++ )
                {
                    DataElement alias = new DataElement( childElement );
                    alias.Name = subFormat.Aliases[ i ];
                    element.Elements.Add( alias );
                }
                bytesConverted += lengthBefore - responseData.Length;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="respByteArray"></param>
        /// <param name="isPNS"></param>
        /// <param name="isMask"></param>
        /// <returns></returns>
        public ConverterArgs ConvertResponse( byte[] respByteArray, bool isPNS )
        {
            if ( isPNS )
            {
                return ConvertPNSResponse( respByteArray );
            }

            return ConvertSalemResponse( Utils.ByteArrayToString( respByteArray ) );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="respByteArray"></param>
        /// <param name="isPNS"></param>
        /// <param name="isMask"></param>
        /// <returns></returns>
        public ConverterArgs ConvertResponse( string resp, bool isPNS )
        {
            if ( isPNS )
            {
                logger.Error( "This method is not valid for PNS" );
                throw new ConverterException( Error.InvalidModule, "This method is not valid for PNS" );
            }

            return ConvertSalemResponse( resp );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isMask"></param>
        /// <returns></returns>
        public ConverterArgs ConvertRequest( IRequest request )
        {
            IRequestImpl req = (IRequestImpl) request;
            req.SetDefaultValues();

            if ( request.MessageFormat == MessageFormat.SLM )
            {
                LoadTemplates( false );
                ConverterArgs args = ConvertRequest( req, false, requestTemplate );
                args.Request = Utils.ByteArrayToString( args.ReqByteArray );
                if ( args.Request.Length > requestTemplate.FormatIndicatorLength )
                {
                    args.Format = args.Request.Substring( 0, requestTemplate.FormatIndicatorLength );
                }

                return args;
            }

            LoadTemplates( true );
            return ConvertRequest( req, true, requestTemplate );
        }


        /// <summary>
        /// Converts a PNS-spec response to an XML document.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The response byte array is converted into a tree of DataElement
        /// objects that define the structure of the XML. Once it is built,
        /// the root DataElements are passed to the ResponseData object to be
        /// turned into XML.
        /// </para>
        /// <para>
        /// In all cases, a PNS response begins with a MessageHeader format,
        /// followed by a bitmap (with a possible secondary bitmap), followed
        /// by the data string for the various bitmaps. The converter builds
        /// the bitmap, and then converts just the bits that were specified.
        /// </para>
        /// <para>
        /// DataElements are quick to process and easier to
        /// work with than XmlNodes, so the Converter works with them. The
        /// ResponseData class manages building the XmlDocument from the
        /// list of DataElements supplied to it.
        /// </para>
        /// </remarks>
        /// <param name="respByteArray"></param>
        /// <param name="isMask"></param>
        /// <returns>An XML document for the response.</returns>
        private ConverterArgs ConvertPNSResponse( byte[] respByteArray )
        {
            try
            {
                LoadTemplates( true );
                ValidatePNSResponseCharSet( respByteArray );
                responseArgs = new ConverterArgs();
                responseArgs.Response = Utils.ByteArrayToISOString( respByteArray );
                responseArgs.MaskedResponse = responseArgs.Response;
                int bitmapPosition = responseTemplate.BitmapPosition;
                int bitmapLength = responseTemplate.BitmapLength;
                int bitmapSize = responseTemplate.BitmapSize;

                response = factory.MakeResponse();
                response.ResponseType = "PNS";
                PNSBitSet bitmap = new PNSBitSet( respByteArray, bitmapPosition, bitmapLength );
                if ( bitmap.Get( 1 ) )
                {
                    bitmap.AddSecondaryBitmap( respByteArray, bitmapPosition + 8, bitmapLength );
                    bitmapLength += 8;
                    bitmapSize *= 2;
                }

                List<DataElement> elements = new List<DataElement>();

                responseData = Utils.ByteArrayToString( respByteArray );
                responseData = responseData.Remove( bitmapPosition, bitmapLength );

                // Convert the Message Header
                Format format = responseTemplate.GetFormat( "MessageHeader" );
                if ( format == null )
                {
                    string msg = "No message header could be found in the template file. The template may be out of sync.";
                    logger.Error( msg );
                    throw new ConverterException( Error.InvalidHeaderInfo, msg );
                }

                DataElement header = new DataElement( "MessageHeader", null, response );
                ConvertResponseFormat( format, header );
                elements.Add( header );

                // Convert Each Bit
                for ( int bit = 2; bit <= bitmapSize; bit++ )
                {
                    if ( response.IsConversionError )
                    {
                        break;
                    }

                    if ( !bitmap.Get( bit ) )
                    {
                        continue;
                    }

                    try
                    {
                        string name = String.Format( "Bit{0}", bit );
                        format = responseTemplate.GetFormat( name );
                        if ( format.IsEmpty )
                        {
                            string msg = String.Format( "Failed to find the format for Bit {0}. The template may be out of sync.", bit );
                            logger.Error( msg );
                            throw new ConverterException( Error.BadDataError, msg );
                        }

                        DataElement element = new DataElement( name, null, response );
                        ConvertResponseFormat( format, element );
                        elements.Add( element );
                    }
                    catch ( ConverterException ex )
                    {
                        string msg = String.Format( "Exception caught while converting Bit {0}.", bit );
                        logger.Error( msg, ex );
                        throw;
                    }
                    catch ( Exception ex )
                    {
                        string msg = String.Format( "Exception caught while converting Bit {0}.", bit );
                        logger.Error( msg );
                        throw new ConverterException( Error.BadDataError, msg, ex );
                    }
                }

                responseArgs.Response = Utils.ByteArrayToString( respByteArray );
                Response resp = new Response();
                resp.IsConversionError = response.IsConversionError;
                resp.LeftoverData = response.LeftoverData;
                resp.ErrorDescription = response.ErrorDescription;
                resp.NumExtraFields = response.NumExtraFields;
                resp.DataElements = elements;
                resp.RawData = respByteArray;
                responseArgs.ResponseData = resp;
                return responseArgs;
            }
            catch ( ConverterException )
            {
                string msg = "Exception caught while creating the response XML";
                logger.Error( msg );
                throw;
            }

        }

        protected void LoadTemplates( bool isPNS )
        {
            RequestType type = ( isPNS ) ? RequestType.PNSOnline : RequestType.Online;
            if ( requestTemplate == null )
            {
                requestTemplate = factory.GetRequestTemplate( type );
                responseTemplate = factory.GetResponseTemplate( type );
            }
        }

        /// <summary>
        /// Convenience method to test if a string is either null or empty.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmpty( string value )
        {
            return ( value == null || value.Trim().Length == 0 );
        }

        /// <summary>
        /// Test to make sure that the Length attribute specified by the Format
        /// matches the total of the Lengths of each Field.
        /// </summary>
        /// <param name="format"></param>
        private void ValidateFormat( Format format )
        {
            if ( format.FormatType == FormatType.Bitmap || format.FormatType == FormatType.Variable || format.FormatType == FormatType.VariableArray )
            {
                return;
            }

            int totalLength = 0;

            foreach ( Field field in format.Fields )
            {
                totalLength += field.Lengths[ field.Lengths.Count - 1 ];
            }

            if ( ( format.Parent == null || format.Parent.FormatType != FormatType.VariableArray ) && ( format.Length != -1 && format.Length != totalLength ) )
            {
                string msg = String.Format( "The Format [{0}] shows inconsistent lengths. The format length is [{1}] while the combined field lengths are [{2}]",
                    format.Name, format.Length, totalLength );
                logger.Error( msg );
                throw new ConverterException( Error.LengthError, msg );
            }
        }

        private List<byte> ProcessChildFormats( IRequestImpl request, string baseName, Format format, PNSBitSet bitmap, out List<byte> maskedConvertedFormats )
        {
            maskedConvertedFormats = new List<byte>();
            List<byte> data = new List<byte>();
            List<byte> maskedData = new List<byte>();

            if ( format.FormatType == FormatType.VariableArray )
            {
                string name = String.Format( "{0}{1}.", baseName, format.Name );
                for ( int i = 0; i < request.Formats.Count; i++ )
                {
                    AddFormatTerminator( request, data, maskedData );
                    string origFormatIndicator = request.Formats[ i ].FormatIndicator;
                    string indicator = String.Format( "{0}{1}", request.Formats[ i ].FormatIndicator, i + 1 );
                    request.Formats[ i ].FormatIndicator = indicator;
                    ConverterArgs args = ConvertRequestFormat( request.Formats[ i ], name, bitmap );
                    // The request shouldn't change permanently. This is used by Request.XML
                    request.Formats[ i ].FormatIndicator = origFormatIndicator;
                    data.AddRange( args.ReqByteArray );
                    maskedData.AddRange( args.MaskedReqByteArray );
                }
                maskedConvertedFormats.AddRange( maskedData );
                return data;
            }

            for ( int i = 0; i < request.Formats.Count; i++ )
            {
                AddFormatTerminator( request, data, maskedData );
                IRequestImpl child = request.Formats[ i ];
                string name = String.Format( "{0}{1}.", baseName, format.Name );
                ConverterArgs args = ConvertRequestFormat( child, name, bitmap );
                data.AddRange( args.ReqByteArray );
                maskedData.AddRange( args.MaskedReqByteArray );
            }

            maskedConvertedFormats.AddRange( maskedData );
            return data;
        }

        private void AddFormatTerminator( IRequestImpl request, List<byte> data, List<byte> maskedData )
        {
            if ( !isPNS && request.IsBatch )
            {
                data.AddRange( requestTemplate.FormatDelimiter.GetBytes() );
                maskedData.AddRange( requestTemplate.FormatDelimiter.GetBytes() );
            }
        }

        /// <summary>
        /// Gets the prefix for the data record, if one exists.
        /// </summary>
        /// <remarks>
        /// There are two types of prefix for a format:
        /// 1. A static string
        /// 2. The total length of the record (not counting the prefix itself)
        ///
        /// If Prefix Data is supplied, then the prefix is static, and that value
        /// should be returned.
        ///
        /// If no Prefix Data is supplied, but the Prefix Length is valid, then
        /// the prefix is the total length.
        /// </remarks>
        /// <param name="format">The format whose prefix is to be determined.</param>
        /// <param name="length">The length of the data record.</param>
        /// <returns>The valid prefix, or an empty string if no prefix.</returns>
        private string GetPrefix( Format format, int length )
        {
            if ( format.PrefixLength == -1 )
            {
                return "";
            }

            string prefix = "";
            if ( format.PrefixData != null )
            {
                prefix = format.PrefixData;
            }

            string lengthStr = Convert.ToString( length );
            if ( format.PrefixLength < lengthStr.Length )
            {
                string allowedLength = "";
                allowedLength = Utils.PadLeft( allowedLength, '9', format.PrefixLength );
                string msg = "The data for the record [" + format.Name +
                    "] is too long. It can only be [" + allowedLength +
                    "] bytes long, but is [" + lengthStr + "].";
                logger.Error( msg );
                throw new ConverterException( Error.LengthError, msg );
            }

            prefix += Utils.PadLeft( lengthStr, '0', format.PrefixLength );
            return prefix;
        }

        private void PrepMultiLengthField( ref Field field, byte[] previousFieldValue )
        {
            if ( !field.MultiLength )
            {
                return;
            }

            int prev = Utils.StringToInt( Utils.ByteArrayToString( previousFieldValue ), -1 ) - 1;
            if ( prev < 0 || prev >= field.PrefixValues.Count )
            {
                int max = field.PrefixValues.Count;
                string msg = "Invalid prefix value. For field [" + field.Name +
                    "], the previous field must be set to a number from 1 to " + max.ToString();
                logger.Error( msg );
                throw new ConverterException( Error.RequiredFieldNotSet, msg );
            }

            field = new Field( field );
            field.Length = field.PrefixValues[ prev ];
        }

        /// <summary>
        /// Gets the appropriate length for the field.
        /// </summary>
        /// <remarks>
        /// Some fields have more than one valid length. This method iterates
        /// through them all to find the one that best matches the field's
        /// value.
        /// </remarks>
        /// <param name="convField">The field whose length is to be determined.</param>
        /// <param name="value">The field's value.</param>
        /// <returns></returns>
        private int GetFieldLength( Field convField, string value )
        {
            // If it's just a normal variable length with only one prefix length
            // return the value's length.
            if ( convField.Lengths.Count == 1 && convField.PrefixLength > 0 )
            {
                if ( value.Length <= convField.Lengths[ 0 ] )
                {
                    return value.Length;
                }
            }

            foreach ( int length in convField.Lengths )
            {
                if ( value.Length <= length )
                {
                    return length;
                }
            }

            string msg = String.Format( "The value for Field={0} is too long. Max=[{1}], length=[{2}]",
                convField.Name, convField.Lengths[ convField.Lengths.Count - 1 ], value.Length );
            logger.Error( msg );
            throw new ConverterException( Error.LengthError, msg );
        }

        /// <summary>
        /// Ensure that field values that are supposed to be numeric are valid numbers.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="convField"></param>
        private void ValidateNumericField( string value, Field convField )
        {
            if ( convField.FieldType == DataType.Numeric && value != null && value.Trim().Length > 0 )
            {
                if ( !Utils.IsNumeric( value ) )
                {
                    string msg = String.Format( "The value for Field={0} must be numeric, value=[{1}]", convField.Name, value );
                    logger.Error( msg );
                    throw new ConverterException( Error.FieldNotNumeric, msg );
                }
            }
        }

        private string MaskValue( string value, Field field, int fieldLength )
        {
            if ( field.MaskWith == null )
            {
                return value;
            }

            if ( field.MaskJustification == TextJustification.Right )
            {
                StringBuilder buff = new StringBuilder( value );
                char ch = field.MaskWith[ 0 ];
                for ( int i = 0; i < field.MaskLength && i < buff.Length; i++ )
                {
                    buff[ i ] = ch;
                }
                return Utils.PadLeft( buff.ToString(), ch, field.MaskLength );
            }
            else
            {
                StringBuilder buff = new StringBuilder( value );
                char ch = field.MaskWith[ 0 ];
                int len = ( fieldLength > buff.Length ) ? buff.Length : fieldLength;
                if ( len == 0 )
                {
                    len = 10;
                    buff = new StringBuilder( "          " );
                }
                for ( int i = len - 1; i >= 0; i-- )
                {
                    buff[ i ] = ch;
                }
                return Utils.PadLeft( buff.ToString(), ch, field.MaskLength );
            }
        }

        private void ValidatePNSResponseCharSet( byte[] response )
        {
            string data = null;

            try
            {
                data = Utils.ByteArrayToString( response );
            }
            catch ( Exception )
            {
                try
                {
                    data = Utils.ByteArrayToISOString( response );
                }
                catch ( Exception e1 )
                {
                    logger.Error( "Failed to convert Response Byte Array", e1 );
                }
            }

            if ( data == null )
            {
                string msg = "CRM must support US-ASCII or ISO-8859-1 char set";
                logger.Error( msg );
                throw new ConverterException( Error.InitializationFailure, msg );
            }
        }

        private int GetIntValueFromData( Format format, int dataLength )
        {
            return Utils.StringToInt( GetValueFromData( format, dataLength ) );
        }

        /// <summary>
        /// Converts the specified field into a DataElement. That element is
        /// then added to the supplied parent element.
        /// </summary>
        /// <param name="field">The field to be converted.</param>
        /// <param name="name">The name of the field. It can be different from the field's name.</param>
        /// <param name="format">The format that owns the field.</param>
        /// <param name="element">The parent DataElement.</param>
        /// <returns>The field's DataElement. This is used for dealing with aliases.</returns>
        private DataElement ConvertResponseField( Field field, string name, Format format, DataElement element )
        {
            try
            {
                string fieldDelimiter = responseTemplate.GetFieldDelimiter( responseData, format, configData );
                DataElement fieldElement = new DataElement( name, null, element );

                if ( field.Length == 0 && field.ArrayIndicator )
                {
                    fieldElement.Value = "";
                    AddAliases( field, fieldElement, element );
                    return fieldElement;
                }

                int dataLength = field.Length;
                string prefix = null;
                if ( field.PrefixLength > 0 )
                {
                    prefix = GetValueFromData( format, field.PrefixLength );
                    dataLength = Utils.StringToInt( prefix );
                }

                if ( dataLength == 0 && format.DataLength > 0 )
                {
                    dataLength = format.DataLength;
                }

                if ( field.IsFloating )
                {
                    fieldElement.Value = GetValueByPosition( field );
                }
                else
                {
                    string respVal = GetValueFromData( format, dataLength );
                    fieldElement.Value = ( respVal != null ) ? respVal.Trim() : respVal;
                }

                if ( fieldElement.Value != null )
                {
                    fieldElement.MaskedValue = MaskValue( fieldElement.Value, field, field.Length );
                    MaskResponseDataField( responseData, field, fieldDelimiter );
                }

                if ( format.DataLength > 0 )
                {
                    format.DataLength -= dataLength;
                }

                if ( field.SuffixLength > 0 )
                {
                    format.DataLength = GetIntValueFromData( format, field.SuffixLength );
                    if ( format.FormatType == FormatType.Static )
                    {
                        int length = format.Length - field.Length;
                        if ( format.DataLength != 0 && format.DataLength != length )
                        {
                            response.IsConversionError = true;
                            response.LeftoverData = TrimRecordDelimiter( format, responseData );
                            response.ErrorDescription = "The format [" + format.Name + "] must have a data length of " + length.ToString() + " but is " + format.DataLength.ToString();
                            logger.ErrorFormat( "Failed to convert the format [{0}], Details: [{1}]", format.Name, response.ErrorDescription );
                            return null;
                        }
                    }
                }

                if ( format.Name == "Default" && fieldElement.Name == "RecordType" )
                {
                    // If the name is numeric, skip this.
                    if ( !Utils.IsNumeric( fieldElement.Value.Substring( 0, 1 ) ) )
                    {
                        element.Name = fieldElement.Value;
                    }
                    else
                    {
                        element.Name = format.Alias;
                    }
                }

                if ( !field.HidePrefixColumnName && field.PrefixColumnName != null )
                {
                    DataElement columnName = new DataElement( field.PrefixColumnName, prefix, element );
                    element.Elements.Add( columnName );
                }

                if ( !field.Hide )
                {
                    element.Elements.Add( fieldElement );
                }

                // Add aliases.
                AddAliases( field, fieldElement, element );

                return fieldElement;
            }
            catch ( Exception ex )
            {
                string elementPath = ( field.FieldID != null ) ? field.FieldID : name;
                logger.DebugFormat( "Exception thrown while converting Field [{0}].", elementPath );
                throw ex;
            }
        }

        /// <summary>
        /// This is used to get the value from a record for a floating
        /// field -- one that does not have its position in list of
        /// calculated field positions.
        ///
        /// This is usually for optional fields that encapsulate the values
        /// of more than one regular field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string GetValueByPosition( Field field )
        {
            int pos = field.Position - 1;
            return this.record.Substring( pos, field.Length );
        }

        private void AddAliases( Field field, DataElement fieldElement, DataElement element )
        {
            if ( field.Alias != null )
            {
                int num = field.Aliases.Length;
                fieldElement.NumAliases = num;
                foreach ( string alias in field.Aliases )
                {
                    if ( alias.Trim() != fieldElement.Name )
                    {
                        DataElement aliasElement = new DataElement( alias.Trim(), null, element );
                        aliasElement.NumAliases = num;
                        aliasElement.Value = fieldElement.Value;
                        aliasElement.MaskedValue = MaskValue( fieldElement.Value, field, field.Length );
                        element.Elements.Add( aliasElement );
                    }
                }
            }
        }

        /// <summary>
        /// Gets the specified number of bytes from the response data.
        /// </summary>
        /// <remarks>
        /// Every value retrieved from the response data must be removed from
        /// the response. This is true for field values, prefixes, suffixes,
        /// and so on. In this way, every time a value is retrieved from the
        /// response data, it is taken from the beginning of the response
        /// data string. This method enforces that behavior by removing the
        /// returned string from the response data.
        /// </remarks>
        /// <param name="dataLength">The number of bytes to get.</param>
        /// <returns>The requested string value.</returns>
        private string GetValueFromData( Format format, int dataLength )
        {
            if ( responseData.Length < dataLength )
            {
                response.IsConversionError = true;
                response.ErrorDescription = "There is not enough data in record \"" + format.Name + "\". The templates may be out of sync";
                response.LeftoverData = TrimRecordDelimiter( format, responseData );
                responseData = "";
                return null;
            }
            string data = responseData.Substring( 0, dataLength );
            responseData = responseData.Substring( dataLength );
            return ( data != null && data.Trim().Length > 0 ) ? data : null;
        }

        private void MaskResponseDataField( string responseData, Field field, string fieldDelimiter )
        {
            if ( fieldDelimiter == null )
            {
                MaskOnlineResponseDataField( responseData, field );
                return;
            }

            string[] fields = responseData.Split( fieldDelimiter[ 0 ] );
            if ( fields.Length < field.Index )
            {
                return;
            }
            string value = Utils.PadLeft( "", '#', fields[ field.Index - 1 ].Length );
            StringBuilder response = new StringBuilder();
            for ( int i = 0; i < fields.Length; i++ )
            {
                if ( i == field.Index - 1 )
                {
                    response.Append( value );
                }
                else
                {
                    response.Append( fields[ i ] );
                }

                if ( i < fields.Length - 1 )
                {
                    response.Append( fieldDelimiter );
                }
            }
            responseArgs.MaskedResponse = response.ToString();
        }

        /// <summary>
        /// Masks the field in the raw record.
        /// </summary>
        /// <remarks>
        /// The responseData parameter contains the data of the record just after the field being masked.
        /// So, the position to start masking is the field's length before the position of responseData
        /// inside MaskedResponse.
        /// </remarks>
        /// <param name="responseData"></param>
        /// <param name="field"></param>
        private void MaskOnlineResponseDataField( string responseData, Field field )
        {
            if ( field.MaskWith == null || responseArgs == null || responseArgs.MaskedResponse == null )
            {
                if ( responseArgs.MaskedResponse == null )
                {
                    responseArgs.MaskedResponse = responseArgs.Response;
                }
                return;
            }

            string respData = responseArgs.MaskedResponse;
            string value = Utils.PadLeft( "", '#', field.Length );
            StringBuilder maskedData = new StringBuilder();
            int pos = respData.IndexOf( responseData ) - field.Length + 1;
            if ( pos < 0 )
            {
                return;
            }
            maskedData.Append( respData.Substring( 0, pos - 1 ) );
            maskedData.Append( value );
            maskedData.Append( respData.Substring( pos + field.Length - 1 ) );
            responseArgs.MaskedResponse = maskedData.ToString();
        }

        protected bool GetPositionBasedFormatName( ConverterArgs args, string data, List<FormatItem> items )
        {
            List<FormatItem> formatItems = GetMatchingFormatItems( data, items );
            foreach ( FormatItem formatItem in formatItems )
            {
                if ( GetPositionBasedFormatName( args, data, items, formatItem ) )
                {
                    return true;
                }
            }

            return false;
        }

        protected bool GetPositionBasedFormatName( ConverterArgs args, string data, List<FormatItem> items, FormatItem parentItem )
        {
            FormatItem formatItem = ( parentItem != null ) ? parentItem : GetMatchingFormatItem( data, items );

            if ( formatItem == null )
            {
                return false;
            }

            if ( formatItem.position - 1 + formatItem.length > data.Length )
            {
                return false;
            }

            string value = data.Substring( formatItem.position - 1, formatItem.length );

            FormatItem foundItem = formatItem.GetItem( value );
            if ( foundItem != null )
            {
                if ( foundItem.formatName != null )
                {
                    args.Format = foundItem.formatName;
                    args.StrData = foundItem.responseType;
                }
                else if ( foundItem.items.Count > 0 )
                {
                    return GetPositionBasedFormatName( args, data, foundItem.items, foundItem );
                }
            }

            return ( foundItem != null );
        }

        protected FormatItem GetMatchingFormatItem( string data, List<FormatItem> items )
        {
            List<FormatItem> returnItems = GetMatchingFormatItems( data, items );
            return ( returnItems.Count > 0 ) ? returnItems[ 0 ] : null;
        }

        protected List<FormatItem> GetMatchingFormatItems( string data, List<FormatItem> items )
        {
            List<FormatItem> returnItems = new List<FormatItem>();
            FormatItem defaultItem = null;

            if ( data == null )
            {
                return returnItems;
            }

            foreach ( FormatItem item in items )
            {
                if ( data.StartsWith( item.Name ) )
                {
                    returnItems.Add( item );
                }
                if ( item.name == "Default" )
                {
                    defaultItem = item;
                }
            }

            if ( returnItems.Count == 0 )
            {
                returnItems.Add( defaultItem );
            }

            return returnItems;
        }

        protected bool GetMOPBasedFormatName( ConverterArgs args, string data, List<FormatItem> items )
        {
            FormatItem item = GetMatchingFormatItem( data, items );
            if ( item == null )
            {
                return false;
            }

            string mop = this.methodOfPayment;
            if ( mop == null )
            {
                mop = GetMOPFromOrderRecord( data );
            }
            if ( !item.ContainsItem( mop ) )
            {
                mop = "Default";
            }
            if ( !item.ContainsItem( mop ) )
            {
                return false;
            }

            item = item.GetItem( mop );
            if ( item != null )
            {
                args.Format = item.formatName;
                args.StrData = item.responseType;
            }

            return ( item != null );
        }

        protected string GetMOPFromOrderRecord( string data )
        {
            ConverterTemplate.Order order = responseTemplate.GetOrder();

            Format format = responseTemplate.GetFormat( order.formatName );
            if ( format.IsEmpty )
            {
                return "Default";
            }

            Field field = format[ order.mopFieldName ];
            if ( field.IsEmpty )
            {
                return "Default";
            }

            // Find the format that contains the MOP field.
            int formatLen = responseTemplate.DefaultFormatLength + 1;
            string formatData = null;
            for ( int position = 0; position + formatLen <= data.Length; position += formatLen )
            {
                string substr = data.Substring( position );
                if ( substr.StartsWith( order.formatName ) )
                {
                    formatData = data.Substring( position );
                    break;
                }
            }

            if ( formatData == null )
            {
                return "Default";
            }

            string mop = formatData.Substring( field.Position - 1, field.Length );
            return mop;
        }
    }
}
