<%@ Control Language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer.ascx" %>
<%@ Register TagPrefix="dnn" TagName="DNNLINK" Src="~/Admin/Skins/DnnLink.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="cef" TagName="ProductMetaService" Src="~/DesktopModules/ClarityEcommerce/MetaService/ProductMetaService.ascx" %>
<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />
<cef:ProductMetaService id="cefProductMetaService" runat="server" />
<script defer src="https://platform-api.sharethis.com/js/sharethis.js#property=5e5e9e68873c9500198e2e95&product=inline-share-buttons&cms=sop" async="async"></script>

<link rel="stylesheet" ng-href="{{'/js/magiczoomplus.css' | corsLink: 'site'}}" />
<link rel="stylesheet" ng-href="{{'/js/magicscroll.css' | corsLink: 'site'}}" />
<!-- <script ng-href="{{'/js/magiczoomplus.js' | corsLink: 'site'}}"></script> -->
<!-- <script ng-href="{{'/js/magicscroll.js' | corsLink: 'site'}}"></script> -->

<div class="siteWrapper">
    <dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" />
        <div id="body-with-min-height" class="w-100" style="min-height: 65vh;"
         ng-if="$root.globalBrandSiteDomain.Url">
            <div class="container-fluid">
                <div class="row">
                    <div id="FluidPane" runat="server" class="contentPane col-12"></div>
                </div>
            </div>
            <div class="container-xl main">
                <div class="row">
                    <div class="col-12"
                         cef-product-details>
                    </div>
                </div>
                <div class="row">
                    <div id="ContentPane" runat="server" class="contentPane col-12"></div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row">
                    <div id="FluidPaneBottom" runat="server" class="contentPane col-12"></div>
                </div>
            </div>
        </div>
    </div>
    <dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />
</div>
