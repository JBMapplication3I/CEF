// <copyright file="TemplateSection.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class TemplateSection
    {
        public bool active { get; set; }

        public bool complete { get; set; }

        public string? name { get; set; }

        public string? title { get; set; }

        public bool show { get; set; }

        public string? headingDetailsURL { get; set; }

        public int position { get; set; }

        public string? templateURL { get; set; }

        public string? continueText { get; set; }

        public bool showButton { get; set; }

        public TemplateSection[]? children { get; set; }
    }
}
