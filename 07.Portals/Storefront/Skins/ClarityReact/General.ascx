<%@ Control language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="HEADER" Src="includes/header.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FOOTER" Src="includes/footer.ascx" %>

<div class="siteWrapper">
  <dnn:HEADER ID="uxHeader" runat="server" SkinPath="<%# SkinPath %>" />
  <!-- Single Fluid Container Row -->
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12">
        <div id="FluidPane" class="contentPane" runat="server"></div>
      </div>
    </div>
  </div>
  <div class="container">
    <!-- Single Container Row -->
    <div class="row">
      <div class="col-md-12">
        <div id="ContentPane" class="contentPane" runat="server"></div>
      </div>
    </div>
    <!-- Left Column -->
    <div class="row">
      <div class="col-md-3">
        <div id="LeftColumnPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-9">
        <div id="RightMainPane" class="contentPane" runat="server"></div>
      </div>
    </div>
    <!-- Right Column -->
    <div class="row">
      <div class="col-md-3 col-md-push-9">
        <div id="RightColumnPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-9 col-md-pull-3">
        <div id="LeftMainPane" class="contentPane" runat="server"></div>
      </div>
    </div>
    <!-- Two Column -->
    <div class="row">
      <div class="col-md-6">
        <div id="LeftHalfPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-6">
        <div id="RightHalfPane" class="contentPane" runat="server"></div>
      </div>
    </div>
    <!-- Three Column -->
    <div class="row">
      <div class="col-md-4">
        <div id="LeftThirdPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-4">
        <div id="MiddleThirdPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-4">
        <div id="RightThirdPane" class="contentPane" runat="server"></div>
      </div>
    </div>
    <!-- Four Column -->
    <div class="row">
      <div class="col-md-4">
        <div id="LeftQuarterPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-4">
        <div id="LeftMiddleQuarterPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-4">
        <div id="RightMiddleQuarterPane" class="contentPane" runat="server"></div>
      </div>
      <div class="col-md-4">
        <div id="RightQuarterPane" class="contentPane" runat="server"></div>
      </div>
    </div>
  </div>
  <dnn:FOOTER ID="uxFooter" runat="server" SkinPath="<%# SkinPath %>" />
</div>
