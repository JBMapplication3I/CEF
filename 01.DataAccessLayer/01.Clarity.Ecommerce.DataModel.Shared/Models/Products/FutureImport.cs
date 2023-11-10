// <copyright file="FutureImport.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IFutureImport
        : INameableBase,
            IHaveAStatusBase<FutureImportStatus>,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableVendor
    {
        #region FutureImport Properties
        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        string? FileName { get; set; }

        /// <summary>Gets or sets the Date/Time to run import at.</summary>
        /// <value>The run import at Date/Time value.</value>
        DateTime RunImportAt { get; set; }

        /// <summary>Gets or sets the attempts.</summary>
        /// <value>The attempts.</value>
        int Attempts { get; set; }

        /// <summary>Gets or sets a value indicating whether this Future Import has errors when it tries to run.</summary>
        /// <value>True if this IFutureImport has error, false if not.</value>
        bool HasError { get; set; }
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

    [SqlSchema("Products", "FutureImport")]
    public class FutureImport : NameableBase, IFutureImport
    {
        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual FutureImportStatus? Status { get; set; }
        #endregion

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableVendor Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Vendor)), DefaultValue(null)]
        public int? VendorID { get; set; } = null;

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Vendor { get; set; } = null;
        #endregion

        #region FutureImport Properties
        /// <inheritdoc/>
        [Required, StringLength(512), StringIsUnicode(false), Index, DefaultValue(null)]
        public string? FileName { get; set; }

        /// <inheritdoc/>
        [Required, /*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index]
        public DateTime RunImportAt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int Attempts { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HasError { get; set; } = false;
        #endregion
    }
}
