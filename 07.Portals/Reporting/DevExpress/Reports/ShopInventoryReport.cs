using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for ShopInventoryReport
/// </summary>
public class ShopInventoryReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel TB_GrandTotal;
    private XRLabel TB_Cost;
    private XRLabel TB_CostMult;
    private XRLabel xrLabel7;
    private XRLabel TB_ProductName;
    private XRLabel TB_Updated;
    private XRLabel TB_Stock;
    private DevExpress.DataAccess.EntityFramework.EFDataSource efDataSource1;
    private XRLabel TB_ShopHeader;
    private PageFooterBand pageFooterBand1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private ReportHeaderBand reportHeaderBand1;
    private XRLabel LB_ReportHeader;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private GroupHeaderBand GroupHeader1;
    private GroupHeaderBand GroupHeader2;
    private XRLabel TB_SectionHeader;
    private CalculatedField GrandTotal;
    private XRLabel LB_Updated;
    private XRLabel xrLabel1;
    private XRLabel LB_ProductName;
    private XRLabel TB_ListPrice;
    private XRLabel LB_CostMult;
    private XRLabel LB_Cost;
    private XRLabel LB_GrandTotal;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public ShopInventoryReport()
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.EntityFramework.EFConnectionParameters efConnectionParameters1 = new DevExpress.DataAccess.EntityFramework.EFConnectionParameters();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TB_GrandTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_Cost = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_CostMult = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_ProductName = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_Updated = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_Stock = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.efDataSource1 = new DevExpress.DataAccess.EntityFramework.EFDataSource(this.components);
            this.TB_ShopHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.LB_ReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.LB_GrandTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.TB_ListPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_CostMult = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_Cost = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_Updated = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.LB_ProductName = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.TB_SectionHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.GrandTotal = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.TB_GrandTotal,
            this.TB_Cost,
            this.TB_CostMult,
            this.xrLabel7,
            this.TB_ProductName,
            this.TB_Updated,
            this.TB_Stock});
            this.Detail.HeightF = 32F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TB_GrandTotal
            // 
            this.TB_GrandTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
                    "duct.VendorProducts.GrandTotal")});
            this.TB_GrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(640F, 0F);
            this.TB_GrandTotal.Name = "TB_GrandTotal";
            this.TB_GrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_GrandTotal.SizeF = new System.Drawing.SizeF(100F, 23F);
            xrSummary1.FormatString = "{0:c2}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.TB_GrandTotal.Summary = xrSummary1;
            // 
            // TB_Cost
            // 
            this.TB_Cost.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
                    "duct.VendorProducts.ActualCost", "{0:c2}")});
            this.TB_Cost.LocationFloat = new DevExpress.Utils.PointFloat(340F, 0F);
            this.TB_Cost.Name = "TB_Cost";
            this.TB_Cost.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_Cost.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // TB_CostMult
            // 
            this.TB_CostMult.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
                    "duct.VendorProducts.CostMultiplier", "{0:0.00%}")});
            this.TB_CostMult.LocationFloat = new DevExpress.Utils.PointFloat(440F, 0F);
            this.TB_CostMult.Name = "TB_CostMult";
            this.TB_CostMult.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_CostMult.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
                    "duct.VendorProducts.ListedPrice", "{0:c2}")});
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(540F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // TB_ProductName
            // 
            this.TB_ProductName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
                    "duct.Name")});
            this.TB_ProductName.LocationFloat = new DevExpress.Utils.PointFloat(20.00001F, 0F);
            this.TB_ProductName.Name = "TB_ProductName";
            this.TB_ProductName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_ProductName.SizeF = new System.Drawing.SizeF(120F, 23F);
            // 
            // TB_Updated
            // 
            this.TB_Updated.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Upd" +
                    "atedDate", "{0:MM/dd/yyyy hh:mm tt}")});
            this.TB_Updated.LocationFloat = new DevExpress.Utils.PointFloat(240F, 0F);
            this.TB_Updated.Name = "TB_Updated";
            this.TB_Updated.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_Updated.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // TB_Stock
            // 
            this.TB_Stock.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Qua" +
                    "ntity", "{0:#,#}")});
            this.TB_Stock.LocationFloat = new DevExpress.Utils.PointFloat(140F, 0F);
            this.TB_Stock.Name = "TB_Stock";
            this.TB_Stock.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_Stock.SizeF = new System.Drawing.SizeF(100F, 23F);
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
            // TB_ShopHeader
            // 
            this.TB_ShopHeader.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.TB_ShopHeader.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.Name")});
            this.TB_ShopHeader.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.TB_ShopHeader.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.TB_ShopHeader.Name = "TB_ShopHeader";
            this.TB_ShopHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_ShopHeader.SizeF = new System.Drawing.SizeF(730F, 26F);
            this.TB_ShopHeader.StyleName = "DataField";
            this.TB_ShopHeader.StylePriority.UseBorders = false;
            this.TB_ShopHeader.StylePriority.UseFont = false;
            this.TB_ShopHeader.StylePriority.UseTextAlignment = false;
            this.TB_ShopHeader.Text = "TB_ShopHeader";
            this.TB_ShopHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // pageFooterBand1
            // 
            this.pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrPageInfo2});
            this.pageFooterBand1.HeightF = 29F;
            this.pageFooterBand1.Name = "pageFooterBand1";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(375F, 23F);
            this.xrPageInfo1.StyleName = "PageInfo";
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(375F, 6F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(375F, 23F);
            this.xrPageInfo2.StyleName = "PageInfo";
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            // 
            // reportHeaderBand1
            // 
            this.reportHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.LB_ReportHeader});
            this.reportHeaderBand1.HeightF = 53F;
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // LB_ReportHeader
            // 
            this.LB_ReportHeader.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_ReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.LB_ReportHeader.Name = "LB_ReportHeader";
            this.LB_ReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_ReportHeader.SizeF = new System.Drawing.SizeF(750F, 35F);
            this.LB_ReportHeader.StyleName = "Title";
            this.LB_ReportHeader.StylePriority.UseBorders = false;
            this.LB_ReportHeader.Text = "Shop Inventory Report";
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 21F);
            this.Title.ForeColor = System.Drawing.Color.Black;
            this.Title.Name = "Title";
            // 
            // FieldCaption
            // 
            this.FieldCaption.BackColor = System.Drawing.Color.Transparent;
            this.FieldCaption.BorderColor = System.Drawing.Color.Black;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.FieldCaption.BorderWidth = 1F;
            this.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FieldCaption.ForeColor = System.Drawing.Color.Black;
            this.FieldCaption.Name = "FieldCaption";
            // 
            // PageInfo
            // 
            this.PageInfo.BackColor = System.Drawing.Color.Transparent;
            this.PageInfo.BorderColor = System.Drawing.Color.Black;
            this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PageInfo.BorderWidth = 1F;
            this.PageInfo.Font = new System.Drawing.Font("Arial", 8F);
            this.PageInfo.ForeColor = System.Drawing.Color.Black;
            this.PageInfo.Name = "PageInfo";
            // 
            // DataField
            // 
            this.DataField.BackColor = System.Drawing.Color.Transparent;
            this.DataField.BorderColor = System.Drawing.Color.Black;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1F;
            this.DataField.Font = new System.Drawing.Font("Arial", 9F);
            this.DataField.ForeColor = System.Drawing.Color.Black;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.LB_GrandTotal,
            this.TB_ListPrice,
            this.LB_CostMult,
            this.LB_Cost,
            this.LB_Updated,
            this.xrLabel1,
            this.LB_ProductName,
            this.TB_ShopHeader});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 48.99999F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // LB_GrandTotal
            // 
            this.LB_GrandTotal.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_GrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(639.9999F, 25.99999F);
            this.LB_GrandTotal.Name = "LB_GrandTotal";
            this.LB_GrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_GrandTotal.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.LB_GrandTotal.StylePriority.UseBorders = false;
            this.LB_GrandTotal.Text = "Grand Total";
            // 
            // TB_ListPrice
            // 
            this.TB_ListPrice.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.TB_ListPrice.LocationFloat = new DevExpress.Utils.PointFloat(540F, 25.99999F);
            this.TB_ListPrice.Name = "TB_ListPrice";
            this.TB_ListPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_ListPrice.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.TB_ListPrice.StylePriority.UseBorders = false;
            this.TB_ListPrice.Text = "List Price";
            // 
            // LB_CostMult
            // 
            this.LB_CostMult.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_CostMult.LocationFloat = new DevExpress.Utils.PointFloat(440F, 25.99999F);
            this.LB_CostMult.Name = "LB_CostMult";
            this.LB_CostMult.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_CostMult.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.LB_CostMult.StylePriority.UseBorders = false;
            this.LB_CostMult.Text = "Cost Mult.";
            // 
            // LB_Cost
            // 
            this.LB_Cost.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_Cost.LocationFloat = new DevExpress.Utils.PointFloat(340F, 25.99999F);
            this.LB_Cost.Name = "LB_Cost";
            this.LB_Cost.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_Cost.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.LB_Cost.StylePriority.UseBorders = false;
            this.LB_Cost.Text = "Cost";
            // 
            // LB_Updated
            // 
            this.LB_Updated.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_Updated.LocationFloat = new DevExpress.Utils.PointFloat(240F, 25.99999F);
            this.LB_Updated.Name = "LB_Updated";
            this.LB_Updated.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_Updated.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.LB_Updated.StylePriority.UseBorders = false;
            this.LB_Updated.Text = "Updated";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(140F, 25.99999F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.Text = "Stock";
            // 
            // LB_ProductName
            // 
            this.LB_ProductName.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.LB_ProductName.LocationFloat = new DevExpress.Utils.PointFloat(20.00001F, 25.99999F);
            this.LB_ProductName.Name = "LB_ProductName";
            this.LB_ProductName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LB_ProductName.SizeF = new System.Drawing.SizeF(120F, 23F);
            this.LB_ProductName.StylePriority.UseBorders = false;
            this.LB_ProductName.Text = "Product";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.TB_SectionHeader});
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("InventoryLocationSections.Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader2.HeightF = 23F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // TB_SectionHeader
            // 
            this.TB_SectionHeader.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "InventoryLocations.InventoryLocationSections.Name")});
            this.TB_SectionHeader.LocationFloat = new DevExpress.Utils.PointFloat(20.00001F, 0F);
            this.TB_SectionHeader.Name = "TB_SectionHeader";
            this.TB_SectionHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TB_SectionHeader.SizeF = new System.Drawing.SizeF(719.9999F, 23F);
            // 
            // GrandTotal
            // 
            this.GrandTotal.DataMember = "InventoryLocations.InventoryLocationSections.ProductInventoryLocationSections.Pro" +
    "duct.VendorProducts";
            this.GrandTotal.Name = "GrandTotal";
            // 
            // ShopInventoryReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.GroupHeader1,
            this.GroupHeader2});
            this.Bookmark = "Shop Inventory Report";
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.GrandTotal});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.efDataSource1});
            this.DataMember = "InventoryLocations";
            this.DataSource = this.efDataSource1;
            this.DisplayName = "Shop Inventory Report";
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.efDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
