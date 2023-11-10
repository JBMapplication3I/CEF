// <copyright file="SampleData.Attributes.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data class's attributes section</summary>
// ReSharper disable CognitiveComplexity, FunctionComplexityOverflow
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private void AddSampleAttribute(
            DateTime createdDate,
            string key,
            string name,
            string? displayName = null,
            string? translationKey = null,
            string? description = null,
            string? jsonAttributes = null,
            int? sortOrder = null,
            bool isFilter = false,
            bool isComparable = false,
            bool isPredefined = false,
            bool isMarkup = false,
            bool isTab = false,
            bool hideFromStorefront = false,
            bool hideFromSuppliers = false,
            bool hideFromProductDetailView = false,
            bool hideFromCatalogViews = false,
            string typeKey = "GENERAL",
            IReadOnlyList<string>? predefinedOptions = null)
        {
            var attribute = context.GeneralAttributes.SingleOrDefault(x => x.Active && x.CustomKey == key)
                ?? new GeneralAttribute();
            var type = context.AttributeTypes.SingleOrDefault(x => x.CustomKey == typeKey)
                ?? new AttributeType { Active = true, CreatedDate = createdDate, CustomKey = typeKey, Name = typeKey, JsonAttributes = "{}" };
            // Base Properties
            attribute.Active = true;
            attribute.CustomKey = key;
            attribute.CreatedDate = createdDate;
            attribute.JsonAttributes = jsonAttributes;
            // NameableBase Properties
            attribute.Name = name;
            attribute.Description = description;
            // DisplayableBase Properties
            attribute.DisplayName = displayName;
            attribute.TranslationKey = translationKey;
            attribute.SortOrder = sortOrder;
            // HaveATypeBase Properties
            if (Contract.CheckValidID(type.ID))
            {
                attribute.TypeID = type.ID;
            }
            else
            {
                attribute.Type = type;
            }
            // GeneralAttribute Properties
            attribute.IsFilter = isFilter;
            attribute.IsComparable = isComparable;
            attribute.IsPredefined = isPredefined;
            attribute.IsMarkup = isMarkup;
            attribute.IsTab = isTab;
            attribute.HideFromStorefront = hideFromStorefront;
            attribute.HideFromSuppliers = hideFromSuppliers;
            attribute.HideFromProductDetailView = hideFromProductDetailView;
            attribute.HideFromCatalogViews = hideFromCatalogViews;
            // Associated Objects
            attribute.GeneralAttributePredefinedOptions ??= new HashSet<GeneralAttributePredefinedOption>();
            if (isPredefined)
            {
                Contract.RequiresNotEmpty(predefinedOptions);
                for (var i = 0; i < predefinedOptions!.Count; i++)
                {
                    var value = predefinedOptions[i];
                    var found = false;
                    foreach (var option in attribute.GeneralAttributePredefinedOptions.Where(x => x.Active))
                    {
                        if (string.Compare(option.Value, value, StringComparison.CurrentCultureIgnoreCase) != 0)
                        {
                            continue;
                        }
                        found = true;
                        option.SortOrder = i;
                        option.Value = value;
                        option.UofM = string.Empty;
                    }
                    if (found)
                    {
                        continue;
                    }
                    attribute.GeneralAttributePredefinedOptions.Add(new()
                    {
                        Active = true,
                        CreatedDate = createdDate,
                        SortOrder = i,
                        Value = value,
                    });
                }
            }
            else
            {
                foreach (var option in attribute.GeneralAttributePredefinedOptions.Where(x => x.Active))
                {
                    option.Active = false;
                    option.UpdatedDate = createdDate;
                }
            }
            if (!Contract.CheckValidID(attribute.ID))
            {
                context.GeneralAttributes.Add(attribute);
            }
            context.SaveUnitOfWork();
        }

        private void AddSampleAttributes(DateTime createdDate)
        {
            if (context?.GeneralAttributes == null)
            {
                return;
            }
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Details",
                name: "Details",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 000,
                isFilter: false,
                isComparable: true,
                isPredefined: false,
                isMarkup: true,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Material",
                name: "Material",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 010,
                isFilter: true,
                isComparable: true,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Model #",
                name: "Model #",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 020,
                isFilter: false,
                isComparable: true,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Color",
                name: "Color",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 030,
                isFilter: true,
                isComparable: true,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Is Digital Download",
                name: "Is Digital Download",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 040,
                isFilter: true,
                isComparable: true,
                isPredefined: true,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT",
                predefinedOptions: new[] { "Yes", "No" });
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Download URL",
                name: "Download URL",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 050,
                isFilter: false,
                isComparable: false,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: true,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Genre",
                name: "Genre",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 060,
                isFilter: true,
                isComparable: true,
                isPredefined: true,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT",
                predefinedOptions: new[] { "Action & Adventure", "Comedy", "Fantasy", "Horror", "Romance", "Sci-fi", "Thriller", "Self Help", "Satire" });
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Size",
                name: "Size",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 070,
                isFilter: true,
                isComparable: true,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Hours",
                name: "Hours",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 080,
                isFilter: true,
                isComparable: true,
                isPredefined: false,
                isMarkup: false,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: false,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Bulleted Specs",
                name: "Bulleted Specs",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 100,
                isFilter: false,
                isComparable: true,
                isPredefined: false,
                isMarkup: true,
                isTab: true,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: true,
                typeKey: "PRODUCT");
            AddSampleAttribute(
                createdDate: createdDate,
                key: "Notes",
                name: "Notes",
                displayName: null,
                translationKey: null,
                description: null,
                jsonAttributes: "{}",
                sortOrder: 200,
                isFilter: false,
                isComparable: true,
                isPredefined: false,
                isMarkup: true,
                isTab: false,
                hideFromStorefront: false,
                hideFromSuppliers: false,
                hideFromProductDetailView: false,
                hideFromCatalogViews: true,
                typeKey: "GENERAL");
        }
    }
}
