<%@ Control language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer.ascx" %>

<dnn:DnnJsInclude ID="bootstrapJS" runat="server" FilePath="bootstrap/js/bootstrap.min.js" PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="customJS" runat="server" FilePath="js/scripts.js" PathNameAlias="SkinPath" AddTag="false" />

<div id="siteWrapper">

<dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" /> 

    <div id="contentWrapper">
        <div class="container">
            <div id="content">
                <div class="row">
                    <div id="ContentPane" class="contentPane" runat="server">
                </div>
                <div class="row">
                    <div id="leftPane" class="col-md-8 leftPane spacingTop" runat="server">
                    <div id="sidebarPane" class="col-md-4 sidebarPane spacingTop" runat="server">
                </div>
                <div class="row">
                    <div id="contentPaneLower" class="col-md-12 contentPane spacingTop" runat="server">
                </div>
            </div>
        </div>
    </div>

<dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />

</div>

<dnn:DnnJsInclude ID="dttg" runat="server" FilePath="js/doubletaptogo.min.js" PathNameAlias="SkinPath" AddTag="false" />
<script>
$(function () { $("#navdttg li:has(ul)").doubleTapToGo(); });
</script>
