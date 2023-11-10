// <copyright file="IUploadResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUploadResult interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Files
{
    /// <summary>Interface for upload result.</summary>
    public interface IUploadResult
    {
        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        string? FileName { get; set; }

        /// <summary>Gets or sets the full pathname of the file.</summary>
        /// <value>The full pathname of the file.</value>
        string? FilePath { get; set; }

        /// <summary>Gets or sets the file key.</summary>
        /// <value>The file key.</value>
        string? FileKey { get; set; }

        /// <summary>Gets or sets the type of the content.</summary>
        /// <value>The type of the content.</value>
        string? ContentType { get; set; }

        /// <summary>Gets or sets the length of the content.</summary>
        /// <value>The length of the content.</value>
        long ContentLength { get; set; }

        /// <summary>Gets or sets the percent done.</summary>
        /// <value>The percent done.</value>
        long PercentDone { get; set; }

        /// <summary>Gets or sets the total number of bytes.</summary>
        /// <value>The total number of bytes.</value>
        long TotalBytes { get; set; }

        /// <summary>Gets or sets the transferred bytes.</summary>
        /// <value>The transferred bytes.</value>
        long TransferredBytes { get; set; }

        /// <summary>Gets or sets the upload file status.</summary>
        /// <value>The upload file status.</value>
        Enums.UploadStatus UploadFileStatus { get; set; }
    }
}
