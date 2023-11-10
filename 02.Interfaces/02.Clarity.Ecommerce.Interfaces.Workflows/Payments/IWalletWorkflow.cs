// <copyright file="IWalletWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWalletWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Providers.Payments;

    /// <summary>Interface for wallet workflow.</summary>
    public partial interface IWalletWorkflow
    {
        /// <summary>Gets user wallet.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="provider">          The provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user wallet wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<List<IWalletModel>>> GetWalletForUserAsync(
            int userID,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Gets wallet entry for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="entryID">           Identifier for the entry.</param>
        /// <param name="provider">          The provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The wallet entry for user.</returns>
        Task<CEFActionResponse<IWalletModel>> GetWalletEntryForUserAsync(
            int userID,
            int entryID,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Creates a wallet entry.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="provider">     The payment wallet.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new credit card wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<IWalletModel>> CreateWalletEntryAsync(
            IWalletModel model,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Updates the wallet entry.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="provider">     The payment wallet.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IWalletModel wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<IWalletModel>> UpdateWalletEntryAsync(
            IWalletModel model,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Sets wallet entry as default.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="entryID">           Identifier for the entry.</param>
        /// <param name="provider">          The provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetWalletEntryAsDefaultAsync(
            int userID,
            int entryID,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Deactivate a wallet entry.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="provider">     The payment wallet.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateWalletEntryAsync(
            int id,
            IWalletProviderBase provider,
            string? contextProfileName);

        /// <summary>Gets decrypted wallet.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="walletID">          Identifier for the wallet.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The decrypted wallet wrapped in a CEFActionResponse.</returns>
        Task<CEFActionResponse<IWalletModel>> GetDecryptedWalletAsync(
            int userID,
            int walletID,
            string? contextProfileName);
    }
}
