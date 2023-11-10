// <copyright file="PropertiesSerializer.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the properties serializer class</summary>
namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>The properties serializer.</summary>
    /// <seealso cref="IDataSerializer{AuthenticationProperties}"/>
    /// <seealso cref="IDataSerializer{AuthenticationProperties}"/>
    public class PropertiesSerializer : IDataSerializer<AuthenticationProperties>
    {
        /// <summary>The format version.</summary>
        private const int FormatVersion = 1;

        /// <summary>Reads the given reader.</summary>
        /// <param name="reader">The reader to read.</param>
        /// <returns>The AuthenticationProperties.</returns>
        public static AuthenticationProperties Read(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            if (reader.ReadInt32() != 1)
            {
                return null;
            }
            var num = reader.ReadInt32();
            var strs = new Dictionary<string, string>(num);
            for (var i = 0; i != num; i++)
            {
                strs.Add(reader.ReadString(), reader.ReadString());
            }
            return new AuthenticationProperties(strs);
        }

        /// <summary>Writes.</summary>
        /// <param name="writer">    The writer.</param>
        /// <param name="properties">The properties.</param>
        public static void Write(BinaryWriter writer, AuthenticationProperties properties)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            writer.Write(1);
            writer.Write(properties.Dictionary.Count);
            foreach (var dictionary in properties.Dictionary)
            {
                writer.Write(dictionary.Key);
                writer.Write(dictionary.Value);
            }
        }

        /// <inheritdoc/>
        public AuthenticationProperties Deserialize(byte[] data)
        {
            AuthenticationProperties authenticationProperty;
            using (var memoryStream = new MemoryStream(data))
            {
                using var binaryReader = new BinaryReader(memoryStream);
                authenticationProperty = Read(binaryReader);
            }
            return authenticationProperty;
        }

        /// <inheritdoc/>
        public byte[] Serialize(AuthenticationProperties model)
        {
            byte[] array;
            using (var memoryStream = new MemoryStream())
            {
                using var binaryWriter = new BinaryWriter(memoryStream);
                Write(binaryWriter, model);
                binaryWriter.Flush();
                array = memoryStream.ToArray();
            }
            return array;
        }
    }
}
