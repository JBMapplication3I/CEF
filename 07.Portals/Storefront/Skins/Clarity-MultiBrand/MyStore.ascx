<%@ Control Language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header-my-store.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer-my-store.ascx" %>
<%@ Register TagPrefix="dnn" TagName="DNNLINK" Src="~/Admin/Skins/DnnLink.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />

<div class="siteWrapper">
    <dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" />
    <div class="container main full-height"
         ng-if="$root.globalBrandSiteDomain.Url">
        <div class="row">
            <div id="ContentPane" class="col-lg-12 contentPane" runat="server"></div>
        </div>
        <div class="row">
            <div class="col-12">
                <div ui-view="main"></div>
            </div>
        </div>
    </div>
    <dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />
