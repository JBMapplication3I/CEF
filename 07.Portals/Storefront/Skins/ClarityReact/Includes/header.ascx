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

<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
<dnn:DnnCssInclude ID="DNNCSSIncludeClarity" runat="server" FilePath="~/DesktopModules/ClarityEcommerce/Shop/static/css/clarity.css" />

<style>
  .imgLogo { aspect-ratio: 400 / 70; }
  /* Hide translations before they load */
  .translate-cloak { display: none !important; }
</style>
<% if (!Request.IsAuthenticated) { %>

<% } %>
<% if (Request.Browser.Type.Contains("Firefox") || Request.Browser.Type.Contains("InternetExplorer")) { %>
<%     if (Request.Browser.Type.Contains("InternetExplorer")) { %>

<%     } %>
<% } else { %>

<%@ Register TagPrefix="fortyfingers" TagName="STYLEHELPER" Src="~/DesktopModules/40Fingers/SkinObjects/StyleHelper/StyleHelper.ascx" %>
<dnn:DnnJsInclude ID="DNNJSIncludeJqueryHoverIntent" runat="server" FilePath="~/Resources/Shared/scripts/jquery/jquery.hoverIntent.min.js" />

<link rel="preload" href="/Portals/_default/Skins/ClarityReact/js/lazysizes.min.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/ClarityReact/js/custom.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/ClarityReact/js/doubletaptogo.min.js" as="script" />
<link rel="preload" href="/Portals/_default/Skins/ClarityReact/js/scripts.js" as="script" />
<% } %>

<fortyfingers:STYLEHELPER ID="STYLEHELPER2" RemoveCssFile="skin.css" runat="server" />
<fortyfingers:STYLEHELPER ID="STYLEHELPERBODYCLASS" AddBodyClass="True" runat="server" />
<!-- Removes dnn "default.css" unless administrator is logged in -->
<fortyfingers:STYLEHELPER ID="STYLEHELPER3" RemoveCssFile="default.css" runat="server" />
<% if (Request.IsAuthenticated) { %>
  <link rel="stylesheet" href="/Resources/Shared/stylesheets/dnndefault/7.0.0/default.css" />
<% } %>


<link rel="apple-touch-icon" sizes="57x57" href="/favicon/apple-touch-icon-57x57.png" />
<link rel="apple-touch-icon" sizes="60x60" href="/favicon/apple-touch-icon-60x60.png" />
<link rel="apple-touch-icon" sizes="72x72" href="/favicon/apple-touch-icon-72x72.png" />
<link rel="icon" type="image/png" href="/favicon/favicon-32x32.png" sizes="32x32" />
<link rel="icon" type="image/png" href="/favicon/favicon-16x16.png" sizes="16x16" />
<link rel="manifest" href="/favicon/manifest.json" />

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
<header id="header" class="container-fluid d-print-none">
  <div id="headerTop" class="row"></div>
  <div id="headerMid" class="row bg-light align-items-center"
    style="min-height: 70px">
    <div class="col-12 xs-text-center col-sm-12 sm-text-center col-md-auto col-lg-auto col-xl-auto">
      <a class="navbar-brand" href="/"
        title="Clarity eCommerce Development Website">
        <img class="img-fluid lazyload"
          alt="Clarity eCommerce Development Website"
          width="225" height="49"
          data-src="/Portals/0/Clarity-eCommerce-Dark.svg" />
      </a>
      <div data-react-component="cefBrandFormattingHeader"></div>
    </div>
    <div class="col d-inline-flex w-auto"
      data-react-component="ExternalSearchBox"
      data-react-props='{"hideSuspense":true}'></div>
    <div class="col-12 col-xl-auto" style="min-height:0">
      <!-- ng-style="{ 'min-height': genericCtrl.cefConfig.featureSet.languages.enabled ? 70 : 0 }" -->
      <div class="row align-items-center">
      <!-- ng-class="{ 'mt-2': genericCtrl.cefConfig.featureSet.languages.enabled }" -->
        <div class="col-auto nav-item"
          id="react-microQuoteCart"
          data-react-component="MicroCart"
          data-react-props='{"type":"Quote"}'></div>
        <div class="col-auto nav-item"
          id="react-microCart"
          data-react-component="MicroCart"
          data-react-props='{"type":"Cart"}'></div>
        <div class="nav-item col-auto xs-ml-auto dropdown"
          data-react-component="MiniMenu"></div>
        <div class="col-auto nav-item"
          data-react-component="CefConfigKeyEnabled"
          data-react-props='{"cefConfigKey":"featureSet.languages.enabled", "children":"LanguageSelectorButton"}'>
        </div>
      </div>
      <div class="row align-items-center mb-0">
        <!-- ng-if="genericCtrl.cefConfig.featureSet.languages.enabled" -->
        <!--
        <div class="nav-item col">
          <div data-react-component="cefAffiliateAccountSelector"></div>
        </div>
        -->
        <!-- <div class="col-auto nav-item"
          data-react-component="CefConfigKeyEnabled"
          data-react-props='{"cefConfigKey":"featureSet.languages.enabled", "children":"LanguageSelectorButton"}'>
        </div> -->
      </div>
    </div>
  </div>
  <div id="headerBot" class="row">
    <div class="col-12 bg-dark-blue">
      <div class="container gx-0">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark-blue row md-py-0">
          <button type="button" class="navbar-toggler w-auto"
            data-toggle="collapse"
            data-target="#headerBotContent"
            aria-controls="headerBotContent"
            aria-expanded="false"
            translate-attr="{ 'aria-label': 'ui.storefront.userDashboard.ToggleNavigation' }">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div id="headerBotContent"
            class="collapse navbar-collapse pl-0">
            <div class="navbar-nav">
              <dnn:MENU ID="MENUVerticalTabsNav" MenuStyle="VerticalTabsNav" runat="server"></dnn:MENU>
            </div>
          </div>
        </nav>
      </div>
    </div>
  </div>
</header>
