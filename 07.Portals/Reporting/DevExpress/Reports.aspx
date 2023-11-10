<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Reports.aspx.cs" Inherits="Clarity.Ecommerce.Service.Reporting.Reports" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%--<%@ Register Namespace="DevExpress.Web.ASPxScheduler" Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" TagPrefix="dx"  %>--%>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server" CssClass="container-fluid">
<%--<link rel="stylesheet" href="Styles/DesignerStyle.css" />--%>
<script>
    // The CustomizeMenuActions event handler.
    function CustomizeMenuActions(s, e) {
        var actions = e.Actions;
        // Register the custom Close menu command.
        actions.push({
            text: "Close",
            imageClassName: "customButton",
            // ReSharper disable once UseOfImplicitGlobalInFunctionScope
            disabled: ko.observable(false),
            visible: true,
            // The clickAction function recieves the client-side report model
            // allowing you interact with the currently opened report.
            clickAction: function (/*report*/) {
                window.location = "Default.aspx";
            },
            container: "menu"
        });
    }
</script>
<div class="form-inline" style="background: #e5e5e5;">
    <label class="control-label">Reporting Loader</label>
    <div class="form-group">
        <div class="input-group">
            <div class="input-group-addon">Please choose a Report:</div>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" />
        </div>
    </div>
</div>
<div class="row">
   <dx:ASPxWebDocumentViewer ID="WebDocumentViewer1" runat="server" nope-ReportSourceId="VendorSalesReport" CssClass="col-md-12"
        ClientSideEvents-CustomizeMenuActions="CustomizeMenuActions" />
</div>
</asp:Content>
