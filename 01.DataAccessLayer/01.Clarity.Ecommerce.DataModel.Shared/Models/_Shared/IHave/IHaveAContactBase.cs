// <copyright file="IHaveAContactBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAContactBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for have a contact base.</summary>
    public interface IHaveAContactBase
    {
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int ContactID { get; set; }

        /// <summary>Gets or sets the contact.</summary>
        /// <value>The contact.</value>
        Contact? Contact { get; set; }
    }

    /// <summary>Interface for have a nullable contact base.</summary>
    public interface IHaveANullableContactBase
    {
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int? ContactID { get; set; }

        /// <summary>Gets or sets the contact.</summary>
        /// <value>The contact.</value>
        Contact? Contact { get; set; }
    }
}
