// <copyright file="UploadFileService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload file service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Files;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    // Note: Intentionally returning an interface, this is ok here
    [PublicAPI,
        Route("/Media/StoredFiles/Upload", "POST",
            Summary = "Upload stored files of any type and they will be injected with your model as you save them (e.g.- Create/Update Product, Category, etc)")]
    public partial class UploadStoredFile : IUpload, IReturn<IUploadResponse>
    {
        /// <inheritdoc/>
        public string UploadID { get; } = Guid.NewGuid().ToString();

        /// <inheritdoc/>
        public DateTime Expires { get; } = DateExtensions.GenDateTime.AddDays(1);

        /// <inheritdoc/>
        public Enums.FileEntityType EntityFileType { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; } = null!;

        /// <inheritdoc/>
        public bool Async { get; set; } = true;
    }

    // Note: Intentionally returning an interface, this is ok here
    [PublicAPI,
        Route("/Media/StoredFiles/UploadResults/{ID}", "GET", Summary = "Get Upload StoredFile Results")]
    public partial class GetUploadStoredFileResults : IReturn<IUploadResponse>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "string", ParameterType = "path", IsRequired = true)]
        public string ID { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Media/StoredFiles/Url", "GET", Summary = "FileUrl")]
    public partial class GetStoredFileUrl : IReturn<string>
    {
        /// <summary>Gets or sets the type of the entity.</summary>
        /// <value>The type of the entity.</value>
        [ApiMember(Name = nameof(EntityType), DataType = "string", ParameterType = "query", IsRequired = true)]
        public Enums.FileEntityType EntityType { get; set; }

        /// <summary>Gets or sets the file key.</summary>
        /// <value>The file key.</value>
        [ApiMember(Name = nameof(FileKey), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string FileKey { get; set; } = null!;
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(UploadStoredFile request)
        {
            var filesProvider = RegistryLoaderWrapper.GetFilesProvider(null);
            if (filesProvider == null)
            {
                throw new System.Configuration.ConfigurationErrorsException("No valid configuration for a Files Provider found.");
            }
            var uploadResponse = await filesProvider.UploadFileAsync(request, Request.Files, request.EntityFileType, null).ConfigureAwait(false);
            if (uploadResponse?.ID == null)
            {
                throw new System.IO.FileLoadException("The File Provider failed to provide a response with an ID");
            }
            return uploadResponse;
        }

        public async Task<object?> Get(GetUploadStoredFileResults request)
        {
            return await Workflows.Uploads.GetUploadAsync(request.ID).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetStoredFileUrl request)
        {
            // TODO: Cached Research
            var url = string.Empty;
            try
            {
                if (request != null)
                {
                    var filesProvider = RegistryLoaderWrapper.GetFilesProvider(null);
                    // This will work for all the entity types since we are just trying to read this one property
                    url = await filesProvider!.GetFileUrlAsync<IProductFileModel>(
                            new ProductFileModel
                            {
                                Slave = new() { FileName = request.FileKey },
                            },
                            request.EntityType)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return url;
        }
    }
}
