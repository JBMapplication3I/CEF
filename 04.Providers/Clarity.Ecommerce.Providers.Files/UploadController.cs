// <copyright file="UploadController.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload controller class</summary>
namespace Clarity.Ecommerce.Providers.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Files;

    /// <summary>A controller for handling uploads. This class cannot be inherited.</summary>
    public sealed class UploadController : IUploadController
    {
        /// <summary>Initializes a new instance of the <see cref="UploadController"/> class.</summary>
        public UploadController()
        {
            Uploads = new();
        }

        /// <inheritdoc/>
        public List<IUploadResponse> Uploads { get; }

        /// <inheritdoc/>
        public async Task AddUploadAsync(IUploadResponse upload)
        {
            await RemoveExpiredAsync().ConfigureAwait(false);
            Uploads.Add(upload);
        }

        /// <inheritdoc/>
        public Task<IUploadResponse> GetUploadAsync(string id)
        {
            return Task.FromResult(Uploads.Find(u => u.Upload?.UploadID == id))!;
        }

        /// <inheritdoc/>
        public Task<IUploadResponse> GetUploadByFileKeyAsync(string fileKey)
        {
            return Task.FromResult(Uploads.Find(u => u.UploadFiles!.Any(f => f.FileKey == fileKey)))!;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveUploadAsync(string id)
        {
            return await RemoveUploadAsync(await GetUploadAsync(id).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveUploadAsync(IUploadResponse upload)
        {
            await RemoveExpiredAsync().ConfigureAwait(false);
            return Uploads.Remove(upload);
        }

        /// <summary>Removes the expired.</summary>
        /// <returns>A Task.</returns>
        private async Task RemoveExpiredAsync()
        {
            foreach (var upload in Uploads.Where(u => u.Upload?.Expires <= DateExtensions.GenDateTime))
            {
                await RemoveUploadAsync(upload).ConfigureAwait(false);
            }
        }
    }
}
