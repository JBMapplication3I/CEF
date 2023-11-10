// <copyright file="ReferralCodeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the referral code workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using Models;
    using Utilities;

    public partial class ReferralCodeWorkflow
    {
        /// <inheritdoc/>
        public Task GenerateDefaultCodeForUserAsync(int userID, string typeKey, string statusKey, string? contextProfileName)
        {
            var code = ReferralCodeGenerator.GenerateCodeForUser(userID);
            var referralCode = new ReferralCodeModel
            {
                // Base Properties
                Active = true,
                CustomKey = code,
                CreatedDate = DateExtensions.GenDateTime,
                // NameableBase Properties
                Name = "Default Referral Code",
                // ReferralCode Properties
                Code = code,
                // Related Objects
                TypeKey = typeKey,
                StatusKey = statusKey,
                UserID = userID,
            };
            return CreateAsync(referralCode, contextProfileName);
        }
    }
}
