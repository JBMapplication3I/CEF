// <copyright file="CustomerSerializable.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the customer serializable class</summary>
// <remarks>This file content was copied from ClarityConnect</remarks>
// ReSharper disable CheckNamespace, InconsistentNaming
namespace Clarity.Connect.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Customer
    {
        //public string eConnect ACTION = "0" Requester_DOCTYPE="Customer" DBNAME="UMC" TABLENAME="RM00101" DATE1="1900-01-01T00:00:00" CUSTNMBR="TEST001">
        //public string Customer>
        public string CUSTNMBR { get; set; } //CUSTNMBR>>TEST001
        public string ADDRESS1 { get; set; } //ADDRESS1>>9417 Great Hills Trail
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string ADRSCODE { get; set; } //ADRSCODE>>PRIMARY
        public string CITY { get; set; } //CITY>>VeryNewCity
        public string CNTCPRSN { get; set; }
        public string COUNTRY { get; set; }
        public string CPRCSTNM { get; set; }
        public string CURNCYID { get; set; }
        public string CUSTCLAS { get; set; }
        public int CUSTDISC { get; set; } //CUSTDISC>>0
        public string CUSTNAME { get; set; } //CUSTNAME>>Vince Pernice
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string PHONE3 { get; set; }
        public string FAX { get; set; }
        public string PYMTRMID { get; set; }
        public string SALSTERR { get; set; }
        public string SHIPMTHD { get; set; }
        public string SLPRSNID { get; set; }
        public string STATE { get; set; }
        public string TAXSCHID { get; set; }
        public string TXRGNNUM { get; set; }
        public string UPSZONE { get; set; }
        public string ZIP { get; set; } //ZIP>>52302
        public string STMTNAME { get; set; } //STMTNAME>>Vince Pernice
        public string SHRTNAME { get; set; } //SHRTNAME>>Vince Pernice
        public string PRBTADCD { get; set; } //PRBTADCD>>PRIMARY
        public string PRSTADCD { get; set; } //PRSTADCD>>PRIMARY
        public string STADDRCD { get; set; } //STADDRCD>>PRIMARY
        public string CHEKBKID { get; set; }
        public int CRLMTTY { get; set; } //CRLMTTYP>P>0
        public decimal CRLMTAMT { get; set; } //CRLMTAMT>>0.00000
        public int CRLMTPER { get; set; } //CRLMTPER>>0
        public decimal CRLMTPAM { get; set; } //CRLMTPAM>>0.00000
        public string RATETPID { get; set; }
        public string PRCLEVEL { get; set; }
        public int MINPYTYP { get; set; } //MINPYTYP>>0
        public decimal MINPYDLR { get; set; } //MINPYDLR>>0.00000
        public int MINPYPCT { get; set; } //MINPYPCT>>0
        public int FNCHATYP { get; set; } //FNCHATYP>>0
        public int FNCHPCNT { get; set; } //FNCHPCNT>>0
        public decimal FINCHDLR { get; set; } //FINCHDLR>>0.00000
        public int MXWOFTYP { get; set; } //MXWOFTYP>>0
        public decimal MXWROFAM { get; set; } //MXWROFAM>>0.00000
        public string COMMENT1 { get; set; }
        public string COMMENT2 { get; set; }
        public string USERDEF1 { get; set; }
        public string USERDEF2 { get; set; }
        public string TAXEXMT1 { get; set; }
        public string TAXEXMT2 { get; set; }
        public int BALNCTYP { get; set; } //BALNCTYP>>0
        public int STMTCYCL { get; set; } //STMTCYCL>>5
        public string BANKNAME { get; set; }
        public string BNKBRNCH { get; set; }
        public DateTime FRSTINDT { get; set; } //FRSTINDT>>1900-01-01T00:00:00
        public int INACTIVE { get; set; } //INACTIVE>>0
        public string HOLD { get; set; } //HOLD>>0
        public string CRCARDID { get; set; }
        public string CRCRDNUM { get; set; }
        public DateTime CCRDXPDT { get; set; } //CCRDXPDT>>1900-01-01T00:00:00
        public int KPDSTHST { get; set; } //KPDSTHST>>1
        public int KPCALHST { get; set; } //KPCALHST>>1
        public int KPERHIST { get; set; } //KPERHIST>>1
        public int KPTRXHST { get; set; } //KPTRXHST>>1
        public DateTime CREATDDT { get; set; } //CREATDDT>>2015-09-17T00:00:00
        public DateTime MODIFDT { get; set; } //MODIFDT>>2015-09-17T00:00:00
        public string Revalue_Customer { get; set; } //Revalue_Customer>>1
        public int Post_Results_To { get; set; } //Post_Results_To>>0
        public string FINCHID { get; set; }
        public string GOVCRPID { get; set; }
        public string GOVINDID { get; set; }
        public int DISGRPER { get; set; } //DISGRPER>>0
        public int DUEGRPER { get; set; } //DUEGRPER>>0
        public string DOCFMTID { get; set; }
        public int Send_Email_Statements { get; set; } //Send_Email_Statements>>0
        public string GPSFOINTEGRATIONID { get; set; }
        public int INTEGRATIONSOURCE { get; set; } //INTEGRATIONSOURCE>>0
        public string INTEGRATIONID { get; set; }

        [XmlArray("Addresses")]
        public HashSet<Address> Addresses { get; set; }
    }
}
