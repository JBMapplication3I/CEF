// <copyright file="IAccountContactModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountContactModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for account contact model.</summary>
    public partial interface IAccountContactModel
    {
        /// <summary>Gets or sets a value indicating whether this is billing.</summary>
        /// <value>True if this is billing, false if not.</value>
        bool IsBilling { get; set; }

        /// <summary>Gets or sets a value indicating whether this is primary.</summary>
        /// <value>True if this is primary, false if not.</value>
        bool IsPrimary { get; set; }

        /// <summary>Gets or sets a value indicating whether this record has been transmitted to ERP.</summary>
        /// <value>True if transmitted to ERP, false if not.</value>
        bool TransmittedToERP { get; set; }

        /// <summary>Gets or sets the slave phone.</summary>
        /// <value>The slave phone.</value>
        string? SlavePhone { get; set; }

        /// <summary>Gets or sets the slave fax.</summary>
        /// <value>The slave fax.</value>
        string? SlaveFax { get; set; }

        /// <summary>Gets or sets the slave email.</summary>
        /// <value>The slave email.</value>
        string? SlaveEmail { get; set; }

        /// <summary>Gets or sets the name of the slave first.</summary>
        /// <value>The name of the slave first.</value>
        string? SlaveFirstName { get; set; }

        /// <summary>Gets or sets the name of the slave last.</summary>
        /// <value>The name of the slave last.</value>
        string? SlaveLastName { get; set; }

        /// <summary>Gets or sets the end date of the account contact.</summary>
        /// <value>The EndDate.</value>
        DateTime? EndDate { get; set; }
    }
}
