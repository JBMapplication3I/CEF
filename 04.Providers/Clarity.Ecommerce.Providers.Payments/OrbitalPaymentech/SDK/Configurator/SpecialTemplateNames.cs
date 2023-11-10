using System.Xml;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Configurator
{
    /// <summary>
    /// 
    /// </summary>
    public class SpecialTemplateNames : ConfigurationData
    {
	    private static readonly string[] names = 
	    { 
		    "Header", "BatchTotal", "Totals", "Trailer", "Rfr", "Rfs"
	    };


		/// <summary>
		/// 
		/// </summary>
		public SpecialTemplateNames()
        {
	    }
    	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
	    public SpecialTemplateNames(XmlNode node)
        {
		    Parse(node, names);
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pairs"></param>
        public SpecialTemplateNames( KeySafeDictionary<string, string> pairs )
        {
            Parse( pairs );
        }



		/// <summary>
		/// 
		/// </summary>
		public string Header => configData["Header"];

        /// <summary>
		/// 
		/// </summary>
		public string BatchTotal => configData["BatchTotal"];

        /// <summary>
		/// 
		/// </summary>
		public string Totals => configData["Totals"];

        /// <summary>
		/// 
		/// </summary>
		public string Trailer => configData["Trailer"];

        /// <summary>
		/// 
		/// </summary>
		public string Rfr => configData["Rfr"];

        /// <summary>
		/// 
		/// </summary>
		public string Rfs => configData["Rfs"];
    }
}
