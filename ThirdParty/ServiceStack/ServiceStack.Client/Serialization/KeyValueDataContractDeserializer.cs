namespace ServiceStack.Serialization
{
#if !(SL5 || __IOS__ || XBOX || ANDROID || PCL)
    using System;
    using System.Collections.Generic;
    using ServiceStack.Web;

    public class KeyValueDataContractDeserializer
    {
        public static KeyValueDataContractDeserializer Instance = new();

        readonly Dictionary<Type, StringMapTypeDeserializer> typeStringMapSerializerMap = new();

        public object Parse(IDictionary<string, string> keyValuePairs, Type returnType)
        {
            return GetOrAddStringMapTypeDeserializer(returnType)
                    .CreateFromMap(keyValuePairs);
        }

        public object Parse(INameValueCollection nameValues, Type returnType)
        {
            return GetOrAddStringMapTypeDeserializer(returnType)
                        .CreateFromMap(nameValues);
        }

        private StringMapTypeDeserializer GetOrAddStringMapTypeDeserializer(Type returnType)
        {
            StringMapTypeDeserializer stringMapTypeDeserializer;
            lock (typeStringMapSerializerMap)
            {
                if (!typeStringMapSerializerMap.TryGetValue(returnType, out stringMapTypeDeserializer))
                {
                    stringMapTypeDeserializer = new(returnType);
                    typeStringMapSerializerMap.Add(returnType, stringMapTypeDeserializer);
                }
            }

            return stringMapTypeDeserializer;
        }

        public To Parse<To>(IDictionary<string, string> keyValuePairs)
        {
            return (To)Parse(keyValuePairs, typeof(To));
        }
    }
}
#endif