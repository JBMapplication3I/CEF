// <copyright file="RoleUser.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role user class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IRoleUser : IBase
    {
        #region RoleUser Properties
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the role.</summary>
        /// <value>The identifier of the role.</value>
        int RoleId { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        UserRole? Role { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserId { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? GroupID { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        Group? Group { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    [SqlSchema("Contacts", "RoleUser")]
    public class RoleUser : IdentityUserRole<int>, IRoleUser
    {
        #region Base Properties
        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public int ID { get; set; }

        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public string? CustomKey { get; set; }

        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public DateTime CreatedDate { get; set; }

        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public DateTime? UpdatedDate { get; set; }

        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public bool Active { get; set; }

        [NotMapped, Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public long? Hash { get; set; }

        public string? JsonAttributes { get; set; }

        [NotMapped, ReadOnly(true)]
        public SerializableAttributesDictionary SerializableAttributes => JsonAttributes.DeserializeAttributesDictionary();
        #endregion

        #region RoleUser Properties
        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? EndDate { get; set; }
        #endregion

        #region Related Properties
        /// <inheritdoc cref="IRoleUser.RoleId"/>
        [Key, Column(Order = 0)] // Foreign Key handled in modelBuilder
        public override int RoleId { get; set; }

        /// <inheritdoc/>
        public virtual UserRole? Role { get; set; }

        /// <inheritdoc cref="IRoleUser.UserId"/>
        [Key, Column(Order = 1)] // Foreign Key handled in modelBuilder
        public override int UserId { get; set; }

        /// <inheritdoc/>
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [ForeignKey("Group"), Column(Order = 2)] // Foreign Key handled in modelBuilder
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        public virtual Group? Group { get; set; }
        #endregion

        /// <inheritdoc/>
        [Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public object Clone()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [Obsolete("This is here to enforce compliance with an interface but is not actually used", true)]
        public string ToHashableString()
        {
            throw new NotImplementedException();
        }
    }
}
