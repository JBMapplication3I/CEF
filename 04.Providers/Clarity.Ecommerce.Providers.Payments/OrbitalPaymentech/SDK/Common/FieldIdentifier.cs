#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace JPMC.MSDK.Common
{
    public class FieldIdentifier
    {
        private string path;
        private static string separator = ".";

        public FieldIdentifier()
        {

        }

        public FieldIdentifier( string path )
        {
            Set( path );
        }

        public void Set( string path )
        {
            this.path = Normalize( path );
        }

        public new string ToString()
        {
            return path;
        }

        public static string Separator => separator;

        public static string Combine( params string[] parts )
        {
            var retVal = "";
            for ( var i = 0; i < parts.Length; i++ )
            {
                if ( parts[i] == null )
                {
                    parts[i] = "";
                }

                if ( i == 0 )
                {
                    retVal = parts[ i ];
                }
                else
                {
                    if ( retVal.EndsWith( separator ) || parts[ i ].StartsWith( separator ) )
                    {
                        retVal += parts[ i ];
                    }
                    else
                    {
                        retVal += separator + parts[ i ];
                    }
                }
            }

            return Normalize( retVal );
        }

        public static string[] Split( string path )
        {
            return path.Split( '.' );
        }

        public string Name => GetName( path );

        public static string GetName( string path )
        {
            path = Normalize( path );

            if ( !path.Contains( separator ) )
            {
                return path;
            }
            return path.Substring( path.LastIndexOf( separator ) + 1 );
        }

        public string FormatPath => GetFormatPath( path );

        public static string GetFormatPath( string path )
        {
            if ( path.LastIndexOf( separator ) < 0 )
            {
                return "";
            }

            return path.Substring( 0, path.LastIndexOf( separator ) );
        }

        public static string Normalize( string path )
        {
            while ( path.EndsWith( separator ) )
            {
                path = path.Substring( 0, path.Length - 1 );
            }

            if ( path.Contains( "/" ) )
            {
                throw new FieldIdentifierException( Error.InvalidField, "The field identifier contains slashes \"/\" as separators. Only periods are allowed as separators." );
            }

            while (path.StartsWith(separator))
            {
                path = path.Substring( 1 );
            }

            path = path.Replace( separator + separator, separator );

            return path;
        }

        /// <summary>
        /// Gets the index of the array, if any. If this is not an array, an
        /// index of -1 is returned.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int GetArrayIndex(string field )
        {
            if ( !field.Contains( "[" ) )
            {
                return -1;
            }

            var pos = field.IndexOf( "[" ) + 1;
            var len = field.IndexOf( "]" ) - pos;
            var num = field.Substring( pos, len );
            return Convert.ToInt32( num );
        }
    }
}