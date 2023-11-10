using System;
using System.Text;

namespace JPMC.MSDK.Filer
{
	/// <summary>
	/// Summary description for OrderSeparator.
	/// </summary>
	public class OrderSeparator
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly string PREFIX = "Order";
		private string type = "Or";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OrderSeparator()
		{
		}

		/// <summary>
		/// Constructs a separator, using the supplied record to fill the fields.
		/// </summary>
		/// <param name="record"></param>
		public OrderSeparator(string record)
		{
			var rec = record.Remove(0, OrderSeparator.PREFIX.Length);
			this.type = rec.Substring(0, 3);
			rec = rec.Remove(0, 3);
			this.Size = long.Parse(rec.Substring(0, 20));
			this.OrderCount = long.Parse(rec.Substring(20, 20));
			this.RecordCount = long.Parse(rec.Substring(40, 20));
			this.Totals = long.Parse(rec.Substring(60, 20));
			this.TotalSales = long.Parse(rec.Substring(80, 20));
			this.TotalRefunds = long.Parse(rec.Substring(100, 20));
		}

		/// <summary>
		/// Converts an order separator into a record.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			try
			{
				var builder = new StringBuilder(OrderSeparator.PREFIX);
				builder.AppendFormat("{0, -3}", type);
				builder.AppendFormat("{0,20}", Size);
				builder.AppendFormat("{0,20}", OrderCount);
				builder.AppendFormat("{0,20}", RecordCount);
				builder.AppendFormat("{0,20}", Totals);
				builder.AppendFormat("{0,20}", TotalSales);
				builder.AppendFormat("{0,20}", TotalRefunds);
            
				var result = $"{builder.ToString(),-128}";
				return result;
			}
			catch (Exception ex)
			{
				throw new FilerException(Error.InvalidOrderSeparator, "The order separator includes invalid data.", ex);
			}
		}

		/// <summary>
		/// The number of records in the order.
		/// </summary>
        public long Size { get; set; }

		/// <summary>
		/// The type of order.
		/// </summary>
		/// <remarks>
		/// This is typically either "Sp" for Special or "Or" for Order.
		/// </remarks>
		public string Type
		{
			get => this.type.Trim();
            set => type = value;
        }

		/// <summary>
		/// The totals value.
		/// </summary>
        public long Totals { get; set; }

		/// <summary>
		/// The number of records in the file.
		/// </summary>
        public long RecordCount { get; set; }

		/// <summary>
		/// The number of orders in the file.
		/// </summary>
        public long OrderCount { get; set; }

		/// <summary>
		/// The total refund amount.
		/// </summary>
        public long TotalRefunds { get; set; }

		/// <summary>
		/// The total sales amount.
		/// </summary>
        public long TotalSales { get; set; }
	}
}
