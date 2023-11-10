// <copyright file="Group.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the group search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A group search extensions.</summary>
    public static class GroupSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter by group owner identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByGroupOwnerID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.GroupOwnerID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by member identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterByMemberID<TEntity>(this IQueryable<TEntity> query, int? id)
            where TEntity : class, IGroup
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Users!.Any(y => y.Active && y.SlaveID == id!.Value));
        }
    }
}
