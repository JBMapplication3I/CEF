// <copyright file="RecentlyUsedAddressModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the recently used address model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the recently used address.</summary>
    /// <seealso cref="IRecentlyUsedAddressModel"/>
    public class RecentlyUsedAddressModel : IRecentlyUsedAddressModel
    {
        /// <inheritdoc cref="IRecentlyUsedAddressModel.ShippingContact"/>
        public ContactModel? ShippingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? IRecentlyUsedAddressModel.ShippingContact { get => ShippingContact; set => ShippingContact = (ContactModel?)value; }

        /// <inheritdoc cref="IRecentlyUsedAddressModel.BillingContact"/>
        public ContactModel? BillingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? IRecentlyUsedAddressModel.BillingContact { get => BillingContact; set => BillingContact = (ContactModel?)value; }
    }
}
