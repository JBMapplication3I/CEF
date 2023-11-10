// <copyright file="ConversationUser.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A conversation user search extensions.</summary>
    public static class ConversationUserSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter conversation users by conversation
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterConversationUsersByConversationID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IConversationUser
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.MasterID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter conversation users by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterConversationUsersByUserID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IConversationUser
        {
            Contract.RequiresNotNull(query);
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return query.Where(x => x.SlaveID == id!.Value);
        }
    }
}
