// <copyright file="DpapiDataProtector.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dpapi data protector class</summary>
namespace Microsoft.Owin.Security.DataProtection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>A dpapi data protector.</summary>
    /// <seealso cref="IDataProtector"/>
    /// <seealso cref="IDataProtector"/>
    internal class DpapiDataProtector2 : IDataProtector
    {
        /// <summary>The protector.</summary>
        private readonly IDataProtector _protector;

        /// <summary>Initializes a new instance of the <see cref="DpapiDataProtector2" /> class.</summary>
        /// <param name="appName"> Name of the application.</param>
        /// <param name="purposes">The purposes.</param>
        public DpapiDataProtector2(string appName, params string[] purposes)
        {
            _protector = new DpapiDataProtectorNet472/* System.Security.Cryptography.DpapiDataProtector*/(
                appName,
                "Microsoft.Owin.Security.IDataProtector",
                purposes)
            {
#if _WINDOWS
                Scope = DataProtectionScope.CurrentUser
#endif
            };
        }

        /// <inheritdoc/>
        public byte[] Protect(byte[] userData)
        {
            return _protector.Protect(userData);
        }

        /// <inheritdoc/>
        public byte[] Unprotect(byte[] protectedData)
        {
            return _protector.Unprotect(protectedData);
        }
    }

    public abstract class DataProtectorNet472 : IDataProtector
    {
        private volatile byte[] m_hashedPurpose;

        protected string ApplicationName { get; }

        protected virtual bool PrependHashedPurposeToPlaintext => true;

        protected string PrimaryPurpose { get; }

        protected IEnumerable<string> SpecificPurposes { get; }

        protected DataProtectorNet472(string applicationName, string primaryPurpose, string[] specificPurposes)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentException("Application names and purposes must contain at least one character which is not white space.", paramName: nameof(applicationName));
            }
            if (string.IsNullOrWhiteSpace(primaryPurpose))
            {
                throw new ArgumentException("Application names and purposes must contain at least one character which is not white space.", paramName: nameof(primaryPurpose));
            }
            if (specificPurposes != null)
            {
                var strArrays = specificPurposes;
                for (var i = 0; i < (int)strArrays.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(strArrays[i]))
                    {
                        throw new ArgumentException("Application names and purposes must contain at least one character which is not white space.", paramName: nameof(specificPurposes));
                    }
                }
            }
            ApplicationName = applicationName;
            PrimaryPurpose = primaryPurpose;
            var strs = new List<string>();
            if (specificPurposes != null)
            {
                strs.AddRange(specificPurposes);
            }
            SpecificPurposes = strs;
        }

        public static DataProtectorNet472 Create(string providerClass, string applicationName, string primaryPurpose, params string[] specificPurposes)
        {
            if (providerClass == null)
            {
                throw new ArgumentNullException(nameof(providerClass));
            }
            return (DataProtectorNet472)CryptoConfig.CreateFromName(providerClass, new object[] { applicationName, primaryPurpose, specificPurposes });
        }

        protected virtual byte[] GetHashedPurpose()
        {
            if (m_hashedPurpose == null)
            {
                using var hashAlgorithm = HashAlgorithm.Create("System.Security.Cryptography.Sha256Cng");
                using (var binaryWriter = new BinaryWriter(new CryptoStream(new MemoryStream(), hashAlgorithm, CryptoStreamMode.Write), new UTF8Encoding(false, true)))
                {
                    binaryWriter.Write(ApplicationName);
                    binaryWriter.Write(PrimaryPurpose);
                    foreach (var specificPurpose in SpecificPurposes)
                    {
                        binaryWriter.Write(specificPurpose);
                    }
                }
                m_hashedPurpose = hashAlgorithm.Hash;
            }
            return m_hashedPurpose;
        }

        public abstract bool IsReprotectRequired(byte[] encryptedData);

        public byte[] Protect(byte[] userData)
        {
            if (userData == null)
            {
                throw new ArgumentNullException(nameof(userData));
            }
            if (PrependHashedPurposeToPlaintext)
            {
                var hashedPurpose = GetHashedPurpose();
                var numArray = new byte[(int)userData.Length + (int)hashedPurpose.Length];
                Array.Copy(hashedPurpose, 0, numArray, 0, (int)hashedPurpose.Length);
                Array.Copy(userData, 0, numArray, (int)hashedPurpose.Length, (int)userData.Length);
                userData = numArray;
            }
            return ProviderProtect(userData);
        }

        protected abstract byte[] ProviderProtect(byte[] userData);

        protected abstract byte[] ProviderUnprotect(byte[] encryptedData);

        public byte[] Unprotect(byte[] encryptedData)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }
            if (!PrependHashedPurposeToPlaintext)
            {
                return ProviderUnprotect(encryptedData);
            }
            var numArray = ProviderUnprotect(encryptedData);
            var hashedPurpose = GetHashedPurpose();
            if (!/*SignedXml.*/CryptographicEquals(hashedPurpose, numArray, (int)hashedPurpose.Length))
            {
                throw new CryptographicException(
                    "The purpose of the protected blob does not match the expected purpose value of this data protector instance.");
            }
            var numArray1 = new byte[(int)numArray.Length - (int)hashedPurpose.Length];
            Array.Copy(numArray, (int)hashedPurpose.Length, numArray1, 0, (int)numArray1.Length);
            return numArray1;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static bool CryptographicEquals(byte[] a, byte[] b, int count)
        {
            var num = 0;
            if ((int)a.Length < count || (int)b.Length < count)
            {
                return false;
            }
            for (var i = 0; i < count; i++)
            {
                num |= a[i] - b[i];
            }
            return num == 0;
        }
    }

    public sealed class DpapiDataProtectorNet472 : DataProtectorNet472
    {
        protected override bool PrependHashedPurposeToPlaintext => false;

        public DataProtectionScope Scope { get; set; }

#if _WINDOWS
        [DataProtectionPermission(SecurityAction.Demand, Unrestricted=true)]
#endif
        [SecuritySafeCritical]
        public DpapiDataProtectorNet472(string appName, string primaryPurpose, params string[] specificPurpose)
            : base(appName, primaryPurpose, specificPurpose)
        {
        }

        public override bool IsReprotectRequired(byte[] encryptedData)
        {
            return true;
        }

#if _WINDOWS
        [DataProtectionPermission(SecurityAction.Assert, ProtectData=true)]
#endif
        [SecuritySafeCritical]
        protected override byte[] ProviderProtect(byte[] userData)
        {
#if _WINDOWS
            return ProtectedData.Protect(userData, GetHashedPurpose(), Scope);
#else
            throw new NotSupportedException();
#endif
        }

#if _WINDOWS
        [DataProtectionPermission(SecurityAction.Assert, UnprotectData=true)]
#endif
        [SecuritySafeCritical]
        protected override byte[] ProviderUnprotect(byte[] encryptedData)
        {
#if _WINDOWS
            return ProtectedData.Unprotect(encryptedData, GetHashedPurpose(), Scope);
#else
            throw new NotSupportedException();
#endif
        }
    }
}
