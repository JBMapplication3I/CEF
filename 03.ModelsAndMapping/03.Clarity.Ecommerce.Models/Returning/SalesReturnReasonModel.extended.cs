// <copyright file="SalesReturnReasonModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return reason model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the sales return reason.</summary>
    /// <seealso cref="TypableBaseModel"/>
    /// <seealso cref="ISalesReturnReasonModel"/>
    public partial class SalesReturnReasonModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsRestockingFeeApplicable), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Is Restocking Fee Applicable")]
        public bool IsRestockingFeeApplicable { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeePercent), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Percent")]
        public decimal? RestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeeAmount), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Amount")]
        public decimal? RestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeeAmountCurrencyID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Amount Currency ID")]
        public int? RestockingFeeAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyName { get; set; }

        /// <inheritdoc cref="ISalesReturnReasonModel.RestockingFeeAmountCurrency"/>
        [ApiMember(Name = nameof(RestockingFeeAmountCurrency), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Amount Currency")]
        public CurrencyModel? RestockingFeeAmountCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ISalesReturnReasonModel.RestockingFeeAmountCurrency { get => RestockingFeeAmountCurrency; set => RestockingFeeAmountCurrency = (CurrencyModel?)value; }
    }
}
