// <copyright file="ConversationUserService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Messaging/ConversationUser/CurrentUser/SetTypingState/{IsTyping}", "POST",
            Summary = "Use to set the typing state while the user is typing.")]
    public partial class SetConversationUserTypingStateForCurrentUser : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(IsTyping), DataType = "bool", ParameterType = "path", IsRequired = true)]
        public bool IsTyping { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Messaging/ConversationUser/GetTypingStateForOtherConversationUsers", "GET",
            Summary = "Use to get the typing states for other users in your non-ended conversations.")]
    public partial class GetOtherConversationUsersTypingStatuses : IReturn<CEFActionResponse<List<UserTypingStatus>>>
    {
    }

    public partial class ConversationUserService
    {
        public async Task<object?> Post(SetConversationUserTypingStateForCurrentUser request)
        {
            return await Workflows.ConversationUsers.SetTypingStateAsync(CurrentUserIDOrThrow401, request.IsTyping, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetOtherConversationUsersTypingStatuses _)
        {
            return await Workflows.ConversationUsers.GetOtherConversationUsersTypingStatusesAsync(CurrentUserIDOrThrow401, null).ConfigureAwait(false);
        }
    }
}
