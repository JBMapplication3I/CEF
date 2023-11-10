// <copyright file="TextEncodings.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the text encodings class</summary>
namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    /// <summary>A text encodings.</summary>
    public static class TextEncodings
    {

        /// <summary>Initializes static members of the Microsoft.Owin.Security.DataHandler.Encoder.TextEncodings class.</summary>
        static TextEncodings()
        {
            Base64 = new Base64TextEncoder();
            Base64Url = new Base64UrlTextEncoder();
        }

        /// <summary>Gets the base 64.</summary>
        /// <value>The base 64.</value>
        public static ITextEncoder Base64 { get; private set; }

        /// <summary>Gets URL of the base 64.</summary>
        /// <value>The base 64 URL.</value>
        public static ITextEncoder Base64Url { get; private set; }
    }
}
