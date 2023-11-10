// <copyright file="DashboardSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class DashboardSettings
    {
        public string? name { get; set; }

        public string? title { get; set; }

        public string? titleKey { get; set; }

        public string? sref { get; set; }

        public bool enabled { get; set; }

        public string? icon { get; set; }

        public DashboardSettings[]? children { get; set; }

        public int order { get; set; }

        public string[]? reqAnyRoles { get; set; }

        public string[]? reqAnyPerms { get; set; }
    }
}
