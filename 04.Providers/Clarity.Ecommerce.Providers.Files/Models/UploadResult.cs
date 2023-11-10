// <copyright file="UploadResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload result class</summary>
namespace Clarity.Ecommerce.Providers.Files
{
    using Interfaces.Providers.Files;

    /// <summary>Encapsulates the result of an upload.</summary>
    /// <seealso cref="IUploadResult"/>
    public class UploadResult : IUploadResult
    {
        /// <inheritdoc/>
        public string? FileName { get; set; }

        /// <inheritdoc/>
        public string? FilePath { get; set; }

        /// <inheritdoc/>
        public string? FileKey { get; set; }

        /// <inheritdoc/>
        public string? ContentType { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public long PercentDone { get; set; }

        /// <inheritdoc/>
        public long TotalBytes { get; set; }

        /// <inheritdoc/>
        public long TransferredBytes { get; set; }

        /// <inheritdoc/>
        public Enums.UploadStatus UploadFileStatus { get; set; }
    }
}
