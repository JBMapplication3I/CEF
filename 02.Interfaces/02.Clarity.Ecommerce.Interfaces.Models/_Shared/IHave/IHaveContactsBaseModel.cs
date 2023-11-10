// <copyright file="IHaveContactsBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveContactsBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have applied contacts base model.</summary>
    /// <typeparam name="TIContactModel">Type of the contact relationship table model's interface.</typeparam>
    public interface IHaveContactsBaseModel<TIContactModel>
        : IBaseModel
        where TIContactModel : IAmAContactRelationshipTableModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the contacts.</summary>
        /// <value>The contacts.</value>
        List<TIContactModel>? Contacts { get; set; }
        #endregion
    }
}
