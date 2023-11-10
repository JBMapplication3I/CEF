// <copyright file="TypeModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data Model for the type.</summary>
    /// <seealso cref="TypableBaseModel"/>
    /// <seealso cref="ITypeModel"/>
    public class TypeModel : TypableBaseModel, ITypeModel
    {
        #region IAmFilterableByNullableStoreModel Properties
        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableStoreModel.Store"/>
        [JsonIgnore]
        public StoreModel? Store { get; set; }

        /// <inheritdoc/>
        IStoreModel? IAmFilterableByNullableStoreModel.Store { get => Store; set => Store = (StoreModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Store Custom Key for objects")]
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Store Name for objects")]
        public string? StoreName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreSeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Store SEO URL for objects")]
        public string? StoreSeoUrl { get; set; }
        #endregion

        #region IAmFilterableByNullableBrandModel Properties
        /// <inheritdoc/>
        public int? BrandID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableBrandModel.Brand"/>
        [JsonIgnore]
        public BrandModel? Brand { get; set; }

        /// <inheritdoc/>
        IBrandModel? IAmFilterableByNullableBrandModel.Brand { get => Brand; set => Brand = (BrandModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Brand Custom Key for objects")]
        public string? BrandKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Brand Name for objects")]
        public string? BrandName { get; set; }
        #endregion
    }
}
