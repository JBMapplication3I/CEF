using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using JPMC.MSDK.Common;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay( "{GetType()}, Name: {name}" )]
    public class RequestField
    {
        /// <summary>
        /// The field's name, like "MerchantOrderNumber"
        /// </summary>
        public string name;
        /// <summary>
        /// The element path, like "/P/AccountNumber"
        /// </summary>
        public string elementID;
        public string fieldID;
        private string value;
        private byte[] binaryValue;
        private string maskedValue;
        private string maskedPrefix;
        /// <summary>
        /// Specifies if the field's value is required. 
        /// </summary>
        public bool required;
        /// <summary>
        /// Specifies if the field is obsolete and should be omitted.
        /// </summary>
        public bool dropped;
        /// <summary>
        /// The default value for the field.
        /// </summary>
        public string defaultValue;
        /// <summary>
        /// Specifies the name of a different field to set instead of this one.
        /// </summary>
        /// <remarks>
        /// <para>When a field is renamed, we can't just remove the old name, 
        /// for backward compatibility. In this case, the old field is 
        /// set to deprecated, and points to the new field. All attempts 
        /// to get or set this field just passes to the new field, referenced here.
        /// </para>
        /// </remarks>
        public string deprecated;
        /// <summary>
        /// The name of the file to load the default value from.
        /// </summary>
        public string file;
        /// <summary>
        /// The XPath string that the default value is read from. 
        /// Used with the file attribute.
        /// </summary>
        public string field;
        /// <summary>
        /// The name of the method to be call to generate the field's value.
        /// </summary>
        public string method;
        /// <summary>
        /// For auto-calculation, this is the start value.
        /// </summary>
        public string start;
        /// <summary>
        /// For auto-calculation, this is the next value.
        /// </summary>
        public string next;
        /// <summary>
        /// Specifies whether the field's value can be set.
        /// </summary>
        public bool readOnly;
        /// <summary>
        /// Is set to true if the field uses the new XML format.
        /// </summary>
        /// <remarks>
        /// In the past, the field's name is the name of the node. 
        /// The new format will have the node name be Field and it will
        /// have a "name" attribute.
        /// </remarks>
        public bool isNewFormat;

        /// <summary>
        /// Is set to true if the field inherits its value from the same field 
        /// of its parent.
        /// </summary>
        public bool inheritValue;

        public RequestField( string name, string value )
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Constructor. Parses an XmlNode. 
        /// </summary>
        /// <param name="node">The XmlNode that contains the field's data.</param>
        /// <param name="elementPath">The element path of the parent node.</param>
        public RequestField( XmlNode node, string elementPath, string formatPath )
        {
            name = node.Name;
            this.elementID = elementPath + FieldIdentifier.Separator + name;
            fieldID = FieldIdentifier.Combine( formatPath, name );

            required = Utils.StringToBoolean( Utils.GetAttributeValue( "required", null, node ) );
            defaultValue = Utils.GetAttributeValue( "default", null, node );
            value = defaultValue;
            deprecated = Utils.GetAttributeValue( "deprecated", null, node );
            method = Utils.GetAttributeValue( "method", null, node );
            start = Utils.GetAttributeValue( "start", null, node );
            if ( start != null )
            {
                defaultValue = start;
            }
            next = Utils.GetAttributeValue( "next", null, node );
            file = Utils.GetAttributeValue( "file", null, node );
            field = Utils.GetAttributeValue( "field", null, node );
            dropped = Utils.StringToBoolean( Utils.GetAttributeValue( "dropped", null, node ) );
            inheritValue = Utils.StringToBoolean( Utils.GetAttributeValue( "inheritValue", null, node ) );
            // Alias is a synonym for deprecated.
            var alias = Utils.GetAttributeValue( "alias", null, node );
            if ( alias != null )
            {
                deprecated = alias;
            }
            readOnly = Utils.StringToBoolean( Utils.GetAttributeValue( "readOnly", "false", node ) );
            if ( !readOnly )
            {
                readOnly = Utils.GetTextNode( node ) == null;
            }
        }

        public RequestField( RequestField copy )
        {
            this.defaultValue = copy.defaultValue;
            this.deprecated = copy.deprecated;
            this.dropped = copy.dropped;
            this.elementID = copy.elementID;
            this.field = copy.field;
            this.file = copy.file;
            this.inheritValue = copy.inheritValue;
            this.isNewFormat = copy.isNewFormat;
            this.method = copy.method;
            this.name = copy.name;
            this.next = copy.next;
            this.readOnly = copy.readOnly;
            this.required = copy.required;
            this.start = copy.start;
            this.value = copy.value;
        }

        /// <summary>
        /// Gets and sets the field's value. When getting, it will return the 
        /// default value, if the value is null.
        /// </summary>
        public string Value
        {
            get => this.value != null ? this.value : defaultValue;
            set => this.value = value;
        }

        public byte[] BinaryValue
        {
            get => this.binaryValue;
            set => this.binaryValue = value;
        }

        public string MaskedValue
        {
            get => this.maskedValue != null ? this.maskedValue : Value;
            set => this.maskedValue = value;
        }

        public string MaskedPrefix
        {
            get => this.maskedPrefix;
            set => this.maskedPrefix = value;
        }
    }
}
