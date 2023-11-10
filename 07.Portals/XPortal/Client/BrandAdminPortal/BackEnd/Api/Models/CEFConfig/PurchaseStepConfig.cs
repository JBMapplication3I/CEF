// <copyright file="PurchaseStepConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class PurchaseStepConfig
    {
        public bool show { get; set; }

        public bool showButton { get; set; }

        public string? name { get; set; }

        public string? titleKey { get; set; }

        public string? continueTextKey { get; set; }

        public string? templateURL { get; set; }

        public int order { get; set; }
    }
}
