// <copyright file="ImplementsCartLookupBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements cart lookup base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements cart lookup base.</summary>
    public abstract class ImplementsCartLookupBase
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The identifier of the store to use, if any")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        [ApiMember(Name = nameof(FranchiseID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The identifier of the franchise to use, if any")]
        public int? FranchiseID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The identifier of the brand to use, if any")]
        public int? BrandID { get; set; }

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