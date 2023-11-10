// <copyright file="StoreCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Store Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.ComponentModel;
    using ServiceStack;

    /// <summary>Form for viewing the product catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class StoreCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="StoreCatalogSearchForm"/> class.</summary>
        public StoreCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="StoreCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public StoreCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }

        #region Badges
        /// <summary>Gets or sets the identifier of the badge.</summary>
        /// <value>The identifier of the badge.</value>
        [ApiMember(Name = nameof(BadgeID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? BadgeID { get; set; }

        /// <summary>Gets or sets the badge IDs any.</summary>
        /// <value>The badge IDs any.</value>
        [ApiMember(Name = nameof(BadgeIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? BadgeIDsAny { get; set; }

        /// <summary>Gets or sets the badge IDs all.</summary>
        /// <value>The badge IDs all.</value>
        [ApiMember(Name = nameof(BadgeIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? BadgeIDsAll { get; set; }
        #endregion

        /// <inheritdoc/>
        public override void CopyFrom(SearchFormBase other)
        {
            base.CopyFrom(other);
            if (other is not StoreCatalogSearchForm asForm)
            {
                return;
            }
            this.BadgeID = asForm.BadgeID;
            this.BadgeIDsAny = asForm.BadgeIDsAny;
            this.BadgeIDsAll = asForm.BadgeIDsAll;
        }
    }
}
