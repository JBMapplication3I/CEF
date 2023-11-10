namespace ServiceStack.Serialization
{
#if !(SL5 || __IOS__ || XBOX || ANDROID || PCL)
    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;
    using ServiceStack.Text;
    using static System.String;

    public sealed class XmlSerializerWrapper : XmlObjectSerializer
    {
        System.Xml.Serialization.XmlSerializer serializer;
        string defaultNS;
        readonly Type objectType;

        public XmlSerializerWrapper(Type type)
            : this(type, null, null)
        {

        }

        public XmlSerializerWrapper(Type type, string name, string ns)
        {
            objectType = type;
            if (!IsNullOrEmpty(ns))
            {
                defaultNS = ns;
                serializer = new(type, ns);
            }
            else
            {
                defaultNS = GetNamespace(type);
                serializer = new(type);
            }
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            throw new NotImplementedException();
        }
        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            throw new NotImplementedException();
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            throw new NotImplementedException();
        }

        public override void WriteObject(XmlDictionaryWriter writer, object graph)
        {
            serializer.Serialize(writer, graph);
        }

        public override object ReadObject(XmlDictionaryReader reader)
        {
            string readersNS;

            readersNS = (IsNullOrEmpty(reader.NamespaceURI)) ? "" : reader.NamespaceURI;
            if (Compare(defaultNS, readersNS) != 0)
            {
                serializer = new(objectType, readersNS);
                defaultNS = readersNS;
            }

            return (serializer.Deserialize(reader));
        }

        /// <summary>
        /// Gets the namespace from an attribute marked on the type's definition
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Namespace of type</returns>
        public static string GetNamespace(Type type)
        {
            var dcAttr = type.FirstAttribute<DataContractAttribute>();
            if (dcAttr != null)
            {
                return dcAttr.Namespace;
            }
            var xrAttr = type.FirstAttribute<XmlRootAttribute>();
            if (xrAttr != null)
            {
                return xrAttr.Namespace;
            }
            var xtAttr = type.FirstAttribute<XmlTypeAttribute>();
            if (xtAttr != null)
            {
                return xtAttr.Namespace;
            }
            var xeAttr = type.FirstAttribute<XmlElementAttribute>();
            return xeAttr?.Namespace;
        }
    }
}
#endif