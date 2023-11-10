// <copyright file="SalesCollectionBaseSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales collection base search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>The sales collection base search extensions.</summary>
    public static class SalesCollectionBaseSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by search model.</summary>
        /// <typeparam name="TEntity">           Type of the entity.</typeparam>
        /// <typeparam name="TStatus">           Type of the status.</typeparam>
        /// <typeparam name="TType">             Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">        Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">         Type of the discount.</typeparam>
        /// <typeparam name="TState">            Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">       Type of the stored file.</typeparam>
        /// <typeparam name="TContact">          Type of the contact.</typeparam>
        /// <typeparam name="TSalesItemDiscount">Type of the sales item discount.</typeparam>
        /// <typeparam name="TSalesItemTarget">  Type of the sales item target.</typeparam>
        /// <typeparam name="TSalesEvent">       Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">   Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsBySearchModel<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesItemDiscount, TSalesItemTarget, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                ISalesCollectionBaseSearchModel model)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : ISalesItemBase<TSalesItem, TSalesItemDiscount, TSalesItemTarget>
            where TSalesItemDiscount : IAppliedDiscountBase<TSalesItem, TSalesItemDiscount>
            where TSalesItemTarget : ISalesItemTargetBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (model == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                // Base
                .FilterByBaseSearchModel(model)
                // Type, Status or State
                .FilterByHaveATypeSearchModel<TEntity, TType>(model)
                .FilterByHaveAStatusSearchModel<TEntity, TStatus>(model)
                .FilterByHaveAStateSearchModel<TEntity, TState>(model)
                // Brands
                .FilterByIAmFilterableByNullableBrandSearchModel(model)
                // Franchises
                .FilterByIAmFilterableByNullableFranchiseSearchModel(model)
                // Stores
                .FilterByIAmFilterableByNullableStoreSearchModel(model)
                // Dates
                .FilterSalesCollectionsByDates(model.MinDate, model.MaxDate)
                // User
                .FilterSalesCollectionsByUserID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.UserID)
                .FilterSalesCollectionsByUserIDOrCustomKeyOrUserName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.UserIDOrCustomKeyOrUserName)
                .FilterSalesCollectionsByUserExternalID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.UserExternalID)
                // Account
                .FilterSalesCollectionsByAccountID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.AccountID)
                .FilterSalesCollectionsByAccountKey<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.AccountKey, false)
                .FilterSalesCollectionsByAccountIDOrCustomKeyOrNameOrDescription<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.AccountIDOrCustomKeyOrNameOrDescription)
                // Billing Contact Info
                .FilterSalesCollectionsByPhone<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.Phone)
                .FilterSalesCollectionsByEmail<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.Email)
                .FilterSalesCollectionsByBillingFirstName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.FirstName)
                .FilterSalesCollectionsByBillingLastName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.LastName)
                .FilterSalesCollectionsByPostalCode<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(model.PostalCode)
                // Product
                .FilterSalesCollectionsByProductKey<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType, TSalesItemDiscount, TSalesItemTarget>(model.ProductKey)
                .FilterSalesCollectionsByProductIDs<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType, TSalesItemDiscount, TSalesItemTarget>(model.ProductIDs);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by dates.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByDates<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? minDate,
                DateTime? maxDate)
            where TEntity : class, IBase
        {
            return Contract.RequiresNotNull(query)
                .FilterSalesCollectionsByMinDate(minDate)
                .FilterSalesCollectionsByMaxDate(maxDate);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by user identifier.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByUserID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == id!.Value
                         || x.Account != null && x.Account.Users!.Any(y => y.Active && y.ID == id.Value));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by user identifier or custom
        /// key or user name.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByUserIDOrCustomKeyOrUserName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? value)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(value))
            {
                return query;
            }
            var search = value!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID.ToString()!.Contains(search)
                         || x.User != null
                         && (x.User.CustomKey!.Contains(search)
                             || x.User.UserName!.Contains(search))
                         || x.Account != null
                         && x.Account.Users!
                             .Any(y => y.ID.ToString().Contains(search)
                                    || y.CustomKey!.Contains(search)
                                    || y.UserName!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by user external identifier.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">         The query to act on.</param>
        /// <param name="userExternalID">Identifier for the user external.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByUserExternalID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? userExternalID)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(userExternalID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                         && (x.User.UserName == userExternalID || x.User.CustomKey == userExternalID));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by account identifier.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByAccountID<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID == id!.Value
                    || x.User != null && x.User.AccountID == id.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by account key.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="key">   The key.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByAccountKey<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? key,
                bool strict)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            if (strict)
            {
                return query
                    .Where(x => x.Account != null && x.Account.CustomKey == key
                             || x.User != null && x.User.Account != null && x.User.Account.CustomKey == key);
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Account != null
                         && x.Account.CustomKey != null
                         && x.Account.CustomKey!.Contains(search)
                         || x.User != null
                         && x.User.Account != null
                         && x.User.Account.CustomKey != null
                         && x.User.Account.CustomKey!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by account identifier or
        /// custom key or name or description.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByAccountIDOrCustomKeyOrNameOrDescription<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? value)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(value))
            {
                return query;
            }
            var search = value!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountID.ToString()!.Contains(search)
                         || x.Account != null
                         && (x.Account.CustomKey!.Contains(search)
                             || x.Account.Name!.Contains(search)
                             || x.Account.Description!.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by phone.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="phone">The phone.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByPhone<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? phone)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(phone))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                         && x.User.Contact != null
                         && (x.User.Contact.Phone1 == phone
                             || x.User.Contact.Phone2 == phone
                             || x.User.Contact.Phone3 == phone));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by email.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="email">The email.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByEmail<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? email)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(email))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                         && x.User.Contact != null
                         && x.User.Contact.Email1 == email);
        }

        /// <summary>Filter sales collections by billing the first name.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByBillingFirstName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.BillingContact!.FirstName!.Contains(search));
        }

        /// <summary>Filter sales collections by billing the last name.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByBillingLastName<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.BillingContact!.LastName!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by postal code.</summary>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByPostalCode<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>(
                this IQueryable<TEntity> query,
                string? postalCode)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(postalCode))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                         && x.User.Contact != null
                         && x.User.Contact.Address != null
                         && x.User.Contact.Address.PostalCode != null
                         && x.User.Contact.Address.PostalCode.Contains(postalCode!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by product key.</summary>
        /// <typeparam name="TEntity">           Type of the entity.</typeparam>
        /// <typeparam name="TStatus">           Type of the status.</typeparam>
        /// <typeparam name="TType">             Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">        Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">         Type of the discount.</typeparam>
        /// <typeparam name="TState">            Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">       Type of the stored file.</typeparam>
        /// <typeparam name="TContact">          Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">       Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">   Type of the sales event type.</typeparam>
        /// <typeparam name="TSalesItemDiscount">Type of the sales item discount.</typeparam>
        /// <typeparam name="TSalesItemTarget">  Type of the sales item target.</typeparam>
        /// <param name="query">           The query to act on.</param>
        /// <param name="productCustomKey">The product custom key.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByProductKey<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType, TSalesItemDiscount, TSalesItemTarget>(
                this IQueryable<TEntity> query,
                string? productCustomKey)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : ISalesItemBase<TSalesItem, TSalesItemDiscount, TSalesItemTarget>
            where TSalesItemDiscount : IAppliedDiscountBase<TSalesItem, TSalesItemDiscount>
            where TSalesItemTarget : ISalesItemTargetBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (!Contract.CheckValidKey(productCustomKey))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SalesItems!
                    .Any(y => y.Active
                           && y.Product != null
                           && y.Product.CustomKey == productCustomKey));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by product i ds.</summary>
        /// <typeparam name="TEntity">           Type of the entity.</typeparam>
        /// <typeparam name="TStatus">           Type of the status.</typeparam>
        /// <typeparam name="TType">             Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">        Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">         Type of the discount.</typeparam>
        /// <typeparam name="TState">            Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">       Type of the stored file.</typeparam>
        /// <typeparam name="TContact">          Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">       Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">   Type of the sales event type.</typeparam>
        /// <typeparam name="TSalesItemDiscount">Type of the sales item discount.</typeparam>
        /// <typeparam name="TSalesItemTarget">  Type of the sales item target.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsByProductIDs<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType, TSalesItemDiscount, TSalesItemTarget>(
                this IQueryable<TEntity> query,
                List<int>? ids)
             where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
             where TType : class, ITypableBase
             where TStatus : class, IStatusableBase
             where TState : class, IStateableBase
             where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
             where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
             where TContact : IAmAContactRelationshipTable<TEntity, TContact>
             where TSalesItem : ISalesItemBase<TSalesItem, TSalesItemDiscount, TSalesItemTarget>
             where TSalesItemDiscount : IAppliedDiscountBase<TSalesItem, TSalesItemDiscount>
             where TSalesItemTarget : ISalesItemTargetBase
             where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
             where TSalesEventType : ITypableBase
        {
            if (Contract.CheckEmpty(ids))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .AsExpandable()
                .Where(ids!.Aggregate(
                    PredicateBuilder.New<TEntity>(false),
                    (c, id) => c.Or(p => p.SalesItems!.Any(soc => soc.Active && soc.ProductID == id))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by sales item product i ds.</summary>
        /// <typeparam name="TEntity">           Type of the entity.</typeparam>
        /// <typeparam name="TStatus">           Type of the status.</typeparam>
        /// <typeparam name="TType">             Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">        Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">         Type of the discount.</typeparam>
        /// <typeparam name="TState">            Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">       Type of the stored file.</typeparam>
        /// <typeparam name="TContact">          Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">       Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">   Type of the sales event type.</typeparam>
        /// <typeparam name="TSalesItemDiscount">Type of the sales item discount.</typeparam>
        /// <typeparam name="TSalesItemTarget">  Type of the sales item target.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="ids">  The identifiers.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterSalesCollectionsBySalesItemProductIDs<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType, TSalesItemDiscount, TSalesItemTarget>(
                this IQueryable<TEntity> query,
                List<int> ids)
            where TEntity : class, ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TType : class, ITypableBase
            where TStatus : class, IStatusableBase
            where TState : class, IStateableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : ISalesItemBase<TSalesItem, TSalesItemDiscount, TSalesItemTarget>
            where TSalesItemDiscount : IAppliedDiscountBase<TSalesItem, TSalesItemDiscount>
            where TSalesItemTarget : ISalesItemTargetBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            if (Contract.CheckEmpty(ids))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(ids.Aggregate(
                    PredicateBuilder.New<TEntity>(false),
                    (c, id) => c.Or(p => p.SalesItems!.Any(soc => soc.Active && soc.ProductID == id))));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by minimum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesCollectionsByMinDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? minDate)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(minDate))
            {
                return query;
            }
            // Strip time zone offset info that come from JavaScript
            var date = minDate!.Value.StripTime();
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedDate >= date);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter sales collections by maximum date.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        private static IQueryable<TEntity> FilterSalesCollectionsByMaxDate<TEntity>(
                this IQueryable<TEntity> query,
                DateTime? maxDate)
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidDate(maxDate))
            {
                return query;
            }
            // Strip time zone offset info that come from JavaScript
            var date = maxDate!.Value.StripTime().AddDays(1); // Basically make it 23:59:59
            return Contract.RequiresNotNull(query)
                .Where(x => x.CreatedDate < date);
        }
    }
}
