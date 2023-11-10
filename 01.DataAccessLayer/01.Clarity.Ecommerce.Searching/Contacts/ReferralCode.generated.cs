// <autogenerated>
// <copyright file="Contacts.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Referral Code SQL search extensions.</content>
    public static partial class ReferralCodeSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{ReferralCode}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{ReferralCode}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<ReferralCode> FilterReferralCodesBySearchModel(
                this IQueryable<ReferralCode> query,
                IReferralCodeSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterByHaveATypeSearchModel<ReferralCode, ReferralCodeType>(model)
                .FilterByHaveAStatusSearchModel<ReferralCode, ReferralCodeStatus>(model)
                .FilterReferralCodesByUserID(model.UserID)
                .FilterReferralCodesByCode(model.Code, model.CodeStrict, model.CodeIncludeNull)
                ;
            return query2;
        }

        #region UserID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterReferralCodesByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IReferralCode
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == parameter);
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
        public static IQueryable<TEntity> FilterReferralCodesByCode<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IReferralCode
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
    }
}
