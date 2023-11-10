// <copyright file="MessageSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the message search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IMessageSearchModel"/>
    public partial class MessageSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(SentFromOrToUserID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Limit messages to relevancy by user id (From or To)")]
        public int? SentFromOrToUserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SentFromUserID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Limit messages to relevancy by from user id")]
        public int? SentFromUserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SentToUserID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Limit messages to relevancy by to user id")]
        public int? SentToUserID { get; set; }
    }
}
