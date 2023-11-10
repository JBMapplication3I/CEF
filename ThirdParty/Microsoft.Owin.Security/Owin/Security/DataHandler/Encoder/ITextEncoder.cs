// <copyright file="ITextEncoder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITextEncoder interface</summary>
namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    /// <summary>Interface for text encoder.</summary>
    public interface ITextEncoder
    {
        /// <summary>Decodes.</summary>
        /// <param name="text">The text.</param>
        /// <returns>A byte[].</returns>
        byte[] Decode(string text);

        /// <summary>Encodes the given data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        string Encode(byte[] data);
    }
}
