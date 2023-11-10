// <copyright file="IAmAStoredFileRelationshipTableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAStoredFileRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a stored file relationship table model.</summary>
    public interface IAmAStoredFileRelationshipTableModel
        : IAmARelationshipTableBaseModel<IStoredFileModel>,
            IHaveSeoBaseModel,
            INameableBaseModel
    {
        /// <summary>Gets or sets the identifier of the file access type.</summary>
        /// <value>The identifier of the file access type.</value>
        int FileAccessTypeID { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
    }
}
