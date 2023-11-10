// <copyright file="CheckoutModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout model class</summary>
// ReSharper disable MissingLinebreak
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the checkout.</summary>
    /// <seealso cref="ICheckoutModel"/>
    public class CheckoutModel : ICheckoutModel
    {
        /// <inheritdoc cref="ICheckoutModel.WithTaxes"/>
        public CheckoutWithTaxes? WithTaxes { get; set; }

        /// <inheritdoc/>
        ICheckoutWithTaxes? ICheckoutModel.WithTaxes { get => WithTaxes; set => WithTaxes = (CheckoutWithTaxes?)value; }

        /// <inheritdoc cref="ICheckoutModel.WithCartInfo"/>
        public CheckoutWithCartInfo? WithCartInfo { get; set; }

        /// <inheritdoc/>
        ICheckoutWithCartInfo? ICheckoutModel.WithCartInfo { get => WithCartInfo; set => WithCartInfo = (CheckoutWithCartInfo?)value; }

        /// <inheritdoc cref="ICheckoutModel.WithUserInfo"/>
        public CheckoutWithUserInfo? WithUserInfo { get; set; }

        /// <inheritdoc/>
        ICheckoutWithUserInfo? ICheckoutModel.WithUserInfo { get => WithUserInfo; set => WithUserInfo = (CheckoutWithUserInfo?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByWalletEntry"/>
        public CheckoutPayByWalletEntry? PayByWalletEntry { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByWalletEntry? ICheckoutModel.PayByWalletEntry { get => PayByWalletEntry; set => PayByWalletEntry = (CheckoutPayByWalletEntry?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByCreditCard"/>
        public CheckoutPayByCreditCard? PayByCreditCard { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByCreditCard? ICheckoutModel.PayByCreditCard { get => PayByCreditCard; set => PayByCreditCard = (CheckoutPayByCreditCard?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByECheck"/>
        public CheckoutPayByECheck? PayByECheck { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByECheck? ICheckoutModel.PayByECheck { get => PayByECheck; set => PayByECheck = (CheckoutPayByECheck?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByBillMeLater"/>
        public CheckoutPayByBillMeLater? PayByBillMeLater { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByBillMeLater? ICheckoutModel.PayByBillMeLater { get => PayByBillMeLater; set => PayByBillMeLater = (CheckoutPayByBillMeLater?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByPayPal"/>
        public CheckoutPayByPayPal? PayByPayPal { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByPayPal? ICheckoutModel.PayByPayPal { get => PayByPayPal; set => PayByPayPal = (CheckoutPayByPayPal?)value; }

        /// <inheritdoc cref="ICheckoutModel.PayByPayoneer"/>
        public CheckoutPayByPayoneer? PayByPayoneer { get; set; }

        /// <inheritdoc/>
        ICheckoutPayByPayoneer? ICheckoutModel.PayByPayoneer { get => PayByPayoneer; set => PayByPayoneer = (CheckoutPayByPayoneer?)value; }

        // Other Things

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ReferringStoreID), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "Referring Store ID"), DefaultValue(null)]
        public int? ReferringStoreID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ReferringBrandID), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "Referring Brand ID"), DefaultValue(null)]
        public int? ReferringBrandID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AffiliateAccountKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Key for the Affiliate Account on B2B orders"), DefaultValue(null)]
        public string? AffiliateAccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SalesOrderID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sales Order ID"), DefaultValue(null)]
        public int? SalesOrderID { get; set; }

        /// <inheritdoc cref="ICheckoutModel.Shipping"/>
        [ApiMember(Name = nameof(Shipping), DataType = "ContactModel", ParameterType = "body", IsRequired = true,
            Description = "Shipping Information"), DefaultValue(null)]
        public ContactModel? Shipping { get; set; }

        /// <inheritdoc/>
        IContactModel? ICheckoutModel.Shipping { get => Shipping; set => Shipping = (ContactModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SpecialInstructions), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Adds a note to the order on submission."), DefaultValue(null)]
        public string? SpecialInstructions { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsSameAsBilling), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Is Same As Billing"), DefaultValue(false)]
        public bool IsSameAsBilling { get; set; }

        /// <inheritdoc cref="ICheckoutModel.Billing"/>
        [ApiMember(Name = nameof(Billing), DataType = "ContactModel", ParameterType = "body", IsRequired = true,
            Description = "Billing Information"), DefaultValue(null)]
        public ContactModel? Billing { get; set; }

        /// <inheritdoc/>
        IContactModel? ICheckoutModel.Billing { get => Billing; set => Billing = (ContactModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PaymentStyle), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Payment Style"), DefaultValue(null)]
        public string? PaymentStyle { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsPartialPayment), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Is a partial payment"), DefaultValue(false)]
        public bool IsPartialPayment { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Amount), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Amount"), DefaultValue(null)]
        public decimal? Amount { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CurrencyKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "CurrencyKey"), DefaultValue(null)]
        public string? CurrencyKey { get; set; }

        // For Quote Submissions

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryIDs), DataType = "int[]", ParameterType = "body", IsRequired = false,
            Description = "For Quote Submissions, list of category ids to attach")]
        public int[]? CategoryIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FileNames), DataType = "List<string>", ParameterType = "body", IsRequired = false,
            Description = "For Quote Submissions, list of Uploaded File Names to attach"), DefaultValue(null)]
        public List<string>? FileNames { get; set; }

        // Custom Attributes to apply to the object

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = false,
            Description = "For Order or Quote submissions, the custom attributes to apply to the record created"), DefaultValue(null)]
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }
}
