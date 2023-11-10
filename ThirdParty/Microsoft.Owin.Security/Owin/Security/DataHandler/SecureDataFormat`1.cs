// <copyright file="SecureDataFormat`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the secure data format` 1 class</summary>
namespace Microsoft.Owin.Security.DataHandler
{
    using DataProtection;
    using Encoder;
    using Serializer;

    /// <summary>A secure data format.</summary>
    /// <typeparam name="TData">Type of the data.</typeparam>
    /// <seealso cref="ISecureDataFormat{TData}"/>
    /// <seealso cref="ISecureDataFormat{TData}"/>
    public class SecureDataFormat<TData> : ISecureDataFormat<TData>
    {
        /// <summary>The encoder.</summary>
        private readonly ITextEncoder _encoder;

        /// <summary>The protector.</summary>
        private readonly IDataProtector _protector;

        /// <summary>The serializer.</summary>
        private readonly IDataSerializer<TData> _serializer;

        /// <summary>Initializes a new instance of the <see cref="SecureDataFormat{TData}"/> class.</summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="protector"> The protector.</param>
        /// <param name="encoder">   The encoder.</param>
        public SecureDataFormat(IDataSerializer<TData> serializer, IDataProtector protector, ITextEncoder encoder)
        {
            _serializer = serializer;
            _protector = protector;
            _encoder = encoder;
        }

        /// <summary>Protects the given data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        public string Protect(TData data)
        {
            var numArray = _serializer.Serialize(data);
            var numArray1 = _protector.Protect(numArray);
            return _encoder.Encode(numArray1);
        }

        /// <summary>Unprotects.</summary>
        /// <param name="protectedText">The protected text.</param>
        /// <returns>A TData.</returns>
        public TData Unprotect(string protectedText)
        {
            try
            {
                if (protectedText == null)
                {
                    return default;
                }
                var numArray = _encoder.Decode(protectedText);
                if (numArray == null)
                {
                    return default;
                }
                var numArray1 = _protector.Unprotect(numArray);
                return numArray1 != null ? _serializer.Deserialize(numArray1) : default;
            }
            catch
            {
                return default;
            }
        }
    }
}
