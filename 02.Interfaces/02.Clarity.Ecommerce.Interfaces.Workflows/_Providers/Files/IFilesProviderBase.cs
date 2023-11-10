// <copyright file="IFilesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFilesProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Files
{
    using System.Threading.Tasks;
    using Models;
    using ServiceStack.Web;

    /// <summary>Interface for files provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IFilesProviderBase : IProviderBase
    {
        /// <summary>Uploads a file.</summary>
        /// <param name="upload">            The upload.</param>
        /// <param name="files">             The files.</param>
        /// <param name="fileEntityType">    Type of the file entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IUploadResponse.</returns>
        Task<IUploadResponse> UploadFileAsync(
            IUpload upload,
            IHttpFile[] files,
            Enums.FileEntityType fileEntityType,
            string? contextProfileName);

        /// <summary>Gets file URL.</summary>
        /// <typeparam name="TIFileModel">Type of the file model's interface.</typeparam>
        /// <param name="file">          The file.</param>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The file URL.</returns>
        Task<string> GetFileUrlAsync<TIFileModel>(TIFileModel file, Enums.FileEntityType fileEntityType)
            where TIFileModel : IAmAStoredFileRelationshipTableModel;

        /// <summary>Gets file URL.</summary>
        /// <param name="file">          The file.</param>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The file URL.</returns>
        Task<string> GetFileUrlAsync(string file, Enums.FileEntityType fileEntityType);

        /// <summary>Gets file save root path.</summary>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The file save root path.</returns>
        Task<string> GetFileSaveRootPathAsync(Enums.FileEntityType? fileEntityType);

        /// <summary>Gets file save root path from file entity type.</summary>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The file save root path from file entity type.</returns>
        Task<string> GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType);

        /// <summary>Gets relative file save root path from file entity type.</summary>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The relative file save root path from file entity type.</returns>
        Task<string?> GetRelativeFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType);
    }
}
