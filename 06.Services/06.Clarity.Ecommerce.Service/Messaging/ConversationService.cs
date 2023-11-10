// <copyright file="ConversationService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A get conversation headers for current user.</summary>
    /// <seealso cref="ConversationSearchModel"/>
    /// <seealso cref="IReturn{IConversationUserModel}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/CurrentUser/ConversationHeaders", "POST",
            Summary = "Use to get conversation headers for the current user")]
    public partial class GetConversationHeadersForCurrentUser : ConversationSearchModel, IReturn<ConversationPagedResults>
    {
    }

    /// <summary>An end conversation.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/Conversation/End/{ID}", "POST",
            Summary = "Use to set a conversation as ended. If it was set to copy, then an email of the conversation will be sent to the user.")]
    public partial class EndConversation : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Messaging/Conversation/InviteUser/{ID}/{UserID}", "PUT",
        Summary = "Use to set a conversation as ended. If it was set to copy, then an email of the conversation will be sent to the user.")]
    public class InviteUserToConversation : ImplementsIDBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true,
            Description = "The identifier of the user to invite to the conversation.")]
        public int UserID { get; set; }
    }

    /// <summary>A set conversation to copy.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/Conversation/SetToCopy/{ID}", "PUT",
            Summary = "Use to set a conversation that when it ends, it should send an email to the user")]
    public partial class SetConversationToCopy : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>Post a message to a conversation.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Messaging/Conversation/PostMessage", "POST",
            Summary = "Use to add a message to a conversation, all message recipients in the conversation will see the message.")]
    public partial class PostMessageToConversation : MessageModel, IReturn<CEFActionResponse<MessageModel>>
    {
    }

    public partial class ConversationService
    {
        public async Task<object?> Post(GetConversationHeadersForCurrentUser request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<IConversationModel, ConversationModel, IConversationSearchModel, ConversationPagedResults>(
                    request,
                    true,
                    Workflows.Conversations)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(SetConversationToCopy request)
        {
            return await Workflows.Conversations.SetToCopyAsync(Contract.RequiresValidID(request.ID), null).ConfigureAwait(false);
        }

        public async Task<object?> Post(InviteUserToConversation request)
        {
            return await Workflows.Conversations.InviteUserAsync(
                    Contract.RequiresValidID(request.ID),
                    Contract.RequiresValidID(request.UserID),
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(EndConversation request)
        {
            return await Workflows.Conversations.EndAsync(Contract.RequiresValidID(request.ID), null).ConfigureAwait(false);
        }

        public async Task<object?> Post(PostMessageToConversation request)
        {
            Contract.RequiresValidID(request.ConversationID);
            return await Workflows.Messages.PostMessageAsync(request, null).ConfigureAwait(false);
        }
    }
}
