// <copyright file="IVendorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVendorModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for vendor model.</summary>
    public partial interface IVendorModel
    {
        #region Vendor Properties
        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        string? Notes1 { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the terms.</summary>
        /// <value>The terms.</value>
        string? Terms { get; set; }

        /// <summary>Gets or sets the term notes.</summary>
        /// <value>The term notes.</value>
        string? TermNotes { get; set; }

        /// <summary>Gets or sets the send method.</summary>
        /// <value>The send method.</value>
        string? SendMethod { get; set; }

        /// <summary>Gets or sets the email subject.</summary>
        /// <value>The email subject.</value>
        string? EmailSubject { get; set; }

        /// <summary>Gets or sets the ship to.</summary>
        /// <value>The ship to.</value>
        string? ShipTo { get; set; }

        /// <summary>Gets or sets the ship via notes.</summary>
        /// <value>The ship via notes.</value>
        string? ShipViaNotes { get; set; }

        /// <summary>Gets or sets who sign this IVendorModel.</summary>
        /// <value>Describes who sign this IVendorModel.</value>
        string? SignBy { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow drop ship.</summary>
        /// <value>True if allow drop ship, false if not.</value>
        bool AllowDropShip { get; set; }

        /// <summary>Gets or sets the default discount.</summary>
        /// <value>The default discount.</value>
        decimal? DefaultDiscount { get; set; }

        /// <summary>Gets or sets the recommended purchase order dollar amount.</summary>
        /// <value>The recommended purchase order dollar amount.</value>
        decimal? RecommendedPurchaseOrderDollarAmount { get; set; }

        /// <summary>Gets or sets a value indicating whether the integration user the must reset the password.</summary>
        /// <value>True if we must reset password, false if not.</value>
        bool MustResetPassword { get; set; }

        /// <summary>Gets or sets the password hash.</summary>
        /// <value>The password hash.</value>
        string? PasswordHash { get; set; }

        /// <summary>Gets or sets the security token.</summary>
        /// <value>The security token.</value>
        string? SecurityToken { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        List<IShipmentModel>? Shipments { get; set; }

        /// <summary>Gets or sets the price rule vendors.</summary>
        /// <value>The price rule vendors.</value>
        List<IPriceRuleVendorModel>? PriceRuleVendors { get; set; }

        /// <summary>Gets or sets the vendor accounts.</summary>
        /// <value>The vendor accounts.</value>
        List<IVendorAccountModel>? VendorAccounts { get; set; }
        #endregion
    }
}
