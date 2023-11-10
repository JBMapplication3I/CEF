#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Collections.Generic;
using JPMC.MSDK.Common;


// (c)2017, Paymentech, LLC. All rights reserved

namespace JPMC.MSDK.Converter
{
    public class Format
    {
        public Field previousField = null;
        public Format Parent { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        private string alias;
        public int Length { get; set; }
        public FormatType FormatType { get; set; }
        public BitMapType BitMapType { get; set; }
        public int PrefixLength { get; set; }
        public string PrefixData { get; set; }
        //	private int countSize = -1;
        public int Bit { get; set; }
        public string ResponseType { get; set; }
        public string DelimitedResponseType { get; set; }
        public bool SkipWrite { get; set; }
        public string TCPFileTerminator { get; set; }
        public string SFTPFileTerminator { get; set; }
        public string FormatName { get; set; }
        private string prefixFormatName;
        public List<Field> Fields { get; set; }
        public string TCPRecordDelimiter { get; set; }
        public string SFTPRecordDelimiter { get; set; }
        private string fieldSeparator;
        //	private string arrayFormatName = null;
        public Format ArrayFormat { get; set; }
        //	private string redirectTo = null;
        public int DataLength { get; set; }
        public bool HideFromFieldID { get; set; }
        private string[] aliases = new string[ 0 ];
        public bool IsRootFormat { get; set; }
        private List<string> formatRefs = new List<string>();
        public bool IgnoreShort { get; set; }
        public int Max { get; set; }
        public string CountField { get; set; }
        public string FormatIndicator { get; set; }
        public string FormatID { get; set; }
        public bool IsArray { get; set; }
        public bool IsPrefixFormat { get; set; }

        public List<Format> PrefixFormats { get; }


        // Because LinkedHashMap is not maintaining the order, like it should,
        // formats is used for quick retrieval, while formatList is used
        // for iterating.
        protected OrderedDictionary<string, Format> formats = new OrderedDictionary<string, Format>();
        //public List<Format> Formats { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Format()
        {
            Path = "";
            Max = 1;
            FormatID = "";
            Bit = -1;
            FormatType = FormatType.Static;
            Fields = new List<Field>();
            PrefixFormats = new List<Format>();
            //Formats = new List<Format>();
            Formats = new OrderedDictionary<string, Format>();
            DataLength = -1;
            PrefixLength = -1;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy"></param>
        public Format( Format cpy )
        {
            if ( cpy == null )
            {
                return;
            }

            var copy = (Format) cpy;

            this.alias = copy.alias;
            this.Parent = copy.Parent;
            this.ArrayFormat = copy.ArrayFormat == null ? null : new Format( copy.ArrayFormat );
            this.Bit = copy.Bit;
            this.DelimitedResponseType = copy.DelimitedResponseType;
            this.Fields = copy.Fields;
            this.fieldSeparator = copy.fieldSeparator;
            this.FormatName = copy.FormatName;
            this.Formats = copy.Formats;
            this.FormatType = copy.FormatType;
            this.Length = copy.Length;
            this.Name = copy.Name;
            this.Path = copy.Path;
            this.PrefixData = copy.PrefixData;
            this.prefixFormatName = copy.prefixFormatName;
            this.PrefixLength = copy.PrefixLength;
            this.ResponseType = copy.ResponseType;
            this.SFTPFileTerminator = copy.SFTPFileTerminator;
            this.SFTPRecordDelimiter = copy.SFTPRecordDelimiter;
            this.SkipWrite = copy.SkipWrite;
            this.TCPFileTerminator = copy.TCPFileTerminator;
            this.TCPRecordDelimiter = copy.TCPRecordDelimiter;
            this.IsRootFormat = copy.IsRootFormat;
            this.Max = copy.Max;
            this.CountField = copy.CountField;
        }

        /// <summary>
        /// Returns true if the Format object is "empty" or uninitialized.
        /// </summary>
        public bool IsEmpty => Name == null;

        private void IsValid()
        {
            if ( IsEmpty )

            {
                throw new ConverterException( Error.InvalidField,
                        "The Converter Field is empty." );
            }
        }

        /// <summary>
        /// Add a new field to the format.
        /// </summary>
        /// <param name="name">The name of the field being added, for easy
        /// reference.</param>
        /// <param name="field">The Field to add.</param>
        public void AddField( string name, Field field )
        {
             Fields.Add( field );
        }

        /// <summary>
        /// Gets the specified field.
        /// </summary>
        /// <param name="name">The name of the field to get.</param>
        /// <returns></returns>
        public Field GetField( string name )
        {
            var fieldName = name;
            if ( fieldName.Contains( "." ) )

            {
                fieldName = fieldName.Substring( fieldName.LastIndexOf( "." ) + 1 );
            }

            foreach ( var theField in Fields )

            {
                if ( theField.Name == fieldName || MatchesAlias( theField, fieldName ) )
                {
                    return theField;
                }
            }

            var
            parts = name.Split( '.' );
            return FindField( parts );
        }

        private Field FindField( string[] parts )
        {
            var field = new Field();
            var found = false;
            for ( var i = 0; i < parts.Length; i++ )
            {
                if ( !found && parts[ i ] != this.Name )
                {
                    continue;
                }

                found = true;
                if ( parts.Length == i + 1 )
                {
                    return field;
                }

                if ( field.IsEmpty )
                {
                    field = GetField( parts[ i + 1 ] );
                }
                else
                {
                    field = field.GetField( parts[ i + 1 ] );
                }
                if ( field == null || field.IsEmpty )
                {
                    return field;
                }
            }

            return field;
        }

        public Format GetFormat( string key )
        {
            return new Format( Formats[ key ] );
        }

        public Field this[ string key ] => GetField( key );

        public bool HasFormat( string key )
        {
            var child = Formats[ key ];
            return child != null && !child.IsEmpty;
        }


        public int GetFormatCount()
        {
            return Formats.Count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Field GetFirstField()
        {
            if ( Fields.Count == 0 )
            {
                return null;
            }

            return Fields[ 0 ];
        }

        /// <summary>
        /// Get a field by specifying the column that it is :.
        /// </summary>
        /// <remarks>
        /// This iterates through all the fields, to find the first one that
        /// has a PrefixColumnName element that matches the supplied string.
        /// </remarks>
        /// <param name="name">The name of the column to look for.</param>
        /// <returns>The Field found, or null if none was found.</returns>
        public Field GetFieldByColumnName( string name )
        {
            foreach ( var field in Fields )

            {
                if ( field.PrefixColumnName != null
                        && field.PrefixColumnName == name )
                {
                    return field;
                }
                if ( field.SuffixColumnName != null
                        && field.SuffixColumnName == name )
                {
                    return field;
                }
            }
            return null;
        }

        private bool MatchesAlias( Field field, string fieldName )
        {
            if ( field == null || field.Alias == null )

            {
                return false;
            }
            var
            aliases = field.Alias.Split( ',' );
            foreach ( var alias in aliases )

            {
                if ( alias == fieldName )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a child Format based on the data (typically response data).
        /// </summary>
        /// <remarks>
        /// Some child formats (mainly : batch responses) have a variable-
        /// length format indicator. This method will iterate through the
        /// keys and return the format that the given data starts with.
        /// If no format was found, then it will return the "Default"
        /// format, if one exists.
        /// </remarks>
        /// <param name="data"></param>
        /// <returns></returns>
        public Format GetFormatByData( string data )
        {
            foreach ( var key in Formats.Keys )
            {
                if ( data.StartsWith( key ) )
                {
                    return Formats[ key ];
                }
            }

            if ( Formats.ContainsKey( "Default" ) )
            {
                return Formats[ "Default" ];
            }

            return new Format();
        }

        /// <summary>
        /// Gets the File Terminator for the given CommMode.
        /// </summary>
        /// <param name="commMode">The communication mode to get the terminator
        /// for.</param>
        /// <param name="defaultValue">The default value, : case no terminator
        /// exists.</param>
        /// <returns></returns>
        public string GetFileTerminator( CommModule module, string defaultValue )
        {
            string value = null;

            if ( module == CommModule.TCPBatch )
            {
                value = this.TCPFileTerminator;
            }
            else if ( module == CommModule.SFTPBatch )
            {
                value = this.SFTPFileTerminator;
            }

            if ( value == null )
            {
                value = defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Returns the response type.
        /// </summary>
        /// <param name="delimited"></param>
        /// <returns></returns>
        public string GetResponseType( bool delimited )
        {
            return delimited ? this.DelimitedResponseType : this.ResponseType;
        }

        /// <summary>
        /// Returns teh name of the response format.
        /// </summary>
        /// <returns></returns>
        public string GetResponseFormat()
        {
            var resp = this.Name;
            if ( resp == null )
            {
                return Path;
            }
            return resp;
        }


        public string Alias
        {
            get
            {
                IsValid();
                return alias;
            }
            set
            {
                alias = value;
                aliases = new string[] { Name };
                if ( alias != null )
                {
                    aliases = alias.Split( ',' );
                }
                if ( aliases.Length > 1 )
                {
                    alias = aliases[ 0 ];
                }
            }
        }

        public string[] Aliases
        {
            get
            {
                IsValid();
                if ( alias != null && aliases.Length == 0 )

                {
                    aliases = new string[] { alias };
                }
                return aliases;
            }
        }

        public string ArrayFormatName
        {
            get
            {
                IsValid();
                return ArrayFormat.Name;
            }
        }

        public void AddAlias( string newAlias )
        {
            if ( newAlias == null )
            {
                return;
            }
            var list = new List<string>( aliases );
            list.Add( newAlias );
            aliases = list.ToArray();
        }

        public OrderedDictionary<string, Format> Formats
        {
            get => formats;
            set => formats = value;
        }

        public bool IsDelmitedFormat
        {
            get
            {
                if ( this.DelimitedResponseType != null )
                {
                    return true;
                }

                try
                {
                    return this.Parent != null
                        && this.Parent.DelimitedResponseType != null;
                }
                catch ( ConverterException )
                {
                    return false;
                }
            }
        }

        public void AddFormatRef( string name )
        {
            if ( !formatRefs.Contains( name ) )
            {
                formatRefs.Add( name );
            }
        }

        public bool IsFormatRef( string name )
        {
            return formatRefs.Contains( name );
        }

        public bool HasFormatRefs => formatRefs.Count > 0;

        public void AddToFormatID( string name )
        {
            if ( this.FormatID == null || this.FormatID.Length == 0 )
            {
                this.FormatID += name;
            }
            else
            {
                this.FormatID += "." + name;
            }
        }

        public void AddPrefixFormat( Format format )
        {
            PrefixFormats.Add( format );
        }

    }
}