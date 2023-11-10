// <copyright file="IPOrganizationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IP organization model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A data Model for the IP organization.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="Interfaces.Models.IIPOrganizationModel"/>
    public partial class IPOrganizationModel
    {
        /// <inheritdoc/>
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        public int? Score { get; set; }

        /// <inheritdoc/>
        public string? VisitorKey { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? PrimaryUserID { get; set; }

        /// <inheritdoc/>
        public string? PrimaryUserKey { get; set; }

        /// <inheritdoc cref="IIPOrganizationModel.PrimaryUser"/>
        [JsonIgnore]
        public UserModel? PrimaryUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IIPOrganizationModel.PrimaryUser { get => PrimaryUser; set => PrimaryUser = (UserModel?)value; }

        /// <inheritdoc/>
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        public string? AddressKey { get; set; }

        /// <inheritdoc cref="IIPOrganizationModel.Address"/>
        public AddressModel? Address { get; set; }

        /// <inheritdoc/>
        IAddressModel? IIPOrganizationModel.Address { get => Address; set => Address = (AddressModel?)value; }
        #endregion
    }
}
