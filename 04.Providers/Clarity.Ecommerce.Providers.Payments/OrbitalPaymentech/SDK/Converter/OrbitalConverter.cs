#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;
using log4net;

namespace JPMC.MSDK.Converter
{
    public class OrbitalConverter : IOnlineConverter
    {
        private OrbitalSchema requestSchema;
        private IConverterFactory factory;
        private FieldMasks masks;
        private OrbitalArrayDef orbitalArrays;
        private ILog logger;

        public OrbitalConverter( IConverterFactory factory )
        {
            requestSchema = factory.GetOrbitalSchema();
            this.factory = factory;
            logger = factory.EngineLogger;

            try
            {
                masks = factory.Config.FieldMasks;
                orbitalArrays = factory.Config.OrbitalArrays;
            }
            catch ( ConfiguratorException e )
            {
                logger.Error( "Failed to load FieldMasks" );
                throw new ConverterException( Error.BadDataError, "Failed to load FieldMasks", e );
            }
        }

        public OrbitalConverter() : this( new ConverterFactory() )
        {
        }

        /**
         * Adds the same string to both StringBuilders, masked and unmasked.
         * This is for strings that have the same value for both.
         * @param xml
         * @param maskedXml
         * @param value
         * @param addLineFeed
         */
        private void Append( StringBuilder xml, StringBuilder maskedXml, string value, bool addLineFeed )
        {
            if ( addLineFeed )
            {
                xml.AppendLine( value );
                maskedXml.AppendLine( value );
            }
            else
            {
                xml.Append( value );
                maskedXml.Append( value );
            }
        }

        public ConverterArgs ConvertRequest( IRequestImpl req )
        {
            var request = (IRequestImpl) req;

            var fields = requestSchema.GetFieldsForFormat( request.TransactionType );

            SetArrayCountFields( request, fields );

            var xml = new StringBuilder();
            var maskedXml = new StringBuilder();
            Append( xml, maskedXml, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>", true );
            Append( xml, maskedXml, "<Request>", true );
            Append( xml, maskedXml, "\t<", false );
            Append( xml, maskedXml, request.TransactionType, false );
            Append( xml, maskedXml, ">", true );

            var tabs = "\t\t";

            for ( var j = 0; j < fields.Count; j++ )
            {
                var fieldBlock = GetBlockOfFields( fields, j );
                if ( fieldBlock == null )
                {
                    continue;
                }
                j += fieldBlock.Count - 1;
                var blockName = FieldIdentifier.GetFormatPath( fields[ j ].name );
                var rootBlockName = blockName.Contains( "." ) ? blockName.Substring( 0, blockName.LastIndexOf( '.' ) ) : "";
                blockName = blockName.Contains( "." ) ? blockName.Substring( blockName.LastIndexOf( '.' ) + 1 ) : blockName;

                // If it's a sub-block and none of its fields are set, skip it.
                var hasNoFields = ProcessSubBlock( fieldBlock, blockName, request );
                if ( hasNoFields )
                {
                    continue;
                }

                // The block of fields to be added will have varying number of values.
                // This gets the highest one.
                var maxBlocks = GetMaxArraySize( request, fieldBlock );

                // Starts the root, which is all the nodes that contains the array's node
                // This will be nothing for everything exception PC3LineItemArray
                tabs = StartRootBlock( xml, maskedXml, rootBlockName, tabs );

                for ( var k = 0; k < maxBlocks; k++ )
                {
                    // Starts the array's node. All 1-level subformats, plus PC3LineItem
                    // This will be nothing for all fields that have no field identifier.
                    tabs = StartBlock( xml, maskedXml, blockName, tabs );

                    AddFields( xml, maskedXml, fieldBlock, k, request, tabs );

                    tabs = EndBlock( xml, maskedXml, blockName, tabs );
                }

                tabs = EndRootBlock( xml, maskedXml, rootBlockName, tabs );
            }

            Append( xml, maskedXml, "\t</", false );
            Append( xml, maskedXml, request.TransactionType, false );
            Append( xml, maskedXml, ">", true );
            Append( xml, maskedXml, "</Request>", true );

            var args = new ConverterArgs();
            args.Request = xml.ToString();
            args.ReqByteArray = xml.ToString().GetBytes();
            args.MaskedRequest = maskedXml.ToString();
            return args;
        }

        private bool ProcessSubBlock( List<FieldData> fieldBlock, string blockName, IRequestImpl request )
        {
            var hasNoFields = false;

            if ( fieldBlock.Count > 1 && blockName.Length > 0 )
            {
                hasNoFields = true;
                foreach ( var field in fieldBlock )
                {
                    var hasNone1 = request.GetArrayValues( field.name ).Count > 0;
                    var hasNone2 = request.HasField( field.name ) && GetFieldValue( field.name, request ) != null;
                    if ( hasNone1 || hasNone2 )
                    {
                        hasNoFields = false;
                        break;
                    }

                    //if ( request.GetArrayValues( field.name ).Count > 0 )
                    //{
                    //    hasNoFields = false;
                    //    break;
                    //}

                    //if ( request.HasField( field.name ) )
                    //{
                    //    try
                    //    {
                    //        if ( request.GetField( field.name ) != null )
                    //        {
                    //            hasNoFields = false;
                    //            break;
                    //        }
                    //    }
                    //    catch ( RequestException )
                    //    {
                    //    }
                    //}
                }

                //if ( hasNoFields )
                //{
                //    continue;
                //}
            }

            return hasNoFields;
        }

        private string GetFieldValue( string name, IRequestImpl request )
        {
            try
            {
                return request.GetField( name );
            }
            catch ( RequestException )
            {
                return null;
            }
        }

        private void SetArrayCountFields( IRequestImpl request, List<FieldData> fields )
        {
            foreach ( var array in orbitalArrays.GetArrays() )
            {
                if ( !request.HasField( array.countField ) )
                {
                    continue;
                }

                var max = 0;
                foreach ( var field in fields )
                {
                    if ( field.name.StartsWith( array.arrayName ) )
                    {
                        var count = request.GetArrayValues( field.name ) != null ? request.GetArrayValues( field.name ).Count : 0;
                        if ( count > max )
                        {
                            max = count;
                        }
                    }
                }

                if ( max == 0 )
                {
                    return;
                }

                try
                {
                    if ( request[ array.countField ] == null || request[ array.countField ].Length == 0 )
                    {
                        request.SetField( array.countField, max );
                    }
                }
                catch ( RequestException)
                {
                }
            }
        }

        private void AddFields( StringBuilder xml, StringBuilder maskedXml, List<FieldData> fieldBlock, int index, IRequestImpl request, string tabs )
        {
            foreach ( var field in fieldBlock )
            {
                var values = request.GetArrayValues( field.name );
                if ( values.Count == 0 )
                {
                    try
                    {
                        values.Add( request[ field.name ] );
                    }
                    catch ( RequestException )
                    {
                    }
                }

                var arrayInfo = orbitalArrays[ FieldIdentifier.GetFormatPath( field.name ) ];

                var shortName = FieldIdentifier.GetName( field.name );

                string value = null;

                if ( arrayInfo != null && arrayInfo.indexField.Equals( shortName ) )
                {
                    value = ( index + 1 ).ToString();
                }

                if ( value == null && ( values.Count <= index || values[ index ] == null ) )
                {
                    var add = tabs + "<" + shortName + "/>";
                    Append( xml, maskedXml, add, true);
                }
                else
                {
                    var temp = values.Count > index ? values[ index ] : null;
                    if ( temp != null && temp.Length > 0 )
                    {
                        value = temp;
                    }
                    var start = tabs + "<" + shortName + ">";
                    var end = "</" + shortName + ">";
                    var add = start + value + end;
                    xml.AppendLine( add );

                    add = start + masks.MaskValue( field.name, value, request.TransactionType ) + end;
                    maskedXml.AppendLine( add );
                }

                if ( field.required && ( value == null || value.Trim().Length == 0 ) )
                {
                    throw new ConverterException( Error.RequiredFieldNotSet, "Required field \"" + field.name + "\" is not set to a value." );
                }
            }
        }

        private string StartBlock( StringBuilder xml, StringBuilder maskedXml, string blockName, string tabs )
        {
            if ( blockName == null || blockName.Length == 0 )
            {
                return tabs;
            }

            var add = tabs + "<" + blockName + ">";
            Append( xml, maskedXml, add, true);
            tabs += "\t";

            return tabs;
        }

        private string EndBlock( StringBuilder xml, StringBuilder maskedXml, string blockName, string tabs )
        {
            if ( blockName == null || blockName.Length == 0 )
            {
                return tabs;
            }

            tabs = tabs.Substring( 1 );
            var add = tabs + "</" + blockName + ">";
            Append( xml, maskedXml, add, true);

            return tabs;
        }

        private string EndRootBlock( StringBuilder xml, StringBuilder maskedXml, string blockName, string tabs )
        {
            var parts = blockName.Split( '.' );
            for ( var i = parts.Length - 1; i >= 0; i-- )
            {
                if ( parts[ i ] == null || parts[ i ].Length == 0 )
                {
                    continue;
                }

                tabs = tabs.Substring( 1 );
                var add = tabs + "</" + parts[ i ] + ">";
                Append( xml, maskedXml, add, true);
            }

            return tabs;
        }

        private string StartRootBlock( StringBuilder xml, StringBuilder maskedXml, string blockName, string tabs )
        {
            var parts = blockName.Split( '.' );
            foreach ( var level in parts )
            {
                if ( level == null || level.Length == 0 )
                {
                    continue;
                }

                var add = tabs + "<" + level + ">";
                Append( xml, maskedXml, add, true);
                tabs += "\t";
            }

            return tabs;
        }

        private int GetMaxArraySize( IRequestImpl request, List<FieldData> fieldBlock )
        {
            var max = 1;

            foreach ( var field in fieldBlock )
            {
                var values = request.GetArrayValues( field.name );
                if ( values.Count > max )
                {
                    max = values.Count;
                }
            }

            return max;
        }

        private List<FieldData> GetBlockOfFields( List<FieldData> fields, int startIndex )
        {
            string blockName = null;
            var block = new List<FieldData>();
            for ( var i = startIndex; i < fields.Count; i++ )
            {
                var field = fields[ i ];
                if ( !field.name.Contains( "." ) )
                {
                    if ( blockName != null )
                    {
                        return block;
                    }
                    block.Add( field );
                    return block;
                }

                if ( blockName == null )
                {
                    blockName = FieldIdentifier.GetFormatPath( field.name );
                }

                if ( !FieldIdentifier.GetFormatPath( field.name ).Equals( blockName ) )
                {
                    return block;
                }

                block.Add( field );
            }

            return null;
        }

        private List<string> GetValues( string fieldName, IRequestImpl request )
        {
            var values = request.GetArrayValues( fieldName );
            if ( values.Count > 0 )
            {
                return values;
            }

            string value = null;
            try
            {
                value = request[ fieldName ];
            }
            catch ( RequestException e )
            {
                throw new ConverterException( Error.FieldDoesNotExist, e.Message, e );
            }

            values = new List<string>();
            values.Add( value );
            return values;
        }

        public ConverterArgs ConvertResponse( string xml, bool isPNS )
        {
            IFullResponse response = null;

            XmlNode node = null;
            try
            {
                response = factory.MakeResponse();

                // Specify no validation in case a schema file is specified.
                var doc = new XmlDocument();
                doc.LoadXml( xml );
                node = doc.DocumentElement;
            }
            catch ( Exception ex )
            {
                throw new ConverterException( Error.ResponseFailure, "Failed to read the response XML. It may be corrupt.", ex );
            }

            response.ResponseType = node.Name;

            string nodeName = null;
            try
            {
                var elements = new List<DataElement>();

                for ( var i = 0; i < node.ChildNodes.Count; i++ )
                {
                    nodeName = null;
                    var child = node.ChildNodes.Item( i );
                    if ( child.NodeType != XmlNodeType.Element )
                    {
                        continue;
                    }

                    nodeName = child.Name;
                    var textNode = Utils.GetTextNode( child );
                    DataElement elem = null;
                    if ( textNode == null )
                    {
                        elem = new DataElement( child.Name, null, response );
                    }
                    else
                    {
                        var value = Utils.GetNodeValue( child ).Trim();
                        value = value.Length == 0 ? null : value;
                        elem = new DataElement( child.Name, value, response );
                    }
                    ConvertResponseChildNodes( child, elem );
                    elements.Add( elem );
                }

                response.DataElements = elements;
            }
            catch ( Exception ex )
            {
                if ( nodeName != null )
                {
                    throw new ConverterException( Error.ResponseFailure, "Failed to convert the response at node \"" + nodeName + "\".", ex );
                }
                throw new ConverterException( Error.ResponseFailure, "Failed to convert the response.", ex );
            }

            var args = new ConverterArgs();
            args.ResponseData = response;
            return args;
        }

        private void ConvertResponseChildNodes( XmlNode node, DataElement parent )
        {
            // changed, not dealing with elements list.
            string nodeName = null;
            try
            {
                for ( var i = 0; i < node.ChildNodes.Count; i++ )
                {
                    nodeName = null;
                    var child = node.ChildNodes.Item( i );
                    if ( child.NodeType != XmlNodeType.Element )
                    {
                        continue;
                    }

                    nodeName = child.Name;
                    var textNode = Utils.GetTextNode( child );
                    DataElement elem = null;
                    if ( textNode == null )
                    {
                        elem = new DataElement( child.Name, null, parent );
                    }
                    else
                    {
                        elem = new DataElement( child.Name, Utils.GetNodeValue( child ), parent );
                    }
                    parent.Elements.Add( elem );
                    ConvertResponseChildNodes( child, elem );
                }
            }
            catch ( Exception ex )
            {
                if ( nodeName != null )
                {
                    throw new ConverterException( Error.ResponseFailure, "Failed to convert the response at node \"" + nodeName + "\".", ex );
                }
                throw new ConverterException( Error.ResponseFailure, "Failed to convert the response.", ex );
            }

        }

        public ConverterArgs ConvertResponse( byte[] respByteArray, bool isPNS )
        {
            return ConvertResponse( Utils.ByteArrayToString( respByteArray ), false );
        }

        public bool MessageMatches( string requestFormat, string responseFormat )
        {
            return true;
        }
    }

}