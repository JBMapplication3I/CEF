// <copyright file="ProductCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Product Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.ComponentModel;
    using ServiceStack;

    /// <summary>Form for viewing the product catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class ProductCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="ProductCatalogSearchForm"/> class.</summary>
        public ProductCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ProductCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public ProductCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
            if (other is ProductCatalogSearchForm asForm)
            {
                BrandName = asForm.BrandName;
            }
        }

        /// <summary>Gets or sets a value indicating whether to filter the catalog by current account roles.</summary>
        /// <value>True if filter by current account roles, false if not.</value>
        [ApiMember(Name = nameof(FilterByCurrentAccountRoles), DataType = "string?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? FilterByCurrentAccountRoles { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        [ApiMember(Name = nameof(BrandName), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? BrandName { get; set; }

        /// <inheritdoc/>
        public override void CopyFrom(SearchFormBase other)
        {
            base.CopyFrom(other);
            if (other is not ProductCatalogSearchForm asForm)
            {
                return;
            }
            BrandName = asForm.BrandName;
        }
    }
}
