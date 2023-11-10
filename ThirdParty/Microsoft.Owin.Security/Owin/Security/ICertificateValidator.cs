// <copyright file="ICertificateValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICertificateValidator interface</summary>
namespace Microsoft.Owin.Security
{
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>Interface for providing pinned certificate validation, which checks HTTPS communication against a
    /// known good list of certificates to protect against compromised or rogue CAs issuing certificates for hosts
    /// without the knowledge of the host owner.</summary>
    public interface ICertificateValidator
    {
        /// <summary>Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.</summary>
        /// <param name="sender">         An object that contains state information for this validation.</param>
        /// <param name="certificate">    The certificate used to authenticate the remote party.</param>
        /// <param name="chain">          The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>A Boolean value that determines whether the specified certificate is accepted for authentication.</returns>
        bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
    }
}
