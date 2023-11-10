// <copyright file="EmailQueueAttachment.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Email Attachment class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for EmailQueue attachment.</summary>
    public interface IEmailQueueAttachment : IAmAStoredFileRelationshipTable<EmailQueue>
    {
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        User? CreatedByUser { get; set; }

        /// <summary>Gets or sets the identifier of the updated by user.</summary>
        /// <value>The identifier of the updated by user.</value>
        int? UpdatedByUserID { get; set; }

        /// <summary>Gets or sets the updated by user.</summary>
        /// <value>The updated by user.</value>
        User? UpdatedByUser { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "EmailQueueAttachment")]
    public class EmailQueueAttachment : NameableBase, IEmailQueueAttachment
    {
        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoMetaData { get; set; }
        #endregion

        #region IAmAStoredFileRelationshipTable<EmailQueue>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual EmailQueue? Master { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        // [InverseProperty(nameof(ID)), ForeignKey(nameof(CreatedByUser))] // Handled by ModelBuilder because of cascading
        [DefaultValue(0)]
        public int CreatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? CreatedByUser { get; set; }

        /// <inheritdoc/>
        // [InverseProperty(nameof(ID)), ForeignKey(nameof(UpdatedByUser))] // Handled by ModelBuilder because of cascading
        [DefaultValue(null)]
        public int? UpdatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? UpdatedByUser { get; set; }
        #endregion
    }
}
