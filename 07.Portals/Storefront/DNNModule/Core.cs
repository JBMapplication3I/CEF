// <copyright file="Core.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the core class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using System.Linq;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Log.EventLog;
    using Newtonsoft.Json;

    public static class Core
    {
        private const string CEFPortalSettingsKeyRoot = "Cef-Settings_";
        private static readonly EventLogController EventLog = new EventLogController();

        public static string CefPortalSettingsKey(int portalId)
        {
            return $"{CEFPortalSettingsKeyRoot}_{portalId}";
        }

        public static CEFPortalSettings GetCEFPortalSettings(PortalInfo portalInfo)
        {
            EventLog.AddLog(
                "CEF DNN Extension",
                $"Portal ID = {portalInfo.PortalID}",
                EventLogController.EventLogType.ADMIN_ALERT);
            var portalController = PortalController.Instance;
            var portalSettingsSettings = portalController.GetPortalSettings(portalInfo.PortalID);
            EventLog.AddLog(
                "CEF DNN Extension",
                $"Portal Settings = {string.Join(",", portalSettingsSettings.Select(kvp => $"[{kvp.Key},{kvp.Value}]"))}",
                EventLogController.EventLogType.ADMIN_ALERT);
            var portalSettingKeyValuePair = portalSettingsSettings.FirstOrDefault(s => s.Key == CefPortalSettingsKey(portalInfo.PortalID));
            CEFPortalSettings settings = null;
            if (!string.IsNullOrWhiteSpace(portalSettingKeyValuePair.Value))
            {
                var portalSettingValue = portalSettingKeyValuePair.Value;
                settings = JsonConvert.DeserializeObject<CEFPortalSettings>(portalSettingValue);
            }
            if (settings == null)
            {
                EventLog.AddLog(
                    "CEF DNN Extension",
                    "CEFPortalSettings = NULL",
                    EventLogController.EventLogType.ADMIN_ALERT);
                settings = new CEFPortalSettings
                {
                    PortalId = portalInfo.PortalID
                };
                UpdateCefPortalSettings(settings, portalInfo);
            }
            if (settings == null || settings.PortalId != portalInfo.PortalID)
            {
                throw new Exception("CEFPortalSettings are not valid");
            }
            return settings;
        }

        public static CEFPortalSettings UpdateCefPortalSettings(CEFPortalSettings settings, PortalInfo portalInfo)
        {
            if (settings == null || settings.PortalId != portalInfo.PortalID)
            {
                throw new ArgumentException("CEFPortalSettings is not valid");
            }
            var portalController = PortalController.Instance;
            portalController.UpdatePortalSetting(
                portalInfo.PortalID,
                CefPortalSettingsKey(portalInfo.PortalID),
                JsonConvert.SerializeObject(settings),
                true,
                portalInfo.CultureCode,
                false);
            return settings;
        }
    }
}
