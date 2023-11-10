namespace ServiceStack.Serialization
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Logging;
    using ServiceStack.Text.Common;
    using Text;
    using Text.Jsv;
    using Web;

    /// <summary>Serializer cache of delegates required to create a type from a string map (e.g. for REST urls)</summary>
    public class StringMapTypeDeserializer
    {
        private static ILog Log = LogManager.GetLogger(typeof(StringMapTypeDeserializer));

        internal class PropertySerializerEntry
        {
            public PropertySerializerEntry(SetMemberDelegate propertySetFn, ParseStringDelegate propertyParseStringFn)
            {
                PropertySetFn = propertySetFn;
                PropertyParseStringFn = propertyParseStringFn;
            }

            public SetMemberDelegate PropertySetFn;
            public ParseStringDelegate PropertyParseStringFn;
            public Type PropertyType;
        }

        private readonly Type type;
        private readonly Dictionary<string, PropertySerializerEntry> propertySetterMap
            = new(PclExport.Instance.InvariantComparerIgnoreCase);

        internal StringMapTypeDeserializer(Type type, ILog log) : this(type)
        {
            Log = log;
        }

        public ParseStringDelegate GetParseFn(Type propertyType)
        {
            // Don't JSV-decode string values for string properties
            return propertyType == typeof(string)
                ? s => s
                : JsvReader.GetParseFn(propertyType);
        }

        public StringMapTypeDeserializer(Type type)
        {
            this.type = type;
            foreach (var propertyInfo in type.GetSerializableProperties())
            {
                var propertySetFn = propertyInfo.CreateSetter();
                var propertyType = propertyInfo.PropertyType;
                var propertyParseStringFn = GetParseFn(propertyType);
                var propertySerializer = new PropertySerializerEntry(propertySetFn, propertyParseStringFn)
                {
                    PropertyType = propertyType,
                };
                var attr = propertyInfo.FirstAttribute<DataMemberAttribute>();
                if (attr?.Name != null)
                {
                    propertySetterMap[attr.Name] = propertySerializer;
                }
                propertySetterMap[propertyInfo.Name] = propertySerializer;
            }
            if (!JsConfig.IncludePublicFields)
            {
                return;
            }
            foreach (var fieldInfo in type.GetSerializableFields())
            {
                var fieldSetFn = fieldInfo.CreateSetter();
                var fieldType = fieldInfo.FieldType;
                var fieldParseStringFn = JsvReader.GetParseFn(fieldType);
                var fieldSerializer = new PropertySerializerEntry(fieldSetFn, fieldParseStringFn)
                {
                    PropertyType = fieldType,
                };
                propertySetterMap[fieldInfo.Name] = fieldSerializer;
            }
        }

        public object PopulateFromMap(object instance, IDictionary<string, string> keyValuePairs, List<string> ignoredWarningsOnPropertyNames = null)
        {
            var errors = new List<RequestBindingError>();
            instance ??= type.CreateInstance();
            instance = keyValuePairs
                .Where(pair => !string.IsNullOrEmpty(pair.Value))
                .Aggregate(
                    instance,
                    (c, n) => PopulateFromKeyValue(c, n.Key, n.Value, out _, errors, ignoredWarningsOnPropertyNames));
            if (errors.Count <= 0)
            {
                return instance;
            }
            var serializationException = new SerializationException($"Unable to bind to request '{type.Name}'");
            serializationException.Data.Add("errors", errors);
            throw serializationException;
        }

        public object PopulateFromMap(object instance, INameValueCollection nameValues, List<string> ignoredWarningsOnPropertyNames = null)
        {
            var errors = new List<RequestBindingError>();
            instance ??= type.CreateInstance();
            foreach (var key in nameValues.AllKeys)
            {
                var value = nameValues[key];
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                instance = PopulateFromKeyValue(
                    instance,
                    key,
                    value,
                    out _,
                    errors,
                    ignoredWarningsOnPropertyNames);
            }
            if (errors.Count <= 0)
            {
                return instance;
            }
            var serializationException = new SerializationException($"Unable to bind to request '{type.Name}'");
            serializationException.Data.Add("errors", errors);
            throw serializationException;
        }

        private object PopulateFromKeyValue(
            object instance,
            string propertyName,
            string propertyTextValue,
            out PropertySerializerEntry propertySerializerEntry,
            List<RequestBindingError> errors,
            List<string> ignoredWarningsOnPropertyNames = null)
        {
            propertySerializerEntry = null;
            try
            {
                if (!propertySetterMap.TryGetValue(propertyName, out propertySerializerEntry))
                {
                    if (propertyName == "v")
                    {
                        if (instance is IHasVersion hasVersion && int.TryParse(propertyTextValue, out var version))
                        {
                            hasVersion.Version = version;
                        }
                        return instance;
                    }
                    var ignoredProperty = propertyName.ToLowerInvariant();
                    if (ignoredWarningsOnPropertyNames == null ||
                        !ignoredWarningsOnPropertyNames.Contains(ignoredProperty))
                    {
                        Log.WarnFormat("Property '{0}' does not exist on type '{1}'", ignoredProperty, type.FullName);
                    }
                    return instance;
                }
                if (propertySerializerEntry.PropertySetFn == null)
                {
                    Log.WarnFormat("Could not set value of read-only property '{0}' on type '{1}'", propertyName, type.FullName);
                    return instance;
                }
                if (propertySerializerEntry.PropertyType == typeof (bool))
                {
                    //InputExtensions.cs#530 MVC Checkbox helper emits extra hidden input field, generating 2 values, first is the real value
                    propertyTextValue = propertyTextValue.LeftPart(',');
                }
                var value = propertySerializerEntry.PropertyParseStringFn(propertyTextValue);
                if (value == null)
                {
                    Log.WarnFormat("Could not create instance on '{0}' for property '{1}' with text value '{2}'", instance, propertyName, propertyTextValue);
                    return instance;
                }
                propertySerializerEntry.PropertySetFn(instance, value);
            }
            catch (Exception ex)
            {
                var error = new RequestBindingError { ErrorMessage = ex.Message };
                if (propertyName != null)
                {
                    error.PropertyName = propertyName;
                }
                if (propertyTextValue != null)
                {
                    error.PropertyValueString = propertyTextValue;
                }
                if (propertySerializerEntry != null && propertySerializerEntry.PropertyType != null)
                {
                    error.PropertyType = propertySerializerEntry.PropertyType;
                }
                errors.Add(error);
            }
            return instance;
        }

        public object CreateFromMap(IDictionary<string, string> keyValuePairs)
        {
            return PopulateFromMap(null, keyValuePairs, null);
        }

        public object CreateFromMap(INameValueCollection nameValues)
        {
            return PopulateFromMap(null, nameValues, null);
        }
    }

    public class RequestBindingError
    {
        public string PropertyName { get; set; }
        public string PropertyValueString { get; set; }
        public Type PropertyType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
