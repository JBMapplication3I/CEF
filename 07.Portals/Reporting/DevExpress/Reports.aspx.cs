namespace Clarity.Ecommerce.Service.Reporting
{
    using System;
    using System.Web.UI.WebControls;
    using DevExpress.XtraPrinting.Native;

    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
            if (DropDownList1.Items.Count > 0)
            {
                // Already done
                return;
                ////DropDownList1.Items.Clear();
            }
            DropDownList1.Items.Add(new ListItem(string.Empty, string.Empty));
            var urlsDictionary = Global.ReportStorage.GetUrls();
            foreach (var kvp in urlsDictionary)
            {
                DropDownList1.Items.Add(new ListItem(kvp.Value, kvp.Key));
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var text = DropDownList1?.SelectedItem?.Value;
            if (text?.IsEmpty() != false)
            {
                return;
            }
            WebDocumentViewer1.OpenReport(text);
        }
    }
}
