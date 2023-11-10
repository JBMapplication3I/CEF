#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Collections.Generic;
using System.Xml;

namespace JPMC.MSDK.Configurator
{
    /// <p>Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
    /// reserved</p>
    ///
    /// @author Rameshkumar Bhaskharan
    /// @version 1.0
    ///
    /// <summary>
    /// Summary description for IConfigurator.
    /// </summary>
    public interface IConfigurator
    {
        /// <summary>
        /// Return the config file name
        /// </summary>
        string ConfigFileName
        {
            get;
        }
        /// <summary>
        /// Return MSDK_HOME env variable
        /// </summary>
        string HomeDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the GeneralData section from the MSDKConfig.xml
        /// file, accessible via Properties.
        /// </summary>
        /// <returns>A GeneralData object that provides properties for accessing its values.</returns>
        GeneralData GeneralData { get; }

            /// <summary>
        /// Gets the SpecialTemplateNames section from the MSDKConfig.xml
        /// file, accessible via Properties.
        /// </summary>
        /// <returns>A SpecialTemplateNames object that provides properties for accessing its values.</returns>
        SpecialTemplateNames SpecialTemplateNames { get; }

        /// <summary>
        /// Gets the contents of the DFRConditions element in the
        /// MSDKConfig.xml file, with its data accessible via
        /// Properties.
        /// </summary>
        /// <returns>A list of DFRConditions objects for each enabled condition.</returns>
        List<string> DFROptions { get; }

        /// <summary>
        /// Gets the XML document for the specified config file.
        /// </summary>
        /// <param name="name">The name of the config file to get.</param>
        /// <returns>The XML Document of the specified config file.</returns>
        XmlDocument GetXmlDocument( string name );

        /// <summary>
        /// Gets the default list of default values.
        /// </summary>
        /// <remarks>
        /// This method is for internal use only.
        /// </remarks>
        /// <param name="configData">The default values are dependant on the SystemType, so this must be supplied.</param>
        /// <returns>A list of DefaultValue objects.</returns>
        DefaultValue[] GetDefaultValues( ConfigurationData configData );

        ConfigurationData GetProtocolConfig( string configName );

        ConfigurationData GetProtocolConfig( CommModule module );

        FieldMasks FieldMasks { get; }

        OrbitalArrayDef OrbitalArrays { get; }

    } // end of interface
}// end of namespace
