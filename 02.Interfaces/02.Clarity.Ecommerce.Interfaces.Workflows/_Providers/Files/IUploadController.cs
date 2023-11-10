// <copyright file="IUploadController.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUploadController interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface for upload controller.</summary>
    public interface IUploadController
    {
        /// <summary>Gets the uploads.</summary>
        /// <value>The uploads.</value>
        List<IUploadResponse> Uploads { get; }

        /// <summary>Adds an upload.</summary>
        /// <param name="upload">The upload.</param>
        /// <returns>A Task.</returns>
        Task AddUploadAsync(IUploadResponse upload);

        /// <summary>Gets an upload.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The upload.</returns>
        Task<IUploadResponse> GetUploadAsync(string id);

        /// <summary>Gets upload by file key.</summary>
        /// <param name="fileKey">The file key.</param>
        /// <returns>The upload by file key.</returns>
        Task<IUploadResponse> GetUploadByFileKeyAsync(string fileKey);

        /// <summary>Removes the upload described by ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> RemoveUploadAsync(string id);

        /// <summary>Removes the upload described by upload.</summary>
        /// <param name="upload">The upload.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> RemoveUploadAsync(IUploadResponse upload);
    }
}
