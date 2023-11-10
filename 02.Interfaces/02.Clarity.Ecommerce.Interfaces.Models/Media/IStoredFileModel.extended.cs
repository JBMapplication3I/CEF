// <copyright file="IStoredFileModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoredFileModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for stored file model.</summary>
    public partial interface IStoredFileModel
    {
        #region Displaying it info (and metadata)
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the SEO title.</summary>
        /// <value>The SEO title.</value>
        string? SeoTitle { get; set; }

        /// <summary>Gets or sets the author.</summary>
        /// <value>The author.</value>
        string? Author { get; set; }

        /// <summary>Gets or sets the copyright.</summary>
        /// <value>The copyright.</value>
        string? Copyright { get; set; }
        #endregion

        #region The file as uploaded
        /// <summary>Gets or sets the file format.</summary>
        /// <value>The file format.</value>
        string? FileFormat { get; set; }

        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        string? FileName { get; set; }

        /// <summary>Gets or sets a value indicating whether this IStoredFileModel is stored in database.</summary>
        /// <value>True if this IStoredFileModel is stored in database, false if not.</value>
        bool IsStoredInDB { get; set; }

        /// <summary>Gets or sets the bytes.</summary>
        /// <value>The bytes.</value>
        byte[]? Bytes { get; set; }
        #endregion
    }
}
