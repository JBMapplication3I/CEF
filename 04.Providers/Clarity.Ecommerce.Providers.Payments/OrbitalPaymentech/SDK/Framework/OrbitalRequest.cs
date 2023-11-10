#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Text;
    using JPMC.MSDK.Common;
    using JPMC.MSDK.Configurator;
    using JPMC.MSDK.Converter;

    public class OrbitalRequest : RequestBase, IRequestImpl
    {
        private KeySafeDictionary<string, string> fields = new KeySafeDictionary<string, string>();
        private KeySafeDictionary<string, List<string>> arrays = new KeySafeDictionary<string, List<string>>();

        public OrbitalRequest(string transactionType, CommModule module)
            : this(transactionType, Enum.GetName(typeof(CommModule), module), new DispatcherFactory())
        {
        }

        public OrbitalRequest(string transactionType, string configName) : this(transactionType, configName, new DispatcherFactory())
        {
        }

        public OrbitalRequest(string transactionType, string configName, IDispatcherFactory factory)
        {
            this.factory = factory;
            try
            {
                Config = factory.Config.GetProtocolConfig(configName);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new RequestException(Error.InitializationFailure, e.Message, e);
            }

            Initialize(transactionType, Config, factory);
        }

        public OrbitalRequest(string transactionType, ConfigurationData configData, IDispatcherFactory factory)
        {
            Initialize(transactionType, configData, factory);
        }

        public OrbitalRequest(OrbitalRequest copy)
        {
            this.TransactionType = copy.TransactionType;
            this.arrays = copy.arrays;
            this.Config = copy.Config;
            this.controlValues = copy.controlValues;
            this.factory = copy.factory;
            this.fields = new KeySafeDictionary<string, string>(copy.fields);
            this.fileMgr = copy.fileMgr;
            this.Metrics = copy.Metrics;
            this.schema = copy.schema;
        }

        private void Initialize(string transactionType, ConfigurationData configData, IDispatcherFactory factory)
        {
            this.TransactionType = transactionType;
            this.Config = configData;
            this.factory = factory;

            try
            {
                schema = factory.GetOrbitalSchema();
            }
            catch (ConverterException e)
            {
                Logger.Error("Failed to load schema.", e);
                throw new RequestException(Error.InitializationFailure, "Failed to load schema.", e);
            }

            SetDefaultValues();

            Metrics = new SDKMetrics(SDKMetrics.Service.Dispatcher, SDKMetrics.ServiceFormat.API);
            Metrics.MessageOriginMetric = SDKMetrics.MessageOrigin.Local;
            Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Orbital;
            Metrics.CommModeMetric = SDKMetrics.CommMode.Internet;
        }


        public override string GetField(string fieldName)
        {
            if (fieldName.Contains("["))
            {
                var index = GetArrayIndex(fieldName);
                var name = GetArrayFieldName(fieldName);
                if (!arrays.ContainsKey(name) || index < 0)
                {
                    return null;
                }
                var values = arrays[name];
                if (values.Count <= index)
                {
                    return null;
                }

                return values[index];
            }

            if (fields.ContainsKey(fieldName))
            {
                return fields[fieldName];
            }

            return null;
        }

        private int GetArrayIndex(string fieldName)
        {
            var start = fieldName.IndexOf("[");
            var end = fieldName.IndexOf("]");
            if (end == -1)
            {
                var msg = "An array field was referenced without a closing bracket.";
                Logger.Error(msg);
                throw new RequestException(Error.InvalidField, msg);
            }

            var value = fieldName.Substring(start + 1, end - start - 1);
            return Utils.StringToInt(value);
        }

        private string GetArrayFieldName(string fieldName)
        {
            var start = fieldName.IndexOf("[");
            var end = fieldName.IndexOf("]");
            var value = fieldName.Substring(0, start) + fieldName.Substring(end + 1);
            return value;
        }

        public string GetMaskedField(string fieldName)
        {
            var field = schema.GetField(TransactionType, fieldName);

            FieldMasks masks = null;
            try
            {
                masks = factory.Config.FieldMasks;
            }
            catch (DispatcherException e)
            {
                Logger.Error("Failed to get Field Mask list.", e);
                throw new RequestException(e.ErrorCode, "Failed to get Field Mask list.", e);
            }

            var value = fields[fieldName];
            value = masks.MaskValue(fieldName, value, TransactionType);
            return value;
        }


        public byte[] GetBinaryField(string fieldName)
        {
            var value = GetField(fieldName);
            return value != null ? Utils.StringToByteArray(value) : new byte[0];

        }


        public string XML => ConvertRequest().Request;

        public string MaskedXML => ConvertRequest().MaskedRequest;

        private ConverterArgs ConvertRequest()
        {
            try
            {
                IOnlineConverter converter = null;
                converter = factory.MakeOnlineConverter(Config, MessageFormat);
                var args = converter.ConvertRequest(this);
                return args;
            }
            catch (ConverterException e)
            {
                Logger.Error("Failed to generate XML.", e);
                throw new RequestException(e.ErrorCode, "Failed to generate XML.", e);
            }
        }

        public SystemType GetProcessingSystem()
        {
            return SystemType.ORB;
        }

        /// <summary>
        /// This indexer gives a convenient interface into the GetField and
        /// SetField methods.
        /// </summary>
        /// <remarks>
        /// This indexer only returns string values, so you will still need
        /// GetIntField and GetLongField (and their appropriate Set methods).
        /// </remarks>
        public string this[string fieldName]
        {
            get => GetField(fieldName);
            set => SetField(fieldName, value);
        }

        public override void SetField(string fieldName, string value)

        {
            // getField tests if the field exists.
            if (!schema.HasField(TransactionType, fieldName))
            {
                // Throws appropriate exception.
                schema.GetField(TransactionType, fieldName);
            }

            var escapedValue = SecurityElement.Escape(value);

            AddAllArrayFields(fieldName, escapedValue);

            fields[fieldName] = escapedValue;
        }

        /**
         * If one field in an array format is set, then we must make
         * sure all the fields for that format are set for that array index,
         * at least set to null.
         * @param fieldName
         */
        private void AddAllArrayFields(string fieldName, string value)
        {
            if (!fieldName.Contains("["))
            {
                return;
            }

            var strippedName = Utils.StripIndexer(fieldName);
            var start = fieldName.IndexOf("[");
            var end = fieldName.IndexOf("]", start);
            var len = end - start + 1;
            var indexer = fieldName.Substring(fieldName.IndexOf("["), len);
            var numStr = indexer.Remove(indexer.Length - 1, 1);
            numStr = numStr.Remove(0, 1);
            var index = Convert.ToInt32(numStr);
            var formatName = fieldName.Substring(0, fieldName.IndexOf("["));

            AddArrayFieldValue(strippedName, index, value);

            List<FieldData> fieldData = null;
            try
            {
                fieldData = schema.GetFieldsForFormat(TransactionType);
            }
            catch (ConverterException)
            {
                return;
            }

            foreach (var field in fieldData)
            {
                if (!field.name.StartsWith(formatName))
                {
                    continue;
                }

                var nameOnly = field.name.Substring(field.name.LastIndexOf("."));
                if (!field.name.Equals(strippedName))
                {
                    var newName = formatName + indexer + nameOnly;
                    if (!HasField(newName))
                    {
                        var val = fields[newName];
                        fields[newName] = val;
                    }
                }
            }

        }

        private void AddArrayFieldValue(string fieldName, int arrayIndex, string value)
        {
            List<string> valueList = null;
            if (arrays.ContainsKey(fieldName))
            {
                valueList = arrays[fieldName];
            }
            else
            {
                valueList = new List<string>();
                arrays[fieldName] = valueList;
            }

            try
            {
                if (arrayIndex == valueList.Count)
                {
                    valueList.Add(value);
                }
                else if (arrayIndex < valueList.Count)
                {
                    valueList[arrayIndex] = value;
                }
                else
                {
                    for (var i = valueList.Count; i < arrayIndex; i++)
                    {
                        valueList.Add(null);
                    }
                    valueList.Add(value);
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("The index is out of bounds.");
                throw new RequestException(Error.InvalidOperation, "The index is out of bounds.", ex);
            }
        }


        public void SetField(string fieldName, int arrayIndex, string value)
        {
            List<string> valueList = null;
            if (arrays.ContainsKey(fieldName))
            {
                valueList = arrays[fieldName];
            }
            else
            {
                valueList = new List<string>();
                arrays[fieldName] = valueList;
            }

            try
            {
                if (arrayIndex == valueList.Count)
                {
                    valueList.Add(value);
                }
                else if (arrayIndex < valueList.Count)
                {
                    valueList[arrayIndex] = value;
                }
                else
                {
                    for (var i = valueList.Count; i < arrayIndex; i++)
                    {
                        valueList.Add(null);
                    }
                    valueList.Add(value);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("The index is out of bounds.", ex);
                throw new RequestException(Error.InvalidOperation, "The index is out of bounds.", ex);
            }
        }


        public void SetField(string fieldName, byte[] value)
        {
            Logger.Error("The method SetField() is not valid for this transaction type.");
            throw new RequestException(Error.InvalidOperation, "The method SetField() is not valid for this transaction type.");
        }


        public void SetField(string fieldName, int value)
        {
            SetField(fieldName, value.ToString());
        }


        public void SetField(string fieldName, long value)
        {
            SetField(fieldName, value.ToString());
        }

        public IRequest GetAdditionalFormat(string formatType)
        {
            Logger.Error("The method GetAdditionalFormat() is not valid for this transaction type.");
            throw new RequestException(Error.InvalidOperation, "The method GetAdditionalFormat() is not valid for this transaction type.");
        }

        public IRequest GetAdditionalFormat(string formatType, bool allowIterative)
        {
            Logger.Error("The method GetAdditionalFormat() is not valid for this transaction type.");
            throw new RequestException(Error.InvalidOperation, "The method GetAdditionalFormat() is not valid for this transaction type.");
        }

        public IRequest GetIterativeFormat(string formatType)
        {
            Logger.Error("The method GetIterativeFormat() is not valid for this transaction type.");
            throw new RequestException(Error.InvalidOperation, "The method GetIterativeFormat() is not valid for this transaction type.");
        }

        public string DumpFieldValues()
        {
            return DumpFieldValues(false);
        }


        public string DumpMaskedFieldValues()
        {
            return DumpFieldValues(true);
        }


        public string DumpFieldValues(bool masked)
        {
            FieldMasks masks = null;
            try
            {
                masks = factory.Config.FieldMasks;
            }
            catch (DispatcherException)
            {
                masked = false;
            }

            var buff = new StringBuilder();

            var keys = new List<string>(fields.Keys);
            keys = OrganizeFieldNames(keys);

            foreach (var field in keys)
            {
                // This field is covered by an array, so skip it.
                if (arrays.ContainsKey(field))
                {
                    continue;
                }
                var value = fields[field] != null ? fields[field] : "";
                value = masked ? masks.MaskValue(field, value, TransactionType) : value;
                var line = field + " = " + value;
                buff.AppendLine(line);
            }

            foreach (var arrayKey in arrays.Keys)
            {
                var values = arrays[arrayKey];
                for (var i = 0; i < values.Count; i++)
                {
                    var value = values[i];
                    value = masked ? masks.MaskValue(arrayKey, value, TransactionType) : value;
                    var line = arrayKey + "[" + i + "] = " + value;
                    buff.AppendLine(line);
                }
            }

            return buff.ToString();
        }


        public bool HasField(string name)
        {
            return schema.HasField(TransactionType, name);
        }


        public bool IsBatch => false;

        public CommModule Module => Config.Protocol;

        public MessageFormat MessageType => MessageFormat.ORB;

        public TransactionControlValues TransactionControlValues
        {
            get
            {
                // If the TraceValue has been set by the user,
                // MerchantID must also be added to the control values.
                // It must be done here, since MerchantID can be set
                // after TraceNumber.
                if (controlValues["TraceNumber"] != null)
                {
                    try
                    {
                        controlValues["MerchantID"] = GetField("MerchantID");
                    }
                    catch { }
                }
                return controlValues;
            }
        }

        public void ReplaceTokensInAttributes(string attribName,
                string tokenValue, string tokenName)
        {
            Logger.Debug("The method replaceTokensInAttributes() is not valid for this transaction type.");
        }


        public bool IsVersionChanged
        {
            get
            {
                Logger.Debug("The method isVersionChanged() is not valid for this transaction type.");
                return false;
            }
        }

        public List<string> GetArrayValues(string fieldName)
        {
            if (!arrays.ContainsKey(fieldName))
            {
                return new List<string>();
            }

            return arrays[fieldName];
        }

        public void SetControlField(string name, string value)
        {
            controlValues[name] = value;
        }

        /**
         * Produces a List of field names in the order they
         * have in the schema.
         *
         * The HashMap that contains all the fields does not
         * guarantee the order in which they are read. In fact,
         * they are usually read in the wrong order. The
         * dumpFieldValues() method needs the fields in the
         * order in which they were added, which corresponds to
         * their order in the XML schema.
         *
         * @param keys
         * @return
         */
        private List<string> OrganizeFieldNames(List<string> keys)
        {
            try
            {
                var fields = schema.GetFieldsForFormat(TransactionType);
                var organizedKeys = new List<string>();

                foreach (var field in fields)
                {
                    organizedKeys.Add(field.name);
                }

                // This adds keys with indexers and removes
                // the array key names without the indexers.
                foreach (var key in keys)
                {
                    var keyName = Utils.StripIndexer(key);
                    if (key.Contains("["))
                    {
                        if (organizedKeys.Contains(keyName))
                        {
                            organizedKeys.Remove(keyName);
                        }
                    }
                    if (!organizedKeys.Contains(key))
                    {
                        organizedKeys.Add(key);
                    }
                }

                organizedKeys.Sort();

                return organizedKeys;
            }
            catch (ConverterException)
            {
                return keys;
            }
        }

        #region Unimplemented Impl Methods


        public string RawData => null;

        public int GetIntField(string fieldName)
        {
            var textValue = GetField(fieldName);
            return Utils.StringToInt(textValue);
        }

        public long GetLongField(string fieldName)
        {
            var textValue = GetField(fieldName);
            try
            {
                return Convert.ToInt64(textValue);
            }
            catch (Exception ex)
            {
                var msg = $"The value \"{textValue}\" is not a valid integer.";
                Logger.Error(msg, ex);
                throw new RequestException(Error.FieldNotNumeric, msg, ex);
            }
        }

        public string Host
        {
            get => null;
            set
            {
            }
        }

        public string Port
        {
            get => null;
            set
            {
            }
        }

        public void ClearField(string fieldName)
        {
            this[fieldName] = null;
        }

        public void ClearAllFields()
        {
            var keys = new List<string>(fields.Keys);
            foreach (var key in keys)
            {
                this[key] = null;
            }
        }

        public string DumpAllFieldValues()
        {
            return DumpFieldValues();
        }

        public string GetField(string fieldName, int index)
        {
            var msg = "The method GetField(string, int) is not valid for this transaction type.";
            Logger.Debug(msg);
            throw new RequestException(Error.InvalidOperation, msg);
        }

        public Format GetFormat(string name)
        {
            throw new NotImplementedException();
        }

        public bool UsesFormat(string usedName)
        {
            throw new NotImplementedException();
        }

        public void SetField(string fieldName, string value, bool setHidden)
        {
            throw new NotImplementedException();
        }

        public void SetSkipDuplicateCheck(bool skip)
        {
            throw new NotImplementedException();
        }

        public MessageFormat MessageFormat => Common.MessageFormat.ORB;
        #endregion
    }
}