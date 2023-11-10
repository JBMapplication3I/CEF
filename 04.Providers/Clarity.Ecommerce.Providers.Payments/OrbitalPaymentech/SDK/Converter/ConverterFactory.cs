using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Framework;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// 
	/// </summary>
    public class ConverterFactory : IConverterFactory
    {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        [MethodImpl( MethodImplOptions.Synchronized )]
        public IConfigurator GetConfig()
        {
            return Configurator.Configurator.GetInstance();
        }

		/// <summary>
		/// 
		/// </summary>
		public IConfigurator Config => GetConfig();

        /// <summary>
		/// 
		/// </summary>
		/// <param name="requestType"></param>
		/// <returns></returns>
        public RequestTemplate GetRequestTemplate( RequestType requestType )
        {
            return RequestTemplate.GetInstance( requestType );
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="responseType"></param>
		/// <returns></returns>
        public ResponseTemplate GetResponseTemplate( RequestType responseType )
        {
            return ResponseTemplate.GetInstance( responseType );
        }

		/// <summary>
		/// 
		/// </summary>
		public ILog EngineLogger => new LoggingWrapper().EngineLogger;

        public IRequest MakeRequest( IRequest request )
		{
            if ( request is OrbitalRequest )
            {
                return new OrbitalRequest( (OrbitalRequest) request );
            }
			return new Request( (Request) request );
		}

		public IFullResponse MakeResponse()
		{
			return new Response();
		}

        public OrbitalSchema GetOrbitalSchema()
        {
            return OrbitalSchema.GetInstance();
        }

        public XmlDocument LoadXMLFile( string fileName )
        {
            return null;
        }

        public Definitions Definitions => new DefinitionsFile( Config.HomeDirectory + "\\lib\\definitions.jar", new ConfiguratorFactory() );
    }
}
