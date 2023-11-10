// <copyright file="CategoriesFeatureSet.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CategoriesFeatureSet
    {
        public bool enabled { get; set; }

        public string? categoryUrlFragment { get; set; }
    }
}
