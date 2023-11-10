// <copyright file="InventoryLocationUser.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location user class, which
// describes a relationship between an InventoryLocation and a User.</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IInventoryLocationUser
        : IAmARelationshipTable<InventoryLocation, User>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Inventory", "InventoryLocationUser")]
    public class InventoryLocationUser : Base, IInventoryLocationUser
    {
        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocation? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave))]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? Slave { get; set; }
        #endregion
    }
}
