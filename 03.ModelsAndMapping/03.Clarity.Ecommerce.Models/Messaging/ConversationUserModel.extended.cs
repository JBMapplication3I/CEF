// <copyright file="ConversationUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    public partial class ConversationUserModel
    {
        /// <inheritdoc/>
        public bool? IsTyping { get; set; }

        /// <inheritdoc/>
        public DateTime? LastHeartbeat { get; set; }
    }
}
