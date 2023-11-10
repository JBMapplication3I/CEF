// <copyright file="TokenEndpointRequestCustomExtension.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token endpoint request custom extension class</summary>
namespace Microsoft.Owin.Security.OAuth.Messages
{
    /// <summary>Data object used by TokenEndpointRequest which contains parameter information when the "grant_type"
    /// is unrecognized.</summary>
    public class TokenEndpointRequestCustomExtension
    {
        /// <summary>The parameter information when the "grant_type" is unrecognized.</summary>
        /// <value>The parameters.</value>
        public IReadableStringCollection Parameters
        {
            get;
            set;
        }
    }
}
