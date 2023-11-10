// <copyright file="_CEFAdmin.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef admin.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    public partial class CEFAdmin : CEFComponentUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientResourceManager.RegisterStyleSheet(Page, $"{CEF_UI_Base}/lib/cef/css/admin-dnn-bundle.css");
            ClientResourceManager.RegisterScript(Page, $"{CEF_UI_Base}/lib/cef/framework/ClarityEcommerceAdmin.js", 100, "DnnFormBottomProvider");
            ClientResourceManager.RegisterScript(Page, "https://use.fontawesome.com/e1eefd7c43.js", 100, "DnnFormBottomProvider");
        }
    }
}
