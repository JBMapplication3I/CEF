// <copyright file="ISalesItemTargetBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemTargetBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for sales item target base.</summary>
    /// <seealso cref="IBase"/>
    public interface ISalesItemTargetBase : IHaveATypeBase<SalesItemTargetType>
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }

        /// <summary>Gets or sets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        bool NothingToShip { get; set; }

        /// <summary>Gets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; }

        /// <summary>Gets or sets the identifier of the destination contact.</summary>
        /// <value>The identifier of the destination contact.</value>
        int DestinationContactID { get; set; }

        /// <summary>Gets or sets destination contact.</summary>
        /// <value>The destination contact.</value>
        Contact? DestinationContact { get; set; }

        /// <summary>Gets or sets the identifier of the origin product inventory location section.</summary>
        /// <value>The identifier of the origin product inventory location section.</value>
        int? OriginProductInventoryLocationSectionID { get; set; }

        /// <summary>Gets or sets the origin product inventory location section.</summary>
        /// <value>The origin product inventory location section.</value>
        ProductInventoryLocationSection? OriginProductInventoryLocationSection { get; set; }

        /// <summary>Gets or sets the identifier of the origin store product.</summary>
        /// <value>The identifier of the origin store product.</value>
        int? OriginStoreProductID { get; set; }

        /// <summary>Gets or sets the origin store product.</summary>
        /// <value>The origin store product.</value>
        StoreProduct? OriginStoreProduct { get; set; }

        /// <summary>Gets or sets the identifier of the brand product.</summary>
        /// <value>The identifier of the brand product.</value>
        int? BrandProductID { get; set; }

        /// <summary>Gets or sets the brand product.</summary>
        /// <value>The brand product.</value>
        BrandProduct? BrandProduct { get; set; }

        /// <summary>Gets or sets the identifier of the origin vendor product.</summary>
        /// <value>The identifier of the origin vendor product.</value>
        int? OriginVendorProductID { get; set; }

        /// <summary>Gets or sets the origin vendor product.</summary>
        /// <value>The origin vendor product.</value>
        VendorProduct? OriginVendorProduct { get; set; }

        /// <summary>Gets or sets the selected rate quote identifier.</summary>
        /// <value>The identifier of the selected rate quote.</value>
        int? SelectedRateQuoteID { get; set; }

        /// <summary>Gets or sets the selected rate quote.</summary>
        /// <value>The selected rate quote.</value>
        RateQuote? SelectedRateQuote { get; set; }
    }
}
