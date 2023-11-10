// <autogenerated>
// <copyright file="InventoryLocationUser.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ISearchModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by ISearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective
#pragma warning disable IDE0005_gen
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for inventory location user model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    public partial interface IInventoryLocationUserSearchModel
        : IBaseSearchModel
            , IAmARelationshipTableBaseSearchModel
    {
        /// <summary>Gets or sets the name of the master.</summary>
        /// <value>The name of the master.</value>
        string? MasterName { get; set; }
    }
}
