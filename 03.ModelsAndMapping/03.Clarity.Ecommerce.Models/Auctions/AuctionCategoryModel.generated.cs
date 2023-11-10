// <autogenerated>
// <copyright file="AuctionCategoryModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Model Interfaces generated to provide base setups</summary>
// <remarks>This file was auto-generated by Models.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#pragma warning disable 618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data transfer model for Auction Category.</summary>
    public partial class AuctionCategoryModel
        : AmARelationshipTableBaseModel
            , IAuctionCategoryModel
    {
        #region IAmFilterableByCategory Properties
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveSeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The SEO URL of the Category.")]
        public string? SlaveSeoUrl { get; set; } // C2

        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByCategoryModel.CategoryID { get => SlaveID; set => SlaveID = value; } // C2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByCategoryModel.CategoryKey { get => SlaveKey; set => SlaveKey = value; } // C2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByCategoryModel.CategoryName { get => SlaveName; set => SlaveName = value; } // C2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByCategoryModel.CategorySeoUrl { get => SlaveSeoUrl; set => SlaveSeoUrl = value; } // C2

        /// <inheritdoc cref="IAmFilterableByCategoryModel.Category"/>
        [JsonIgnore]
        ICategoryModel? IAmFilterableByCategoryModel.Category { get => Slave; set => Slave = (CategoryModel?)value; } // C2
        #endregion
        #region IAmARelationshipTable<IAuctionModel,ICategoryModel>
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Master record.")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Slave record.")]
        public string? SlaveName { get; set; }

        /// <inheritdoc/>
        public AuctionModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{ICategoryModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "CategoryModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public CategoryModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        ICategoryModel? IAmARelationshipTableBaseModel<ICategoryModel>.Slave { get => Slave; set => Slave = (CategoryModel?)value; }
        #endregion
    }
}
