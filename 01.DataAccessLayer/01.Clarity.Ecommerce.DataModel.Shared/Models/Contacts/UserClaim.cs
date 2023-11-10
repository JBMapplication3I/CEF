// <copyright file="UserClaim.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user claim class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IUserClaim : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int Id { get; set; }

        /// <summary>Gets or sets the type of the claim.</summary>
        /// <value>The type of the claim.</value>
        string ClaimType { get; set; }

        /// <summary>Gets or sets the claim value.</summary>
        /// <value>The claim value.</value>
        string ClaimValue { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserId { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    [SqlSchema("Contacts", "UserClaim")]
    public class UserClaim : IdentityUserClaim<int>, IUserClaim
    {
        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        /// <inheritdoc/>
        // [InverseProperty(nameof(ID)), ForeignKey(nameof(User))] // Handled in modelBuilder
        public override int UserId { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
    }
}
