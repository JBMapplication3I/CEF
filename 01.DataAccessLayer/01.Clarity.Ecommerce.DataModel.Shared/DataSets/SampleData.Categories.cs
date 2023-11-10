// <copyright file= "SampleData.Categories.cs" company= "clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. categories class</summary>
// ReSharper disable CyclomaticComplexity, CognitiveComplexity, PossibleInvalidOperationException
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private void AddSampleCategories(DateTime createdDate)
        {
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-1") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-1",
                    Name = "Movies",
                    Description = "These movies are pretty good.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Movies",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Movies Title",
                    SeoKeywords = "Movies,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-2") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-2",
                    Name = "Books",
                    Description = "These books are ok.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Books",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Book Title",
                    SeoKeywords = "Book,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-3") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-3",
                    Name = "Toys",
                    Description = "These toys are pretty good.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Toys",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Toy Title",
                    SeoKeywords = "Toy,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-4") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-4",
                    Name = "Tools",
                    Description = "These tools work.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Tools",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Tool Title",
                    SeoKeywords = "tool,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-5") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-5",
                    Name = "Sports",
                    Description = "These sports gear are top of the line.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Sports",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Sports Title",
                    SeoKeywords = "Sports,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-6") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-6",
                    Name = "Electronics",
                    Description = "These electronic systems are top quality.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Electronics",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Electronic Title",
                    SeoKeywords = "Electronic,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-7") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-7",
                    Name = "Clothing",
                    Description = "The clothing are pretty good.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Clothing",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Clothing Title",
                    SeoKeywords = "Clothing,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
            if (!context?.Categories?.Any(x => x.CustomKey == "CAT-8") == true)
            {
                context!.Categories!.Add(new()
                {
                    ParentID = null,
                    TypeID = context.CategoryTypes.Where(ct => ct.Active && ct.Name == "General").Select(ct => ct.ID).FirstOrDefault(),
                    CustomKey = "CAT-8",
                    Name = "Automotive",
                    Description = "The automotive parts have been tested and approved.",
                    SortOrder = 0,
                    IsVisible = true,
                    IncludeInMenu = true,
                    SeoUrl = "Automotive",
                    CreatedDate = createdDate,
                    UpdatedDate = null,
                    Active = true,
                    SeoPageTitle = "Automotive Title",
                    SeoKeywords = "Automotive,Meta",
                    SeoDescription = "Meta Description",
                });
                context.SaveUnitOfWork();
            }
        }
    }
}
