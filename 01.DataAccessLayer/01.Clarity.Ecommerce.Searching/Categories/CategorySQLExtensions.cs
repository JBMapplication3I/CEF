// <copyright file="CategorySQLExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category SQL extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;

    /// <summary>A category SQL extensions.</summary>
    public static class CategorySQLExtensions
    {
        /// <summary>Gets primary image file name.</summary>
        /// <returns>The primary image file name.</returns>
        public static Expression<Func<IEnumerable<ICategoryImage>?, string?>> GetPrimaryImageFileName()
        {
            return x => x!
                .Where(i => i.Active)
                .OrderByDescending(i => i.IsPrimary)
                .ThenByDescending(i => i.OriginalWidth)
                .ThenByDescending(i => i.OriginalHeight)
                .Take(1)
                .Select(i => i.OriginalFileName)
                .FirstOrDefault();
        }
    }
}
