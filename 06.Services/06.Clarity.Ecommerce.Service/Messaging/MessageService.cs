// <copyright file="MessageService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A get messages for current user.</summary>
    /// <seealso cref="MessageSearchModel"/>
    /// <seealso cref="IReturn{MessagePagedResults}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/Messages", "POST", Summary = "Use to get messages")]
    public partial class GetMessagesForCurrentUser : MessageSearchModel, IReturn<MessagePagedResults>
    {
    }

    /// <summary>A get messages from current user.</summary>
    /// <seealso cref="MessageSearchModel"/>
    /// <seealso cref="IReturn{MessagePagedResults}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/Messages/From", "POST", Summary = "Use to get messages")]
    public partial class GetMessagesFromCurrentUser : MessageSearchModel, IReturn<MessagePagedResults>
    {
    }

    /// <summary>A get messages to current user.</summary>
    /// <seealso cref="MessageSearchModel"/>
    /// <seealso cref="IReturn{MessagePagedResults}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/Messages/To", "POST", Summary = "Use to get messages")]
    public partial class GetMessagesToCurrentUser : MessageSearchModel, IReturn<MessagePagedResults>
    {
    }

    /// <summary>A get messages for active conversations for current user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/Messages/ActiveConversations", "GET",
            Summary = "Use to get all messages for all non-ended conversations with the current user. Include a PostedSince to limit by time-frame for monitoring calls.")]
    public partial class GetMessagesForActiveConversationsForCurrentUser
        : IReturn<CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>>
    {
        [ApiMember(Name = nameof(PostedSince), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? PostedSince { get; set; }
    }

    /// <summary>A get messages for ended conversations for current user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/Messages/EndedConversations", "GET",
            Summary = "Use to get all messages for all ended conversations with the current user. Include a PostedSince to limit by time-frame for monitoring calls.")]
    public partial class GetMessagesForEndedConversationsForCurrentUser
        : IReturn<CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>>
    {
        [ApiMember(Name = nameof(ID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? ID { get; set; }

        [ApiMember(Name = nameof(PostedSince), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? PostedSince { get; set; }
    }

    public partial class MessageService
    {
        public async Task<object?> Post(GetMessagesForCurrentUser request)
        {
            request.SentFromOrToUserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<IMessageModel, MessageModel, IMessageSearchModel, MessagePagedResults>(request, request.AsListing, Workflows.Messages).ConfigureAwait(false);
        }

        public async Task<object?> Post(GetMessagesFromCurrentUser request)
        {
            request.SentFromUserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<IMessageModel, MessageModel, IMessageSearchModel, MessagePagedResults>(request, request.AsListing, Workflows.Messages).ConfigureAwait(false);
        }

        public async Task<object?> Post(GetMessagesToCurrentUser request)
        {
            request.SentToUserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<IMessageModel, MessageModel, IMessageSearchModel, MessagePagedResults>(request, request.AsListing, Workflows.Messages).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetMessagesForActiveConversationsForCurrentUser request)
        {
            // TODO: Cached Research
            var userID = CurrentUserIDOrThrow401;
            var results = await Workflows.Conversations.GetMessagesForActiveConversationsAsync(userID, request.PostedSince, null).ConfigureAwait(false);
            if (!results.ActionSucceeded)
            {
                return new CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>(results);
            }
            return new CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>(results)
            {
                Result = results.Result!.ToList().ToDictionary(x => x.Key, v => v.Value.Cast<MessageModel>().ToList()),
            };
        }

        public async Task<object?> Get(GetMessagesForEndedConversationsForCurrentUser request)
        {
            // TODO: Cached Research
            var userID = CurrentUserIDOrThrow401;
            var results = await Workflows.Conversations.GetMessagesForEndedConversationsAsync(userID, request.PostedSince, request.ID, null).ConfigureAwait(false);
            if (!results.ActionSucceeded)
            {
                return new CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>(results);
            }
            return new CEFActionResponse<Dictionary<int /*ConversationID*/, List<MessageModel>>>(results)
            {
                Result = results.Result!.ToList().ToDictionary(x => x.Key, v => v.Value.Cast<MessageModel>().ToList()),
            };
        }
    }
}
