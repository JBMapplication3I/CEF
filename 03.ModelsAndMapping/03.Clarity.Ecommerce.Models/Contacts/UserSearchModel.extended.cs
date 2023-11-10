// <copyright file="UserSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the user search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IUserSearchModel"/>
    public partial class UserSearchModel
    {
        /// <inheritdoc/>
        public string? Name { get; set; }

        /// <inheritdoc/>
        public string? FirstName { get; set; }

        /// <inheritdoc/>
        public string? LastName { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccessibleFromAccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccessibleFromAccountID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserNameOrContactName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserNameOrContactName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserNameOrCustomKeyOrEmailOrContactName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserNameOrCustomKeyOrEmailOrContactName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrUserNameOrCustomKeyOrEmailOrContactName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? IDOrUserNameOrCustomKeyOrEmailOrContactName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserNameOrCustomKeyOrEmail), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserNameOrCustomKeyOrEmail { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserNameOrEmail), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserNameOrEmail { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(OnlineStatusID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? OnlineStatusID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccessibleLevels), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccessibleLevels { get; set; }
    }
}
