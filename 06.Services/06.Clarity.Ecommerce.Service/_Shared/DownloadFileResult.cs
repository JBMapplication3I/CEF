// <copyright file="DownloadFileResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the download file result class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    /// <summary>Encapsulates the result of a download file.</summary>
    public class DownloadFileResult
    {
        /// <summary>Gets or sets URL of the download.</summary>
        /// <value>The download URL.</value>
        public string DownloadUrl { get; set; } = null!;
    }
}
