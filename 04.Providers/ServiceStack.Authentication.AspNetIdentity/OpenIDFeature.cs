// <copyright file="OpenIDFeature.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the open identifier service class</summary>
namespace ServiceStack.Authentication.OpenID
{
    using JetBrains.Annotations;

    /// <inheritdoc/>
    [PublicAPI]
    public class OpenIDFeature : IPlugin
    {
        /// <summary>Registers this object.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
