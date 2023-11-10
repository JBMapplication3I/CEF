// <copyright file="StoredFileModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stored file model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the stored file.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IStoredFileModel"/>
    public partial class StoredFileModel
    {
        #region Displaying it info (and metadata)
        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        public string? SeoTitle { get; set; }

        /// <inheritdoc/>
        public string? Author { get; set; }

        /// <inheritdoc/>
        public string? Copyright { get; set; }
        #endregion

        #region The file as uploaded
        /// <inheritdoc/>
        public string? FileFormat { get; set; }

        /// <inheritdoc/>
        public string? FileName { get; set; }

        /// <inheritdoc/>
        public bool IsStoredInDB { get; set; }

        /// <inheritdoc/>
        public byte[]? Bytes { get; set; }
        #endregion
    }
}
