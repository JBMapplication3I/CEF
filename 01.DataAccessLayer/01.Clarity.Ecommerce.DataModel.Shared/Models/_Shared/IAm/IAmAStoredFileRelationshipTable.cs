// <copyright file="IAmAStoredFileRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAStoredFileRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for am a stored file relationship table.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    /// <seealso cref="IAmARelationshipTable{TMaster,StoredFile}"/>
    public interface IAmAStoredFileRelationshipTable<out TMaster>
        : IAmARelationshipTable<TMaster, StoredFile>,
            IHaveSeoBase,
            INameableBase
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the file access type.</summary>
        /// <value>The identifier of the file access type.</value>
        int FileAccessTypeID { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
    }
}
