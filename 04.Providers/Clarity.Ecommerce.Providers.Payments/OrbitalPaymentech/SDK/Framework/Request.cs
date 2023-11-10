#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using Common;
    using Configurator;
    using Converter;

    /**
     * <p>
     * Title:
     * </p>
     *
     * <p>
     * Description: Provides a simple API for accessing the fields of a transaction
     * request. Use the convenient getters and setters to access each field : the
     * request.
     * </p>
     *
     * <p> (c)2017, Paymentech, LLC. All rights reserved</p>
     *
     * <p>Company: J. P. Morgan</p>
     *
     * @author not attributable
     * @version 1.0
     */
    public class Request : RequestBase, IRequestImpl
    {
        private SafeDictionary<string, string> fields = new SafeDictionary<string, string>();
        private SafeDictionary<string, byte[]> binaryFields = new SafeDictionary<string, byte[]>();
        private RequestTemplate template;
        private bool skipDuplicates;

        public string Host { get; set; }
        public string Port { get; set; }

        public Request(string transactionType, ConfigurationData configData, IDispatcherFactory factory)
        {
            this.factory = factory;
            this.Config = configData;
            this.TransactionType = transactionType;

            Initialize();
        }

        public Request(Request copy)
        {
            this.binaryFields = new SafeDictionary<string, byte[]>(copy.binaryFields);
            this.Config = copy.Config;
            this.controlValues = copy.controlValues;
            this.factory = copy.factory;
            this.fields = new SafeDictionary<string, string>(copy.fields);
            this.Host = copy.Host;
            this.Metrics = copy.Metrics;
            this.Port = copy.Port;
            this.template = copy.template;
            this.TransactionType = copy.TransactionType;
        }

        private void Initialize()
        {
            Metrics = new SDKMetrics(SDKMetrics.Service.Dispatcher, SDKMetrics.ServiceFormat.API)
            {
                MessageOriginMetric = SDKMetrics.MessageOrigin.Local
            };

            var reqType = RequestType.Online;
            switch (Config.Protocol)
            {
                case CommModule.TCPConnect:
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Stratus;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Intranet;
                    reqType = RequestType.Online;
                    break;
                case CommModule.HTTPSConnect:
                    reqType = Config.MessageFormat == "PNS" ? RequestType.PNSOnline : RequestType.Online;
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.NetConnect;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Internet;
                    break;
                case CommModule.HTTPSUpload:
                    reqType = RequestType.PNSOnline;
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.NetConnect;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Internet;
                    break;
                case CommModule.PNSConnect:
                case CommModule.PNSUpload:
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Tandem;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Intranet;
                    reqType = RequestType.PNSOnline;
                    break;
                case CommModule.TCPBatch:
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Stratus;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Intranet;
                    reqType = RequestType.Batch;
                    break;
                case CommModule.SFTPBatch:
                    Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.NetConnect;
                    Metrics.CommModeMetric = SDKMetrics.CommMode.Internet;
                    reqType = RequestType.Batch;
                    break;
                default:
                    break;
            }

            try
            {
                Logger.Debug("Getting definitions for Request Type \"" + reqType + "\".");
                template = factory.GetRequestTemplate(reqType);
            }
            catch (DispatcherException e)
            {
                Logger.Error("Failed to load the template.", e);
                throw new RequestException(Error.InitializationFailure, "Failed to load the template.", e);
            }

            var format = this.GetFormat(TransactionType);
            if (format == null || format.IsEmpty)
            {
                var msg = $"The Transaction Type \"{TransactionType}\" does not exist.";
                Logger.Error(msg);
                throw new RequestException(Error.FormatNotFoundError, msg);
            }
        }


        public override void SetField(string fieldName, string value)
        {
            SetField(fieldName, value, false);
        }
        public void SetField(string fieldName, string value, bool setHidden)
        {
            var realFieldName = ProcessFieldForSetting(fieldName, value, setHidden);
            fields[realFieldName] = value;
        }

        private string ProcessFieldForSetting(string fname, string value, bool setHidden)
        {
            TestForNonASCIICharacters(value);

            if (fname == null)
            {
                var msg = "The field name is null.";
                Logger.Error(msg);
                throw new RequestException(Error.NullFieldName, msg);
            }

            var fieldName = getFieldIDFromName(fname);

            if (fields.ContainsKey(fieldName))
            {
                return fieldName;
            }

            var field = GetFieldDef(fieldName);
            if (field == null)
            {
                return fieldName;
            }

            if (!setHidden && field.Hide)
            {
                var msg = "The field \"" + fieldName + "\" cannot be modified by the user. It is set automatically.";
                Logger.Error(msg);
                throw new RequestException(Error.FieldHidden, msg);
            }

            var arrayIndex = "";

            if (fieldName.IndexOf('[') == -1 && field.ParentFormat != null && field.ParentFormat.IsArray)
            {
                var pos = fieldName.LastIndexOf('.');
                fieldName = $"{fieldName.Substring(0, pos)}[{0}]{fieldName.Substring(pos)}";
            }

            if (fieldName.IndexOf('[') > -1)
            {
                TestArrayBounds(fieldName, field);
                HandleSequence(fieldName, field);
                IncrementCountField(fieldName, field);
                var pos = fieldName.IndexOf('[');
                var len = fieldName.IndexOf(']') + 1 - pos;
                arrayIndex = fieldName.Substring(pos, len);
            }

            try
            {
                if (field.FieldType != DataType.Binary && value != null && value.Length > field.Length)
                {
                    var msg =
                        $"The value of field \"{fieldName}\" is too long. Max length = {field.Length}, value length = {value.Length}";
                    Logger.Error(msg);
                    throw new RequestException(Error.FieldTooLong, msg);
                }

                var realFieldName = InsertArrayIndex(field.FieldID, arrayIndex);
                if (realFieldName.StartsWith("."))
                {
                    realFieldName = realFieldName.Substring(1);
                }
                return realFieldName;
            }
            catch (ConverterException e)
            {
                throw new RequestException(Error.InvalidField, "Problem retrieving the field.", e);
            }
        }

        private string InsertArrayIndex(string fieldName, string arrayIndex)
        {
            var format = FindArrayFormat(fieldName);

            if (format == null)
            {
                return fieldName;
            }

            var pos = format.FormatID.Length;
            return fieldName.Substring(0, pos) + arrayIndex + fieldName.Substring(pos);
        }

        private Format FindArrayFormat(string fieldName)
        {
            try
            {
                var format = template.GetFormat(FieldIdentifier.GetFormatPath(fieldName), TransactionType);
                while (format != null && !format.IsArray)
                {
                    format = format.Parent;
                }

                return format;
            }
            catch (ConverterException)
            {
            }

            return null;
        }

        private void IncrementCountField(string fieldName, Field field)
        {
            Format format = null;
            var name = fieldName;
            var index = FieldIdentifier.GetArrayIndex(fieldName);

            try
            {
                format = template.GetFormat(fieldName.Substring(0, fieldName.IndexOf('[')), TransactionType);
                while (format.Parent != null && !format.Parent.IsEmpty)
                {
                    name = FieldIdentifier.GetFormatPath(name);
                    if (format.CountField != null)
                    {
                        //string countField = format.getCountField();
                        name = FieldIdentifier.GetFormatPath(name);
                        name = name + "." + format.CountField;
                        var count = GetIntField(name);
                        if (count > index)
                        {
                            return;
                        }

                        SetField(name, count + 1);
                        return;
                    }
                    format = format.Parent;
                }
            }
            catch (ConverterException)
            {
            }
        }

        private void HandleSequence(string fieldName, Field field)
        {
            var index = FieldIdentifier.GetArrayIndex(fieldName);
            var formatName = Utils.StripArrayIndex(fieldName);

            Format format = null;
            try
            {
                format = GetFormat(TransactionType + "." + formatName);
                foreach (var child in format.Fields)
                {
                    if (child == field)
                    {
                        continue;
                    }
                    if (child.Start != null)
                    {
                        var name = FieldIdentifier.GetFormatPath(fieldName);
                        name += "." + child.Name;
                        if (index == 0)
                        {
                            SetField(name, child.Start, true);
                        }
                        else
                        {
                            var nextVal = Utils.StringToInt(child.Next, -1);

                            SetField(name, Convert.ToString(nextVal + (index - 1)), true);
                        }
                    }
                    if (child.MatchParentField != null)
                    {
                        var parentField = $"{format.Parent.FormatID}[{index}].{child.MatchParentField}";
                        var name = FieldIdentifier.GetFormatPath(fieldName) + "." + child.Name;
                        SetField(name, GetField(parentField), true);
                    }
                }
            }
            catch (ConverterException)
            {
            }
        }

        private void TestArrayBounds(string fieldName, Field field)
        {
            var index = FieldIdentifier.GetArrayIndex(fieldName);

            Format format = null;
            try
            {
                var name = fieldName.Substring(0, fieldName.IndexOf('['));
                name = !name.StartsWith(TransactionType) ? TransactionType + "." + name : name;
                format = template.GetFormat(name, TransactionType);
                if (index >= format.Max)
                {
                    var msg = $"The maximum number of \"{format.Name}\" formats has been exceeded.";
                    Logger.Debug(msg);
                    throw new RequestException(Error.MaxFormatsExceeded, msg);
                }
            }
            catch (ConverterException)
            {
            }
        }


        public void SetField(string fieldName, int arrayIndex, string value)
        {
            if (!fieldName.Contains("."))
            {
                var msg = "The field \"" + fieldName + "\" cannot be used as an array.";
                Logger.Error(msg);
                throw new RequestException(Error.InvalidField, msg);
            }

            var pos = fieldName.LastIndexOf(".");
            var name = $"{fieldName.Substring(0, pos)}[{arrayIndex}]{fieldName.Substring(pos)}";

            SetField(name, value);
        }


        public void SetField(string fieldName, byte[] value)

        {
            var field = GetFieldDef(fieldName);

            if (value.Length > field.Length)
            {
                var msg =
                    $"The value of field \"{fieldName}\" is too long. Max length = {field.Length}, value length = {value.Length}";
                Logger.Error(msg);
                throw new RequestException(Error.FieldTooLong, msg);
            }

            var realFieldName = this.ProcessFieldForSetting(fieldName, "<BINARY VALUE>", false);
            fields[realFieldName] = null;
            binaryFields[realFieldName] = value;
        }


        public void SetField(string fieldName, int value)
        {
            SetField(fieldName, Convert.ToString(value));
        }

        public void SetField(string fieldName, long value)
        {
            SetField(fieldName, Convert.ToString(value));
        }

        public string DumpFieldValues()
        {
            return DumpFieldValues(false, false);
        }

        public string DumpFieldValues(bool isMasked)
        {
            return DumpFieldValues(isMasked, false);
        }

        //
        public string DumpAllFieldValues()
        {
            return DumpFieldValues(false, true);
        }


        public string DumpMaskedFieldValues()
        {
            return DumpFieldValues(true, false);
        }

        private void BuildFieldLists(List<string> fieldNames, List<string> fieldIDs, bool includeUnsetFields)
        {
            var formatNames = new List<string>();

            foreach (var name in fields.Keys)
            {
                if (name.Contains("."))
                {
                    fieldIDs.Add(name);
                    var formatName = FieldIdentifier.GetFormatPath(name);
                    if (!formatNames.Contains(formatName))
                    {
                        formatNames.Add(formatName);
                    }
                }
                else
                {
                    fieldNames.Add(name);
                    if (!formatNames.Contains(TransactionType))
                    {
                        formatNames.Add(TransactionType);
                    }
                }
            }

            if (includeUnsetFields)
            {
                AddSubFormatFields(fieldIDs, fieldNames, formatNames);
            }

        }

        private void AddSubFormatFields(List<string> fieldIDs, List<string> fieldNames, List<string> formatNames)
        {
            foreach (var formatName in formatNames)
            {
                Format format = null;

                try
                {
                    format = GetFormat(Utils.StripArrayIndex(formatName));
                }
                catch (RequestException)
                {
                }
                if (format == null || format.IsEmpty)
                {
                    continue;
                }

                foreach (var field in format.Fields)
                {
                    if (field.Hide)
                    {
                        continue;
                    }
                    try
                    {
                        if (!fieldNames.Contains(field.Name) && !fieldIDs.Contains(field.FieldID))
                        {
                            if (formatName == TransactionType)
                            {
                                fieldNames.Add(field.Name);
                            }
                            else
                            {
                                var name = formatName + "." + field.Name;
                                if (!fieldIDs.Contains(name))
                                {
                                    fieldIDs.Add(name);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }


                }
            }
        }

        private string DumpFieldValues(bool masked, bool includeAllFields)
        {
            var fieldNames = new List<string>();
            var fieldIDs = new List<string>();
            BuildFieldLists(fieldNames, fieldIDs, includeAllFields);
            fieldNames.Sort();
            fieldIDs.Sort();

            var text = new StringBuilder();

            foreach (var fieldName in fieldNames)
            {
                AddToDump(fieldName, text, masked);
            }
            foreach (var fieldName in fieldIDs)
            {
                AddToDump(fieldName, text, masked);
            }

            return text.ToString();
        }

        private void AddToDump(string fieldName, StringBuilder text, bool masked)
        {
            var field = GetSafeFieldDef(fieldName);

            var value = fields[fieldName];
            if (value == null)
            {
                value = "";
            }

            value = MaskFieldValue(masked, field, value);

            text.AppendLine(fieldName + " = " + value);
        }


        private string MaskFieldValue(bool masked, Field field, string value)
        {
            if (masked && field.HasMask)
            {
                if (field.MaskLength < value.Length)
                {
                    if (field.MaskJustification == TextJustification.Left)
                    {
                        value = value.Remove(0, field.MaskLength);
                        var pad = new string(field.MaskWith[0], field.MaskLength);
                        value = pad + value;
                    }
                    else
                    {
                        value = value.Substring(0, value.Length - field.MaskLength);
                        var pad = new string(field.MaskWith[0], field.MaskLength);
                        value = value + pad;
                    }
                }
                else
                {
                    value = new string(field.MaskWith[0], field.MaskLength);
                }
            }
            return value;
        }


        public string XML => GetXML(false);

        public string MaskedXML => GetXML(true);

        private string GetXML(bool masked)
        {
            var fieldNames = new List<string>();
            var fieldIDs = new List<string>();
            BuildFieldLists(fieldNames, fieldIDs, false);
            fieldNames.Sort();
            fieldIDs.Sort();
            fieldNames.AddRange(fieldIDs);

            var baseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><Request>\n</Request>";
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(baseXml);
            }
            catch (Exception e)
            {
                throw new RequestException(Error.InitializationFailure, "Failed to create XML document.", e);
            }

            foreach (var name in fieldNames)
            {
                var field = GetSafeFieldDef(name);
                var value = fields[name];
                value = MaskFieldValue(masked, field, value);
                AddXmlNode(name, doc.DocumentElement, doc, value);
            }

            return Utils.ConvertXMLToString(doc);
        }

        private XmlElement FindXmlNode(string name, XmlElement parent, int index)
        {
            for (var i = 0; i < parent.ChildNodes.Count; i++)
            {
                var node = parent.ChildNodes.Item(i);
                if (node.Name == name)
                {
                    if (index == -1)
                    {
                        return (XmlElement)node;
                    }

                    var arrayIndex = Utils.GetAttributeValue("index", "-1", node);

                    if (Utils.StringToInt(arrayIndex, -1) == index)
                    {
                        return (XmlElement)node;
                    }
                }
            }

            return null;
        }

        private void AddXmlNode(string name, XmlElement parentNode, XmlDocument doc, string value)
        {
            var parts = name.Split('.');

            if (!name.Contains("."))
            {
                var node = doc.CreateElement(name);
                parentNode.AppendChild(node);
                if (value != null)
                {
                    var textNode = doc.CreateTextNode(value);
                    node.AppendChild(textNode);
                }
            }
            else
            {
                var field = parts[0];
                var index = -1;
                if (field.Contains("["))
                {
                    var start = field.IndexOf('[') + 1;
                    var len = field.IndexOf(']') - start;
                    var arrayIndex = field.Substring(start, len);
                    index = Utils.StringToInt(arrayIndex, -1);
                    field = field.Substring(0, field.IndexOf('['));
                }

                var node = FindXmlNode(field, parentNode, index);
                if (node == null)
                {
                    node = doc.CreateElement(field);
                    parentNode.AppendChild(node);
                    if (index != -1)
                    {
                        node.SetAttribute("index", index.ToString());
                    }
                }

                AddXmlNode(name.Substring(name.IndexOf(".") + 1), node, doc, value);
            }
        }


        public void SetControlField(string name, string value)
        {
            TransactionControlValues[name] = value;
        }


        public override string GetField(string fieldName)
        {
            return GetField(fieldName, false);
        }

        public string this[string fieldName]
        {
            get => GetField(fieldName);
            set => SetField(fieldName, value);
        }


        public string GetField(string fieldName, int index)
        {
            var arrayIndex = $"[{index}]";
            var name = fieldName.Substring(0, fieldName.LastIndexOf('.')) + arrayIndex + fieldName.Substring(fieldName.LastIndexOf('.'));
            return GetField(name, false);
        }


        public string GetMaskedField(string fieldName)
        {
            return GetField(fieldName, true);
        }

        private string GetField(string fname, bool masked)
        {
            var fieldName = getFieldIDFromName(fname);

            string value = null;
            var field = GetFieldDef(fieldName);

            var realFieldName = fieldName;

            try
            {
                if (field != null && fieldName.Contains("["))
                {
                    var pos = fieldName.IndexOf('[');
                    var len = fieldName.IndexOf(']') + 1 - pos;
                    var arrayIndex = fieldName.Substring(pos, len);
                    realFieldName = InsertArrayIndex(field.FieldID, arrayIndex);
                }
            }
            catch (ConverterException e)
            {
                throw new RequestException(Error.FieldDoesNotExist, "Field [" + fieldName + "] not found.", e);
            }

            var tempName = FieldIdentifier.GetFormatPath(fieldName) + "[0]." + FieldIdentifier.GetName(fieldName);

            if (fields.ContainsKey(realFieldName))
            {
                value = fields[realFieldName];
                value = MaskFieldValue(masked, field, value);
                return value;
            }
            else if (fields.ContainsKey(fieldName))
            {
                value = fields[realFieldName];
                value = MaskFieldValue(masked, field, value);
                return value;
            }
            else if (fields.ContainsKey(tempName))
            {
                value = fields[tempName];
                value = MaskFieldValue(masked, field, value);
                return value;
            }
            else
            {
                try
                {
                    if (fields.ContainsKey(field.FieldID))
                    {
                        value = fields[field.FieldID];
                        value = MaskFieldValue(masked, field, value);
                        return value;
                    }
                }
                catch (ConverterException)
                {
                    // Do not handle this exception. The method just failed to find the value.
                }
            }

            return null;
        }

        private string getFieldIDFromName(string fieldName)
        {
            if (this.skipDuplicates)

            {
                return fieldName;
            }


            var numFields = this.template.GetFieldNameCount(TransactionType, fieldName);

            // If the fieldName is not a full field identifier and exists in the transaction type's format,
            // just return it, even if there are other fields with that name.
            if (numFields > 0 && !fieldName.Contains("."))
            {
                var name = this.template.GetFieldName(this.TransactionType, fieldName, 0);
                if (name == fieldName || name == this.TransactionType + "." + fieldName)
                {
                    return name;
                }
            }

            if (!fieldName.Contains(".") && numFields > 1)
            {
                var msg =
                    $"There are {numFields} fields with the name \"{fieldName}\". You must specify the Field Identifier for this field.";
                Logger.Error(msg);

                throw new RequestException(Error.InvalidField, msg);
            }

            if (!fieldName.Contains(".") && numFields == 1)
            {
                return this.template.GetFieldName(this.TransactionType, fieldName, 0);
            }

            return fieldName;
        }


        public byte[] GetBinaryField(string fieldName)
        {
            if (binaryFields.ContainsKey(fieldName))
            {
                return binaryFields[fieldName];
            }

            GetFieldDef(fieldName);

            return null;
        }

        public TransactionControlValues TransactionControlValues => controlValues;

        private Field GetSafeFieldDef(string fieldName)
        {
            Field field = null;
            try
            {
                field = GetFieldDef(fieldName);
            }
            catch
            {
            }

            return field;
        }

        public bool UsesFormat(string usedName)
        {
            var name = usedName;

            if (name.Length == 0)
            {
                return false;
            }

            var formatName = name;

            foreach (var field in fields.Keys)
            {
                if (field.StartsWith(formatName + ".") || field.StartsWith(formatName + '['))
                {
                    return true;
                }
            }

            return false;
        }

        public Format GetFormat(string name)
        {
            try
            {
                return template.GetFormat(name, TransactionType);
            }
            catch (ConverterException e)
            {
                var errorMsg = "The field \"" + name + "\" could not be found.";
                Logger.Error(errorMsg, e);
                throw new RequestException(Error.FieldDoesNotExist, errorMsg, e);
            }
        }

        private Field GetFieldDef(string fieldName)
        {
            var errorMsg = "The field \"" + fieldName + "\" could not be found.";
            Field field = null;

            var name = fieldName;
            if (name.Contains("["))
            {
                name = name.Substring(0, name.IndexOf('[')) + name.Substring(name.IndexOf(']') + 1);
            }

            var formatName = TransactionType + "." + FieldIdentifier.GetFormatPath(name);
            if (formatName.EndsWith("."))
            {
                formatName += name;
            }

            try
            {
                var format = template.GetFormat(formatName, TransactionType);
                if (format.IsEmpty)
                {
                    Logger.Error(errorMsg);
                    throw new RequestException(Error.FieldDoesNotExist, errorMsg);
                }

                field = format.GetField(name);
                field.ParentFormat = format;
            }
            catch (ConverterException ex)
            {
                Logger.Error(errorMsg, ex);
                throw new RequestException(Error.FieldDoesNotExist, errorMsg, ex);
            }

            if (field.IsEmpty)
            {
                Logger.Error(errorMsg);
                throw new RequestException(Error.FieldDoesNotExist, errorMsg);
            }

            return field;
        }

        public bool HasField(string name)
        {
            return this.GetSafeFieldDef(name) != null;
        }

        public MessageFormat MessageFormat
        {
            get
            {
                try
                {
                    return (MessageFormat)Enum.Parse(typeof(MessageFormat), Config.MessageFormat);
                }
                catch (Exception)
                {
                    return MessageFormat.None;
                }
            }
        }

        public bool IsBatch => TransactionType.StartsWith("Submission");

        public long GetLongField(string fieldName)
        {
            var textValue = GetField(fieldName);
            try
            {
                if (IsEmpty(textValue))
                {
                    return 0;
                }
                return Convert.ToInt64(textValue);
            }
            catch (Exception ex)
            {
                var msg = $"The value \"{textValue}\" is not a valid long.";
                Logger.Error(msg, ex);
                throw new RequestException(Error.FieldNotNumeric, msg, ex);
            }
        }


        public int GetIntField(string fieldName)
        {
            var textValue = GetField(fieldName);
            try
            {
                if (IsEmpty(textValue))
                {
                    return 0;
                }
                return Convert.ToInt32(textValue);
            }
            catch (Exception ex)
            {
                var msg = $"The value \"{textValue}\" is not a valid integer.";
                Logger.Error(msg, ex);
                throw new RequestException(Error.FieldNotNumeric, msg, ex);
            }
        }


        public List<string> GetArrayValues(string fieldName)
        {
            var formatName = FieldIdentifier.GetFormatPath(fieldName);
            var count = 0;
            foreach (var key in fields.Keys)
            {
                if (key.StartsWith(formatName + '['))
                {
                    count++;
                }
            }

            var values = new List<string>();
            for (var i = 0; i < count; i++)
            {
                try
                {
                    values.Add(this.GetField(fieldName, i));
                }
                catch (RequestException)
                {
                    values.Add(null);
                }
            }
            return values;
        }

        public void ClearField(string fieldName)
        {
            if (fields.ContainsKey(fieldName))
            {
                fields.Remove(fieldName);
            }
        }

        public void ClearAllFields()
        {
            fields.Clear();
        }

        public void SetSkipDuplicateCheck(bool skip)
        {
            this.skipDuplicates = skip;
        }

        private void TestForNonASCIICharacters(string value)
        {
            if (value == null)
            {
                return;
            }

            if (value.Contains("\r") || value.Contains("\n"))
            {
                var msg = "The value you are setting contains a carriage return or linefeed character. These are not supported.";
                Logger.Error(msg);
                throw new RequestException(Error.InvalidField, msg);
            }

            if (value != null && Encoding.UTF8.GetByteCount(value) != value.Length)
            {
                var msg = "The value you are setting contains non-ASCII characters. Non-ASCII characters are not supported.";
                Logger.Error(msg);
                throw new RequestException(Error.InvalidField, msg);
            }

            // Checks for other non-ASCII encoding
            var bytes = Utils.StringToByteArray(value);
            foreach (var bt in bytes)
            {
                var i = bt & 0x80;
                if (i != 0)
                {
                    var msg = "The value you are setting contains non-ASCII characters. Non-ASCII characters are not supported.";
                    Logger.Error(msg);
                    throw new RequestException(Error.InvalidField, msg);
                }
            }
        }
    }
}
