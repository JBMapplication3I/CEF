// <copyright file="CheckoutProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout provider base class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Checkouts
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SampleRequestHandlers.Checkouts;
    using Interfaces.Providers.Taxes;

    /// <summary>A sample request checkout workflow.</summary>
    /// <seealso cref="ISampleRequestCheckoutProviderBase"/>
    public abstract class SampleRequestCheckoutProviderBase : ProviderBase, ISampleRequestCheckoutProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SampleRequestCheckout;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            string cartType,
            ITaxesProviderBase? taxesProvider,
            bool isTaxable,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName);
    }
}
