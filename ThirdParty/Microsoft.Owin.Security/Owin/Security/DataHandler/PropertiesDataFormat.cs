// <copyright file="PropertiesDataFormat.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the properties data format class</summary>
namespace Microsoft.Owin.Security.DataHandler
{
    using DataProtection;
    using Encoder;
    using Serializer;

    /// <summary>The properties data format.</summary>
    /// <seealso cref="SecureDataFormat{AuthenticationProperties}"/>
    /// <seealso cref="SecureDataFormat{AuthenticationProperties}"/>
    public class PropertiesDataFormat : SecureDataFormat<AuthenticationProperties>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="PropertiesDataFormat" /> class.</summary>
        /// <param name="protector">The protector.</param>
        public PropertiesDataFormat(IDataProtector protector)
            : base(DataSerializers.Properties, protector, TextEncodings.Base64Url)
        {
        }
    }
}
