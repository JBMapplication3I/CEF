// <copyright file="Catalog.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the catalog.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class Catalog : CEFComponentUserControl
    {
        private int recentlyViewedProductsSize;
        private int personalizationProductsSize;

        protected bool IncludeRecentlyViewedProductsAttributeValue { get; private set; }

        protected string RecentlyViewedProductsSizeAttributeValue => recentlyViewedProductsSize.ToString();

        protected bool IncludePersonalizationProductsAttributeValue { get; private set; }

        protected string PersonalizationProductsSizeAttributeValue => personalizationProductsSize.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            IncludeRecentlyViewedProductsAttributeValue = Convert.ToBoolean(Component.GetParameter("IncludeRecentlyViewedProducts")?.Value ?? 0);
            recentlyViewedProductsSize = Convert.ToInt32(Component.GetParameter("RecentlyViewedProductsSize")?.Value ?? 0);
            IncludePersonalizationProductsAttributeValue = Convert.ToBoolean(Component.GetParameter("IncludePersonalizationProducts")?.Value ?? 0);
            personalizationProductsSize = Convert.ToInt32(Component.GetParameter("PersonalizationProductsSize")?.Value ?? 0);
        }
    }
}
