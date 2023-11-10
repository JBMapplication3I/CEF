// <copyright file="PermissionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the permission model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the permission.</summary>
    /// <seealso cref="Clarity.Ecommerce.Interfaces.Models.IPermissionModel"/>
    public class PermissionModel : IPermissionModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(Id), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string? Name { get; set; }
    }
}