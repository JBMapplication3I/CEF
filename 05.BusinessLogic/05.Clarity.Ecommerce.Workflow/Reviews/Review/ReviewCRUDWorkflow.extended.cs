// <copyright file="ReviewCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the review workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A review workflow.</summary>
    /// <seealso cref="NameableWorkflowBase{IReviewModel, IReviewSearchModel, IReview, Review}"/>
    /// <seealso cref="Interfaces.Workflow.IReviewWorkflow"/>
    public partial class ReviewWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> ApproveAsync(int id, int userID, string? contextProfileName)
        {
            Contract.RequiresAllValidIDs(id, userID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Reviews.FilterByID(id).SingleOrDefaultAsync().ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found");
            }
            if (entity.Approved)
            {
                // Do Nothing
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            entity.Approved = true;
            entity.UpdatedDate = timestamp;
            entity.ApprovedDate = timestamp;
            entity.ApprovedByUserID = userID;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Something about editing this record failed");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UnapproveAsync(int id, int userID, string? contextProfileName)
        {
            Contract.RequiresAllValidIDs(id, userID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Reviews.FilterByID(id).SingleOrDefaultAsync().ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found");
            }
            if (!entity.Approved)
            {
                // Do Nothing
                return CEFAR.PassingCEFAR();
            }
            entity.Approved = false;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            entity.ApprovedByUserID = null; // Clear the assignment
            entity.ApprovedDate = null; // Clear the assignment
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Something about editing this record failed");
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IReview entity,
            IReviewModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Review Properties
            entity.UpdateReviewFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await DetermineTypeNameByReviewedObjectAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RelateSubmittedByUserAsync(entity, model.SubmittedByUserID, model.SubmittedByUserUserName, model.SubmittedByUser, context.ContextProfileName).ConfigureAwait(false);
            await RelateApprovedByUserAsync(entity, model.ApprovedByUserID, model.ApprovedByUserUserName, model.ApprovedByUser, context.ContextProfileName).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <summary>Override type data on model.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="typeKey">The type key.</param>
        private static void OverrideTypeDataOnModel(IReviewModel model, string typeKey)
        {
            model.TypeID = 0;
            model.Type = null;
            model.TypeName = null;
            model.TypeKey = typeKey;
        }

        /// <summary>Relate submitted by user.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task RelateSubmittedByUserAsync(
            IReview entity,
            int? userID,
            string? userName,
            IUserModel? user,
            string? contextProfileName)
        {
            if (Contract.CheckValidID(userID))
            {
                entity.SubmittedByUserID = userID!.Value;
                return;
            }
            if (Contract.CheckValidID(user?.ID))
            {
                entity.SubmittedByUserID = user!.ID;
                return;
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var lookedUpUserID = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserNameOrCustomKey(userName, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync().ConfigureAwait(false);
                if (lookedUpUserID.HasValue)
                {
                    entity.SubmittedByUserID = lookedUpUserID.Value;
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(user?.UserName))
            {
                return;
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var lookedUpUserID = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserNameOrCustomKey(user!.UserName, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync().ConfigureAwait(false);
                if (lookedUpUserID.HasValue)
                {
                    entity.SubmittedByUserID = lookedUpUserID.Value;
                }
            }
        }

        /// <summary>Relate approved by user.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task RelateApprovedByUserAsync(
            IReview entity,
            int? userID,
            string? userName,
            IUserModel? user,
            string? contextProfileName)
        {
            if (Contract.CheckValidID(userID))
            {
                entity.ApprovedByUserID = userID!.Value;
                return;
            }
            if (Contract.CheckValidID(user?.ID))
            {
                entity.ApprovedByUserID = user!.ID;
                return;
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var lookedUpUserID = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserNameOrCustomKey(userName, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync().ConfigureAwait(false);
                if (lookedUpUserID.HasValue)
                {
                    entity.ApprovedByUserID = lookedUpUserID.Value;
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(user?.UserName))
            {
                return;
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var lookedUpUserID = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserNameOrCustomKey(user!.UserName, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync().ConfigureAwait(false);
                if (lookedUpUserID.HasValue)
                {
                    entity.ApprovedByUserID = lookedUpUserID.Value;
                }
            }
        }

        /// <summary>Executes all relate workflows operations with the default information.</summary>
        /// <remarks>Perform any extra resolvers before running this function.</remarks>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedRelateWorkflowsAsync(
            IReview entity,
            IReviewModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            await RelateOptionalCategoryAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalManufacturerAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalProductAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalStoreAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalUserAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalVendorAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Determine type name by reviewed object.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity
        private Task DetermineTypeNameByReviewedObjectAsync(
            IReview entity,
            IReviewModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            if (model.CategoryID > 0
                || !string.IsNullOrWhiteSpace(model.Category?.CustomKey ?? model.CategoryKey)
                || !string.IsNullOrWhiteSpace(model.Category?.Name ?? model.CategoryName))
            {
                OverrideTypeDataOnModel(model, "Category");
            }
            else if (model.ProductID > 0
                || !string.IsNullOrWhiteSpace(model.Product?.CustomKey ?? model.ProductKey)
                || !string.IsNullOrWhiteSpace(model.Product?.Name ?? model.ProductName))
            {
                OverrideTypeDataOnModel(model, "Product");
            }
            else if (model.ManufacturerID > 0
                || !string.IsNullOrWhiteSpace(model.Manufacturer?.CustomKey ?? model.ManufacturerKey)
                || !string.IsNullOrWhiteSpace(model.Manufacturer?.Name ?? model.ManufacturerName))
            {
                OverrideTypeDataOnModel(model, "Manufacturer");
            }
            else if (model.StoreID > 0
                || !string.IsNullOrWhiteSpace(model.Store?.CustomKey ?? model.StoreKey)
                || !string.IsNullOrWhiteSpace(model.Store?.Name ?? model.StoreName))
            {
                OverrideTypeDataOnModel(model, "Store");
            }
            else if (model.UserID > 0
                || !string.IsNullOrWhiteSpace(model.User?.CustomKey ?? model.UserKey)
                || !string.IsNullOrWhiteSpace(model.User?.UserName ?? model.UserUserName))
            {
                OverrideTypeDataOnModel(model, "User");
            }
            else if (model.VendorID > 0
                || !string.IsNullOrWhiteSpace(model.Vendor?.CustomKey ?? model.VendorKey)
                || !string.IsNullOrWhiteSpace(model.Vendor?.Name ?? model.VendorName))
            {
                OverrideTypeDataOnModel(model, "Vendor");
            }
            // Actually relate the type
            return RelateRequiredTypeAsync(entity, model, timestamp, contextProfileName);
        }
    }
}
