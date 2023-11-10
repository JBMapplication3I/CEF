// <copyright file="SystemWebClientFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the system web client factory class</summary>
namespace System.Net
{
    /// <summary>A system web client factory.</summary>
    /// <seealso cref="IWebClientFactory"/>
    public class SystemWebClientFactory : IWebClientFactory
    {
        /// <inheritdoc/>
        public IWebClient Create()
        {
            return new SystemWebClient();
        }
    }
}
