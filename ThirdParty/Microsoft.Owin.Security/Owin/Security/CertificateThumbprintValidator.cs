// <copyright file="CertificateThumbprintValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the certificate thumbprint validator class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>Provides pinned certificate validation based on the certificate thumbprint.</summary>
    /// <seealso cref="ICertificateValidator"/>
    /// <seealso cref="ICertificateValidator"/>
    public class CertificateThumbprintValidator : ICertificateValidator
    {
        /// <summary>The valid certificate thumbprints.</summary>
        private readonly HashSet<string> _validCertificateThumbprints;

        /// <summary>Initializes a new instance of the
        /// <see cref="CertificateThumbprintValidator" /> class.</summary>
        /// <param name="validThumbprints">A set of thumbprints which are valid for an HTTPS request.</param>
        public CertificateThumbprintValidator(IEnumerable<string> validThumbprints)
        {
            if (validThumbprints == null)
            {
                throw new ArgumentNullException(nameof(validThumbprints));
            }
            _validCertificateThumbprints = new HashSet<string>(validThumbprints, StringComparer.OrdinalIgnoreCase);
            if (_validCertificateThumbprints.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validThumbprints));
            }
        }

        /// <summary>Validates that the certificate thumbprints in the signing chain match at least one whitelisted
        /// thumbprint.</summary>
        /// <param name="sender">         An object that contains state information for this validation.</param>
        /// <param name="certificate">    The certificate used to authenticate the remote party.</param>
        /// <param name="chain">          The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>A Boolean value that determines whether the specified certificate is accepted for authentication.</returns>
        public bool Validate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                return false;
            }
            if (chain == null)
            {
                throw new ArgumentNullException(nameof(chain));
            }
            if (chain.ChainElements.Count < 2)
            {
                return false;
            }
            var enumerator = chain.ChainElements.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var thumbprint = enumerator.Current.Certificate.Thumbprint;
                if (thumbprint == null || !_validCertificateThumbprints.Contains(thumbprint))
                {
                    continue;
                }
                return true;
            }
            return false;
        }
    }
}
