// <copyright file="ProductDownloadModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product download model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    public partial class ProductDownloadModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsAbsoluteUrl), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "True if this Download uses absolute url, false if not.")]
        public bool IsAbsoluteUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AbsoluteUrl), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The absolute URL.")]
        public string? AbsoluteUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RelativeUrl), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The relative URL to initiate the download.")]
        public string? RelativeUrl { get; set; }
    }
}
