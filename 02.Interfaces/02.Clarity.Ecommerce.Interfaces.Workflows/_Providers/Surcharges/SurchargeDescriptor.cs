// <copyright file="SurchargeDescriptor.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the surcharge descriptor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.Surcharges
{
    using System.Collections.Generic;
    using Models;

    /// <summary>The currently known conditions under which we generate a surcharge. Ideally, always providing enough
    /// information here allows the provider to figure out which transaction you're talking about, even if you don't
    /// have a Key/ProviderKey.</summary>
    /// <remarks>Collected into an interface to make it easier to add new context conditions without changing
    /// everything that touches surcharges.</remarks>
    public struct SurchargeDescriptor
    {
        /// <summary>Gets or sets. A unique key to identify this surcharging context. May be arbitrarily specified by the
        /// caller and the surcharge provider must ensure that all operations for a particular Key relate back to the
        /// same logical surcharge.</summary>
        /// <value>The key.</value>
        public string? Key { get; set; }

        /// <summary>Gets or sets. A unique key to identify this surcharging context. Specified and only generate-able by
        /// the remote provider.</summary>
        /// <value>The provider key.</value>
        public string? ProviderKey { get; set; }

        /// <summary>Gets or sets. The Bank Identification Number of the card. Determines whether the card is surcharge-
        /// able.</summary>
        /// <value>The bin.</value>
        public string? BIN { get; set; }

        /// <summary>Gets or sets. The user who will be making the payment. Typically used for
        /// <see cref="ISurchargeProviderBase.ResolveKeyAsync"/>.</summary>
        /// <value>The purchasing user.</value>
        public IUserModel? PurchasingUser { get; set; }

        /// <summary>Gets or sets. The contact that will be making the payment. Typically used to zero out the fee if the
        /// contact is in a region which does not allow surcharges.</summary>
        /// <value>The billing contact.</value>
        public IContactModel? BillingContact { get; set; }

        /// <summary>Gets or sets. All invoices currently involved in the surcharging operation.</summary>
        /// <value>The applicable invoices.</value>
        public HashSet<ISalesInvoiceModel>? ApplicableInvoices { get; set; }

        // If orders are needed in the future, they'd be another list here.

        /// <summary>Gets or sets. The total amount that will be paid, not including the surcharge.</summary>
        /// <remarks>We can't use an IPaymentModel here because the payment doesn't yet exist when we need to first
        /// calculate a surcharge.</remarks>
        /// <value>The total number of amount.</value>
        public decimal? TotalAmount { get; set; }
    }
}
