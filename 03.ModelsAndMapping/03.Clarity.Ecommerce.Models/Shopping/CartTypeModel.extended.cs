// <copyright file="CartTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    public partial class CartTypeModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CreatedByUserID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Created by this User ID (custom wish lists)")]
        public int? CreatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }

        /// <inheritdoc cref="ICartTypeModel.CreatedByUser"/>
        [ApiMember(Name = nameof(CreatedByUser), DataType = "UserModel", ParameterType = "body", IsRequired = false,
            Description = "Created by this User (custom wish lists)")]
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? ICartTypeModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }
    }
}
