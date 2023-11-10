using System;
using System.Collections.Generic;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Configurator;

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
    public enum RequestType
    {
		/// <summary>
		/// 
		/// </summary>
        Online,
		/// <summary>
		/// 
		/// </summary>
        Batch,
		/// <summary>
		/// 
		/// </summary>
        PNSOnline
    }

	/// <summary>
	/// 
	/// </summary>
	public enum Case
	{
		/// <summary>
		/// Do not change the case. Default value.
		/// </summary>
		None,
		/// <summary>
		/// Lowercase
		/// </summary>
		Lower,
		/// <summary>
		/// Uppercase
		/// </summary>
		Upper
	}

	/// <summary>
	/// 
	/// </summary>
	public enum DataType
	{
		/// <summary>
		/// 
		/// </summary>
		AlphaNumeric,
		/// <summary>
		/// 
		/// </summary>
		Numeric,
        /// <summary>
        /// 
        /// </summary>
        Binary
	}

	/// <summary>
	/// 
	/// </summary>
	public enum TextJustification
	{
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// 
		/// </summary>
		Left,
		/// <summary>
		/// 
		/// </summary>
		Right
	}

	/// <summary>
	/// 
	/// </summary>
    public class ConverterTemplate
    {
		/// <summary>
		/// 
		/// </summary>
        protected bool initialized = false;
		/// <summary>
		/// 
		/// </summary>
		protected ILog engineLogger = null;
		/// <summary>
		/// 
		/// </summary>
		protected IConfigurator configurator = null;
		/// <summary>
		/// 
		/// </summary>
		/// <summary>
		/// 
		/// </summary>
		/// <summary>
		/// 
		/// </summary>
		protected IDispatcherFactory factory = null;
		/// <summary>
		/// 
		/// </summary>
		protected List<String> bitmapFormats = new List<String>();
		/// <summary>
		/// 
		/// </summary>
		protected SafeDictionary<string, Format> formats = new SafeDictionary<string, Format>();
        protected SafeDictionary<string, string> aliases = new SafeDictionary<string, string>();
        /// <summary>
		/// 
		/// </summary>
		protected int defaultFormatLength = 1000;

        // Data
		/// <summary>
		/// 
		/// </summary>
		protected int formatIndicatorLength = 0;
		
		
		/// <summary>
		/// 
		/// </summary>
		protected string templateVersion = null;
		/// <summary>
		/// 
		/// </summary>
		protected SafeDictionary<String, String> messageFormats = new SafeDictionary<String, String>();
		/// <summary>
		/// 
		/// </summary>
		protected Order order = new Order();

		/// <summary>
		/// 
		/// </summary>
        public class Order
        {
			/// <summary>
			/// 
			/// </summary>
            public string formatName = null;
			/// <summary>
			/// 
			/// </summary>
			public string amountFieldName = null;
			/// <summary>
			/// 
			/// </summary>
			public string actionCodeFieldName = null;
			/// <summary>
			/// 
			/// </summary>
			public string mopFieldName = null;
			/// <summary>
			/// 
			/// </summary>
			public KeySafeDictionary<String, OrderLists> lists = new KeySafeDictionary<String, OrderLists>();
        }

		/// <summary>
		/// 
		/// </summary>
        public class OrderLists
        {
			/// <summary>
			/// 
			/// </summary>
            public List<String> actionCodeNotIn = new List<String>();
			/// <summary>
			/// 
			/// </summary>
			public List<String> actionCodeIn = new List<String>();
			/// <summary>
			/// 
			/// </summary>
			public List<String> defaults = new List<String>();
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nodeName"></param>
		/// <param name="document"></param>
		/// <returns></returns>
        public string FindNodeValue( string nodeName, XmlDocument document )
        {
            return FindNodeValue( nodeName, null, document );
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nodeName"></param>
		/// <param name="defaultValue"></param>
		/// <param name="document"></param>
		/// <returns></returns>
        public string FindNodeValue( string nodeName, string defaultValue,
                XmlDocument document )
        {
            XmlNodeList nodes = document.GetElementsByTagName( nodeName );
            if ( nodes.Count == 0 )
            {
                return defaultValue;
            }
            XmlNode textNode = Utils.GetTextNode( nodes[0] );
            if ( textNode == null )
            {
                return defaultValue;
            }

            string text = textNode.Value;
            if ( text == null || text.Length == 0 )
            {
                return defaultValue;
            }
            return text;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		protected bool IsEmpty( Format format )
		{
			return ( format == null || format.IsEmpty );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nodeName"></param>
		/// <param name="attributeName"></param>
		/// <param name="document"></param>
		/// <returns></returns>
        public string FindNodeAttribute( string nodeName, string attributeName,
                XmlDocument document )
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
        public string FindNodeAttribute( string nodeName, string attributeName,
                string defaultValue, XmlDocument document )
        {
            XmlNodeList nodes = document.GetElementsByTagName( nodeName );
            if ( nodes.Count == 0 )
            {
                return defaultValue;
            }
            return Utils.GetAttributeValue( attributeName, defaultValue, nodes[0] );
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
        /// Takes a list of nodes whose Type attribute is set to "Format" 
        /// and parses them into Format objects that is stored in a Dictionary.
        /// </summary>
        /// <param name="formatXmlNodes">The list of Format nodes to parse</param>
        /// <param name="name">The full dot-separated "path" of the name, such as "Request.PIDH"</param>
        /// <param name="parent">The parent format</param>
        protected void ParseFormats( XmlNodeList formatXmlNodes, string name, Format parent )
        {
            for ( int i = 0; i < formatXmlNodes.Count; i++ )
            {
				try
				{
					XmlNode node = formatXmlNodes[i];
					if ( name == "Request" && node.Name == "BitMap" )
					{
						bitmapFormats.Add( name );
					}
					if ( node.NodeType != XmlNodeType.Element )
					{
						continue;
					}
					if ( Utils.GetAttributeValue( "Type", "", node ) != "Format" && node.Name != "Format" )
					{
						continue;
					}

					Format format = new Format();
					format.Parent = parent;
					format.Name = ( node.Name == "Format" ) ? Utils.GetAttributeValue( "Name", null, node ) : node.Name;
					format.RedirectTo = Utils.GetAttributeValue( "RedirectTo", null, node );
					format.Type = SetFormatType( Utils.GetAttributeValue( "FormatType", null, node ) );
					format.BitMapFormat = SetBitMapType( Utils.GetAttributeValue( "BitMapType", null, node ) );
					format.PrefixLength = GetIntNodeAttribute( "PrefixLength", -1, node );
					format.PrefixData = Utils.GetAttributeValue( "PrefixData", null,
							node );
                    format.HideFromFieldID = GetBoolAttribute( "HideFromPath", false, node );
                    format.IsRootFormat = GetBoolAttribute( "IsRootFormat", false, node );
                    format.IgnoreShort = GetBoolAttribute( "IgnoreShort", false, node );
                    format.CountSize = GetIntNodeAttribute( "CountSize", -1, node );
					format.Alias = Utils.GetAttributeValue( "Alias", null, node );
					format.FieldSeparator = Utils.GetAttributeValue( "FieldSeparator",
							format.FieldSeparator, node );
					if ( format.FieldSeparator != null
							&& format.FieldSeparator.StartsWith( "[" )
							&& format.FieldSeparator.EndsWith( "]" ) )
					{
						format.FieldSeparator = Utils
								.StringToHexChar( format.FieldSeparator );
					}
					format.Bit = GetIntNodeAttribute( "Bit", -1, node );
					format.ResponseType = Utils.GetAttributeValue( "ResponseType",
							null, node );
					format.PrefixFormatName = Utils.GetAttributeValue(
							"PrefixFormatName", null, node );
					format.DelimitedResponseType = Utils.GetAttributeValue(
							"DelimitedResponseType", null, node );
					format.SkipWrite = Utils.StringToBoolean( Utils.GetAttributeValue(
							"SkipWrite", null, node ) );
					format.Condition = Utils.StringToBoolean( Utils.GetAttributeValue(
							"Condition", null, node ) );
					format.TCPFileTerminator = Utils.GetAttributeValue(
                            "TCPFileTerminator", FileTerminator, node );
					format.TCPRecordDelimiter = Utils.GetAttributeValue(
							"TCPRecordDelimiter", null, node );
					format.SFTPRecordDelimiter = Utils.GetAttributeValue(
							"SFTPRecordDelimiter", null, node );
					format.SFTPFileTerminator = Utils.GetAttributeValue(
							"SFTPFileTerminator", null, node );
					format.FormatName = Utils.GetAttributeValue( "FormatName", null,
							node );

                    if ( name == "Request" || name == "Response" )
                    {
                        format.Path = FieldIdentifier.Separator + format.Name;
                    }
                    else
                    {
                        format.Path = name + FieldIdentifier.Separator + format.Name;
                    }

					if ( format.Type == FormatType.Bitmap )
					{
						bitmapFormats.Add( format.Path );
					}
                    ParseFields( node.ChildNodes, format, format.Path + FieldIdentifier.Separator );
					if ( format.Type == FormatType.Bitmap )
					{
						bitmapFormats.Add( format.Path );
					}
					ParseFormats( node.ChildNodes, format.Path, format );

                    ParseFormatRefs( node.ChildNodes, format );

					if ( format.CountSize == -1 )
					{
						if ( parent != null )
						{
							parent.Formats[format.Name] = format;
						}
						else
						{
							formats[format.Name] = format;
                            if ( format.Alias != null )
                            {
                                if ( !aliases.ContainsKey( format.Alias ) )
                                {
                                    aliases.Add( format.Alias, format.Name );
                                }
                                else
                                {
                                    engineLogger.DebugFormat( "The format alias \"{0}\" for \"{1}\" is already in use. Skipping.", format.Alias, format.Name );
                                }
                            }
						}
					}
					else
					{
						for ( int index = 1; index <= format.CountSize; index++ )
						{
							Format fmt = new Format( format );
							fmt.Name = String.Format( "{0}{1}", format.Name, index );

							if ( parent != null )
							{
								parent.Formats[fmt.Name] = fmt;
							}
							else
							{
								formats[fmt.Name] = fmt;
                                if ( format.Alias != null )
                                {
                                    aliases.Add( fmt.Alias, fmt.Name );
                                }
                            }
						}
					}
				}
				catch ( Exception ex )
				{
					throw new ConverterException( Error.InitializationFailure, "Failed to parse Formats", ex );
				}
            }
        }

        private void ParseFormatRefs( XmlNodeList nodes, Format format )
        {
            for ( int i = 0; i < nodes.Count; i++ )
            {
                XmlNode node = nodes.Item( i );
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

			switch ( type.ToLower().Trim() )
			{
				case "hex":
					return BitMapType.Hex;
				default:
					return BitMapType.Binary;
			}
		}

		private FormatType SetFormatType( string type )
		{
			if ( type == null || type.Trim().Length == 0 )
			{
				return FormatType.Static;
			}

			switch ( type.ToLower().Trim() )
			{
				case "bitmap":
					return FormatType.Bitmap;
				case "variable":
					return FormatType.Variable;
				case "multiformat":
					return FormatType.MultiFormat;
				case "variablearray":
					return FormatType.VariableArray;
				default:
					return FormatType.Static;
			}
		}

        /// <summary>
        /// Parses a list of fields and stores them in the supplied Format object.
        /// </summary>
        /// <param name="fieldXmlNodes">The node containing the fields to be parsed.</param>
        /// <param name="format">The format to store the fields in.</param>
        /// <param name="path">A dot-separated field name.</param>
		/// <returns></returns>
        protected void ParseFields( XmlNodeList fieldXmlNodes, Format format, string path )
        {
            int index = 0;
			int position = 1;
			int prevLength = 0;
            for ( int i = 0; i < fieldXmlNodes.Count; i++ )
            {
                XmlNode node = fieldXmlNodes[i];
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                if ( Utils.GetAttributeValue( "Type", "", node ) != "Field" )
                {
                    continue;
                }

                index++;
                Field field = ParseSingleField( node, path, format, index );
                if ( field == null )
                {
                    index--;
                    continue;
                }

				// Calculate the position property.
				int prefix = field.PrefixLength;
				position += prevLength + ( ( prefix < 0 ) ? 0 : prefix );
				if ( field.Position == -1 || field.Position == position )
				{
					field.Position = position;
					prefix = field.SuffixLength;
					prevLength = field.Length + ( ( prefix < 0 ) ? 0 : prefix );
				}
				else
				{
					field.IsFloating = true;
				}

				field = AssertDuplicateField( field, format.Fields, format.Name );
                format.AddField( field.Name, field );
                if ( field.Lengths.Count > 0 )
                {
                    format.Length = format.Length + field.Lengths[ 0 ];
                }
            }
        }

		private bool GetBoolAttribute( string name, bool defValue, XmlNode node )
		{
			string def = ( defValue ) ? "true" : "false";
			return Utils.StringToBoolean( Utils.GetAttributeValue( name, def, node ) );
		}

		private int GetIntAttribute( string name, int defValue, XmlNode node )
		{
			return Utils.StringToInt( Utils.GetAttributeValue( name, defValue.ToString(), node ) );
		}

		/// <summary>
        /// Parses and individual field.
        /// </summary>
        /// <param name="node">The node containing the field's XML.</param>
        /// <param name="path">The dot-separated path to the field.</param>
        /// <returns>A fully-parsed Field object.</returns>
        private Field ParseSingleField( XmlNode node, string path, Format format, int index )
        {
            string option = Utils.GetAttributeValue( "Option", null, node );
            if ( option != null )
            {
                if ( factory != null && !factory.Config.DFROptions.Contains( option ) )
                {
                    return null;
                }
            }

            Field field = new Field();
            field.Name = ( node.Name == "Field" ) ? Utils.GetAttributeValue( "Name", null, node ) : node.Name;
            field.Path = path + field.Name;
            field.Hide = GetBoolAttribute( "Hide", false, node );
            field.Alias = Utils.GetAttributeValue( "Alias", null, node );
            field.IsMultiColumn = GetBoolAttribute( "MultiColumn", false, node );
            field.Index = index;
			int len = GetIntAttribute( "Length", -1, node );
			if ( len > -1 )
			{
				field.Lengths.Add( len );
			}
			field.StoreAs = Utils.GetAttributeValue( "StoreAs", field.StoreAs, node );
            field.FieldSeparator = Utils.GetAttributeValue( "FieldSeparator", field.FieldSeparator, node );
			field.Include = GetBoolAttribute( "Include", true, node );
			field.CacheField = Utils.GetAttributeValue( "CacheField", null, node );
			field.CacheEquals = Utils.GetAttributeValue( "CacheEquals", null, node );
            field.HasCacheValue = GetBoolAttribute( "SetCache", false, node );
            field.ArrayNodeName = Utils.GetAttributeValue( "ArrayNodeName", null, node );
			field.ArrayIndicator = GetBoolAttribute( "IsArray", false, node );
			if ( field.FieldSeparator != null
                    && field.FieldSeparator.StartsWith( "[" )
                    && field.FieldSeparator.EndsWith( "]" ) )
            {
                field.FieldSeparator = Utils.StringToHexChar( field.FieldSeparator );
            }
            XmlNodeList children = node.ChildNodes;
            int position = 1;
            int prevLength = 0;
            for ( int j = 0; j < children.Count; j++ )
            {
                XmlNode child = children[j];
                if ( child.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
				switch ( child.Name )
				{
					case "Length":
						field.Lengths.Add( GetIntNodeValue( child, -1 ) );
						break;
					case "Position":
						field.Position = GetIntNodeValue( child, 0 );
						break;
					case "Default":
						field.DefaultValue = Utils.GetNodeValue( child );
                        field.FillDefault = GetBoolAttribute( "fill", false, child );
						break;
					case "Justification":
						field.Justification = ConvertJustification( child );
						break;
					case "Fillwith":
						ParseFillwith( field, child );
						break;
					case "PrefixLength":
						field.PrefixLength = GetIntNodeValue( child, -1 );
						break;
					case "Type":
						ParseType( field, child );
						break;
					case "Case":
						ParseCase( field, child );
						break;
					case "Mask":
						ParseFieldMask( child.ChildNodes, field );
						break;
					case "ArrayNodeName":
						field.ArrayNodeName = Utils.GetNodeValue( child );
						break;
					case "ArrayLength":
						field.ArrayLength = this.GetIntNodeValue( child, -1 );
						break;
					case "ArrayIndicator":
						field.ArrayIndicator = Utils.StringToBoolean( Utils
								.GetNodeValue( child ) );
						break;
					case "Hide":
						field.Hide = Utils.StringToBoolean( Utils
								.GetNodeValue( child ) );
						break;
					case "ArrayName":
						format.ArrayFormatName = Utils.GetNodeValue( child );
						break;
					case "MultiLength":
						field.MultiLength = Utils.StringToBoolean( Utils
								.GetNodeValue( child ) );
						break;
					case "Suppress":
						field.Suppress = Utils.StringToBoolean( Utils.GetNodeValue( child ) );
						break;
					case "SuffixLength":
						field.SuffixLength = GetIntNodeValue( child, -1 );
						break;
					case "SuffixColumnName":
						field.SuffixColumnName = Utils.GetNodeValue( child );
						field.HideSuffixColumnName = Utils.StringToBoolean( Utils
								.GetAttributeValue( "Hide", "false", child ) );
						break;
					case "PrefixColumnName":
						field.PrefixColumnName = Utils.GetNodeValue( child );
						field.HidePrefixColumnName = Utils.StringToBoolean( Utils
								.GetAttributeValue( "Hide", "false", child ) );
						break;
				}
				
				if ( child.Name.StartsWith( "PrefixValue_" ) )
				{
					ParsePrefixValue( child, field );
				}
				else if ( Utils.GetAttributeValue( "Type", "", child ) == "Field" )
				{
                    Field childField = ParseSingleField( child, field.Path + FieldIdentifier.Separator, format, -1 );
                    if ( childField != null )
                    {
                        //AssertDuplicateField( childField, field.Fields, field.Name );
                        //field.SetField( childField );

                        AssertDuplicateField( childField, field.Fields, field.Name );
                        field.SetField( childField );
                        int prefix = childField.PrefixLength;
                        position += prevLength + ( ( prefix < 0 ) ? 0 : prefix );
                        childField.Position = position;
                        prefix = childField.SuffixLength;
                        prevLength = childField.Length + ( ( prefix < 0 ) ? 0 : prefix );
                    }
				}
            }

            if ( field.Justification == TextJustification.None )
            {
                field.Justification = ( field.Type == DataType.Numeric ) ? TextJustification.Right : TextJustification.Left;
            }

            if ( field.FillWith == null && format.Type != FormatType.VariableArray )
            {
                field.FillWith = ( field.Type == DataType.Numeric ) ? "0" : " ";
            }

			// We want the lengths in ascending order.
			field.Lengths.Sort();
            return field;
        }

		private void ParseCase( Field field, XmlNode child )
		{
			string val = Utils.GetNodeValue( child );
			if ( val == null )
			{
				val = "";
			}
			switch ( val.ToLower() )
			{
				case "l":
					field.CaseType = Case.Lower;
					break;
				case "u":
					field.CaseType = Case.Upper;
					break;
				default:
					field.CaseType = Case.None;
					break;
			}
		}

		private void ParseFillwith( Field field, XmlNode child )
		{
			field.FillWith = Utils.GetNodeValue( child );
			if ( Utils.GetAttributeValue( "DataType", "none", child ).ToLower()
					== "hex" )
			{
				field.FillWith = Utils.StringToHexChar( field.FillWith );
			}
		}

		private void ParseType( Field field, XmlNode child )
		{
			string val = Utils.GetNodeValue( child );
			if ( val == null )
			{
				val = "";
			}
			switch ( val.ToLower() )
			{
				case "n":
					field.Type = DataType.Numeric;
					break;
				case "b":
					field.Type = DataType.Binary;
					break;
				case "a":
				default:
					field.Type = DataType.AlphaNumeric;
					break;
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
		private Field AssertDuplicateField( Field childField, List<Field> fields, string parentName )
		{
            foreach ( Field child in fields )
            {
                if ( child.Name == childField.Name )
                {
                    if ( childField.Hide )
                    {
                        string name = childField.Name;
                        string[] parts = SplitNumberFromName( name );
                        int num = 0;
                        if ( parts[ 1 ] != null && parts[ 1 ].Length > 0 )
                        {
                            num = Convert.ToInt32( parts[ 1 ] );
                        }
                        num++;
                        childField.Name = parts[ 0 ] + num;
                        return AssertDuplicateField( childField, fields, parentName );
                    }
                    string msg = String.Format( "A field with the name \"{0}\" already exists in \"{1}\".",
                        childField.Name, parentName );
                    engineLogger.Error( msg );
                    throw new ConverterException( Error.InvalidField, msg );
                }
            }
			return childField;
		}

		private string[] SplitNumberFromName( string name )
		{
			string[] parts = new string[2];
			for ( int i = name.Length - 1; i >= 0; i-- )
			{
				if ( !Char.IsDigit( name[i] ) )
				{
					parts[0] = name.Substring( 0, i + 1 );
					parts[1] = name.Substring( i + 1 );
					return parts;
				}
			}

			parts[0] = name;
			return parts;
		}

		private TextJustification ConvertJustification( XmlNode node )
		{
			string val = Utils.GetNodeValue( node );
			if ( val == null )
			{
				val = "";
			}
			switch ( val.ToLower() )
			{
				case "l":
					return TextJustification.Left;
				case "r":
					return TextJustification.Right;
				default:
					return TextJustification.None;
			}
		}

        /// <summary>
        /// Parses the array of PrefixValues in a given field.
        /// </summary>
        /// <remarks>
        /// Some fields contain an array of one or more PrefixValue elements. 
        /// Each one has an element name that contains the array index, such as "PrefixValue_1".
        /// These elements have to be parsed into field's PrefixValues list with the appropriate
        /// indexes.
        /// </remarks>
        /// <param name="node">The node of a specific PrefixValue_X element.</param>
        /// <param name="field"></param>
        private void ParsePrefixValue( XmlNode node, Field field )
        {
            string nodeName = ( node.Name == "Field" ) ? Utils.GetAttributeValue( "Name", null, node ) : node.Name;
            nodeName = nodeName.Substring( 12 ).Trim();
            int index = Utils.StringToInt( nodeName, 1 );
            if ( index - 1 > field.PrefixValues.Count )
            {
                string msg = "The PrefixValues of field " + field.Name
                        + " are not numbered properly.";
                throw new ConverterException( Error.BadDataError, msg );
            }
            XmlNodeList nodes = node.ChildNodes;
            for ( int i = 0; i < nodes.Count; i++ )
            {
                if ( nodes[i].Name == "Length" )
                {
                    int length = Utils.StringToInt( Utils.GetTextNode( nodes[i] ).Value, 0 );
                    field.PrefixValues.Insert( index - 1, length );
                    return;
                }
            }
        }

        /// <summary>
        /// Parses the Mask element, which has its own properties.
        /// </summary>
        /// <param name="maskFields"></param>
        /// <param name="field"></param>
        private void ParseFieldMask( XmlNodeList maskFields, Field field )
        {
            field.HasMask = true;
            for ( int i = 0; i < maskFields.Count; i++ )
            {
                XmlNode node = maskFields[i];
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                if ( node.Name == "Length" )
                {
                    field.MaskLength = GetIntNodeValue( node, -1 );
                }
                else if ( node.Name == "MaskWith" )
                {
                    field.MaskWith = Utils.GetNodeValue( node );
                }
                else if ( node.Name == "Justification" )
                {
					field.MaskJustification = ConvertJustification( node );
                }
            }
        }

        /// <summary>
        /// Gets a format matching the specified name.
        /// </summary>
        /// <param name="name">The name of the format to get.</param>
        /// <returns>The requested format, or null if not found.</returns>
        public Format GetFormat( string name )
        {
            return GetFormat( name, true );
        }

        /// <summary>
        /// Gets a format matching the specified name.
        /// </summary>
        /// <param name="name">The name of the format to get.</param>
        /// <param name="returnEmpty">True if it should return an empty Format
        /// object if the format was not found, false to return null.</param>
        /// <returns>The requested format.</returns>
        private Format GetFormat( string name, bool returnEmpty )
        {
            if ( name == null )
            {
                return ( returnEmpty ) ? new Format() : null;
            }
            string formatName = name;
            if ( formatName.StartsWith( "Request." ) )
            {
                formatName = formatName.Substring( 8 );
            }
            if ( formatName.StartsWith( "Response." ) )
            {
                formatName = formatName.Substring( 9 );
            }

            String[] path = formatName.Split( '.' );
            Format format = formats[path[0]];
            if ( IsEmpty( format ) )
            {
                if ( aliases.ContainsKey( path[ 0 ] ) )
                {
                    format = formats[ aliases[ path[ 0 ] ] ];
                }
                if ( IsEmpty( format ) )
                {
                    return ( returnEmpty ) ? new Format() : null;
                }
            }
            for ( int i = 1; i < path.Length; i++ )
            {
                Format child = format.Formats[path[i]];
                if ( child != null )
                {
                    format = child;
                }
				else if ( format.ArrayFormat != null )
				{
					String arrayElementName = name.Substring( 0, name.LastIndexOf( '.' ) + 1 );
					if ( name.StartsWith( format.ArrayFormat.Path ) )
					{
						int start = format.ArrayFormat.Path.Length;
						if ( start < arrayElementName.Length )
						{
							String numText = arrayElementName.Substring( start );
							if ( Utils.IsNumeric( numText ) )
							{
								return format.ArrayFormat;
							}
						}
					}
				}
				else if ( format.ArrayFormatName != null && path[i] == format.ArrayFormatName )
				{

				}
				else if ( path[i] != format.Name && format[path[i]].IsEmpty )
				{
					return new Format();
				}
            }

			if ( format.RedirectTo != null )
			{
				return GetFormat( format.RedirectTo );
			}

            return format;
        }

		/// <summary>
		/// 
		/// </summary>
        public int DefaultFormatLength
        {
            get { return defaultFormatLength; }
        }

		/// <summary>
		/// 
		/// </summary>
		public Order GetOrder()
        {
            return order;
        }

		/// <summary>
		/// 
		/// </summary>
        public string FileTerminator { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
        public string FormatDelimiter { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
		public string MessageDelimiter { get; protected set; }

        private void ParseListXmlNode( List<String> list, XmlNode listTypeXmlNode )
        {
            XmlNode listXmlNode = GetChildXmlNodeByName( "List", listTypeXmlNode );
            if ( listXmlNode == null )
            {
                return;
            }
            string content = Utils.GetNodeValue( listXmlNode );
            String[] array = content.Split( ',' );
            for ( int i = 0; i < array.Length; i++ )
            {
                list.Add( array[i] );
            }
        }

        /// <summary>
        /// Parses the comma-delimited lists in the Order element.
        /// </summary>
        /// <param name="node">The node containing the list element.</param>
        private void ParseList( XmlNode node )
        {
            string name = node.Name;
            OrderLists lists = new OrderLists();
            XmlNodeList listTypeXmlNodes = node.ChildNodes;
            for ( int i = 0; i < listTypeXmlNodes.Count; i++ )
            {
                XmlNode listTypeXmlNode = listTypeXmlNodes[i];
                if ( listTypeXmlNode.Name == "ActionCodeNotIn" )
                {
                    ParseListXmlNode( lists.actionCodeNotIn, listTypeXmlNode );
                }
                else if ( listTypeXmlNode.Name == "ActionCodeIn" )
                {
                    ParseListXmlNode( lists.actionCodeIn, listTypeXmlNode );
                }
                else if ( listTypeXmlNode.Name == "Default" )
                {
                    ParseListXmlNode( lists.defaults, listTypeXmlNode );
                }
            }
            order.lists[name] = lists;
        }

        /// <summary>
        /// Parses the MessageFormat elements in the template.
        /// </summary>
        /// <param name="document"></param>
        protected void ParseMessageFormats( XmlDocument document )
        {
            XmlNodeList messageFormats = document
                    .GetElementsByTagName( "MessageFormat" );
            if ( messageFormats.Count == 0 )
            {
                return;
            }
            XmlNodeList formats = messageFormats[0].ChildNodes;
            for ( int i = 0; i < formats.Count; i++ )
            {
                XmlNode node = formats[i];
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                string name = node.Name;
                XmlNodeList children = node.ChildNodes;
                for ( int j = 0; j < children.Count; j++ )
                {
                    if ( children[j].Name == "Response" )
                    {
                        this.messageFormats[name] = 
							Utils.GetTextNode( children[j] ).Value;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Parses the Order element in the template.
        /// </summary>
        /// <param name="document"></param>
        protected void ParseOrder( XmlDocument document )
        {
            order = new Order();

            XmlNodeList orderXmlNode = document.GetElementsByTagName( "Order" );
            if ( orderXmlNode == null || orderXmlNode.Count == 0 )
            {
                return;
            }
            orderXmlNode = orderXmlNode[0].ChildNodes;
            for ( int i = 0; i < orderXmlNode.Count; i++ )
            {
                XmlNode node = orderXmlNode[i];
                if ( node.NodeType != XmlNodeType.Element )
                {
                    continue;
                }
                if ( node.Name == "FormatName" )
                {
                    order.formatName = Utils.GetNodeValue( node );
                }
                else if ( node.Name == "AmountFieldName" )
                {
                    order.amountFieldName = Utils.GetNodeValue( node );
                }
                else if ( node.Name == "ActionCodeFieldName" )
                {
                    order.actionCodeFieldName = Utils.GetNodeValue( node );
                }
                else if ( node.Name == "MOPFieldName" )
                {
                    order.mopFieldName = Utils.GetNodeValue( node );
                }
                else
                {
                    ParseList( node );
                }
            }
        }

        private XmlNode GetChildXmlNodeByName( string name, XmlNode node )
        {
            XmlNodeList children = node.ChildNodes;
            for ( int i = 0; i < children.Count; i++ )
            {
                XmlNode child = children[i];
                if ( child.Name == name )
                {
                    return child;
                }
            }
            return null;
        }
    }
}
