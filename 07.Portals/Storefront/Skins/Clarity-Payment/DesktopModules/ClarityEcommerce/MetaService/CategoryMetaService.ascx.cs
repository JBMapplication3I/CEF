using System;
using System.IO;
using System.Net;
using System.Web;
using DotNetNuke.UI.Skins;
using Newtonsoft.Json;

public partial class DesktopModules_ClarityEcommerce_MetaService_CategoryService : SkinObjectBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetMetadataTitle();
    }
    private void SetNoMetaData(
        Exception ex = null,
        int line = -1,
        int seoStart = -1,
        string path = null,
        string seoUrl = null,
        string originalRequestedUrlString = null,
        string rawUrl = null,
        string fullUrl = null)
    {
        ((DotNetNuke.Framework.CDefault)Page).Title = "Product Catalog | " + PortalSettings.PortalName;
        var desc = "The product catalog for " + PortalSettings.PortalName;
        //if (ex != null)
        //{
        //    desc += "\r\nEx Message: " + ex.Message;
        //    desc += "\r\nEx StackTrace: " + ex.StackTrace;
        //    desc += "\r\nLine: " + line.ToString();
        //    desc += "\r\nseoStart: " + seoStart.ToString();
        //    desc += "\r\nPath: " + path;
        //    desc += "\r\nseoUrl: " + seoUrl;
        //    desc += "\r\noriginalRequestedUrlString: " + originalRequestedUrlString;
        //    desc += "\r\nrawUrl: " + rawUrl;
        //    desc += "\r\nfullUrl: " + fullUrl;
        //    desc += "\r\nHttpContext.Current.Request.Url.Host: " + HttpContext.Current.Request.Url.Host;
        //    desc += "\r\nHttpContext.Current.Request.Url.Authority: " + HttpContext.Current.Request.Url.Authority;
        //    desc += "\r\nHttpContext.Current.Request.Url.Port: " + HttpContext.Current.Request.Url.Port;
        //    desc += "\r\nHttpContext.Current.Request.Url.AbsolutePath: " + HttpContext.Current.Request.Url.AbsolutePath;
        //    desc += "\r\nHttpContext.Current.Request.ApplicationPath: " + HttpContext.Current.Request.ApplicationPath;
        //    desc += "\r\nHttpContext.Current.Request.Url.AbsoluteUri: " + HttpContext.Current.Request.Url.AbsoluteUri;
        //    desc += "\r\nHttpContext.Current.Request.Url.PathAndQuery: " + HttpContext.Current.Request.Url.PathAndQuery;
        //    desc += "\r\nHttpContext.Current.Request.Url.Fragment: " + HttpContext.Current.Request.Url.Fragment;
        //    desc += "\r\nHttpContext.Current.Request.RawUrl: " + HttpContext.Current.Request.RawUrl;
        //    desc += "\r\nContext.Items: (list)";
        //    foreach (System.Collections.DictionaryEntry i in Context.Items)
        //    {
        //        desc += "\r\n" + i.Key + ": " + i.Value as string;
        //    }
        //}
        ((DotNetNuke.Framework.CDefault)Page).Description = desc;
    }
    private void SetMetadataTitle()
    {
        var line = 0;
        var seoStart = 0;
        var path = string.Empty;
        var seoUrl = string.Empty;
        var originalRequestedUrlString = string.Empty;
        var rawUrl = string.Empty;
        var fullUrl = string.Empty;
        try
        {
            rawUrl = HttpContext.Current.Request.Url.Scheme
                + "://" + HttpContext.Current.Request.Url.Authority
                + HttpContext.Current.Request.RawUrl;
            // fullUrl = TabController.CurrentPage.FullUrl;
            originalRequestedUrlString = Context.Items["UrlRewrite:OriginalUrl"] as string;
            if (string.IsNullOrWhiteSpace(originalRequestedUrlString))
            {
                SetNoMetaData();
                return;
            }
            var originalRequestUrl = new Uri(originalRequestedUrlString);
            path = originalRequestUrl.PathAndQuery + originalRequestUrl.Fragment;
            var indexOfPathPart = path.IndexOf("/Category/", StringComparison.InvariantCultureIgnoreCase);
            if (indexOfPathPart != -1)
            {
                seoStart = indexOfPathPart + "/Category/".Length;
            }
            if (seoStart < 0)
            {
                // ex: "?category=Electronics%7CCAT-6"
                var indexOfHashQueryPart = path.IndexOf("category=", StringComparison.InvariantCultureIgnoreCase);
                if (indexOfHashQueryPart != -1)
                {
                    seoStart = indexOfHashQueryPart + "category=".Length;
                }
            }
            if (seoStart < 0)
            {
                SetNoMetaData();
                return;
            }
            line = 62;
            seoUrl = path.Substring(seoStart);
            line = 64;
            if (seoUrl.Contains("&"))
            {
                line = 67;
                seoUrl = seoUrl.Substring(0, seoUrl.IndexOf("&"));
                line = 69;
            }
            line = 71;
            if (string.IsNullOrWhiteSpace(seoUrl))
            {
                line = 74;
                /*Response.Redirect("/", true);*/
                SetNoMetaData();
                return;
            }
            line = 79;
            // Get site url (could be different in dev environment)
            var siteUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/");
            line = 82;
            var endPoint = @siteUrl + "DesktopModules/ClarityEcommerce/API-Storefront/Categories/Category/Metadata"
                + "?seoUrl=" + HttpUtility.UrlEncode(seoUrl)
                + "&format=json";
            line = 86;
            var request = WebRequest.Create(endPoint);
            request.Method = "GET";
            request.ContentType = "application/json";
            line = 90;
            var response = (HttpWebResponse)request.GetResponse();
            line = 92;
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            line = 94;
            var model = JsonConvert.DeserializeObject<Clarity.Ecommerce.Models.CategoryModel>(responseString);
            line = 96;
            if (model == null)
            {
                line = 99;
                SetNoMetaData();
                return;
            }
            line = 103;
            var hasPageTitle = !string.IsNullOrWhiteSpace(model.SeoPageTitle);
            var hasMetaDescription = !string.IsNullOrWhiteSpace(model.SeoDescription);
            line = 106;
            var name = hasPageTitle ? model.SeoPageTitle : (model.Name + " | " + PortalSettings.PortalName);
            var desc = hasMetaDescription ? model.SeoDescription : model.Description;
            var keys = model.SeoKeywords;
            line = 110;
            // Create and add meta tag for page title so "ShareThis" code can set the content, inside ajax call is too late for ShareThis to pick up
            ((DotNetNuke.Framework.CDefault)Page).Title = name;
            ((DotNetNuke.Framework.CDefault)Page).Description = desc;
            ((DotNetNuke.Framework.CDefault)Page).KeyWords = keys;
            line = 115;
        }
        catch (Exception ex)
        {
            // TODO: Log to DNN Event log
            SetNoMetaData(ex, line, seoStart, path, seoUrl, originalRequestedUrlString, rawUrl, fullUrl);
        }
    }
}
