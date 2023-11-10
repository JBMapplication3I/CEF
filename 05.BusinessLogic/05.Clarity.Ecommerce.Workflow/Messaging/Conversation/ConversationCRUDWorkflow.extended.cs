// <copyright file="ConversationCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using MoreLinq;
    using Providers.Emails;
    using Utilities;

    public partial class ConversationWorkflow
    {
        static ConversationWorkflow()
        {
            try
            {
                EnsureMapOutHooks();
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse<Dictionary<int, List<IMessageModel>>>> GetMessagesForActiveConversationsAsync(
            int userID,
            DateTime? postedSince,
            string? contextProfileName)
        {
            return GetMessagesForConversationsAsync(userID, postedSince, true, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse<Dictionary<int, List<IMessageModel>>>> GetMessagesForEndedConversationsAsync(
            int userID,
            DateTime? postedSince,
            int? conversationID,
            string? contextProfileName)
        {
            return GetMessagesForConversationsAsync(userID, postedSince, false, conversationID, contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetToCopyAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Conversations.FilterByID(id).SingleAsync();
            if (entity.CopyUserWhenEnded ?? false)
            {
                return CEFAR.PassingCEFAR();
            }
            entity.CopyUserWhenEnded = true;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save the data.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> InviteUserAsync(int id, int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (await context.ConversationUsers
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterConversationUsersByConversationID(id)
                    .FilterConversationUsersByUserID(userID)
                    .AnyAsync()
                    .ConfigureAwait(false))
            {
                return CEFAR.PassingCEFAR("The user was already part of the conversation.");
            }
            var entity = new ConversationUser
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                MasterID = id,
                SlaveID = userID,
            };
            context.ConversationUsers.Add(entity);
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("Failed to save the data.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> EndAsync(int id, string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var entity = await context.Conversations.FilterByID(id).SingleAsync();
                if (entity.HasEnded.HasValue && entity.HasEnded.Value)
                {
                    return CEFAR.PassingCEFAR();
                }
                entity.HasEnded = true;
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("Failed to save the data.");
                }
                if (!entity.CopyUserWhenEnded.HasValue || !entity.CopyUserWhenEnded.Value)
                {
                    return CEFAR.PassingCEFAR();
                }
            }
            var result = await SendACopyOfConversationToUserEmailAsync(id, contextProfileName).ConfigureAwait(false);
            return !result.ActionSucceeded ? result : CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SendACopyOfConversationToUserEmailAsync(
            int conversationID,
            string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var conversationMessages = context.Messages
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterMessagesByConversationID(conversationID)
                    .Select(x => x.Body)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => c + "\r\n</td>\r\n</tr>\r\n<tr>\r\n<td>\r\n" + n);
                Contract.RequiresValidKey(conversationMessages, "Cannot send a conversation that has no message contents.");
                foreach (var email in context.ConversationUsers
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterConversationUsersByConversationID(conversationID)
                    .Select(x => x.Slave!.Email))
                {
                    Contract.RequiresValidKey(email, "Cannot send a conversation to a user that has no email set.");
                    const string Prefix = "<table>\r\n<tbody>\r\n<tr>\r\n<td>\r\n";
                    const string Suffix = "\r\n</td>\r\n</tr>\r\n</tbody>\r\n</table>";
                    var messageContents = Prefix + conversationMessages + Suffix;
                    var result = await new MessagingConversationCopyToCustomerEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: email,
                            parameters: new() { ["messageContents"] = messageContents })
                        .ConfigureAwait(false);
                    if (!result.ActionSucceeded)
                    {
                        return result;
                    }
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Gets messages for conversations.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="postedSince">       The posted since.</param>
        /// <param name="active">            True to active.</param>
        /// <param name="conversationID">    Identifier for the conversation.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The messages for conversations.</returns>
        private static async Task<CEFActionResponse<Dictionary<int, List<IMessageModel>>>> GetMessagesForConversationsAsync(
            int userID,
            DateTime? postedSince,
            bool active,
            int? conversationID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var results = await context.Conversations
                .AsNoTracking()
                .FilterByActive(true)
                .FilterConversationsByHasEnded(!active)
                .FilterConversationsByUserID(userID)
                .FilterByID(conversationID)
                .SelectMany(x => x.Messages!)
                .FilterByActive(true)
                .FilterByModifiedSince(postedSince)
                .OrderBy(x => x.CreatedDate)
                .GroupBy(x => x.ConversationID!.Value)
                .ToDictionaryAsync(
                    x => x.Key,
                    x => x.Select(y => y.CreateMessageModelFromEntityList(contextProfileName)).ToList())
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR()!;
        }

        /// <summary>Ensures that map out hooks.</summary>
        private static void EnsureMapOutHooks()
        {
            if (ModelMapperForConversation.CreateConversationModelFromEntityHooksList != null)
            {
                return;
            }
            ModelMapperForConversation.CreateConversationModelFromEntityHooksList = (entity, model, _) =>
            {
                model.MessagesCount = entity.Messages!.Count(x => x.Active);
                model.ConversationUsersCount = entity.Users!
                    .Where(x => x.Active && x.Slave?.Active == true)
                    .DistinctBy(x => x.Slave!.UserName)
                    .Count();
                model.ConversationUserUserNames = entity.Users!
                    .Where(x => x.Active && x.Slave?.Active == true)
                    .Select(x => x.Slave!.UserName)
                    .Distinct()
                    .ToList()!;
                return model;
            };
        }
    }
}
