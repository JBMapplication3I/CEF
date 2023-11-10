// <copyright file="IUploadWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUploadWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Providers.Files;

    /// <summary>Interface for upload workflow.</summary>
    public interface IUploadWorkflow
    {
        /// <summary>Gets an upload.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The upload.</returns>
        Task<IUploadResponse> GetUploadAsync(string id);
    }
}
