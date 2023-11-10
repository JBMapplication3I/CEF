using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for VendorInventoryReport
/// </summary>
public class VendorInventoryReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel TB_ProductID;
    private XRLabel TB_Cost;
    private XRLabel TB_InvCount;
    private XRLabel TB_GrandTotal;
    private XRLabel TB_ListPrice;
    private DevExpress.DataAccess.EntityFramework.EFDataSource efDataSource1;
    private ReportHeaderBand ReportHeader;
    private CalculatedField GrandTotal;
    private GroupHeaderBand GroupHeader1;
    private XRLabel TB_VendorName;
    private XRLabel xrLabel7;
    private PageHeaderBand PageHeader;
    private XRLabel LB_GrandTotal;
    private XRLabel LB_InvCount;
    private XRLabel LB_ListPrice;
    private XRLabel LB_Cost;
    private XRLabel LB_ProductID;
    private XRLabel LB_VendorName;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo3;
    private XRPageInfo xrPageInfo4;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRControlStyle Style_Detail_Evens;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public VendorInventoryReport()
    {
        InitializeComponent();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.EntityFramework.EFConnectionParameters efConnectionParameters1 = new DevExpress.DataAccess.EntityFramework.EFConnectionParameters();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_ProductID = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_Cost = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_InvCount = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_GrandTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_ListPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.efDataSource1 = new DevExpress.DataAccess.EntityFramework.EFDataSource(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.GrandTotal = new DevExpress.XtraReports.UI.CalculatedField();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.TB_VendorName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_GrandTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_InvCount = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_ListPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_Cost = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_ProductID = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_VendorName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo4 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.Style_Detail_Evens = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.TB_ProductID,
            this.TB_Cost,
            this.TB_InvCount,
            this.TB_GrandTotal,
            this.TB_ListPrice});
            this.Detail.EvenStyleName = "Style_Detail_Evens";
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.Product.Name")});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(180F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(160F, 18F);
            this.xrLabel1.Text = "xrLabel1";
            // 
            // TB_ProductID
            // 
            this.TB_ProductID.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.ProductID")});
            this.TB_ProductID.LocationFloat = new DevExpress.Utils.PointFloat(110F, 0F);
            this.TB_ProductID.Name = "TB_ProductID";
            this.TB_ProductID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_ProductID.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.TB_ProductID.StylePriority.UseTextAlignment = false;
            this.TB_ProductID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TB_Cost
            // 
            this.TB_Cost.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.ActualCost", "{0:c2}")});
            this.TB_Cost.LocationFloat = new DevExpress.Utils.PointFloat(340F, 0F);
            this.TB_Cost.Name = "TB_Cost";
            this.TB_Cost.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_Cost.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.TB_Cost.StylePriority.UseTextAlignment = false;
            this.TB_Cost.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TB_InvCount
            // 
            this.TB_InvCount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.InventoryCount", "{0:n0}")});
            this.TB_InvCount.LocationFloat = new DevExpress.Utils.PointFloat(540F, 0F);
            this.TB_InvCount.Name = "TB_InvCount";
            this.TB_InvCount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_InvCount.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.TB_InvCount.StylePriority.UseTextAlignment = false;
            this.TB_InvCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TB_GrandTotal
            // 
            this.TB_GrandTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.GrandTotal", "{0:c2}")});
            this.TB_GrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(640F, 0F);
            this.TB_GrandTotal.Name = "TB_GrandTotal";
            this.TB_GrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_GrandTotal.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.TB_GrandTotal.StylePriority.UseTextAlignment = false;
            this.TB_GrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TB_ListPrice
            // 
            this.TB_ListPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.ListedPrice", "{0:c2}")});
            this.TB_ListPrice.LocationFloat = new DevExpress.Utils.PointFloat(440F, 0F);
            this.TB_ListPrice.Name = "TB_ListPrice";
            this.TB_ListPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_ListPrice.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.TB_ListPrice.StylePriority.UseTextAlignment = false;
            this.TB_ListPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // efDataSource1
            // 
            efConnectionParameters1.ConnectionString = "";
            efConnectionParameters1.ConnectionStringName = "ClarityEcommerceEntities";
            efConnectionParameters1.Source = typeof(Clarity.Ecommerce.DataModel.ClarityEcommerceEntities);
            this.efDataSource1.ConnectionParameters = efConnectionParameters1;
            this.efDataSource1.Name = "efDataSource1";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
            this.ReportHeader.HeightF = 51.45833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 24F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(750F, 41.45833F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Vendor Inventory Report";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // GrandTotal
            // 
            this.GrandTotal.DataMember = "VendorProducts";
            this.GrandTotal.Expression = "[ActualCost] * [InventoryCount]";
            this.GrandTotal.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
            this.GrandTotal.Name = "GrandTotal";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.TB_VendorName});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Vendor.Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 18F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // TB_VendorName
            // 
            this.TB_VendorName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "VendorProducts.Vendor.Name"),
            new DevExpress.XtraReports.UI.XRBinding("Bookmark", null, "VendorProducts.Vendor.Name")});
            this.TB_VendorName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.TB_VendorName.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.TB_VendorName.Name = "TB_VendorName";
            this.TB_VendorName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_VendorName.SizeF = new System.Drawing.SizeF(730F, 18F);
            this.TB_VendorName.StylePriority.UseFont = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.LB_GrandTotal,
            this.LB_InvCount,
            this.LB_ListPrice,
            this.LB_Cost,
            this.LB_ProductID,
            this.LB_VendorName});
            this.PageHeader.HeightF = 25.29167F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(180F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(160F, 18F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Product Name";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_GrandTotal
            // 
            this.LB_GrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_GrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(640F, 0F);
            this.LB_GrandTotal.Name = "LB_GrandTotal";
            this.LB_GrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_GrandTotal.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.LB_GrandTotal.StylePriority.UseFont = false;
            this.LB_GrandTotal.StylePriority.UseTextAlignment = false;
            this.LB_GrandTotal.Text = "Grand Total";
            this.LB_GrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_InvCount
            // 
            this.LB_InvCount.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_InvCount.LocationFloat = new DevExpress.Utils.PointFloat(540F, 0F);
            this.LB_InvCount.Name = "LB_InvCount";
            this.LB_InvCount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_InvCount.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.LB_InvCount.StylePriority.UseFont = false;
            this.LB_InvCount.StylePriority.UseTextAlignment = false;
            this.LB_InvCount.Text = "Inv. Count";
            this.LB_InvCount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_ListPrice
            // 
            this.LB_ListPrice.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_ListPrice.LocationFloat = new DevExpress.Utils.PointFloat(440F, 0F);
            this.LB_ListPrice.Name = "LB_ListPrice";
            this.LB_ListPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_ListPrice.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.LB_ListPrice.StylePriority.UseFont = false;
            this.LB_ListPrice.StylePriority.UseTextAlignment = false;
            this.LB_ListPrice.Text = "List Price";
            this.LB_ListPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_Cost
            // 
            this.LB_Cost.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_Cost.LocationFloat = new DevExpress.Utils.PointFloat(340F, 0F);
            this.LB_Cost.Name = "LB_Cost";
            this.LB_Cost.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_Cost.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.LB_Cost.StylePriority.UseFont = false;
            this.LB_Cost.StylePriority.UseTextAlignment = false;
            this.LB_Cost.Text = "Cost";
            this.LB_Cost.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_ProductID
            // 
            this.LB_ProductID.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_ProductID.LocationFloat = new DevExpress.Utils.PointFloat(110F, 0F);
            this.LB_ProductID.Name = "LB_ProductID";
            this.LB_ProductID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_ProductID.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.LB_ProductID.StylePriority.UseFont = false;
            this.LB_ProductID.StylePriority.UseTextAlignment = false;
            this.LB_ProductID.Text = "Product ID";
            this.LB_ProductID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LB_VendorName
            // 
            this.LB_VendorName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.LB_VendorName.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.LB_VendorName.Name = "LB_VendorName";
            this.LB_VendorName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_VendorName.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.LB_VendorName.StylePriority.UseFont = false;
            this.LB_VendorName.StylePriority.UseTextAlignment = false;
            this.LB_VendorName.Text = "Vendor";
            this.LB_VendorName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrPageInfo4});
            this.PageFooter.HeightF = 23F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Format = "Page {0} of {1}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(437F, 0F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            // 
            // xrPageInfo4
            // 
            this.xrPageInfo4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageInfo4.Name = "xrPageInfo4";
            this.xrPageInfo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo4.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo4.SizeF = new System.Drawing.SizeF(313F, 23F);
            this.xrPageInfo4.StylePriority.UseTextAlignment = false;
            this.xrPageInfo4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // Style_Detail_Evens
            // 
            this.Style_Detail_Evens.BackColor = System.Drawing.Color.Silver;
            this.Style_Detail_Evens.Name = "Style_Detail_Evens";
            this.Style_Detail_Evens.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // VendorInventoryReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.PageHeader,
            this.PageFooter});
            this.Bookmark = "Vendor Inventory Report";
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.GrandTotal});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.efDataSource1});
            this.DataMember = "VendorProducts";
            this.DataSource = this.efDataSource1;
            this.DisplayName = "Vendor Inventory Report";
            this.FilterString = "[InventoryCount] > 0";
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Style_Detail_Evens});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
