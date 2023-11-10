// <copyright file="FilesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the files provider base class</summary>
namespace Clarity.Ecommerce.Providers.Files
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Files;
    using ServiceStack.Web;

    /// <summary>The files provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IFilesProviderBase"/>
    public abstract class FilesProviderBase : ProviderBase, IFilesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Files;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<IUploadResponse> UploadFileAsync(
            IUpload upload, IHttpFile[] files, Enums.FileEntityType fileEntityType, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<string> GetFileUrlAsync<TIFileModel>(TIFileModel file, Enums.FileEntityType fileEntityType)
            where TIFileModel : IAmAStoredFileRelationshipTableModel;

        /// <inheritdoc/>
        public abstract Task<string> GetFileUrlAsync(string file, Enums.FileEntityType fileEntityType);

        /// <inheritdoc/>
        public abstract Task<string> GetFileSaveRootPathAsync(Enums.FileEntityType? fileEntityType);

        /// <inheritdoc/>
        public abstract Task<string> GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType);

        /// <inheritdoc/>
        public abstract Task<string?> GetRelativeFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType);
    }
}
