using System;
using System.Collections.Generic;
//using System.Xml;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;
using log4net;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// 
	/// </summary>
	public class OnlineConverter : Converter, IOnlineConverter
	{
		/// <summary>
		/// 
		/// </summary>
		public OnlineConverter() : this( new ConverterFactory(), null )
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="factory"></param>
		public OnlineConverter( IConverterFactory factory, ConfigurationData configData ) 
		{
			this.factory = factory;
			logger = factory.EngineLogger;
			this.configData = configData;
		}

		/// <summary>
		/// Verifies that the inbound message format belongs to the supplied 
		/// outbound message.
		/// </summary>
		/// <param name="requestFormat"></param>
		/// <param name="responseFormat"></param>
		/// <returns></returns>
		public bool MessageMatches( string requestFormat, string responseFormat )
		{
			LoadTemplates( false );
            var goodResponseFormats = requestTemplate.GetMessageFormat( requestFormat );

			// No mapping available
			if ( goodResponseFormats == null )
			{
				return false;
			}

			var validResponseFormats = goodResponseFormats.Split( ',' );
			for ( var ctr = 0; ctr < validResponseFormats.Length; ctr++ )
			{
				if ( validResponseFormats[ ctr ] == responseFormat )
				{
					return true;
				}
			}

			return false;
		}
	}
}
