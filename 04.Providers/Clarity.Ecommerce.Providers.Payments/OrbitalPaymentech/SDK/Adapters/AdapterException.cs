#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace JPMC.MSDK.Adapters
{
    public class AdapterException : Exception
    {
        public AdapterException() { }

        public AdapterException(string message)
            : base(message)
        {
        }

        public AdapterException(Exception cause)
            : base(cause.Message, cause)
        {
        }

        public AdapterException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
