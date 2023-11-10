// <copyright file="ImplementsKeyOnQueryBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements key base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements identifier on the query parameters of the request instead of the path.</summary>
    public abstract class ImplementsKeyOnQueryBase : IImplementsKeyBase
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        [ApiMember(Name = nameof(Key), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The CustomKey of the record to call")]
        public string Key { get; set; } = null!;

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        [ApiMember(Name = nameof(noCache), DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
        public long? noCache { get; set; }
    }
}
