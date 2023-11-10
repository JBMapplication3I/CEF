// <copyright file="MessageAttachment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message attachment class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for message attachment.</summary>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMessageAttachment
        : IAmAStoredFileRelationshipTable<Message>
    {
        #region Related Objects
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
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "MessageAttachment")]
    public class MessageAttachment : NameableBase, IMessageAttachment
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

        #region IAmARelationshipTable<Message, StoredFile>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Message? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual StoredFile? Slave { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int FileAccessTypeID { get; set; }

        /// <inheritdoc/>
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
