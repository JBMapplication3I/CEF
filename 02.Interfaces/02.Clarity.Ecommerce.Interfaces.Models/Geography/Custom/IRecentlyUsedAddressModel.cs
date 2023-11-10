// <copyright file="IRecentlyUsedAddressModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRecentlyUsedAddressModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for recently used address model.</summary>
    public interface IRecentlyUsedAddressModel
    {
        /// <summary>Gets or sets the shipping contact.</summary>
        /// <value>The shipping contact.</value>
        IContactModel? ShippingContact { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        IContactModel? BillingContact { get; set; }
    }
}
