// <copyright file="VendorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the vendor.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IVendorModel"/>
    public partial class VendorModel
    {
        #region Vendor Properties
        /// <inheritdoc/>
        public string? Notes1 { get; set; }

        /// <inheritdoc/>
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        public string? Terms { get; set; }

        /// <inheritdoc/>
        public string? TermNotes { get; set; }

        /// <inheritdoc/>
        public string? SendMethod { get; set; }

        /// <inheritdoc/>
        public string? EmailSubject { get; set; }

        /// <inheritdoc/>
        public string? ShipTo { get; set; }

        /// <inheritdoc/>
        public string? ShipViaNotes { get; set; }

        /// <inheritdoc/>
        public string? SignBy { get; set; }

        /// <inheritdoc/>
        public decimal? DefaultDiscount { get; set; }

        /// <inheritdoc/>
        public bool AllowDropShip { get; set; }

        /// <inheritdoc/>
        public decimal? RecommendedPurchaseOrderDollarAmount { get; set; }

        /// <inheritdoc/>
        public bool MustResetPassword { get; set; }

        /// <inheritdoc/>
        public string? PasswordHash { get; set; }

        /// <inheritdoc/>
        public string? SecurityToken { get; set; }

        /// <inheritdoc/>
        public string? UserName { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IVendorModel.Shipments" />
        public List<ShipmentModel>? Shipments { get; set; }

        /// <inheritdoc cref="IVendorModel.VendorAccounts" />
        public List<VendorAccountModel>? VendorAccounts { get; set; }

        /// <inheritdoc cref="IVendorModel.PriceRuleVendors" />
        [ApiMember(Name = nameof(PriceRuleVendors), DataType = "List<PriceRuleVendorModel>", ParameterType = "body", IsRequired = false,
            Description = "Price rules associated with the Vendor")]
        public List<PriceRuleVendorModel>? PriceRuleVendors { get; set; }

        /// <inheritdoc/>
        List<IShipmentModel>? IVendorModel.Shipments { get => Shipments?.ToList<IShipmentModel>(); set => Shipments = value?.Cast<ShipmentModel>().ToList(); }

        /// <inheritdoc/>
        List<IPriceRuleVendorModel>? IVendorModel.PriceRuleVendors { get => PriceRuleVendors?.ToList<IPriceRuleVendorModel>(); set => PriceRuleVendors = value?.Cast<PriceRuleVendorModel>().ToList(); }

        /// <inheritdoc/>
        List<IVendorAccountModel>? IVendorModel.VendorAccounts { get => VendorAccounts?.ToList<IVendorAccountModel>(); set => VendorAccounts = value?.Cast<VendorAccountModel>().ToList(); }
        #endregion
    }
}
