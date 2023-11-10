// <copyright file="IHaveAStoredFileBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStoredFileBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for have a stored file base.</summary>
    public interface IHaveAStoredFileBase : IBase
    {
        /// <summary>Gets or sets the identifier of the stored file.</summary>
        /// <value>The identifier of the stored file.</value>
        int StoredFileID { get; set; }

        /// <summary>Gets or sets the stored file.</summary>
        /// <value>The stored file.</value>
        StoredFile? StoredFile { get; set; }
    }

    /// <summary>Interface for have a nullable stored file base.</summary>
    public interface IHaveANullableStoredFileBase : IBase
    {
        /// <summary>Gets or sets the identifier of the stored file.</summary>
        /// <value>The identifier of the stored file.</value>
        int? StoredFileID { get; set; }

        /// <summary>Gets or sets the stored file.</summary>
        /// <value>The stored file.</value>
        StoredFile? StoredFile { get; set; }
    }
}
