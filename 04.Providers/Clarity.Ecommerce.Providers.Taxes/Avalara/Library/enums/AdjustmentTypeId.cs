/*
 * AvaTax API Client Library
 *
 * (c) 2004-2018 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Ted Spence
 * @author Zhenya Frolov
 * @author Greg Hester
 */
namespace Avalara.AvaTax.RestClient
{
    /// <summary>Values that represent adjustment type Identifiers.</summary>
    public enum AdjustmentTypeId
    {
        /// <summary>An enum constant representing the other option.</summary>
        Other,

        /// <summary>An enum constant representing the current period rounding option.</summary>
        CurrentPeriodRounding,

        /// <summary>An enum constant representing the prior period rounding option.</summary>
        PriorPeriodRounding,

        /// <summary>An enum constant representing the current period discount option.</summary>
        CurrentPeriodDiscount,

        /// <summary>An enum constant representing the prior period discount option.</summary>
        PriorPeriodDiscount,

        /// <summary>An enum constant representing the current period collection option.</summary>
        CurrentPeriodCollection,

        /// <summary>An enum constant representing the prior period collection option.</summary>
        PriorPeriodCollection,

        /// <summary>An enum constant representing the penalty option.</summary>
        Penalty,

        /// <summary>An enum constant representing the interest option.</summary>
        Interest,

        /// <summary>An enum constant representing the discount option.</summary>
        Discount,

        /// <summary>An enum constant representing the rounding option.</summary>
        Rounding,

        /// <summary>An enum constant representing the csp fee option.</summary>
        CspFee,

        /// <summary>An enum constant representing the marketplace option.</summary>
        Marketplace,
    }
}
