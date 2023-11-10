// <copyright file="Cart.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class Cart : CEFComponentUserControl
    {
        private string cartType;
        private bool includeQuickOrder;
        private bool includeDiscounts;

        protected string CartTypeAttributeValue => cartType ?? string.Empty;

        protected string IncludeQuickOrderAttributeValue => includeQuickOrder ? "true" : "false";

        protected string IncludeDiscountsAttributeValue => includeDiscounts ? "true" : "false";

        protected void Page_Load(object sender, EventArgs e)
        {
            cartType = Convert.ToString(Component.GetParameter("CartType")?.Value ?? string.Empty);
            includeQuickOrder = Convert.ToBoolean(Component.GetParameter("IncludeQuickOrder")?.Value ?? false);
            includeDiscounts = Convert.ToBoolean(Component.GetParameter("IncludeDiscounts")?.Value ?? false);
        }
    }
}
