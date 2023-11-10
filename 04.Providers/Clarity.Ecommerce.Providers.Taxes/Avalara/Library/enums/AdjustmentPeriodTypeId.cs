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
    /// <summary>Values that represent adjustment period type Identifiers.</summary>
    public enum AdjustmentPeriodTypeId
    {
        /// <summary>An enum constant representing the none option.</summary>
        None = 0,

        /// <summary>An enum constant representing the current period option.</summary>
        CurrentPeriod = 1,

        /// <summary>An enum constant representing the next period option.</summary>
        NextPeriod = 2,
    }
}
