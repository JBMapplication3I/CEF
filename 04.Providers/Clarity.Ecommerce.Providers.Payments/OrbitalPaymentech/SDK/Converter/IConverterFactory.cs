using System;
using System.Collections.Generic;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
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
    public interface IConverterFactory
    {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        IConfigurator GetConfig();
        /// <summary>
        /// 
        /// </summary>
		IConfigurator Config { get; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="requestType"></param>
		/// <returns></returns>
        RequestTemplate GetRequestTemplate( RequestType requestType );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="responseType"></param>
		/// <returns></returns>
        ResponseTemplate GetResponseTemplate( RequestType responseType );
		/// <summary>
		/// 
		/// </summary>
        ILog EngineLogger { get; }

		IRequest MakeRequest( IRequest request );
		IFullResponse MakeResponse();
        OrbitalSchema GetOrbitalSchema();
        XmlDocument LoadXMLFile( string fileName );
        Definitions Definitions { get; }
	}
}
