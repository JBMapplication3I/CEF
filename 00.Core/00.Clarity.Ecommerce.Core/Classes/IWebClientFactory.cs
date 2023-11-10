// <copyright file="IWebClientFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWebClientFactory interface</summary>
namespace System.Net
{
    /// <summary>Interface for web client factory.</summary>
    public interface IWebClientFactory
    {
        /// <summary>Creates a new IWebClient.</summary>
        /// <returns>An IWebClient.</returns>
        IWebClient Create();
    }
}
