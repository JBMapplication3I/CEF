using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Configurator
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneralData : ConfigurationData
    {
	    private static readonly string[] names = 
	    { 
            "Version", "FW", 
            "LoadAndWatchLog=true",
        };
    	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
	    public GeneralData(XmlNode node)
        {
		    Parse(node, names);
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pairs"></param>
		public GeneralData( KeySafeDictionary<string, string> pairs )
        {
            Parse( pairs );
        }


		/// <summary>
		/// 
		/// </summary>
        public string Version => configData["Version"];

        /// <summary>
		/// 
		/// </summary>
		public string FW => configData["FW"];

        /// <summary>
		/// 
		/// </summary>
		public bool LoadAndWatchLog => GetBool( "LoadAndWatchLog", false );
    }
}
