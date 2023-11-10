// <copyright file="UploadResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload response class</summary>
namespace Clarity.Ecommerce.Providers.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Providers.Files;
    using Utilities;

    /// <summary>An upload response.</summary>
    /// <seealso cref="IUploadResponse"/>
    public class UploadResponse : IUploadResponse
    {
        /// <summary>Initializes a new instance of the <see cref="UploadResponse"/> class.</summary>
        /// <param name="upload">The upload.</param>
        public UploadResponse(IUpload upload)
        {
            Contract.RequiresNotNull(upload);
            Upload = upload;
        }

        /// <inheritdoc/>
        public IUpload Upload { get; }

        /// <inheritdoc/>
        public Enums.UploadStatus UploadStatus
        {
            get
            {
                if (UploadFiles == null)
                {
                    return Enums.UploadStatus.None;
                }
                UploadFiles = UploadFiles.Where(x => x != null).ToList();
                if (UploadFiles.Any(u => u.UploadFileStatus == Enums.UploadStatus.InProgress))
                {
                    return Enums.UploadStatus.InProgress;
                }
                if (UploadFiles.Any(u => u.UploadFileStatus == Enums.UploadStatus.Failed)
                    && UploadFiles.All(u => u?.UploadFileStatus != Enums.UploadStatus.Created))
                {
                    return Enums.UploadStatus.Failed;
                }
                if (UploadFiles.Any(u => u.UploadFileStatus == Enums.UploadStatus.Completed)
                    && UploadFiles.All(u => u.UploadFileStatus != Enums.UploadStatus.Created))
                {
                    return Enums.UploadStatus.Completed;
                }
                return Enums.UploadStatus.Created;
            }
        }

        /// <inheritdoc/>
        public string? ID => Upload?.UploadID;

        /// <inheritdoc cref="IUploadResponse.UploadFiles" />
        public List<UploadResult>? UploadFiles { get; set; } = new();

        /// <inheritdoc/>
        List<IUploadResult>? IUploadResponse.UploadFiles { get => UploadFiles?.ToList<IUploadResult>(); set => UploadFiles = value?.Cast<UploadResult>().ToList(); }
    }
}
