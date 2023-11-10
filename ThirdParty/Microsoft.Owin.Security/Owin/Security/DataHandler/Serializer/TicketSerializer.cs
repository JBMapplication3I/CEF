// <copyright file="TicketSerializer.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ticket serializer class</summary>
namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>A ticket serializer.</summary>
    /// <seealso cref="IDataSerializer{AuthenticationTicket}"/>
    public class TicketSerializer : IDataSerializer<AuthenticationTicket>
    {
        /// <summary>The format version.</summary>
        private const int FormatVersion = 3;

        /// <summary>Reads the given reader.</summary>
        /// <param name="reader">The reader to read.</param>
        /// <returns>An AuthenticationTicket.</returns>
        public static AuthenticationTicket Read(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            if (reader.ReadInt32() != FormatVersion)
            {
                return null;
            }
            var str = reader.ReadString();
            var str1 = ReadWithDefault(reader, DefaultValues.NameClaimType);
            var str2 = ReadWithDefault(reader, DefaultValues.RoleClaimType);
            var num = reader.ReadInt32();
            var claim = new Claim[num];
            for (var i = 0; i != num; i++)
            {
                var str3 = ReadWithDefault(reader, str1);
                var str4 = reader.ReadString();
                var str5 = ReadWithDefault(reader, DefaultValues.StringValueType);
                var str6 = ReadWithDefault(reader, DefaultValues.LocalAuthority);
                var str7 = ReadWithDefault(reader, str6);
                claim[i] = new Claim(str3, str4, str5, str6, str7);
            }
            var claimsIdentity = new ClaimsIdentity(claim, str, str1, str2);
            if (reader.ReadInt32() > 0)
            {
                claimsIdentity.BootstrapContext = reader.ReadString();
            }
            return new AuthenticationTicket(claimsIdentity, PropertiesSerializer.Read(reader));
        }

        /// <summary>Writes.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="model"> The model.</param>
        public static void Write(BinaryWriter writer, AuthenticationTicket model)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            writer.Write(FormatVersion);
            var identity = model.Identity;
            writer.Write(identity.AuthenticationType);
            WriteWithDefault(
                writer,
                identity.NameClaimType,
                DefaultValues.NameClaimType);
            WriteWithDefault(
                writer,
                identity.RoleClaimType,
                DefaultValues.RoleClaimType);
            writer.Write(identity.Claims.Count());
            foreach (var claim in identity.Claims)
            {
                WriteWithDefault(writer, claim.Type, identity.NameClaimType);
                writer.Write(claim.Value);
                WriteWithDefault(writer, claim.ValueType, DefaultValues.StringValueType);
                WriteWithDefault(writer, claim.Issuer, DefaultValues.LocalAuthority);
                WriteWithDefault(writer, claim.OriginalIssuer, claim.Issuer);
            }
            if (identity.BootstrapContext is not string bootstrapContext || string.IsNullOrWhiteSpace(bootstrapContext))
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(bootstrapContext.Length);
                writer.Write(bootstrapContext);
            }
            PropertiesSerializer.Write(writer, model.Properties);
        }

        /// <inheritdoc/>
        public virtual AuthenticationTicket Deserialize(byte[] data)
        {
            AuthenticationTicket authenticationTicket;
            using (var memoryStream = new MemoryStream(data))
            {
                using var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
                using var binaryReader = new BinaryReader(gZipStream);
                authenticationTicket = Read(binaryReader);
            }
            return authenticationTicket;
        }

        /// <inheritdoc/>
        public virtual byte[] Serialize(AuthenticationTicket model)
        {
            byte[] array;
            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    using var binaryWriter = new BinaryWriter(gZipStream);
                    Write(binaryWriter, model);
                }
                array = memoryStream.ToArray();
            }
            return array;
        }

        /// <summary>Reads with default.</summary>
        /// <param name="reader">      The reader.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The with default.</returns>
        private static string ReadWithDefault(BinaryReader reader, string defaultValue)
        {
            var str = reader.ReadString();
            return string.Equals(str, DefaultValues.DefaultStringPlaceholder, StringComparison.Ordinal)
                ? defaultValue
                : str;
        }

        /// <summary>Writes a with default.</summary>
        /// <param name="writer">      The writer.</param>
        /// <param name="value">       The value.</param>
        /// <param name="defaultValue">The default value.</param>
        private static void WriteWithDefault(BinaryWriter writer, string value, string defaultValue)
        {
            if (string.Equals(value, defaultValue, StringComparison.Ordinal))
            {
                writer.Write(DefaultValues.DefaultStringPlaceholder);
                return;
            }
            writer.Write(value);
        }

        /// <summary>A default values.</summary>
        private static class DefaultValues
        {
            /// <summary>The default string placeholder.</summary>
            public const string DefaultStringPlaceholder = "\0";

            /// <summary>The local authority.</summary>
            public const string LocalAuthority = "LOCAL AUTHORITY";

            /// <summary>Type of the name claim.</summary>
            public const string NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

            /// <summary>Type of the role claim.</summary>
            public const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

            /// <summary>Type of the string value.</summary>
            public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
        }
    }
}
