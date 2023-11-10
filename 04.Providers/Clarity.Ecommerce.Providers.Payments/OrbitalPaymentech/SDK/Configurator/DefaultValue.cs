using System;
using System.Collections.Generic;
using System.Text;

namespace JPMC.MSDK.Configurator
{
	/// <summary>
	/// 
	/// </summary>
    public class DefaultValue
    {
		/// <summary>
		/// 
		/// </summary>
        public string name;
		/// <summary>
		/// 
		/// </summary>
        public string value;
		/// <summary>
		/// 
		/// </summary>
        public string destination;

		/// <summary>
		/// 
		/// </summary>
        public DefaultValue()
        {
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="destination"></param>
        public DefaultValue( string name, string value, string destination )
        {
            this.name = name;
            this.value = value;
            this.destination = destination;
        }
    }
}
