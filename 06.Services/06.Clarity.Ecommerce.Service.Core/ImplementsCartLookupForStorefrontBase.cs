// <copyright file="ImplementsCartLookupForStorefrontBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements cart lookup for storefront base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements cart lookup for storefront base.</summary>
    /// <seealso cref="ImplementsCartLookupBase"/>
    public abstract class ImplementsCartLookupForStorefrontBase : ImplementsCartLookupBase
    {
        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Cart Type Name")]
        public string? TypeName { get; set; }
    }
}