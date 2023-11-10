#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Configurator
{
    public class FieldMasks
    {
        private KeySafeDictionary<string, List<Mask>> masks = new KeySafeDictionary<string, List<Mask>>();

        public FieldMasks( XmlElement documentNode )
        {
            var maskNodes = documentNode.GetElementsByTagName( "TransactionType" );

            for ( var i = 0; i < maskNodes.Count; i++ )
            {
                var node = maskNodes.Item( i );
                var name = Utils.GetAttributeValue( "name", null, node );
                if ( name == null )
                {
                    continue;
                }

                var list = new List<Mask>();

                var includeNodes = ((XmlElement) node).GetElementsByTagName( "Include" );
                for ( var j = 0; j < includeNodes.Count; j++ )
                {
                    var child = includeNodes.Item( j );
                    var include = Utils.GetAttributeValue( "name", null, child );
                    if ( masks.ContainsKey( include ) )
                    {
                        list.AddRange( masks[ include ] );
                    }
                }

                var fieldNodes = ( (XmlElement) node ).GetElementsByTagName( "Field" );
                for ( var j = 0; j < fieldNodes.Count; j++ )
                {
                    var child = fieldNodes.Item( j );
                    list.Add( ParseMask( child ) );
                }

                masks[ name ] = list;
            }
        }

        private Mask ParseMask( XmlNode node )
        {
            var name = Utils.GetAttributeValue( "name", null, node );
            var justification = Utils.GetAttributeValue( "justification", "None", node );
            justification = justification.Substring( 0, 1 ).ToUpper() + justification.Substring( 1 ).ToLower();
            var expose = Utils.StringToInt( Utils.GetAttributeValue( "expose", "0", node ) );
            var mask = new Mask();
            mask.name = name;
            mask.justification = (TextJustification) Enum.Parse( typeof( TextJustification ), justification );
            mask.expose = expose;

            return mask;
        }

        public List<Mask> GetMasks( string transactionType )
        {
            if ( !masks.ContainsKey( transactionType ) )
            {
                return new List<Mask>();
            }

            return masks[ transactionType ];
        }

        public string MaskValue( string name, string value, string transactionType )
        {
            if ( value == null || value.Length == 0)
            {
                return value;
            }

            var masks = GetMasks( transactionType );

            foreach ( var mask in masks )
            {
                if ( name == mask.name )
                {
                    string result = null;
                    switch ( mask.justification )
                    {
                        case TextJustification.None:
                        case TextJustification.Left:
                            result = MaskLeft( value, '#', value.Length - mask.expose );
                            break;
                        case TextJustification.Right:
                            result = MaskRight( value, '#', mask.expose );
                            break;
                    }
                    return result;
                }
            }

            return value;
        }

        public static string MaskLeft( string source, char padWith, int length )
        {
            var safeSource = "";
            StringBuilder buffer = null;
            if ( source != null )
            {
                safeSource = source;
            }
            buffer = new StringBuilder( safeSource );
            for ( var i = 0; i < length; i++ )
            {
                buffer[ i ] = padWith;
            }
            return buffer.ToString();
        }

        public static string MaskRight( string source, char padWith, int expose )
        {
            var safeSource = "";
            StringBuilder buffer = null;
            if ( source != null )
            {
                safeSource = source;
            }
            buffer = new StringBuilder( safeSource );
            var len = safeSource.Length;
            for ( var i = expose; i < len; i++ )
            {
                buffer[ i ] = padWith;
            }
            return buffer.ToString();
        }
    }
}