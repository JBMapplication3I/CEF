// <copyright file="BaseValidatingClientContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base validating client context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Base class used for certain event contexts.</summary>
    /// <seealso cref="BaseValidatingContext{OAuthAuthorizationServerOptions}"/>
    public abstract class BaseValidatingClientContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes base class used for certain event contexts.</summary>
        /// <param name="context"> The context.</param>
        /// <param name="options"> Options for controlling the operation.</param>
        /// <param name="clientId">Identifier for the client.</param>
        protected BaseValidatingClientContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId) : base(context, options)
        {
            ClientId = clientId;
        }

        /// <summary>The "client_id" parameter for the current request. The Authorization Server application is
        /// responsible for validating this value identifies a registered client.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId
        {
            get;
            protected set;
        }
    }
}
