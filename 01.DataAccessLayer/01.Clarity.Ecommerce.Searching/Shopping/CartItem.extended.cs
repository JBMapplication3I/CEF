// <copyright file="CartItem.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using Utilities;

    /// <summary>A cart item search extensions.</summary>
    public static class CartItemSearchExtensions
    {
        public static IQueryable<TEntity> FilterCartItemsByCartLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                SessionCartBySessionAndTypeLookupKey? lookupKey)
            where TEntity : class, ICartItem
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => ((Cart)x.Master!).Type!.Name == lookupTypeName)
                .FilterCartItemsByCartSessionID(lookupKey.SessionID)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartItemsByCartLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                TargetCartLookupKey? lookupKey,
                bool useStartsWithPrefix)
            where TEntity : class, ICartItem
        {
            if (lookupKey is null)
            {
                return query;
            }
            query = Contract.RequiresNotNull(query)
                .FilterCartItemsByCartSessionID(lookupKey.SessionID)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID);
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return useStartsWithPrefix
                ? query.Where(x => ((Cart)x.Master!).Type!.Name!.StartsWith("Target-Grouping-"))
                : query.Where(x => ((Cart)x.Master!).Type!.Name == lookupTypeName);
        }

        public static IQueryable<TEntity> FilterCartItemsByCartLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                StaticCartLookupKey? lookupKey)
            where TEntity : class, ICartItem
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => ((Cart)x.Master!).Type!.Name == lookupTypeName)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartItemsByCartLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                CompareCartBySessionLookupKey? lookupKey)
            where TEntity : class, ICartItem
        {
            if (lookupKey is null)
            {
                return query;
            }
            var lookupTypeName = lookupKey.TypeKey.Trim();
            return Contract.RequiresNotNull(query)
                .Where(x => ((Cart)x.Master!).Type!.Name == lookupTypeName)
                .FilterCartItemsByCartSessionID(lookupKey.SessionID)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID);
        }

        public static IQueryable<TEntity> FilterCartItemsByCartLookupKey<TEntity>(
                this IQueryable<TEntity> query,
                CartByIDLookupKey? lookupKey)
            where TEntity : class, ICartItem
        {
            if (lookupKey is null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterCartItemsByCartID(lookupKey.CartID)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by item and cart active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="active">The active.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByItemAndCartActive<TEntity>(
                this IQueryable<TEntity> query,
                bool? active)
            where TEntity : class, ICartItem
        {
            if (!active.HasValue)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Active == active.Value
                    && x.Master != null
                    && x.Master.Active == active.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by has quantity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByHasQuantity<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, ICartItem
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m) > 0);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by force unique line item key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">     The query to act on.</param>
        /// <param name="key">       The key.</param>
        /// <param name="matchNulls">True to match nulls.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByForceUniqueLineItemKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key,
                bool matchNulls = true)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckValidKey(key))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ForceUniqueLineItemKey == key);
            }
            if (matchNulls)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ForceUniqueLineItemKey == null);
            }
            return query;
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by product identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByProductID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ProductID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by product active.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByProductActive<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, ICartItem
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null && x.Product.Active);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by product has something to ship.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByProductHasSomethingToShip<TEntity>(
                this IQueryable<TEntity> query)
            where TEntity : class, ICartItem
        {
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null && !x.Product.NothingToShip);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by product key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByProductKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null
                    && x.Product.CustomKey != null
                    && x.Product.CustomKey.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by product name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="name"> The name.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByProductName<TEntity>(
                this IQueryable<TEntity> query,
                string? name)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            var search = name!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Product != null
                    && x.Product.Name != null
                    && x.Product.Name.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by SKU.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="sku">  The SKU.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsBySku<TEntity>(
                this IQueryable<TEntity> query,
                string? sku)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(sku))
            {
                return query;
            }
            var search = sku!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Sku != null
                    && x.Sku.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by original currency identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByOriginalCurrencyID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.OriginalCurrencyID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by selling currency identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsBySellingCurrencyID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.SellingCurrencyID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.UserID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by user key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByUserKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                    && x.User.CustomKey != null
                    && x.User.CustomKey.Trim().ToLower().Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by user username.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByUserUsername<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            var search = key!.Trim().ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.User != null
                    && (x.User.CustomKey != null && x.User.CustomKey.Contains(search)
                        || x.User.UserName != null && x.User.UserName.Contains(search)));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart session identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartSessionID<TEntity>(
                this IQueryable<TEntity> query,
                Guid? id)
            where TEntity : class, ICartItem
        {
            if (id is null || id == default(Guid))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && ((Cart)x.Master).SessionID.HasValue
                    && ((Cart)x.Master).SessionID == id.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart type identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="typeID">Identifier for the type.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartTypeID<TEntity>(
                this IQueryable<TEntity> query,
                int? typeID)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(typeID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && ((Cart)x.Master).TypeID == typeID!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart type name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query"> The query to act on.</param>
        /// <param name="name">  The name.</param>
        /// <param name="strict">True to strict.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartTypeName<TEntity>(
                this IQueryable<TEntity> query,
                string? name,
                bool strict = false)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(name))
            {
                return query;
            }
            Contract.RequiresNotNull(query);
            if (strict)
            {
                return query
                    .Where(x => x.Master != null
                        && ((Cart)x.Master).Type != null
                        && ((Cart)x.Master).Type!.Name == name);
            }
            var search = name!.Trim().ToLower();
            return query
                .Where(x => x.Master != null
                    && ((Cart)x.Master).Type != null
                    && ((Cart)x.Master).Type!.Name != null
                    && ((Cart)x.Master).Type!.Name!.Contains(search));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.MasterID == id);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartKey<TEntity>(
                this IQueryable<TEntity> query,
                string? key)
            where TEntity : class, ICartItem
        {
            if (!Contract.CheckValidKey(key))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.CustomKey == key);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart user identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartUserID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.UserID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart account identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartAccountID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.AccountID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart brand identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartBrandID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.BrandID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart franchise identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartFranchiseID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.FranchiseID == id!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter cart items by cart store identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="id">   The identifier.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterCartItemsByCartStoreID<TEntity>(
                this IQueryable<TEntity> query,
                int? id)
            where TEntity : class, ICartItem
        {
            if (Contract.CheckInvalidID(id))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.Master != null
                    && x.Master.StoreID == id!.Value);
        }
    }
}
