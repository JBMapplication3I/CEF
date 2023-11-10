// <copyright file="ImplementsTypeNameForAdminBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements type name for admin base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements type name for admin base.</summary>
    /// <seealso cref="ImplementsCartLookupForAdminBase"/>
    /// <seealso cref="IImplementsTypeNameBase"/>
    public abstract class ImplementsTypeNameForAdminBase : ImplementsCartLookupForAdminBase, IImplementsTypeNameBase
    {
        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Cart Type Name")]
        public string? TypeName { get; set; }
    }
}