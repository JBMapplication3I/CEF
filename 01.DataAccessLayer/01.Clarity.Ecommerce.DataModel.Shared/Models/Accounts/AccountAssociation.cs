// <copyright file="AccountAssociation.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account association class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAccountAssociation
        : IHaveATypeBase<AccountAssociationType>,
            IAmARelationshipTable<Account, Account>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Accounts", "AccountAssociation")]
    public class AccountAssociation : Base, IAccountAssociation
    {
        #region IAmARelationshipTable<Account, Account>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null)]
        public virtual Account? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null)]
        public virtual Account? Slave { get; set; }
        #endregion

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type))]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual AccountAssociationType? Type { get; set; }
        #endregion
    }
}
