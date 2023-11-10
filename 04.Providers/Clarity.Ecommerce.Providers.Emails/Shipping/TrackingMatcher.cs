// <copyright file="TrackingMatcher.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tracking matcher class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>A tracking matcher.</summary>
    internal class TrackingMatcher
    {
        /// <summary>The matchers.</summary>
        /* Direct URL for UPS shipment tracking:
         *      http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=1ZXXXXXXXXXXXXXXXX
         * If any value is provided for the "track" parameter then the confirmation page is skipped and the user goes directly to the tracking page.
         * If you'd rather send the user to the confirmation page then just omit that parameter.
         * Direct URL For UPS Mail Innovations tracking:
         *      https://www.ups-mi.net/packageID/packageid.aspx?pid=XXXXXXXXXXXXXXXXX
         * Direct URL for FedEx shipment tracking:
         *      http://www.fedex.com/Tracking?action=track&tracknumbers=XXXXXXXXXXXXXXXXX
         * Direct URL for USPS shipment tracking
         *      https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=XXXXXXXXXXXXXXXXX
         * Direct URL for DHL US shipment tracking
         *      http://track.dhl-usa.com/TrackByNbr.asp?ShipmentNumber=XXXXXXXXXXXXXXXXX
         *  Direct URL for DHL Express shipment tracking
         *      http://www.dhl.com/en/express/tracking.html?AWB=XXXXXXXXXXXXXXXXX
         * Direct URL for DHL Global shipment tracking
         *      http://webtrack.dhlglobalmail.com/?mobile=&trackingnumber=XXXXXXXXXXXXXXXXX
         * Direct URL for OnTrac shipment tracking
         *      http://www.ontrac.com/trackingdetail.asp?tracking=XXXXXXXXXXXXXXXXX
         * Direct URL for ICC World shipment tracking
         *      http://iccworld.com/track.asp?txtawbno=XXXXXXXXXXXXXXXXX
         * Direct URL for LaserShip shipment tracking
         *      http://www.lasership.com/track.php?track_number_input=XXXXXXXXXXXXXXXXX
         * Direct URL for Canada Post shipment tracking
         *      http://www.canadapost.ca/cpotools/apps/track/personal/findByTrackNumber?trackingNumber=XXXXXXXXXXXXXXXXX&amp;LOCALE=en
         * (change LOCALE= en to LOCALE= fr for results in French)
         * Direct URL for Averitt Express shipment tracking
         *      https://www.averittexpress.com/trackLTLById.avrt?serviceType=LTL&resultsPageTitle=LTL+Tracking+by+PRO+and+BOL&trackPro=XXXXXXXXXXXXXXXXX
         * Direct URL for Conway Freight shipment tracking
         *      https://www.con-way.com/webapp/manifestrpts_p_app/shipmentTracking.do?PRO=XXXXXXXXXXXXXXXXX
         * Direct URL for Old Dominion shipment tracking
         *      https://www.odfl.com/Trace/standardResult.faces?pro=XXXXXXXXXXXXXXXXX
         * Direct URL for YRC shipment tracking
         *      http://www.usfc.com/shipmentStatus/track.do?proNumber=XXXXXXXXXXXXXXXXX
         * R + L Carriers
         *      http://www2.rlcarriers.com/freight/shipping/shipment-tracing?pro=XXXXXXXXXXXXXXXXX&docType=PRO
         */
        internal static readonly List<TrackingMatcher> Matchers = new()
        {
            new()
            {
                Carrier = "UPS",
                Method = "Standard",
                Regex = new(@"/\b(1Z ?[0-9A-Z]{3} ?[0-9A-Z]{3} ?[0-9A-Z]{2} ?[0-9A-Z]{4} ?[0-9A-Z]{3} ?[0-9A-Z]|[\dT]\d\d\d ?\d\d\d\d ?\d\d\d)\b/"),
                Link = "http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=",
            },
            new()
            {
                Carrier = "UPS",
                Method = "Mail Innovations",
                Regex = new(@"/\b(1Z ?[0-9A-Z]{3} ?[0-9A-Z]{3} ?[0-9A-Z]{2} ?[0-9A-Z]{4} ?[0-9A-Z]{3} ?[0-9A-Z]|[\dT]\d\d\d ?\d\d\d\d ?\d\d\d|\d{22})\b/i"),
                Link = "https://www.ups-mi.net/packageID/packageid.aspx?pid=",
            },
            new()
            {
                Carrier = "FedEx",
                Method = "Standard 1",
                Regex = new(@"/(\b96\d{20}\b)|(\b\d{15}\b)|(\b\d{12}\b)/"),
                Link = "http://www.fedex.com/Tracking?action=track&tracknumbers=",
            },
            new()
            {
                Carrier = "FedEx",
                Method = "Standard 2",
                Regex = new(@"/\b((98\d\d\d\d\d?\d\d\d\d|98\d\d) ?\d\d\d\d ?\d\d\d\d( ?\d\d\d)?)\b/"),
                Link = "http://www.fedex.com/Tracking?action=track&tracknumbers=",
            },
            new()
            {
                Carrier = "FedEx",
                Method = "Standard 3",
                Regex = new(@"/^[0-9]{15}$/"),
                Link = "http://www.fedex.com/Tracking?action=track&tracknumbers=",
            },
            new()
            {
                Carrier = "USPS",
                Method = "Standard 1",
                Regex = new(@"/(\b\d{30}\b)|(\b91\d+\b)|(\b\d{20}\b)/"),
                Link = "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=",
            },
            new()
            {
                Carrier = "USPS",
                Method = "Standard 2",
                Regex = new(@"/^E\D{1}\d{9}\D{2}$|^9\d{15,21}$/"),
                Link = "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=",
            },
            new()
            {
                Carrier = "USPS",
                Method = "Standard 3",
                Regex = new(@"/^91[0-9]+$/"),
                Link = "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=",
            },
            new()
            {
                Carrier = "USPS",
                Method = "Standard 4",
                Regex = new(@"/^[A-Za-z]{2}[0-9]+US$/"),
                Link = "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=",
            },
        };

        /// <summary>Gets or sets the carrier.</summary>
        /// <value>The carrier.</value>
        internal string Carrier { get; set; } = null!;

        /// <summary>Gets or sets the method.</summary>
        /// <value>The method.</value>
        internal string Method { get; set; } = null!;

        /// <summary>Gets or sets the RegEx.</summary>
        /// <value>The RegEx.</value>
        internal Regex Regex { get; set; } = null!;

        /// <summary>Gets or sets the link.</summary>
        /// <value>The link.</value>
        internal string Link { get; set; } = null!;
    }
}
