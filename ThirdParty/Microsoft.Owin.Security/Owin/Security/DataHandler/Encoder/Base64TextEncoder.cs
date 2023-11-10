// <copyright file="Base64TextEncoder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base 64 text encoder class</summary>
namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    using System;

    /// <summary>A base 64 text encoder.</summary>
    /// <seealso cref="ITextEncoder"/>
    /// <seealso cref="ITextEncoder"/>
    public class Base64TextEncoder : ITextEncoder
    {
        /// <summary>Decodes.</summary>
        /// <param name="text">The text.</param>
        /// <returns>A byte[].</returns>
        public byte[] Decode(string text)
        {
            return Convert.FromBase64String(text);
        }

        /// <summary>Encodes the given data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        public string Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
