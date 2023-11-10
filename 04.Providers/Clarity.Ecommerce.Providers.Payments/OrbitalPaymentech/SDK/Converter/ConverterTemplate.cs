using System;
using System.Collections.Generic;
using System.Xml;
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
    public class ConverterTemplate
    {
        protected bool initialized = false;
        protected ILog engineLogger = null;
        protected IConfigurator configurator = null;
        protected IDispatcherFactory factory = null;
        protected List<string> bitmapFormats = new List<string>();
        public int DefaultFormatLength { get; protected set; }


        // Because LinkedHashMap is not maintaining the order, like it should,
        // formats is used for quick retrieval, while formatList is used
        // for iterating.
        protected SafeDictionary<string, Format> formats = new OrderedDictionary<string, Format>();
        protected List<Format> formatList = new List<Format>();

        // Data
        public int FormatIndicatorLength { get; protected set;  }
        public string MessageDelimiter { get; protected set; }
        public string FormatDelimiter { get; protected set; }
        protected SafeDictionary<string, string> messageFormats = new OrderedDictionary<string, string>();
        protected Order order = new Order();

        // for Batch
        public string FileTerminator { get; protected set; }

        // For allowing merchants to specify just the field name.
        protected SafeDictionary<string, SafeDictionary<string, List<string>>> uniqueFieldNames = new SafeDictionary<string, SafeDictionary<string, List<string>>>();

        public class Order
        {
            public string FormatName { get; set; }
            public string AmountFieldName { get; set; }
            public string ActionCodeFieldName { get; set; }
            public string MopFieldName { get; set; }
            public SafeDictionary<string, OrderLists> Lists { get; private set; }

            public Order()
            {
                Lists = new SafeDictionary<string, OrderLists>();
            }

            public OrderLists GetItem( string name )
            {
                return Lists[ name ];
            }
        }

        public class OrderLists
        {
            private List<string> actionCodeNotIn = new List<string>();
            private List<string> actionCodeIn = new List<string>();
            private List<string> defaults = new List<string>();

            public List<string> ActionCodeNotIn => actionCodeNotIn;

            public List<string> ActionCodeIn => actionCodeIn;

            public List<string> Defaults => defaults;
        }

        private class PositionData
        {
            public int position = 1;
            public int prevLength;
        }

        public ConverterTemplate()
        {
            DefaultFormatLength = 1000;
        }

        public string FindNodeValue( string nodeName, XmlDocument document )
        {
            return Utils.FindNodeValue( nodeName, null, document.DocumentElement, true );
        }

        public int GetFieldNameCount( string transType, string fieldName )
        {
            SafeDictionary<string, List<string>> fieldNames = null;
            if ( this.uniqueFieldNames.ContainsKey( transType ) )
            {
                fieldNames = this.uniqueFieldNames[ transType ];
            }
            else
            {
                return 0;
            }

            if ( fieldNames.ContainsKey( fieldName ) )
            {
                return fieldNames[ fieldName ].Count;
            }

            return 0;
        }

        public string GetFieldName( string transType, string fieldName, int index )
        {
            SafeDictionary<string, List<string>> fieldNames = null;
            if ( this.uniqueFieldNames.ContainsKey( transType ) )
            {
                fieldNames = this.uniqueFieldNames[ transType ];
            }
            else
            {
                return null;
            }

            if ( fieldNames.ContainsKey( fieldName ) && fieldNames[ fieldName ].Count > index )
            {
                return fieldNames[ fieldName ][ index ];
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        protected bool IsEmpty( Format format )
        {
            return format == null || format.IsEmpty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        protected bool IsEmpty( Field field )
        {
            return field == null || field.IsEmpty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="attributeName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public string FindNodeAttribute( string nodeName, string attributeName, XmlDocument document )
        {
            return FindNodeAttribute( nodeName, attributeName, null, document );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public string FindNodeAttribute( string nodeName, string attributeName, string defaultValue, XmlDocument document )
        {
            var nodes = document.GetElementsByTagName( nodeName );
            if ( nodes.Count == 0 )
            {
                return defaultValue;
            }
            return Utils.GetAttributeValue( attributeName, defaultValue, nodes.Item( 0 ) );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public int GetIntAttribute( string nodeName, string attributeName,
                int defaultValue, XmlDocument document )
        {
            return Utils.StringToInt(
                    FindNodeAttribute( nodeName, attributeName, document ),
                    defaultValue );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetIntNodeValue( XmlNode node, int defaultValue )
        {
            return Utils.StringToInt( Utils.GetNodeValue( node ), defaultValue );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetIntNodeAttribute( string name, int defaultValue, XmlNode node )
        {
            return Utils.StringToInt( Utils.GetAttributeValue( name, null, node ),
                    defaultValue );
        }

        /// <summary>
        /// Takes a list of nodes whose getType() attribute is set to "Format"
        /// and parses them into Format objects that is stored in a Dictionary.
        /// </summary>
        /// <param name="formatXmlNodes">The list of Format nodes to parse</param>
        /// <param name="name">The full dot-separated "path" of the name, such as
        /// "Request.PIDH"</param>
        /// <param name="parent">The parent format</param>
        protected void ParseFormats( XmlNodeList formatXmlNodes, string name, Format parent, string transType )
        {
            for ( var i = 0; i < formatXmlNodes.Count; i++ )
            {
                try
                {
                    var node = formatXmlNodes.Item( i );
                    if ( name == "Request" && node.Name == "BitMap" )
                    {
                        bitmapFormats.Add( name );
                    }
                    if ( node.NodeType != XmlNodeType.Element || node.Name != "Format" )
                    {
                        continue;
                    }

                    var format = new Format();
                    HandleFormatID( format, parent, node );

                    format.IsPrefixFormat = Utils.GetBoolAttributeValue( "prefix_format", false, node );
                    format.FormatIndicator = Utils.GetAttributeValue( "prefix", null, node );
                    format.FormatType = SetFormatType( Utils.GetAttributeValue( "format_type", null, node ) );
                    format.BitMapType = SetBitMapType( Utils.GetAttributeValue( "bitmap_type", null, node ) );
                    format.PrefixLength = GetIntNodeAttribute( "prefix_length", -1, node );
                    format.PrefixData = Utils.GetAttributeValue( "prefix_data", null, node );
                    format.HideFromFieldID = Utils.GetBoolAttributeValue( "hide", false, node );
                    format.IsRootFormat = node.ParentNode.Name != "Format";
                    format.IgnoreShort = Utils.GetBoolAttributeValue( "ignore_short", false, node );
                    format.Alias = Utils.GetAttributeValue( "alias", null, node );
                    format.Max = GetIntNodeAttribute( "max", 1, node );
                    format.CountField = Utils.GetAttributeValue( "count_field", null, node );
                    format.Bit = GetIntNodeAttribute( "bit", -1, node );
                    format.ResponseType = Utils.GetAttributeValue( "response_type", null, node );
                    format.DelimitedResponseType = Utils.GetAttributeValue( "delim_type", null, node );
                    format.SkipWrite = Utils.StringToBoolean( Utils.GetAttributeValue( "skip", null, node ) );
                    format.FormatName = format.Alias;
                    format.TCPFileTerminator = Utils.GetAttributeValue( "tcp_terminator", null, node );
                    format.SFTPFileTerminator = Utils.GetAttributeValue( "sftp_terminator", null, node );
                    format.IsArray = Utils.GetBoolAttributeValue( "is_array", false, node );
                    var deprecatedName = Utils.GetAttributeValue( "deprecated_name", null, node );
                    format.AddAlias( deprecatedName );

                    SetFormatValues( format, name );

                    var transactionType = transType == null && format.Parent == null ? format.Name : transType;

                    ParseFields( node.ChildNodes, format, transactionType );
                    if ( format.FormatType == FormatType.Bitmap )
                    {
                        bitmapFormats.Add( format.Path );
                    }
                    ParseFormats( node.ChildNodes, format.Path, format, transactionType );
                    foreach ( var childName in format.Formats.Keys )
                    {
                        var child = format.Formats[ childName ];
                        if ( child.Max > 1 )
                        {
                            format.ArrayFormat = child;
                        }
                    }

                    ParseFormatRefs( node.ChildNodes, format );
                    AddFormatToLists( format, parent );
                }
                catch ( Exception ex )
                {
                    throw new ConverterException( Error.InitializationFailure, "Failed to parse Formats", ex );
                }
            }
        }

        private void SetFormatValues( Format format, string name )
        {
            if ( !format.IsArray && format.Max > 1 )
            {
                format.IsArray = true;
            }

            if ( name == "Request" || name == "Response" )
            {
                format.Path = format.Name;
            }
            else
            {
                format.Path = FieldIdentifier.Combine( name, format.Name );
            }

            if ( format.FormatType == FormatType.Bitmap )
            {
                bitmapFormats.Add( format.Path );
            }
        }

        private void HandleFormatID( Format format, Format parent, XmlNode node )
        {
            format.Parent = parent;
            if ( parent != null )
            {
                format.FormatID = parent.FormatID;
            }

            format.Name = Utils.GetAttributeValue( "name", null, node );
            if ( parent != null )
            {
                format.AddToFormatID( format.Name );
            }
        }

        private void AddFormatToLists( Format format, Format parent )
        {
            if ( parent != null )
            {
                if ( !format.IsPrefixFormat )
                {
                    if ( parent.Formats.ContainsKey( format.Name ) )
                    {
                        var msg = "The format [" + format.Name + "] is already in the format map for format [" + parent.Name + "].";
                        engineLogger.Error( msg );
                        throw new ConverterException( Error.BadDataError, msg );
                    }
                    parent.Formats.Add( format.Name, format );
                }
                else
                {
                    parent.Formats.Add( format.Name, format );
                    parent.AddPrefixFormat( format );
                }
            }
            else
            {
                formatList.Add( format );
                formats.Add( format.Name, format );
            }
        }

        private void ParseFormatRefs( XmlNodeList nodes, Format format )
        {
            for ( var i = 0; i < nodes.Count; i++ )
            {
                var node = nodes.Item( i );
                if ( node.Name == "FormatRef" )
                {
                    format.AddFormatRef( Utils.GetAttributeValue( "Name", null, node ) );
                }
            }
        }

        private BitMapType SetBitMapType( string type )
        {
            if ( type == null || type.Trim().Length == 0 )
            {
                return BitMapType.Binary;
            }

            if ( type.ToLower().Trim() == "hex" )
            {
                return BitMapType.Hex;
            }
            return BitMapType.Binary;
        }

        private FormatType SetFormatType( string formatType )
        {
            var type = formatType;

            if ( type == null || type.Trim().Length == 0 )
            {
                return FormatType.Static;
            }

            type = type.ToLower().Trim();

            if ( type.Equals( "bitmap" ) )
            {
                return FormatType.Bitmap;
            }
            if ( type.Equals( "variable" ) )
            {
                return FormatType.Variable;
            }
            if ( type.Equals( "multiformat" ) )
            {
                return FormatType.MultiFormat;
            }
            if ( type.Equals( "variablearray" ) )
            {
                return FormatType.VariableArray;
            }

            return FormatType.Static;
        }

        /// <summary>
        /// Parses a list of fields and stores them in the supplied Format object.
        /// </summary>
        /// <param name="fieldXmlNodes">The node containing the fields to be
        /// parsed.</param>
        /// <param name="format">The format to store the fields in.</param>
        /// <param name="path">A dot-separated field name.</param>
        /// <returns></returns>
        protected void ParseFields( XmlNodeList fieldXmlNodes, Format format, string transType )
        {
            var index = 0;
            var position = new PositionData();
            for ( var i = 0; i < fieldXmlNodes.Count; i++ )
            {
                var node = fieldXmlNodes.Item( i );
                if ( node.NodeType != XmlNodeType.Element || node.Name != "Field" )
                {
                    continue;
                }

                index++;
                var field = ParseSingleField( node, format, index, transType );
                if ( field == null )
                {
                    index--;
                }
                else
                {
                    CalculatePosition( field, position );

                    field = AssertDuplicateField( field, format.Fields, format.Name );
                    format.AddField( field.Name, field );
                    if ( field.Lengths.Count != 0 )
                    {
                        format.Length = format.Length + field.Lengths[ 0 ];
                    }
                }
            }
        }

        private void CalculatePosition( Field field, PositionData position )
        {
            var prefix = field.PrefixLength;
            position.position += position.prevLength + ( prefix < 0 ? 0 : prefix );
            if ( field.Position == -1 || field.Position == position.position )
            {
                field.Position = position.position;
                prefix = field.SuffixLength;
                position.prevLength = field.Length + ( prefix < 0 ? 0 : prefix );
            }
            else
            {
                field.IsFloating = true;
            }
        }

        private bool IsValidOption( XmlNode node )
        {
            // Skip the field if depends on a DFR Option that is not enabled.
            try
            {
                var option = Utils.GetAttributeValue( "option", null, node );
                if ( option != null && factory != null && !factory.Config.DFROptions.Contains( option ) )
                {
                    return false;
                }
            }
            catch ( DispatcherException ex )
            {
                throw new ConverterException( Error.InitializationFailure, "Failed to get the DFR Options.", ex );
            }

            return true;
        }

        /// <summary>
        /// Parses and individual field.
        /// </summary>
        /// <param name="node">The node containing the field's XML.</param>
        /// <param name="path">The dot-separated path to the field.</param>
        /// <returns>A fully-parsed Field object.</returns>
        private Field ParseSingleField( XmlNode node, Format format, int index, string transType )
        {
            if ( !IsValidOption( node ) )
            {
                return null;
            }

            var field = new Field();
            field.Name = Utils.GetAttributeValue( "name", null, node );
            field.FieldID = format.FormatID + "." + field.Name;
            field.Hide = Utils.GetBoolAttributeValue( "hide", false, node );

            var alias = Utils.GetAttributeValue( "alias", null, node );
            var dep = Utils.GetAttributeValue( "deprecated_name", null, node );
            if ( dep != null )
            {
                alias = alias == null ? dep : alias + "," + dep;
            }
            field.Alias = alias;
            field.MultiColumn = Utils.GetBoolAttributeValue( "multi_column", false, node );
            field.Index = index;
            field.ArrayNodeName = Utils.GetAttributeValue( "array_node", null, node );
            field.ArrayIndicator = Utils.GetBoolAttributeValue( "array", false, node );
            field.Lengths.Add( GetIntNodeAttribute( "length", -1, node ) );
            field.DefaultValue = Utils.GetAttributeValue( "default", null, node );
            field.Required = Utils.GetAttributeValue( "required", "O", node );
            field.Justification = ConvertJustification( Utils.GetAttributeValue( "justification", null, node ) );
            ParseFillwith( field, Utils.GetAttributeValue( "fill", null, node ) );
            field.PrefixLength = GetIntNodeAttribute( "prefix_length", -1, node );
            ParseType( field, Utils.GetAttributeValue( "type", "A", node ) );
            ParseCase( field, Utils.GetAttributeValue( "case", null, node ) );
            field.MultiLength = Utils.GetBoolAttributeValue( "multi_length", false, node );
            field.IsSuppress = Utils.GetBoolAttributeValue( "suppress", false, node );
            field.SuffixLength = GetIntNodeAttribute( "suffix_len", -1, node );
            field.SuffixColumnName = Utils.GetAttributeValue( "suffix_col", null, node );
            field.PrefixColumnName = Utils.GetAttributeValue( "prefix_col", null, node );
            field.Position = GetIntNodeAttribute( "position", -1, node );
            var prefixValue = GetIntNodeAttribute( "prefix_1", 0, node );
            if ( prefixValue > 0 )
            {
                field.AddPrefixValue( prefixValue );
            }
            prefixValue = GetIntNodeAttribute( "prefix_2", 0, node );
            if ( prefixValue > 0 )
            {
                field.AddPrefixValue( prefixValue );
            }
            field.Start = Utils.GetAttributeValue( "start", null, node );
            field.Next = Utils.GetAttributeValue( "next", null, node );
            field.MatchParentField = Utils.GetAttributeValue( "match_parent", null, node );

            ParseChildFields( node, field, format, transType );

            if ( field.Justification == TextJustification.None )
            {
                field.Justification = field.FieldType == DataType.Numeric ? TextJustification.Right : TextJustification.Left;
            }

            if ( field.FillWith == null && format.FormatType != FormatType.VariableArray )
            {
                field.FillWith = field.FieldType == DataType.Numeric ? "0" : " ";
            }

            AddToUniqueFieldMap( field, transType );

            // We want the lengths in ascending order.
            field.Lengths.Sort();
            return field;
        }

        private void AddToUniqueFieldMap( Field field, string transType )
        {
            var transactionType = transType;

            SafeDictionary<string, List<string>> filenames = null;
            if ( this.uniqueFieldNames.ContainsKey( transactionType ) )
            {
                filenames = this.uniqueFieldNames[ transactionType ];
            }
            else
            {
                filenames = new SafeDictionary<string, List<string>>();
                this.uniqueFieldNames.Add( transactionType, filenames );
            }

            if ( filenames.ContainsKey( field.Name ) )
            {
                var list = filenames[ field.Name ];
                list.Add( field.FieldID );
            }
            else
            {
                var list = new List<string>();
                list.Add( field.FieldID );
                filenames.Add( field.Name, list );
            }
        }

        private void ParseChildFields( XmlNode node, Field field, Format format, string transType )
        {
            var children = node.ChildNodes;
            var position = new PositionData();
            for ( var j = 0; j < children.Count; j++ )
            {
                var child = children.Item( j );
                if ( child.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                var nodeName = child.Name;
                if ( nodeName.Equals( "Mask" ) )
                {
                    ParseFieldMask( child, field );
                }
                else if ( child.Name.Equals( "Field" ) )
                {
                    var childField = ParseSingleField( child, format, -1, transType );
                    ProcessChildField( childField, field, position );
                }
            }
        }

        private void ProcessChildField( Field childField, Field field, PositionData position )
        {
            if ( childField != null )
            {
                AssertDuplicateField( childField, field.Fields, field.Name );
                field.SetField( childField );
                var prefix = childField.PrefixLength;
                position.position += position.prevLength + ( prefix < 0 ? 0 : prefix );
                childField.Position = position.position;
                prefix = childField.SuffixLength;
                position.prevLength = childField.Length + ( prefix < 0 ? 0 : prefix );
            }
        }

        private void ParseCase( Field field, string value )
        {
            var val = value;

            if ( val == null )
            {
                val = "";
            }
            val = val.ToLower();
            if ( val.Equals( "l" ) )
            {
                field.CaseType = Case.Lower;
            }
            else if ( val.Equals( "u" ) )
            {
                field.CaseType = Case.Upper;
            }
            else
            {
                field.CaseType = Case.None;
            }
        }

        private void ParseFillwith( Field field, string val )
        {
            var value = val;

            if ( value != null && value.StartsWith( "0x" ) )
            {
                value = value.Substring( 2 );
                field.FillWith = Utils.StringToHexChar( value );
            }
            else
            {
                field.FillWith = value;
            }
        }

        private void ParseType( Field field, string value )
        {
            var val = value;

            if ( val == null )
            {
                val = "";
            }
            val = val.ToLower();
            if ( val.Equals( "n" ) )
            {
                field.FieldType = DataType.Numeric;
            }
            else if ( val.Equals( "b" ) )
            {
                field.FieldType = DataType.Binary;
            }
            else
            {
                field.FieldType = DataType.AlphaNumeric;
            }

            if ( field.Justification == TextJustification.None )
            {
                field.Justification = field.FieldType == DataType.Numeric ? TextJustification.Right : TextJustification.Left;
            }
        }

        /// <summary>
        /// Throw an exception if a field with this name already exists.
        /// </summary>
        /// <remarks>
        /// It's okay to have duplicate names for fields that are hidden,
        /// because their names don't matter. But they still must be unique,
        /// so in this case, we don't throw an exception, but rename the
        /// field to give it a unique numeric suffix.
        /// </remarks>
        /// <param name="childField">The field to test</param>
        /// <param name="fields">The list of existing fields</param>
        /// <param name="parentName">The name of the parent item</param>
        /// <returns></returns>
        private Field AssertDuplicateField( Field childField, List<Field> fields,
                string parentName )
        {
            foreach ( var child in fields )
            {
                if ( child.Name.Equals( childField.Name ) )
                {
                    if ( childField.Hide )
                    {
                        var name = childField.Name;
                        var parts = SplitNumberFromName( name );
                        var num = 0;
                        if ( parts[ 1 ] != null && parts[ 1 ].Length > 0 )
                        {
                            num = Utils.StringToInt( parts[ 1 ], 0 );
                        }
                        num++;
                        childField.Name = parts[ 0 ] + num;
                        return AssertDuplicateField( childField, fields,
                                parentName );
                    }
                    var msg = $"A field with the name \"{childField.Name}\" already exists in \"{parentName}\".";
                    engineLogger.Error( msg );
                    throw new ConverterException( Error.InvalidField, msg );
                }
            }
            return childField;
        }

        private string[] SplitNumberFromName( string name )
        {
            var parts = new string[ 2 ];
            for ( var i = name.Length - 1; i >= 0; i-- )
            {
                if ( !char.IsDigit( name[ i ] ) )
                {
                    parts[ 0 ] = name.Substring( 0, i + 1 );
                    parts[ 1 ] = name.Substring( i + 1 );
                    return parts;
                }
            }

            parts[ 0 ] = name;
            return parts;
        }

        private TextJustification ConvertJustification( string value )
        {
            var val = value;

            if ( val == null )
            {
                val = "";
            }
            val = val.ToLower();
            if ( val.Equals( "l" ) )
            {
                return TextJustification.Left;
            }
            else if ( val.Equals( "r" ) )
            {
                return TextJustification.Right;
            }
            else
            {
                return TextJustification.None;
            }
        }


        private void ParseFieldMask( XmlNode maskNode, Field field )
        {
            var len = field.Length;

            field.HasMask = true;
            field.MaskLength = GetIntNodeAttribute( "length", len, maskNode );
            field.MaskWith = Utils.GetAttributeValue( "with", " ", maskNode );
            field.MaskJustification = ConvertJustification( Utils.GetAttributeValue( "justification", "L", maskNode ) );
        }

        public Format GetFormat( string name )
        {
            return GetFormat( name, null );
        }

        public Format GetFormat( string name, string transType )
        {
            if ( name == null )
            {
                return new Format();
            }
            var path = name.Split( '.' );
            var format = FindRootFormat( path[ 0 ], transType );
            if ( format == null || IsEmpty( format ) )
            {
                return new Format();
            }
            for ( var i = 1; i < path.Length; i++ )
            {
                var child = format.Formats[ path[ i ] ];
                child = GetChildFromFormatList( child, format, path[ i ] );

                if ( child != null )
                {
                    format = child;
                }
                else if ( format.ArrayFormat != null )
                {
                    format = GetArrayFormat( name, format );
                }
                else if ( format.FormatType == FormatType.VariableArray )
                {
                    child = format.Formats[ path[ i ] + "1" ];
                    if ( child != null )
                    {
                        format = child;
                    }
                }
                else if ( !path[ i ].Equals( format.Name ) && format.GetField( path[ i ] ).IsEmpty )
                {
                    return new Format();
                }
            }

            return format;
        }

        private Format GetArrayFormat( string name, Format format )
        {
            var arrayElementName = name.Substring( 0, name.LastIndexOf( '.' ) + 1 );
            if ( name.StartsWith( format.ArrayFormat.Path ) )
            {
                var start = format.ArrayFormat.Path.Length;
                if ( start < arrayElementName.Length )
                {
                    var numText = arrayElementName.Substring( start );
                    if ( Utils.IsNumeric( numText ) )
                    {
                        return format.ArrayFormat;
                    }
                }
            }

            return format;
        }

        private Format GetChildFromFormatList( Format childFormat, Format format, string path )
        {
            var child = childFormat;
            if ( child != null )
            {
                return child;
            }

            for ( var j = 0; j < format.Formats.Count; j++ )
            {
                foreach ( var alias in format.Formats.Get( j ).Aliases )
                {
                    if ( alias != null && alias.Equals( path ) )
                    {
                        child = format.Formats[ format.Formats.Get( j ).Name ];
                        if ( child != null )
                        {
                            return child;
                        }
                    }
                }
            }

            return child;
        }

        private Format FindRootFormat( string name, string transType )
        {
            var format = formats[ name ];
            if ( !IsEmpty( format ) )
            {
                return format;
            }

            if ( transType != null )
            {
                format = this.formats[ transType ];
                if ( !this.IsEmpty( format ) )
                {
                    format = format.Formats[ name ];
                    if ( !this.IsEmpty( format ) )
                    {
                        format.IsRootFormat = format.IsRootFormat;
                        return format;
                    }
                }
            }

            foreach ( var child in formats.Values )
            {
                format = child;
                var isRoot = format.IsRootFormat;
                if ( format.HideFromFieldID )
                {
                    format = format.Formats[ name ];
                    if ( !IsEmpty( format ) )
                    {
                        format.IsRootFormat = isRoot;
                        return format;
                    }
                }
            }

            return null;
        }

        public Order GetOrder()
        {
            return order;
        }


        private void ParseListXmlNode( List<string> list, XmlNode listTypeXmlNode )
        {
            var listXmlNode = GetChildXmlNodeByName( "List", listTypeXmlNode );
            if ( listXmlNode == null )
            {
                return;
            }
            var content = Utils.GetNodeValue( listXmlNode );
            var array = content.Split( ',' );
            for ( var i = 0; i < array.Length; i++ )
            {
                list.Add( array[ i ] );
            }
        }

        /**
         * Parses the comma-delimited lists in the Order element.
         * @param node
         */
        private void ParseList( XmlNode node )
        {
            var name = node.Name;
            var lists = new OrderLists();
            var listTypeXmlNodes = node.ChildNodes;
            for ( var i = 0; i < listTypeXmlNodes.Count; i++ )
            {
                var listTypeXmlNode = listTypeXmlNodes.Item( i );
                if ( listTypeXmlNode.Name.Equals( "ActionCodeNotIn" ) )
                {
                    ParseListXmlNode( lists.ActionCodeNotIn, listTypeXmlNode );
                }
                else if ( listTypeXmlNode.Name.Equals( "ActionCodeIn" ) )
                {
                    ParseListXmlNode( lists.ActionCodeIn, listTypeXmlNode );
                }
                else if ( listTypeXmlNode.Name.Equals( "Default" ) )
                {
                    ParseListXmlNode( lists.Defaults, listTypeXmlNode );
                }
            }
            order.Lists.Add( name, lists );
        }

        /// <summary>
        /// Parses the MessageFormat elements in the template.
        /// </summary>
        /// <param name="document"></param>
        protected void ParseMessageFormats( XmlDocument document )
        {
            var messageFormatNode = document.GetElementsByTagName( "MessageFormat" );
            if ( messageFormatNode.Count == 0 )
            {
                return;
            }
            var formatNodes = messageFormatNode.Item( 0 ).ChildNodes;
            for ( var i = 0; i < formatNodes.Count; i++ )
            {
                var node = formatNodes.Item( i );
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                var name = node.Name;
                var children = node.ChildNodes;
                for ( var j = 0; j < children.Count; j++ )
                {
                    if ( children.Item( j ).Name.Equals( "Response" ) )
                    {
                        this.messageFormats.Add( name, Utils.GetTextNode( children.Item( j ) ).InnerText );
                        break;
                    }
                }
            }
        }

        /**
         * Parses the Order element in the template.
         * @param document
         */
        protected void ParseOrder( XmlDocument document )
        {
            order = new Order();

            var orderXmlNode = document.GetElementsByTagName( "Order" );
            if ( orderXmlNode == null || orderXmlNode.Count == 0 )
            {
                return;
            }
            orderXmlNode = orderXmlNode.Item( 0 ).ChildNodes;
            for ( var i = 0; i < orderXmlNode.Count; i++ )
            {
                var node = orderXmlNode.Item( i );
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                if ( node.Name.Equals( "FormatName" ) )
                {
                    order.FormatName = Utils.GetNodeValue( node );
                }
                else if ( node.Name.Equals( "AmountFieldName" ) )
                {
                    order.AmountFieldName = Utils.GetNodeValue( node );
                }
                else if ( node.Name.Equals( "ActionCodeFieldName" ) )
                {
                    order.ActionCodeFieldName = Utils.GetNodeValue( node );
                }
                else if ( node.Name.Equals( "MOPFieldName" ) )
                {
                    order.MopFieldName = Utils.GetNodeValue( node );
                }
                else
                {
                    ParseList( node );
                }
            }
        }

        private XmlNode GetChildXmlNodeByName( string name, XmlNode node )
        {
            var children = node.ChildNodes;
            for ( var i = 0; i < children.Count; i++ )
            {
                var child = children.Item( i );
                if ( child.Name.Equals( name ) )
                {
                    return child;
                }
            }
            return null;
        }

        //private void ParseFormatFinders( XmlNode rootNode )
        //{
        //    foreach ( XmlNode node in rootNode )
        //    {
        //        FormatFinders.Add( new FormatFinder( node ) );
        //    }
        //}
    }
}