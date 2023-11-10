// <copyright file="IAmFilterableByCategoryModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByCategoryModel interface</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by category model.</summary>
    /// <typeparam name="TModel">Type of the category relationship model.</typeparam>
    public interface IAmFilterableByCategoryModel<TModel> : IBaseModel
        where TModel : IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel, new()
    {
        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        List<TModel>? Categories { get; set; }
    }

    /// <summary>Interface for am a category relationship table where category is the slave model.</summary>
    public interface IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
        : IAmARelationshipTableModel<CategoryModel>
    {
        /// <summary>Gets or sets the master name.</summary>
        /// <value>The master name.</value>
        string? MasterName { get; set; }

        /// <summary>Gets or sets the slave name.</summary>
        /// <value>The slave name.</value>
        string? SlaveName { get; set; }
    }

    /// <summary>Interface for am a relationship table model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IAmARelationshipTableModel
        : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        string? MasterKey { get; set; }

        /// <summary>Gets or sets the identifier of the slave.</summary>
        /// <value>The identifier of the slave.</value>
        int SlaveID { get; set; }

        /// <summary>Gets or sets the slave key.</summary>
        /// <value>The slave key.</value>
        string? SlaveKey { get; set; }
    }

    /// <summary>Interface for am a relationship table model.</summary>
    /// <typeparam name="TSlaveModel"> Type of the slave model.</typeparam>
    /// <seealso cref="IAmARelationshipTableModel"/>
    public interface IAmARelationshipTableModel<TSlaveModel>
        : IAmARelationshipTableModel
    {
        /// <summary>Gets or sets the slave.</summary>
        /// <value>The slave.</value>
        TSlaveModel? Slave { get; set; }
    }

    /// <summary>A model for the auction.</summary>
    public partial class AuctionModel
        : NameableBaseModel,
            IAmFilterableByCategoryModel<AuctionCategoryModel>
    {
#if MANUFACTURERADMIN
        public List<AuctionCategoryModel>? Categories { get; set; }
#endif
    }

    /// <summary>A model for the auction.</summary>
    public partial class BrandModel
        : IAmFilterableByCategoryModel<BrandCategoryModel>
    {
    }

    /// <summary>A model for the auction.</summary>
    public partial class FranchiseModel
        : NameableBaseModel,
            IAmFilterableByCategoryModel<FranchiseCategoryModel>
    {
    }

    /// <summary>A model for the auction.</summary>
    public partial class LotModel
        : NameableBaseModel,
            IAmFilterableByCategoryModel<LotCategoryModel>
    {
#if MANUFACTURERADMIN
        public List<LotCategoryModel>? Categories { get; set; }
#endif
    }

    /// <summary>A model for the auction.</summary>
    public partial class StoreModel : NameableBaseModel, IAmFilterableByCategoryModel<StoreCategoryModel>
    {
    }

    /// <content>A model for the auction category.</content>
    public partial class AuctionCategoryModel
        : AmARelationshipTableBaseModel,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
    {
#if MANUFACTURERADMIN
        public string? MasterName { get; set; }
        public string? SlaveName { get; set; }
        public CategoryModel? Slave { get; set; }
#endif
    }

    /// <content>A model for the brand category.</content>
    public partial class BrandCategoryModel
        : AmARelationshipTableBaseModel,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
    {
    }

    /// <content>A model for the franchise category.</content>
    public partial class FranchiseCategoryModel
        : AmARelationshipTableBaseModel,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
    {
    }

    /// <content>A model for the lot category.</content>
    public partial class LotCategoryModel
        : AmARelationshipTableBaseModel,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
    {
#if MANUFACTURERADMIN
        public string? MasterName { get; set; }
        public string? SlaveName { get; set; }
        public CategoryModel? Slave { get; set; }
#endif
    }

    /// <content>A model for the store category.</content>
    public partial class StoreCategoryModel
        : AmARelationshipTableBaseModel,
            IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
    {
    }
}
