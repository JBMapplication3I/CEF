// <autogenerated>
// <copyright file="IStoreCategoryModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by IModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for store category model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseModel"/>
    /// <seealso cref="IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel"/>
    /// <seealso cref="IAmAStoreRelationshipTableWhereStoreIsTheMasterModel{ICategoryModel}"/>
    /// <seealso cref="IAmFilterableByCategoryModel"/>
    /// <seealso cref="IAmFilterableByStoreModel"/>
    public partial interface IStoreCategoryModel
        : IAmARelationshipTableBaseModel
            , IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
            , IAmAStoreRelationshipTableWhereStoreIsTheMasterModel<ICategoryModel>
    {
        #region IAmARelationshipTable
        /// <summary>Gets or sets the name of the master.</summary>
        /// <value>The name of the master.</value>
        string? MasterName { get; set; }

        /// <summary>Gets or sets the name of the slave.</summary>
        /// <value>The name of the slave.</value>
        string? SlaveName { get; set; }
        #endregion
    }
}
