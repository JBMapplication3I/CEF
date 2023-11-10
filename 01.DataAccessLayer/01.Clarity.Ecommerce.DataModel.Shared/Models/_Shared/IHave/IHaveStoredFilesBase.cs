// <copyright file="IHaveStoredFilesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveStoredFilesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have stored files base.</summary>
    /// <typeparam name="TMaster">    Type of the master.</typeparam>
    /// <typeparam name="TStoredFile">Type of the stored file.</typeparam>
    public interface IHaveStoredFilesBase<out TMaster, TStoredFile>
        where TStoredFile : IAmAStoredFileRelationshipTable<TMaster>
        where TMaster : IBase
    {
        /// <summary>Gets or sets the stored files.</summary>
        /// <value>The stored files.</value>
        ICollection<TStoredFile>? StoredFiles { get; set; }
    }
}
