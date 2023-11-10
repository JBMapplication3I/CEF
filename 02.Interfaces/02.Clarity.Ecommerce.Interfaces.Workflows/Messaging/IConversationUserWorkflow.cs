// <copyright file="IConversationUserWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IConversationUserWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for conversation user workflow.</summary>
    public partial interface IConversationUserWorkflow
    {
        /// <summary>Sets typing state.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="isTyping">          True if this IConversationUserWorkflow is typing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetTypingStateAsync(int userID, bool isTyping, string? contextProfileName);

        /// <summary>Gets other conversation users typing statuses.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The other conversation users typing statuses.</returns>
        Task<CEFActionResponse<List<UserTypingStatus>>> GetOtherConversationUsersTypingStatusesAsync(int userID, string? contextProfileName);
    }
}
