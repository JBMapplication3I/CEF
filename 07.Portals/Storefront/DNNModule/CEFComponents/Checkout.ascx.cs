// <copyright file="Checkout.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class Checkout : CEFComponentUserControl
    {
        protected string CssClassAttributeValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CssClassAttributeValue = Convert.ToString(Component.GetParameter("CssClass")?.Value ?? string.Empty);
        }
    }
}
