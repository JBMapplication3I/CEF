// <copyright file="UploadWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using Interfaces.Providers.Files;
    using Interfaces.Workflow;

    /// <summary>An upload workflow.</summary>
    /// <seealso cref="IUploadWorkflow"/>
    public class UploadWorkflow : IUploadWorkflow
    {
        /// <inheritdoc/>
        public Task<IUploadResponse> GetUploadAsync(string id)
        {
            return RegistryLoaderWrapper.GetInstance<IUploadController>().GetUploadAsync(id);
        }
    }
}
