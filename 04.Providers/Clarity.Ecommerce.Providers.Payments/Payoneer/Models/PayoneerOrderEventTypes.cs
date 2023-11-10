// <copyright file="PayoneerOrderEventTypes.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer order event types class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using JetBrains.Annotations;

    /// <summary>Values that represent payoneer order event types.</summary>
    [PublicAPI]
    public enum PayoneerOrderEventTypes
    {
        /// <summary>Order created.</summary>
        OrderCreate = 0,

        /// <summary>Order sent to involved parties.</summary>
        Sent = 1,

        /// <summary>Payment in full for order received by Armor Payments and placed in secure escrow.</summary>
        Paid = 2,

        /// <summary>Goods shipped to buyer.</summary>
        GoodsShipped = 3,

        /// <summary>Goods received by buyer.</summary>
        GoodsReceived = 4,

        /// <summary>Dispute initiated.</summary>
        Dispute = 5,

        /// <summary>Payment released to seller and/or payees for order.</summary>
        FundsReleased = 6,

        /// <summary>Dispute for order escalated to arbitration.</summary>
        Arbitration = 7,

        /// <summary>Document added to order.</summary>
        UploadDocument = 9,

        /// <summary>Offer made to settle dispute on order.</summary>
        DisputeOffer = 10,

        /// <summary>Offer to settle dispute on order accepted.</summary>
        OfferAccepted = 11,

        /// <summary>Offer to settle dispute on order rejected. </summary>
        /// <deprecated>Use of this event is now deprecated. Offers will be countered, rather than rejected.</deprecated>
        [Obsolete("Use of this event is now deprecated. Offers will be countered, rather than rejected.", true)]
        OfferRejected = 12,

        /// <summary>Counter-offer made to settle dispute.</summary>
        OfferCountered = 13,

        /// <summary>Note added to order.</summary>
        NoteAdded = 14,

        /// <summary>Shipment added to order (<seealso cref="GoodsShipped"/>).</summary>
        ShipmentAdded = 15,

        /// <summary>Order cancelled.</summary>
        Cancelled = 16,

        /// <summary>Order insured.</summary>
        Insured = 17,

        /// <summary>Order milestone reached.</summary>
        MilestoneReached = 18,

        /// <summary>Payment for completed milestone released from secure escrow.</summary>
        MilestoneReleased = 19,

        /// <summary>Service provider indicated services completed.</summary>
        ServiceComplete = 20,

        /// <summary>A balance transfer was made from this order to another.</summary>
        BalanceXfrOut = 21,

        /// <summary>A balance transfer was made from another order to this one.</summary>
        BalanceXfrIn = 22,

        /// <summary>A disbursement payment was added to the order.</summary>
        DisbursementAdded = 23,

        /// <summary>A payment was received in the secure escrow account for this order.</summary>
        PaymentAdded = 24,

        /// <summary>Goods inspection completed.</summary>
        Inspected = 26,

        /// <summary>Service provider requested release of funds from escrow, but at a lower amount than planned.</summary>
        ServiceCompletePartial = 27,

        /// <summary>New payer added as an ad hoc participant in the order.</summary>
        PayerAdded = 28,

        /// <summary>Payer's participation in the order accepted by the seller.</summary>
        PayerAccepted = 29,

        /// <summary>Payer's participation in the order rejected by the seller.</summary>
        PayerRejected = 30,

        /// <summary>Domain marked as having been transferred to the buyer.</summary>
        DomainTransferred = 31,

        /// <summary>Payment failed.</summary>
        PaymentFailed = 32,

        /// <summary>Order flagged for manual review.</summary>
        OrderUnderReview = 33,

        /// <summary>Order is approved following manual review.</summary>
        OrderPassedReview = 34,

        /// <summary>Order is declined following manual review.</summary>
        OrderFailedReview = 35,

        /// <summary>Sent agreement to participant.</summary>
        AgreementSent = 36,

        /// <summary>Participant signed the agreement.</summary>
        AgreementSigned = 37,

        /// <summary>Payoneer Escrow confirmed the agreement.</summary>
        AgreementConfirmed = 38,

        /// <summary>Domain transfer details given.</summary>
        DomainDetailsGiven = 39,

        /// <summary>Domain transfer into Payoneer Escrow complete.</summary>
        DomainXfrIn = 40,

        /// <summary>Domain transfer away from Payoneer Escrow complete.</summary>
        DomainXfrOut = 41,

        /// <summary>Installment period started.</summary>
        PaymentSeriesStart = 42,

        /// <summary>Installment period ended.</summary>
        PaymentSeriesEnd = 43,

        /// <summary>Payment past due.</summary>
        PaymentPastDue = 44,

        /// <summary>The order was cancelled due to nonpayment of installment.</summary>
        CancelledInstallmentNonpayment = 45,

        /// <summary>Current order milestone is fully funded.</summary>
        MilestoneFunded = 46,
    }
}
