// <autogenerated>
// <copyright file="CountryImage.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ISearchModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by ISearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective
#pragma warning disable IDE0005_gen
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for country image model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    /// <seealso cref="IHaveATypeBaseSearchModel"/>
    public partial interface ICountryImageSearchModel
        : INameableBaseSearchModel
            , IHaveATypeBaseSearchModel
    {
        /// <summary>Gets or sets the IsPrimary.</summary>
        /// <value>The IsPrimary.</value>
        bool? IsPrimary { get; set; }

        /// <summary>Gets or sets the OriginalIsStoredInDB.</summary>
        /// <value>The OriginalIsStoredInDB.</value>
        bool? OriginalIsStoredInDB { get; set; }

        /// <summary>Gets or sets the ThumbnailIsStoredInDB.</summary>
        /// <value>The ThumbnailIsStoredInDB.</value>
        bool? ThumbnailIsStoredInDB { get; set; }

        /// <summary>Gets or sets the minimum Latitude.</summary>
        /// <value>The minimum Latitude.</value>
        decimal? MinLatitude { get; set; }

        /// <summary>Gets or sets the maximum Latitude.</summary>
        /// <value>The maximum Latitude.</value>
        decimal? MaxLatitude { get; set; }

        /// <summary>Gets or sets the match Latitude.</summary>
        /// <value>The match Latitude.</value>
        decimal? MatchLatitude { get; set; }

        /// <summary>Gets or sets the Latitude when matching must include nulls.</summary>
        /// <value>The Latitude when matching must include nulls.</value>
        bool? MatchLatitudeIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum Longitude.</summary>
        /// <value>The minimum Longitude.</value>
        decimal? MinLongitude { get; set; }

        /// <summary>Gets or sets the maximum Longitude.</summary>
        /// <value>The maximum Longitude.</value>
        decimal? MaxLongitude { get; set; }

        /// <summary>Gets or sets the match Longitude.</summary>
        /// <value>The match Longitude.</value>
        decimal? MatchLongitude { get; set; }

        /// <summary>Gets or sets the Longitude when matching must include nulls.</summary>
        /// <value>The Longitude when matching must include nulls.</value>
        bool? MatchLongitudeIncludeNull { get; set; }

        /// <summary>Gets or sets the MasterID.</summary>
        /// <value>The MasterID.</value>
        int? MasterID { get; set; }

        /// <summary>Gets or sets the MasterID when matching must include nulls.</summary>
        /// <value>The MasterID when matching must include nulls.</value>
        bool? MasterIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MediaDate.</summary>
        /// <value>The minimum MediaDate.</value>
        DateTime? MinMediaDate { get; set; }

        /// <summary>Gets or sets the maximum MediaDate.</summary>
        /// <value>The maximum MediaDate.</value>
        DateTime? MaxMediaDate { get; set; }

        /// <summary>Gets or sets the match MediaDate.</summary>
        /// <value>The match MediaDate.</value>
        DateTime? MatchMediaDate { get; set; }

        /// <summary>Gets or sets the MediaDate when matching must include nulls.</summary>
        /// <value>The MediaDate when matching must include nulls.</value>
        bool? MatchMediaDateIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum OriginalHeight.</summary>
        /// <value>The minimum OriginalHeight.</value>
        int? MinOriginalHeight { get; set; }

        /// <summary>Gets or sets the maximum OriginalHeight.</summary>
        /// <value>The maximum OriginalHeight.</value>
        int? MaxOriginalHeight { get; set; }

        /// <summary>Gets or sets the match OriginalHeight.</summary>
        /// <value>The match OriginalHeight.</value>
        int? MatchOriginalHeight { get; set; }

        /// <summary>Gets or sets the OriginalHeight when matching must include nulls.</summary>
        /// <value>The OriginalHeight when matching must include nulls.</value>
        bool? MatchOriginalHeightIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum OriginalWidth.</summary>
        /// <value>The minimum OriginalWidth.</value>
        int? MinOriginalWidth { get; set; }

        /// <summary>Gets or sets the maximum OriginalWidth.</summary>
        /// <value>The maximum OriginalWidth.</value>
        int? MaxOriginalWidth { get; set; }

        /// <summary>Gets or sets the match OriginalWidth.</summary>
        /// <value>The match OriginalWidth.</value>
        int? MatchOriginalWidth { get; set; }

        /// <summary>Gets or sets the OriginalWidth when matching must include nulls.</summary>
        /// <value>The OriginalWidth when matching must include nulls.</value>
        bool? MatchOriginalWidthIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum SortOrder.</summary>
        /// <value>The minimum SortOrder.</value>
        int? MinSortOrder { get; set; }

        /// <summary>Gets or sets the maximum SortOrder.</summary>
        /// <value>The maximum SortOrder.</value>
        int? MaxSortOrder { get; set; }

        /// <summary>Gets or sets the match SortOrder.</summary>
        /// <value>The match SortOrder.</value>
        int? MatchSortOrder { get; set; }

        /// <summary>Gets or sets the SortOrder when matching must include nulls.</summary>
        /// <value>The SortOrder when matching must include nulls.</value>
        bool? MatchSortOrderIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum ThumbnailHeight.</summary>
        /// <value>The minimum ThumbnailHeight.</value>
        int? MinThumbnailHeight { get; set; }

        /// <summary>Gets or sets the maximum ThumbnailHeight.</summary>
        /// <value>The maximum ThumbnailHeight.</value>
        int? MaxThumbnailHeight { get; set; }

        /// <summary>Gets or sets the match ThumbnailHeight.</summary>
        /// <value>The match ThumbnailHeight.</value>
        int? MatchThumbnailHeight { get; set; }

        /// <summary>Gets or sets the ThumbnailHeight when matching must include nulls.</summary>
        /// <value>The ThumbnailHeight when matching must include nulls.</value>
        bool? MatchThumbnailHeightIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum ThumbnailWidth.</summary>
        /// <value>The minimum ThumbnailWidth.</value>
        int? MinThumbnailWidth { get; set; }

        /// <summary>Gets or sets the maximum ThumbnailWidth.</summary>
        /// <value>The maximum ThumbnailWidth.</value>
        int? MaxThumbnailWidth { get; set; }

        /// <summary>Gets or sets the match ThumbnailWidth.</summary>
        /// <value>The match ThumbnailWidth.</value>
        int? MatchThumbnailWidth { get; set; }

        /// <summary>Gets or sets the ThumbnailWidth when matching must include nulls.</summary>
        /// <value>The ThumbnailWidth when matching must include nulls.</value>
        bool? MatchThumbnailWidthIncludeNull { get; set; }

        /// <summary>Gets or sets the Author.</summary>
        /// <value>The Author.</value>
        string? Author { get; set; }

        /// <summary>Gets or sets the match Author strict requirement.</summary>
        /// <value>The match Author strict requirement.</value>
        bool? AuthorStrict { get; set; }

        /// <summary>Gets or sets the Author when matching must include nulls.</summary>
        /// <value>The Author when matching must include nulls.</value>
        bool? AuthorIncludeNull { get; set; }

        /// <summary>Gets or sets the Copyright.</summary>
        /// <value>The Copyright.</value>
        string? Copyright { get; set; }

        /// <summary>Gets or sets the match Copyright strict requirement.</summary>
        /// <value>The match Copyright strict requirement.</value>
        bool? CopyrightStrict { get; set; }

        /// <summary>Gets or sets the Copyright when matching must include nulls.</summary>
        /// <value>The Copyright when matching must include nulls.</value>
        bool? CopyrightIncludeNull { get; set; }

        /// <summary>Gets or sets the DisplayName.</summary>
        /// <value>The DisplayName.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the match DisplayName strict requirement.</summary>
        /// <value>The match DisplayName strict requirement.</value>
        bool? DisplayNameStrict { get; set; }

        /// <summary>Gets or sets the DisplayName when matching must include nulls.</summary>
        /// <value>The DisplayName when matching must include nulls.</value>
        bool? DisplayNameIncludeNull { get; set; }

        /// <summary>Gets or sets the Location.</summary>
        /// <value>The Location.</value>
        string? Location { get; set; }

        /// <summary>Gets or sets the match Location strict requirement.</summary>
        /// <value>The match Location strict requirement.</value>
        bool? LocationStrict { get; set; }

        /// <summary>Gets or sets the Location when matching must include nulls.</summary>
        /// <value>The Location when matching must include nulls.</value>
        bool? LocationIncludeNull { get; set; }

        /// <summary>Gets or sets the OriginalFileFormat.</summary>
        /// <value>The OriginalFileFormat.</value>
        string? OriginalFileFormat { get; set; }

        /// <summary>Gets or sets the match OriginalFileFormat strict requirement.</summary>
        /// <value>The match OriginalFileFormat strict requirement.</value>
        bool? OriginalFileFormatStrict { get; set; }

        /// <summary>Gets or sets the OriginalFileFormat when matching must include nulls.</summary>
        /// <value>The OriginalFileFormat when matching must include nulls.</value>
        bool? OriginalFileFormatIncludeNull { get; set; }

        /// <summary>Gets or sets the OriginalFileName.</summary>
        /// <value>The OriginalFileName.</value>
        string? OriginalFileName { get; set; }

        /// <summary>Gets or sets the match OriginalFileName strict requirement.</summary>
        /// <value>The match OriginalFileName strict requirement.</value>
        bool? OriginalFileNameStrict { get; set; }

        /// <summary>Gets or sets the OriginalFileName when matching must include nulls.</summary>
        /// <value>The OriginalFileName when matching must include nulls.</value>
        bool? OriginalFileNameIncludeNull { get; set; }

        /// <summary>Gets or sets the SeoTitle.</summary>
        /// <value>The SeoTitle.</value>
        string? SeoTitle { get; set; }

        /// <summary>Gets or sets the match SeoTitle strict requirement.</summary>
        /// <value>The match SeoTitle strict requirement.</value>
        bool? SeoTitleStrict { get; set; }

        /// <summary>Gets or sets the SeoTitle when matching must include nulls.</summary>
        /// <value>The SeoTitle when matching must include nulls.</value>
        bool? SeoTitleIncludeNull { get; set; }

        /// <summary>Gets or sets the ThumbnailFileFormat.</summary>
        /// <value>The ThumbnailFileFormat.</value>
        string? ThumbnailFileFormat { get; set; }

        /// <summary>Gets or sets the match ThumbnailFileFormat strict requirement.</summary>
        /// <value>The match ThumbnailFileFormat strict requirement.</value>
        bool? ThumbnailFileFormatStrict { get; set; }

        /// <summary>Gets or sets the ThumbnailFileFormat when matching must include nulls.</summary>
        /// <value>The ThumbnailFileFormat when matching must include nulls.</value>
        bool? ThumbnailFileFormatIncludeNull { get; set; }

        /// <summary>Gets or sets the ThumbnailFileName.</summary>
        /// <value>The ThumbnailFileName.</value>
        string? ThumbnailFileName { get; set; }

        /// <summary>Gets or sets the match ThumbnailFileName strict requirement.</summary>
        /// <value>The match ThumbnailFileName strict requirement.</value>
        bool? ThumbnailFileNameStrict { get; set; }

        /// <summary>Gets or sets the ThumbnailFileName when matching must include nulls.</summary>
        /// <value>The ThumbnailFileName when matching must include nulls.</value>
        bool? ThumbnailFileNameIncludeNull { get; set; }
    }
}
