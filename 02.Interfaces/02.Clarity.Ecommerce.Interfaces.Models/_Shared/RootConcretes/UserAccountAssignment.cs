// <copyright file="UserAccountAssignment.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user account assignment class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>A user account assignment.</summary>
    public class UserAccountAssignment
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        public string? UserKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string? UserUserName { get; set; }

        /// <summary>Gets or sets the user email.</summary>
        /// <value>The user email.</value>
        public string? UserEmail { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        public int? AccountID { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        public string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        public string? AccountName { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                this,
                SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
