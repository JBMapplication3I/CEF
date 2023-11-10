/*
 * AvaTax API Client Library
 *
 * (c) 2004-2019 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Genevieve Conty
 * @author Greg Hester
 */
namespace Avalara.AvaTax.RestClient
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model for retrieving customer data schema
    /// </summary>
    public class AdvancedRuleCustomerDataSchemaModel
    {
        /// <summary>
        /// Unique identifier for the rule
        /// </summary>
        public string ruleId { get; set; }

        /// <summary>
        /// Customer data schema
        /// </summary>
        public string customerDataSchema { get; set; }

        /// <summary>
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
