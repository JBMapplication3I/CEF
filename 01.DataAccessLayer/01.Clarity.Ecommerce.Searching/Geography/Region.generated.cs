// <autogenerated>
// <copyright file="Geography.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Region SQL search extensions.</content>
    public static partial class RegionSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{Region}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{Region}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<Region> FilterRegionsBySearchModel(
                this IQueryable<Region> query,
                IRegionSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterRegionsByCountryID(model.CountryID)
                .FilterRegionsByCode(model.Code, model.CodeStrict, model.CodeIncludeNull)
                .FilterRegionsByISO31661(model.ISO31661, model.ISO31661Strict, model.ISO31661IncludeNull)
                .FilterRegionsByISO31662(model.ISO31662, model.ISO31662Strict, model.ISO31662IncludeNull)
                .FilterRegionsByISO3166Alpha2(model.ISO3166Alpha2, model.ISO3166Alpha2Strict, model.ISO3166Alpha2IncludeNull)
                ;
            return query2;
        }

        #region CountryID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterRegionsByCountryID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IRegion
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CountryID == parameter);
        }
        #endregion

        #region Code
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterRegionsByCode<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IRegion
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Code == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Code == null || x.Code == string.Empty);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Code != null && x.Code.Contains(search));
        }
        #endregion

        #region ISO31661
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterRegionsByISO31661<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IRegion
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO31661 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO31661 == null || x.ISO31661 == string.Empty);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ISO31661 != null && x.ISO31661.Contains(search));
        }
        #endregion

        #region ISO31662
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterRegionsByISO31662<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IRegion
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO31662 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO31662 == null || x.ISO31662 == string.Empty);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ISO31662 != null && x.ISO31662.Contains(search));
        }
        #endregion

        #region ISO3166Alpha2
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterRegionsByISO3166Alpha2<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IRegion
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO3166Alpha2 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ISO3166Alpha2 == null || x.ISO3166Alpha2 == string.Empty);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.ISO3166Alpha2 != null && x.ISO3166Alpha2.Contains(search));
        }
        #endregion
    }
}
