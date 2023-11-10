// <copyright file="AccountUserRole.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IAccountUserRole
        : IAmFilterableByAccount, IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets the master.</summary>
        /// <value>The master.</value>
        Account? Master { get; }

        /// <summary>Gets or sets the identifier of the slave.</summary>
        /// <value>The identifier of the slave.</value>
        int SlaveID { get; set; }

        /// <summary>Gets or sets the slave.</summary>
        /// <value>The slave.</value>
        UserRole? Slave { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "AccountUserRole")]
    public class AccountUserRole : Base, IAccountUserRole
    {
        #region IAmAnAccountRelationshipTableWhereAccountIsTheMaster<Account>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null)]
        public virtual Account? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithLite, DefaultValue(null)]
        public virtual UserRole? Slave { get; set; }
        #endregion

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? EndDate { get; set; }
    }
}
