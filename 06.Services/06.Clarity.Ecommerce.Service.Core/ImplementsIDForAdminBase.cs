// <copyright file="ImplementsIDForAdminBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements identifier for admin base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements identifier for admin base.</summary>
    /// <seealso cref="ImplementsCartLookupForAdminBase"/>
    /// <seealso cref="IImplementsIDBase"/>
    public abstract class ImplementsIDForAdminBase : ImplementsCartLookupForAdminBase, IImplementsIDBase
    {
        /// <summary>Gets or sets the identifier that is passed in via the path.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "path", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int ID { get; set; }
    }
}