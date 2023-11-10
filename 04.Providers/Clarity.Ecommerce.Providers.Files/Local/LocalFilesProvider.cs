// <copyright file="LocalFilesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the local files provider class</summary>
namespace Clarity.Ecommerce.Providers.Files.LocalFileSystem
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Interfaces.Providers.Files;
    using ServiceStack.Web;

    /// <summary>A local files provider.</summary>
    /// <seealso cref="FilesProviderBase"/>
    public class LocalFilesProvider : FilesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => LocalFilesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<IUploadResponse> UploadFileAsync(
            IUpload upload,
            IHttpFile[] files,
            Enums.FileEntityType fileEntityType,
            string? contextProfileName)
        {
            var uploadResponse = new UploadResponse(upload);
            try
            {
                foreach (var file in files)
                {
                    uploadResponse.UploadFiles!.Add(new()
                    {
                        FileName = file.FileName,
                        FilePath = await SaveFileAsync(file, fileEntityType, contextProfileName).ConfigureAwait(false),
                        ContentType = file.ContentType,
                        ContentLength = file.ContentLength,
                        UploadFileStatus = Enums.UploadStatus.Completed,
                    });
                }
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(LocalFilesProvider)}.{nameof(UploadFileAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                throw;
            }
            return uploadResponse;
        }

        /// <inheritdoc/>
        public override Task<string> GetFileUrlAsync<TIFileModel>(
            TIFileModel file,
            Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(file.Slave!.FileName!);
        }

        /// <inheritdoc/>
        public override Task<string> GetFileUrlAsync(string file, Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(file);
        }

        /// <inheritdoc/>
        public override Task<string> GetFileSaveRootPathAsync(Enums.FileEntityType? fileEntityType)
        {
            return Task.FromResult(LocalFilesProviderConfig.GetPath(fileEntityType));
        }

        /// <inheritdoc/>
        public override Task<string> GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(LocalFilesProviderConfig.GetPath(fileEntityType));
        }

        /// <inheritdoc/>
        public override Task<string?> GetRelativeFileSaveRootPathFromFileEntityTypeAsync(
            Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(LocalFilesProviderConfig.GetRelativePath(fileEntityType));
        }

        /// <summary>Saves a file.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="file">              The file.</param>
        /// <param name="fileEntityType">    Type of the file entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A string.</returns>
        private async Task<string> SaveFileAsync(
            IHttpFile file,
            Enums.FileEntityType fileEntityType,
            string? contextProfileName)
        {
            string filePath;
            try
            {
                if (file == null)
                {
                    throw new ArgumentNullException(nameof(file));
                }
                var path = await GetFileSaveRootPathFromFileEntityTypeAsync(fileEntityType).ConfigureAwait(false);
                // Try to build the directory path if it doesn't exist
                var pathDirectories = path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var pathBuilder = path.StartsWith("\\\\") ? "\\\\" : string.Empty;
                foreach (var directory in pathDirectories)
                {
                    if (directory.Contains(":"))
                    {
                        pathBuilder = directory;
                    }
                    else
                    {
                        if (pathBuilder != string.Empty && pathBuilder != "\\\\")
                        {
                            pathBuilder += "\\";
                        }
                        pathBuilder += directory;
                    }
                    if (Directory.Exists(pathBuilder))
                    {
                        continue;
                    }
                    Directory.CreateDirectory(pathBuilder);
                }
                var fileNameNoExt = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExt = Path.GetExtension(file.FileName);
                // ReSharper disable once AssignNullToNotNullAttribute
                filePath = Path.Combine(path, file.FileName);
                if (File.Exists(filePath))
                {
                    for (var counter = 2; File.Exists(filePath); counter++)
                    {
                        filePath = Path.Combine(path, $"{fileNameNoExt}-({counter}){fileExt}");
                    }
                }
                using var stream = new FileStream(filePath, FileMode.CreateNew);
                var buffer = new byte[file.ContentLength];
#if NET5_0_OR_GREATER
                await file.InputStream.ReadAsync(buffer.AsMemory(0, (int)file.ContentLength)).ConfigureAwait(false);
                await stream.WriteAsync(buffer.AsMemory(0, (int)file.ContentLength)).ConfigureAwait(false);
#else
                await file.InputStream.ReadAsync(buffer, 0, (int)file.ContentLength).ConfigureAwait(false);
                await stream.WriteAsync(buffer, 0, (int)file.ContentLength).ConfigureAwait(false);
#endif
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(LocalFilesProvider)}.{nameof(SaveFileAsync)}.{ex.GetType().Name}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                throw;
            }
            return filePath;
        }

        /////// <summary>Deletes the file described by filePath.</summary>
        /////// <param name="filePath">Full pathname of the file.</param>
        ////private void DeleteFile(string filePath)
        ////{
        ////    if (File.Exists(filePath))
        ////    {
        ////        File.Delete(filePath);
        ////    }
        ////}
    }
}
