// <autogenerated>
// <copyright file="Attributes.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The General Attribute SQL search extensions.</content>
    public static partial class GeneralAttributeSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{GeneralAttribute}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{GeneralAttribute}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<GeneralAttribute> FilterGeneralAttributesBySearchModel(
                this IQueryable<GeneralAttribute> query,
                IGeneralAttributeSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByTypableBaseSearchModel(model)
                .FilterByHaveATypeSearchModel<GeneralAttribute, AttributeType>(model)
                .FilterGeneralAttributesByHideFromCatalogViews(model.HideFromCatalogViews)
                .FilterGeneralAttributesByHideFromProductDetailView(model.HideFromProductDetailView)
                .FilterGeneralAttributesByHideFromStorefront(model.HideFromStorefront)
                .FilterGeneralAttributesByHideFromSuppliers(model.HideFromSuppliers)
                .FilterGeneralAttributesByIsComparable(model.IsComparable)
                .FilterGeneralAttributesByIsFilter(model.IsFilter)
                .FilterGeneralAttributesByIsMarkup(model.IsMarkup)
                .FilterGeneralAttributesByIsPredefined(model.IsPredefined)
                .FilterGeneralAttributesByIsTab(model.IsTab)
                .FilterGeneralAttributesByAttributeGroupID(model.AttributeGroupID, model.AttributeGroupIDIncludeNull)
                .FilterGeneralAttributesByAttributeTabID(model.AttributeTabID, model.AttributeTabIDIncludeNull)
                ;
            return query2;
        }

        #region HideFromCatalogViews
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByHideFromCatalogViews<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.HideFromCatalogViews == parameter);
        }
        #endregion

        #region HideFromProductDetailView
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByHideFromProductDetailView<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.HideFromProductDetailView == parameter);
        }
        #endregion

        #region HideFromStorefront
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByHideFromStorefront<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.HideFromStorefront == parameter);
        }
        #endregion

        #region HideFromSuppliers
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByHideFromSuppliers<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.HideFromSuppliers == parameter);
        }
        #endregion

        #region IsComparable
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByIsComparable<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IsComparable == parameter);
        }
        #endregion

        #region IsFilter
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByIsFilter<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IsFilter == parameter);
        }
        #endregion

        #region IsMarkup
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByIsMarkup<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IsMarkup == parameter);
        }
        #endregion

        #region IsPredefined
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByIsPredefined<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IsPredefined == parameter);
        }
        #endregion

        #region IsTab
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a boolean value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByIsTab<TEntity>(
                this IQueryable<TEntity> query,
                bool? parameter)
            where TEntity : class, IGeneralAttribute
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IsTab == parameter);
        }
        #endregion

        #region AttributeGroupID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByAttributeGroupID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IGeneralAttribute
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AttributeGroupID == parameter);
        }
        #endregion

        #region AttributeTabID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterGeneralAttributesByAttributeTabID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IGeneralAttribute
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AttributeTabID == parameter);
        }
        #endregion
    }
}
