// ReSharper disable CheckNamespace, InconsistentNaming
namespace Clarity.Connect.Shared
{
    using Classes;
    using System;

    [Serializable]
    public class eConnect
    {
        public string ACTION { get; set; } // "0"
        public string Requester_DOCTYPE { get; set; } // "Customer"
        public string DBNAME { get; set; } // "UMC"
        public string TABLENAME { get; set; } // "RM00101"
        public DateTime DATE1 { get; set; } // "1900-01-01T00:00:00"

        public string CUSTNMBR { get; set; } // "TEST001"
        public Customer Customer { get; set; }

        //public Sales SO_Trans { get; set; }
        //public string SOPNUMBE { get; set; }

        //public PricePoint ItemPriceLevels { get; set; }
        //public string UOMPRICE { get; set; }

        //public Item Item { get; set; }
        //public List<Quantity> ItemQuantities { get; set; }
        //public string ITEMNMBR { get; set; }
    }
}
