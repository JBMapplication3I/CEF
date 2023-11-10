// <copyright file="IConversationWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IConversationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IConversationWorkflow
    {
        /// <summary>Sets to copy.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetToCopyAsync(int id, string? contextProfileName);

        /// <summary>Ends the conversation.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> EndAsync(int id, string? contextProfileName);

        /// <summary>Sends a copy of conversation to user email.</summary>
        /// <param name="conversationID">    Identifier for the conversation.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SendACopyOfConversationToUserEmailAsync(int conversationID, string? contextProfileName);

        /// <summary>Invite user.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> InviteUserAsync(int id, int userID, string? contextProfileName);

        /// <summary>Gets messages for active conversations.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="postedSince">       The posted since.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The messages for active conversations.</returns>
        Task<CEFActionResponse<Dictionary<int, List<IMessageModel>>>> GetMessagesForActiveConversationsAsync(
            int userID,
            DateTime? postedSince,
            string? contextProfileName);

        /// <summary>Gets messages for ended conversations.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="postedSince">       The posted since.</param>
        /// <param name="conversationID">    Identifier for the conversation.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The messages for ended conversations.</returns>
        Task<CEFActionResponse<Dictionary<int, List<IMessageModel>>>> GetMessagesForEndedConversationsAsync(
            int userID,
            DateTime? postedSince,
            int? conversationID,
            string? contextProfileName);
    }
}
