// <copyright file="CategoriesMenuBar.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the categories menu bar.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class CategoriesMenuBar : CEFComponentUserControl
    {
        protected string CssClassAttributeValue { get; private set; }

        protected string RenderAttributeValue { get; private set; }

        protected string RenderInnerAttributeValue { get; private set; }

        protected string BehaviorAttributeValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CssClassAttributeValue = Convert.ToString(Component.GetParameter("CssClass")?.Value ?? string.Empty);
            RenderAttributeValue = Convert.ToString(Component.GetParameter("Render")?.Value ?? string.Empty);
            RenderInnerAttributeValue = Convert.ToString(Component.GetParameter("RenderInner")?.Value ?? string.Empty);
            BehaviorAttributeValue = Convert.ToString(Component.GetParameter("Behavior")?.Value ?? string.Empty);
        }
    }
}
