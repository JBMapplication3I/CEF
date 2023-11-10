// <copyright file="IAccountPricePointWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountPricePointWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for account price point workflow.</summary>
    public partial interface IAccountPricePointWorkflow
    {
        /// <summary>Gets the record.</summary>
        /// <param name="keys">              The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IAccountPricePointModel.</returns>
        Task<IAccountPricePointModel?> GetAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName);

        /// <summary>Gets the record.</summary>
        /// <param name="keys">   The account key.</param>
        /// <param name="context">The context.</param>
        /// <returns>An IAccountPricePointModel.</returns>
        Task<IAccountPricePointModel?> GetAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context);

        /// <summary>Deactivates the record.</summary>
        /// <param name="keys">              The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName);

        /// <summary>Deactivates the record.</summary>
        /// <param name="keys">   The account key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context);

        /// <summary>Reactivates the record.</summary>
        /// <param name="keys">              The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName);

        /// <summary>Reactivates the record.</summary>
        /// <param name="keys">   The account key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context);

        /// <summary>Deletes the record.</summary>
        /// <param name="keys">              The account key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName);

        /// <summary>Deletes the record.</summary>
        /// <param name="keys">   The account key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context);
    }
}
