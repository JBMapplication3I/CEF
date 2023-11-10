<%@ Control language="c#" AutoEventWireup="false" Explicit="True" %>
<%@ Import Namespace="DotNetNuke.Entities.Host" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="DNNLINK" Src="~/Admin/Skins/DnnLink.ascx" %>

<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>

<script runat="server" language="C#">
  public string BuildNumber = Host.CrmVersion.ToString(CultureInfo.InvariantCulture);
</script>

<footer
  id="footer"
  translate-cloak
  ng-controller="genericCtrl as genericCtrl">
  <div class="container">
    <div class="row align-items-center">
      <div class="col-md-6 text-center text-md-left">
        <div class="logo-footer">
          <a href="/">
            <img
              src="https://api-jbm-test.jandbmedical.com/images/ecommerce/logo-secondary.png"
              alt="J&amp;B medical ems"
              width="102"
              height="30"
            />
          </a>
        </div>
      </div>
      <div class="col-md-6 text-center text-md-right">
        <ul class="social-list justify-content-center justify-content-md-end">
          <li>
            <a href="https://www.facebook.com/jandbathome/" target="_blank" rel="noopener noreferrer nofollow"
              ><span class="sr-only">facebook link</span
              ><i class="fa fa-facebook-square"></i
            ></a>
          </li>
          <li>
            <a href="https://twitter.com/jandbmedical?lang=en" target="_blank" rel="noopener noreferrer nofollow"
              ><span class="sr-only">twitter link</span
              ><i class="fa fa-twitter-square"></i
            ></a>
          </li>
        </ul>
      </div>
    </div>
    <hr />
    <div class="row footer-holder">
      <div class="col-lg-6 text-center text-lg-left">
        <ul
          class="footer-navigation justify-content-center justify-content-lg-start"
        >
          <li><a ui-sref-plus uisrp-root="/About">About Us</a></li>
          <li><a ui-sref-plus uisrp-root="/Contact-Us">Contact Us</a></li>
          <li><a ui-sref-plus uisrp-root="/Documentation">Documentation</a></li>
          <li>
            <a 
              ui-sref-plus 
              uisrp-is-dashboard="true" 
              uisrp-state="userDashboard.dashboard">
              My Account
            </a>
          </li>
          <li><a ui-sref-plus uisrp-root="/Cart">My Cart</a></li>
        </ul>
      </div>
      <div
        class="col-lg-6 d-flex flex-wrap justify-content-lg-end justify-content-center text-lg-right"
      >
        <span class="copy">Site and all content © 2022 J&B Medical</span>
        <ul class="footer-navigation justify-content-center text-lg-right">
          <li><a ui-sref-plus uisrp-root="/Privacy">Privacy Policy</a></li>
          <li><a ui-sref-plus uisrp-root="/Terms">Terms & Conditions</a></li>
        </ul>
      </div>
    </div>
  </div>
</footer>

<% if (Request.Browser.Type.Contains("Firefox") || Request.Browser.Type.Contains("InternetExplorer")) { %>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/1-angular.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/Portals/_default/Skins/JBM/js/lazysizes.min.js"></script>
<script defer src="/Portals/_default/Skins/JBM/bootstrap/javascripts/bootstrap.bundle.min.js"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/4-cef-store-main.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/5-cef-store-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/API-Storefront/JSConfigs/StoreFront?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/6-cef-store-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous"></script>
<script src="/Portals/_default/Skins/JBM/js/custom.js"></script>
<script src="/Portals/_default/Skins/JBM/js/doubletaptogo.min.js"></script>
<script src="/Portals/_default/Skins/JBM/js/scripts.js"></script>
<% } else { %>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/1-angular.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/Portals/_default/Skins/JBM/js/lazysizes.min.js"></script>
<script defer src="/Portals/_default/Skins/JBM/bootstrap/javascripts/bootstrap.bundle.min.js"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/4-cef-store-main.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/5-cef-store-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/API-Storefront/JSConfigs/StoreFront?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api-jbm-test.jandbmedical.com/UI-Storefront/lib/cef/js/6-cef-store-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous"></script>
<script defer src="/Portals/_default/Skins/JBM/js/custom.js"></script>
<script defer src="/Portals/_default/Skins/JBM/js/doubletaptogo.min.js"></script>
<script defer src="/Portals/_default/Skins/JBM/js/scripts.js"></script>
<% } %>
<noscript>This website requires JavaScript to be enabled to operate.</noscript>
