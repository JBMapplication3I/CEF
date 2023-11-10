// <copyright file="NativeMethods.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the native methods class</summary>
#pragma warning disable CS0649
namespace Microsoft.Win32
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

    /// <summary>A native methods.</summary>
    [Localizable(false)]
    internal static class NativeMethods
    {
        /// <summary>The 509 asn encoding.</summary>
        public const int X509_ASN_ENCODING = 1;

        /// <summary>Information describing the 509 public key.</summary>
        public const int X509_PUBLIC_KEY_INFO = 8;

        /// <summary>Encodes a structure of the type indicated by the value of the lpszStructType parameter.</summary>
        /// <param name="dwCertEncodingType">Type of encoding used.</param>
        /// <param name="lpszStructType">    The high-order word is zero, the low-order word specifies the integer
        ///                                  identifier for the type of the specified structure so we can use the
        ///                                  constants in
        ///                                  http://msdn.microsoft.com/en-us/library/windows/desktop/aa378145%28v=vs.85%29.aspx.</param>
        /// <param name="pvStructInfo">      A pointer to the structure to be encoded.</param>
        /// <param name="pbEncoded">         A pointer to a buffer to receive the encoded structure. This parameter can
        ///                                  be NULL to retrieve the size of this information for memory allocation
        ///                                  purposes.</param>
        /// <param name="pcbEncoded">        A pointer to a DWORD variable that contains the size, in bytes, of the
        ///                                  buffer pointed to by the pbEncoded parameter.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = false, SetLastError = true)]
        internal static extern bool CryptEncodeObject(
            uint dwCertEncodingType,
            IntPtr lpszStructType,
            ref CERT_PUBLIC_KEY_INFO pvStructInfo,
            byte[] pbEncoded,
            ref uint pcbEncoded);

        /// <summary>A cert context.</summary>
        internal struct CERT_CONTEXT
        {
            /// <summary>Type of the cert encoding.</summary>
            public int dwCertEncodingType;

            /// <summary>The pb cert encoded.</summary>
            public IntPtr pbCertEncoded;

            /// <summary>The cert encoded.</summary>
            public int cbCertEncoded;

            /// <summary>Information describing the cert.</summary>
            public IntPtr pCertInfo;

            /// <summary>The cert store.</summary>
            public IntPtr hCertStore;
        }

        /// <summary>Information about the cert.</summary>
        internal class CERT_INFO
        {
            /// <summary>The extension.</summary>
            public int cExtension;

            /// <summary>The version.</summary>
            public int dwVersion;

            /// <summary>The issuer.</summary>
            public CRYPT_BLOB Issuer;

            /// <summary>Unique identifier for the issuer.</summary>
            public CRYPT_BIT_BLOB IssuerUniqueId;

            /// <summary>The not after.</summary>
            public FILETIME NotAfter;

            /// <summary>The not before.</summary>
            public FILETIME NotBefore;

            /// <summary>The extension.</summary>
            public IntPtr rgExtension;

            /// <summary>The serial number.</summary>
            public CRYPT_BLOB SerialNumber;

            /// <summary>The signature algorithm.</summary>
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

            /// <summary>The subject.</summary>
            public CRYPT_BLOB Subject;

            /// <summary>Information describing the subject public key.</summary>
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

            /// <summary>Unique identifier for the subject.</summary>
            public CRYPT_BIT_BLOB SubjectUniqueId;
        }

        /// <summary>Information about the cert public key.</summary>
        internal struct CERT_PUBLIC_KEY_INFO
        {
            /// <summary>The algorithm.</summary>
            public CRYPT_ALGORITHM_IDENTIFIER Algorithm;

            /// <summary>The public key.</summary>
            public CRYPT_BIT_BLOB PublicKey;
        }

        /// <summary>A crypt algorithm identifier.</summary>
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            /// <summary>Identifier for the object.</summary>
            public string pszObjId;

            /// <summary>Options for controlling the operation.</summary>
            public CRYPT_BLOB Parameters;
        }

        /// <summary>A crypt bit blob.</summary>
        internal struct CRYPT_BIT_BLOB
        {
            /// <summary>The data.</summary>
            public int cbData;

            /// <summary>Information describing the pb.</summary>
            public IntPtr pbData;

            /// <summary>The unused bits.</summary>
            public int cUnusedBits;
        }

        /// <summary>A crypt blob.</summary>
        internal struct CRYPT_BLOB
        {
            /// <summary>The data.</summary>
            public int cbData;

            /// <summary>Information describing the pb.</summary>
            public IntPtr pbData;
        }
    }
}
