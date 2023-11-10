// <copyright file="CertificateSubjectKeyIdentifierValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the certificate subject key identifier validator class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>Provides pinned certificate validation based on the subject key identifier of the certificate.</summary>
    /// <seealso cref="ICertificateValidator"/>
    /// <seealso cref="ICertificateValidator"/>
    public class CertificateSubjectKeyIdentifierValidator : ICertificateValidator
    {
        /// <summary>List of identifiers for the valid subject keys.</summary>
        private readonly HashSet<string> _validSubjectKeyIdentifiers;

        /// <summary>Initializes a new instance of the
        /// <see cref="CertificateSubjectKeyIdentifierValidator" /> class.</summary>
        /// <param name="validSubjectKeyIdentifiers">A set of subject key identifiers which are valid for an HTTPS
        ///                                          request.</param>
        public CertificateSubjectKeyIdentifierValidator(IEnumerable<string> validSubjectKeyIdentifiers)
        {
            if (validSubjectKeyIdentifiers == null)
            {
                throw new ArgumentNullException(nameof(validSubjectKeyIdentifiers));
            }
            _validSubjectKeyIdentifiers = new HashSet<string>(
                validSubjectKeyIdentifiers,
                StringComparer.OrdinalIgnoreCase);
            if (_validSubjectKeyIdentifiers.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validSubjectKeyIdentifiers));
            }
        }

        /// <summary>Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.</summary>
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
                var subjectKeyIdentifier = GetSubjectKeyIdentifier(enumerator.Current.Certificate);
                if (string.IsNullOrWhiteSpace(subjectKeyIdentifier)
                    || !_validSubjectKeyIdentifiers.Contains(subjectKeyIdentifier))
                {
                    continue;
                }
                return true;
            }
            return false;
        }

        /// <summary>Gets subject key identifier.</summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>The subject key identifier.</returns>
        private static string GetSubjectKeyIdentifier(X509Certificate2 certificate)
        {
            if (certificate.Extensions["2.5.29.14"] is not X509SubjectKeyIdentifierExtension item)
            {
                return null;
            }
            return item.SubjectKeyIdentifier;
        }
    }
}
