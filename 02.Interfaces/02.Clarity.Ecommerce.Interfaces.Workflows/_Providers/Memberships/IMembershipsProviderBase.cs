// <copyright file="IMembershipsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMembershipsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Memberships
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for memberships provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IMembershipsProviderBase : IProviderBase
    {
        /// <summary>Implement product membership from order item.</summary>
        /// <param name="salesOrderUserID">     Identifier for the sales order user.</param>
        /// <param name="salesOrderAccountID">  Identifier for the sales order account.</param>
        /// <param name="salesOrderItem">       The sales order item.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="invoiceID">            The invoice.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ImplementProductMembershipFromOrderItemAsync(
            int? salesOrderUserID,
            int? salesOrderAccountID,
            ISalesItemBaseModel salesOrderItem,
            IPricingFactoryContextModel pricingFactoryContext,
            int? invoiceID,
            DateTime timestamp,
            string? contextProfileName);

        /// <summary>Is subscription in renewal period.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> IsSubscriptionInRenewalPeriodAsync(int subscriptionID, string? contextProfileName);

        /// <summary>Is subscription in upgrade period.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> IsSubscriptionInUpgradePeriodAsync(int subscriptionID, string? contextProfileName);

        /// <summary>Renew membership.</summary>
        /// <param name="previousSubscriptionID">Identifier for the previous subscription.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="invoiceID">             Identifier for the invoice.</param>
        /// <param name="timestamp">             The timestamp Date/Time.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> RenewMembershipAsync(
            int previousSubscriptionID,
            int productID,
            int? invoiceID,
            DateTime timestamp,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Renew membership.</summary>
        /// <param name="previousSubscriptionID">Identifier for the previous subscription.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="invoiceID">             Identifier for the invoice.</param>
        /// <param name="timestamp">             The timestamp Date/Time.</param>
        /// <param name="fee">                   The fee.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> RenewMembershipAsync(
            int previousSubscriptionID,
            int productID,
            int? invoiceID,
            DateTime timestamp,
            decimal fee,
            string? contextProfileName);

        /////// <summary>Renew subscription.</summary>
        /////// <param name="subscriptionID">       Identifier for the subscription.</param>
        /////// <param name="walletID">             Identifier for the wallet.</param>
        /////// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /////// <param name="taxesProvider">        The taxes provider.</param>
        /////// <param name="contextProfileName">   Name of the context profile.</param>
        /////// <returns>A CEFActionResponse.</returns>
        ////Task<CEFActionResponse> RenewSubscriptionAsync(
        ////    int subscriptionID,
        ////    int walletID,
        ////    IPricingFactoryContextModel pricingFactoryContext,
        ////    ITaxesProviderBase? taxesProvider,
        ////    string? contextProfileName);

        /// <summary>Cancel membership.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CancelMembershipAsync(int subscriptionID, string? contextProfileName);

        /// <summary>Gets available upgrades.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The available upgrades.</returns>
        Task<List<IProductModel>> GetAvailableUpgradesAsync(int subscriptionID, string? contextProfileName);

        /// <summary>Modify subscription for user.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="billingContactID">  Identifier for the billing contact.</param>
        /// <param name="shippingContactID"> Identifier for the shipping contact.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ModifySubscriptionForUserAsync(
            int subscriptionID,
            int? billingContactID,
            int? shippingContactID,
            int userID,
            string? contextProfileName);
    }
}
