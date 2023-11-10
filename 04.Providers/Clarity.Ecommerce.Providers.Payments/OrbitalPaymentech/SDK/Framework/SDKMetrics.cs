#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Framework
{
    using System;
    using System.Reflection;
    using System.Text;
    using System.Xml.XPath;
    using Common;
    using Configurator;

    public class SDKMetrics
    {
        public enum PointOfEntry { Stratus, Tandem, NetConnect, Orbital, None };
        public enum Service { Servlet, NVPAdapter, Dispatcher, None };
        public enum ServiceFormat { JSON_RPC, XML, CSV, UE, NVP, API, None };
        public enum MessageOrigin { Local, Remote, None };

        public enum CommMode { None, Intranet, Internet };

        // Static version information is gotten from the identity file
        private static string identityPathname = "Clarity.Ecommerce.Providers.Payments.OrbitalPaymentech.SDK.Configurator.Definition.identity.xml";
        private static string defaultVersionString = "****";

        public PointOfEntry PointOfEntryMetric { get; set; }     // 2 bits
        public static int TheRuntimeIndex { get; set; }          // 2 bits
        public Service ServiceMetric { get; set; }               // 2 bits
        public ServiceFormat ServiceFormatMetric { get; set; }   // 3 bits
        public MessageOrigin MessageOriginMetric { get; set; }   // 1 bit
        public static int OSIndexMetric { get; set; }            // 6 bits
        public CommMode CommModeMetric { get; set; }             // 1 bit
        public static int SDKVersionIndexMetric { get; set; }    // 7 bits

        // SDK version (textual) is not included in the bitmap, but is referenced elsewhere.
        public static string SDKVersion { get; set; }
        private volatile static bool isInitiated;

        public SDKMetrics()
        {
            // Initialize the static information on the first instantiation.
            // Note: uses the fixed double-checked locking pattern for thread safety and efficiency.
            if (isInitiated)
            {
                return;
            }
            lock (this)
            {
                if (!isInitiated)
                {
                    Init();
                }
            }
        }

        public SDKMetrics(Service service, ServiceFormat format) : this()
        {
            ServiceMetric = service;
            ServiceFormatMetric = format;
            PointOfEntryMetric = PointOfEntry.Stratus;
            MessageOriginMetric = MessageOrigin.Local;
        }

        /**
         * Initializes the static information in the Metrics object
         */
        private void Init()
        {
            PointOfEntryMetric = PointOfEntry.None;
            TheRuntimeIndex = 0;
            ServiceMetric = Service.None;
            ServiceFormatMetric = ServiceFormat.None;
            MessageOriginMetric = MessageOrigin.None;
            OSIndexMetric = 0;
            CommModeMetric = CommMode.None;
            SDKVersionIndexMetric = 0;
            SDKVersion = null;
            var asm = Assembly.GetExecutingAssembly();
            var xmlStream = asm.GetManifestResourceStream(identityPathname);
            if (xmlStream == null)
            {
                throw new ConfiguratorException(Error.ConfigInitError, "Exception while loading identity file - couldn't locate identity.xml");
            }
            // Use XPathDocument for speed
            XPathDocument identityDoc;
            try
            {
                identityDoc = new XPathDocument(xmlStream);
            }
            catch (Exception e)
            {
                throw new ConfiguratorException(Error.ConfigInitError, "Exception while loading identity file " + e);
            }
            var osName = GetOSName();
            OSIndexMetric = GetIdentityValue(identityDoc, osName);
            // Get and set the SDK version value
            SDKVersion = Utils.GetXPathValue(identityDoc, "//Version/Current/matches/text()");
            SDKVersionIndexMetric = Utils.StringToInt(Utils.GetXPathValue(identityDoc, "//Version/Current/value/text()"));
            var frameworkName = Environment.Version.Major + "." + Environment.Version.Minor;
            TheRuntimeIndex = GetIdentityValue(identityDoc, frameworkName);
            isInitiated = true;
        }

        public void SetCommMode(CommModule module)
        {
            CommModeMetric = IsInternet(module) ? CommMode.Internet : CommMode.Intranet;
        }

        public void SetPointOfEntry(MessageFormat format)
        {
            switch (format)
            {
                case MessageFormat.SLM:
                    PointOfEntryMetric = PointOfEntry.Stratus;
                    break;
                case MessageFormat.PNS:
                    PointOfEntryMetric = PointOfEntry.Tandem;
                    break;
                case MessageFormat.ORB:
                    PointOfEntryMetric = PointOfEntry.Orbital;
                    break;
                case MessageFormat.None:
                    break;
            }
        }

        public void SetPointOfEntryFromTransactionType(string trans)
        {
            switch (trans)
            {
                case null:
                    PointOfEntryMetric = PointOfEntry.None;
                    break;
                case "NewTransaction":
                    PointOfEntryMetric = PointOfEntry.Stratus;
                    break;
                case "PNSNewTransaction":
                    PointOfEntryMetric = PointOfEntry.Tandem;
                    break;
                case "HeartBeat":
                    PointOfEntryMetric = PointOfEntry.Stratus;
                    break;
                case "Submission":
                    PointOfEntryMetric = PointOfEntry.Stratus;
                    break;
                default:
                    PointOfEntryMetric = PointOfEntry.None;
                    break;
            }
        }

        /**
         * Converts the metrics to a bitmap and then Base64 encodes it.
         * Returns the 4-byte character string.
         */
        public string ToBase64()
        {
            var digit = new char[4];
            const byte SixBits = 63;
            // check that the parameters are within range
            if ((int)PointOfEntryMetric > 3
               || TheRuntimeIndex > 3
               || (int)ServiceMetric > 3
               || (int)ServiceFormatMetric > 7
               || (int)MessageOriginMetric > 1
               || OSIndexMetric > 63
               || (int)CommModeMetric > 2
               || (int)CommModeMetric < 1
               || SDKVersionIndexMetric > 127)
            {
                return defaultVersionString;
            }

            //
            // Build the bitmap
            //

            var bitmap = (int)PointOfEntryMetric;

            bitmap <<= 1;
            bitmap |= (int)CommModeMetric - 1;  // CommMode's 0th ordinal is "none"

            bitmap <<= 2;
            bitmap |= TheRuntimeIndex;

            bitmap <<= 2;
            bitmap |= (int)ServiceMetric;

            bitmap <<= 3;
            bitmap |= (int)ServiceFormatMetric;

            bitmap <<= 1;
            bitmap |= (int)MessageOriginMetric;

            bitmap <<= 6;
            bitmap |= OSIndexMetric;

            bitmap <<= 7;
            bitmap |= SDKVersionIndexMetric;

            // now split into 6 bit chunks

            // the first chunk is bit 23 through bit 18 so if we shift 18
            // we will get the result we want
            var tmp = (byte)(bitmap >> 18);
            tmp &= SixBits; // if sign extension
            digit[0] = (char)Utils.Base64Digit(tmp);

            // shifting the original by only 12 gives us the high 12 bits
            // so we have to mask off the high order 6 (actually only the
            // high order 2 bits because we lost the top 4 when we cast to
            // a char
            digit[1] = (char)(bitmap >> 12);

            // sixBits is 63 which is all 6 low order bits set
            tmp = (byte)(digit[1] & SixBits);
            digit[1] = (char)Utils.Base64Digit(tmp);

            // now shift by just 6 so we get the high 18 bits (casted down
            // to the low order 8 bits by the cast
            digit[2] = (char)(bitmap >> 6);

            // we want just the low order 6 of these
            tmp = (byte)(digit[2] & SixBits);
            digit[2] = (char)Utils.Base64Digit(tmp);

            // for the lowest order 6 bits we don't have to shift we just
            // mask so only they are left
            tmp = (byte)(bitmap & SixBits);
            digit[3] = (char)Utils.Base64Digit(tmp);

            return new string(digit);
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append($"PointOfEntry = {PointOfEntryMetric.ToString()}\n");
            str.Append($"CommMode = {CommModeMetric.ToString()}\n");
            str.Append($"RuntimeIndex = {TheRuntimeIndex}\n");
            str.Append($"Service = {ServiceMetric.ToString()}\n");
            str.Append($"ServiceFormat = {ServiceFormatMetric.ToString()}\n");
            str.Append($"MessageOrigin = {MessageOriginMetric.ToString()}\n");
            str.Append($"OSIndex = {OSIndexMetric}\n");
            str.Append($"SDKVersionIndex = {SDKVersionIndexMetric}\n");
            str.Append($"SDKVersion = {SDKVersion}\n");
            str.Append($"Base64 = {ToBase64()}\n");

            return str.ToString();
        }

        /**
         * Extract the value from identity.xml
         *
         * @param key
         * @return
         */
        private int GetIdentityValue(XPathDocument identityDoc, string key)
        {
            var xpathStr = "//*[.='" + key + "']";
            var nodesIter = Utils.GetXPathNodes(identityDoc, xpathStr);
            // Found a match
            if (nodesIter == null)
            {
                return 0;
            }
            if (nodesIter.Count <= 0)
            {
                return 0;
            }
            xpathStr = "//" + GetParentNodeName(nodesIter) + "/value/text()";
            var retValue = Utils.GetXPathValue(identityDoc, xpathStr);
            return Utils.StringToInt(retValue);
            // No match in identity.xml
        }

        private static string GetParentNodeName(XPathNodeIterator currNodeList)
        {
            currNodeList.Current.MoveToParent();
            return currNodeList.Current.Name;
        }

        public static bool IsInternet(CommModule module)
        {
            switch (module)
            {
                case CommModule.TCPConnect:
                case CommModule.PNSConnect:
                case CommModule.TCPBatch:
                case CommModule.PNSUpload:
                    return false;
                case CommModule.HTTPSConnect:
                case CommModule.SFTPBatch:
                case CommModule.HTTPSUpload:
                    return true;
                case CommModule.Unknown:
                    return false;
            }

            return false;
        }

        public string GetOSName()
        {
            string osName = null;
            var osInfo = Environment.OSVersion;
            switch (osInfo.Platform)
            {
                case PlatformID.Win32Windows:
                {
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                        {
                            osName = "Windows 95";
                            break;
                        }
                        case 10:
                        {
                            osName = "Windows 98";
                            break;
                        }
                        case 90:
                        {
                            osName = "Windows Me";
                            break;
                        }
                    }
                    break;
                }
                case PlatformID.Win32NT:
                {
                    switch (osInfo.Version.Major)
                    {
                        case 3:
                        {
                            osName = "Windows NT 3.51";
                            break;
                        }
                        case 4:
                        {
                            osName = "Windows NT 4.0";
                            break;
                        }
                        case 5:
                        {
                            if (osInfo.Version.Minor == 0)
                            {
                                osName = "Windows 2000";
                            }
                            else if (osInfo.Version.Minor == 1)
                            {
                                osName = "Windows XP";
                            }
                            else if (osInfo.Version.Minor == 2)
                            {
                                osName = "Windows Server 2003";
                            }
                            break;
                        }
                        case 6:
                        {
                            if (osInfo.Version.Minor == 0)
                            {
                                osName = "Windows Vista";
                            }
                            else if (osInfo.Version.Minor == 1)
                            {
                                osName = "Windows 7";
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return osName;
        }
    }
}
