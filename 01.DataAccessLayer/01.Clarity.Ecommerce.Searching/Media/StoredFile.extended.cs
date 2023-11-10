// <copyright file="StoredFile.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements additional query filters for Stored Files.</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Linq;
    using Ecommerce.DataModel;
    using Utilities;

    public static partial class StoredFileSQLSearchExtensions
    {
        public static IQueryable<StoredFile> FilterStoredFilesByAccountID(
            this IQueryable<StoredFile> query,
            int? accountID)
        {
            if (!Contract.CheckValidID(accountID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.AccountFiles.Any(y => y.MasterID == accountID));
        }

        public static IQueryable<StoredFile> FilterStoredFilesByCategoryID(
            this IQueryable<StoredFile> query,
            int? categoryID)
        {
            if (!Contract.CheckValidID(categoryID))
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .Where(x => x.CategoryFiles.Any(y => y.MasterID == categoryID));
        }
    }
}
