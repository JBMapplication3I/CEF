// <copyright file="CartValidator.Accounts.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using Models;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Validates the account is not on hold if account is available to check.</summary>
        /// <param name="response">            The response.</param>
        /// <param name="currentAccountOnHold">The current account on hold state.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual bool ValidateAccountIsNotOnHoldIfAccountIsAvailableToCheck(
            CEFActionResponse response,
            bool? currentAccountOnHold)
        {
            if (!currentAccountOnHold.HasValue || !currentAccountOnHold.Value)
            {
                // SUCCESS! Your Account is not on hold.
                return true;
            }
            response.Messages.Add("ERROR! Your Account is currently on hold. Please contact support for assistance.");
            return false;
        }
    }
}
