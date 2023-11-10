namespace FixerSharp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>A fixer.</summary>
    public class Fixer
    {
        /// <summary>(Immutable) URI of the base.</summary>
        private const string BaseUri = "http://data.fixer.io/api/";

        /// <summary>The API key.</summary>
        private static string? _apiKey;

        /// <summary>Gets or sets the API key.</summary>
        /// <value>The API key.</value>
        private static string ApiKey
        {
            get => !string.IsNullOrWhiteSpace(_apiKey)
                ? _apiKey!
                : throw new InvalidOperationException("Fixer.io now requires an API key! Call .SetApiKey(\"key\") first");
            set => _apiKey = value;
        }

        /// <summary>Converts.</summary>
        /// <param name="from">  Source for the.</param>
        /// <param name="to">    to.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="date">  The date.</param>
        /// <returns>A double.</returns>
        public static double Convert(string from, string to, double amount, DateTime? date = null)
        {
            return GetRate(from, to, date).Convert(amount);
        }

        /// <summary>Convert an original value in the from currency to the to currency. Can optionally specify a date to
        /// get a historical conversion.</summary>
        /// <param name="from">  Source for the.</param>
        /// <param name="to">    to.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="date">  The date.</param>
        /// <returns>The converted value.</returns>
        public static async Task<double> ConvertAsync(string from, string to, double amount, DateTime? date = null)
        {
            return (await GetRateAsync(from, to, date)).Convert(amount);
        }

        /// <summary>Rates.</summary>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <param name="date">The date.</param>
        /// <returns>An ExchangeRate.</returns>
        public static ExchangeRate Rate(string from, string to, DateTime? date = null)
        {
            return GetRate(from, to, date);
        }

        /// <summary>Rate asynchronous.</summary>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <param name="date">The date.</param>
        /// <returns>A Task{ExchangeRate}.</returns>
        public static async Task<ExchangeRate> RateAsync(string from, string to, DateTime? date = null)
        {
            return await GetRateAsync(from, to, date);
        }

        /// <summary>Sets API key.</summary>
        /// <param name="apiKey">The API key.</param>
        public static void SetApiKey(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>Gets a rate.</summary>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <param name="date">The date.</param>
        /// <returns>The rate.</returns>
        private static ExchangeRate GetRate(string from, string to, DateTime? date = null)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            if (!Symbols.IsValid(from))
            {
                throw new ArgumentException("Symbol not found for provided currency", nameof(from));
            }
            if (!Symbols.IsValid(to))
            {
                throw new ArgumentException("Symbol not found for provided currency", nameof(to));
            }
            var url = GetFixerUrl(date);
            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return ParseData(response.Content.ReadAsStringAsync().Result, from, to);
        }

        /// <summary>Gets rate asynchronous.</summary>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <param name="date">The date.</param>
        /// <returns>The rate asynchronous.</returns>
        private static async Task<ExchangeRate> GetRateAsync(string from, string to, DateTime? date = null)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            if (!Symbols.IsValid(from))
            {
                throw new ArgumentException("Symbol not found for provided currency", nameof(from));
            }
            if (!Symbols.IsValid(to))
            {
                throw new ArgumentException("Symbol not found for provided currency", nameof(to));
            }
            var url = GetFixerUrl(date);
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return ParseData(await response.Content.ReadAsStringAsync(), from, to);
        }

        /// <summary>Parse data.</summary>
        /// <param name="data">The data.</param>
        /// <param name="from">The originating currency code. <seealso cref="Symbols"/></param>
        /// <param name="to">  The target currency code. <seealso cref="Symbols"/></param>
        /// <returns>An ExchangeRate.</returns>
        private static ExchangeRate ParseData(string data, string from, string to)
        {
            // Parse JSON
            var root = JObject.Parse(data);
            var rates = root.Value<JObject>("rates");
            if (rates is null)
            {
                throw new ArgumentException("Unable to parse rates object from reply");
            }
            var fromRate = rates.Value<double>(from);
            var toRate = rates.Value<double>(to);
            var rate = toRate / fromRate;
            // Parse returned date
            // Note: This may be different to the requested date as Fixer will return the closest available
            var returnedDate = DateTime.ParseExact(
                root.Value<string>("date") ?? throw new ArgumentException("Unable to parse rate date"),
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture);
            return new ExchangeRate(from, to, rate, returnedDate);
        }

        /// <summary>Gets fixer URL.</summary>
        /// <param name="date">The date.</param>
        /// <returns>The fixer URL.</returns>
        private static string GetFixerUrl(DateTime? date = null)
        {
            return $"{BaseUri}{(date.HasValue ? date.Value.ToString("yyyy-MM-dd") : "latest")}?access_key={ApiKey}";
        }
    }
}
