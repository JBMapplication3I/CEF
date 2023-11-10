// <copyright file="ContractorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contractor model class.</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class ContractorModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }

        /// <inheritdoc cref="IContractorModel.Account"/>
        public AccountModel? Account { get; set; }

        /// <inheritdoc/>
        IAccountModel? IContractorModel.Account { get => Account; set => Account = (AccountModel?)value; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        public string? UserName { get; set; }

        /// <inheritdoc cref="IContractorModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IContractorModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        public string? StoreName { get; set; }

        /// <inheritdoc cref="IContractorModel.Store"/>
        public StoreModel? Store { get; set; }

        /// <inheritdoc/>
        IStoreModel? IContractorModel.Store { get => Store; set => Store = (StoreModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IContractorModel.ServiceAreas"/>
        public List<ServiceAreaModel>? ServiceAreas { get; set; }

        /// <inheritdoc/>
        List<IServiceAreaModel>? IContractorModel.ServiceAreas { get => ServiceAreas?.ToList<IServiceAreaModel>(); set => ServiceAreas = value?.Cast<ServiceAreaModel>().ToList(); }
        #endregion
    }
}
