// <copyright file="CustomerAddressSerializable.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the customer address serializable class</summary>
// <remarks>This file content was copied from ClarityConnect</remarks>
// ReSharper disable CheckNamespace, InconsistentNaming
namespace Clarity.Connect.Classes
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("Address")]
    public class Address
    {
        public string CUSTNMBR { get; set; } //CUSTNMBR>>TEST001
        public string ADRSCODE { get; set; } //ADRSCODE>>PRIMARY
        public string SLPRSNID { get; set; }
        public string UPSZONE { get; set; }
        public string SHIPMTHD { get; set; }
        public string TAXSCHID { get; set; }
        public string CNTCPRSN { get; set; }
        public string ADDRESS1 { get; set; } //ADDRESS1>>9417 Great Hills Trail
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string COUNTRY { get; set; }
        public string CITY { get; set; } //CITY>>VeryNewCity
        public string STATE { get; set; }
        public string ZIP { get; set; } //ZIP>>52302
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string PHONE3 { get; set; }
        public string FAX { get; set; }
        public string GPSFOINTEGRATIONID { get; set; }
        public string INTEGRATIONSOURCE { get; set; } //INTEGRATIONSOURCE>>0
        public string INTEGRATIONID { get; set; }
        public Internet_Address Internet_Address { get; set; }
    }

    [Serializable]
    [XmlType("Internet_Address")]
    public class Internet_Address
    {
        public string Master_ID { get; set; } // 3502484
        public string ADRSCODE { get; set; } // Main
        public string Master_Type { get; set; } // CUS
        public string INET1 { get; set; } // wumcsp @verizon.net
        public string INET2 { get; set; }
        public string INET3 { get; set; }
        public string INET4 { get; set; }
        public string INET5 { get; set; } // 35024084
        public string INET6 { get; set; } // a5V56UwJDV
        public string INET7 { get; set; }
        public string INET8 { get; set; }
    }
}
