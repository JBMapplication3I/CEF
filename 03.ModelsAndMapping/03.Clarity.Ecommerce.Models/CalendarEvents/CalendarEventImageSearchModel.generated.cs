// <autogenerated>
// <copyright file="CalendarEventImageSearchModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchModel Classes generated to provide base setups.</summary>
// <remarks>This file was auto-generated by SearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable MissingXmlDoc, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective, UnusedMember.Global
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the Calendar Event Image search.</summary>
    public partial class CalendarEventImageSearchModel
        : NameableBaseSearchModel
        , ICalendarEventImageSearchModel
    {
        #region IHaveATypeBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects")]
        public int? TypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically include")]
        public int?[]? TypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects to specifically exclude")]
        public int? ExcludedTypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically exclude")]
        public int?[]? ExcludedTypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically include")]
        public string?[]? TypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects to specifically exclude")]
        public string? ExcludedTypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically exclude")]
        public string?[]? ExcludedTypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically include")]
        public string?[]? TypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects to specifically exclude")]
        public string? ExcludedTypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically include")]
        public string?[]? TypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects to specifically exclude")]
        public string? ExcludedTypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Translation Key for objects")]
        public string? TypeTranslationKey { get; set; }
        #endregion
        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsPrimary), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IsPrimary { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalIsStoredInDB), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? OriginalIsStoredInDB { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailIsStoredInDB), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ThumbnailIsStoredInDB { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinLatitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinLatitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxLatitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxLatitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchLatitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MatchLatitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchLatitudeIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchLatitudeIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinLongitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinLongitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxLongitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxLongitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchLongitude), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MatchLongitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchLongitudeIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchLongitudeIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MasterID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MasterIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinMediaDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MinMediaDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxMediaDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MaxMediaDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchMediaDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MatchMediaDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchMediaDateIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchMediaDateIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinOriginalHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinOriginalHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxOriginalHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxOriginalHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchOriginalHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchOriginalHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchOriginalHeightIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchOriginalHeightIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinOriginalWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinOriginalWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxOriginalWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxOriginalWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchOriginalWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchOriginalWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchOriginalWidthIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchOriginalWidthIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrderIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchSortOrderIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinThumbnailHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinThumbnailHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxThumbnailHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxThumbnailHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchThumbnailHeight), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchThumbnailHeight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchThumbnailHeightIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchThumbnailHeightIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinThumbnailWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinThumbnailWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxThumbnailWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxThumbnailWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchThumbnailWidth), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchThumbnailWidth { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchThumbnailWidthIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchThumbnailWidthIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Author), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Author { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AuthorStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? AuthorStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AuthorIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? AuthorIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Copyright), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Copyright { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CopyrightStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? CopyrightStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CopyrightIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? CopyrightIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DisplayNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? DisplayNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DisplayNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? DisplayNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Location), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Location { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(LocationStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? LocationStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(LocationIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? LocationIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileFormat), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? OriginalFileFormat { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileFormatStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? OriginalFileFormatStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileFormatIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? OriginalFileFormatIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? OriginalFileName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? OriginalFileNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OriginalFileNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? OriginalFileNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoTitle), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? SeoTitle { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoTitleStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoTitleStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoTitleIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoTitleIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileFormat), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ThumbnailFileFormat { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileFormatStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ThumbnailFileFormatStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileFormatIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ThumbnailFileFormatIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ThumbnailFileName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ThumbnailFileNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ThumbnailFileNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ThumbnailFileNameIncludeNull { get; set; }
    }
}
