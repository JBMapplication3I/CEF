// <copyright file="ScoutCategory.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scout category class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IScoutCategory
        : IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Scout>,
          IHaveATypeBase<ScoutCategoryType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Scouting", "ScoutCategory")]
    public class ScoutCategory : Base, IScoutCategory
    {
        #region IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Scout>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Scout? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Category? Slave { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Scout>.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        int IAmFilterableByCategory.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        Category? IAmACategoryRelationshipTableWhereCategoryIsTheSlave<Scout>.Category { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        Category? IAmFilterableByCategory.Category { get => Slave; set => Slave = value; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ScoutCategoryType? Type { get; set; }
        #endregion
    }
}
