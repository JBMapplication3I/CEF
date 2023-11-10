// <copyright file="ISalesItemTargetBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemTargetBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>Interface for sales item base target model.</summary>
    /// <see cref="IBaseModel"/>
    public interface ISalesItemTargetBaseModel : IHaveATypeBaseModel<ITypeModel>, DataModel.ICloneable
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }

        /// <summary>Gets or sets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        bool NothingToShip { get; set; }

        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        /// <remarks>NOTE: This property is only used for auto-generation and should never be used directly.</remarks>
        [IgnoreDataMember]
        int MasterID { get; set; }

        /// <summary>Gets or sets the custom split key.</summary>
        /// <value>The custom split key.</value>
        /// <remarks>This property is for internal use by the analyzer only, it is not to be serialized over the wire.</remarks>
        [IgnoreDataMember, JsonIgnore]
        string? CustomSplitKey { get; set; }

        /// <summary>Gets or sets the identifier of the destination contact.</summary>
        /// <value>The identifier of the destination contact.</value>
        int DestinationContactID { get; set; }

        /// <summary>Gets or sets destination contact key.</summary>
        /// <value>The destination contact key.</value>
        string? DestinationContactKey { get; set; }

        /// <summary>Gets or sets destination contact.</summary>
        /// <value>The destination contact.</value>
        IContactModel? DestinationContact { get; set; }

        /// <summary>Gets or sets the identifier of the brand product.</summary>
        /// <value>The identifier of the brand product.</value>
        int? BrandProductID { get; set; }

        /// <summary>Gets or sets the brand product key.</summary>
        /// <value>The brand product key.</value>
        string? BrandProductKey { get; set; }

        /// <summary>Gets or sets the brand product.</summary>
        /// <value>The brand product.</value>
        IBrandProductModel? BrandProduct { get; set; }

        /// <summary>Gets or sets the selected rate quote identifier.</summary>
        /// <value>The identifier of the selected rate quote.</value>
        int? SelectedRateQuoteID { get; set; }

        /// <summary>Gets or sets the selected rate quote key.</summary>
        /// <value>The selected rate quote key.</value>
        string? SelectedRateQuoteKey { get; set; }

        /// <summary>Gets or sets the selected rate quote name.</summary>
        /// <value>The name of the selected rate quote.</value>
        string? SelectedRateQuoteName { get; set; }

        /// <summary>Gets or sets the selected rate quote.</summary>
        /// <value>The selected rate quote.</value>
        IRateQuoteModel? SelectedRateQuote { get; set; }
    }
}
