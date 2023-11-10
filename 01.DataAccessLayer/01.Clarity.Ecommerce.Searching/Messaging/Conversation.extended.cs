// <copyright file="Conversation.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A conversation search extensions.</summary>
    public static class ConversationSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter conversations by has ended.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="has">  The has.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterConversationsByHasEnded<TEntity>(this IQueryable<TEntity> query, bool? has)
            where TEntity : class, IConversation
        {
            Contract.RequiresNotNull(query);
            if (!has.HasValue)
            {
                return query;
            }
            return query.Where(x => x.HasEnded == has.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter conversations by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterConversationsByUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IConversation
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.Users!.Any(y => y.Active && y.SlaveID == id!.Value));
        }
    }
}
