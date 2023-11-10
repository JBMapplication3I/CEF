#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Text;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Framework
{
    public class TransactionControlValues
    {
        private KeySafeDictionary<string, string> values = new KeySafeDictionary<string, string>();

        /// <summary>
        /// Gets the Unique Identifier for the PNS Batch that this Request is for.
        /// </summary>
        /// <returns>A string representing the UID for the batch.</returns>
        public string PNSBatchUID
        {
            get
            {
                if ( values[ "AuthMid" ] != null && values[ "AuthTid" ] != null )
                {
                    return values[ "AuthMid" ] + values[ "AuthTid" ];
                }
                return null;
            }
        }

        public string this[ string name ]
        {
            get => values.ContainsKey( name ) ? values[ name ] : null;
            set => values[ name ] = value;
        }

        public bool GetBoolValue( string name )
        {
            var val = values.ContainsKey( name ) ? values[ name ] : "false";
            return val.ToLower() == "true";
        }

        public string DumpFieldValues()
        {
            var buff = new StringBuilder();

            foreach ( var field in values.Keys )
            {
                var value = values[ field ] != null ? values[ field ] : "";
                var line = field + " = " + value;
                buff.AppendLine( line );
            }

            return buff.ToString();
        }
    }
}
