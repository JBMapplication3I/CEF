// <autogenerated>
// <copyright file="Invoicing.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Sales Invoice File SQL search extensions.</content>
    public static partial class SalesInvoiceFileSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{SalesInvoiceFile}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{SalesInvoiceFile}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<SalesInvoiceFile> FilterSalesInvoiceFilesBySearchModel(
                this IQueryable<SalesInvoiceFile> query,
                ISalesInvoiceFileSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterByIAmARelationshipTableBaseSearchModel<SalesInvoiceFile, SalesInvoice, StoredFile>(model)
                .FilterIHaveSeoBaseBySearchModel(model)
                .FilterSalesInvoiceFilesByFileAccessTypeID(model.FileAccessTypeID)
                ;
            return query2;
        }

        #region FileAccessTypeID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterSalesInvoiceFilesByFileAccessTypeID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, ISalesInvoiceFile
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.FileAccessTypeID == parameter);
        }
        #endregion
    }
}
