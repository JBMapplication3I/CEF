// <copyright file="MembershipService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership service class</summary>
#pragma warning disable CA1822
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable 1584, 1658
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Memberships;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A check if subscription is in renewal period.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
     Authenticate,
     Route("/Providers/Payments/IsSubscriptionInRenewalPeriod", "POST",
        Summary = "Use to check if the subscription is in the renewal period.")]
    public class CheckIfSubscriptionIsInRenewalPeriod : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A check if subscription is in upgrade period.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
     Authenticate,
     Route("/Providers/Payments/IsSubscriptionInUpgradePeriod", "POST",
        Summary = "Use to check if the subscription is in the upgrade period.")]
    public class CheckIfSubscriptionIsInUpgradePeriod : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A cancel subscription.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Providers/Payments/CancelSubscription", "POST",
        Summary = "Use to cancel an active subscription.")]
    public class CancelSubscription : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A get available upgrades.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{List{ProductModel}}}"/>
    [PublicAPI,
     Authenticate,
     Route("/Providers/Payments/AvailableSubscriptionUpgrades", "POST",
        Summary = "Use to get the available upgrades.")]
    public class GetAvailableUpgrades : ImplementsIDBase, IReturn<CEFActionResponse<List<ProductModel>>>
    {
    }

    /// <summary>A modify subscription for current user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Providers/Payments/AvailableSubscriptionUpgrades", "PATCH",
        Summary = "Use to get the available upgrades.")]
    public class ModifySubscriptionForCurrentUser : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the subscription.</summary>
        /// <value>The identifier of the subscription.</value>
        [ApiMember(Name = nameof(SubscriptionID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier of the subscription to modify.")]
        public int SubscriptionID { get; set; }

        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        [ApiMember(Name = nameof(BillingContactID), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "When set, will change the billing contact information for the subscription. " +
                "When null, no action is taken against the Billing Contact.")]
        public int? BillingContactID { get; set; }

        /// <summary>Gets or sets the identifier of the shipping contact.</summary>
        /// <value>The identifier of the shipping contact.</value>
        [ApiMember(Name = nameof(ShippingContactID), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "When set, will change the shipping contact information for the subscription. " +
                "When null, no action is taken against the Shipping Contact.")]
        public int? ShippingContactID { get; set; }
    }

    /////// <summary>A renew subscription.</summary>
    /////// <seealso cref="ImplementsIDBase"/>
    /////// <seealso cref="IReturn{SubscriptionModel}"/>
    ////[PublicAPI,
    //// Authenticate,
    //// Route("/Providers/Payments/RenewSubscription", "POST", Summary = "Use to renew the subscription.")]
    ////public class RenewSubscription : ImplementsIDBase, IReturn<CEFActionResponse>
    ////{
    ////    /// <summary>Gets or sets the identifier of the wallet entry.</summary>
    ////    /// <value>The identifier of the wallet entry.</value>
    ////    [ApiMember(Name = nameof(WalletID), DataType = "int", ParameterType = "body", IsRequired = true,
    ////        Description = "Wallet's ID")]
    ////    public int WalletID { get; set; }
    ////}

    /// <summary>A membership service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class MembershipService : ClarityEcommerceServiceBase
    {
        /// <summary>Service Handler for the <see cref="CheckIfSubscriptionIsInRenewalPeriod"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CEFActionResponse.</returns>
        public async Task<object> Post(CheckIfSubscriptionIsInRenewalPeriod request)
        {
            return await VerifyMembershipsProviderIsAvailable()
                .IsSubscriptionInRenewalPeriodAsync(Contract.RequiresValidID(request.ID), null)
                .ConfigureAwait(false);
        }

        /// <summary>Service Handler for the <see cref="CheckIfSubscriptionIsInUpgradePeriod"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CEFActionResponse.</returns>
        public async Task<object> Post(CheckIfSubscriptionIsInUpgradePeriod request)
        {
            return await VerifyMembershipsProviderIsAvailable()
                .IsSubscriptionInUpgradePeriodAsync(Contract.RequiresValidID(request.ID), null)
                .ConfigureAwait(false);
        }

        /// <summary>Service Handler for the <see cref="CancelSubscription"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CEFActionResponse.</returns>
        public async Task<object> Post(CancelSubscription request)
        {
            return await VerifyMembershipsProviderIsAvailable()
                .CancelMembershipAsync(Contract.RequiresValidID(request.ID), null)
                .ConfigureAwait(false);
        }

        /// <summary>Service Handler for the <see cref="GetAvailableUpgrades"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CEFActionResponse{List{ProductModel}}.</returns>
        public async Task<object> Post(GetAvailableUpgrades request)
        {
            return (await VerifyMembershipsProviderIsAvailable()
                .GetAvailableUpgradesAsync(Contract.RequiresValidID(request.ID), null).ConfigureAwait(false))
                .WrapInPassingCEFARIfNotNullOrEmpty<List<IProductModel>, IProductModel>();
        }

        /// <summary>Service Handler for the <see cref="ModifySubscriptionForCurrentUser"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CEFActionResponse.</returns>
        public async Task<object> Patch(ModifySubscriptionForCurrentUser request)
        {
            return await VerifyMembershipsProviderIsAvailable()
                .ModifySubscriptionForUserAsync(
                    request.SubscriptionID,
                    request.BillingContactID,
                    request.ShippingContactID,
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        /////// <summary>Service Handler for the <see cref="RenewSubscription"/> request.</summary>
        /////// <param name="request">The request.</param>
        /////// <returns>A CEFActionResponse{SubscriptionModel}.</returns>
        ////public CEFActionResponse Post(RenewSubscription request)
        ////{
        ////    return VerifyMembershipsProviderIsAvailable().RenewSubscription(
        ////            Contract.RequiresValidID(request.ID),
        ////            Contract.RequiresValidID(request.WalletID),
        ////            PricingFactoryContext,
        ////            GetTaxProvider(),
        ////            null);
        ////}

        private static IMembershipsProviderBase VerifyMembershipsProviderIsAvailable()
        {
            return Contract.RequiresNotNull(
                RegistryLoaderWrapper.GetMembershipProvider(ServiceContextProfileName),
                "No Membership Provider is available");
        }
    }

    /// <summary>A membership feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class MembershipsFeature : IPlugin
    {
        /// <summary>Registers this CheckoutFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
