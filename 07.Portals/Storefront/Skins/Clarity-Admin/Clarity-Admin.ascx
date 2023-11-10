<%@ Control Language="C#" AutoEventWireup="true" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="DotNetNuke.Entities.Host" %>
<%@ Register TagPrefix="fortyfingers" TagName="STYLEHELPER" Src="/DesktopModules/40Fingers/SkinObjects/StyleHelper/StyleHelper.ascx" %>
<head>
<meta name="fragment" content="!">
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0">
<link rel="stylesheet" href="https://api.jandbmedical.com/UI-Admin/lib/cef/css/admin-bundle.css" />
<title>Clarity Ecommerce Framework Admin</title>
<script src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/0-jQuery.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/1-angular.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/2-kendo.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
</head>
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="admin.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="personaBarContainer.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="main.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="graph.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="Pages.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="ComboBox.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="ComboBox.DnnBlack.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="ControlBar.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="default.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="dnn.DropDownList.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="dnn.FileUpload.css" />
<fortyfingers:STYLEHELPER runat="server" RemoveCssFile="dnn.jScrollBar.css" />
<style>
#ControlBar_ControlPanel { display: none; }
.personalBarContainer { display: none; }
#Body { margin-left: 0 !important; }
</style>
<script runat="server" language="C#">
  public string BuildNumber = Host.CrmVersion.ToString(CultureInfo.InvariantCulture);
</script>
<% if (Request.IsAuthenticated) { %>
<asp:HiddenField ID="hdnCurrentUsername" runat="server" />
<asp:HiddenField ID="hdnCurrentToken" runat="server" />
<script type="C#" runat="server">
private const string PasswordHash = "qpzm9731";
private const string SaltKey = "Cl4r1tyV3ntur3s";
private const string VIKey = "46g7HeDF@52c831B";

private static string Encrypt(string plainText, bool isUrlEncoded = false)
{
var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
byte[] cipherTextBytes;
using (var memoryStream = new MemoryStream())
{
using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
{
cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
cryptoStream.FlushFinalBlock();
cipherTextBytes = memoryStream.ToArray();
}
}
var output = Convert.ToBase64String(cipherTextBytes);
if (isUrlEncoded) { output = HttpUtility.UrlEncode(output); }
return output;
}

private static IEnumerable<byte> GetBytes(string str)
{
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
.Concat(GetBytes(UserController.Instance.GetCurrentUserInfo().Username))
.ToArray()
)
);
</script>
<script>
$("#<%=hdnCurrentUsername.ClientID %>").attr("value","<%=UserController.Instance.GetCurrentUserInfo().Username%>");
$("#<%=hdnCurrentToken.ClientID %>").attr("value","<%=token%>");
</script>
<% } %>
<base href="/" />
<div class="standalone full-height" ng-cloak>
<div class="container-fluid menu-hidden">
<div id="header" cv-admin-site-menu-2></div>
</div>
<div id="content" ng-controller="AppController" ng-class="{'cvContentNoAuth':!$root.currentUser.isAuthenticated}">
<div class="innerLR" cef-admin-body>
<div ui-view="page" class="full-height"></div>
</div>
</div>
<!-- <script src="http://www.google.com/jsapi"></script> -->
<!-- <script defer src="https://api.jandbmedical.com/UI-Admin/Scripts/thirdparty/google/gviz-api.js"></script> -->

<script defer src="https://kit.fontawesome.com/4d87c1b73b.js" crossorigin="anonymous"></script>
<link rel="stylesheet" href="/Portals/_default/Skins/Clarity-Admin/kendo.common-bootstrap.min.css" />
<link rel="stylesheet" href="/Portals/_default/Skins/Clarity-Admin/kendo.bootstrap.min.css" />
<script defer src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/4-cef-admin-main.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/5-cef-admin-templates.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api.jandbmedical.com/API-Admin/JSConfigs/Admin?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
<script defer src="https://api.jandbmedical.com/UI-Admin/lib/cef/js/6-cef-admin-init.min.js?_=<%= 1/*DateTime.Now.Ticks*/ %><%= BuildNumber %>"></script>
</div>
<div ID="ContentPane" runat="server" class="hide"></div>
