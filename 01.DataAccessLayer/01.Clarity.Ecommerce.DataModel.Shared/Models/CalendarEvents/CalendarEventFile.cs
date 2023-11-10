// <copyright file="CalendarEventFile.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event file class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICalendarEventFile : IAmAStoredFileRelationshipTable<CalendarEvent>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("CalendarEvents", "CalendarEventFile")]
    public class CalendarEventFile : NameableBase, ICalendarEventFile
    {
        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null), DontMapOutWithListing]
        public string? SeoMetaData { get; set; }
        #endregion

        #region IAmAStoredFileRelationshipTable<CalendarEvent>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual CalendarEvent? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion
    }
}
