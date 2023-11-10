// <copyright file="RecordVersion.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the record version class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IRecordVersion
        : INameableBase,
            IHaveATypeBase<RecordVersionType>,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        #region Record Version Properties
        /// <summary>Gets or sets the identifier of the record.</summary>
        /// <value>The identifier of the record.</value>
        int? RecordID { get; set; }

        /// <summary>Gets or sets the original publish date.</summary>
        /// <value>The original publish date.</value>
        DateTime? OriginalPublishDate { get; set; }

        /// <summary>Gets or sets the most recent publish date.</summary>
        /// <value>The most recent publish date.</value>
        DateTime? MostRecentPublishDate { get; set; }

        /// <summary>Gets or sets a value indicating whether this IRecordVersion is a draft (non-published).</summary>
        /// <value>True if this IRecordVersion is a draft (non-published), false if not.</value>
        bool IsDraft { get; set; }

        /// <summary>Gets or sets the serialized record.</summary>
        /// <value>The serialized record.</value>
        string? SerializedRecord { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the published by user.</summary>
        /// <value>The identifier of the published by user.</value>
        int? PublishedByUserID { get; set; }

        /// <summary>Gets or sets the published by user.</summary>
        /// <value>The published by user.</value>
        User? PublishedByUser { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("System", "RecordVersion")]
    public class RecordVersion : NameableBase, IRecordVersion
    {
        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual RecordVersionType? Type { get; set; }
        #endregion

        #region Record Version Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? RecordID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? OriginalPublishDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? MostRecentPublishDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsDraft { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), DefaultValue(null), DontMapOutWithListing]
        public string? SerializedRecord { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PublishedByUser)), DefaultValue(null)]
        public int? PublishedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? PublishedByUser { get; set; }
        #endregion
    }
}
