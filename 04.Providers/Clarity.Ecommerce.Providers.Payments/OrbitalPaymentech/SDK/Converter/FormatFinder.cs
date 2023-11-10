#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Converter
{
    public class FormatFinder
    {
        private string type; // If, Contains, etc.
        private int position = -1;
        private int length = -1;
        private string contains;
        private string value;
        private int fieldCount = -1;
        private string format;
        private string respType;
        public string FieldDelimiter { get; set; }
        public string FormatName { get; private set; }
        public string ResponseType { get; private set; }
        private List<FormatFinder> finders = new List<FormatFinder>();
        public ResponseTemplate Template { get; set; }

        public FormatFinder( XmlNode node, string delim, ResponseTemplate template )
        {
            Template = template;
            FieldDelimiter = delim;
            type = node.Name;
            position = Utils.StringToInt( Utils.GetAttributeValue( "position", "-1", node ) );
            length = Utils.StringToInt( Utils.GetAttributeValue( "length", "-1", node ) );
            contains = Utils.GetAttributeValue( "contains", null, node );
            value = Utils.GetAttributeValue( "value", null, node );
            fieldCount = Utils.StringToInt( Utils.GetAttributeValue( "field_count", "-1", node ) );
            format = Utils.GetAttributeValue( "format", null, node );
            respType = Utils.GetAttributeValue( "response_type", null, node );

            foreach ( XmlNode child in node.ChildNodes )
            {
                finders.Add( new FormatFinder( child, FieldDelimiter, this.Template ) );
            }
        }

        public bool FindFormat( string data, string methodOfPayment )
        {
            switch ( type )
            {
                case "If":
                    return ResolveIf( data, methodOfPayment );
                case "Contains":
                    return ResolveContains( data );
                case "Mop":
                    return ResolveMOP( data, methodOfPayment );
                case "Default":
                    return ResolveDefault();
                default:
                    var msg = $"Format finder failed. The type \"{type}\" is invalid.";
                    throw new ConverterException( Error.TemplateLibraryMismatch, msg );
            }
        }

        private bool ResolveIf( string data, string methodOfPayment )
        {
            if ( position > -1 )
            {
                return ResolvePositionIf( data, methodOfPayment );
            }

            if ( fieldCount > -1 )
            {
                return ResolveFieldCount( data );
            }

            var msg = "Format finder failed. An invalid If statement was found.";
            throw new ConverterException( Error.TemplateLibraryMismatch, msg );
        }

        private bool ResolveFieldCount( string data )
        {
            FieldDelimiter = FindDelimiter( data );
            if ( FieldDelimiter == null )
            {
                var msg = "If statement failed: The field delimiter was not specified.";
                throw new ConverterException( Error.BadDataError, msg );
            }

            var count = 0;
            if ( data.Contains( FieldDelimiter ) )
            {
                count = data.Split( FieldDelimiter[ 0 ] ).Length;
            }

            if ( fieldCount != count )
            {
                return false;
            }

            return ProcessChildFinders( data, null );
        }

        private string FindDelimiter( string data )
        {
            if ( FieldDelimiter != null || Template == null )
            {
                return FieldDelimiter;
            }

            for ( var i = 0; i < Template.DFRDelimiter.Length; i++ )
            {
                if ( data.Contains( Template.DFRDelimiter[ i ] ) )
                {
                    FieldDelimiter = new string( Template.DFRDelimiter[ i ], 1 );
                    break;
                }
            }

            return FieldDelimiter;
        }

        private bool ProcessChildFinders( string data, string methodOfPayment )
        {
            foreach ( var finder in finders )
            {
                finder.Template = Template;
                finder.FieldDelimiter = FieldDelimiter;

                if ( finder.FindFormat( data, methodOfPayment ) )
                {
                    FormatName = finder.FormatName;
                    ResponseType = finder.ResponseType;
                    return true;
                }
            }

            return false;
        }

        private bool ResolveContains( string data )
        {
            if ( data == value )
            {
                FormatName = format;
                ResponseType = respType;
                return true;
            }

            return false;
        }

        private bool ResolveDefault()
        {
            FormatName = format;
            ResponseType = respType;
            return true;
        }

        private bool ResolvePositionIf( string data, string methodOfPayment )
        {
            if ( position + length >= data.Length )
            {
                var msg =
                    $"Not enough data to process a position-based If statement. Data Length={data.Length}, Position={position}, Length={length}";
                throw new ConverterException( Error.BadDataError, msg );
            }
            var val = data.Substring( position, length );

            if ( contains != null )
            {
                if ( contains == val && format != null )
                {
                    FormatName = format;
                    ResponseType = respType;
                    return true;
                }
                else if ( contains != val )
                {
                    return false;
                }

                // We already resolved the "contains" and we have truth.
                // Any child finders will be if's that require the full data.
                val = data;
            }

            return ProcessChildFinders( val, methodOfPayment );
        }

        private bool ResolveMOP( string data, string methodOfPayment )
        {
            if ( methodOfPayment != null && methodOfPayment == value )
            {
                FormatName = this.format;
                ResponseType = respType;
                return true;
            }

            var order = Template.GetOrder();
            var format = Template.GetFormat( order.FormatName );
            if ( format == null )
            {
                var msg = "Format Finder failed. The Order's Format could not be found";
                throw new ConverterException( Error.BadDataError, msg );
            }
            var mopField = format.GetField( order.MopFieldName );
            if ( mopField == null )
            {
                var msg = "Format Finder failed. The Method of Payment Field could not be found";
                throw new ConverterException( Error.BadDataError, msg );
            }

            var increment = Template.DefaultFormatLength + 1;

            string mop = null;
            for ( var pos = increment; pos < data.Length; pos += increment )
            {
                var chkFormat = data.Substring( pos, order.FormatName.Length );
                if ( chkFormat == order.FormatName )
                {
                    // Position is 1-based.
                    mop = data.Substring( pos + mopField.Position - 1, mopField.Length );
                    break;
                }
            }

            if ( mop != null && mop == value )
            {
                FormatName = this.format;
                ResponseType = respType;
                return true;
            }

            return false;
        }

    }
}
