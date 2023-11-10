// <copyright file="SearchCatalogExternalSearchBox.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search catalog external search box.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class SearchCatalogExternalSearchBox : CEFComponentUserControl
    {
        protected string CssClassAttributeValue { get; private set; }

        protected string StyleAttributeValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CssClassAttributeValue = Convert.ToString(Component.GetParameter("CssClass")?.Value ?? string.Empty);
            StyleAttributeValue = Convert.ToString(Component.GetParameter("Style")?.Value ?? string.Empty);
        }
    }
}
