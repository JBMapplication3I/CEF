// <copyright file="IReferralCodeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IReferralCodeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;

    /// <summary>Interface for referral code workflow.</summary>
    public partial interface IReferralCodeWorkflow
    {
        /// <summary>Generates a default code for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="typeKey">           The type key.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task GenerateDefaultCodeForUserAsync(
            int userID,
            string typeKey,
            string statusKey,
            string? contextProfileName);
    }
}
