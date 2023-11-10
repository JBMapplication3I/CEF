// <copyright file="UserLogin.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user login class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IUserLogin : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the login provider.</summary>
        /// <value>The login provider.</value>
        string? LoginProvider { get; set; }

        /// <summary>Gets or sets the provider key.</summary>
        /// <value>The provider key.</value>
        string? ProviderKey { get; set; }

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

    [SqlSchema("Contacts", "UserLogin")]
    public class UserLogin : IdentityUserLogin<int>, IUserLogin
    {
        /// <inheritdoc/>
        [Key, Column(Order = 1), StringLength(128), StringIsUnicode(false)]
        public override string? LoginProvider { get; set; }

        /// <inheritdoc/>
        [Key, Column(Order = 2), StringLength(128), StringIsUnicode(false)]
        public override string? ProviderKey { get; set; }

        /// <inheritdoc/>
        [Key, Column(Order = 0), InverseProperty(nameof(DataModel.User.Id)), ForeignKey(nameof(User))]
        public override int UserId { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
    }
}
