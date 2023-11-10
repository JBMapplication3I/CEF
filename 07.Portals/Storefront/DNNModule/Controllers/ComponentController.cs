// <copyright file="ComponentController.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the component controller class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using Newtonsoft.Json;

    public class ComponentController : IUpgradeable ////: IPortable, ISearchable, IUpgradeable
    {
        /// <summary>The cef components extension.</summary>
        public const string CEFComponentsExtension = ".CDef.json";

        /// <summary>The CEF Components Path</summary>
        public static readonly string CEFComponentsPath
            = $"{HttpRuntime.AppDomainAppPath}DesktopModules\\ClarityEcommerceDNN\\CEFComponents\\";

        /// <summary>Full pathname of the CEF components virtual file.</summary>
        // ReSharper disable once StyleCop.SA1401
        public static string CEFComponentsVirtualPath = "~/DesktopModules/ClarityEcommerceDNN/CEFComponents/";

        /// <summary>The cef component settings key.</summary>
        private const string CEFComponentSettingsKey = "CEFComponent-Settings";

        /// <summary>The module controller.</summary>
        private static readonly IModuleController ModuleController
            = DotNetNuke.Entities.Modules.ModuleController.Instance;

        /// <summary>Gets cef component settings.</summary>
        /// <param name="moduleInfo">Information describing the module.</param>
        /// <returns>The cef component settings.</returns>
        public static CEFComponentSettings GetCEFComponentSettings(ModuleInfo moduleInfo)
        {
            var moduleSettings = moduleInfo.ModuleSettings;
            var componentSettingsSetting = (string)moduleSettings[CEFComponentSettingsKey];
            CEFComponentSettings settings = null;
            if (!string.IsNullOrWhiteSpace(componentSettingsSetting))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<CEFComponentSettings>(componentSettingsSetting);
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }
            }
            if (settings != null)
            {
                return settings;
            }
            settings = new CEFComponentSettings(moduleInfo.ModuleID);
            ModuleController.UpdateModuleSetting(
                moduleInfo.ModuleID,
                CEFComponentSettingsKey,
                JsonConvert.SerializeObject(settings));
            return settings;
        }

        /// <summary>Sets module component.</summary>
        /// <param name="moduleInfo">Information describing the module.</param>
        /// <param name="definition">The definition.</param>
        public static void SetModuleComponent(ModuleInfo moduleInfo, CEFComponentDefinition definition)
        {
            var settings = GetCEFComponentSettings(moduleInfo);
            settings.ComponentDefinition = definition.Name;
            ModuleController.UpdateModuleSetting(
                moduleInfo.ModuleID,
                CEFComponentSettingsKey,
                JsonConvert.SerializeObject(settings));
        }

        /// <summary>Updates the module component parameters.</summary>
        /// <param name="moduleInfo">Information describing the module.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        public static void UpdateModuleComponentParameters(
            ModuleInfo moduleInfo, Dictionary<string, CEFComponentParameter> parameters)
        {
            var settings = GetCEFComponentSettings(moduleInfo);
            settings.Parameters = parameters;
            ModuleController.UpdateModuleSetting(
                moduleInfo.ModuleID,
                CEFComponentSettingsKey,
                JsonConvert.SerializeObject(settings));
        }

        /// <summary>Component path.</summary>
        /// <param name="name">     The name.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>A string.</returns>
        public static string ComponentPath(string name, string extension)
        {
            return $"{CEFComponentsPath}{name}{extension}";
        }

        /// <summary>Component virtual path.</summary>
        /// <param name="name">     The name.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>A string.</returns>
        public static string ComponentVirtualPath(string name, string extension)
        {
            return $"{CEFComponentsVirtualPath}{name}{extension}";
        }

        /// <summary>Gets component definition.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The component definition.</returns>
        public CEFComponentDefinition GetComponentDefinition(string name)
        {
            CEFComponentDefinition definition = null;
            if (!string.IsNullOrWhiteSpace(name))
            {
                definition = ReadComponentDefinitionFile(ComponentPath(name, CEFComponentsExtension));
            }
            return definition;
        }

        /// <summary>Gets component definitions.</summary>
        /// <returns>The component definitions.</returns>
        public List<CEFComponentDefinition> GetComponentDefinitions()
        {
            return Directory.GetFiles(CEFComponentsPath, $"*{CEFComponentsExtension}")
                .Select(ReadComponentDefinitionFile)
                .Where(x => x != null)
                .ToList();
        }

        /// <summary>Gets a component.</summary>
        /// <param name="moduleId">Identifier for the module.</param>
        /// <param name="tabId">   Identifier for the tab.</param>
        /// <returns>The component.</returns>
        public CEFComponent GetComponent(int moduleId, int tabId)
        {
            var moduleInfo = ModuleController.GetModule(moduleId, tabId, false);
            if (moduleInfo == null)
            {
                throw new Exception("M1: Error Rendering Component.");
            }
            var settings = GetCEFComponentSettings(moduleInfo);
            if (settings == null)
            {
                throw new Exception("S1: Error Rendering Component.");
            }
            var definition = GetComponentDefinition(settings.ComponentDefinition);
            var parameters = settings.Parameters;
            // Ensure All Settings
            if (!(definition?.Parameters?.Any() ?? false))
            {
                return definition != null
                    ? new CEFComponent(definition, parameters)
                    : null;
            }
            foreach (var definitionParameter in definition.Parameters)
            {
                if (!parameters.ContainsKey(definitionParameter.Key))
                {
                    parameters[definitionParameter.Key] = definitionParameter.Value.Clone();
                }
                else
                {
                    // Overwrite Parameter Properties
                    parameters[definitionParameter.Key].Name = definitionParameter.Value?.Name;
                    parameters[definitionParameter.Key].FriendlyName = definitionParameter.Value?.FriendlyName;
                    parameters[definitionParameter.Key].Description = definitionParameter.Value?.Description;
                    parameters[definitionParameter.Key].Group = definitionParameter.Value?.Group;
                    parameters[definitionParameter.Key].Order = definitionParameter.Value?.Order ?? 0;
                    parameters[definitionParameter.Key].Type = definitionParameter.Value?.Type;
                }
            }
            return new CEFComponent(definition, parameters);
        }

        /// <summary>UpgradeModule implements the IUpgradeable Interface.</summary>
        /// <param name="version">The current version of the module.</param>
        /// <returns>A string.</returns>
        public string UpgradeModule(string version)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>Reads component definition file.</summary>
        /// <param name="filePath">Full pathname of the file.</param>
        /// <returns>The component definition file.</returns>
        private static CEFComponentDefinition ReadComponentDefinitionFile(string filePath)
        {
            CEFComponentDefinition definition = null;
            try
            {
                var json = File.ReadAllText(filePath);
                definition = JsonConvert.DeserializeObject<CEFComponentDefinition>(json);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return definition;
        }
    }
}
