#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Converter
{
    public class OrbitalSchema
    {
        private static OrbitalSchema instance;

        private IConverterFactory factory;
        private string schemaFileName;
        private KeySafeDictionary<string, List<FieldData>> formats;
        private string ptiVersion;

        public OrbitalSchema() : this( new ConverterFactory() )
        {

        }
        /**
         * Default constructor for singleton object
         * @param factory
         */
        public OrbitalSchema( IConverterFactory factory )
        {
            this.factory = factory;
            if ( factory == null )
            {
                this.factory = new ConverterFactory();
            }
        }

        /**
         * gets a static object for the request type
         * @return
         * @throws ConverterException
         */
        public static OrbitalSchema GetInstance()
        {
            if ( instance != null )
            {
                return instance;
            }

            instance = new OrbitalSchema( null );
            try
            {
                var filename = SchemaFileName;
                instance.Initialize( filename );
            }
            catch ( ConverterException )
            {
                throw;
            }
            catch ( Exception e )
            {
                throw new ConverterException( Error.InitializationFailure,
                        "Failed to initialize the OrbitalRequest", e );
            }

            return instance;
        }

        private static string SchemaFileName
        {
            get
            {
                IConverterFactory factory = new ConverterFactory();
                using ( var defs = factory.Definitions )
                {
                    var filename = defs.FindFileNameByExtension( ".xsd" );
                    if ( filename != null )
                    {
                        return filename;
                    }
                }

                throw new ConverterException( Error.InitializationFailure, "Failed to find the schema file." );
            }
        }

        public void Initialize( string schemaFileName )
        {
            ptiVersion = schemaFileName.Replace( "Request_", "" ).Replace( ".xsd", "" );
            XmlDocument schema = null;
            using ( var defs = factory.Definitions )
            {
                schema = defs.GetXmlDocument( schemaFileName, false );
            }

            //XmlDocument schema = factory.LoadXMLFile( schemaFileName );
            Initialize( schema );
            this.schemaFileName = schemaFileName;
        }

        public void Initialize( XmlDocument schema )
        {
            var requestNode = schema.GetElementsByTagName( "xs:choice" ).Item( 0 );
            if ( requestNode == null )
            {
                throw new ConverterException( Error.InitializationFailure, "Orbital Schema is missing Request." );
            }

            LoadFormatMap( requestNode, schema.DocumentElement );
        }

        private void LoadFormatMap( XmlNode requestNode, XmlElement docElem )
        {
            formats = new KeySafeDictionary<string, List<FieldData>>();
            var elements = ( (XmlElement) requestNode ).GetElementsByTagName( "xs:element" );

            for ( var i = 0; i < elements.Count; i++ )
            {
                var element = elements.Item( i );
                var name = Utils.GetAttributeValue( "name", null, element );
                var dataType = Utils.GetAttributeValue( "type", null, element );
                var fields = BuildFieldList( dataType, "", docElem );
                formats[ name ] = fields;
            }
        }

        private List<FieldData> BuildFieldList( string nodeName, string basePath, XmlElement docElem )
        {
            var fields = new List<FieldData>();

            var node = GetElementByAttributeValue( docElem, "name", nodeName );
            if ( node == null )
            {
                return fields;
            }

            var elements = ( (XmlElement) node ).GetElementsByTagName( "xs:element" );

            if ( elements == null || elements.Count == 0 )
            {
                return fields;
            }


            var path = basePath.Length > 0 ? basePath + "." : basePath;
            for ( var i = 0; i < elements.Count; i++ )
            {
                var element = elements.Item( i );
                var name = Utils.GetAttributeValue( "name", null, element );
                var dataType = Utils.GetAttributeValue( "type", null, element );
                var minOccurs = Utils.GetAttributeValue( "minOccurs", "1", element );

                if ( dataType == null )
                {
                    continue;
                }

                var list = BuildFieldList( dataType, path + name, docElem );
                if ( dataType.Equals( "xs:string" ) || list.Count == 0 )
                {
                    var fieldName = path + name;
                    var field = new FieldData( fieldName, Utils.StringToInt( minOccurs ) > 0, false );
                    fields.Add( field );
                }
                else
                {
                    fields.AddRange( list );
                }
            }

            return fields;
        }

        public XmlNode GetElementByAttributeValue( XmlNode rootElement, string attributeName, string attributeValue )
        {
            if ( rootElement != null && rootElement.HasChildNodes )
            {
                var nodeList = rootElement.ChildNodes;

                for ( var i = 0; i < nodeList.Count; i++ )
                {
                    var subNode = nodeList.Item( i );
                    var attribValue = Utils.GetAttributeValue( attributeName, null, subNode );
                    if ( attribValue != null && attribValue.Equals( attributeValue ) )
                    {
                        return subNode;
                    }
                }
            }

            return null;
        }

        public List<FieldData> GetFieldsForFormat( string formatName )
        {
            if ( !formats.ContainsKey( formatName ) )
            {
                throw new ConverterException( Error.FormatNotFoundError, "The format \"" + formatName + "\" was not found." );
            }

            return formats[ formatName ];
        }

        public bool HasFormat( string formatName )
        {
            return formats.ContainsKey( formatName );
        }

        public string FileName => schemaFileName;

        public string PTIVersion => ptiVersion;

        public FieldData GetField( string transactionType, string fieldName )
        {
            if ( !formats.ContainsKey( transactionType ) )
            {
                throw new RequestException( Error.FormatNotFoundError, "The format \"" + transactionType + "\" does not exist." );
            }

            var fields = formats[ transactionType ];

            var newName = Utils.StripIndexer( fieldName );

            foreach ( var field in fields )
            {
                if ( field.name.Equals( newName ) )
                {
                    return field;
                }
            }

            throw new RequestException( Error.FieldDoesNotExist, "The field \"" + fieldName + "\" does not exist." );
        }

        public bool HasField( string transactionType, string fieldName )
        {
            if ( !formats.ContainsKey( transactionType ) )
            {
                return false;
            }

            var fields = formats[ transactionType ];

            foreach ( var field in fields )
            {
                if ( field.name.Equals( fieldName ) )
                {
                    return true;
                }
            }

            return false;
        }
    }

}