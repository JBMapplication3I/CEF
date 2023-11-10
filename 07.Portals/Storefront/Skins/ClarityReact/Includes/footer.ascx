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
  class="container-fluid bg-dark-blue text-light py-4 d-print-none text-white-50">
  <div id="footerTop2" class="row">
    <div class="col-12">
      <div data-react-component="BrandFormattingFooter"></div>
    </div>
  </div>
  <div id="footerTop" class="row">
  </div>
  <div id="footerMid" class="row">
    <div class="order-1 col-12 order-sm-1 col-sm-6 order-md-1 col-md-4 order-lg-1 col-lg-3 lg-ml-auto order-xl-1 col-xl-2 xl-ml-auto tk-ml-0">
      <a id="btnFooterLogo" class="mb-1" title="Logo"
        href="https://www.clarity-ventures.com/"
        rel="noopener" target="_blank">
        <img class="img-fluid lazyload" alt="Clarity Ventures Website"
          width="225" height="49"
          data-src="/Portals/0/Clarity-eCommerce-light.svg" />
      </a>
      <p class="text-center small mt-2">
        The Clarity eCommerce platform takes you where the other stores can't go. It allows full
        customization of integrations, reporting, styling, tracking, inventory... the list goes on.
      </p>
    </div>
    <div class="order-2 col-7 order-sm-2 col-sm-6 order-md-5 col-md-4 order-lg-5 col-lg-3 lg-mr-auto order-xl-5 col-xl-auto xl-mr-auto mb-3 tk-mr-0">
      <h4 class="text-uppercase small text-light">Contact us</h4>
      <div class="no-gutters mx-0 align-items-center mb-3">
        <div class="d-inline-flex align-items-center">
          <div class="border border-light rounded-circle p-2">
            <div data-react-component="Icon"
            data-react-props='{"icon":"faMapMarker", "type":"solid", "classes":"fa-fw fa-lg"}'></div>
          </div>
          <div class="pl-3 pr-0" id="companyAddress">
            <address class="mb-0 small"><span class="address">6805 Capital of Texas Hwy<br/>Suite 312<br/>Austin, TX 78731</span></address>
          </div>
        </div>
        
      </div>
      <div class="no-gutters mx-0 align-items-center mb-3">
        <div class="d-inline-flex align-items-center px-0">
          <div class="border border-light rounded-circle p-2">
            <div data-react-component="Icon"
              data-react-props='{"icon":"faPhoneSquare", "type":"solid", "classes":"fa-fw fa-lg"}'></div>
          </div>
          <div class="pl-3 pr-0" id="companyPhone">
            <a class="text-white-50 small" href="tel:800-928-8160">800-928-8160</a>
          </div>
        </div>
        
      </div>
      <div class="no-gutters mx-0 align-items-center mb-3">
        <div class="d-inline-flex align-items-center px-0">
          <div class="border border-light rounded-circle p-2">
            <div data-react-component="Icon"
              data-react-props='{"icon":"faAt", "type":"solid", "classes":"fa-fw fa-lg"}'></div>
          </div>
          <div class="pl-3 pr-0" id="companyEmail">
            <a class="text-white-50 small" href="mailto:rfq@clarity-ventures.com">rfq@clarity-ventures.com</a>
          </div>
        </div>
      </div>
    </div>
    <div class="order-3 col-5 xs-text-right order-sm-6 col-sm-6 order-md-6 col-md-auto order-lg-6 col-lg-auto lg-mr-auto order-xl-6 col-xl-6 xl-text-right col-tk-auto xl-mr-0">
      <a href="https://www.facebook.com/ClarityTeam" title="Facebook">
        <div class="d-inline-block"
          data-react-component="Icon"
          data-react-props='{"icon":"faFacebookSquare", "type":"brands", "classes":"fa-3x text-white-50"}'></div>
      </a>
      <a href="https://twitter.com/ClarityTeam" title="Twitter">
        <div class="d-inline-block"
          data-react-component="Icon"
          data-react-props='{"icon":"faTwitterSquare", "type":"brands", "classes":"fa-3x text-white-50"}'></div>
      </a>
      <a href="https://www.linkedin.com/company/1155055" title="LinkedIn">
        <div class="d-inline-block"
          data-react-component="Icon"
          data-react-props='{"icon":"faLinkedin", "type":"brands", "classes":"fa-3x text-white-50"}'></div>
      </a>
    </div>
    <div class="order-4 col-6 order-sm-3 col-sm-6 order-md-2 col-md-auto order-lg-2 col-lg-auto order-xl-2 col-xl-auto">
      <h4 class="text-uppercase small text-light">Shopping Guide</h4>
      <ul class="links pl-3 small" style="margin-left: 18px;">
        <li><a class="text-white-50" title="FAQs" href="/faqs">FAQs</a></li>
        <li><a class="text-white-50" title="Payment" href="/payment">Payment</a></li>
        <li><a class="text-white-50" title="Shipment" href="/shipment">Shipment</a></li>
        <li><a class="text-white-50" title="Where is my order?" href="/order">Where is my order?</a></li>
        <li class="last"><a class="text-white-50" title="Return policy" href="/return">Return policy</a></li>
      </ul>
    </div>
    <div class="order-5 col-6 order-sm-4 col-sm-6 order-md-3 col-md-auto order-lg-3 col-lg-auto order-xl-3 col-xl-auto">
      <h4 class="text-uppercase small text-light"
        data-react-component="Translate"
        data-react-props='{"translateKey":"ui.storefront.common.Features", "suspenseMessage":"...Loading"}'
        ></h4>
      <ul class="links pl-3 small" style="margin-left: 18px;">
        <li class="first Profile">
          <a class="text-white-50" href="/Dashboard/Account-Profile"
            translate-attr="{ title: 'ui.storefront.userDashboard2.controls.YourAccount',
                              'aria-label': 'ui.storefront.userDashboard2.controls.YourAccount' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.userDashboard2.controls.YourAccount", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" href="/Dashboard/Wishlist"
            translate-attr="{ title: 'ui.storefront.common.WishList',
                              'aria-label': 'ui.storefront.common.Wish-List' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.common.WishList", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" href="/Dashboard/FavoritesList"
            translate-attr="{ title: 'ui.storefront.common.FavoritesList',
                              'aria-label': 'ui.storefront.common.FavoritesList' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.common.FavoritesList", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" href="/Dashboard/Address-Book"
            translate-attr="{ title: 'ui.storefront.common.AddressBook',
                              'aria-label': 'ui.storefront.common.AddressBook' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.common.AddressBook", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
        <li>
          <a class="text-white-50" href="/Dashboard/In-Stock-Alerts"
            translate-attr="{ title: 'ui.storefront.menu.miniMenu.inStockAlerts',
                              'aria-label': 'ui.storefront.menu.miniMenu.inStockAlerts' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.menu.miniMenu.inStockAlerts", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
        <li class="last Orders">
          <a class="text-white-50" href="/Dashboard/Orders"
            translate-attr="{ title: 'ui.storefront.storeDashboard.storeOrderHistory.OrderHistory',
                              'aria-label': 'ui.storefront.storeDashboard.storeOrderHistory.OrderHistory' }">
            <span data-react-component="Translate"
              data-react-props='{"translateKey":"ui.storefront.storeDashboard.storeOrderHistory.OrderHistory", "suspenseMessage":"...Loading"}'></span>
          </a>
        </li>
      </ul>
    </div>
    <div class="order-6 col-6 order-sm-5 col-sm-6 order-md-4 col-md-auto order-lg-4 col-lg-auto order-xl-4 col-xl-auto">
      <h4 class="text-uppercase small text-light">Information</h4>
      <ul class="links pl-3 small" style="margin-left: 18px;">
        <li>
          <a class="text-white-50"
            href="/Contact-Us"
            title="Contact Us">
            Contact Us
          </a>
        </li>
        <!-- <li><a class="text-white-50" ui-sref-plus uisrp-root="/Stores" title="Our stores" class="link-rss">Our Stores</a></li> -->
        <li><dnn:TERMS runat="server" /></li>
        <li><dnn:PRIVACY runat="server" /></li>
      </ul>
    </div>
    <div class="order-7 col-12 xs-text-center order-sm-7 col-sm-12 sm-text-center order-md-7 col-md-auto order-lg-7 col-lg-auto lg-ml-auto order-xl-7 col-xl-6 col-tk-auto mr-tk-auto xl-ml-0">
      <div class="d-inline-block"
        data-react-component="Icon"
        data-react-props='{"icon":"faCcPaypal", "type":"brands", "classes":"fa-3x"}'></div>
      <div class="d-inline-block"
        data-react-component="Icon"
        data-react-props='{"icon":"faCcVisa", "type":"brands", "classes":"fa-3x"}'></div>
      <div class="d-inline-block"
        data-react-component="Icon"
        data-react-props='{"icon":"faCcMastercard", "type":"brands", "classes":"fa-3x"}'></div>
      <div class="d-inline-block"
        data-react-component="Icon"
        data-react-props='{"icon":"faCcAmex", "type":"brands", "classes":"fa-3x"}'></div>
      <div class="d-inline-block"
        data-react-component="Icon"
        data-react-props='{"icon":"faCcDiscover", "type":"brands", "classes":"fa-3x"}'></div>
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
<dnn:DnnJsInclude ID="DNNJSIncludeStoreVendors" runat="server" FilePath="~/DesktopModules/ClarityEcommerce/Shop/1-cef-store-vendors.js" />
<dnn:DnnJsInclude ID="DNNJSIncludeStoreMain"    runat="server" FilePath="~/DesktopModules/ClarityEcommerce/Shop/0-cef-store-main.js" />

<% if (Request.Browser.Type.Contains("Firefox") || Request.Browser.Type.Contains("InternetExplorer")) { %>
<script defer src="/Portals/_default/Skins/ClarityReact/js/lazysizes.min.js"></script>
<script src="/Portals/_default/Skins/ClarityReact/js/custom.js"></script>
<script src="/Portals/_default/Skins/ClarityReact/js/doubletaptogo.min.js"></script>
<script src="/Portals/_default/Skins/ClarityReact/js/scripts.js"></script>
<% } else { %>
<script defer src="/Portals/_default/Skins/ClarityReact/js/lazysizes.min.js"></script>
<script defer src="/Portals/_default/Skins/ClarityReact/js/custom.js"></script>
<script defer src="/Portals/_default/Skins/ClarityReact/js/doubletaptogo.min.js"></script>
<script defer src="/Portals/_default/Skins/ClarityReact/js/scripts.js"></script>
<% } %>
<noscript>This website requires JavaScript to operate.</noscript>
