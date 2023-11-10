namespace FixerSharp
{
    using System;

    /// <summary>An exchange rate.</summary>
    public class ExchangeRate
    {
        /// <summary>Initializes a new instance of the <see cref="ExchangeRate"/> class.</summary>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <param name="rate">The rate.</param>
        /// <param name="date">The date Date/Time.</param>
        public ExchangeRate(string from, string to, double rate, DateTime date)
        {
            From = from;
            To = to;
            Rate = rate;
            Date = date;
        }

        /// <summary>Gets the originating currency code.</summary>
        /// <value>The originating currency code.</value>
        /// <seealso cref="Symbols"/>
        public string From { get; }

        /// <summary>Gets the target currency code.</summary>
        /// <value>The target currency code.</value>
        /// <seealso cref="Symbols"/>
        public string To { get; }

        /// <summary>Gets the rate of conversion at the timestamp of <seealso cref="Date"/>.</summary>
        /// <value>The rate.</value>
        public double Rate { get; }

        /// <summary>Gets or sets the Date/Time of the date this exchange rate was recorded.</summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>Converts an amount from the original currency to the target currency described by the codes in
        /// <seealso cref="From"/> and <seealso cref="To"/>.</summary>
        /// <param name="amount">The amount.</param>
        /// <returns>A double.</returns>
        public double Convert(double amount)
        {
            return amount * Rate;
        }
    }
}
