#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Collections.Generic;
using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Configurator
{
    public class OrbitalArrayDef
    {
        private KeySafeDictionary<string, OrbitalArray> arrays = new KeySafeDictionary<string, OrbitalArray>();

        public OrbitalArrayDef( XmlElement documentNode )
        {
            var arrayNodes = documentNode.GetElementsByTagName( "Array" );

            for ( var i = 0; i < arrayNodes.Count; i++ )
            {
                var arrayNode = arrayNodes.Item( i );

                var array = new OrbitalArray();
                array.arrayName = Utils.GetAttributeValue( "name", null, arrayNode );
                array.countField = Utils.GetAttributeValue( "count", null, arrayNode );
                array.indexField = Utils.GetAttributeValue( "index", null, arrayNode );

                arrays[ array.arrayName ] = array;
            }
        }

        public Dictionary<string, OrbitalArray>.ValueCollection GetArrays()
        {
            return arrays.Values;
        }

        public OrbitalArray this[ string name ]
        {
            get
            {
                if ( arrays.ContainsKey( name ) )
                {
                    return arrays[ name ];
                }

                return null;
            }
        }
    }
}