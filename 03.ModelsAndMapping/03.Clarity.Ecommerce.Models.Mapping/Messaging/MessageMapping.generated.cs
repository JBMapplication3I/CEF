// <autogenerated>
// <copyright file="Mapping.Messaging.Message.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Messaging section of the Mapping class</summary>
// <remarks>This file was auto-generated by Mapping.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow, InvokeAsExtensionMethod, MergeCastWithTypeCheck
// ReSharper disable MissingLinebreak, RedundantDelegateInvoke, RedundantUsingDirective
#pragma warning disable CS0618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using LinqKit;
    using MoreLinq;
    using Utilities;

    public static partial class ModelMapperForMessage
    {
        public sealed class AnonMessage : Message
        {
            public new IEnumerable<MessageAttachment>? MessageAttachments { get; set; }
            public new IEnumerable<MessageRecipient>? MessageRecipients { get; set; }
            public Contact? SentByUserContact { get; set; }
        }

        public static readonly Func<Message?, string?, IMessageModel?> MapMessageModelFromEntityFull = CreateMessageModelFromEntityFull;

        public static readonly Func<Message?, string?, IMessageModel?> MapMessageModelFromEntityLite = CreateMessageModelFromEntityLite;

        public static readonly Func<Message?, string?, IMessageModel?> MapMessageModelFromEntityList = CreateMessageModelFromEntityList;

        public static Func<IMessage, IMessageModel, string?, IMessageModel>? CreateMessageModelFromEntityHooksFull { get; set; }

        public static Func<IMessage, IMessageModel, string?, IMessageModel>? CreateMessageModelFromEntityHooksLite { get; set; }

        public static Func<IMessage, IMessageModel, string?, IMessageModel>? CreateMessageModelFromEntityHooksList { get; set; }

        public static Expression<Func<Message, AnonMessage>>? PreBuiltMessageSQLSelectorFull { get; set; }

        public static Expression<Func<Message, AnonMessage>>? PreBuiltMessageSQLSelectorLite { get; set; }

        public static Expression<Func<Message, AnonMessage>>? PreBuiltMessageSQLSelectorList { get; set; }

        /// <summary>An <see cref="IMessageModel"/> extension method that creates a(n) <see cref="Message"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="Message"/> entity.</returns>
        public static IMessage CreateMessageEntity(
            this IMessageModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IMessageModel, Message>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateMessageFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IMessageModel"/> extension method that updates a(n) <see cref="Message"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="Message"/> entity.</returns>
        public static IMessage UpdateMessageFromModel(
            this IMessage entity,
            IMessageModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Message Properties
            entity.Body = model.Body;
            entity.Context = model.Context;
            entity.IsReplyAllAllowed = model.IsReplyAllAllowed;
            entity.Subject = model.Subject;
            // Message's Related Objects
            // Message's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenMessageSQLSelectorFull()
        {
            PreBuiltMessageSQLSelectorFull = x => x == null ? null! : new AnonMessage
            {
                StoreID = x.StoreID,
                Store = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Store!),
                BrandID = x.BrandID,
                Brand = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Brand!),
                Subject = x.Subject,
                Context = x.Context,
                Body = x.Body,
                IsReplyAllAllowed = x.IsReplyAllAllowed,
                ConversationID = x.ConversationID,
                Conversation = ModelMapperForConversation.PreBuiltConversationSQLSelectorList.Expand().Compile().Invoke(x.Conversation!),
                SentByUserID = x.SentByUserID,
                SentByUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.SentByUser!),
                MessageRecipients = x.MessageRecipients!.Where(y => y.Active).Select(ModelMapperForMessageRecipient.PreBuiltMessageRecipientSQLSelectorList.Expand().Compile()).ToList(),
                MessageAttachments = x.MessageAttachments!.Where(y => y.Active).Select(ModelMapperForMessageAttachment.PreBuiltMessageAttachmentSQLSelectorList.Expand().Compile()).ToList(),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenMessageSQLSelectorLite()
        {
            PreBuiltMessageSQLSelectorLite = x => x == null ? null! : new AnonMessage
            {
                StoreID = x.StoreID,
                Store = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Store!),
                BrandID = x.BrandID,
                Brand = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Brand!),
                Subject = x.Subject,
                Context = x.Context,
                Body = x.Body,
                IsReplyAllAllowed = x.IsReplyAllAllowed,
                ConversationID = x.ConversationID,
                Conversation = ModelMapperForConversation.PreBuiltConversationSQLSelectorList.Expand().Compile().Invoke(x.Conversation!),
                SentByUserID = x.SentByUserID,
                SentByUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.SentByUser!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenMessageSQLSelectorList()
        {
            PreBuiltMessageSQLSelectorList = x => x == null ? null! : new AnonMessage
            {
                StoreID = x.StoreID,
                Store = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Store!), // For Flattening Properties (List)
                BrandID = x.BrandID,
                Brand = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Brand!), // For Flattening Properties (List)
                Subject = x.Subject,
                Context = x.Context,
                Body = x.Body,
                IsReplyAllAllowed = x.IsReplyAllAllowed,
                ConversationID = x.ConversationID,
                Conversation = ModelMapperForConversation.PreBuiltConversationSQLSelectorList.Expand().Compile().Invoke(x.Conversation!), // For Flattening Properties (List)
                SentByUserID = x.SentByUserID,
                SentByUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.SentByUser!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                SentByUserContact = x.SentByUser == null ? null : ModelMapperForContact.PreBuiltContactSQLSelectorList.Expand().Compile().Invoke(x.SentByUser.Contact!), // For Flattening Properties
            };
        }

        public static IEnumerable<IMessageModel> SelectFullMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMessageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IMessageModel> SelectLiteMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMessageSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IMessageModel> SelectListMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMessageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityList(x, contextProfileName))!;
        }

        public static IMessageModel? SelectFirstFullMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMessageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IMessageModel? SelectFirstListMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMessageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IMessageModel? SelectSingleFullMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMessageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IMessageModel? SelectSingleLiteMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMessageSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IMessageModel? SelectSingleListMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMessageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMessageModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IMessageModel> results, int totalPages, int totalCount) SelectFullMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMessageSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateMessageModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IMessageModel> results, int totalPages, int totalCount) SelectLiteMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMessageSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateMessageModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IMessageModel> results, int totalPages, int totalCount) SelectListMessageAndMapToMessageModel(
            this IQueryable<Message> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMessageSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMessageSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateMessageModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IMessageModel? CreateMessageModelFromEntityFull(this IMessage? entity, string? contextProfileName)
        {
            return CreateMessageModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IMessageModel? CreateMessageModelFromEntityLite(this IMessage? entity, string? contextProfileName)
        {
            return CreateMessageModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IMessageModel? CreateMessageModelFromEntityList(this IMessage? entity, string? contextProfileName)
        {
            return CreateMessageModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IMessageModel? CreateMessageModelFromEntity(
            this IMessage? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IMessageModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Message's Properties
                    // Message's Related Objects
                    model.Brand = ModelMapperForBrand.CreateBrandModelFromEntityLite(entity.Brand, contextProfileName);
                    model.Conversation = ModelMapperForConversation.CreateConversationModelFromEntityLite(entity.Conversation, contextProfileName);
                    model.SentByUser = ModelMapperForUser.CreateUserModelFromEntityLite(entity.SentByUser, contextProfileName);
                    model.Store = ModelMapperForStore.CreateStoreModelFromEntityLite(entity.Store, contextProfileName);
                    // Message's Associated Objects
                    model.MessageAttachments = (entity is AnonMessage ? ((AnonMessage)entity).MessageAttachments : entity.MessageAttachments)?.Where(x => x.Active).Select(x => ModelMapperForMessageAttachment.CreateMessageAttachmentModelFromEntityList(x, contextProfileName)).ToList()!;
                    model.MessageRecipients = (entity is AnonMessage ? ((AnonMessage)entity).MessageRecipients : entity.MessageRecipients)?.Where(x => x.Active).Select(x => ModelMapperForMessageRecipient.CreateMessageRecipientModelFromEntityList(x, contextProfileName)).ToList()!;
                    // Additional Mappings
                    if (CreateMessageModelFromEntityHooksFull != null) { model = CreateMessageModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Message's Properties
                    // Message's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // Message's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateMessageModelFromEntityHooksLite != null) { model = CreateMessageModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // Message's Properties
                    model.Body = entity.Body;
                    model.Context = entity.Context;
                    model.IsReplyAllAllowed = entity.IsReplyAllAllowed;
                    model.Subject = entity.Subject;
                    // Message's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.BrandID = entity.BrandID;
                    model.BrandKey = entity.Brand?.CustomKey;
                    model.BrandName = entity.Brand?.Name;
                    model.ConversationID = entity.ConversationID;
                    model.ConversationKey = entity.Conversation?.CustomKey;
                    model.SentByUserID = entity.SentByUserID;
                    model.SentByUserKey = entity.SentByUser?.CustomKey;
                    model.SentByUserUserName = entity.SentByUser?.UserName;
                    model.SentByUserContactFirstName = entity is AnonMessage ? ((AnonMessage)entity).SentByUserContact?.FirstName : entity.SentByUser?.Contact?.FirstName;
                    model.SentByUserContactLastName = entity is AnonMessage ? ((AnonMessage)entity).SentByUserContact?.LastName : entity.SentByUser?.Contact?.LastName;
                    model.StoreID = entity.StoreID;
                    model.StoreKey = entity.Store?.CustomKey;
                    model.StoreName = entity.Store?.Name;
                    model.StoreSeoUrl = entity.Store?.SeoUrl;
                    // Message's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateMessageModelFromEntityHooksList != null) { model = CreateMessageModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
