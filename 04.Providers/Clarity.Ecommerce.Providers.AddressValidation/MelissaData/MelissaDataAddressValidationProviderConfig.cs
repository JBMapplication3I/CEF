// <copyright file="MelissaDataAddressValidationProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the melissa data address validation provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.AddressValidation.MelissaData
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A melissa data address validation provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class MelissaDataAddressValidationProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="MelissaDataAddressValidationProviderConfig" /> class.</summary>
        static MelissaDataAddressValidationProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(MelissaDataAddressValidationProviderConfig));
        }

        /// <summary>Gets URL of the base.</summary>
        /// <value>The base URL.</value>
        [AppSettingsKey("Clarity.Address.Validation.MelissaData.BaseUrl"),
         DefaultValue(null)]
        internal static string? BaseUrl
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(MelissaDataAddressValidationProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(MelissaDataAddressValidationProviderConfig));
        }

        /// <summary>Gets the license key.</summary>
        /// <value>The license key.</value>
        [AppSettingsKey("Clarity.Address.Validation.MelissaData.LicenseKey"),
         DefaultValue(null)]
        internal static string? LicenseKey
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(MelissaDataAddressValidationProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(MelissaDataAddressValidationProviderConfig));
        }

        /// <summary>Gets the error codes.</summary>
        /// <value>The error codes.</value>
        internal static string[] ErrorCodes { get; }
            = "AE01,AE02,AE03,AE05,AE08,AE09,AE10,AE11,AE12,AE13,AE14,AE17".Split(',');

        /// <summary>Gets the change codes.</summary>
        /// <value>The change codes.</value>
        internal static string[] ChangeCodes { get; }
            = "AC01,AC02,AC03,AC09,AC10,AC11,AC12,AC13,AC14,AC15,AC16,AC17,AC18,AC19,AC22".Split(',');

        /// <summary>Gets the partially verified.</summary>
        /// <value>The partially verified.</value>
        internal static string[] PartiallyVerified { get; }
            = "AV11,AV12,AV13,AV14".Split(',');

        /// <summary>Gets the low level verified.</summary>
        /// <value>The low level verified.</value>
        internal static string[] LowLevelVerified { get; }
            = "AV21,AV22,AV23".Split(',');

        /// <summary>Gets the high level verified.</summary>
        /// <value>The high level verified.</value>
        internal static string[] HighLevelVerified { get; }
            = "AV24,AV25".Split(',');

        /// <summary>Gets the av 3 countries.</summary>
        /// <value>The av 3 countries.</value>
        internal static string[] Av3Countries { get; }
            = "GN,GW,ET,DO,VA,HN,IR,IQ,HT,GY,CI,KE,JM,LA,KG,LS,GA,GM,GH,GE,ER,GQ,SV,FK,BJ,BO,BB,BZ,DZ,AF,AW,AM,CU,CD,CG,BF,BI,KH,CM,CV,CF,TD,CN,SH,RW,NE,KP,PK,MW,MV,ML,MR,MG,MD,MN,MU,NP,NR,MM,SY,SD,SO,SS,VC,WS,LC,SL,ST,SN,KN,UG,TO,TT,TZ,TG,UZ,VE,ZM".Split(',');

        /// <summary>Gets the av 2 countries.</summary>
        /// <value>The av 2 countries.</value>
        internal static string[] Av2Countries { get; }
            = "AI,AQ,AG,BT,BQ,IO,VG,CX,CC,KM,CK,CW,DJ,DM,FO,FJ,PF,TF,GL,GD,KI,LR,LY,MS,AN,NC,NI,NU,NF,PS,PG,PN,MF,SC,SX,SB,GS,SJ,TJ,TL,TK,TM,TC,TV,UM,VU,WF,EH,YE".Split(',');

        /// <summary>Gets the code messages.</summary>
        /// <value>The code messages.</value>
        internal static Dictionary<string, string> CodeMessages { get; } = new()
        {
            { "AE01", "Postal Code Error/General Error | The address could not be verified at least up to the postal code level." },
            { "AE02", "Unknown Street | Could not match the input street to a unique street name. Either no matches or too many matches found." },
            { "AE03", "Component Mismatch Error | The combination of directionals (N, E, SW, etc) and the suffix (AVE, ST, BLVD) is not correct and produced multiple possible matches." },
            { "AE05", "Multiple Match | The address was matched to multiple records. There is not enough information available in the address to break the tie between multiple records." },
            { "AE08", "Sub Premise Number Invalid | The thoroughfare (street address) was found but the sub premise (suite) was not valid." },
            { "AE09", "Sub Premise Number Missing | The thoroughfare (street address) was found but the sub premise (suite) was missing." },
            { "AE10", "Premise Number Invalid | The premise (house or building) number for the address is not valid." },
            { "AE11", "Premise Number Missing | The premise (house or building) number for the address is missing." },
            { "AE12", "Box Number Invalid | The PO (Post Office Box), RR (Rural Route), or HC (Highway Contract) Box number is invalid." },
            { "AE13", "Box Number Missing | The PO (Post Office Box), RR (Rural Route), or HC (Highway Contract) Box number is missing." },
            { "AE14", "PMB Number Missing | US Only. The address is a Commercial Mail Receiving Agency (CMRA) and the Private Mail Box (PMB or #) number is missing." },
            { "AE17", "Sub Premise Not Required (Deprecated - See AS23) | A sub premise (suite) number was entered but the address does not have secondaries. (Deprecated - See AS23)" },
            { "AC01", "Postal Code Change | The postal code was changed or added." },
            { "AC02", "Administrative Area Change | The administrative area (state, province) was added or changed." },
            { "AC03", "Locality Change | The locality (city, municipality) name was added or changed." },
            { "AC09", "Dependent Locality Change | The dependent locality (urbanization) was changed." },
            { "AC10", "Thoroughfare Name Change | The thoroughfare (street) name was added or changed due to a spelling correction." },
            { "AC11", "Thoroughfare Type Change | The thoroughfare (street) leading or trailing type was added or changed, such as from 'St' to 'Rd.'" },
            { "AC12", "Thoroughfare Directional Change | The thoroughfare (street) pre-directional or post-directional was added or changed, such as from 'N' to 'NW.'" },
            { "AC13", "Sub Premise Type Change | The sub premise (suite) type was added or changed, such as from 'STE' to 'APT.'" },
            { "AC14", "Sub Premise Number Change | The sub premise (suite) unit number was added or changed." },
            { "AC15", "Double Dependent Locality Change | The double dependent locality was added or changed." },
            { "AC16", "SubAdministrative Area Change | The subadministrative area was added or changed." },
            { "AC17", "SubNational Area Change | The subnational area was added or changed." },
            { "AC18", "PO Box Change | The PO Box was added or changed." },
            { "AC19", "Premise Type Change | The premise type was added or changed." },
            { "AC22", "Organization Change | The organization was added or changed." },
            { "GS01", "Geocoded to Street Level | The record was coded to the street level (Zip+4 for US, full postal code for CA)." },
            { "GS02", "Geocoded to the Neighborhood Level | The record was geocoded down to neighborhood level (Zip+2 for US)." },
            { "GS03", "Geocoded to Community Level | The record was coded to the community level (ZIP centroid for US, 3-digit postal code for CA)." },
            { "GS04", "Geocoded to State Level | The record was geocoded to the state (administrative area) level." },
            { "GS05", "Geocoded to Rooftop Level | The record was geocoded down to the rooftop level, meaning the point is within the property boundaries, usually the center." },
            { "GS06", "Geocoded to Interpolated Rooftop Level | The record was geocoded down to the rooftop level using interpolation (educated estimations using street coordinates). The point may be in or close to the property boundaries." },
            { "AV11", "Administrative Area Partial	The address has been partially verified to the Administrative Area (State) Level, which is NOT the highest level possible with the reference data." },
            { "AV12", "Locality Partial | The address has been partially verified to the Locality (City) Level, which is NOT the highest level possible with the reference data." },
            { "AV13", "Thoroughfare Partial | The address has been partially verified to the Thoroughfare (Street) Level, which is NOT the highest level possible with the reference data." },
            { "AV14", "Premise Partial | The address has been partially verified to the Premise (House or Building) Level, which is NOT the highest level possible with the reference data." },
            { "AV21", "Administrative Area Full | The address has been verified to the Administrative Area (State) Level, which is the highest level possible with the reference data." },
            { "AV22", "Locality Full | The address has been verified to the Locality (City) Level, which is the highest level possible with the reference data." },
            { "AV23", "Thoroughfare Full | The address has been verified to the Thoroughfare (Street) Level, which is the highest level possible with the reference data." },
            { "AV24", "Premises Full | The address has been verified to the Premise (House or Building) Level, which is the highest level possible with the reference data." },
            { "AV25", "SubPremises Full | The address has been verified to the SubPremise (Suite) or PO Box Level, which is the highest level possible with the reference data." },
        };

        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Config is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<MelissaDataAddressValidationProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(BaseUrl, LicenseKey);
    }
}
