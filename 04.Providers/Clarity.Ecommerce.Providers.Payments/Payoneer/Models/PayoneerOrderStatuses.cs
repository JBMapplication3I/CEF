// <copyright file="PayoneerOrderStatuses.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer order statuses class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using JetBrains.Annotations;

    /// <summary>Values that represent payoneer order statuses.</summary>
    [PublicAPI]
    public enum PayoneerOrderStatuses
    {
        /// <summary>A brand-new Order.</summary>
        New = 0,

        /// <summary>The Order has been sent to the relevant parties.</summary>
        Sent = 1,

        /// <summary>Payment for the Order has been made, and is held in escrow.</summary>
        Paid = 2,

        /// <summary>Goods to complete the order have been shipped.</summary>
        Shipped = 3,

        /// <summary>Goods to complete the order have been delivered to the buyer.</summary>
        Delivered = 4,

        /// <summary>Payment for the Order has been released from escrow.</summary>
        Released = 5,

        /// <summary>Seller's banking details are missing or incomplete.</summary>
        PendingIncomplete = 6,

        /// <summary>There was an error transferring payment to the seller. This is most often caused by incorrect banking
        /// details.</summary>
        PendingError = 7,

        /// <summary>A Dispute has been initiated for this Order.</summary>
        Dispute = 8,

        /// <summary>The Order is complete, and final payment has been made. This is an internal Armor Payments status, and
        /// indicates that Armor Payments has closed the books on this order. This will occur sometime after your users have
        /// finished their last interaction with the order. It is included here because if you look up an old order, you may
        /// see this status and need to know what it represents, but is for reference only.</summary>
        Complete = 9,

        /// <summary>The Order has been cancelled.</summary>
        Cancelled = 10,

        /// <summary>The Dispute for this Order has been escalated to 3rd party arbitration.</summary>
        Arbitration = 11,

        /// <summary>A milestone order with at least one completed milestone, awaiting next milestone completion.</summary>
        MilestoneProgress = 15,

        /// <summary>The milestone order has a completed milestone, awaiting release of milestone payment by buyer.</summary>
        MilestonePending = 16,

        /// <summary>A non-milestone service order awaiting completion by service provider.</summary>
        ServiceProgress = 17,

        /// <summary>A non-milestone services order awaiting payment release by buyer.</summary>
        ServicePending = 18,

        /// <summary>Order has been cancelled, and archived.</summary>
        CancelledFinal = 98,

        /// <summary>The Order is complete, and has been archived.</summary>
        Archive = 99,

        /// <summary>There is an error with the Order.</summary>
        Error = 255,
    }
}
