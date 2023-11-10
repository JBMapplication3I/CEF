// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.BIFFRECORDTYPE
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>Values that represent biffrecordtypes.</summary>
    internal enum BIFFRECORDTYPE : ushort
    {
        BLANK_OLD = 1,
        INTEGER_OLD = 2,
        NUMBER_OLD = 3,
        LABEL_OLD = 4,
        BOOLERR_OLD = 5,
        FORMULA_OLD = 6,
        BOF_V2 = 9,

        /// <summary>0x000A.</summary>
        EOF = 10,

        /// <summary>0x000C.</summary>
        CALCCOUNT = 12,

        /// <summary>0x000D.</summary>
        CALCMODE = 13,

        /// <summary>0x000E.</summary>
        PRECISION = 14,

        /// <summary>0x000F.</summary>
        REFMODE = 15,

        /// <summary>0x0010.</summary>
        DELTA = 16,

        /// <summary>0x0011.</summary>
        ITERATION = 17,

        /// <summary>0x0012.</summary>
        PROTECT = 18,

        /// <summary>0x0013.</summary>
        PASSWORD = 19,

        /// <summary>0x0014.</summary>
        HEADER = 20,

        /// <summary>0x0015.</summary>
        FOOTER = 21,

        /// <summary>0x0019.</summary>
        WINDOWPROTECT = 25,

        /// <summary>0x001A.</summary>
        VERTICALPAGEBREAKS = 26,

        /// <summary>0x001C.</summary>
        NOTE = 28,

        /// <summary>0x001D.</summary>
        SELECTION = 29,

        /// <summary>0x001E.</summary>
        FORMAT_V23 = 30,

        /// <summary>0x0022.</summary>
        RECORD1904 = 34,

        /// <summary>0x002A.</summary>
        PRINTHEADERS = 42,

        /// <summary>0x002B.</summary>
        PRINTGRIDLINES = 43,

        /// <summary>0x0031.</summary>
        FONT = 49,

        /// <summary>0x003C.</summary>
        CONTINUE = 60,

        /// <summary>0x003D.</summary>
        WINDOW1 = 61,

        /// <summary>0x0040.</summary>
        BACKUP = 64,

        /// <summary>0x0042.</summary>
        CODEPAGE = 66,

        /// <summary>0x0043.</summary>
        XF_V2 = 67,

        /// <summary>0x0055.</summary>
        DFAULTCOLWIDTH = 85,

        /// <summary>0x0059.</summary>
        XCT = 89,

        /// <summary>0x005C.</summary>
        WRITEACCESS = 92,

        /// <summary>0x005D.</summary>
        OBJ = 93,

        /// <summary>0x005E.</summary>
        UNCALCED = 94,

        /// <summary>0x005F.</summary>
        SAVERECALC = 95,

        /// <summary>0x0080.</summary>
        GUTS = 128,

        /// <summary>0x0081.</summary>
        WSBOOL = 129,

        /// <summary>0x0082.</summary>
        GRIDSET = 130,

        /// <summary>0x0083.</summary>
        HCENTER = 131,

        /// <summary>0x0084.</summary>
        VCENTER = 132,

        /// <summary>0x0085.</summary>
        BOUNDSHEET = 133,

        /// <summary>0x008C.</summary>
        COUNTRY = 140,

        /// <summary>0x008D.</summary>
        HIDEOBJ = 141,

        /// <summary>0x009C.</summary>
        FNGROUPCOUNT = 156,

        /// <summary>0x00A1.</summary>
        PRINTSETUP = 161,

        /// <summary>0x00BC.</summary>
        SHRFMLA_OLD = 188,

        /// <summary>0x00BD.</summary>
        MULRK = 189,

        /// <summary>0x00BE.</summary>
        MULBLANK = 190,

        /// <summary>0x00C1.</summary>
        MMS = 193,

        /// <summary>0x00C6.</summary>
        SXDB = 198,

        /// <summary>0x00D6.</summary>
        RSTRING = 214,

        /// <summary>0x00D7.</summary>
        DBCELL = 215,

        /// <summary>0x00DA.</summary>
        BOOKBOOL = 218,

        /// <summary>0x00DC.</summary>
        PARAMQRY = 220,

        /// <summary>0x00DC.</summary>
        SXEXT = 220,

        /// <summary>0x00E0.</summary>
        XF = 224,

        /// <summary>0x00E1.</summary>
        INTERFACEHDR = 225,

        /// <summary>0x00E2.</summary>
        INTERFACEEND = 226,

        /// <summary>0x00EB.</summary>
        MSODRAWINGGROUP = 235,

        /// <summary>0x00EC.</summary>
        MSODRAWING = 236,

        /// <summary>0x00ED.</summary>
        MSODRAWINGSELECTION = 237,

        /// <summary>0x00F0.</summary>
        SXRULE = 240,

        /// <summary>0x00F1.</summary>
        SXEX = 241,

        /// <summary>0x00F2.</summary>
        SXFILT = 242,

        /// <summary>0x00F6.</summary>
        SXNAME = 246,

        /// <summary>0x00F7.</summary>
        SXSELECT = 247,

        /// <summary>0x00F8.</summary>
        SXPAIR = 248,

        /// <summary>0x00F9.</summary>
        SXFMLA = 249,

        /// <summary>0x00FB.</summary>
        SXFORMAT = 251,

        /// <summary>0x00FC.</summary>
        SST = 252,

        /// <summary>0x00FD.</summary>
        LABELSST = 253,

        /// <summary>0x00FF.</summary>
        EXTSST = 255,

        /// <summary>0x0100.</summary>
        SXVDEX = 256,

        /// <summary>0x0103.</summary>
        SXFORMULA = 259,

        /// <summary>0x0122.</summary>
        SXDBEX = 290,

        /// <summary>0x013D.</summary>
        TABID = 317,

        /// <summary>0x0160.</summary>
        USESELFS = 352,

        /// <summary>0x0161.</summary>
        DSF = 353,

        /// <summary>0x0162.</summary>
        XL5MODIFY = 354,

        /// <summary>0x01A9.</summary>
        USERBVIEW = 425,

        /// <summary>0x01AA.</summary>
        USERSVIEWBEGIN = 426,

        /// <summary>0x01AB.</summary>
        USERSVIEWEND = 427,

        /// <summary>0x01AD.</summary>
        QSI = 429,

        /// <summary>0x01AE.</summary>
        SUPBOOK = 430,

        /// <summary>0x01AF.</summary>
        PROT4REV = 431,

        /// <summary>0x01B0.</summary>
        CONDFMT = 432,

        /// <summary>0x01B1.</summary>
        CF = 433,

        /// <summary>0x01B2.</summary>
        DVAL = 434,

        /// <summary>0x01B5.</summary>
        DCONBIN = 437,

        /// <summary>0x01B6.</summary>
        TXO = 438,

        /// <summary>0x01B7.</summary>
        REFRESHALL = 439,

        /// <summary>0x01B8.</summary>
        HLINK = 440,

        /// <summary>0x01BA.</summary>
        CODENAME = 442,

        /// <summary>0x01BB.</summary>
        SXFDBTYPE = 443,

        /// <summary>0x01BC.</summary>
        PROT4REVPASSWORD = 444,

        /// <summary>0x01BE.</summary>
        DV = 446,

        /// <summary>0x0200.</summary>
        DIMENSIONS = 512,

        /// <summary>0x0201.</summary>
        BLANK = 513,

        /// <summary>0x0202.</summary>
        INTEGER = 514,

        /// <summary>0x0203.</summary>
        NUMBER = 515,

        /// <summary>0x0204.</summary>
        LABEL = 516,

        /// <summary>0x0205.</summary>
        BOOLERR = 517,

        /// <summary>0x0207.</summary>
        STRING = 519,

        /// <summary>0x0208.</summary>
        ROW = 520,

        /// <summary>0x0209.</summary>
        BOF_V3 = 521,

        /// <summary>0x020B.</summary>
        INDEX = 523,

        /// <summary>0x0221.</summary>
        ARRAY = 545,

        /// <summary>0x0225.</summary>
        DEFAULTROWHEIGHT = 549,

        /// <summary>0x0231.</summary>
        FONT_V34 = 561,

        /// <summary>0x023E.</summary>
        WINDOW2 = 574,

        /// <summary>0x0243.</summary>
        XF_V3 = 579,

        /// <summary>0x027E.</summary>
        RK = 638,

        /// <summary>0x0293.</summary>
        STYLE = 659,

        /// <summary>0x0406.</summary>
        FORMULA = 1030,

        /// <summary>0x0409.</summary>
        BOF_V4 = 1033,

        /// <summary>0x041E.</summary>
        FORMAT = 1054,

        /// <summary>0x0443.</summary>
        XF_V4 = 1091,

        /// <summary>0x04BC.</summary>
        SHRFMLA = 1212,

        /// <summary>0x0800.</summary>
        QUICKTIP = 2048,

        /// <summary>0x0809.</summary>
        BOF = 2057,
    }
}
