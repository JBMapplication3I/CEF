#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Configurator
{
    /// <summary>
    ///
    /// </summary>
    public class ConfigurationData
    {
        protected string id;
        protected KeySafeDictionary<string, string> configData = new KeySafeDictionary<string, string>();
        protected List<string> maskedFields = new List<string>();
        protected KeySafeDictionary<string, int> integerCache = new KeySafeDictionary<string, int>();
        protected KeySafeDictionary<string, bool> boolCache = new KeySafeDictionary<string, bool>();

        protected CommModule module = CommModule.Unknown;

        protected KeySafeDictionary<string, string> defaultValues = new KeySafeDictionary<string, string>();
        protected List<string> usedFields = new List<string>();
        public List<string> ModifiedFields { get; private set; } = new List<string>();
        protected readonly string[] nonModifyFields = new string[] {
            "SubmissionFilePassword",
            "PGPMerchantPrivateKey",
            "PGPMerchantPassPhrase",
            "PGPServerPublicKey",
            "UserName",
            "UserPassword",
            "ProxyUserName",
            "ProxyUserPassword",
            "RSAMerchantPassPhrase",
            "RSAMerchantPrivateKey",
            "PID",
            "HostProcessingSystem"
        };


        public ConfigurationData()
        {
        }

        public ConfigurationData( XmlNode node )
        {
            Parse( node );
        }

        public ConfigurationData( ConfigurationData copy )
        {
            this.configData = new KeySafeDictionary<string, string>( copy.configData );
            this.maskedFields.AddRange( copy.maskedFields );
            this.integerCache = new KeySafeDictionary<string, int>( copy.integerCache );
            this.boolCache = new KeySafeDictionary<string, bool>( copy.boolCache );
            this.module = copy.module;
            this.ConfigName = copy.ConfigName;
            this.defaultValues = new KeySafeDictionary<string, string>( copy.defaultValues );
            this.ModifiedFields.AddRange( copy.ModifiedFields );
            this.MessageFormat = copy.MessageFormat;
        }

        public ConfigurationData( KeySafeDictionary<string, string> pairs )
        {
            Parse( pairs );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        public void Parse( XmlNode node )
        {
            Parse( node, new string[ 0 ] );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fieldNames"></param>
        protected void Parse( XmlNode node, string[] fieldNames )
        {
            configData = new KeySafeDictionary<string, string>();
            maskedFields = new List<string>();

            if ( node == null )
            {
                return;
            }

            id = node.Name;

            var protocol = Utils.GetAttributeValue( "protocol", null, node );
            if ( protocol != null )
            {
                module = (CommModule) Enum.Parse( typeof( CommModule ), protocol );
            }

            MessageFormat = SDKUtils.GetMessageFormat( node, protocol );


            ParseChildren( node );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pairs"></param>
        protected void Parse( KeySafeDictionary<string, string> pairs )
        {
            configData = new KeySafeDictionary<string, string>();
            maskedFields = new List<string>();

            foreach ( var key in pairs.Keys )
            {
                if ( configData.ContainsKey( key ) )
                {
                    configData[ key ] = pairs[ key ];
                }
                else
                {
                    configData.Add( key, pairs[ key ] );
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool IsMaskedField( string fieldName )
        {
            return maskedFields.Contains( fieldName );
        }

        /// <summary>
        /// Gets a list of fields that can be masked.
        /// </summary>
        public List<string> MaskedFields => maskedFields;

        /// <summary>
        /// This is a reentrant method that searches the node and all
        /// child nodes for the named properties.
        /// </summary>
        /// <param name="node">The node to search.</param>
        private void ParseChildren( XmlNode node )
        {
            var children = node.ChildNodes;
            for ( var i = 0; i < children.Count; i++ )
            {
                if ( children[ i ].NodeType != XmlNodeType.Element )
                {
                    continue;
                }

                var name = children[ i ].Name;
                var textNode = Utils.GetTextNode( children[ i ] );
                if ( textNode != null )
                {
                    if ( !configData.ContainsKey( name ) )
                    {
                        configData.Add( name, null );
                    }
                    var value = textNode.Value;
                    if ( IsEmpty( configData[ name ] ) || !IsEmpty( value ) )
                        configData[ name ] = value;
                    var doMask = Utils.GetAttributeValue( "mask", "false", children[ i ] ).ToLower();
                    if ( doMask == "true" )
                    {
                        maskedFields.Add( name );
                    }
                }
                ParseChildren( children[ i ] );
            }
        }

        public CommModule Protocol
        {
            get => module;
            set => module = value;
        }

        public string MessageFormat { get; set; }

        public string ConfigName { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[ string name ]
        {
            get
            {
                if ( name != null && !usedFields.Contains( name ) )
                {
                    usedFields.Add( name );
                }

                var text = configData[ name ];
                if ( text != null && text.Trim().Length == 0 )
                {
                    return null;
                }

                return text;
            }
            set => SetField( name, value, true );
        }

        public void SetField( string name, string value )
        {
            SetField( name, value, true );
        }


        public void SetField( string name, string value, bool setModified )
        {
            var prevValue = configData[ name ];

            if ( setModified && !( value == null && prevValue == null ) )
            {
                var skip = false;
                if ( value != null && prevValue != null && prevValue.Equals(value ) )
                {
                    skip = true;
                }
                else
                {
                    foreach ( var field in nonModifyFields )
                    {
                        if ( name.Equals( field ) )
                        {
                            skip = true;
                            break;
                        }
                    }
                }

                if ( !skip )
                {
                    ModifiedFields.Add( name );
                }
            }

            if ( value != null && ( value.ToLower() == "true" || value.ToLower() == "false" ) )
            {
                boolCache[ name ] = Utils.StringToBoolean( value );
            }
            if ( value != null && Utils.IsNumeric( value ) )
            {
                integerCache[ name ] = Utils.StringToInt( value );
            }

            configData[ name ] = value ;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o )
        {
            if ( !(o is ConfigurationData) )
            {
                return false;
            }

            var compare = (ConfigurationData) o;

            var oneIsNull = false;
            if ( compare.ConfigName == null || ConfigName == null )
            {
                oneIsNull = true;
                if ( compare.ConfigName != null || ConfigName != null )
                {
                    return false;
                }
            }

            if ( compare != this )
            {
                if ( !oneIsNull && ! compare.ConfigName.Equals( ConfigName ) )
                {
                    return false;
                }

                if ( compare.module != module )
                {
                    return false;
                }
            }

            if ( ModifiedFields.Count != compare.ModifiedFields.Count )
            {
                return false;
            }

            var combinedModified = new List<string>( this.ModifiedFields );

            combinedModified.AddRange( compare.ModifiedFields );
            string cmp;
            string own;
            foreach ( var name in combinedModified )
            {
                own = this[ name ];
                cmp = compare[ name ];

                if ( cmp == null && own != null )
                {
                    return false;
                }

                if ( cmp != null && own == null )
                {
                    return false;
                }

                if ( cmp != null && ! cmp.Equals( own ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsEmpty( string value )
        {
            return value == null || value.Trim().Length == 0;
        }

        protected bool StringToBoolDefault( string sval, bool dval )
        {
            var retVal = dval;

            if ( sval != null &&
                (sval.ToLower().Equals( "true" ) ||
                    sval.ToLower().Equals( "false" )) )
            {
                retVal = Utils.StringToBoolean( sval );
            }

            return retVal;
        }

        protected int StringToIntegerDefault( string sval, int dval )
        {
            var retVal = dval;

            if ( sval != null )
            {
                retVal = Utils.StringToInt( sval );
            }
            return retVal;
        }

        protected string StringToStringDefault( string sval, string dval )
        {
            var retVal = sval;

            if ( sval == null )
            {
                retVal = dval;
            }

            return retVal;
        }
        public int GetInteger( string name, int defaultValue )
        {
            int retVal;


            // first see if this is the first time this method was called
            if ( !integerCache.ContainsKey( name ) )
            {
                retVal = StringToIntegerDefault( this[ name ], defaultValue );

                // save it so we don't do a string to int conversion every time
                integerCache[ name ] = retVal;
            }
            else
            {
                retVal = integerCache[ name ];
            }

            return retVal;
        }

        public bool GetBool( string name, bool defaultValue )
        {
            bool retVal;

            // first see if this is the first time this method was called
            if ( !boolCache.ContainsKey( name ) )
            {
                retVal = StringToBoolDefault( this[ name ], defaultValue );

                // save it so we don't do a string to int conversion every time
                boolCache[ name ] = retVal;
            }
            else
            {
                retVal = boolCache[ name ];
            }

            return retVal;
        }

        public string GetField( string name )
        {
            return this[ name ];
        }

        public string GetString( string name, string defaultValue )
        {
            return StringToStringDefault( this[ name ], defaultValue );
        }

        public string[] StringToStringArray( string rawString )
        {
            string[] splitStrings = null;
            string[] result = null;

            if ( rawString != null )
            {
                // replace commas and quotes with whitespace so can key off of white only
                var noCommas = rawString.Replace( ',', ' ' );
                var noDoubleQuotes = noCommas.Replace( '"', ' ' );
                var cleanString = noDoubleQuotes.Replace( '\'', ' ' );
                splitStrings = cleanString.Split( new char[] {' ','\t','\r','\n','\f','\v'}, StringSplitOptions.RemoveEmptyEntries ); // split on white space
            }

            // if either rawString or result of split is null then make empty array
            if ( splitStrings == null )
            {
                splitStrings = new string[ 0 ];
            }

            // see if there was leading white space (normally there is)
            if ( splitStrings.Length > 1 && splitStrings[ 0 ].Equals( "" ) ) // this happens when leading white space
            {
                result = new string[ splitStrings.Length - 1 ];

                for ( var i=0; i < result.Length; i++ )
                {
                    result[ i ] = splitStrings[ i + 1 ];
                }
            }
            else
            {
                result = splitStrings;
            }

            return result;
        }

        public string DumpFieldValues()
        {
            return DumpFieldValues( true );
        }

        public string DumpFieldValues( bool masked )
        {
            var buff = new StringBuilder();

            foreach ( var key in this.configData.Keys )
            {
                buff.Append( key );
                buff.Append( " = " );
                if ( masked && ( maskedFields.Contains( key ) || key.ToLower().Contains( "pass" ) ) )
                {
                    buff.Append( "########" );
                }
                else
                {
                    buff.Append( configData[ key ] );
                }
                buff.Append( Environment.NewLine );
            }

            return buff.ToString();
        }


    }
}
