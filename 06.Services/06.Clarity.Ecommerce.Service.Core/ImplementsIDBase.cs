﻿// <copyright file="ImplementsIDBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements identifier base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements identifier.</summary>
    public abstract class ImplementsIDBase : IImplementsIDBase
    {
        /// <summary>Gets or sets the identifier that is passed in via the path.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "path", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int ID { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        [ApiMember(Name = nameof(noCache), DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
        public long? noCache { get; set; }
    }
}
