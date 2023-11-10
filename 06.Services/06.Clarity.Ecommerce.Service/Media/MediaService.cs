namespace Clarity.Ecommerce.Service.Media
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, Authenticate,
     Route("/Media/Downloads","POST",
        Summary = "Endpoint to get downloads for a specific product")]
    public class GetProductDownloads : IReturn<CEFActionResponse> { }

    public partial class MediaService : CEFSharedServiceBase
    {
       public CEFActionResponse Post(GetProductDownloads request)
       {
            var search = new ProductFileSearchModel
            {
                Active = true,
                ProductsOfTypeKey = "Digital"
            };
            return Workflows.ProductFiles.Search(n, out _, null);
        }
    }
}