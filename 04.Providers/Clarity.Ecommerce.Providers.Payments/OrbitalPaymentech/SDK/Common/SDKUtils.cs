#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Common
{
    using System.Xml;
    using Configurator;

    public class SDKUtils
    {
        public static string GetUniqueBatchName(string baseName, string configName)
        {
            ConfigurationData config = null;
            try
            {
                config = Configurator.GetInstance().GetProtocolConfig(configName);
            }
            catch (ConfiguratorException)
            {
            }
            return new FileManager().CreateTempName(baseName, FileType.Outgoing, config);
        }

        public static string GetMessageFormat(XmlNode node, string protocol)
        {
            var format = Utils.GetAttributeValue("message_format", null, node);
            if (format == null)
            {
                switch (protocol)
                {
                    case "TCPBatch":
                    case "TCPConnect":
                    case "SFTPBatch":
                        format = "SLM";
                        break;
                    case "PNSConnect":
                    case "PNSUpload":
                        format = "PNS";
                        break;
                    default:
                        break;
                }
            }
            return format;
        }
    }
}
