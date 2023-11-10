// <autogenerated>
// <copyright file="MessageRecipientSearchModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchModel Classes generated to provide base setups.</summary>
// <remarks>This file was auto-generated by SearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable MissingXmlDoc, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective, UnusedMember.Global
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the Message Recipient search.</summary>
    public partial class MessageRecipientSearchModel
        : AmARelationshipTableBaseSearchModel
        , IMessageRecipientSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel
        #endregion
        #region IAmFilterableByUserSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "User ID For Search, Note: This will be overridden on data calls automatically")]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for UserID field")]
        public bool? UserIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Key for objects")]
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserUsername), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Name for objects")]
        public string? UserUsername { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDOrCustomKeyOrUserName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against User assigned to the object (includes UserName even though the name of the property doesn't say it)")]
        public string? UserIDOrCustomKeyOrUserName { get; set; }
        #endregion
        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSentAnEmail), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSentAnEmail { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsArchived), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IsArchived { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsRead), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IsRead { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinArchivedAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MinArchivedAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxArchivedAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MaxArchivedAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchArchivedAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MatchArchivedAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchArchivedAtIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchArchivedAtIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinEmailSentAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MinEmailSentAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxEmailSentAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MaxEmailSentAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchEmailSentAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MatchEmailSentAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchEmailSentAtIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchEmailSentAtIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(GroupID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(GroupIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? GroupIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinReadAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MinReadAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxReadAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MaxReadAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchReadAt), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? MatchReadAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchReadAtIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchReadAtIncludeNull { get; set; }
    }
}
