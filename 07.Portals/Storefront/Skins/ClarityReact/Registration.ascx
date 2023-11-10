<%@ Control language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />

<div class="siteWrapper">
  <dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" />
  <div id="body-with-min-height" class="w-100" style="min-height: 65vh;">
    <div class="container-fluid">
      <div class="row">
        <div id="FluidPane" runat="server" class="contentPane col-12"></div>
      </div>
    </div>
    <div class="container">
      <div class="row">
        <div id="ContentPane" runat="server" class="col-12"></div>
      </div>
      <div class="row">
        <div class="col-12 col-lg-8 mx-auto">
          <div class="mt-3" data-react-component="Registration"
            data-react-props='{"usernameIsEmail":"true","addressKey":"true"}'></div>
        </div>
      </div>
    </div>
    <div class="container-fluid">
      <div class="row">
        <div id="FluidPaneBottom" runat="server" class="contentPane col-12"></div>
      </div>
    </div>
  </div>
  <dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />
</div>
