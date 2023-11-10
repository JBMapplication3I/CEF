// <copyright file="IUploadResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUploadResponse interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Files
{
    using System.Collections.Generic;

    /// <summary>Interface for upload response.</summary>
    public interface IUploadResponse
    {
        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        string? ID { get; }

        /// <summary>Gets the upload.</summary>
        /// <value>The upload.</value>
        IUpload Upload { get; }

        /// <summary>Gets the upload status.</summary>
        /// <value>The upload status.</value>
        Enums.UploadStatus UploadStatus { get; }

        /// <summary>Gets or sets the upload files.</summary>
        /// <value>The upload files.</value>
        List<IUploadResult>? UploadFiles { get; set; }
    }
}
