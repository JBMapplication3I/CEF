// <copyright file="Default.aspx.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default.aspx class</summary>
namespace Clarity.Ecommerce.Service.Reporting
{
    using System;
    using System.Web.UI;

    // ReSharper disable once InconsistentNaming
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) { return; }
            OnCreateNewReportClick(null, null);
        }

        protected void OnCreateNewReportClick(object sender, EventArgs e)
        {
            Session["DesignerTask"] = new DesignerTask
            {
                Mode = ReportEditingMode.NewReport
            };
            Response.Redirect("Designer.aspx");
        }
    }
}
