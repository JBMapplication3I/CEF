// <copyright file="Message.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A message search extensions.</summary>
    public static class MessageSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter messages by conversation identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterMessagesByConversationID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IMessage
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ConversationID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter messages by sent from or to user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterMessagesBySentFromOrToUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IMessage
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SentByUserID == id!.Value
                    || x.MessageRecipients!.Any(y => y.Active && y.SlaveID == id.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter messages by sent from user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterMessagesBySentFromUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IMessage
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SentByUserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter messages by sent to user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterMessagesBySentToUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IMessage
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MessageRecipients!.Any(y => y.Active && y.SlaveID == id!.Value));
        }
    }
}
