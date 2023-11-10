// <autogenerated>
// <copyright file="Tracking.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Visitor SQL search extensions.</content>
    public static partial class VisitorSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{Visitor}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{Visitor}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<Visitor> FilterVisitorsBySearchModel(
                this IQueryable<Visitor> query,
                IVisitorSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterVisitorsByAddressID(model.AddressID, model.AddressIDIncludeNull)
                .FilterVisitorsByIPOrganizationID(model.IPOrganizationID, model.IPOrganizationIDIncludeNull)
                .FilterVisitorsByMinScore(model.MinScore)
                .FilterVisitorsByMaxScore(model.MaxScore)
                .FilterVisitorsByMatchScore(model.MatchScore, model.MatchScoreIncludeNull)
                .FilterVisitorsByUserID(model.UserID, model.UserIDIncludeNull)
                .FilterVisitorsByIPAddress(model.IPAddress, model.IPAddressStrict, model.IPAddressIncludeNull)
                ;
            return query2;
        }

        #region AddressID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByAddressID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IVisitor
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AddressID == parameter);
        }
        #endregion

        #region IPOrganizationID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByIPOrganizationID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IVisitor
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.IPOrganizationID == parameter);
        }
        #endregion

        #region Score
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a minimum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByMinScore<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IVisitor
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Score >= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a maximum integer value).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByMaxScore<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IVisitor
        {
            if (!parameter.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Score <= parameter);
        }

        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (a integer value to match).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByMatchScore<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IVisitor
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Score == parameter);
        }
        #endregion

        #region UserID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IVisitor
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == parameter);
        }
        #endregion

        #region IPAddress
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="strict">     If set, must match the string exactly (false or null will use a Contains/LIKE).</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterVisitorsByIPAddress<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IVisitor
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.IPAddress == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.IPAddress == null || x.IPAddress == string.Empty);
            }
            search = search!.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.IPAddress != null && x.IPAddress.Contains(search));
        }
        #endregion
    }
}
