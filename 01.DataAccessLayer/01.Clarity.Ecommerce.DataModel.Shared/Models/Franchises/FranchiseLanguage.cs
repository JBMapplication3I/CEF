// <copyright file="FranchiseLanguage.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise language class</summary>
// ReSharper disable InconsistentNaming
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFranchiseLanguage
        : IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Language>
    {
        #region FranchiseLanguage Properties
        /// <summary>Gets or sets the locale override.</summary>
        /// <value>The locale override.</value>
        string? OverrideLocale { get; set; }

        /// <summary>Gets or sets the name of the unicode override.</summary>
        /// <value>The name of the unicode override.</value>
        string? OverrideUnicodeName { get; set; }

        /// <summary>Gets or sets the ISO 639 1 2002 code override.</summary>
        /// <value>The ISO 639 1 2002 code override.</value>
        string? OverrideISO639_1_2002 { get; set; }

        /// <summary>Gets or sets the ISO 639 2 1998 code override.</summary>
        /// <value>The ISO 639 2 1998 code override.</value>
        string? OverrideISO639_2_1998 { get; set; }

        /// <summary>Gets or sets the ISO 639 3 2007 code override.</summary>
        /// <value>The ISO 639 3 2007 code override.</value>
        string? OverrideISO639_3_2007 { get; set; }

        /// <summary>Gets or sets the ISO 639 5 2008 code override.</summary>
        /// <value>The ISO 639 5 2008 code override.</value>
        string? OverrideISO639_5_2008 { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Franchises", "FranchiseLanguage")]
    public class FranchiseLanguage : Base, IFranchiseLanguage
    {
        #region IAmARelationshipTable<Franchise, Language>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Franchise? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Language? Slave { get; set; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByFranchise.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmFilterableByFranchise.Franchise { get => Master; set => Master = value; }
        #endregion

        #region IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Language>.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Language>.Franchise { get => Master; set => Master = value; }
        #endregion

        #region FranchiseLanguage Properties
        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(true), Index, DefaultValue(null)]
        public string? OverrideLocale { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true), DefaultValue(null)]
        public string? OverrideUnicodeName { get; set; }

        /// <inheritdoc/>
        [StringLength(2), StringIsUnicode(false), DefaultValue(null)]
        public string? OverrideISO639_1_2002 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? OverrideISO639_2_1998 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? OverrideISO639_3_2007 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? OverrideISO639_5_2008 { get; set; }
        #endregion
    }
}
