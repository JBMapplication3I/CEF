<%@ Control language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer.ascx" %>

<div class="siteWrapper">
  <dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" />
  <div id="body-with-min-height" class="w-100" style="min-height: 65vh;">
    <div class="container-fluid p-0">
      <div class="row no-gutters">
        <div id="FluidPane" runat="server" class="contentPane col-12"></div>
      </div>
    </div>
    <div class="container">
      <div class="row">
        <div id="ContentPane" runat="server" class="col-12"></div>
      </div>
    </div>
    <div class="main-section">
        <div class="container">
          <div class="text-center">
            <div cef-featured-products category-key="FeaturedProductHomepage"></div>
          </div>
        </div>
      </div>
      <div class="main-section">
        <div class="image-decor">
          <img src="https://api.jandbmedical.com/images/ecommerce/homepageCatPattern.png"/>
        </div>
        <div class="container">
          <div class="text-center">
            <div id="About_Us">&nbsp;</div>
            <div cef-popular-categories></div>
          </div>
        </div>
      </div>
    <div class="container-fluid p-0"  id="about_us" style="scroll-margin-top: 9rem">
      <div class="row">
        <div id="FluidPaneBottom" runat="server" class="contentPane col-12"></div>
      </div>
    </div>
  </div>
  <dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />
</div>
