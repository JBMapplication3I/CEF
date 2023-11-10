// <copyright file="IRecordVersionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRecordVersionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for record version model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IRecordVersionModel
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

        /// <summary>Gets or sets a value indicating whether this is a draft (non-published).</summary>
        /// <value>True if this is a draft (non-published), false if not.</value>
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
        IUserModel? PublishedByUser { get; set; }

        /// <summary>Gets or sets the published by user key.</summary>
        /// <value>The published by user key.</value>
        string? PublishedByUserKey { get; set; }
        #endregion
    }
}
