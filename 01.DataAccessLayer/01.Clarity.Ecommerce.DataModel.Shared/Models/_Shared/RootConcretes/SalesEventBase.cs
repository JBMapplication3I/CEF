// <copyright file="SalesEventBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales event base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    /// <summary>The sales event base.</summary>
    /// <typeparam name="TMaster">        Type of the master.</typeparam>
    /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
    /// <seealso cref="NameableBase"/>
    /// <seealso cref="ISalesEventBase{TMaster,TSalesEventType}"/>
    public abstract class SalesEventBase<TMaster, TSalesEventType>
        : NameableBase,
          ISalesEventBase<TMaster, TSalesEventType>
        where TMaster : IBase
        where TSalesEventType : ITypableBase
    {
        #region ISalesEventBase Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OldStateID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? NewStateID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OldStatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? NewStatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OldTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? NewTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public long? OldHash { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public long? NewHash { get; set; }

        /// <inheritdoc/>
        [DontMapOutWithLite, DontMapOutWithListing, DefaultValue(null)]
        public string? OldRecordSerialized { get; set; }

        /// <inheritdoc/>
        [DontMapOutWithLite, DontMapOutWithListing, DefaultValue(null)]
        public string? NewRecordSerialized { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), Index, DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual TSalesEventType? Type { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual TMaster? Master { get; set; }
        #endregion
    }
}
