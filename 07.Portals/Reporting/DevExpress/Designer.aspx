<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Designer.aspx.cs" Inherits="Clarity.Ecommerce.Service.Reporting.Designer" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<%--<link rel="stylesheet" href="Styles/DesignerStyle.css" />--%>
<script>
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
<style>
#ctl00_ctl00_MainPane_Content_MainContent_ASPxReportDesigner1 {
    width: 100% !important;
    height: 100vh !important;
}
</style>
<div>
    <dx:ASPxReportDesigner
        ID="ASPxReportDesigner1"
        runat="server"
        ColorScheme="Dark"
        ClientSideEvents-CustomizeMenuActions="CustomizeMenuActions">
    </dx:ASPxReportDesigner>
</div>
</asp:Content>
