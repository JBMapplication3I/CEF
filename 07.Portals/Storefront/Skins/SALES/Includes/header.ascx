<%@ Control Language="c#" AutoEventWireup="false" Explicit="True" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="DotNetNuke.Entities.Host" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="BREADCRUMB" Src="~/Admin/Skins/BreadCrumb.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>

<% if (Request.IsAuthenticated) { %>
<asp:HiddenField ID="hdnCurrentUsername" runat="server" />
<asp:HiddenField ID="hdnCurrentToken" runat="server" />
<script type="C#" runat="server">
  private const string PasswordHash = "qpzm9731";
  private const string SaltKey = "Cl4r1tyV3ntur3s";
  private const string VIKey = "46g7HeDF@52c831B";

  private static string Encrypt(string plainText, bool isUrlEncoded = false) {
    var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
    var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
    var symmetricKey = new RijndaelManaged {
      Mode = CipherMode.CBC,
      Padding = PaddingMode.Zeros
    };
    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
    byte[]cipherTextBytes;
    using(var memoryStream = new MemoryStream()) {
      using(var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        cipherTextBytes = memoryStream.ToArray();
      }
    }
    var output = Convert.ToBase64String(cipherTextBytes);
    if (isUrlEncoded) {
      output = HttpUtility.UrlEncode(output);
    }
    return output;
  }

  private static IEnumerable < byte > GetBytes(string str) {
    var bytes = new byte[str.Length * sizeof(char)];
    Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
  }

  private readonly string token = UserController.Instance == null
      || UserController.Instance.GetCurrentUserInfo() == null
      || UserController.Instance.GetCurrentUserInfo().Username == null
      ? string.Empty
      : Encrypt(
          Convert.ToBase64String(
              BitConverter.GetBytes(DateTime.UtcNow.ToBinary())
                  .Concat(GetBytes("|"))
                  .Concat(Guid.NewGuid().ToByteArray())
                  .Concat(GetBytes("|"))
                  .Concat(GetBytes(UserController.Instance.GetCurrentUserInfo().Username)).ToArray()));
</script>
<script>
$("#<%=hdnCurrentUsername.ClientID %>").attr("value", "<%=UserController.Instance.GetCurrentUserInfo().Username%>");
$("#<%=hdnCurrentToken.ClientID %>").attr("value", "<%=token%>");
</script>
<% } %>

<script runat="server" language="C#">
  public string SkinPath { get; set; }
  public string BuildNumber = Host.CrmVersion.ToString(CultureInfo.InvariantCulture);
</script>
<style>
.imgLogo { aspect-ratio: 400 / 70; }
/* Hide translations before they load */
.translate-cloak { display: none !important; }
</style>
<% if (!Request.IsAuthenticated) { %>
<script src="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/0-jQuery.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<% } %>
<% if (Request.Browser.Type.Contains("Firefox") || Request.Browser.Type.Contains("InternetExplorer")) { %>
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.common-bootstrap.min.css" />
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.bootstrap.min.css" />
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/SALES/css/clarity.css?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" />
<link rel="stylesheet" type="text/css" href="/Resources/Shared/stylesheets/dnndefault/7.0.0/default.min.css" />
<%     if (Request.Browser.Type.Contains("InternetExplorer")) { %>
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/SALES/css/ie11.shiv.css" />
<%     } %>
<% } else { %>
<link rel="preload" type="text/css" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.common-bootstrap.min.css" as="style" onload="this.onload=null;this.rel='stylesheet'" />
<noscript><link rel="stylesheet" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.common-bootstrap.min.css" /></noscript>

<link rel="preload" type="text/css" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.bootstrap.min.css" as="style" onload="this.onload=null;this.rel='stylesheet'" />
<noscript><link rel="stylesheet" href="/Portals/_default/Skins/SALES/css/thirdparty/kendo.bootstrap.min.css" /></noscript>

<link rel="preload" type="text/css" href="/Portals/_default/Skins/SALES/css/clarity.css?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="style" onload="this.onload=null;this.rel='stylesheet'" />
<noscript><link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/SALES/css/clarity.css?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" /></noscript>

<link rel="preload" type="text/css" href="/Resources/Shared/stylesheets/dnndefault/7.0.0/default.min.css" as="style" onload="this.onload=null;this.rel='stylesheet'">
<noscript><link rel="stylesheet" type="text/css" href="/Resources/Shared/stylesheets/dnndefault/7.0.0/default.min.css" /></noscript>

<%@ Register TagPrefix="fortyfingers" TagName="STYLEHELPER" Src="~/DesktopModules/40Fingers/SkinObjects/StyleHelper/StyleHelper.ascx" %>
<dnn:DnnJsInclude ID="DNNJSIncludeJqueryHoverIntent" runat="server" FilePath="~/Resources/Shared/scripts/jquery/jquery.hoverIntent.min.js" />

<link rel="preload" href="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/1-angular.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="/Portals/_default/Skins/SALES/js/lazysizes.min.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/SALES/bootstrap/javascripts/bootstrap.bundle.min.js" as="script" />
<link rel="preload" href="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/4-cef-store-main.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/5-cef-store-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="/DesktopModules/ClarityEcommerce/API-Storefront/JSConfigs/StoreFront?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="/DesktopModules/ClarityEcommerce/UI-Storefront/lib/cef/js/6-cef-store-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>" as="script" />
<link rel="preload" href="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous" as="script" />
<link rel="preload" href="/Portals/_default/Skins/SALES/js/custom.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/SALES/js/doubletaptogo.min.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/SALES/js/scripts.js" as="script" />
<% } %>

<fortyfingers:STYLEHELPER ID="STYLEHELPER2" RemoveCssFile="skin.css" runat="server" />
<fortyfingers:STYLEHELPER ID="STYLEHELPERBODYCLASS" AddBodyClass="True" runat="server" />

<% if (!Request.Browser.Type.Contains("Firefox")) { %>
<link rel="apple-touch-icon" sizes="57x57" href="/favicon/apple-touch-icon-57x57.png" />
<link rel="apple-touch-icon" sizes="60x60" href="/favicon/apple-touch-icon-60x60.png" />
<link rel="apple-touch-icon" sizes="72x72" href="/favicon/apple-touch-icon-72x72.png" />
<link rel="icon" type="image/png" href="/favicon/favicon-32x32.png" sizes="32x32" />
<link rel="icon" type="image/png" href="/favicon/favicon-16x16.png" sizes="16x16" />
<link rel="manifest" href="/favicon/manifest.json" />
<% } %>

<meta name="msapplication-TileColor" content="#ffffff" />
<meta name="theme-color" content="#ffffff" />

<!-- Browser detection for easy IE fixes -->
<!--[if lt IE 7]><div class="ie6"><![endif]-->
<!--[if IE 7]><div class="ie7"><![endif]-->
<!--[if IE 8]><div class="ie8"><![endif]-->
<!--[if IE 9]><div class="ie9"><![endif]-->
<!--[if gt IE 9]><div><![endif]-->
<!--[if !IE]><!--><div id="browser-detection" class="full-height"><!--<![endif]-->

<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->
<header id="header" class="container-fluid d-print-none" ng-controller="genericCtrl as genericCtrl">
  <div id="headerTop" class="row"></div>
  <div id="headerMid" class="row bg-light align-items-center"
    ng-style="{ 'min-height': genericCtrl.cefConfig.featureSet.languages.enabled ? 0 : 70 }">
    <div class="col-12 xs-text-center col-sm-12 sm-text-center col-md-auto col-lg-auto col-xl-auto">
      <a class="navbar-brand"
        title="Clarity eCommerce Development Website"
        ui-sref-plus uisrp-is-home="true">
        <img class="img-fluid lazyload"
          alt="Clarity eCommerce Development Website"
          width="225" height="49"
          data-src="/Portals/0/Clarity-eCommerce-Dark.svg"
        />
      </a>
      <div cef-brand-formatting-header></div>
    </div>
    <div class="col form-inline"
      cef-search-catalog-external-search-box>
    </div>
    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-auto"
      ng-style="{ 'min-height': genericCtrl.cefConfig.featureSet.languages.enabled ? 70 : 0 }">
      <div class="row align-items-center"
        ng-class="{ 'mt-2': genericCtrl.cefConfig.featureSet.languages.enabled }">
        <div ng-if="genericCtrl.cefConfig.featureSet.salesQuotes.enabled"
          class="nav-item col-auto"
          cef-micro-cart type="'Quote Cart'">
        </div>
        <div ng-if="genericCtrl.cefConfig.featureSet.carts.enabled"
          class="nav-item col-auto"
          cef-micro-cart type="'Cart'">
        </div>
      </div>
      <div class="row align-items-center mb-2">
        <!--
        <div class="nav-item col">
           <div cef-affiliate-account-selector></div>
        </div>
        -->
        <div class="nav-item col-auto"
          cef-language-selector-button>
        </div>
        <div ng-if="genericCtrl.cefConfig.featureSet.login.enabled"
          class="nav-item col-auto xs-ml-auto sm-ml-auto md-ml-auto lg-ml-auto"
          cef-mini-menu>
        </div>
      </div>
    </div>
  </div>
  <div id="headerBot" class="row">
    <div class="col-12 bg-dark-blue">
      <div class="container">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark-blue row md-py-0 lg-py-0 xl-py-0 tk-py-0 fk-py-0">
          <button type="button" class="navbar-toggler"
            data-toggle="collapse"
            data-target="#headerBotContent"
            aria-controls="headerBotContent"
            aria-expanded="false"
            translate-attr="{ 'aria-label': 'ui.storefront.userDashboard.ToggleNavigation' }">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div id="headerBotContent"
            class="collapse navbar-collapse">
            <div class="navbar-nav">
              <dnn:MENU ID="MENUVerticalTabsNav" MenuStyle="VerticalTabsNav" runat="server"></dnn:MENU>
            </div>
          </div>
        </nav>
      </div>
    </div>
  </div>
</header>
