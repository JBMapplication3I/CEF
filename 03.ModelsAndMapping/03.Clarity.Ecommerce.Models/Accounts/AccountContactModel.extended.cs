// <copyright file="AccountContactModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account contact model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    /// <summary>A data Model for the account contact.</summary>
    public partial class AccountContactModel
    {
        /// <inheritdoc/>
        public bool IsPrimary { get; set; }

        /// <inheritdoc/>
        public bool IsBilling { get; set; }

        /// <inheritdoc/>
        public bool TransmittedToERP { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }
    }
}
