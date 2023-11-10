// <copyright file="GroupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the group model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the group.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IGroupModel"/>
    public partial class GroupModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int? GroupOwnerID { get; set; }

        /// <inheritdoc/>
        public string? GroupOwnerKey { get; set; }

        /// <inheritdoc cref="IGroupModel.GroupOwner"/>
        public UserModel? GroupOwner { get; set; }

        /// <inheritdoc/>
        IUserModel? IGroupModel.GroupOwner { get => GroupOwner; set => GroupOwner = (UserModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IGroupModel.GroupUsers"/>
        public List<GroupUserModel>? GroupUsers { get; set; }

        /// <inheritdoc/>
        List<IGroupUserModel>? IGroupModel.GroupUsers { get => GroupUsers?.ToList<IGroupUserModel>(); set => GroupUsers = value?.Cast<GroupUserModel>().ToList(); }
        #endregion
    }
}
