#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Configurator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;
    using Common;
    using Framework;
    using log4net;

    /// <summary>
    /// The JPMC.MSDK.Configuration namespace contains types and methods
    /// that can be used to read the contents of the configuration files.
    /// </summary>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
        // This code is included in order to add XML doc comments to the namespace.
    }

    /// <p>Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights reserved</p>
    /// @author Rameshkumar Bhaskharan
    /// @version 1.0
    /// <summary>
    /// Configurator is a singleton object which hold all the config parameters
    /// </summary>
    public class Configurator : IConfigurator
    {
        /// <summary>
        /// Private static variables
        /// </summary>
        private static Configurator config;
        private static string configFileName;

        private SafeDictionary<string, XmlDocument> xmlDocs;
        private string defConfigName = "MSDKConfig.xml";
        private string defConfigLoc = "config";
        private ILog engineLogger;
        private SafeDictionary<string, ConfigurationData> configData;
        private KeySafeDictionary<string, string> configNames;
        private KeySafeDictionary<string, DefaultValue[]> defaultValues;
        private IConfiguratorFactory factory;

        public Configurator(IConfiguratorFactory factory)
        {
            this.factory = factory;
            configData = new SafeDictionary<string, ConfigurationData>();
            xmlDocs = new SafeDictionary<string, XmlDocument>();
            DFROptions = new List<string>();
        }

        public Configurator(IConfiguratorFactory factory, ILog logger, string homeDir)
        {
            this.factory = factory;
            configData = new SafeDictionary<string, ConfigurationData>();
            xmlDocs = new SafeDictionary<string, XmlDocument>();
            DFROptions = new List<string>();

            engineLogger = logger;
            LoadConfigurator(homeDir);
            DumpMainConfig();
        }

        /// <summary>
        /// Return the status of the configurator
        /// </summary>
        public static bool Initialized => config != null;

        /// <summary>
        /// Resets the configurator.
        /// </summary>
        public static void Reset()
        {
            if (config != null)
            {
                new LoggingWrapper().EngineLogger.Info("Initiated a configuration reset");
                config = null;
            }
            configFileName = null;
        }

        public string HomeDirectory { get; set; }

        /// <summary>
        /// Return a singleton object
        /// </summary>
        public static Configurator GetInstance()
        {
            SetupConfigurator(null, new ConfiguratorFactory());
            return config;
        }

        /// <summary>
        /// Return a singleton object
        /// </summary>
        public static Configurator GetInstance(string homeDir, IConfiguratorFactory factory)
        {
            SetupConfigurator(homeDir, factory);
            return config;
        }

        /// <summary>
        /// Create and return configurator object for the given config file
        /// </summary>
        /// <param name="configFile"></param>
        public static Configurator GetInstance(string configFile)
        {
            if (configFile == null)
            {
                throw new ConfiguratorException(Error.ConfigInitError, "Invalid config file name provided");
            }
            if (configFile.Trim().Length == 0)
            {
                throw new ConfiguratorException(Error.ConfigInitError, "Invalid config file name provided");
            }
            configFile = Utils.FormatDirectoryPath(configFile);

            // Return if already exists
            if (configFile != null && config == null)
            {
                if (configFileName == null)
                {
                    configFileName = configFile;
                }
            }
            if (config != null && !configFileName.ToUpper().Equals(configFile.ToUpper()))
            {
                throw new ConfiguratorException(Error.ConfigInitError, "A singleton configurator is already initialized with the following  [" + configFileName + "]");
            }
            SetupConfigurator(configFile, new ConfiguratorFactory());
            return config;
        }

        public DefaultValue[] GetDefaultValues(ConfigurationData configData)
        {
            var type = configData["HostProcessingSystem"];
            if (type == null)
            {
                type = configData.MessageFormat;
            }
            if (defaultValues.ContainsKey(type))
            {
                return defaultValues[type];
            }
            return new DefaultValue[0];
        }

        /// <summary>
        /// Initialize the configurator object
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static Configurator SetupConfigurator(string homeDir, IConfiguratorFactory factory)
        {
            // Object already exists
            if (config != null)
            {
                return config;
            }
            // create the configurator
            try
            {
                config = new Configurator(factory);
                // Initialize the configurator from config files.
                config.LoadConfigurator(homeDir);
                return config;
            }
            catch (Exception)
            {
                config = null;
                throw;
            }
        }

        /// <summary>
        /// Load the configurator from the config file
        /// </summary>
        public void LoadConfigurator(string overrideHome)
        {
            var logSetup = factory.MakeLoggerWrapper();
            if (overrideHome != null)
            {
                configFileName = Path.Combine(Path.Combine(overrideHome, defConfigLoc), defConfigName);
                if (factory.FileExists(configFileName))
                {
                    HomeDirectory = Path.GetFullPath(overrideHome);
                }
            }
            if (HomeDirectory == null)
            {
                HomeDirectory = factory.GetEnvironmentVariable("MSDK_HOME");
            }
            try
            {
                if (configFileName == null)
                {
                    configFileName = factory.GetFilePath(defConfigName, defConfigLoc, HomeDirectory);
                }
            }
            catch (Exception e)
            {
                config = null;
                engineLogger = null;
                throw new ConfiguratorException(Error.InitializationFailure, "Failed to get HomeDirectory", e);
            }
            if (configFileName != null)
            {
                HomeDirectory = Path.Combine(ExtractDirectoryFromPath(configFileName), "..\\");
            }
            if (HomeDirectory == null)
            {
                throw new ConfiguratorException(Error.InvalidHomeDirectory, "MSDK home directory could not be found.");
            }
            ValidateConfigPath();

            // Load the config file
            try
            {
                LoadMainConfig(configFileName);

                // Sorting the keys is useful for unit testing.
                var keys = new List<string>(configNames.Keys);
                keys.Sort();
                foreach (var configName in keys)
                {
                    LoadProtocolConfig(configName, configNames[configName]);
                }
            }
            catch (Exception e)
            {
                config = null;
                engineLogger = null;
                throw new ConfiguratorException(Error.ConfigInitError, "Exception while loading configuration file" + e.Message, e);
            }
            logSetup.ConfigureLogging(GeneralData.LoadAndWatchLog, HomeDirectory);
            engineLogger = logSetup.EngineLogger;
            engineLogger.Info("Configurator configuration file = " + configFileName);
            SetVersions();

            // Preload the converter templates
            // factory.GetRequestTemplate( RequestType.Online );
            // factory.GetRequestTemplate( RequestType.Batch );
            // factory.GetRequestTemplate( RequestType.PNSOnline );
            // factory.GetResponseTemplate( RequestType.Online );
            // factory.GetResponseTemplate( RequestType.Batch );
            // factory.GetResponseTemplate( RequestType.PNSOnline );
            factory.GetOrbitalSchema();
            using (var defs = factory.GetDefinitions(HomeDirectory))
            {
                var doc = defs.GetXmlDocument("definitions/OrbitalRequest.xml", false);
                var nodes = doc.GetElementsByTagName("Masking");
                if (nodes.Count == 0)
                {
                    throw new ConfiguratorException(Error.ConfigInitError, "Orbital Field Masking could not be found.");
                }
                FieldMasks = new FieldMasks((XmlElement)nodes[0]);
                nodes = doc.GetElementsByTagName("Arrays");
                if (nodes.Count == 0)
                {
                    throw new ConfiguratorException(Error.ConfigInitError, "Orbital Array Config could not be found.");
                }
                OrbitalArrays = new OrbitalArrayDef((XmlElement)nodes[0]);
            }
            LoadDefaultValues();
            var detailLogger = logSetup.DetailLogger;
            if (!detailLogger.IsDebugEnabled)
            {
                detailLogger.Info("MSDK logs the transaction details into Detail log only in DEBUG mode");
            }
            DumpMainConfig();
        }

        private static string ExtractDirectoryFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { return string.Empty; }
            var uri = new UriBuilder(path);
            path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) ?? string.Empty;
        }

        private void DumpMainConfig()
        {
            var msg = "DFR Options that are enabled: ";
            var first = true;
            foreach (var option in DFROptions)
            {
                if (!first)
                {
                    msg += ", ";
                }
                msg += option;
                first = false;
            }
            engineLogger.Info(msg);
            var buff = new StringBuilder("Configuration Name mapping: ");
            buff.AppendLine();
            foreach (var name in configNames.Keys)
            {
                buff.AppendFormat("{0} = {1}", name, configNames[name]);
                buff.AppendLine();
            }
            engineLogger.Info(buff.ToString());
        }

        private void ValidateConfigPath()
        {
            // Invalid config location
            if (configFileName != null)
            {
                return;
            }
            config = null;
            engineLogger = null;
            var msg = $"Exception while loading configuration  file - couldn't locate the file {configFileName}";
            throw new ConfiguratorException(Error.ConfigInitError, msg);
        }

        private void LoadMainConfig(string filename)
        {
            var document = factory.LoadXml(filename);

            var nodes = document.GetElementsByTagName("General");
            if (nodes.Count > 0)
            {
                configData.Add("GeneralData", new GeneralData(nodes[0]));
            }

            nodes = document.GetElementsByTagName("SpecialTemplateNames");
            if (nodes.Count > 0)
            {
                configData.Add("SpecialTemplateNames", new SpecialTemplateNames(nodes[0]));
            }
            xmlDocs.Add(Path.GetFileNameWithoutExtension(filename), document);
            nodes = document.GetElementsByTagName("Option");
            foreach (XmlNode node in nodes)
            {
                var name = Utils.GetAttributeValue("name", null, node);
                var enabled = Utils.StringToBoolean(Utils.GetAttributeValue("enabled", "false", node));
                if (enabled)
                {
                    DFROptions.Add(name);
                }
            }
            configNames = new KeySafeDictionary<string, string>();
            nodes = document.GetElementsByTagName("Config");
            foreach (XmlNode node in nodes)
            {
                var name = Utils.GetAttributeValue("name", null, node);
                var value = Utils.GetAttributeValue("file", null, node);
                if (name == null || value == null)
                {
                    continue;
                }
                configNames.Add(name, value);
            }

            // LoadProtocolConfig( defConfigName.Replace( ".xml", "" ), defConfigName );
        }

        private void LoadProtocolConfig(string configName, string file)
        {
            var filename = Path.Combine(HomeDirectory, "config");
            filename = Path.Combine(filename, file);
            if (!filename.EndsWith(".xml"))
            {
                filename += ".xml";
            }

            var document = factory.LoadXml(filename);

            var protocol = Utils.GetAttributeValue("protocol", null, document.DocumentElement);
            xmlDocs.Add(Path.GetFileNameWithoutExtension(filename), document);

            var data = new ConfigurationData();
            data.ConfigName = configName;
            if (protocol != null)
            {
                try
                {
                    data.Protocol = (CommModule)Enum.Parse(typeof(CommModule), protocol);
                }
                catch
                {
                    data.Protocol = CommModule.Unknown;
                }
            }

            data.MessageFormat = SDKUtils.GetMessageFormat(document.DocumentElement, protocol);

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    var value = Utils.GetNodeValue(node);
                    if (value != null && value.Trim().Length > 0)
                    {
                        data[node.Name] = value;
                    }
                }
            }

            configData.Add(configName, data);
        }

        /// <summary>
        /// Property for config file name
        /// </summary>
        public string ConfigFileName => configFileName;

        public GeneralData GeneralData => (GeneralData)configData["GeneralData"];

        public SpecialTemplateNames SpecialTemplateNames => (SpecialTemplateNames)configData["SpecialTemplateNames"];

        public List<string> DFROptions { get; private set; }

        public XmlDocument GetXmlDocument(string name)
        {
            return xmlDocs[name];
        }

        private void LoadDefaultValues()
        {
            var filename = Path.GetDirectoryName(ConfigFileName);
            filename = Path.Combine(filename, "DefaultValues.xml");
            if (!factory.FileExists(filename))
            {
                engineLogger.DebugFormat("Failed to find the Default Values at {0}", filename);
            }
            try
            {
                var doc = factory.LoadXml(filename);
                defaultValues = new KeySafeDictionary<string, DefaultValue[]>();
                var nodes = doc.DocumentElement.ChildNodes;
                for (var i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    defaultValues.Add(node.Name, GetDefaultValues(node));
                }
            }
            catch (Exception e)
            {
                var msg = $"An exception was caught while loading the default values from {filename}";
                engineLogger.Debug(msg, e);
            }
        }

        private DefaultValue[] GetDefaultValues(XmlNode parentNode)
        {
            var list = new List<DefaultValue>();
            var nodes = parentNode.ChildNodes;
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                var def = new DefaultValue
                {
                    name = Utils.GetAttributeValue("name", null, node),
                    value = Utils.GetAttributeValue("value", null, node),
                    destination = Utils.GetAttributeValue("destination", "Request", node)
                };
                list.Add(def);
            }
            return list.ToArray();
        }

        public ConfigurationData GetProtocolConfig(string configName)
        {
            if (!configData.ContainsKey(configName))
            {
                var msg = "No ConfigurationData by the name \"" + configName + "\" exists.";
                throw new ConfiguratorException(Error.ConfigurationNotFound, msg);
            }
            return new ConfigurationData(configData[configName]);
        }

        public ConfigurationData GetProtocolConfig(CommModule module)
        {
            return GetProtocolConfig(Enum.GetName(typeof(CommModule), module));
        }

        public FieldMasks FieldMasks { get; private set; }

        public OrbitalArrayDef OrbitalArrays { get; private set; }

        private void SetVersions()
        {
            LoadTemplateLibraryVersion();
            var metrics = new SDKMetrics(SDKMetrics.Service.Dispatcher, SDKMetrics.ServiceFormat.API);
            engineLogger.Info("SDK Version is [" + SDKMetrics.SDKVersion + "]");
            engineLogger.Info("Template Library Version is [" + TemplateLibraryVersion + "]");
            if (SDKMetrics.SDKVersion == null || TemplateLibraryVersion == null)
            {
                return;
            }
            var sdkMajorStr = SDKMetrics.SDKVersion.Split('.')[0];
            var templateMajorStr = TemplateLibraryVersion.Split('.')[0];
            var sdkMajor = Utils.StringToInt(sdkMajorStr, -1);
            var templateMajor = Utils.StringToInt(templateMajorStr, -1);
            if (sdkMajor >= TemplateLibraryMinVersion && sdkMajor <= templateMajor)
            {
                return;
            }
            var msg = $"The installed Template Library installed is not valid for this version of the SDK. SDK Version={SDKMetrics.SDKVersion}, Template Library Version={TemplateLibraryVersion}";
            engineLogger.Error(msg);
            throw new ConfiguratorException(Error.TemplateLibraryMismatch, msg);
        }

        private void LoadTemplateLibraryVersion()
        {
            using (var defs = factory.GetDefinitions(HomeDirectory))
            {
                var props = defs.GetProperties("TemplateLibraryVersion.txt");
                if (props == null)
                {
                    var msg = $"The installed Template Library installed is not valid for this version of the SDK. SDK Version={SDKMetrics.SDKVersion}, Template Library Version=NO VERSION FOUND";
                    engineLogger.Error(msg);
                    throw new ConfiguratorException(Error.TemplateLibraryMismatch, msg);
                }
                if (props.ContainsKey("MinSupportedVersion"))
                {
                    TemplateLibraryMinVersion = Utils.StringToInt(props["MinSupportedVersion"]);
                }
                if (props.ContainsKey("TemplateVersion"))
                {
                    TemplateLibraryVersion = props["TemplateVersion"];
                }
            }
        }

        public string TemplateLibraryVersion { get; set; }

        public int TemplateLibraryMinVersion { get; set; }
    }
}
