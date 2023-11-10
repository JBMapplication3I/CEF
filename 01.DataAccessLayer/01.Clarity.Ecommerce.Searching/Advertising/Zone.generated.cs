// <autogenerated>
// <copyright file="Advertising.ISearchModels.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FilterBy Query Extensions generated to provide searching queries.</summary>
// <remarks>This file was auto-generated by FilterBys.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable PartialTypeWithSinglePart, RedundantUsingDirective, RegionWithSingleElement
#pragma warning disable 8669 // nullable reference types disabled
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <content>The Zone SQL search extensions.</content>
    public static partial class ZoneSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{Zone}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{Zone}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<Zone> FilterZonesBySearchModel(
                this IQueryable<Zone> query,
                IZoneSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterByHaveATypeSearchModel<Zone, ZoneType>(model)
                .FilterByHaveAStatusSearchModel<Zone, ZoneStatus>(model)
                .FilterZonesByMinHeight(model.MinHeight)
                .FilterZonesByMaxHeight(model.MaxHeight)
                .FilterZonesByMatchHeight(model.MatchHeight)
                .FilterZonesByMinWidth(model.MinWidth)
                .FilterZonesByMaxWidth(model.MaxWidth)
                .FilterZonesByMatchWidth(model.MatchWidth)
                ;
            return query2;
        }

        #region Height
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMinHeight<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Height >= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMaxHeight<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Height <= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a integer value to match).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMatchHeight<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Height == parameter);
        }
        #endregion

        #region Width
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMinWidth<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Width >= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMaxWidth<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Width <= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a integer value to match).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterZonesByMatchWidth<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IZone
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Width == parameter);
        }
        #endregion
    }
}
