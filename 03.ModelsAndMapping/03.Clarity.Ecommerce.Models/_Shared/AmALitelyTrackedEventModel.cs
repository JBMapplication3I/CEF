// <copyright file="AmALitelyTrackedEventModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a litely tracked event model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the am a litely tracked event.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IAmALitelyTrackedEventBaseModel"/>
    public abstract class AmALitelyTrackedEventModel : NameableBaseModel, IAmALitelyTrackedEventBaseModel
    {
        #region IAmALitelyTrackedEvent Properties
        /// <inheritdoc/>
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        public int? Score { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        public string? AddressKey { get; set; }

        /// <inheritdoc cref="IAmALitelyTrackedEventBaseModel.Address"/>
        public AddressModel? Address { get; set; }

        /// <inheritdoc/>
        IAddressModel? IAmALitelyTrackedEventBaseModel.Address { get => Address; set => Address = (AddressModel?)value; }

        /// <inheritdoc/>
        public int? IPOrganizationID { get; set; }

        /// <inheritdoc/>
        public string? IPOrganizationKey { get; set; }

        /// <inheritdoc/>
        public string? IPOrganizationName { get; set; }

        /// <inheritdoc cref="IAmALitelyTrackedEventBaseModel.IPOrganization"/>
        public IPOrganizationModel? IPOrganization { get; set; }

        /// <inheritdoc/>
        IIPOrganizationModel? IAmALitelyTrackedEventBaseModel.IPOrganization { get => IPOrganization; set => IPOrganization = (IPOrganizationModel?)value; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc cref="IAmALitelyTrackedEventBaseModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IAmALitelyTrackedEventBaseModel.User { get => User; set => User = (UserModel?)value; }
        #endregion
    }
}
