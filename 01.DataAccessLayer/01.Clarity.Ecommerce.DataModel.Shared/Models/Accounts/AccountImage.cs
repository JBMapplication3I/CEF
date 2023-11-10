// <copyright file="AccountImage.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account image class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAccountImage : IImageBase<Account, AccountImageType>, IAmFilterableByAccount
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "AccountImage")]
    public class AccountImage : ImageBase, IAccountImage
    {
        #region IImageBase
        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public override int? MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Account? Master { get; set; }
        #endregion

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public override int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AccountImageType? Type { get; set; }
        #endregion
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByAccount.AccountID { get => MasterID!.Value; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Account? IAmFilterableByAccount.Account { get => Master; set => Master = value; }
        #endregion
    }
}
