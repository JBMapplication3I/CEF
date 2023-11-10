// <copyright file="ImplementsSeoUrlBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements SEO URL base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements name.</summary>
    public abstract class ImplementsSeoUrlBase
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The SEO URL of the record to call.")]
        public string SeoUrl { get; set; } = null!;

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
