using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Clarity.Ecommerce.Service.Reporting
{
    public partial class ReportTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand Detail;
        private XRLabel xrLabel2;
        private XRLabel xrLabel1;
        private ReportHeaderBand ReportHeader;
        private DevExpress.DataAccess.EntityFramework.EFDataSource efDataSource1;
        private IContainer components;
        private BottomMarginBand bottomMarginBand1;

        public ReportTemplate()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var efConnectionParameters1 = new DevExpress.DataAccess.EntityFramework.EFConnectionParameters();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.efDataSource1 = new DevExpress.DataAccess.EntityFramework.EFDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // topMarginBand1
            //
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            //
            // Detail
            //
            this.Detail.HeightF = 100F;
            this.Detail.Name = "Detail";
            //
            // bottomMarginBand1
            //
            this.bottomMarginBand1.HeightF = 50F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            //
            // xrLabel2
            //
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10.0001F, 10.00001F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(729.9999F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Sample Report";
            //
            // xrLabel1
            //
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.0001F, 37.5F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(729.9999F, 36.54168F);
            this.xrLabel1.Text = "To create a report layout, add report controls to this template. This report is b" +
    "ound to your Clarity Ecommerce Framework instance database by default.";
            //
            // ReportHeader
            //
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1});
            this.ReportHeader.HeightF = 86.45834F;
            this.ReportHeader.Name = "ReportHeader";
            //
            // efDataSource1
            //
            efConnectionParameters1.ConnectionString = "";
            efConnectionParameters1.ConnectionStringName = "ClarityEcommerceEntities";
            efConnectionParameters1.Source = typeof(Clarity.Ecommerce.DataModel.ClarityEcommerceEntities);
            this.efDataSource1.ConnectionParameters = efConnectionParameters1;
            this.efDataSource1.Name = "efDataSource1";
            //
            // ReportTemplate
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.Detail,
            this.bottomMarginBand1,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.efDataSource1});
            this.DataSource = this.efDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
