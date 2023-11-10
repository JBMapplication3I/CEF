// <copyright file="PersonaBarMenuController.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the persona bar menu controller class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services
{
    using System.Net.Http;
    using System.Web.Http;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Security;
    using DotNetNuke.Web.Api;

    [SupportedModules("ClarityEcommerceDNN"), DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    public class PersonaBarMenuController : DnnApiController
    {
        [HttpGet]
        public HttpResponseMessage Enable()
        {
            return Request.CreateResponse("Hello World!");
        }

        protected string GetEditUrl(int id)
        {
            var editUrl = DotNetNuke.Common.Globals.NavigateURL(
                "Edit",
                $"mid={ActiveModule.ModuleID}",
                $"tid={id}");
            if (PortalSettings.EnablePopUps)
            {
                editUrl = UrlUtils.PopUpUrl(
                    editUrl, PortalSettings, false, false, 550, 950);
            }
            return editUrl;
        }
    }
}
