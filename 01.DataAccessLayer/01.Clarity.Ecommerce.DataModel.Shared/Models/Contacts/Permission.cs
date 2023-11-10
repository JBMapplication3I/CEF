// <copyright file="Permission.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the permission class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPermission : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Contacts", "Permission")]
    public class Permission : IPermission
    {
        public Permission()
        {
        }

        public Permission(string name)
        {
            Name = name;
        }

        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false)]
        public string? Name { get; set; }
    }
}
