// <copyright file="ConversationUserCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class ConversationUserWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetTypingStateAsync(
            int userID,
            bool isTyping,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var entity in context.ConversationUsers
                .AsNoTracking()
                .FilterByActive(true)
                .FilterConversationUsersByUserID(userID))
            {
                entity.LastHeartbeat = timestamp;
                entity.IsTyping = isTyping;
                entity.UpdatedDate = timestamp;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("Failed to save the data.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<UserTypingStatus>>> GetOtherConversationUsersTypingStatusesAsync(
            int userID,
            string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var results = (await context.Conversations
                        .FilterByActive(true)
                        .FilterConversationsByUserID(userID)
                        .FilterConversationsByHasEnded(false)
                        .SelectMany(x => x.Users!)
                        .FilterByActive(true)
                        .Select(x => new { x.SlaveID, x.Slave!.UserName, x.IsTyping })
                        .GroupBy(x => x.SlaveID)
                        .Select(x => new
                        {
                            UserID = x.Key,
                            UserName = x.FirstOrDefault() == null ? null : x.First().UserName,
                            Status = x.Any(y => y.IsTyping.HasValue && y.IsTyping.Value),
                        })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new UserTypingStatus
                    {
                        UserID = x.UserID,
                        UserName = x.UserName,
                        Status = x.Status,
                    })
                    .ToList();
                return results.WrapInPassingCEFAR()!;
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR<List<UserTypingStatus>>(ex.Message);
            }
        }
    }
}
