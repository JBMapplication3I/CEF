// <copyright file="IProductDownloadModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductDownloadModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IProductDownloadModel
    {
        #region ProductDownload Properties
        /// <summary>Gets or sets a value indicating whether this Download use absolute URL.</summary>
        /// <value>True if this Download uses absolute url, false if not.</value>
        bool IsAbsoluteUrl { get; set; }

        /// <summary>Gets or sets the absolute URL to initiate the download.</summary>
        /// <value>The absolute URL to initiate the download.</value>
        string? AbsoluteUrl { get; set; }

        /// <summary>Gets or sets the relative URL to initiate the download.</summary>
        /// <value>The relative URL to initiate the download.</value>
        string? RelativeUrl { get; set; }
        #endregion
    }
}
