// <copyright file="AmAStoreRelationshipTableSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a store relationship table SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am a store relationship table SQL search extensions.</summary>
    public static class AmAStoreRelationshipTableSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter relationships by master store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TAlt">   Type of the alternate.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRelationshipsByMasterStoreID<TEntity, TAlt>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmAStoreRelationshipTableWhereStoreIsTheMaster<TAlt>
            where TAlt : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master!.Active && x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter relationships by slave store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TAlt">   Type of the alternate.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRelationshipsBySlaveStoreID<TEntity, TAlt>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmAStoreRelationshipTableWhereStoreIsTheSlave<TAlt>
            where TAlt : class, IBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Slave!.Active && x.SlaveID == id);
        }
    }
}
