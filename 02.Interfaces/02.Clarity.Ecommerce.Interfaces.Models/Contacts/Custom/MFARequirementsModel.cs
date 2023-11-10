// <copyright file="MFARequirementsModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mfa requirements model class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>A data Model for the mfa requirements.</summary>
    public class MFARequirementsModel
    {
        /// <summary>Gets or sets a value indicating whether the phone.</summary>
        /// <value>True if phone, false if not.</value>
        public bool Phone { get; set; }

        /// <summary>Gets or sets a value indicating whether the email.</summary>
        /// <value>True if email, false if not.</value>
        public bool Email { get; set; }

        /// <summary>Gets or sets the phone last four.</summary>
        /// <value>The phone last four.</value>
        public string? PhoneLastFour { get; set; }

        /// <summary>Gets or sets the email first and last four.</summary>
        /// <value>The email first and last four.</value>
        public string? EmailFirstAndLastFour { get; set; }
    }
}
