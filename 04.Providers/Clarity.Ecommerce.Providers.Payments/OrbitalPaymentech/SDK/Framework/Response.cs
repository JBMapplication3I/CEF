#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Converter;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Represents a response from the server for an online request,
    /// or the processing results of an order from a batch response
    /// file.
    /// </summary>
    /// <remarks>
    /// Access each field for the request type you specified by using
    /// the class' indexer. The available fields are based on the
    /// the data returned from the server. Refer to the Developer's Guide
    /// for more information on what fields to expect here.
    ///
    /// Example:
    ///    <code>
    ///    IResponse response = dispatcher.ReceiveResponse(request);
    ///    string amount = response["Amount"];
    ///    </code>
    /// </remarks>
    [Guid( "3864F487-2660-4db0-B53E-55F571C27F3B" )]
    [ClassInterface( ClassInterfaceType.None )]
    [ComVisible( true )]
    public class Response : FrameworkBase, IFullResponse
    {
        private List<DataElement> elements = new List<DataElement>();
        private KeySafeDictionary<string, DataElement> elementRefs = new KeySafeDictionary<string, DataElement>();
        private KeySafeDictionary<string, int> arrayCounts = new KeySafeDictionary<string, int>();
        private bool isPNS;

        /// <summary>
        ///
        /// </summary>
        public Response() : this( new DispatcherFactory() )
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        public Response( IDispatcherFactory factory ) : this( null, false, factory )
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="factory"></param>
        public Response( List<DataElement> elements, bool isPNS, IDispatcherFactory factory )
        {
            this.factory = factory;
            this.isPNS = isPNS;
            DataElements = elements;
            MIMEHeaders = new WebHeaderCollection();
        }

        /// <summary>
        /// This converts an XML string (in the same format that the XML
        /// property produces) into the DataElement tree.
        /// </summary>
        /// <remarks>
        /// This is not for general use. Currently, it is only used by
        /// the unit test framework. The Converter does not pass XML
        /// to the Response class.
        /// </remarks>
        /// <param name="xml"></param>
        /// <param name="factory"></param>
        public Response( string xml, IDispatcherFactory factory )
        {
            this.factory = factory;
            MIMEHeaders = new WebHeaderCollection();

            var doc = new XmlDocument();
            doc.LoadXml( xml );

            ResponseType = doc.DocumentElement.FirstChild.Name;

            foreach ( XmlNode child in doc.DocumentElement.ChildNodes )
            {
                ParseXMLElement( child, null );
            }
            BuildElementRefs( this.elements, "" );
        }

        private void ParseXMLElement( XmlNode node, DataElement parent )
        {
            if ( node.NodeType != XmlNodeType.Element )
            {
                return;
            }

            var name = node.Name;
            var index = Utils.GetAttributeValue( "index", null, node );
            if ( index != null )
            {
                name = $"{name}[{index}]";
            }
            var value = Utils.GetNodeValue( node );
            DataElement element = null;
            if ( parent == null )
            {
                element = new DataElement( name, value, this );
                AddAndSortElement( elements, element );
            }
            else
            {
                element = new DataElement( name, value, parent );
                AddAndSortElement( parent.Elements, element );
            }

            foreach ( XmlNode child in node.ChildNodes )
            {
                ParseXMLElement( child, element );
            }
        }

        /// <summary>
        ///
        /// </summary>
        public List<DataElement> DataElements
        {
            get => this.elements;
            set
            {
                if ( value == null )
                {
                    return;
                }

                this.elements.Clear();
                if ( !isPNS )
                {
                    for ( var i = 0; i < value.Count; i++ )
                    {
                        if ( value[ i ].HideFromFieldID )
                        {
                            foreach ( var elem in value[ i ].Elements )
                            {
                                AddAndSortElement( elements, elem );
                            }
                        }
                        else
                        {
                            AddAndSortElement( elements, value[ i ] );
                        }
                    }
                }
                else
                {
                    foreach ( var elem in value )
                    {
                        AddAndSortElement( elements, elem );
                    }
                }
                var path = "";
                BuildElementRefs( this.elements, path );
            }
        }

        private void AddAndSortElement( List<DataElement> elements, DataElement value )
        {
            if ( value.Name == null )
            {
                return;
            }

            for ( var i = 0; i < elements.Count; i++ )
            {
                if ( value.Name.ToLower().CompareTo( elements[ i ].Name.ToLower() ) < 0 )
                {
                    elements.Insert( i, value );
                    return;
                }
            }

            elements.Add( value );
        }

        private void BuildElementRefs( List<DataElement> list, string path )
        {
            var basePath = path;
            foreach ( var element in list )
            {
                path = FieldIdentifier.Combine( basePath, element.Name );
                //try
                //{
                //    path = FieldIdentifier.Normalize( path );
                //}
                //catch ( FieldIdentifierException ex )
                //{
                //    throw new ResponseException( ex.ErrorCode, ex.Message, ex );
                //}

                if ( element.Elements.Count == 0 )
                {
                    if ( path.Contains( "[" ) )
                    {
                        UpdateArrayCount( path );
                    }

                    elementRefs[ path ] = element;
                }
                else
                {
                    var newPath = path;
                    BuildElementRefs( element.Elements, newPath );
                }
            }


        }

        private string GeneralizeArrayPath( string path )
        {
            var start = path.IndexOf( '[' );
            if ( start == -1 )
            {
                return path;
            }
            var end = path.IndexOf( ']' );
            return path.Substring( 0, start ) + path.Substring( end + 1 );
        }

        private void UpdateArrayCount( string path )
        {
            path = GeneralizeArrayPath( path );
            if ( arrayCounts.ContainsKey( path ) )
            {
                var count = arrayCounts[ path ];
                count++;
                arrayCounts[ path ] = count;
            }
            else
            {
                arrayCounts.Add( path, 1 );
            }
        }

        private DataElement GetDataElement( string fieldName )
        {
            return GetDataElement( fieldName, true );
        }

        private DataElement GetDataElement( string field, bool throwException )
        {
            if ( field == null )
            {
                return null;
            }

            try
            {
                field = FieldIdentifier.Normalize( field );
            }
            catch ( FieldIdentifierException ex )
            {
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }

            foreach ( var element in elements )
            {
                var fieldName = field;

                if ( !fieldName.Contains( FieldIdentifier.Separator ) )
                {
                    var retVal = FindFieldIdentifier( fieldName );
                    if ( retVal != null )
                    {
                        return retVal;
                    }
                }

                var elem = element.FindField( fieldName );
                if ( elem != null )
                {
                    return elem;
                }

                if ( !fieldName.StartsWith( "BitMap" ) && fieldName.StartsWith( "Bit" ) )
                {
                    fieldName = "BitMap." + fieldName;
                    elem = element.FindField( fieldName );
                    if ( elem != null )
                    {
                        return elem;
                    }
                }

                if ( fieldName.Contains( "[0]" ) )
                {
                    elem = element.FindField( fieldName.Replace( "[0]", "" ) );
                    if ( elem != null )
                    {
                        return elem;
                    }
                }
                else
                {
                    var pos = fieldName.LastIndexOf( '.' );
                    if ( pos > -1 )
                    {
                        var tempName = fieldName.Substring( 0, pos + 1 ) + "[0]" + fieldName.Substring( pos );
                        elem = element.FindField( tempName );
                        if ( elem != null )
                        {
                            if ( !throwException )
                            {
                                return null;
                            }
                            var msg =
                                $"There is more than one field by the name \"{fieldName}\". You must use an element path to get this value.";
                            Logger.Error( msg );
                            throw new ResponseException( Error.InvalidField, msg );
                        }
                    }
                }
            }

            if ( !throwException )
            {
                return null;
            }
            var message = $"Field [{field}] not found. If there are more than one by this name, use GetFieldArray()";
            Logger.Error( message );
            throw new ResponseException( Error.FieldDoesNotExist, message );
        }

        private DataElement FindFieldIdentifier( string fieldName )
        {
            var toFind = fieldName;
            var numberOfTimesFound = 0;
            DataElement retVal = null;
            foreach ( var path in elementRefs.Keys )
            {
                // If we find an exact match, use it!
                if ( path.Equals( toFind ) )
                {
                    return elementRefs[ path ];
                }

                if ( path.Equals( toFind ) || path.EndsWith( FieldIdentifier.Separator + toFind ) )
                {
                    retVal = elementRefs[ path ];
                    if ( retVal.NumAliases == 1 && retVal.Parent.NumAliases > 1 )
                    {
                        retVal.NumAliases = retVal.Parent.NumAliases;
                    }

                    if ( !retVal.IsAlias )
                    {
                        numberOfTimesFound++;
                    }
                }
            }

            if ( retVal != null && numberOfTimesFound > retVal.NumAliases )
            {
                var message = "More than one field exists by the name \"" + fieldName + "\". Use an element path instead.";
                Logger.Error( message );
                throw new ResponseException( Error.InvalidField, message );
            }

            return retVal;
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsOrder { get; set; }

        #region IResponse Members

        /// <summary>
        /// This indexer gives a convenient interface into the GetField and
        /// SetField methods.
        /// </summary>
        /// <remarks>
        /// This indexer only returns string values, so you will still need
        /// GetIntField and GetLongField (and their appropriate Set methods).
        /// </remarks>
        public string this[ string fieldName ] => GetField( fieldName );

        /// <summary>
        /// It returns a string version of the value of the specified field.
        /// This is identical to using the indexer.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value you want
        /// to retrieve.</param>
        /// <returns>The value of the field as a string.</returns>
        public string GetField( string fieldName )
        {
            ValidateFieldName( fieldName );
            var val = GetDataElement( fieldName ).Value;
            if ( val != null )
            {
                return val.Trim();
            }
            return val;
        }

        /// <summary>
        /// Returns true if the field exists in the response, false if it does not.
        /// </summary>
        /// <param name="fieldName">The name of the field to test.</param>
        /// <returns>True if the field exists in the response, false if it does not.</returns>
        public bool HasField(string fieldName )
        {
            try
            {
                return GetDataElement( fieldName, false ) != null;
            }
            catch ( ResponseException )
            {
                return false;
            }
        }

        private void ValidateFieldName( string fieldName )
        {
            if ( IsEmpty( fieldName ) )
            {
                Logger.Error( "The field name is null." );
                throw new ResponseException( Error.NullFieldName, "The field name is null." );
            }
        }

        /// <summary>
        /// It returns a string version of the masked value of the specified field.
        /// This is identical to using the indexer.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value you want
        /// to retrieve.</param>
        /// <returns>The value of the field as a string.</returns>
        public string GetMaskedField( string fieldName )
        {
            var element = GetDataElement( fieldName );
            return element != null ? element.MaskedValue : null;
        }

        /// <summary>
        /// Get the value for a specific element in an array of like fields.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value you want
        /// to retrieve.</param>
        /// <param name="index">The array index that points to the value you want.</param>
        /// <returns>The value of the field specified by both the fieldName and index.</returns>
        public string GetField( string fieldName, int index )
        {
            ValidateFieldName( fieldName );
            var path = new FieldIdentifier( fieldName );
            fieldName = $"{path.FormatPath}[{index}].{path.Name}";
            return GetField( fieldName );
        }

        /// <summary>
        /// Returns an integer version of the value of the specified field.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value you want
        /// to retrieve.</param>
        /// <returns>The value of the field as an integer.</returns>
        public int GetIntField( string fieldName )
        {
            var val = GetField( fieldName );
            if ( this.IsEmpty( val ) )
            {
                return 0;
            }

            try
            {
                return Convert.ToInt32( val );
            }
            catch
            {
                var msg = "The field [" + fieldName +
                    "] does not have a numeric value, value=[" + val + "].";
                Logger.Error( msg );
                throw new ResponseException( Error.FieldNotNumeric, msg );
            }
        }

        /// <summary>
        /// Get the number of identically named fields.
        /// Used in conjunction with GetField( string, int ).
        /// </summary>
        /// <param name="fieldName">The name of the field whose item count to return.</param>
        /// <returns>The number of fields in the array.</returns>
        public int GetCount( string fieldName )
        {
            string fieldID = null;
            try
            {
                fieldID = FieldIdentifier.Normalize( fieldName );
            }
            catch ( FieldIdentifierException ex )
            {
                Logger.Debug( "GetCount call failed with field \"" + fieldName + "\".", ex );
                return 0;
            }

            fieldID = GeneralizeArrayPath( fieldID );
            var count = arrayCounts.ContainsKey( fieldID ) ? arrayCounts[ fieldID ] : 0;
            if ( count > 0 )
            {
                return count;
            }

            if ( fieldID.StartsWith( "Bit" ) )
            {
                count = arrayCounts.ContainsKey( "BitMap." + fieldID ) ? arrayCounts[ "BitMap." + fieldID ] : 0;
                if ( count > 0 )
                {
                    return count;
                }
            }

            foreach ( var key in arrayCounts.Keys )
            {
                var path = new FieldIdentifier( key );
                var format = path.FormatPath;
                format = format.Replace( "[*]", "" );
                if ( fieldID.Equals( format ) )
                {
                    return arrayCounts[ key ];
                }
            }

            return CountFormats( fieldName, DataElements );
        }

        /// <summary>
        /// Re-entrant method that counts the number of DataElements
        /// with the specified element path. GetCount uses this
        /// to count formats instead of fields, if no fields exist
        /// with that name.
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        private int CountFormats( string fieldID, List<DataElement> elements )
        {
            var count = 0;
            foreach ( var element in elements )
            {
                if ( element.FieldID == fieldID )
                {
                    count++;
                }
                else if ( count == 0 && fieldID.StartsWith( element.FieldID ) )
                {
                    var cnt = CountFormats( fieldID, element.Elements );
                    if ( cnt > 0 )
                    {
                        return cnt;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Returns an long integer version of the value of the specified field.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value you want
        /// to retrieve.</param>
        /// <returns>The value of the field as a long.</returns>
        public long GetLongField( string fieldName )
        {
            var val = GetField( fieldName );
            if ( IsEmpty( val ) )
            {
                return 0;
            }

            try
            {
                return Convert.ToInt64( val );
            }
            catch
            {
                var msg = "The field [" + fieldName +
                    "] does not have a numeric value, value=[" + val + "].";
                Logger.Error( msg );
                throw new ResponseException( Error.FieldNotNumeric, msg );
            }
        }

        /// <summary>
        /// Some responses have an array of fields. This returns an array of values
        ///	for the specified field.
        ///
        /// It will throw an exception if there is no array for this field.
        /// </summary>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public string[] GetFieldArray( string fieldID )
        {
            var count = GetCount( fieldID );
            var values = new List<string>();

            for ( var i = 0; i < count; i++ )
            {
                var path = new FieldIdentifier( fieldID );
                var format = path.FormatPath;
                var field = path.Name;
                var name = $"{format}[{i}].{field}";
                values.Add( GetField( name ) );
            }

            return values.ToArray();
        }

        public WebHeaderCollection MIMEHeaders { get; set; }

        public string DumpFieldValues()
        {
            return DumpFieldValues( false );
        }

        public string DumpFieldIdentifiers()
        {
            var fieldIDs = GetFieldIdentifiers( elements );
            var resp = new StringBuilder();
            foreach ( var field in fieldIDs )
            {
                resp.AppendLine( field );
            }

            return resp.ToString();
        }

        public string DumpMaskedFieldValues()
        {
            return DumpFieldValues( true );
        }

        private string DumpFieldValues( bool isMasked )
        {
            var buff = new StringBuilder();

            var fieldIDs = GetFieldIdentifiers( elements );
            foreach ( var id in fieldIDs )
            {
                try
                {
                    var value = isMasked ? GetMaskedField( id ) : GetField( id );
                    if ( value == null )
                    {
                        buff.AppendLine( id + " = " );
                    }
                    else
                    {
                        buff.AppendLine( id + " = " + value );
                    }
                }
                catch
                {
                }
            }

            buff.Append( "Properties.NumExtraFields = " );
            buff.Append( NumExtraFields );
            buff.AppendLine( "" );
            buff.Append( "Properties.IsConvError = " );
            buff.Append( IsConversionError );
            buff.AppendLine( "" );
            buff.Append( "Properties.LeftoverData = " );
            buff.Append( LeftoverData != null ? LeftoverData : "" );
            buff.AppendLine( "" );
            buff.Append( "Properties.ErrorDescription = " );
            buff.Append( ErrorDescription != null ? ErrorDescription : "" );
            buff.AppendLine( "" );

            return buff.ToString();
        }

        private List<string> GetFieldIdentifiers( List<DataElement> elements )
        {
            var list = new List<string>();

            foreach ( var element in elements )
            {
                if ( element.Elements != null && element.Elements.Count > 0 )
                {
                    list.AddRange( GetFieldIdentifiers( element.Elements ) );
                }
                else
                {
                    if ( element.Parent != null && element.Parent.HideFromFieldID )
                    {
                        list.Add( element.Name );
                    }
                    else
                    {
                        list.Add( element.FieldID );
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// Returns the entire XML of the response. This may be useful in testing
        /// and debugging.
        /// </summary>
        public string XML => GetXML( false );

        /// <summary>
        /// Returns the entire masked XML of the response. This may be useful in testing
        /// and debugging.
        /// </summary>
        public string MaskedXML => GetXML( true );

        /// <summary>
        /// Returns the actual string record returned by the server.
        /// </summary>
        /// <remarks>
        /// The record
        /// will contain many fields in a format defined by a Chase
        /// specification. It is recommended that you only use this property
        /// when you truly need to.
        /// </remarks>
        public byte[] RawData { get; set; }

        /// <summary>
        /// Returns true if an error occurred when processing the data returned
        /// from the server.
        /// </summary>
        /// <remarks>
        /// This typically only happens when, for some reason, the server returns
        /// corrupt data.
        ///
        /// When this happens, this property will be set to true and the LeftoverData
        /// property will contain all of the data that MSDK failed to process.
        /// </remarks>
        public bool IsConversionError { get; set; }

        /// <summary>
        /// Returns a readable description of the cause of the conversion error.
        /// </summary>
        /// <remarks>
        /// When the IsConversionError is true, this property will be set to a
        /// string that describes the nature of the error.
        /// </remarks>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Returns all of the data that MSDK failed to process.
        /// </summary>
        /// <remarks>
        /// This works in conjunction with <see cref="IsConversionError"/> and will
        /// always be set to null when IsConversionError is false.
        /// </remarks>
        public string LeftoverData { get; set; }

        /// <summary>
        /// Returns the name of the host that the request was sent to.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Returns the name of the port that the request was sent to.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Returns a string that represents the version of the MSDK SDK that
        /// was used to send the request that this response belongs to.
        /// </summary>
        public string SDKVersion { get; set; }

        /// <summary>
        /// Returns a name that represents the type of response you are getting.
        /// For instance, an Orbital request can return a NewOrderResp or a QuickResp.
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// Returns true if the delimited file record has more fields than expected.
        /// </summary>
        /// <remarks>
        /// This typically happens when new fields are added to the report, but the
        /// SDK has not been upgraded to support the new field.
        ///
        /// These new fields will appear as fields with the names ExtraFieldX, where
        /// X is the index for the new field (ExtraField1, ExtraField2, etc.).
        /// </remarks>
        public bool HasExtraFields => NumExtraFields > 0;

        /// <summary>
        /// Returns the number of extra fields found in the record.
        /// </summary>
        /// <remarks>
        /// This is greater than zero when HasExtraFields is true.
        ///
        /// This can be used to iterate through the ExtraField list.
        /// </remarks>
        public int NumExtraFields { get; set; }

        /// <summary>
        /// Gets an array of element paths for all fields available in the
        /// Response.
        /// </summary>
        public string[] ResponseFieldIDs
        {
            get
            {
                var ids = GetFieldIdentifiers( elements );
                return ids.ToArray();
            }
        }

        #endregion


        private string GetXML( bool getMaskedData )
        {
            try
            {
                var doc = new XmlDocument();

                var xml = doc.AppendChild( doc.CreateElement( "Response" ) );

                CreateSubNodes( doc, elements, xml, getMaskedData );

                var props = xml.AppendChild( doc.CreateElement( "Properties" ) );

                var XmlNode = props.AppendChild( doc.CreateElement( "NumExtraFields" ) );
                XmlNode.AppendChild( doc.CreateTextNode( NumExtraFields.ToString() ) );

                XmlNode = props.AppendChild( doc.CreateElement( "IsConvError" ) );
                var convError = IsConversionError.ToString();
                XmlNode.AppendChild( doc.CreateTextNode( convError ) );

                XmlNode = props.AppendChild( doc.CreateElement( "LeftoverData" ) );
                if ( LeftoverData != null )
                {
                    XmlNode.AppendChild( doc.CreateTextNode( LeftoverData.Trim() ) );
                }

                XmlNode = props.AppendChild( doc.CreateElement( "ErrorDescription" ) );
                if ( ErrorDescription != null )
                {
                    XmlNode.AppendChild( doc.CreateTextNode( ErrorDescription ) );
                }

                return Utils.ConvertXMLToString( doc );
            }
            catch
            {
                return null;
            }
        }

        private void CreateSubNodes( XmlDocument doc, List<DataElement> elements, XmlNode parent, bool getMaskedData )
        {
            foreach ( var element in elements )
            {
                XmlNode XmlNode = doc.CreateElement( MakeValidXML( element.Name ) );
                if ( element.Value != null )
                {
                    var value = getMaskedData ? element.MaskedValue : element.Value;
                    XmlNode.AppendChild( doc.CreateTextNode( value ) );
                }
                parent.AppendChild( XmlNode );
                CreateSubNodes( doc, element.Elements, XmlNode, getMaskedData );
            }
        }

        private string MakeValidXML(string value )
        {
            var pos = value.IndexOf( '[' );
            if ( pos != -1 )
            {
                value = value.Substring( 0, pos );
            }

            return value;
        }
    }
}
