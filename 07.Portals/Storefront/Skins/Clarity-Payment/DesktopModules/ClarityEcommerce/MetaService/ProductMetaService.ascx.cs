using System;
using System.IO;
using System.Net;
using System.Web;
using DotNetNuke.UI.Skins;
using Newtonsoft.Json;

public partial class DesktopModules_ClarityEcommerce_MetaService_ProductMetaService : SkinObjectBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetMetadataTitle();
    }
    private void SetMetadataTitle()
    {
        try
        {
            var originalRequestedUrlString = Context.Items["UrlRewrite:OriginalUrl"] as string;
            if (string.IsNullOrWhiteSpace(originalRequestedUrlString)) { return; }
            var originalRequestUrl = new Uri(originalRequestedUrlString);
            var path = originalRequestUrl.PathAndQuery;
            var seoStart = path.IndexOf("/Product/", StringComparison.InvariantCultureIgnoreCase) + "/Product/".Length;
            if (seoStart < 0) { return; }
            var seoUrl = path.Substring(seoStart);
            if (string.IsNullOrWhiteSpace(seoUrl)) { /*Response.Redirect("/", true);*/ return; }
            // Get site url (could be different in dev environment)
            var siteUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/");
            var endPoint = @siteUrl + "DesktopModules/ClarityEcommerce/API-Storefront/Products/Product/Metadata"
                + "?seoUrl=" + HttpUtility.UrlEncode(seoUrl)
                + "&format=json";
            var request = WebRequest.Create(endPoint);
            request.Method = "GET";
            request.ContentType = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            // ReSharper disable once AssignNullToNotNullAttribute
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (string.IsNullOrWhiteSpace(responseString)) { return; }
            var model = JsonConvert.DeserializeObject<Clarity.Ecommerce.Models.ProductModel>(responseString);
            if (model == null) { return; }
            var hasPageTitle = !string.IsNullOrWhiteSpace(model.SeoPageTitle);
            var hasMetaDescription = !string.IsNullOrWhiteSpace(model.SeoDescription);
            var name = hasPageTitle ? model.SeoPageTitle : (model.Name + " | " + PortalSettings.PortalName);
            var desc = hasMetaDescription ? model.SeoDescription : model.ShortDescription;
            var keys = model.SeoKeywords;
            // Create and add meta tag for page title so "ShareThis" code can set the content, inside ajax call is too late for ShareThis to pick up
            ((DotNetNuke.Framework.CDefault)Page).Title = name;
            ((DotNetNuke.Framework.CDefault)Page).Description = desc;
            ((DotNetNuke.Framework.CDefault)Page).KeyWords = keys;
        }
        catch (Exception ex)
        {
            // TODO: Log to DNN Event log
        }
    }
}
