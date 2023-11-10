// <copyright file="ISubscriptionProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for subscription provider base.</summary>
    public interface ISubscriptionProviderBase
    {
        /// <summary>Creates a subscription.</summary>
        /// <param name="model">                  The model.</param>
        /// <param name="paymentAlreadyConverted">True if payment already converted.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>The new subscription.</returns>
        Task<IPaymentResponse> CreateSubscriptionAsync(
            ISubscriptionPaymentModel model,
            bool paymentAlreadyConverted,
            string? contextProfileName);

        /// <summary>Updates the subscription described by model.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> UpdateSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName);

        /// <summary>Gets a subscription.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The subscription.</returns>
        Task<IPaymentResponse> GetSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName);

        /// <summary>Cancel subscription.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> CancelSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName);

        /// <summary>Implement product subscription from order item.</summary>
        /// <param name="userID">               Identifier for the user.</param>
        /// <param name="accountID">            Identifier for the account.</param>
        /// <param name="salesGroupID">         Identifier for the sales group.</param>
        /// <param name="item">                 The item.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="invoiceID">            Identifier for the invoice.</param>
        /// <param name="salesOrderID">         Identifier for the sales order.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ImplementProductSubscriptionFromOrderItemAsync(
            int? userID,
            int? accountID,
            int? salesGroupID,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> item,
            IPricingFactoryContextModel pricingFactoryContext,
            int? invoiceID,
            int? salesOrderID,
            DateTime timestamp,
            string? contextProfileName);
    }
}
