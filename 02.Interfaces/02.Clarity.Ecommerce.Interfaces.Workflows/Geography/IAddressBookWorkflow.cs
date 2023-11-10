// <copyright file="IAddressBookWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressBookWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for address book workflow.</summary>
    public interface IAddressBookWorkflow
    {
        /// <summary>Gets address book.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The address book.</returns>
        Task<List<IAccountContactModel>> GetAddressBookAsync(int accountID, string? contextProfileName);

        /// <summary>Gets address book primary shipping.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The address book primary shipping.</returns>
        Task<IAccountContactModel?> GetAddressBookPrimaryShippingAsync(int accountID, string? contextProfileName);

        /// <summary>Gets address book primary billing.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The address book primary billing.</returns>
        Task<IAccountContactModel?> GetAddressBookPrimaryBillingAsync(int accountID, string? contextProfileName);

        /// <summary>Creates address in book.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="currentUserID">     The identifier of the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new address in book.</returns>
        Task<IAccountContactModel?> CreateAddressInBookAsync(IAccountContactModel request, int? currentUserID, string? contextProfileName);

        /// <summary>Updates the address in book.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IAccountContactModel.</returns>
        Task<IAccountContactModel?> UpdateAddressInBookAsync(IAccountContactModel request, string? contextProfileName);

        /// <summary>Deactivate address in book.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<CEFActionResponse> DeactivateAddressInBookAsync(int id, string? contextProfileName);

        /// <summary>Deactivate address in book.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<CEFActionResponse> DeactivateAddressInBookAsync(string key, string? contextProfileName);
    }
}
