// <autogenerated>
// <copyright file="Discounts.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Applied Sales Order Discount SQL search extensions.</content>
    public static partial class AppliedSalesOrderDiscountSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{AppliedSalesOrderDiscount}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{AppliedSalesOrderDiscount}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<AppliedSalesOrderDiscount> FilterAppliedSalesOrderDiscountsBySearchModel(
                this IQueryable<AppliedSalesOrderDiscount> query,
                IAppliedSalesOrderDiscountSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByBaseSearchModel(model)
                .FilterByIAmARelationshipTableBaseSearchModel<AppliedSalesOrderDiscount, SalesOrder, Discount>(model)
                .FilterAppliedSalesOrderDiscountsByMinDiscountTotal(model.MinDiscountTotal)
                .FilterAppliedSalesOrderDiscountsByMaxDiscountTotal(model.MaxDiscountTotal)
                .FilterAppliedSalesOrderDiscountsByMatchDiscountTotal(model.MatchDiscountTotal)
                .FilterAppliedSalesOrderDiscountsByMinApplicationsUsed(model.MinApplicationsUsed)
                .FilterAppliedSalesOrderDiscountsByMaxApplicationsUsed(model.MaxApplicationsUsed)
                .FilterAppliedSalesOrderDiscountsByMatchApplicationsUsed(model.MatchApplicationsUsed, model.MatchApplicationsUsedIncludeNull)
                .FilterAppliedSalesOrderDiscountsByMinTargetApplicationsUsed(model.MinTargetApplicationsUsed)
                .FilterAppliedSalesOrderDiscountsByMaxTargetApplicationsUsed(model.MaxTargetApplicationsUsed)
                .FilterAppliedSalesOrderDiscountsByMatchTargetApplicationsUsed(model.MatchTargetApplicationsUsed, model.MatchTargetApplicationsUsedIncludeNull)
                ;
            return query2;
        }

        #region DiscountTotal
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMinDiscountTotal<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            var search = parameter.Value;
            return Contract.RequiresNotNull(query)
                .Where(x => x.DiscountTotal >= search);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum decimal value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">    The query to act on.</param>
        /// <param name="parameter">The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMaxDiscountTotal<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.DiscountTotal <= parameter.Value);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a decimal value to match exactly).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMatchDiscountTotal<TEntity>(
                this IQueryable<TEntity> query,
                decimal? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.DiscountTotal == parameter);
        }
        #endregion

        #region ApplicationsUsed
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMinApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ApplicationsUsed >= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMaxApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ApplicationsUsed <= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a integer value to match).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMatchApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ApplicationsUsed == parameter);
        }
        #endregion

        #region TargetApplicationsUsed
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMinTargetApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.TargetApplicationsUsed >= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMaxTargetApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.TargetApplicationsUsed <= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a integer value to match).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterAppliedSalesOrderDiscountsByMatchTargetApplicationsUsed<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IAppliedSalesOrderDiscount
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.TargetApplicationsUsed == parameter);
        }
        #endregion
    }
}
