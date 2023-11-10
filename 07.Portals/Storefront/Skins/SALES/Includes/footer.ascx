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

<footer id="footer"
  class="container-fluid bg-dark-blue text-light py-4 d-print-none text-white-50 translate-cloak" translate-cloak
  ng-controller="genericCtrl as genericCtrl">
  <div id="footerTop2" class="row">
    <div class="col-12">
      <div cef-brand-formatting-footer></div>
    </div>
  </div>
  <div id="footerTop" class="row">
  </div>
  <div id="footerMid" class="row">
    <div class="order-1 col-12 order-sm-1 col-sm-6 order-md-1 col-md-4 order-lg-1 col-lg-3 lg-ml-auto order-xl-1 col-xl-2 xl-ml-auto text-center">
      <a id="btnFooterLogo" class="mb-1" title="Logo"
        href="https://www.clarity-ventures.com/"
        rel="noopener" target="_blank">
        <img class="img-fluid lazyload"
          alt="Clarity eCommerce"
          width="225" height="49"
          data-src="/Portals/0/Clarity-eCommerce-Light.svg">
      </a>
      <p class="text-center small mt-2">
        The Clarity eCommerce platform takes you where the other stores can't go. It allows full
        customization of integrations, reporting, styling, tracking, inventory... the list goes on.
      </p>
    </div>
    <div class="order-2 col-7 order-sm-2 col-sm-6 order-md-5 col-md-4 order-lg-5 col-lg-3 lg-mr-auto order-xl-5 col-xl-auto xl-mr-auto mb-3">
      <h4 class="text-uppercase small text-light">Contact us</h4>
      <div class="row no-gutters align-items-center mb-3">
        <div class="col-auto">
          <div class="border border-light rounded-circle p-2">
            <i class="far fa-map-marker fa-fw fa-lg"></i>
          </div>
        </div>
        <div class="col pl-3" id="companyAddress">
          <address class="mb-0 small"><span class="address">6805 Capital of Texas Hwy<br/>Suite 312<br/>Austin, TX 78731</span></address>
        </div>
      </div>
      <div class="row no-gutters align-items-center mb-3">
        <div class="col-auto">
          <div class="border border-light rounded-circle p-2">
            <i class="far fa-phone-rotary fa-fw fa-lg"></i>
          </div>
        </div>
        <div class="col pl-3" id="companyPhone">
          <a class="text-white-50 small" href="tel:800-928-8160">800-928-8160</a>
        </div>
      </div>
      <div class="row no-gutters align-items-center mb-3">
        <div class="col-auto">
          <div class="border border-light rounded-circle p-2">
            <i class="far fa-at fa-fw fa-lg"></i>
          </div>
        </div>
        <div class="col pl-3" id="companyEmail">
          <a class="text-white-50 small" href="mailto:rfq@clarity-ventures.com">rfq@clarity-ventures.com</a>
        </div>
      </div>
    </div>
    <div class="order-3 col-5 xs-text-right order-sm-6 col-sm-6 order-md-6 col-md-auto order-lg-6 col-lg-auto lg-mr-auto order-xl-6 col-xl-6 xl-text-right col-tk-auto">
      <a href="https://www.facebook.com/ClarityTeam" title="Facebook"><i class="fab fa-facebook-square fa-3x text-white-50"></i></a>
      <a href="https://twitter.com/ClarityTeam" title="Twitter"><i class="fab fa-twitter-square fa-3x text-white-50"></i></a>
      <a href="https://www.linkedin.com/company/1155055" title="LinkedIn"><i class="fab fa-linkedin fa-3x text-white-50"></i></a>
    </div>
    <div class="order-4 col-6 order-sm-3 col-sm-6 order-md-2 col-md-auto order-lg-2 col-lg-auto order-xl-2 col-xl-auto">
      <h4 class="text-uppercase small text-light">Shopping Guide</h4>
      <ul class="links pl-3 small">
        <li><a class="text-white-50" ui-sref-plus uisrp-root="/faqs" title="FAQs">FAQs</a></li>
        <li><a class="text-white-50" ui-sref-plus uisrp-root="/payment" title="Payment">Payment</a></li>
        <li><a class="text-white-50" ui-sref-plus uisrp-root="/shipment" title="Shipment">Shipment</a></li>
        <li><a class="text-white-50" ui-sref-plus uisrp-root="/order" title="Where is my order?">Where is my order?</a></li>
        <li class="last"><a class="text-white-50" ui-sref-plus uisrp-root="/return" title="Return policy">Return policy</a></li>
      </ul>
    </div>
    <div class="order-5 col-6 order-sm-4 col-sm-6 order-md-3 col-md-auto order-lg-3 col-lg-auto order-xl-3 col-xl-auto">
      <h4 class="text-uppercase small text-light">Features</h4>
      <ul class="links pl-3 small">
        <li class="first Profile">
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.dashboard"
            translate-attr="{ title: 'ui.storefront.userDashboard2.controls.YourAccount',
                              'aria-label': 'ui.storefront.userDashboard2.controls.YourAccount' }">
            <span data-translate="ui.storefront.userDashboard2.controls.YourAccount"></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.wishList"
            translate-attr="{ title: 'ui.storefront.common.WishList',
                              'aria-label': 'ui.storefront.common.WishList' }">
            <span data-translate="ui.storefront.common.WishList"></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.favorites"
            translate-attr="{ title: 'ui.storefront.common.FavoritesList',
                              'aria-label': 'ui.storefront.common.FavoritesList' }">
            <span data-translate="ui.storefront.common.FavoritesList"></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.addressBook"
            translate-attr="{ title: 'ui.storefront.common.AddressBook',
                              'aria-label': 'ui.storefront.common.AddressBook' }">
            <span data-translate="ui.storefront.common.AddressBook"></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.notifyMeList"
            translate-attr="{ title: 'ui.storefront.menu.miniMenu.inStockAlerts',
                              'aria-label': 'ui.storefront.menu.miniMenu.inStockAlerts' }">
            <span data-translate="ui.storefront.menu.miniMenu.inStockAlerts"></span>
          </a>
        </li>
        <li class="last Orders">
          <a class="text-white-50" ui-sref-plus uisrp-is-dashboard="true"
            uisrp-state="userDashboard.salesOrders.list"
            translate-attr="{ title: 'ui.storefront.storeDashboard.storeOrderHistory.OrderHistory',
                              'aria-label': 'ui.storefront.storeDashboard.storeOrderHistory.OrderHistory' }">
            <span data-translate="ui.storefront.storeDashboard.storeOrderHistory.OrderHistory"></span>
          </a>
        </li>
      </ul>
    </div>
    <div class="order-6 col-6 order-sm-5 col-sm-6 order-md-4 col-md-auto order-lg-4 col-lg-auto order-xl-4 col-xl-auto">
      <h4 class="text-uppercase small text-light">Information</h4>
      <ul class="links pl-3 small">
        <li>
          <a class="text-white-50"
            ui-sref-plus uisrp-root="/Contact-Us"
            title="Contact Us">
            Contact Us
          </a>
        </li>
        <!-- <li><a class="text-white-50" ui-sref-plus uisrp-root="/Stores" title="Our stores" class="link-rss">Our Stores</a></li> -->
        <li>
          <a class="text-white-50"
            ui-sref-plus uisrp-root="/Terms"
            title="Terms Of Use">
            Terms Of Use
          </a>
        </li>
        <li>
          <a class="text-white-50"
            ui-sref-plus uisrp-root="/Privacy"
            title="Privacy Statement">
            Privacy Statement
          </a>
        </li>
      </ul>
    </div>
    <div class="order-7 col-12 xs-text-center order-sm-7 col-sm-12 sm-text-center order-md-7 col-md-auto order-lg-7 col-lg-auto lg-ml-auto order-xl-7 col-xl-6 col-tk-auto mr-tk-auto">
      <i class="fab fa-cc-paypal fa-3x"></i>
      <i class="fab fa-cc-visa fa-3x"></i>
      <i class="fab fa-cc-mastercard fa-3x"></i>
      <i class="fab fa-cc-amex fa-3x"></i>
      <i class="fab fa-cc-discover fa-3x"></i>
    </div>
  </div>
  <div id="footerBot" class="row mt-3">
    <div class="col-12 text-center small" id="copyright-entry">
      <dnn:COPYRIGHT ID="dnnCopyright" runat="server" />&nbsp;|&nbsp;<!--
      --><a class="text-white-50"
          href="https://www.clarity-ventures.com/"
          target="_blank" rel="noopener"
          >Enterprise eCommerce By Clarity</a>
    </div>
  </div>
</footer>

<% if (Request.Browser.Type.Contains("Firefox") || Request.Browser.Type.Contains("InternetExplorer")) { %>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/1-angular.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/Portals/_default/Skins/SALES/js/lazysizes.min.js"></script>
<script defer src="/Portals/_default/Skins/SALES/bootstrap/javascripts/bootstrap.bundle.min.js"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/4-cef-store-main.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/5-cef-store-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/API-Storefront/JSConfigs/StoreFront?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/6-cef-store-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous"></script>
<script src="/Portals/_default/Skins/SALES/js/custom.js"></script>
<script src="/Portals/_default/Skins/SALES/js/doubletaptogo.min.js"></script>
<script src="/Portals/_default/Skins/SALES/js/scripts.js"></script>
<% } else { %>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/1-angular.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/Portals/_default/Skins/SALES/js/lazysizes.min.js"></script>
<script defer src="/Portals/_default/Skins/SALES/bootstrap/javascripts/bootstrap.bundle.min.js"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/4-cef-store-main.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/5-cef-store-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/API-Storefront/JSConfigs/StoreFront?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/6-cef-store-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous"></script>
<script defer src="/Portals/_default/Skins/SALES/js/custom.js"></script>
<script defer src="/Portals/_default/Skins/SALES/js/doubletaptogo.min.js"></script>
<script defer src="/Portals/_default/Skins/SALES/js/scripts.js"></script>
<% } %>
<noscript>This website requires JavaScript to be enabled to operate.</noscript>
