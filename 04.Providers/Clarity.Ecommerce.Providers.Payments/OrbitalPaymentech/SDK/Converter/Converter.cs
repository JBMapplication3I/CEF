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

// (c)2017, Paymentech, LLC. All rights reserved

namespace JPMC.MSDK.Converter
{

    public class Converter : IConverter
    {
        private static readonly string MESSAGE_HEADER = "MessageHeader";
        private static readonly string FORMAT_CONVERT_FAILED = "Failed to convert the format [{0}], Details: [{1}]";
        private static readonly string ARRAY_FORMAT_ID = "{0}[{1}].{2}";
        private static readonly string DEFAULT_FORMAT = "Default";

        protected RequestTemplate requestTemplate;
        protected ResponseTemplate responseTemplate;
        protected ILog logger;
        protected IConverterFactory factory;
        public ConverterArgs RecordInfo { get; set; }
        protected CommModule module = CommModule.Unknown;
        protected ConfigurationData configData;

        private IFullResponse response;
        private string responseData;
        private ConverterArgs responseArgs;
        private bool isPNS;

        // For handling MOP Based format mapping.
        protected string MethodOfPayment { get; set; }
        protected string record = null;

        public class DataElementPair
        {
            public DataElement element;
            public Format format;
        }

        public Converter()
        {
        }

        public Converter( ConfigurationData configData, IConverterFactory factory )
        {
            this.factory = factory;
            try
            {
                logger = factory.EngineLogger;
            } catch ( DispatcherException e )
            {
                throw new ConverterException( Error.InitializationFailure, "Failed to initialize logging.", e );
            }
            this.configData = configData;

            this.module = configData.Protocol;
        }


        private ConverterArgs ConvertRequest( IRequestImpl request, bool isPNS, RequestTemplate onlineRequest )
        {
            this.requestTemplate = onlineRequest;
            this.isPNS = isPNS;

            var data = new List<byte>();
            var maskedData = new List<byte>();

            Format format;
            try
            {
                format = request.GetFormat( request.TransactionType );
            }
            catch ( RequestException e )
            {
                throw new ConverterException( Error.BadDataError, "Failed to convert format", e );
            }

            foreach ( var prefixFormat in format.PrefixFormats )
            {
                if ( request.UsesFormat( prefixFormat.Name ) )
                {
                    ConvertRootFormats( prefixFormat, request, data, maskedData, onlineRequest, false );
                }
            }

            ConvertRootFormats( format, request, data, maskedData, onlineRequest, true );

            if ( !request.IsBatch && onlineRequest.MessageDelimiter != null )
            {
                data.AddRange( onlineRequest.MessageDelimiter.GetBytes() );
                maskedData.AddRange( onlineRequest.MessageDelimiter.GetBytes() );
            }

            var args = new ConverterArgs();
            args.ReqByteArray = data.ToArray();
            args.Request = Utils.ByteArrayToString( args.ReqByteArray );
            args.MaskedReqByteArray = maskedData.ToArray();
            args.MaskedRequest = Utils.ByteArrayToString( maskedData.ToArray() );
            return args;
        }

        private void ConvertRootFormats( Format format, IRequestImpl request, List<byte> data, List<byte> maskedData, RequestTemplate onlineRequest, bool skipPrefixFormats )
        {
            var convArgs = ConvertRequestFormat( format, request, "", null, -1, skipPrefixFormats );
            data.AddRange( convArgs.ReqByteArray );
            maskedData.AddRange( convArgs.MaskedReqByteArray );
            if ( !isPNS && request.IsBatch )
            {
                var bytes = onlineRequest.FormatDelimiter.GetBytes();
                data.AddRange( bytes );
                maskedData.AddRange( bytes );
            }
        }

        private ConverterArgs ConvertRequestFormat( Format format, IRequestImpl request, string baseName, PNSBitSet prevBitmap, int arrayIndex, bool skipPrefixFormats )
        {
            var retArgs = new ConverterArgs();
            var bitmap = prevBitmap;
            var newBitmap = false;
            if ( format.IsEmpty )
            {
                var msg = "The format [" + format.FormatIndicator + "] could not be found.";
                logger.Error( msg );
                throw new ConverterException( Error.FormatNotFoundError, msg );
            }

            var convertedData = new List<byte>();
            var maskedConvertedData = new List<byte>();
            var data = new List<byte>();
            var maskedData = new List<byte>();

            if ( format.FormatType == FormatType.Bitmap )
            {
                bitmap = new PNSBitSet();
                newBitmap = true;
            }

            ValidateFormat( format );

            // Set the appropriate bit : the bitmap, if applicable.
            if ( format.Bit != -1 && bitmap != null )
            {
                bitmap.Set( format.Bit );
            }

            // Convert fields
            byte[] previousFieldValue = null;
            foreach ( var field in format.Fields )
            {
                var args = ConvertRequestField( request, field, format, previousFieldValue, arrayIndex );
                previousFieldValue = args.ReqByteArray;

                data.AddRange( previousFieldValue );
                if ( args.MaskedRequest != null )
                {
                    if ( field.HasMask && args.MaskedResponse != null )
                    {
                        maskedData.AddRange( args.MaskedResponse.GetBytes() );
                    }
                    maskedData.AddRange( args.MaskedRequest.GetBytes() );
                }
            }

            // Convert sub-formats
            var convertedFormats = new List<byte>();
            var maskedConvertedFormats = new List<byte>();
            if ( format.GetFormatCount() > 0 )
            {
                var maskedChildData = new List<byte>();
                var children = ProcessChildFormats( request, baseName, format, bitmap, maskedChildData, arrayIndex, skipPrefixFormats );
                convertedFormats.AddRange( children );
                maskedConvertedFormats.AddRange( maskedChildData );
            }

            CreateBitmap( newBitmap, bitmap, format, convertedData, maskedConvertedData );

            convertedData.AddRange( data );
            convertedData.AddRange( convertedFormats );

            maskedConvertedData.AddRange( maskedData );
            maskedConvertedData.AddRange( maskedConvertedFormats );

            // The bitmap might need to be inserted after the MessageHeader,
            // if a MessageHeader is supplied.
            if ( bitmap != null && format.Formats.Count > 0 && format.Formats.Get( 0 ).Name.Equals( MESSAGE_HEADER ) )
            {
                var bitSet = bitmap.BitSetToBytes();
                var header = format.Formats.Get( 0 );
                convertedData.InsertRange( header.Length, bitSet );
                maskedConvertedData.InsertRange( header.Length, bitSet );
            }

            var prefix = GetPrefix( format, convertedData.Count );
            var prefixBytes = prefix.GetBytes();
            convertedData.InsertRange( 0, prefixBytes );
            maskedConvertedData.InsertRange( 0, prefixBytes );

            retArgs.ReqByteArray = convertedData.ToArray();
            retArgs.MaskedReqByteArray = maskedConvertedData.ToArray();
            return retArgs;
        }

        private void CreateBitmap( bool newBitmap, PNSBitSet bitmap, Format format, List<byte> convertedData, List<byte> maskedConvertedData )
        {
            try
            {
                if ( newBitmap && bitmap != null )
                {
                    if ( format.BitMapType == BitMapType.Hex )
                    {
                        var bitSet = bitmap.BitSetToIsoHex().GetBytes();
                        convertedData.AddRange( bitSet );
                        maskedConvertedData.AddRange( bitSet );
                    }
                    else
                    {
                        var bitSet = bitmap.BitSetToBytes();
                        if ( format.Formats.Count == 0 || !format.Formats.Get( 0 ).Name.Equals( MESSAGE_HEADER ) )
                        {
                            convertedData.AddRange( bitSet );
                            maskedConvertedData.AddRange( bitSet );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                var msg = "Failed to create the bitmap.";
                logger.Error( msg, ex );
                throw new ConverterException( Error.BadDataError, msg, ex );
            }
        }


        private ConverterArgs ConvertRequestField( IRequestImpl request, Field field, Format format, byte[] previousFieldValue, int arrayIndex )
        {
            var convField = field;
            var retVal = new ConverterArgs();
            string maskedValue = null;

            var fieldValue = SetInitialFieldValue( arrayIndex, convField, request, format, retVal );
            if ( retVal.ReqByteArray != null )
            {
                return retVal;
            }

            convField = PrepMultiLengthField( convField, fieldValue, previousFieldValue );

            var fieldLength = 0;

            if ( IsEmpty( fieldValue ) && convField.DefaultValue != null )
            {
                if ( fieldValue.Length > 1 && convField.Length > fieldValue.Length )
                {
                    fieldValue = Utils.PadLeft( fieldValue, convField.DefaultValue[ 0 ], convField.Length );
                }
                else
                {
                    fieldValue = convField.DefaultValue;
                }
            }

            if ( convField.IsRequired && IsEmpty( fieldValue ) )
            {
                var formatName = format.Parent != null ? format.Alias : "";
                var msg = $"The required field \"{convField.FieldID}\" is not set.";
                logger.Error( msg );
                throw new ConverterException( Error.RequiredFieldNotSet, msg );
            }

            ValidateNumericField( fieldValue, convField );

            if ( convField.PrefixLength > 0 && fieldValue != null )
            {
                var prefix = fieldValue.Length.ToString();
                fieldLength = fieldValue.Length;
                if ( prefix.Length > convField.PrefixLength )
                {
                    var msg =
                        $"The field [{convField.Name}] is too long. The prefix Length is [{prefix.Length}], but must be [{convField.PrefixLength}].";
                    logger.Error( msg );
                    throw new ConverterException( Error.LengthError, msg );
                }
                prefix = Utils.PadLeft( prefix, '0', convField.PrefixLength );
                fieldValue = prefix + fieldValue;
                retVal.MaskedResponse = prefix;
                if ( convField.HasMask )
                {
                    var str = Utils.PadLeft( "", '#', prefix.Length );
                    retVal.MaskedResponse = str;
                }
            }
            else
            {
                fieldLength = convField.Length;
            }

            fieldValue = FillWith( fieldValue, convField, fieldLength, format.FormatType );
            fieldValue = fieldValue == null ? "" : fieldValue;

            if ( convField.CaseType == Case.Upper )
            {
                fieldValue = fieldValue.ToUpper();
            }
            else if ( convField.CaseType == Case.Lower )
            {
                fieldValue = fieldValue.ToLower();
            }

            maskedValue = MaskValue( fieldValue, convField, fieldLength );
            maskedValue = maskedValue == null ? fieldValue : maskedValue;

            retVal.MaskedRequest = maskedValue;
            retVal.ReqByteArray = fieldValue.GetBytes();
            return retVal;
        }

        private string SetInitialFieldValue( int arrayIndex, Field convField, IRequestImpl request, Format format, ConverterArgs retVal )
        {
            string fieldValue = null;

            try
            {
                if ( arrayIndex == -1 )
                {
                    fieldValue = request.GetField( convField.FieldID );
                }
                else
                {
                    fieldValue = request.GetField( convField.FieldID, arrayIndex );
                }

                if ( convField.FieldType == DataType.Binary && request.GetBinaryField( convField.FieldID ) == null )
                {
                    try
                    {
                        var binaryData = Convert.FromBase64String( fieldValue );
                        request.SetField( convField.FieldID, binaryData );
                    }
                    catch ( Exception ex )
                    {
                        var msg = "The value of field " + format.FormatID + " is not a valid Base64 String."; ;
                        logger.Error( msg, ex );
                        throw new ConverterException( Error.BadDataError, msg, ex );
                    }
                }

                var binaryValue = request.GetBinaryField( convField.FieldID );

                if ( request.GetBinaryField( convField.FieldID ) != null )
                {
                    if ( binaryValue.Length != convField.Length )
                    {
                        var msg =
                            $"The field [{convField.Name}] is {binaryValue.Length} bytes long when it must be {convField.Length} bytes long.";
                        logger.Error( msg );
                        throw new ConverterException( Error.LengthError, msg );
                    }

                    retVal.ReqByteArray = binaryValue;
                }
            }
            catch ( RequestException ex )
            {
                throw new ConverterException( Error.FieldDoesNotExist, ex.Message, ex );
            }

            if ( fieldValue == null )
            {
                fieldValue = "";
            }

            return fieldValue;
        }

        private string FillWith( string val, Field convField, int fieldLength, FormatType formatType )
        {
            if ( fieldLength == 0 )
            {
                return val;
            }

            var value = val;

            if ( convField.FillWith != null )
            {
                if ( convField.Justification == TextJustification.Right )
                {
                    // If the value is a single space, we always pad with spaces,
                    // even for numeric fields.
                    var fillWithChar = convField.FillWith[ 0 ];
                    if ( fillWithChar == '0' && value.Equals( " " ) )
                    {
                        fillWithChar = ' ';
                    }
                    value = Utils.PadLeft( value, fillWithChar, fieldLength );
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

        /**
         * Converts a Salem-spec response string to an XML document.
         *
         * The response string is converted into a tree of DataElement objects that
         * define the structure of the XML. Once it is built, the root DataElements
         * are passed to the ResponseData object to be turned into XML.
         *
         * DataElements are quick to process and easier to work with than XmlNodes,
         * so the Converter works with them. The ResponseData class manages building
         * the XmlDocument from the list of DataElements supplied to it.
         *
         * @param respData
         * @return
         * @
         */
        private ConverterArgs ConvertSalemResponse( string respData )
        {
            string formatName = null;

            try
            {
                logger.Debug( "Converting a Salem Spec Response" );
                LoadTemplates( false );
                response = factory.MakeResponse();
                //response..SetIsPNS( false );
                responseData = respData;
                var formatIndLen = responseTemplate.FormatIndicatorLength;
                responseArgs = new ConverterArgs();
                responseArgs.MaskedResponse = respData;
                responseArgs.Response = respData;

                if ( responseData.Length < formatIndLen )
                {
                    var msg = "The response data is shorter than the format indicator Length. The data returned from the server appears to be corrupt";
                    logger.Error( msg );
                    throw new ConverterException( Error.BadDataError, msg );
                }

                var formatIndicator = responseData.Substring( 0, formatIndLen );

                // Is there a base Batch report container?
                var containerFormat = GetContainerFormat();
                var elements = new SafeDictionary<string, DataElement>();

                var parent = new DataElementPair();
                while ( responseData.Length > 0 && !responseTemplate.IsMessageDelimiter( responseData ) )
                {
                    formatName = ProcessResponseData( formatIndLen, containerFormat, elements, parent );
                    if ( response.IsConversionError )
                    {
                        break;
                    }
                }

                if ( !respData.EndsWith( responseTemplate.MessageDelimiter ) && !respData.EndsWith( responseTemplate.TCPRecordDelimiter ) && !respData.EndsWith( responseTemplate.SFTPRecordDelimiter ) )
                {
                    this.response.IsConversionError = true;
                    this.response.ErrorDescription = "The record does not end with the record terminator";
                    this.response.LeftoverData = "";
                }

                this.response.RawData = respData.GetBytes();
                this.response.DataElements = new List<DataElement>( elements.Values );
                this.responseArgs.Format = formatIndicator;
                this.responseArgs.Response = respData;
                this.responseArgs.ResponseData = this.response;
                return this.responseArgs;
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

        private string ProcessResponseData( int formatIndLen, Format containerFormat, SafeDictionary<string, DataElement> elements, DataElementPair parent )
        {
            var formatName = responseData.Substring( 0, formatIndLen );

            var format = GetSLMFormat( formatName, containerFormat );
            if ( format.IsEmpty )
            {
                MakeConversionError( format, formatName, "The remaining data does not belong to a known format." );
                return formatName;
            }

            var element = CreateDataElement( format, response, elements );

            var isFormatRef = AddDataElement( element, parent.format, parent.element, elements );
            if ( !isFormatRef )
            {
                parent.element = null;
                parent.format = null;
            }


            if ( format.HasFormatRefs )
            {
                parent.format = format;
                parent.element = element;
            }

            CreateElementsForAliases( format, element, parent.element, isFormatRef, elements );

            if ( this.responseData.StartsWith( this.responseTemplate.MessageDelimiter ) )
            {
                this.responseData = this.responseData.Substring( 1 );
            }

            // SFTPBatch response files sometimes have an extra newline at
            // the end of each
            // record. We must Remove it, if it exists.
            if ( this.module == CommModule.SFTPBatch && this.responseData.StartsWith( "\n" ) )
            {
                this.responseData = responseData.Remove( 0, 1 );
            }

            return formatName;
        }

        private Format GetSLMFormat( string formatName, Format containerFormat )
        {
            logger.Debug( "Getting format [" + formatName + "]" );
            var format = GetFormatFromContainer( formatName, containerFormat );
            if ( format.IsEmpty )
            {
                format = responseTemplate.GetFormat( formatName );
            }

            if ( format.IsEmpty )
            {
                var finder = FindFormat( responseData, this.MethodOfPayment );
                if ( finder != null )
                {
                    formatName = finder.FormatName;
                    format = responseTemplate.GetFormat( formatName );
                }
            }

            if ( format.IsEmpty )
            {
                format = GetVariableLengthNamedFormat( responseData );
            }

            return format;
        }

        private void MakeConversionError( Format format, string formatName, string v )
        {
            response.IsConversionError = true;
            response.LeftoverData = TrimRecordDelimiter( format, responseData );
            response.ErrorDescription = v;
            var msg = string.Format( FORMAT_CONVERT_FAILED, formatName, response.ErrorDescription );
            logger.Error( msg );
        }

        private DataElement CreateDataElement( Format format, IFullResponse response, SafeDictionary<string, DataElement> elements )
        {
            DataElement element = null;
            try
            {
                element = new DataElement( format.Aliases[ 0 ], null, response );
                var aliasLength = format.Aliases.Length > 0 ? format.Aliases.Length : 1;
                element.NumAliases = aliasLength;
                element.HideFromFieldID = format.HideFromFieldID;
                ConvertResponseFormat( format, element );
                element.Name = format.Aliases[ 0 ];
                DealWithArray( elements, element );
                return element;
            }
            catch ( Exception ex )
            {
                var msg = $"Failed to create the element {element.Name} to the response.";
                logger.Error( msg, ex );
                throw new ConverterException( Error.ResponseFailure, msg, ex );
            }
        }

        private bool AddDataElement( DataElement element, Format parentFormat, DataElement parentElement, SafeDictionary<string, DataElement> elements )
        {
            var isFormatRef = false;

            try
            {
                if ( parentFormat != null && parentElement != null && parentFormat.IsFormatRef( element.Name ) )
                {
                    AddFormatRefElement( parentElement, element );
                    isFormatRef = true;
                }
                else
                {
                    isFormatRef = false;
                    parentElement = null;
                    parentFormat = null;
                    elements.Add( element.FieldID, element );
                }
            }
            catch ( Exception ex )
            {
                var msg = $"Failed to add the element {element.Name} to the response.";
                logger.Error( msg, ex );
                throw new ConverterException( Error.ResponseFailure, msg, ex );
            }

            return isFormatRef;
        }

        private void CreateElementsForAliases( Format format, DataElement element, DataElement parentElement, bool isFormatRef, SafeDictionary<string, DataElement> elements )
        {
            for ( var i = 1; i < format.Aliases.Length; i++ )
            {
                string newName = null;
                try
                {
                    var oldName = element.Name;
                    oldName = oldName.Contains( "[" ) ? oldName.Substring( 0, oldName.IndexOf( '[' ) + 1 ) : oldName;
                    newName = format.Aliases[ i ];
                    if ( newName == null )
                    {
                        continue;
                    }
                    var clone = new DataElement( element );
                    clone.Name = newName;
                    for ( var j = 0; j < clone.Elements.Count; j++ )
                    {
                        var id = clone.Elements[ j ].FieldID;
                        id = id.Replace( oldName, newName );
                        clone.Elements[ j ].FieldID = id;
                    }
                    DealWithArray( elements, clone );
                    if ( isFormatRef )
                    {
                        if ( parentElement != null )
                        {
                            parentElement.Elements.Add( clone );
                        }
                    }
                    else
                    {
                        elements.Add( clone.FieldID, clone );
                    }
                }
                catch ( Exception ex )
                {
                    newName = newName == null ? "[null]" : newName;
                    var msg = $"Failed to convert the alias {newName} for the response.";
                    logger.Error( msg, ex );
                    throw new ConverterException( Error.ResponseFailure, msg, ex );
                }
            }
        }

        protected FormatFinder FindFormat( string responseData, string methodOfPayment )
        {
            if ( responseData == null )
            {
                return null;
            }

            foreach ( var finder in responseTemplate.FormatFinders )
            {
                if ( finder.FindFormat( responseData, methodOfPayment ) )
                {
                    return finder;
                }
            }

            return null;
        }

        private void AddFormatRefElement( DataElement parentElement, DataElement newElement )
        {
            var prevIndex = -1;
            newElement.FieldID = parentElement.FieldID + "." + newElement.FieldID;
            if ( newElement.Elements.Count > 0 )
            {
                var name = newElement.Elements[ 0 ].FieldID;
                newElement.Elements[ 0 ].FieldID = parentElement.FieldID + "." + name;
            }

            for ( var i = 0; i < parentElement.Elements.Count; i++ )
            {
                var elem = parentElement.Elements[ i ];

                if ( elem.Elements.Count == 0 )
                {
                    continue;
                }

                var prevFieldID = elem.Elements[ 0 ].FieldID;
                var testPrev = Utils.StripArrayIndex( prevFieldID );
                var currFieldID = newElement.Elements[ 0 ].FieldID;

                if ( testPrev.Equals( currFieldID ) )
                {
                    prevIndex = GetArrayIndex( prevFieldID );

                    // Add index to array elem that had none before.
                    if ( prevIndex == -1 )
                    {
                        prevIndex = 0;
                        var id = string.Format( ARRAY_FORMAT_ID, elem.FieldID, prevIndex, elem.Elements[ 0 ].Name );
                        elem.Elements[ 0 ].FieldID = id;
                    }
                }
            }

            if ( prevIndex == -1 )
            {
                parentElement.Elements.Add( newElement );
            } else
            {
                var currFieldID = string.Format( ARRAY_FORMAT_ID, newElement.FieldID, prevIndex + 1, newElement.Elements[ 0 ].Name );

                newElement.Elements[ 0 ].FieldID = currFieldID;
                parentElement.Elements.Add( newElement );
            }
        }

        private int GetArrayIndex( string array )
        {
            var start = array.IndexOf( '[' );
            var end = array.IndexOf( ']' );
            if ( start == -1 || end == -1 )
            {
                return -1;
            }

            var pos = start + 1;
            var len = end - pos;

            var num = array.Substring( pos, len );
            return Convert.ToInt32( num.Trim() );
        }

        private void DealWithArray( SafeDictionary<string, DataElement> elements, DataElement element )
        {
            var fieldID = element.FieldID;
            if ( !elements.ContainsKey( fieldID ) && !elements.ContainsKey( fieldID + "[0]" ) )
            {
                return;
            }

            if ( elements.ContainsKey( fieldID ) )
            {
                var elem = elements[ element.FieldID ];
                elem.SetArrayIndex( 0 );
                elements.Remove( fieldID );
                elements.Add( elem.FieldID, elem );

                element.SetArrayIndex( 1 );
                return;
            }

            // Count the number of elements.
            var count = 0;
            for ( count = 0; elements.ContainsKey($"{fieldID}[{count}]"); count++ )
            {
                // Counting.
            }

            element.SetArrayIndex( count );
        }

        private string TrimRecordDelimiter( Format format, string data )
        {
            var delim = responseTemplate.GetRecordDelimiter( format, module );
            delim = delim == null ? responseTemplate.MessageDelimiter : delim;
            var resp = data;
            if ( delim != null && data.EndsWith( delim ) )
            {
                resp = data.Substring( 0, data.Length - delim.Length + 1 );
            }

            return resp;
        }

        private Format GetVariableLengthNamedFormat( string data )
        {
            for ( var i = responseTemplate.FormatIndicatorLength + 1; i < 7 && i < data.Length; i++ )
            {
                var format = GetFormat( data, i );
                if ( !format.IsEmpty )
                {
                    return format;
                }
            }

            return new Format();
        }

        private Format GetFormat( string name, int Length )
        {
            return responseTemplate.GetFormat( name.Substring( 0, Length ) );
        }

        protected Format GetContainerFormat()
        {
            var containerFormat = new Format();
            if ( RecordInfo != null && RecordInfo.Format != null )
            {
                containerFormat = responseTemplate.GetFormat( RecordInfo.Format );
            }
            return containerFormat;
        }

        /**
         * Gets the format from a container format, if necessary.
         *
         * Some batch formats are grouped together inside a container format. For
         * instance, report files have a header, detail, and trailer record that
         * belongs to one specific report. So, there is a format for the report
         * (like HBCBIN), and it has a format for each of its records inside it, as
         * child formats.
         *
         * This method will get the format from the container format, if one exists.
         *
         * @param formatName
         * @param containerFormat
         * @param format
         * @return
         * @
         */
        protected Format GetFormatFromContainer( string formatName, Format containerFormat )
        {
            if ( containerFormat.IsEmpty )
            {
                return new Format();
            }

            if ( containerFormat.HasFormat( formatName ) )
            {
                return containerFormat.GetFormat( formatName );
            }

            return containerFormat.GetFormatByData( responseData );
        }

        /**
         * Converts a single format into DataElements.
         *
         * @param format
         *            The Format to be converted.
         * @param element
         *            The DataElement to insert the new format's DataElement into.
         * @
         */
        private void ConvertResponseFormat( Format format, DataElement element )
        {
            logger.Debug($"Converting format \"{format.Name}\"");
            element.Name = format.Alias != null ? format.Aliases[0] : element.Name;

            if ( format.PrefixLength > 0 )
            {
                format.DataLength = GetIntValueFromData( format, format.PrefixLength );
                logger.Debug($"Setting getDataLength() for format \"{format.Name}\" to \"{format.DataLength}\".");
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
            var dataLength = 0;
            foreach ( var field in fields )
            {
                var fieldElement = ConvertResponseField( field, field.Aliases[0], format, element );
                if ( response.IsConversionError )
                {
                    break;
                }

                dataLength = dataLength == 0 ? format.DataLength : dataLength;

                if ( field.ArrayIndicator )
                {
                    var itemCount = Utils.StringToInt( fieldElement.Value, 0 );
                    var count = 0;
                    // format.DataLength will be -1 in Salem responses, so we must
                    // rely on the item count.
                    // format.DataLength will be > 0 in PNS responses, so use that
                    // and then compare to item count afterward.
                    while ( format.DataLength == -1 && count < itemCount || format.DataLength > 0 )
                    {
                        var name = $"{field.ArrayNodeName}[{count}]";
                        var arrayNode = new DataElement( name, null, element );
                        element.Elements.Add( arrayNode );
                        ProcessResponseFields( field.Fields, format, arrayNode );
                        count++;
                    }

                    if ( itemCount > 0 && itemCount != count )
                    {
                        response.IsConversionError = true;
                        response.LeftoverData = TrimRecordDelimiter( format, responseData );
                        response.ErrorDescription =
                            $"The format [{format.Name}] must have {itemCount} array items, but has {count} instead.";
                        logger.Error( string.Format( FORMAT_CONVERT_FAILED, format.Name, response.ErrorDescription ) );
                        return;
                    }
                }
            }
        }

        /**
         * Some getFormats() ins a Response have child formats. This method iterates
         * through them and converts them.
         *
         * @param format
         * @param element
         * @
         */
        private void ConvertResponseSubFormats( Format format, DataElement element )
        {
            if ( format.FormatType != FormatType.MultiFormat )
            {
                return;
            }

            var bytesConverted = 0;
            while ( bytesConverted < format.DataLength )
            {
                if ( response.IsConversionError )
                {
                    return;
                }

                var subFormat = GetSubFormat( format );

                var lengthBefore = responseData.Length;
                var childElement = new DataElement( subFormat.Alias, null, element );
                element.HideFromFieldID = subFormat.HideFromFieldID;
                ConvertResponseFormat( subFormat, childElement );
                element.Elements.Add( childElement );
                for ( var i = 1; i < subFormat.Aliases.Length; i++ )
                {
                    var alias = new DataElement( childElement );
                    alias.Name = subFormat.Aliases[i];
                    element.Elements.Add( alias );
                }
                bytesConverted += lengthBefore - responseData.Length;
            }
        }

        private Format GetSubFormat( Format format )
        {
            var formatIndicator = responseData.Substring( 0, responseTemplate.FormatIndicatorLength );
            var subFormat = format.GetFormat( formatIndicator );
            if ( subFormat == null || subFormat.IsEmpty )
            {
                // Sometimes the format name is longer than the format
                // indicator.
                subFormat = format.GetFormatByData( responseData );
                if ( subFormat == null || subFormat.IsEmpty )
                {
                    // Is there a Default SubFormat?
                    subFormat = format.GetFormat( DEFAULT_FORMAT );
                    if ( subFormat == null || subFormat.IsEmpty )
                    {
                        var msg = $"The format data does match a valid format, Format [{formatIndicator}]";
                        logger.Error( msg );
                        throw new ConverterException( Error.BadDataError, msg );
                    }
                    logger.Debug($"A default format will be used for the FormatIndicator \"{formatIndicator}\"");
                }
            }

            return subFormat;
        }

        public ConverterArgs ConvertResponse( byte[] respByteArray, bool pnsFlag )
        {
            if ( pnsFlag )
            {
                return ConvertPNSResponse( respByteArray );
            }

            return ConvertSalemResponse( Utils.ByteArrayToString( respByteArray ) );
        }

        public ConverterArgs ConvertResponse( string resp, bool pnsFlag )
        {
            if ( pnsFlag )
            {
                var msg = "This method is not valid for PNS";
                logger.Error( msg );
                throw new ConverterException( Error.InvalidModule, msg );
            }

            return ConvertSalemResponse( resp );
        }

        public ConverterArgs ConvertRequest( IRequestImpl request )
        {
            var req = request;
            req.SetSkipDuplicateCheck( true );

            try
            {
                req.SetDefaultValues();
            }
            catch ( RequestException )
            {
            }

            var hostProcessingSystem = request.Config.GetField( "HostProcessingSystem" );
            if ( hostProcessingSystem == null || hostProcessingSystem.Equals( "SLM" ) )
            {
                LoadTemplates( false );
                var args = ConvertRequest( req, false, requestTemplate );
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

        /**
         * Converts a PNS-spec response to an XML document.
         *
         * The response byte array is converted into a tree of DataElement objects
         * that define the structure of the XML. Once it is built, the root
         * DataElements are passed to the ResponseData object to be turned into XML.
         *
         * In all cases, a PNS response begins with a MessageHeader format, followed
         * by a bitmap (with a possible secondary bitmap), followed by the data
         * string for the various bitmaps. The converter builds the bitmap, and then
         * converts just the bits that were specified.
         *
         * DataElements are quick to process and easier to work with than XmlNodes,
         * so the Converter works with them. The ResponseData class manages building
         * the XmlDocument from the list of DataElements supplied to it.
         *
         * @param respByteArray
         * @return
         * @
         */
        private ConverterArgs ConvertPNSResponse( byte[] respByteArray )
        {
            try
            {
                LoadTemplates( true );
                ValidatePNSResponseCharSet( respByteArray );
                responseArgs = new ConverterArgs();
                responseArgs.Response = Utils.ByteArrayToISOString( respByteArray );
                responseArgs.MaskedResponse = responseArgs.Response;
                var bitmapPosition = responseTemplate.BitmapPosition;
                var bitmapLength = responseTemplate.BitmapLength;
                var bitmapSize = responseTemplate.BitmapSize;
                PNSBitSet bitmap = null;

                response = factory.MakeResponse();
                //response.setIsPNS( true );
                bitmap = new PNSBitSet( respByteArray, bitmapPosition, bitmapLength );
                if ( bitmap.Get( 1 ) )
                {
                    bitmap.AddSecondaryBitmap( respByteArray, bitmapPosition + 8, bitmapLength );
                    bitmapLength += 8;
                    bitmapSize *= 2;
                }

                var elements = new List<DataElement>();

                responseData = Utils.ByteArrayToString( respByteArray );
                responseData = responseData.Remove( bitmapPosition, bitmapLength );

                // Convert the Message Header
                var format = responseTemplate.GetFormat( MESSAGE_HEADER );
                if ( format.IsEmpty )
                {
                    var msg = "No message header could be found in the template file. The template may be out of sync.";
                    logger.Error( msg );
                    throw new ConverterException( Error.InvalidHeaderInfo, msg );
                }

                var header = new DataElement( MESSAGE_HEADER, null, response );
                ConvertResponseFormat( format, header );
                elements.Add( header );

                // Convert Each Bit
                for ( var bit = 2; bit <= bitmapSize; bit++ )
                {
                    if ( response.IsConversionError )
                    {
                        break;
                    }

                    if ( bitmap.Get( bit ) )
                    {
                        ConvertPNSResponseFormat( bit, elements );
                    }
                }

                responseArgs.Response = Utils.ByteArrayToString( respByteArray );
                var resp = factory.MakeResponse();
                resp.IsConversionError = response.IsConversionError;
                resp.LeftoverData = response.LeftoverData;
                resp.ErrorDescription = response.ErrorDescription;
                resp.NumExtraFields = response.NumExtraFields;
                resp.DataElements = elements;
                resp.RawData = respByteArray;
                responseArgs.ResponseData = resp;

                return responseArgs;
            }
            catch ( Exception ex )
            {
                var msg = "Exception caught while creating the response";
                logger.Error( msg, ex );
                throw new ConverterException( Error.ResponseFailure, msg, ex );
            }

        }

        private void ConvertPNSResponseFormat( int bit, List<DataElement> elements )
        {
            try
            {
                var name = $"Bit{bit}";
                var format = responseTemplate.GetFormat( name );
                if ( format.IsEmpty )
                {
                    var msg = $"Failed to find the format for Bit {bit}. The template may be out of sync.";
                    logger.Error( msg );
                    throw new ConverterException( Error.BadDataError, msg );
                }

                var element = new DataElement( name, null, response );
                ConvertResponseFormat( format, element );
                elements.Add( element );
            }
            catch ( ConverterException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                var msg = $"Exception caught while converting Bit {bit}.";
                logger.Error( msg, ex );
                throw new ConverterException( Error.BadDataError, msg, ex );
            }
        }

        protected void LoadTemplates( bool isPNS )
        {
            try
            {
                var type = isPNS ? RequestType.PNSOnline : RequestType.Online;
                if ( requestTemplate == null )
                {
                    requestTemplate = factory.GetRequestTemplate( type );
                    responseTemplate = factory.GetResponseTemplate( type );
                }
            }
            catch ( Exception ex )
            {
                var msg = "Exception occurred while loading templates.";
                logger.Error( msg, ex );
                throw new ConverterException( Error.InitializationFailure, msg, ex );
            }
        }

        protected bool IsEmpty( string value )
        {
            return value == null || value.Trim().Length == 0;
        }

        /**
         * Test to make sure that the Length attribute specified by the Format
         * matches the total of the Lengths of each Field.
         *
         * @param format
         */
        private void ValidateFormat( Format format )
        {
            if ( format.FormatType == FormatType.Bitmap || format.FormatType == FormatType.Variable || format.FormatType == FormatType.VariableArray )
            {
                return;
            }

            var totalLength = 0;

            foreach ( var field in format.Fields )
            {
                totalLength += field.Lengths[ field.Lengths.Count - 1 ];
            }

            if ( (format.Parent == null || format.Parent.FormatType != FormatType.VariableArray) && format.Length != -1 && format.Length != totalLength )
            {
                var msg =
                    $"The Format [{format.Name}] shows inconsistent lengths. The format Length is [{format.Length}] while the combined field lengths are [{totalLength}]";
                logger.Error( msg );
                throw new ConverterException( Error.LengthError, msg );
            }
        }

        private List<byte> ProcessChildFormats( IRequestImpl request, string baseName, Format format, PNSBitSet bitmap, List<byte> maskedConvertedFormats, int arrayIndex, bool skipPrefixFormats )
        {
            var data = new List<byte>();
            var maskedData = new List<byte>();

            if ( ProcessVariableArrayChildFormats( data, format, request, maskedData, maskedConvertedFormats, bitmap, skipPrefixFormats ) != null )
            {
                return data;
            }

            for ( var i = 0; i < format.Formats.Count; i++ )
            {
                var child = format.Formats.Get( i );

                if ( skipPrefixFormats && child.IsPrefixFormat )
                {
                    continue;
                }

                var formatName = child.FormatID;
                if ( arrayIndex != -1 )
                {
                    formatName = string.Format( ARRAY_FORMAT_ID, baseName, arrayIndex, child.Name );
                }

                // The format list includes all formats,
                // even ones that the request isn't referencing.
                var builders = new List<byte>[] { data, maskedData };
                if ( request.UsesFormat( formatName ) && !ProcessIndividualChildFormat( child, formatName, arrayIndex, baseName, request, builders, bitmap, skipPrefixFormats ) )
                {
                    break;
                }
            }

            maskedConvertedFormats.AddRange( maskedData );
            return data;
        }

        private bool ProcessIndividualChildFormat( Format child, string formatName, int arrayIndex, string baseName, IRequestImpl request, List<byte>[] builders, PNSBitSet bitmap, bool skipPrefixFormats )
        {
            var format = child.Parent;

            if ( child.IsArray )
            {
                var max = child.IsArray ? child.Max : child.Parent.Max;
                for ( var arrayInd = 0; arrayInd < max; arrayInd++ )
                {
                    var indexer = $"[{arrayInd}]";
                    if ( !request.UsesFormat( child.FormatID + indexer ) )
                    {
                        break;
                    }
                    AddFormatTerminator( request, builders[ 0 ], builders[ 1 ] );
                    var args = ConvertRequestFormat( child, request, child.FormatID, bitmap, arrayInd, skipPrefixFormats );
                    builders[ 0 ].AddRange( args.ReqByteArray );
                    builders[ 1 ].AddRange( args.MaskedReqByteArray );
                }
            }
            else if ( child.Parent != null && child.Parent.IsArray )
            {
                if ( !request.UsesFormat( formatName ) )
                {
                    return false;
                }
                AddFormatTerminator( request, builders[ 0 ], builders[ 1 ] );
                var args = ConvertRequestFormat( child, request, child.FormatID, bitmap, arrayIndex, skipPrefixFormats );
                builders[ 0 ].AddRange( args.ReqByteArray );
                builders[ 1 ].AddRange( args.MaskedReqByteArray );
            }
            else
            {
                AddFormatTerminator( request, builders[ 0 ], builders[ 1 ] );
                var name = baseName + format.Name + ".";
                var args = ConvertRequestFormat( child, request, name, bitmap, -1, skipPrefixFormats );
                builders[ 0 ].AddRange( args.ReqByteArray );
                builders[ 1 ].AddRange( args.MaskedReqByteArray );
            }

            return true;
        }

        private List<byte> ProcessVariableArrayChildFormats( List<byte> data, Format format, IRequestImpl request, List<byte> maskedData, List<byte> maskedConvertedFormats, PNSBitSet bitmap, bool skipPrefixFormats )
        {
            if ( format.FormatType == FormatType.VariableArray )
            {
                var arrayFormat = format.ArrayFormat;
                if ( arrayFormat == null )
                {
                    var msg = "The format [" + format.FormatID + "] is VariableArray, but has no array format.";
                    logger.Error( msg );
                    throw new ConverterException( Error.FormatNotFoundError, msg );
                }

                for ( var i = 0; i < arrayFormat.Max; i++ )
                {
                    var indexer = $"[{i}]";
                    if ( !request.UsesFormat( arrayFormat.FormatID + indexer ) )
                    {
                        break;
                    }
                    var args = ConvertRequestFormat( arrayFormat, request, format.FormatID, bitmap, i, skipPrefixFormats );
                    data.AddRange( args.ReqByteArray );
                    maskedData.AddRange( args.MaskedReqByteArray );
                }

                maskedConvertedFormats.AddRange( maskedData );
                return data;
            }

            return null;
        }

        private void AddFormatTerminator( IRequestImpl request, List<byte> data, List<byte> maskedData )
        {
            if ( !isPNS && request.IsBatch )
            {
                data.AddRange( requestTemplate.FormatDelimiter.GetBytes() );
                maskedData.AddRange( requestTemplate.FormatDelimiter.GetBytes() );
            }
        }

        /**
         * Gets the prefix for the data record, if one exists.
         *
         * There are two types of prefix for a format: 1. A static string 2. The
         * total Length of the record (not counting the prefix itself)
         *
         * If Prefix Data is supplied, then the prefix is static, and that value
         * should be returned.
         *
         * If no Prefix Data is supplied, but the Prefix Length is valid, then the
         * prefix is the total Length.
         *
         * @param format
         *            The format whose prefix is to be determined.
         * @param Length
         *            The Length of the data record.
         * @return The valid prefix, or an empty string if no prefix.
         * @
         */
        private string GetPrefix( Format format, int Length )
        {
            if ( format.PrefixLength == -1 )
            {
                return "";
            }

            var prefix = "";
            if ( format.PrefixData != null )
            {
                prefix = format.PrefixData;
            }

            var lengthStr = Length.ToString();
            if ( format.PrefixLength < lengthStr.Length )
            {
                var allowedLength = "";
                allowedLength = Utils.PadLeft( allowedLength, '9', format.PrefixLength );
                var msg = "The data for the record [" + format.Name + "] is too long. It can only be [" + allowedLength + "] bytes long, but is [" + lengthStr + "].";
                logger.Error( msg );
                throw new ConverterException( Error.LengthError, msg );
            }

            prefix += Utils.PadLeft( lengthStr, '0', format.PrefixLength );
            return prefix;
        }

        private Field PrepMultiLengthField( Field field, string fieldValue, byte[] previousFieldValue )
        {
            if ( !field.MultiLength )
            {
                return field;
            }

            var prev = Utils.StringToInt( Utils.ByteArrayToString( previousFieldValue ), -1 ) - 1;
            if ( prev < 0 || prev >= field.PrefixValues.Count )
            {
                var max = field.PrefixValues.Count;
                var msg = "Invalid prefix value. For field [" + field.Name + "], the previous field must be set to a number from 1 to " + max.ToString();
                logger.Error( msg );
                throw new ConverterException( Error.RequiredFieldNotSet, msg );
            }

            var newField = new Field( field );
            newField.Length = newField.PrefixValues[ prev ];

            if ( fieldValue != null && fieldValue.Length > newField.Length )
            {
                var msg =
                    $"The value for field \"{field.FieldID}\" is too long. It should be no longer than {newField.Length}, but is {fieldValue.Length}";
                this.logger.Error( msg );
                throw new ConverterException( Error.LengthError, msg );
            }

            return newField;
        }

        /**
         * Ensure that field values that are supposed to be numeric are valid
         * numbers.
         *
         * @param value
         * @param convField
         * @
         */
        private void ValidateNumericField( string value, Field convField )
        {
            if ( convField.FieldType == DataType.Numeric && value != null && value.Trim().Length > 0 && !Utils.IsNumeric( value ) )
            {
                var msg = $"The value for Field={convField.Name} must be numeric, value=[{value}]";
                logger.Error( msg );
                throw new ConverterException( Error.FieldNotNumeric, msg );
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
                var buffer = new StringBuilder( value );
                var chr = field.MaskWith[ 0 ];
                for ( var i = 0; i < field.MaskLength && i < buffer.Length; i++ )
                {
                    buffer.Insert( i, chr );
                }
                return Utils.PadLeft( buffer.ToString(), chr, field.MaskLength );
            }

            var buff = new StringBuilder( value );
            var ch = field.MaskWith[ 0 ];
            field.PrefixLength = field.PrefixLength == -1 ? 0 : field.PrefixLength;
            var len = fieldLength > buff.Length ? buff.Length : fieldLength + field.PrefixLength;
            len = len > buff.Length ? buff.Length : len;
            if ( len == 0 )
            {
                len = 10;
                buff = new StringBuilder( "          " );
            }
            for ( var i = len - 1; i >= 0; i-- )
            {
                buff[ i ] = ch;
            }
            var test = Utils.PadLeft( buff.ToString(), ch, field.MaskLength );
            return test;
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
                var msg = "CRM must support US-ASCII or ISO-8859-1 char set";
                logger.Error( msg );
                throw new ConverterException( Error.InitializationFailure, msg );
            }
        }

        private int GetIntValueFromData( Format format, int dataLength )
        {
            return Utils.StringToInt( GetValueFromData( format, dataLength ), 0 );
        }

        /**
         * Converts the specified field into a DataElement. That element is then
         * added to the supplied parent element.
         *
         * @param field
         *            The field to be converted.
         * @param name
         *            The name of the field. It can be different from the field's
         *            name.
         * @param format
         *            The format that owns the field.
         * @param element
         *            The parent DataElement.
         * @return The field's DataElement. This is used for dealing with aliases.
         * @
         */
        private DataElement ConvertResponseField( Field field, string name, Format format, DataElement element )
        {
            logger.Debug( "Converting field \"" + name + "\"" );
            var fieldDelimiter = responseTemplate.GetFieldDelimiter( responseData, format, configData );
            var fieldElement = new DataElement( name, null, element );

            if ( field.Length == 0 && field.ArrayIndicator )
            {
                fieldElement.Value = "";
                AddAliases( field, fieldElement, element );
                return fieldElement;
            }

            var dataLength = field.Length;
            string prefix = null;
            if ( field.PrefixLength > 0 )
            {
                prefix = GetValueFromData( format, field.PrefixLength );
                dataLength = Utils.StringToInt( prefix, 0 );
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
                var respVal = GetValueFromData( format, dataLength );
                fieldElement.Value = respVal != null ? respVal.Trim() : respVal;
            }
            if ( fieldElement.Value != null )
            {
                fieldElement.MaskedValue = MaskValue( fieldElement.Value, field, field.Length );
                MaskResponseDataField( responseData, field, fieldDelimiter );
            }

            if ( format.DataLength > 0 )
            {
                format.DataLength = format.DataLength - dataLength;
            }

            SetDataLengthFromSuffix( field, format );
            if ( response.IsConversionError )
            {
                return null;
            }

            SetDataElementName( format, fieldElement, element );

            if ( !field.HidePrefixColumnName && field.PrefixColumnName != null )
            {
                var columnName = new DataElement( field.PrefixColumnName, prefix, element );
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

        private void SetDataElementName( Format format, DataElement fieldElement, DataElement element )
        {
            if ( format.Name.Equals( DEFAULT_FORMAT ) && fieldElement.Name.Equals( "RecordType" ) && fieldElement.Value != null )
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
        }

        private void SetDataLengthFromSuffix( Field field, Format format )
        {
            if ( field.SuffixLength > 0 )
            {
                format.DataLength = GetIntValueFromData( format, field.SuffixLength );
                if ( format.FormatType == FormatType.Static )
                {
                    var Length = format.Length - field.Length;
                    if ( format.DataLength != 0 && format.DataLength != Length )
                    {
                        response.IsConversionError = true;
                        response.LeftoverData = TrimRecordDelimiter( format, responseData );
                        response.ErrorDescription =
                            $"The format [{format.Name}] must have a data Length of {Length} but is {format.DataLength}";
                        logger.Error( string.Format( FORMAT_CONVERT_FAILED, format.Name, response.ErrorDescription ) );
                    }
                }
            }
        }

        /**
         * This is used to get the value from a record for a floating field -- one
         * that does not have its position in list of calculated field positions.
         *
         * This is usually for optional fields that encapsulate the values of more
         * than one regular field.
         *
         * @param field
         * @return
         * @
         */
        private string GetValueByPosition( Field field )
        {
            var pos = field.Position - 1;
            return record.Substring( pos, field.Length );
        }

        private void AddAliases( Field field, DataElement fieldElement, DataElement element )
        {
            if ( field.Alias != null )
            {
                var num = field.Aliases.Count;
                fieldElement.NumAliases = num;
                foreach ( var alias in field.Aliases )
                {
                    if ( !alias.Trim().Equals( fieldElement.Name ) )
                    {
                        var aliasElement = new DataElement( alias.Trim(), null, element );
                        aliasElement.NumAliases = num;
                        aliasElement.Value = fieldElement.Value;
                        aliasElement.MaskedValue = MaskValue( fieldElement.Value, field, field.Length );
                        element.Elements.Add( aliasElement );
                    }
                }
            }
        }

        /**
         * Gets the specified number of bytes from the response data.
         *
         *
         * Every value retrieved from the response data must be Removed from the
         * response. This is true for field values, prefixes, suffixes, and so on.
         * In this way, every time a value is retrieved from the response data, it
         * is taken from the beginning of the response data string. This method
         * enforces that behavior by removing the returned string from the response
         * data.
         *
         * @param dataLength
         *            The number of bytes to get.
         * @return The requested string value.
         */
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

            string data = null;
            if ( responseData.Length > dataLength )
            {
                data = responseData.Substring( 0, dataLength );
            }
            else
            {
                data = responseData;
            }
            responseData = responseData.Substring( dataLength );

            return data != null && data.Trim().Length > 0 ? data : null;
        }

        private void MaskResponseDataField( string responseData, Field field, string fieldDelimiter )
        {
            if ( fieldDelimiter == null )
            {
                MaskOnlineResponseDataField( responseData, field );
                return;
            }

            var fields = responseData.Split( fieldDelimiter[ 0 ] );
            if ( fields.Length < field.Index )
            {
                return;
            }
            var value = Utils.PadLeft( "", '#', fields[field.Index - 1].Length );
            var resp = new StringBuilder();
            for ( var i = 0; i < fields.Length; i++ )
            {
                if ( i == field.Index - 1 )
                {
                    resp.Append( value );
                }
                else
                {
                    resp.Append( fields[i] );
                }

                if ( i < fields.Length - 1 )
                {
                    resp.Append( fieldDelimiter );
                }
            }
            responseArgs.MaskedResponse = resp.ToString();
        }

        /**
         * Masks the field in the raw record.
         *
         * The responseData parameter Contains the data of the record just after the
         * field being masked. So, the position to start masking is the field's
         * Length before the position of responseData inside MaskedResponse.
         *
         * @param responseData
         * @param field
         * @
         */
        private void MaskOnlineResponseDataField( string responseData, Field field )
        {
            if ( field.MaskWith == null || responseArgs == null || responseArgs.MaskedResponse == null )
            {
                if ( responseArgs != null && responseArgs.MaskedResponse == null )
                {
                    responseArgs.MaskedResponse = responseArgs.Response;
                }
                return;
            }

            var respData = responseArgs.MaskedResponse;
            var value = Utils.PadLeft( "", '#', field.Length );
            var maskedData = new StringBuilder();
            var pos = respData.IndexOf( responseData ) - field.Length + 1;
            if ( pos < 0 )
            {
                return;
            }
            maskedData.Append( respData.Substring( 0, pos - 1 ) );
            maskedData.Append( value );
            maskedData.Append( respData.Substring( pos + field.Length - 1 ) );
            responseArgs.MaskedResponse = maskedData.ToString();
        }

        protected string GetMOPFromOrderRecord( string data )
        {
            var order = responseTemplate.GetOrder();

            var format = responseTemplate.GetFormat( order.FormatName );
            if ( format.IsEmpty )
            {
                return DEFAULT_FORMAT;
            }

            var field = format.GetField( order.MopFieldName );
            if ( field.IsEmpty )
            {
                return DEFAULT_FORMAT;
            }

            // Find the format that Contains the MOP field.
            var formatLen = responseTemplate.DefaultFormatLength + 1;
            string formatData = null;
            for ( var position = 0; position + formatLen <= data.Length; position += formatLen )
            {
                var substr = data.Substring( position );
                if ( substr.StartsWith( order.FormatName ) )
                {
                    formatData = data.Substring( position );
                    break;
                }
            }

            if ( formatData == null )
            {
                return DEFAULT_FORMAT;
            }

            return formatData.Substring( field.Position - 1, field.Length );
        }
    }
}