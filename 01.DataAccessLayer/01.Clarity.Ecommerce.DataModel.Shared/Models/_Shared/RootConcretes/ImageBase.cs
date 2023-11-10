// <copyright file="ImageBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the image base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using Interfaces.DataModel;

    /// <summary>An image base.</summary>
    /// <seealso cref="NameableBase"/>
    /// <seealso cref="IImageBase"/>
    public abstract class ImageBase : NameableBase, IImageBase
    {
        #region Displaying it info (and metadata)
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? SeoTitle { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Author { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? MediaDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Copyright { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Location { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 8), DefaultValue(null)]
        public decimal? Latitude { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 8), DefaultValue(null)]
        public decimal? Longitude { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPrimary { get; set; }
        #endregion

        #region The original (unmodified) image as uploaded
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OriginalWidth { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OriginalHeight { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? OriginalFileFormat { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? OriginalFileName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool OriginalIsStoredInDB { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public byte[]? OriginalBytes { get; set; }
        #endregion

        #region A generated thumbnail of the original image
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ThumbnailWidth { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ThumbnailHeight { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ThumbnailFileFormat { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ThumbnailFileName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ThumbnailIsStoredInDB { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public byte[]? ThumbnailBytes { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public abstract int? MasterID { get; set; }
        #endregion

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        public abstract int TypeID { get; set; }
        #endregion
    }
}
