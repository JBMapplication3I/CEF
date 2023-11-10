// <copyright file="IAmADiscountFilterRelationshipTableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmADiscountFilterRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a discount filter relationship table model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmADiscountFilterRelationshipTableModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>
        where TSlave : IBaseModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the discount.</summary>
        /// <value>The identifier of the discount.</value>
        int DiscountID { get; set; }

        /// <summary>Gets or sets the discount key.</summary>
        /// <value>The discount key.</value>
        string? DiscountKey { get; set; }

        /// <summary>Gets or sets the name of the discount.</summary>
        /// <value>The name of the discount.</value>
        string? DiscountName { get; set; }
        #endregion
    }
}
