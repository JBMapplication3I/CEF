#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Converter
{
    public class FieldData
    {
        public FieldData( string name, bool required, bool masked )
        {
            this.name = name;
            this.required = required;
            this.masked = masked;
        }

        public string name;
        public bool required;
        public bool masked;
    }
}
