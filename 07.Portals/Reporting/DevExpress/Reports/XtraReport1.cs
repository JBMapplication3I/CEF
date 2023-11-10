using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel11;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrLabel16;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private XRLabel xrLabel19;
    private XRLabel xrLabel20;
    private XRLabel xrLabel21;
    private XRLabel xrLabel22;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private XRLabel xrLabel31;
    private XRLabel xrLabel32;
    private XRLabel xrLabel33;
    private XRLabel xrLabel34;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRLabel xrLabel38;
    private XRLabel xrLabel39;
    private XRLabel xrLabel40;
    private XRLabel xrLabel41;
    private XRLabel xrLabel42;
    private XRLabel xrLabel43;
    private XRLabel xrLabel44;
    private XRLabel xrLabel45;
    private XRLabel xrLabel46;
    private XRLabel xrLabel47;
    private XRLabel xrLabel48;
    private XRLabel xrLabel49;
    private XRLabel xrLabel50;
    private XRLabel xrLabel51;
    private XRLabel xrLabel52;
    private XRLabel xrLabel53;
    private XRLabel xrLabel54;
    private XRLabel xrLabel55;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private XRLabel xrLabel58;
    private XRLabel xrLabel59;
    private XRLabel xrLabel60;
    private XRLine xrLine1;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private PageFooterBand pageFooterBand1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private ReportHeaderBand reportHeaderBand1;
    private XRLabel xrLabel61;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReport1()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
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
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReport1));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            //
            // Detail
            //
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel15,
            this.xrLabel16,
            this.xrLabel17,
            this.xrLabel18,
            this.xrLabel19,
            this.xrLabel20,
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel23,
            this.xrLabel24,
            this.xrLabel25,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel28,
            this.xrLabel29,
            this.xrLabel30,
            this.xrLabel31,
            this.xrLabel32,
            this.xrLabel33,
            this.xrLabel34,
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel40,
            this.xrLabel41,
            this.xrLabel42,
            this.xrLabel43,
            this.xrLabel44,
            this.xrLabel45,
            this.xrLabel46,
            this.xrLabel47,
            this.xrLabel48,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel51,
            this.xrLabel52,
            this.xrLabel53,
            this.xrLabel54,
            this.xrLabel55,
            this.xrLabel56,
            this.xrLabel57,
            this.xrLabel58,
            this.xrLabel59,
            this.xrLabel60,
            this.xrLine1});
            this.Detail.HeightF = 728F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            //
            // TopMargin
            //
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            //
            // BottomMargin
            //
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            //
            // sqlDataSource1
            //
            this.sqlDataSource1.ConnectionName = "sqla\\sql2014_CEF_4_5_0_BUL_Connection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "Query";
            customSqlQuery1.Sql = resources.GetString("customSqlQuery1.Sql");
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            //
            // xrLabel1
            //
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(6F, 9F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel1.StyleName = "FieldCaption";
            this.xrLabel1.Text = "Carat";
            //
            // xrLabel2
            //
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(6F, 33F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel2.StyleName = "FieldCaption";
            this.xrLabel2.Text = "Category";
            //
            // xrLabel3
            //
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(6F, 57F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel3.StyleName = "FieldCaption";
            this.xrLabel3.Text = "Certificate";
            //
            // xrLabel4
            //
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(6F, 81F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel4.StyleName = "FieldCaption";
            this.xrLabel4.Text = "Certificates";
            //
            // xrLabel5
            //
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(6F, 105F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel5.StyleName = "FieldCaption";
            this.xrLabel5.Text = "Class";
            //
            // xrLabel6
            //
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(6F, 129F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel6.StyleName = "FieldCaption";
            this.xrLabel6.Text = "Color";
            //
            // xrLabel7
            //
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(6F, 153F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel7.StyleName = "FieldCaption";
            this.xrLabel7.Text = "Comments";
            //
            // xrLabel8
            //
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(6F, 177F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel8.StyleName = "FieldCaption";
            this.xrLabel8.Text = "Cost";
            //
            // xrLabel9
            //
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(6F, 201F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel9.StyleName = "FieldCaption";
            this.xrLabel9.Text = "Cut";
            //
            // xrLabel10
            //
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(6F, 225F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel10.StyleName = "FieldCaption";
            this.xrLabel10.Text = "Cut#1";
            //
            // xrLabel11
            //
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(6F, 249F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel11.StyleName = "FieldCaption";
            this.xrLabel11.Text = "Cut#2";
            //
            // xrLabel12
            //
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(6F, 273F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel12.StyleName = "FieldCaption";
            this.xrLabel12.Text = "Discount";
            //
            // xrLabel13
            //
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(6F, 297F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel13.StyleName = "FieldCaption";
            this.xrLabel13.Text = "Image Name";
            //
            // xrLabel14
            //
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(6F, 321F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel14.StyleName = "FieldCaption";
            this.xrLabel14.Text = "Mainstone";
            //
            // xrLabel15
            //
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(6F, 345F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel15.StyleName = "FieldCaption";
            this.xrLabel15.Text = "Margin";
            //
            // xrLabel16
            //
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(6F, 369F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel16.StyleName = "FieldCaption";
            this.xrLabel16.Text = "Memo#";
            //
            // xrLabel17
            //
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(6F, 393F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel17.StyleName = "FieldCaption";
            this.xrLabel17.Text = "Metal";
            //
            // xrLabel18
            //
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(6F, 417F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel18.StyleName = "FieldCaption";
            this.xrLabel18.Text = "Movement Date";
            //
            // xrLabel19
            //
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(6F, 441F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel19.StyleName = "FieldCaption";
            this.xrLabel19.Text = "pricerange";
            //
            // xrLabel20
            //
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(6F, 465F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel20.StyleName = "FieldCaption";
            this.xrLabel20.Text = "Product ID";
            //
            // xrLabel21
            //
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(6F, 489F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel21.StyleName = "FieldCaption";
            this.xrLabel21.Text = "Product Name";
            //
            // xrLabel22
            //
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(6F, 513F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel22.StyleName = "FieldCaption";
            this.xrLabel22.Text = "Qty";
            //
            // xrLabel23
            //
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(6F, 537F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel23.StyleName = "FieldCaption";
            this.xrLabel23.Text = "Retail";
            //
            // xrLabel24
            //
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(6F, 561F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel24.StyleName = "FieldCaption";
            this.xrLabel24.Text = "Sap Code";
            //
            // xrLabel25
            //
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(6F, 585F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel25.StyleName = "FieldCaption";
            this.xrLabel25.Text = "sellingprice";
            //
            // xrLabel26
            //
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(6F, 609F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel26.StyleName = "FieldCaption";
            this.xrLabel26.Text = "Status";
            //
            // xrLabel27
            //
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(6F, 633F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel27.StyleName = "FieldCaption";
            this.xrLabel27.Text = "Store";
            //
            // xrLabel28
            //
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(6F, 657F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel28.StyleName = "FieldCaption";
            this.xrLabel28.Text = "Supplier";
            //
            // xrLabel29
            //
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(6F, 681F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel29.StyleName = "FieldCaption";
            this.xrLabel29.Text = "Type";
            //
            // xrLabel30
            //
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(6F, 705F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(225F, 18F);
            this.xrLabel30.StyleName = "FieldCaption";
            this.xrLabel30.Text = "Vendor";
            //
            // xrLabel31
            //
            this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Carat")});
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(237F, 9F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel31.StyleName = "DataField";
            this.xrLabel31.Text = "xrLabel31";
            //
            // xrLabel32
            //
            this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Category")});
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(237F, 33F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel32.StyleName = "DataField";
            this.xrLabel32.Text = "xrLabel32";
            //
            // xrLabel33
            //
            this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Certificate")});
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(237F, 57F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel33.StyleName = "DataField";
            this.xrLabel33.Text = "xrLabel33";
            //
            // xrLabel34
            //
            this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Certificates")});
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(237F, 81F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel34.StyleName = "DataField";
            this.xrLabel34.Text = "xrLabel34";
            //
            // xrLabel35
            //
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Class")});
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(237F, 105F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel35.StyleName = "DataField";
            this.xrLabel35.Text = "xrLabel35";
            //
            // xrLabel36
            //
            this.xrLabel36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Color")});
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(237F, 129F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel36.StyleName = "DataField";
            this.xrLabel36.Text = "xrLabel36";
            //
            // xrLabel37
            //
            this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Comments")});
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(237F, 153F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel37.StyleName = "DataField";
            this.xrLabel37.Text = "xrLabel37";
            //
            // xrLabel38
            //
            this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Cost")});
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(237F, 177F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel38.StyleName = "DataField";
            this.xrLabel38.Text = "xrLabel38";
            //
            // xrLabel39
            //
            this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Cut")});
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(237F, 201F);
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel39.StyleName = "DataField";
            this.xrLabel39.Text = "xrLabel39";
            //
            // xrLabel40
            //
            this.xrLabel40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Cut#1")});
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(237F, 225F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel40.StyleName = "DataField";
            this.xrLabel40.Text = "xrLabel40";
            //
            // xrLabel41
            //
            this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Cut#2")});
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(237F, 249F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel41.StyleName = "DataField";
            this.xrLabel41.Text = "xrLabel41";
            //
            // xrLabel42
            //
            this.xrLabel42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Discount")});
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(237F, 273F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel42.StyleName = "DataField";
            this.xrLabel42.Text = "xrLabel42";
            //
            // xrLabel43
            //
            this.xrLabel43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.ImageName")});
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(237F, 297F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel43.StyleName = "DataField";
            this.xrLabel43.Text = "xrLabel43";
            //
            // xrLabel44
            //
            this.xrLabel44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Mainstone")});
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(237F, 321F);
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel44.StyleName = "DataField";
            this.xrLabel44.Text = "xrLabel44";
            //
            // xrLabel45
            //
            this.xrLabel45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Margin")});
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(237F, 345F);
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel45.StyleName = "DataField";
            this.xrLabel45.Text = "xrLabel45";
            //
            // xrLabel46
            //
            this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Memo#")});
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(237F, 369F);
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel46.StyleName = "DataField";
            this.xrLabel46.Text = "xrLabel46";
            //
            // xrLabel47
            //
            this.xrLabel47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Metal")});
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(237F, 393F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel47.StyleName = "DataField";
            this.xrLabel47.Text = "xrLabel47";
            //
            // xrLabel48
            //
            this.xrLabel48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.MovementDate")});
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(237F, 417F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel48.StyleName = "DataField";
            this.xrLabel48.Text = "xrLabel48";
            //
            // xrLabel49
            //
            this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.pricerange")});
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(237F, 441F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel49.StyleName = "DataField";
            this.xrLabel49.Text = "xrLabel49";
            //
            // xrLabel50
            //
            this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.ProductID")});
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(237F, 465F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel50.StyleName = "DataField";
            this.xrLabel50.Text = "xrLabel50";
            //
            // xrLabel51
            //
            this.xrLabel51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.ProductName")});
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(237F, 489F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel51.StyleName = "DataField";
            this.xrLabel51.Text = "xrLabel51";
            //
            // xrLabel52
            //
            this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Qty")});
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(237F, 513F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel52.StyleName = "DataField";
            this.xrLabel52.Text = "xrLabel52";
            //
            // xrLabel53
            //
            this.xrLabel53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Retail")});
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(237F, 537F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel53.StyleName = "DataField";
            this.xrLabel53.Text = "xrLabel53";
            //
            // xrLabel54
            //
            this.xrLabel54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.SapCode")});
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(237F, 561F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel54.StyleName = "DataField";
            this.xrLabel54.Text = "xrLabel54";
            //
            // xrLabel55
            //
            this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.sellingprice")});
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(237F, 585F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel55.StyleName = "DataField";
            this.xrLabel55.Text = "xrLabel55";
            //
            // xrLabel56
            //
            this.xrLabel56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Status")});
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(237F, 609F);
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel56.StyleName = "DataField";
            this.xrLabel56.Text = "xrLabel56";
            //
            // xrLabel57
            //
            this.xrLabel57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Store")});
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(237F, 633F);
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel57.StyleName = "DataField";
            this.xrLabel57.Text = "xrLabel57";
            //
            // xrLabel58
            //
            this.xrLabel58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Supplier")});
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(237F, 657F);
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel58.StyleName = "DataField";
            this.xrLabel58.Text = "xrLabel58";
            //
            // xrLabel59
            //
            this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Type")});
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(237F, 681F);
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel59.StyleName = "DataField";
            this.xrLabel59.Text = "xrLabel59";
            //
            // xrLabel60
            //
            this.xrLabel60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.Vendor")});
            this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(237F, 705F);
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel60.SizeF = new System.Drawing.SizeF(657F, 18F);
            this.xrLabel60.StyleName = "DataField";
            this.xrLabel60.Text = "xrLabel60";
            //
            // xrLine1
            //
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(6F, 3F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(888F, 2F);
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
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(6F, 6F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(438F, 23F);
            this.xrPageInfo1.StyleName = "PageInfo";
            //
            // xrPageInfo2
            //
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(456F, 6F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(438F, 23F);
            this.xrPageInfo2.StyleName = "PageInfo";
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            //
            // reportHeaderBand1
            //
            this.reportHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel61});
            this.reportHeaderBand1.HeightF = 61F;
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            //
            // xrLabel61
            //
            this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(6F, 6F);
            this.xrLabel61.Name = "xrLabel61";
            this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel61.SizeF = new System.Drawing.SizeF(888F, 43F);
            this.xrLabel61.StyleName = "Title";
            this.xrLabel61.Text = "Sales By Aging Report Version 2";
            //
            // Title
            //
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.Teal;
            this.Title.Name = "Title";
            //
            // FieldCaption
            //
            this.FieldCaption.BackColor = System.Drawing.Color.Transparent;
            this.FieldCaption.BorderColor = System.Drawing.Color.Black;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.FieldCaption.BorderWidth = 1F;
            this.FieldCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FieldCaption.ForeColor = System.Drawing.Color.Black;
            this.FieldCaption.Name = "FieldCaption";
            //
            // PageInfo
            //
            this.PageInfo.BackColor = System.Drawing.Color.Transparent;
            this.PageInfo.BorderColor = System.Drawing.Color.Black;
            this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PageInfo.BorderWidth = 1F;
            this.PageInfo.Font = new System.Drawing.Font("Arial", 9F);
            this.PageInfo.ForeColor = System.Drawing.Color.Black;
            this.PageInfo.Name = "PageInfo";
            //
            // DataField
            //
            this.DataField.BackColor = System.Drawing.Color.Transparent;
            this.DataField.BorderColor = System.Drawing.Color.Black;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1F;
            this.DataField.Font = new System.Drawing.Font("Arial", 10F);
            this.DataField.ForeColor = System.Drawing.Color.Black;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            //
            // XtraReport1
            //
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.pageFooterBand1,
            this.reportHeaderBand1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "Query";
            this.DataSource = this.sqlDataSource1;
            this.Landscape = true;
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
