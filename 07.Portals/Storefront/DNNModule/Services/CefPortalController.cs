// <copyright file="CEFPortalController.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef portal controller class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using Dnn.PersonaBar.Library;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Definitions;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Tabs;
    using Extensions;

    public class CEFPortalController : PersonaBarApiController
    {
        // ReSharper disable once StyleCop.SA1306, StyleCop.SA1401
        protected ComponentController ComponentController = new ComponentController();

        private const string AdminTabName = "Clarity eCommerce Admin";

        [HttpGet]
        public HttpResponseMessage Portals()
        {
            Dictionary<int, string> portals;
            try
            {
                portals = GetPortals()
                    .Select(p => new { p.PortalID, p.PortalName })
                    .ToDictionary(p => p.PortalID, p => p.PortalName);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ApiResponse.Failed(ex.Message, ex));
            }
            return Request.CreateResponse(ApiResponse.Succeeded(portals));
        }

        [HttpGet]
        public HttpResponseMessage Settings(int id)
        {
            CEFPortalSettings settings;
            try
            {
                settings = GetCEFPortalSettings(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ApiResponse.Failed(ex.Message, ex));
            }
            return Request.CreateResponse(ApiResponse.Succeeded(settings));
        }

        [HttpGet]
        public HttpResponseMessage Enable(int id)
        {
            CEFPortalSettings settings;
            try
            {
                settings = GetCEFPortalSettings(id);
                settings.Enabled = true;
                UpdateCEFPortalSettings(settings, id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ApiResponse.Failed(ex.Message, ex));
            }
            return Request.CreateResponse(ApiResponse.Succeeded(settings));
        }

        [HttpGet]
        public HttpResponseMessage Disable(int id)
        {
            CEFPortalSettings settings;
            try
            {
                settings = GetCEFPortalSettings(id);
                settings.Enabled = false;
                UpdateCEFPortalSettings(settings, id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ApiResponse.Failed(ex.Message, ex));
            }
            return Request.CreateResponse(ApiResponse.Succeeded(settings));
        }

        [HttpPost]
        public HttpResponseMessage SaveSettings(CEFPortalSettingsUpdate settingsUpdate)
        {
            try
            {
                var settings = GetCEFPortalSettings(settingsUpdate.PortalId);
                settings.AsService = settingsUpdate.AsService;
                settings.ServiceEndpoint = settingsUpdate.ServiceEndpoint;
                settings.AdminTab = EnsureAdminTab(settings);
                UpdateCEFPortalSettings(settings, settingsUpdate.PortalId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(ApiResponse.Failed(ex.Message, ex));
            }
            return Request.CreateResponse(ApiResponse.Succeeded());
        }

        private static IEnumerable<PortalInfo> GetPortals()
        {
            return PortalController.Instance.GetPortals().Cast<PortalInfo>();
        }

        private static CEFPortalSettings GetCEFPortalSettings(int portalId)
        {
            return Core.GetCEFPortalSettings(PortalController.Instance.GetPortal(portalId));
        }

        private static void UpdateCEFPortalSettings(CEFPortalSettings settings, int portalId)
        {
            Core.UpdateCefPortalSettings(settings, PortalController.Instance.GetPortal(portalId));
        }

        private CEFTabSettings EnsureAdminTab(CEFPortalSettings settings)
        {
            // Admin Tab Settings
            var adminTabSettings = settings.AdminTab;
            if ((adminTabSettings?.TabId ?? 0) == 0)
            {
                adminTabSettings = new CEFTabSettings
                {
                    TabId = 0,
                    ParentId = PortalSettings.AdminTabId,
                    TabName = AdminTabName,
                    Title = AdminTabName,
                    Description = AdminTabName,
                    SkinSrc = "[G]Skins/_default/No Skin.ascx",
                    ContainerSrc = "[G]Containers/_default/No Container.ascx"
                };
            }
            // Admin Tab Components
            var adminComponentDefinition = ComponentController.GetComponentDefinition("_CEFAdmin");
            var adminComponents = new List<CEFComponent>
            {
                new CEFComponent(adminComponentDefinition, adminComponentDefinition.Parameters)
            };
            return EnablePage(adminTabSettings, adminComponents);
        }

        private CEFTabSettings EnablePage(CEFTabSettings tabSettings, ICollection<CEFComponent> cefComponents)
        {
            var tabName = tabSettings.TabName;
            var portalId = PortalSettings.PortalId;
            var parentTabId = tabSettings.ParentId;
            var defaultPortalSkin = PortalSettings.DefaultPortalSkin;
            var defaultPortalContainer = PortalSettings.DefaultPortalContainer;
            var tabController = TabController.Instance;
            var parentTab = tabController.GetTab(parentTabId, portalId);
            if (parentTab == null)
            {
                throw new Exception($"Cannot find Parent Page/Tab with ID {parentTabId}");
            }
            var tab = tabController.GetTabByName(tabName, portalId)
                ?? new TabInfo();
            tab.TabName = tabName;
            tab.PortalID = portalId;
            tab.ParentId = parentTab.TabID;
            tab.Title = tabSettings.Title;
            tab.Description = tabSettings.Description;
            tab.KeyWords = tabSettings.KeyWords;
            tab.IsVisible = true;
            tab.IsDeleted = false;
            tab.DisableLink = false;
            tab.Url = string.Empty;
            tab.SkinSrc = !string.IsNullOrWhiteSpace(tabSettings.SkinSrc) ? tabSettings.SkinSrc : defaultPortalSkin;
            tab.ContainerSrc = !string.IsNullOrWhiteSpace(tabSettings.ContainerSrc) ? tabSettings.ContainerSrc : defaultPortalContainer;
            tab.IsSuperTab = false;
            tab.PageHeadText = tabSettings.PageHeaderText;
            // Commenting this out so that permissions have to be set in DNN. This will make any page/tan Admin access by default.
            // Add View Permission if it does not already exist.
            ////if (tab.TabPermissionsSpecified && !tab.TabPermissions.Where(p => p.PermissionKey == "VIEW").Any())
            ////{
            ////    foreach (PermissionInfo p in PermissionController.GetPermissionsByTab())
            ////    {
            ////        if (p.PermissionKey == "VIEW")
            ////        {
            ////            var tpi = new TabPermissionInfo
            ////            {
            ////                PermissionID = p.PermissionID,
            ////                PermissionKey = p.PermissionKey,
            ////                PermissionName = p.PermissionName,
            ////                AllowAccess = true,
            ////                RoleID = -1 //ID of all users
            ////            };
            ////            tab.TabPermissions.Add(tpi);
            ////        }
            ////    }
            ////}
            if (tab.TabID > 0)
            {
                tabController.UpdateTab(tab);
            }
            else
            {
                tab.TabID = tabController.AddTab(tab, true);
            }
            tabSettings.TabId = tab.TabID;
            DataCache.ClearModuleCache(tab.TabID);
            // Find Existing Clarity eCommerce Components
            var moduleController = new ModuleController();
            var cefComponentModules = moduleController.GetTabModules(tab.TabID)
                .Select(i => i.Value)
                .Where(m => m.DesktopModule?.ModuleName == "CEFComponent" && m.IsDeleted == false);
            var existingCefComponents = new List<string>();
            foreach (var module in cefComponentModules)
            {
                var settings = ComponentController.GetCEFComponentSettings(module);
                var cefComponent = cefComponents?.FirstOrDefault(c => c.Definition?.Name == settings.ComponentDefinition);
                if (cefComponent == null) { continue; }
                // Update Module
                module.ModuleTitle = cefComponent.Definition?.FriendlyName;
                moduleController.UpdateModule(module);
                existingCefComponents.Add(settings.ComponentDefinition);
            }
            // Get an info for CEFComponent Module
            DesktopModuleInfo info = null;
            foreach (var kvp in DesktopModuleController.GetDesktopModules(portalId))
            {
                var mod = kvp.Value;
                if (mod == null || mod.ModuleName != "Clarity.Ecommerce.Dnn.Extensions.Component")
                {
                    continue;
                }
                info = mod;
                break;
            }
            if (cefComponents == null)
            {
                return tabSettings;
            }
            foreach (var cefComponent in cefComponents)
            {
                if (existingCefComponents.Contains(cefComponent.Definition?.Name) || info == null)
                {
                    continue;
                }
                // Add Module
                foreach (var moduleDefinitionInfo in
                    ModuleDefinitionController.GetModuleDefinitionsByDesktopModuleID(info.DesktopModuleID).Values)
                {
                    var moduleInfo = new ModuleInfo
                    {
                        PortalID = portalId,
                        TabID = tab.TabID,
                        ModuleOrder = 1,
                        ModuleTitle = cefComponent.Definition?.FriendlyName,
                        PaneName = "ContentPane",
                        ModuleDefID = moduleDefinitionInfo.ModuleDefID,
                        CacheTime = moduleDefinitionInfo.DefaultCacheTime,
                        InheritViewPermissions = true,
                        AllTabs = false,
                        Alignment = "Top"
                    };
                    var moduleId = moduleController.AddModule(moduleInfo);
                    moduleInfo = moduleController.GetModule(moduleId);
                    ComponentController.SetModuleComponent(moduleInfo, cefComponent.Definition);
                    ////ComponentController.UpdateModuleComponentParameters(moduleInfo, cefComponent.Parameters);
                }
            }
            return tabSettings;
        }
    }
}
