// <autogenerated>
// <copyright file="Messaging.ISearchModels.cs" company="clarity-ventures.com">
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

    /// <content>The Email Queue Attachment SQL search extensions.</content>
    public static partial class EmailQueueAttachmentSQLSearchExtensions
    {
        /// <summary>An <see cref="IQueryable{EmailQueueAttachment}" /> extension method that filters  by each of the properties of
        /// the search model which have been set.</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The search model to filter by.</param>
        /// <returns>The <see cref="IQueryable{EmailQueueAttachment}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<EmailQueueAttachment> FilterEmailQueueAttachmentsBySearchModel(
                this IQueryable<EmailQueueAttachment> query,
                IEmailQueueAttachmentSearchModel model)
        {
            if (model == null)
            {
                return query;
            }
            var query2 = Contract.RequiresNotNull(query)
                .FilterByNameableBaseSearchModel(model)
                .FilterByIAmARelationshipTableBaseSearchModel<EmailQueueAttachment, EmailQueue, StoredFile>(model)
                .FilterIHaveSeoBaseBySearchModel(model)
                .FilterEmailQueueAttachmentsByCreatedByUserID(model.CreatedByUserID)
                .FilterEmailQueueAttachmentsByFileAccessTypeID(model.FileAccessTypeID)
                .FilterEmailQueueAttachmentsByUpdatedByUserID(model.UpdatedByUserID, model.UpdatedByUserIDIncludeNull)
                ;
            return query2;
        }

        #region CreatedByUserID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterEmailQueueAttachmentsByCreatedByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IEmailQueueAttachment
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedByUserID == parameter);
        }
        #endregion

        #region FileAccessTypeID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterEmailQueueAttachmentsByFileAccessTypeID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter)
            where TEntity : class, IEmailQueueAttachment
        {
            if (Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.FileAccessTypeID == parameter);
        }
        #endregion

        #region UpdatedByUserID
        /// <summary>An <see cref="IQueryable{TEntity}" /> extension method that filters records by the parameter (an identifier).</summary>
        /// <remarks>Pass a parameter value of null to not filter by this in a fluid call chain.</remarks>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter to filter by.</param>
        /// <param name="includeNull">If set, match the parameter even if it's null.</param>
        /// <returns>The <see cref="IQueryable{TEntity}" /> with an additional Where applied if the parameter has a value.</returns>
        public static IQueryable<TEntity> FilterEmailQueueAttachmentsByUpdatedByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IEmailQueueAttachment
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UpdatedByUserID == parameter);
        }
        #endregion
    }
}
