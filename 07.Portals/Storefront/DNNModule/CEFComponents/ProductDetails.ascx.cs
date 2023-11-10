// <copyright file="ProductDetails.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product details.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;
    using System.IO;
    using System.Web;

    public partial class Product : CEFComponentUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddProductMetaService();
        }

        private void AddProductMetaService()
        {
            CEFProductMetaServicePlaceholder.Controls.Clear();
            if (!File.Exists($"{HttpRuntime.AppDomainAppPath}DesktopModules\\ClarityEcommerce\\MetaService\\ProductMetaService.ascx"))
            {
                return;
            }
            var control = LoadControl("~/DesktopModules/ClarityEcommerce/MetaService/ProductMetaService.ascx");
            if (control != null)
            {
                CEFProductMetaServicePlaceholder.Controls.Add(control);
            }
        }
    }
}
