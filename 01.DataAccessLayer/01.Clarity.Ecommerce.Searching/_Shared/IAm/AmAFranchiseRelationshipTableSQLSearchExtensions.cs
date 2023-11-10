// <copyright file="AmAFranchiseRelationshipTableSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a franchise relationship table SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am a franchise relationship table SQL search extensions.</summary>
    public static class AmAFranchiseRelationshipTableSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter relationships by master franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TAlt">   Type of the alternate.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRelationshipsByMasterFranchiseID<TEntity, TAlt>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<TAlt>
            where TAlt : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter relationships by slave franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TAlt">   Type of the alternate.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRelationshipsBySlaveFranchiseID<TEntity, TAlt>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<TAlt>
            where TAlt : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SlaveID == id);
        }
    }
}
