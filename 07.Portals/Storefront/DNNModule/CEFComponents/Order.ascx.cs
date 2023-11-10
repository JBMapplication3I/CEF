// <copyright file="Order.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the order.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class Order : CEFComponentUserControl
    {
        protected string OrderTypeAttributeValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            OrderTypeAttributeValue = Convert.ToString(Component.GetParameter("OrderType")?.Value ?? string.Empty);
        }
    }
}
