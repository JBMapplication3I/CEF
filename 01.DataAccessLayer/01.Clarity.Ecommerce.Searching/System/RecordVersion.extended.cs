// <copyright file="RecordVersion.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the record version search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A record version search extensions.</summary>
    public static class RecordVersionSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter record versions by search model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterRecordVersionsBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IRecordVersionSearchModel model)
            where TEntity : class, IRecordVersion
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterByHaveATypeSearchModel<TEntity, Ecommerce.DataModel.RecordVersionType>(model)
                .FilterByIAmFilterableByNullableStoreSearchModel(model)
                .FilterByIAmFilterableByNullableBrandSearchModel(model)
                .FilterRecordVersionsByIsDraft(model.IsDraft)
                .FilterRecordVersionsByRecordID(model.RecordID)
                .FilterRecordVersionsByPublishedByUserID(model.PublishedByUserID)
                .FilterRecordVersionsByOriginalPublishMinDate(model.MinOriginalPublishDate)
                .FilterRecordVersionsByOriginalPublishMaxDate(model.MaxOriginalPublishDate)
                .FilterRecordVersionsByMostRecentPublishMinDate(model.MinMostRecentPublishDate)
                .FilterRecordVersionsByMostRecentPublishMaxDate(model.MaxMostRecentPublishDate)
                .FilterRecordVersionsByEitherPublishMinDate(model.MinEitherPublishDate)
                .FilterRecordVersionsByEitherPublishMaxDate(model.MaxEitherPublishDate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by either publish minimum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByEitherPublishMinDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalPublishDate.HasValue && x.OriginalPublishDate >= date!.Value
                         || x.MostRecentPublishDate.HasValue && x.MostRecentPublishDate >= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by either publish maximum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByEitherPublishMaxDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalPublishDate.HasValue && x.OriginalPublishDate <= date!.Value
                         || x.MostRecentPublishDate.HasValue && x.MostRecentPublishDate <= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by original publish minimum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByOriginalPublishMinDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalPublishDate.HasValue && x.OriginalPublishDate >= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by original publish maximum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByOriginalPublishMaxDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalPublishDate.HasValue && x.OriginalPublishDate <= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by most recent publish minimum
        /// date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByMostRecentPublishMinDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MostRecentPublishDate.HasValue && x.MostRecentPublishDate >= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by most recent publish maximum
        /// date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="date"> The date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByMostRecentPublishMaxDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? date)
            where TEntity : class, IRecordVersion
        {
            if (!Contract.CheckValidDate(date))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MostRecentPublishDate.HasValue && x.MostRecentPublishDate <= date!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by published by user
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByPublishedByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRecordVersion
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.PublishedByUserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter record versions by record identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterRecordVersionsByRecordID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, IRecordVersion
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.RecordID == id!.Value);
        }
    }
}
