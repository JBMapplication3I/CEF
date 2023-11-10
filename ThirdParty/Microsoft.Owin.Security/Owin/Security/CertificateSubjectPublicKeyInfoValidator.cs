// <copyright file="CertificateSubjectPublicKeyInfoValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the certificate subject public key information validator class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net.Security;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using Win32;

    /// <summary>Implements a cert pinning validator passed on
    /// http://datatracker.ietf.org/doc/draft-ietf-websec-key-pinning/?include_text=1.</summary>
    /// <seealso cref="Microsoft.Owin.Security.ICertificateValidator"/>
    /// <seealso cref="ICertificateValidator"/>
    public class CertificateSubjectPublicKeyInfoValidator : ICertificateValidator
    {
        /// <summary>The algorithm.</summary>
        private readonly SubjectPublicKeyInfoAlgorithm _algorithm;

        /// <summary>The valid base 64 encoded subject public key information hashes.</summary>
        private readonly HashSet<string> _validBase64EncodedSubjectPublicKeyInfoHashes;

        /// <summary>Initializes a new instance of the
        /// <see cref="Microsoft.Owin.Security.CertificateSubjectPublicKeyInfoValidator" /> class.</summary>
        /// <param name="validBase64EncodedSubjectPublicKeyInfoHashes">A collection of valid base64 encoded hashes of
        ///                                                            the certificate public key information blob.</param>
        /// <param name="algorithm">                                   The algorithm used to generate the hashes.</param>
        public CertificateSubjectPublicKeyInfoValidator(
            IEnumerable<string> validBase64EncodedSubjectPublicKeyInfoHashes,
            SubjectPublicKeyInfoAlgorithm algorithm)
        {
            if (validBase64EncodedSubjectPublicKeyInfoHashes == null)
            {
                throw new ArgumentNullException(nameof(validBase64EncodedSubjectPublicKeyInfoHashes));
            }
            _validBase64EncodedSubjectPublicKeyInfoHashes =
                new HashSet<string>(validBase64EncodedSubjectPublicKeyInfoHashes);
            if (_validBase64EncodedSubjectPublicKeyInfoHashes.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validBase64EncodedSubjectPublicKeyInfoHashes));
            }
            if (_algorithm != SubjectPublicKeyInfoAlgorithm.Sha1 && _algorithm != SubjectPublicKeyInfoAlgorithm.Sha256)
            {
                throw new ArgumentOutOfRangeException(nameof(algorithm));
            }
            _algorithm = algorithm;
        }

        /// <summary>Validates at least one SPKI hash is known.</summary>
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
            using var hashAlgorithm = CreateHashAlgorithm();
            var enumerator = chain.ChainElements.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var x509Certificate2 = enumerator.Current.Certificate;
                var base64String = Convert.ToBase64String(hashAlgorithm.ComputeHash(ExtractSpkiBlob(x509Certificate2)));
                if (!_validBase64EncodedSubjectPublicKeyInfoHashes.Contains(base64String))
                {
                    continue;
                }
                return true;
            }
            return false;
        }

        /// <summary>Extracts the spki BLOB described by certificate.</summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>The extracted spki BLOB.</returns>
        private static byte[] ExtractSpkiBlob(X509Certificate2 certificate)
        {
            var subjectPublicKeyInfo = ((NativeMethods.CERT_INFO)Marshal.PtrToStructure(
                ((NativeMethods.CERT_CONTEXT)Marshal.PtrToStructure(
                    certificate.Handle,
                    typeof(NativeMethods.CERT_CONTEXT))).pCertInfo,
                typeof(NativeMethods.CERT_INFO))).SubjectPublicKeyInfo;
            uint num = 0;
            var intPtr = new IntPtr(8);
            if (!NativeMethods.CryptEncodeObject(1, intPtr, ref subjectPublicKeyInfo, null, ref num))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            var numArray = new byte[num];
            if (!NativeMethods.CryptEncodeObject(1, intPtr, ref subjectPublicKeyInfo, numArray, ref num))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return numArray;
        }

        /// <summary>Creates hash algorithm.</summary>
        /// <returns>The new hash algorithm.</returns>
        private HashAlgorithm CreateHashAlgorithm()
        {
            if (_algorithm != SubjectPublicKeyInfoAlgorithm.Sha1)
            {
                return new SHA256CryptoServiceProvider();
            }
            return new SHA1CryptoServiceProvider();
        }
    }
}
