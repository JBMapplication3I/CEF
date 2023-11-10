// <copyright file="IImageBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImageBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    /// <summary>Interface for am an image table.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    /// <typeparam name="TType">  Type of the type.</typeparam>
    public interface IImageBase<TMaster, TType>
        : IImageBase, IHaveATypeBase<TType>
        where TMaster : IBase
        where TType : ITypableBase
    {
        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        TMaster? Master { get; set; }
    }

    public interface IImageBase
        : INameableBase, IHaveATypeBase
    {
        #region Displaying it info (and metadata)
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the seo title.</summary>
        /// <value>The seo title.</value>
        string? SeoTitle { get; set; }

        /// <summary>Gets or sets the author.</summary>
        /// <value>The author.</value>
        string? Author { get; set; }

        /// <summary>Gets or sets the media date.</summary>
        /// <value>The media date.</value>
        DateTime? MediaDate { get; set; }

        /// <summary>Gets or sets the copyright.</summary>
        /// <value>The copyright.</value>
        string? Copyright { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        string? Location { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        decimal? Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        decimal? Longitude { get; set; }
        #endregion

        #region Primary of a group of images
        /// <summary>Gets or sets a value indicating whether this IImageBase{TMaster, TType} is primary.</summary>
        /// <value>True if this IImageBase{TMaster, TType} is primary, false if not.</value>
        bool IsPrimary { get; set; }
        #endregion

        #region The original (unmodified) image as uploaded
        /// <summary>Gets or sets the width of the original.</summary>
        /// <value>The width of the original.</value>
        int? OriginalWidth { get; set; }

        /// <summary>Gets or sets the height of the original.</summary>
        /// <value>The height of the original.</value>
        int? OriginalHeight { get; set; }

        /// <summary>Gets or sets the original file format.</summary>
        /// <value>The original file format.</value>
        string? OriginalFileFormat { get; set; }

        /// <summary>Gets or sets the filename of the original file.</summary>
        /// <value>The filename of the original file.</value>
        string? OriginalFileName { get; set; }

        /// <summary>Gets or sets a value indicating whether the original is stored in database.</summary>
        /// <value>True if original is stored in database, false if not.</value>
        bool OriginalIsStoredInDB { get; set; }

        /// <summary>Gets or sets the original bytes.</summary>
        /// <value>The original bytes.</value>
        byte[]? OriginalBytes { get; set; }
        #endregion

        #region A generated thumbnail of the original image
        /// <summary>Gets or sets the width of the thumbnail.</summary>
        /// <value>The width of the thumbnail.</value>
        int? ThumbnailWidth { get; set; }

        /// <summary>Gets or sets the height of the thumbnail.</summary>
        /// <value>The height of the thumbnail.</value>
        int? ThumbnailHeight { get; set; }

        /// <summary>Gets or sets the thumbnail file format.</summary>
        /// <value>The thumbnail file format.</value>
        string? ThumbnailFileFormat { get; set; }

        /// <summary>Gets or sets the filename of the thumbnail file.</summary>
        /// <value>The filename of the thumbnail file.</value>
        string? ThumbnailFileName { get; set; }

        /// <summary>Gets or sets a value indicating whether the thumbnail is stored in database.</summary>
        /// <value>True if thumbnail is stored in database, false if not.</value>
        bool ThumbnailIsStoredInDB { get; set; }

        /// <summary>Gets or sets the thumbnail bytes.</summary>
        /// <value>The thumbnail bytes.</value>
        byte[]? ThumbnailBytes { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int? MasterID { get; set; }
        #endregion
    }
}
