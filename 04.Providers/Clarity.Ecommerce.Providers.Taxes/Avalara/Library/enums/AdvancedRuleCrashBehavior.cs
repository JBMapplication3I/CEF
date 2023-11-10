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
    /// <summary>Values that represent advanced rule crash behaviors.</summary>
    public enum AdvancedRuleCrashBehavior
    {
        /// <summary>An enum constant representing the fail on error option.</summary>
        FailOnError = 0,

        /// <summary>An enum constant representing the proceed with original option.</summary>
        ProceedWithOriginal = 1,
    }
}
