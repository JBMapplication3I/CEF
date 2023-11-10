﻿// <copyright file="ImplementsTypeNameBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements type name base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements type name.</summary>
    public abstract class ImplementsTypeNameBase : IImplementsTypeNameBase
    {
        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Cart Type Name")]
        public string? TypeName { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        [ApiMember(Name = nameof(noCache), DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300, IDE1006
        public long? noCache { get; set; }
#pragma warning restore SA1300, IDE1006
    }
}
