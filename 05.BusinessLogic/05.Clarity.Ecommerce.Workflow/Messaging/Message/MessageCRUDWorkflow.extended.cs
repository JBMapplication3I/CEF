// <copyright file="MessageCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Providers.Emails;
    using Utilities;

    public partial class MessageWorkflow
    {
        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IMessageModel model,
            string? contextProfileName)
        {
            var messageCreateResponse = await base.CreateAsync(model, contextProfileName).ConfigureAwait(false);
            if (CEFConfigDictionary.EmailNotificationsMessagingEmailCopiesEnabled)
            {
                var message = await GetAsync(messageCreateResponse.Result, contextProfileName).ConfigureAwait(false);
                await new MessagingNewMessageWaitingNotificationToRecipientsEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["senderUsername"] = message!.SentByUserUserName,
                            ["subject"] = message.Subject,
                            ["messageID"] = Contract.RequiresValidID(message.ID),
                            ["recipients"] = message.MessageRecipients!
                                .Where(x => x.Active && Contract.CheckValidKey(x.User?.Email) && x.User!.Active)
                                .Select(x => x.User!.Email)
                                .ToArray(),
                        })
                    .ConfigureAwait(false);
            }
            return messageCreateResponse;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> PostMessageAsync(
            IMessageModel model,
            string? contextProfileName)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            var conversationID = await CheckExistsAsync(
                    Contract.RequiresValidID(model.ConversationID),
                    contextProfileName)
                .ConfigureAwait(false);
            Contract.RequiresValidID(conversationID);
            var timestamp = DateExtensions.GenDateTime;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                model.MessageRecipients = (await context.ConversationUsers
                        .FilterByActive(true)
                        .FilterConversationUsersByConversationID(conversationID)
                        .Select(x => x.SlaveID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Where(x => x != model.SentByUserID)
                    .Select(x => new MessageRecipientModel
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        SlaveID = x,
                    })
                    .ToList<IMessageRecipientModel>();
            }
            await PostToProviderIfOfflineAsync(
                    fromUserID: model.SentByUserID!.Value,
                    messageRecipients: model.MessageRecipients,
                    body: model.Body!,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await CreateAsync(model, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Message>> FilterQueryByModelCustomAsync(
            IQueryable<Message> query,
            IMessageSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterMessagesBySentFromOrToUserID(search.SentFromOrToUserID)
                .FilterMessagesBySentFromUserID(search.SentFromUserID)
                .FilterMessagesBySentToUserID(search.SentToUserID);
        }

        /// <summary>Posts to provider if offline.</summary>
        /// <param name="fromUserID">        Identifier for from user.</param>
        /// <param name="messageRecipients"> The message recipients.</param>
        /// <param name="body">              The body.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task PostToProviderIfOfflineAsync(
            int fromUserID,
            IEnumerable<IMessageRecipientModel> messageRecipients,
            string body,
            string? contextProfileName)
        {
            var fromInfo = ParseOutProviderInfoForUser(fromUserID, contextProfileName);
            var provider = RegistryLoaderWrapper.GetChattingProvider(contextProfileName);
            if (provider == null)
            {
                return;
            }
            foreach (var userID in messageRecipients.Select(x => x.SlaveID))
            {
                var (p, username) = ParseOutProviderInfoForUser(userID, contextProfileName);
                if (p == null)
                {
                    continue;
                }
                await provider.PostMessageAsync(fromInfo.provider, username, body).ConfigureAwait(false);
            }
        }

        /// <summary>Parse out provider information for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Tuple.</returns>
        private static (string? provider, string? username) ParseOutProviderInfoForUser(
            int userID,
            string? contextProfileName)
        {
            (string? provider, string? username) result = (null, null);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = context.Users
                .FilterByActive(true)
                .FilterByID(userID)
                .Select(x => new
                {
                    x.JsonAttributes,
                    StatusName = x.UserOnlineStatus == null ? "Offline" : x.UserOnlineStatus.Name,
                })
                .Single();
            var attributes = user.JsonAttributes.DeserializeAttributesDictionary();
            if (user.StatusName != "Offline"
                || !attributes.TryGetValue("Offline-Messaging-Setting", out var setting)
                || string.IsNullOrWhiteSpace(setting.Value))
            {
                return result;
            }
            result.provider = setting.UofM;
            result.username = setting.Value;
            return result;
        }
    }
}
