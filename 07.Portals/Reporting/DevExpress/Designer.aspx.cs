namespace Clarity.Ecommerce.Service.Reporting
{
    using System;

    public partial class Designer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
            var task = (DesignerTask)Session["DesignerTask"];
            if (task != null)
            {
                InitDesignerPage(task);
            }
            else if (!Page.IsCallback)
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void InitDesignerPage(DesignerTask task)
        {
            switch (task.Mode)
            {
                case ReportEditingMode.NewReport:
                {
                    // Create a new report from the template
                    ASPxReportDesigner1.OpenReport(new ReportTemplate());
                    break;
                }
                case ReportEditingMode.ModifyReport:
                {
                    // Load an existing report from the report storage
                    if (string.IsNullOrWhiteSpace(task.ReportID))
                    {
                        ASPxReportDesigner1.OpenReport(task.Report);
                    }
                    else
                    {
                        ASPxReportDesigner1.OpenReport(task.ReportID);
                    }
                    break;
                }
                // ReSharper disable once RedundantCaseLabel
                case ReportEditingMode.None:
                default:
                {
                    Response.Redirect("Default.aspx");
                    break;
                }
            }
        }

        /*protected void ASPxReportDesigner1_OnUnload(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }*/
    }
}
