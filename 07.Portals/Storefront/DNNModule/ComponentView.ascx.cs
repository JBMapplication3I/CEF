// <copyright file="ComponentView.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the component view.ascx class</summary>
// ReSharper disable LocalizableElement, StringLiteralTypo
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CEFComponents;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Security;
    using DotNetNuke.Security.Permissions;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Web.Client.ClientResourceManagement;
    using Newtonsoft.Json;

    public partial class ComponentView : ComponentModuleBase, IActionable
    {
        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                {
                    {
                        GetNextActionID(),
                        Localization.GetString("EditModule", LocalResourceFile),
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        EditUrl(),
                        false,
                        SecurityAccessLevel.Edit,
                        true,
                        false
                    }
                };
                return actions;
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            try
            {
                CEFComponentPlaceholder.Controls.Clear();
                var cefPortalSettings = Core.GetCEFPortalSettings(PortalController.Instance.GetPortal(PortalId));
                if (!(cefPortalSettings?.Enabled ?? false))
                {
                    if (ModulePermissionController.CanEditModuleContent(ModuleConfiguration))
                    {
                        CEFComponentPlaceholder.Controls.Add(new Label
                        {
                            Text = "<div class=\"dnnFormMessage dnnFormError\">CEF Has Not Been Enabled"
                                + $"<p>{JsonConvert.SerializeObject(cefPortalSettings)}</p></div>"
                        });
                    }
                    return;
                }
                if (ComponentInitialized)
                {
                    var controlFilePath = ComponentDefinition.ControlPath;
                    if (!File.Exists(controlFilePath))
                    {
                        throw new FileNotFoundException(
                            $"Clarity eCommerce Component Control File [{controlFilePath}] Not Found.");
                    }
                    if (IsEditable)
                    {
                        CEFComponentPlaceholder.Controls.Add(EditHeadingControl(ComponentDefinition.FriendlyName));
                    }
                    const string ModuleRoot = "/DesktopModules/ClarityEcommerceDNN/";
                    if (ComponentDefinition.Name != "_CEFAdmin")
                    {
                        ClientResourceManager.RegisterStyleSheet(Page, $"{ModuleRoot}css/kendo.common-bootstrap.min.css");
                        ClientResourceManager.RegisterStyleSheet(Page, $"{ModuleRoot}css/kendo.bootstrap.min.css");
                        ClientResourceManager.RegisterStyleSheet(Page, $"{ModuleRoot}css/clarity.css");
                        ClientResourceManager.RegisterStyleSheet(Page, "https://use.fontawesome.com/e1eefd7c43.css");
                        ClientResourceManager.RegisterScript(Page, $"{CEFPortalSettings.UIEndpoint}/lib/cef/framework/ClarityEcommerce.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/custom.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/doubletaptogo.min.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/bootstrap.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/scripts.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/magiczoomplus.js", 100, "DnnFormBottomProvider");
                        ClientResourceManager.RegisterScript(Page, $"{ModuleRoot}js/magicscroll.js", 100, "DnnFormBottomProvider");
                    }
                    if (ComponentDefinition.IsUserControl)
                    {
                        var control = (CEFComponentUserControl)LoadControl(ComponentDefinition.ControlVirtualPath);
                        if (control != null)
                        {
                            control.Initialize(ModuleConfiguration, Component, CEFPortalSettings.UIEndpoint);
                            CEFComponentPlaceholder.Controls.Add(control);
                        }
                    }
                    else
                    {
                        // TODO: Support Token Replacement of Parameters so that HTML Files can have similar flexibility to UserControls
                        CEFComponentPlaceholder.Controls.Add(new LiteralControl(File.ReadAllText(controlFilePath)));
                    }
                }
                else
                {
                    if (IsEditable)
                    {
                        CEFComponentPlaceholder.Controls.Add(EditHeadingControl());
                    }
                    if (ModulePermissionController.CanEditModuleContent(ModuleConfiguration))
                    {
                        CEFComponentPlaceholder.Controls.Add(new Label
                        {
                            Text = "<div class=\"dnnFormMessage dnnFormWarning\">"
                                + "Clarity eCommerce Component Has Not Been Initialized</div>"
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                // Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
            LiteralControl EditHeadingControl(string name = "")
            {
                var controlName = !string.IsNullOrWhiteSpace(name)
                    ? $" : <b>{ComponentDefinition.FriendlyName}</b>"
                    : string.Empty;
                return new LiteralControl(
                    $"<div style=\"position:absolute;top:2px;left:5px;\">Clarity eCommerce Component{controlName}</div>");
            }
        }
    }
}
