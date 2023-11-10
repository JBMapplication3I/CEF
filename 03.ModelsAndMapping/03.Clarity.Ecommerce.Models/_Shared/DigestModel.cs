// <copyright file="DigestModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the digest model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the digest.</summary>
    /// <seealso cref="IDigestModel"/>
    public class DigestModel : IDigestModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "Identifier of the object")]
        public int ID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Key), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Custom Key of the object")]
        public string? Key { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Hash), DataType = "long", ParameterType = "body", IsRequired = false,
            Description = "Hash of the object")]
        public long Hash { get; set; }
    }
}
