// <copyright file="IShippingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShippingProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Shipping
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for shipping provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IShippingProviderBase : IProviderBase
    {
        /// <summary>Gets rate quotes.</summary>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="salesItemIDs">      The IDs of the cart items.</param>
        /// <param name="origin">            The origin of the shipment.</param>
        /// <param name="destination">       The destination for the shipment.</param>
        /// <param name="expedited">         True if expedited.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The rate quotes wrapped in a <see cref="CEFActionResponse{TResult}"/>.</returns>
        Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName);

        /// <summary>Gets base or net charges.</summary>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="salesItemIDs">      The IDs of the cart items.</param>
        /// <param name="origin">            The origin of the shipment.</param>
        /// <param name="destination">       The destination for the shipment.</param>
        /// <param name="expedited">         True if expedited.</param>
        /// <param name="useBaseCharge">     True to use base charge.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The base or net charges.</returns>
        Task<List<IShipmentRate>> GetBaseOrNetChargesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            bool useBaseCharge,
            string? contextProfileName);
    }
}
