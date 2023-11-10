// <copyright file="AmFilterableByVendorMasterTSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am filterable by vendor T-SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>An am filterable by vendor master tsql search extensions.</summary>
    public static class AmFilterableByVendorMasterTSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i am filterable by master vendors by search
        /// model.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TVendorLink">Type of the vendor link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterIAmFilterableByMasterVendorsBySearchModel<TEntity, TVendorLink>(
                this IQueryable<TEntity> query,
                IAmFilterableByVendorSearchModel model)
            where TEntity : class, IAmFilterableByVendor<TVendorLink>
            where TVendorLink : class, IAmAVendorRelationshipTableWhereVendorIsTheMaster<TEntity>
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByVendorID<TEntity, TVendorLink>(model.VendorID)
                .FilterByVendorKey<TEntity, TVendorLink>(model.VendorKey)
                .FilterByVendorName<TEntity, TVendorLink>(model.VendorName);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by vendor identifier.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TVendorLink">Type of the vendor link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByVendorID<TEntity, TVendorLink>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IAmFilterableByVendor<TVendorLink>
            where TVendorLink : class, IAmAVendorRelationshipTableWhereVendorIsTheMaster<TEntity>
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendors!
                    .Any(y => y.Active && y.Master!.Active && y.MasterID == id));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by vendor key.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TVendorLink">Type of the vendor link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByVendorKey<TEntity, TVendorLink>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, IAmFilterableByVendor<TVendorLink>
            where TVendorLink : class, IAmAVendorRelationshipTableWhereVendorIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendors!
                    .Any(y => y.Active && y.Master!.Active && y.Master.CustomKey == key));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter by vendor name.</summary>
        /// <typeparam name="TEntity">   Type of the entity.</typeparam>
        /// <typeparam name="TVendorLink">Type of the vendor link.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterByVendorName<TEntity, TVendorLink>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, IAmFilterableByVendor<TVendorLink>
            where TVendorLink : class, IAmAVendorRelationshipTableWhereVendorIsTheMaster<TEntity>
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Vendors!
                    .Any(y => y.Active && y.Master!.Active && y.Master.Name == name));
        }
    }
}
