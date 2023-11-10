// <copyright file="MembershipProviderBase.Shared.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership provider base class (Shared part)</summary>
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce.Providers.Memberships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces.Providers.Memberships;
    using JetBrains.Annotations;
    using Utilities;

    /// <summary>A membership provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IMembershipsProviderBase"/>
    public abstract partial class MembershipProviderBase : ProviderBase, IMembershipsProviderBase
    {
        private static readonly Dictionary<string, int> WordToNumberLookup = new()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "ten", 10 },
            { "eleven", 11 },
            { "twelve", 12 },
            { "thirteen", 13 },
            { "fourteen", 14 },
            { "fifteen", 15 },
            { "sixteen", 16 },
            { "seventeen", 17 },
            { "eighteen", 18 },
            { "nineteen", 19 },
            { "twenty", 20 },
            { "thirty", 30 },
            { "forty", 40 },
            { "fifty", 50 },
            { "sixty", 60 },
            { "seventy", 70 },
            { "eighty", 80 },
            { "ninety", 90 },
            { "hundred", 100 },
        };

        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Memberships;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Gets or sets the identifier of the membership product type.</summary>
        /// <value>The identifier of the membership product type.</value>
        private static int ProductTypeIDOfMembership { get; set; }

        /// <summary>Gets or sets the identifier of the kit product type.</summary>
        /// <value>The identifier of the kit product type.</value>
        private static int ProductTypeIDOfKit { get; set; }

        /// <summary>Gets or sets the identifier of the variant kit product type.</summary>
        /// <value>The identifier of the variant kit product type.</value>
        private static int ProductTypeIDOfVariantKit { get; set; }

        /// <summary>Membership calculate date.</summary>
        /// <param name="fromDate">     From date.</param>
        /// <param name="repeatTypeKey">The repeat type key.</param>
        /// <param name="repeatCount">  The repeat count.</param>
        /// <returns>A DateTime.</returns>
        [Pure]
        internal static DateTime MembershipCalculateDate(DateTime fromDate, string repeatTypeKey, int? repeatCount)
        {
            if (repeatCount < 1)
            {
                repeatCount = 1;
            }
            // Eventually boil everything down to a count of months to add, but we need to figure it out from the
            // english language representation
            // Examples of what we think we need to process:
            // * 13 Months
            // * 13Months
            // * 13
            // * 13-months
            // * 13-month
            // * Yearly
            // * 1 year
            // * 2 years
            // * Monthly
            // * quarterly
            // * semi-annual
            // * semiannual
            // * semiyearly
            // * annual
            // * annually
            var matches = Regex.Matches(
                    repeatTypeKey.ToLowerInvariant(),
                    @"(?<numbers>\d+)|(?<words>[a-z]+)",
                    RegexOptions.IgnoreCase)
                .Cast<Match>();
            var count = 0;
            var byYear = new Regex(@"(?:year|annual)(?:ly|s)?");
            var byQuarter = new Regex(@"quarter(?:ly|s)?");
            var byHalf = new Regex(@"semi|half");
            var byDouble = new Regex(@"bi");
            var thinkInYears = false;
            var thinkInQuarters = false;
            var thinkInHalves = false;
            var thinkInDoubles = false;
            foreach (var match in matches)
            {
                if (Contract.CheckValidKey(match.Groups["numbers"]?.Value))
                {
                    count = int.Parse(match.Groups["numbers"].Value);
                    continue;
                }
                if (WordToNumberLookup.ContainsKey(match.Groups["words"].Value))
                {
                    count = WordToNumberLookup[match.Groups["words"].Value];
                    continue;
                }
                if (byYear.IsMatch(match.Groups["words"].Value))
                {
                    thinkInYears = true;
                }
                else if (byQuarter.IsMatch(match.Groups["words"].Value))
                {
                    thinkInQuarters = true;
                }
                if (byHalf.IsMatch(match.Groups["words"].Value))
                {
                    thinkInHalves = true;
                }
                if (byDouble.IsMatch(match.Groups["words"].Value))
                {
                    thinkInDoubles = true;
                }
            }
            if (count <= 0)
            {
                count = 1;
            }
            if (thinkInYears)
            {
                count *= 12;
            }
            else if (thinkInQuarters)
            {
                count *= 3;
            }
            else if (thinkInHalves)
            {
                // thinkInMonths
                // Force doubling
                thinkInDoubles = true;
            }
            if (thinkInHalves)
            {
                count /= 2;
            }
            if (thinkInDoubles)
            {
                count *= 2;
            }
            return fromDate.AddMonths(count * (repeatCount ?? 0));
        }

        ////private static CEFActionResponse<string> ProcessTransactionForApproval(
        ////    IPaymentsProviderBase? gateway,
        ////    IProviderPayment payment,
        ////    IContactModel billing,
        ////    string? contextProfileName)
        ////{
        ////    IPaymentResponse transaction;
        ////    switch (CommonMembershipProviderConfig.PaymentProcess)
        ////    {
        ////        case "AuthorizeOnly":
        ////        {
        ////            transaction = gateway.Authorize(payment, billing, null, contextProfileName);
        ////            break;
        ////        }
        ////        default: // AuthorizeAndCapture
        ////        {
        ////            transaction = gateway.AuthorizeAndACapture(payment, billing, null, contextProfileName);
        ////            break;
        ////        }
        ////    }
        ////    return transaction.Approved
        ////        ? transaction.TransactionID.WrapInPassingCEFAR()
        ////        : CEFAR.FailingCEFAR<string>(transaction.ResponseCode);
        ////}
    }
}
