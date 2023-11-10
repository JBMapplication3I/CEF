// <copyright file="ICartValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartValidator interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.CartValidation
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Taxes;

    /// <summary>Interface for cart validator.</summary>
    public interface ICartValidator
    {
        /// <summary>Validates the ready for checkout.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="currentAccount">       The current account.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="currentUserID">        Identifier for the current user.</param>
        /// <param name="currentAccountID">     Identifier for the current account.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateReadyForCheckoutAsync(
            ICartModel cart,
            IAccountModel? currentAccount,
            ITaxesProviderBase? taxesProvider,
            IPricingFactoryContextModel pricingFactoryContext,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName);

        /// <summary>Clears the caches described by pattern.</summary>
        /// <param name="pattern">Specifies the pattern (optional).</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ClearCachesAsync(string? pattern = null);
    }
}
