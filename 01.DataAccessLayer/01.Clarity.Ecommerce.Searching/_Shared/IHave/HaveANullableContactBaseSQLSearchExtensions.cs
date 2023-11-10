// <copyright file="HaveANullableContactBaseSQLSearchExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the have a nullable contact base SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using DataModel;
    using Utilities;

    /// <summary>A have a nullable contact base SQL search extensions.</summary>
    public static class HaveANullableContactBaseSQLSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by search
        /// model.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">The query to act on.</param>
        /// <param name="model">The model.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        public static IQueryable<TEntity> FilterIHaveANullableContactBaseBySearchModel<TEntity>(
                this IQueryable<TEntity> query,
                IHaveAContactBaseSearchModel model)
            where TEntity : class, IHaveANullableContactBase
        {
            return Contract.RequiresNotNull(query)
                .FilterIHaveANullableContactBaseByContactID(model.ContactID, model.ContactIDIncludeNull)
                .FilterIHaveANullableContactBaseByContactKey(model.ContactKey, model.ContactKeyStrict, model.ContactKeyIncludeNull)
                .FilterIHaveANullableContactBaseByContactFirstName(model.ContactFirstName, model.ContactFirstNameStrict, model.ContactFirstNameIncludeNull)
                .FilterIHaveANullableContactBaseByContactLastName(model.ContactLastName, model.ContactLastNameStrict, model.ContactLastNameIncludeNull)
                .FilterIHaveANullableContactBaseByContactPhone(model.ContactPhone, model.ContactPhoneStrict, model.ContactPhoneIncludeNull)
                .FilterIHaveANullableContactBaseByContactFax(model.ContactFax, model.ContactFaxStrict, model.ContactFaxIncludeNull)
                .FilterIHaveANullableContactBaseByContactEmail(model.ContactEmail, model.ContactEmailStrict, model.ContactEmailIncludeNull);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by contact
        /// identifier.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactID<TEntity>(
                this IQueryable<TEntity> query,
                int? parameter,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && Contract.CheckInvalidID(parameter))
            {
                return query;
            }
            if (includeNull == true && Contract.CheckInvalidID(parameter))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.ContactID == null);
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.ContactID == parameter!.Value);
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by contact
        /// key.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactKey<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.CustomKey == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.CustomKey == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.CustomKey != null && x.Contact.CustomKey.Contains(search!));
        }

        /// <summary>Filter i have a nullable contact base by contact the first name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactFirstName<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.FirstName == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.FirstName == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.FirstName != null && x.Contact.FirstName.Contains(search!));
        }

        /// <summary>Filter i have a nullable contact base by contact the last name.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactLastName<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.LastName == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.LastName == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.LastName != null && x.Contact.LastName.Contains(search!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by contact
        /// phone.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactPhone<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Phone1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Phone1 == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.Phone1 != null && x.Contact.Phone1.Contains(search!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by contact
        /// fax.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactFax<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Fax1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Fax1 == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.Fax1 != null && x.Contact.Fax1.Contains(search!));
        }

        /// <summary>An IQueryable{TEntity} extension method that filter i have a nullable contact base by contact
        /// email.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">      The query to act on.</param>
        /// <param name="parameter">  The parameter.</param>
        /// <param name="strict">     The strict.</param>
        /// <param name="includeNull">The include null.</param>
        /// <returns>An IQueryable{TEntity}.</returns>
        private static IQueryable<TEntity> FilterIHaveANullableContactBaseByContactEmail<TEntity>(
                this IQueryable<TEntity> query,
                string? parameter,
                bool? strict,
                bool? includeNull)
            where TEntity : class, IHaveANullableContactBase
        {
            if (includeNull != true && !Contract.CheckValidKey(parameter))
            {
                return query;
            }
            var search = parameter?.Trim();
            if (strict == true)
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Email1 == search);
            }
            if (includeNull == true && !Contract.CheckValidKey(search))
            {
                return Contract.RequiresNotNull(query)
                    .Where(x => x.Contact != null && x.Contact.Email1 == null);
            }
            search = search?.ToLower();
            return Contract.RequiresNotNull(query)
                .Where(x => x.Contact != null && x.Contact.Email1 != null && x.Contact.Email1.Contains(search!));
        }
    }
}
