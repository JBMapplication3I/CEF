// <copyright file="ImplementsIDOnBodyForAdminBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements identifier on body for admin base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements identifier on body for admin base.</summary>
    /// <seealso cref="ImplementsCartLookupForAdminBase"/>
    /// <seealso cref="IImplementsIDBase"/>
    public abstract class ImplementsIDOnBodyForAdminBase : ImplementsCartLookupForAdminBase, IImplementsIDBase
    {
        /// <summary>Gets or sets the identifier that is passed in via the body.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int ID { get; set; }
    }
}
