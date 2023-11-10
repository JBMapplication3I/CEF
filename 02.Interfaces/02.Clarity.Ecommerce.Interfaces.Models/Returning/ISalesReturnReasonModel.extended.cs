// <copyright file="ISalesReturnReasonModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesReturnReasonModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales return reason model.</summary>
    /// <seealso cref="ITypableBaseModel"/>
    public partial interface ISalesReturnReasonModel
    {
        /// <summary>Gets or sets a value indicating whether this ISalesReturnReasonModel is restocking fee
        /// applicable.</summary>
        /// <value>True if this ISalesReturnReasonModel is restocking fee applicable, false if not.</value>
        bool IsRestockingFeeApplicable { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency key.</summary>
        /// <value>The restocking fee amount currency key.</value>
        string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the restocking fee amount currency.</summary>
        /// <value>The name of the restocking fee amount currency.</value>
        string? RestockingFeeAmountCurrencyName { get; set; }

        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        ICurrencyModel? RestockingFeeAmountCurrency { get; set; }
    }
}
