// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.Rfc6238AuthenticationService
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>A rfc 6238 authentication service.</summary>
    internal static class Rfc6238AuthenticationService
    {
        /// <summary>The encoding.</summary>
        private static readonly Encoding _encoding = new UTF8Encoding(false, true);

        /// <summary>The timestep.</summary>
        private static readonly TimeSpan Timestep = TimeSpan.FromMinutes(3.0);

        /// <summary>The unix epoch Date/Time.</summary>
        private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>Generates a code.</summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="modifier">     The modifier.</param>
        /// <returns>The code.</returns>
        public static int GenerateCode(SecurityToken securityToken, string modifier = null)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using var hmacshA1 = new HMACSHA1(securityToken.GetDataNoClone());
            return ComputeTotp(hmacshA1, currentTimeStepNumber, modifier);
        }

        /// <summary>Validates the code.</summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="code">         The code.</param>
        /// <param name="modifier">     The modifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool ValidateCode(SecurityToken securityToken, int code, string modifier = null)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using (var hmacshA1 = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                for (var index = -2; index <= 2; ++index)
                {
                    if (ComputeTotp(hmacshA1, currentTimeStepNumber + (ulong)index, modifier) == code)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>Applies the modifier.</summary>
        /// <param name="input">   The input.</param>
        /// <param name="modifier">The modifier.</param>
        /// <returns>A byte[].</returns>
        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (string.IsNullOrEmpty(modifier))
            {
                return input;
            }
            var bytes = _encoding.GetBytes(modifier);
            var numArray = new byte[checked(input.Length + bytes.Length)];
            Buffer.BlockCopy(input, 0, numArray, 0, input.Length);
            Buffer.BlockCopy(bytes, 0, numArray, input.Length, bytes.Length);
            return numArray;
        }

        /// <summary>Calculates the totp.</summary>
        /// <param name="hashAlgorithm"> The hash algorithm.</param>
        /// <param name="timestepNumber">The timestep number.</param>
        /// <param name="modifier">      The modifier.</param>
        /// <returns>The calculated totp.</returns>
        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier)
        {
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var hash = hashAlgorithm.ComputeHash(ApplyModifier(bytes, modifier));
            var index = hash[^1] & 15;
            return (((hash[index] & sbyte.MaxValue) << 24)
                    | ((hash[index + 1] & byte.MaxValue) << 16)
                    | ((hash[index + 2] & byte.MaxValue) << 8)
                    | (hash[index + 3] & byte.MaxValue))
                % 1000000;
        }

        /// <summary>Gets current time step number.</summary>
        /// <returns>The current time step number.</returns>
        private static ulong GetCurrentTimeStepNumber()
        {
            return (ulong)((DateTime.UtcNow - _unixEpoch).Ticks / Timestep.Ticks);
        }
    }
}
