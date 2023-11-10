// <copyright file="IHaveAStoredFileBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStoredFileBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a stored file base model.</summary>
    public interface IHaveAStoredFileBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the stored file.</summary>
        /// <value>The identifier of the stored file.</value>
        int StoredFileID { get; set; }

        /// <summary>Gets or sets the stored file key.</summary>
        /// <value>The stored file key.</value>
        string? StoredFileKey { get; set; }

        /// <summary>Gets or sets the stored file.</summary>
        /// <value>The stored file.</value>
        IStoredFileModel? StoredFile { get; set; }
    }

    /// <summary>Interface for have a nullable stored file base model.</summary>
    public interface IHaveANullableStoredFileBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the stored file.</summary>
        /// <value>The identifier of the stored file.</value>
        int? StoredFileID { get; set; }

        /// <summary>Gets or sets the stored file key.</summary>
        /// <value>The stored file key.</value>
        string? StoredFileKey { get; set; }

        /// <summary>Gets or sets the stored file.</summary>
        /// <value>The stored file.</value>
        IStoredFileModel? StoredFile { get; set; }
    }
}
