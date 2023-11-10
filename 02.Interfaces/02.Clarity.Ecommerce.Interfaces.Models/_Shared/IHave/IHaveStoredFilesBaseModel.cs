// <copyright file="IHaveStoredFilesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveStoredFilesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have stored files base model.</summary>
    /// <typeparam name="TIStoredFileModel">Type of the stored file model's interface.</typeparam>
    public interface IHaveStoredFilesBaseModel<TIStoredFileModel> : IBaseModel
        where TIStoredFileModel : IAmAStoredFileRelationshipTableModel
    {
        /// <summary>Gets or sets the stored files.</summary>
        /// <value>The stored files.</value>
        List<TIStoredFileModel>? StoredFiles { get; set; }
    }
}
