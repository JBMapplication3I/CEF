// <copyright file="ComponentModuleBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the component module base class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Exceptions;
    using Encryption;

    public class ComponentModuleBase : PortalModuleBase
    {
        public Dictionary<string, CEFComponentParameter> ComponentParameters { get; set; } = new Dictionary<string, CEFComponentParameter>();

        protected ComponentController ComponentController { get; } = new ComponentController();

        protected List<CEFComponentDefinition> ComponentDefinitions { get; set; }

        protected CEFComponentDefinition ComponentDefinition { get; set; }

        protected CEFComponent Component { get; set; }

        protected CEFPortalSettings CEFPortalSettings { get; set; }

        protected bool ComponentInitialized => Component?.Initialized ?? false;

        protected virtual void Page_Init(object sender, EventArgs e)
        {
            try
            {
                CEFPortalSettings = Core.GetCEFPortalSettings(PortalController.Instance.GetPortal(PortalId));
                ComponentDefinitions = ComponentController.GetComponentDefinitions();
                LoadComponent();
            }
            catch (Exception exc)
            {
                // Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Request.IsAuthenticated)
                {
                    return;
                }
                var currentUserName = (HtmlInputHidden)FindControl("cef_dnn_hdnCurrentUsername");
                if (currentUserName == null)
                {
                    currentUserName = new HtmlInputHidden
                    {
                        ID = "cef_dnn_hdnCurrentUsername",
                        ClientIDMode = ClientIDMode.Static,
                        Value = GetCurrentUserName()
                    };
                    Controls.Add(currentUserName);
                }
                var currentToken = (HtmlInputHidden)FindControl("cef_dnn_hdnCurrentToken");
                if (currentToken != null)
                {
                    return;
                }
                currentToken = new HtmlInputHidden
                {
                    ID = "cef_dnn_hdnCurrentToken",
                    ClientIDMode = ClientIDMode.Static,
                    Value = GetCurrentToken()
                };
                Controls.Add(currentToken);
            }
            catch (Exception exc)
            {
                // Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void LoadComponent()
        {
            Component = ComponentController.GetComponent(ModuleId, TabId);
            ComponentDefinition = Component?.Definition;
            ComponentParameters = Component?.Parameters;
            if (ComponentInitialized && ComponentDefinitions.All(d => d.Name != ComponentDefinition?.Name))
            {
                // Current Component is no longer supported because there is no definition with the same name.
                ComponentDefinition = null;
            }
        }

        private static string GetCurrentUserName()
        {
            return UserController.Instance.GetCurrentUserInfo().Username;
        }

        private static string GetCurrentToken()
        {
            return UserController.Instance?.GetCurrentUserInfo()?.Username == null
                ? string.Empty
#pragma warning disable 618
                : CMSApiEncoder.Encrypt(GetTokenValue());
#pragma warning restore 618
        }

        private static string GetTokenValue()
        {
            // Note: It can occur that the date when encoded in this fashion can produce a value that contains an '|'
            // character which may throw things off if '|' is used as a delimiter. In fact I suppose any character
            // would face the same challenge. Recommend refactoring.
            return Convert.ToBase64String(
                BitConverter.GetBytes(DateTime.UtcNow.ToBinary())
                    .Concat(GetBytes("|"))
                    .Concat(Guid.NewGuid().ToByteArray())
                    .Concat(GetBytes("|"))
                    .Concat(GetBytes(UserController.Instance.GetCurrentUserInfo().Username))
                    .ToArray());
        }

        private static IEnumerable<byte> GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
