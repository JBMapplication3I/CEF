// <copyright file="IUpload.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUpload interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Files
{
    /// <summary>Interface for upload.</summary>
    public interface IUpload
    {
        /// <summary>Gets the identifier of the upload.</summary>
        /// <value>The identifier of the upload.</value>
        string UploadID { get; }

        /// <summary>Gets the expires.</summary>
        /// <value>The expires.</value>
        System.DateTime Expires { get; }

        /// <summary>Gets or sets the type of the entity file.</summary>
        /// <value>The type of the entity file.</value>
        Enums.FileEntityType EntityFileType { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>Gets or sets a value indicating whether the asynchronous.</summary>
        /// <value>true if asynchronous, false if not.</value>
        bool Async { get; set; }
    }
}
