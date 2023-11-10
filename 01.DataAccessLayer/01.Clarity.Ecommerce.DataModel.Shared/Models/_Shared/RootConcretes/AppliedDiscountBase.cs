// <copyright file="AppliedDiscountBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied discount base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    public abstract class AppliedDiscountBase<TMaster, TApplied>
        : Base, IAppliedDiscountBase<TMaster, TApplied>
        where TMaster : IHaveAppliedDiscountsBase<TMaster, TApplied>
        where TApplied : IAppliedDiscountBase<TMaster, TApplied>
    {
        /// <inheritdoc/>
        [InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual TMaster? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Discount? Slave { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public decimal DiscountTotal { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ApplicationsUsed { get; set; } = 1;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? TargetApplicationsUsed { get; set; } = null;
    }
}
