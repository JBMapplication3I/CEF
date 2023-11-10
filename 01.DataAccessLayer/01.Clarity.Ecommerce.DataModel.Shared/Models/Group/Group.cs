// <copyright file="Group.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the group class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IGroup
        : INameableBase,
          IHaveATypeBase<GroupType>,
          IHaveAStatusBase<GroupStatus>,
          IHaveAParentBase<Group>,
          IAmFilterableByUser<GroupUser>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the group owner.</summary>
        /// <value>The identifier of the group owner.</value>
        int? GroupOwnerID { get; set; }

        /// <summary>Gets or sets the group that owns this item.</summary>
        /// <value>The group owner.</value>
        User? GroupOwner { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Groups", "Group")]
    public class Group : NameableBase, IGroup
    {
        private ICollection<Group>? children;
        private ICollection<GroupUser>? users;

        public Group()
        {
            // IAmFilterableByUser Properties
            users = new HashSet<GroupUser>();
            // IHaveAParentBase Properties
            children = new HashSet<Group>();
        }

        #region HaveAParentBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Parent)), DefaultValue(null)]
        public int? ParentID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Group? Parent { get; set; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Group>? Children { get => children; set => children = value; }
        #endregion

        #region IAmFilterableByUser Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<GroupUser>? Users { get => users; set => users = value; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual GroupType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual GroupStatus? Status { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(GroupOwner)), DefaultValue(null)]
        public int? GroupOwnerID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? GroupOwner { get; set; }
        #endregion
    }
}
