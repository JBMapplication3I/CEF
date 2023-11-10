namespace DotNetShipping.ShippingProviders
{
    using System;
    using System.Collections.Generic;
    using FedExRateService;

    /// <summary>
    ///     Provides rates from FedEx (Federal Express) excluding SmartPost. Please use <see cref="FedExSmartPostProvider" />
    ///     for SmartPost rates.
    /// </summary>
    public class FedExProvider : FedExBaseProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="password"></param>
        /// <param name="accountNumber"></param>
        /// <param name="meterNumber"></param>
        public FedExProvider(string key, string password, string accountNumber, string meterNumber)
        {
            Init(key, password, accountNumber, meterNumber, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="password"></param>
        /// <param name="accountNumber"></param>
        /// <param name="meterNumber"></param>
        /// <param name="useProduction"></param>
        public FedExProvider(string key, string password, string accountNumber, string meterNumber, bool useProduction)
        {
            Init(key, password, accountNumber, meterNumber, useProduction);
        }

        /// <summary>
        ///     Sets service codes.
        /// </summary>
        protected override sealed void SetServiceCodes()
        {
            _serviceCodes = new Dictionary<string, string>
            {
                { "PRIORITY_OVERNIGHT", "FedEx Priority Overnight" },
                { "FEDEX_2_DAY", "FedEx 2nd Day" },
                { "FEDEX_2_DAY_AM", "FedEx 2nd Day A.M." },
                { "STANDARD_OVERNIGHT", "FedEx Standard Overnight" },
                { "FIRST_OVERNIGHT", "FedEx First Overnight" },
                { "FEDEX_EXPRESS_SAVER", "FedEx Express Saver" },
                { "FEDEX_GROUND", "FedEx Ground" },
                { "GROUND_HOME_DELIVERY", "FedEx Ground Residential" },
                { "FEDEX_INTERNATIONAL_GROUND", "FedEx International Ground" },
                { "INTERNATIONAL_ECONOMY", "FedEx International Economy" },
                { "INTERNATIONAL_PRIORITY", "FedEx International Priority" },
            };
        }

        /// <summary>
        ///     Sets shipment details
        /// </summary>
        /// <param name="request"></param>
        protected override sealed void SetShipmentDetails(RateRequest request)
        {
            request.RequestedShipment = new RequestedShipment
            {
                ShipTimestamp = DateTime.Now, // Shipping date and time
                ShipTimestampSpecified = true,
                DropoffType =
                DropoffType
                    .REGULAR_PICKUP, // Drop off types are BUSINESS_SERVICE_CENTER, DROP_BOX, REGULAR_PICKUP, REQUEST_COURIER, STATION
                DropoffTypeSpecified = true,
                PackagingType = "YOUR_PACKAGING"
            };
            //request.RequestedShipment.PackagingTypeSpecified = true;

            SetOrigin(request);

            SetDestination(request);

            SetPackageLineItems(request);

            request.RequestedShipment.RateRequestTypes = new RateRequestType[1];
            request.RequestedShipment.RateRequestTypes[0] = RateRequestType.LIST;
            request.RequestedShipment.PackageCount = Shipment.PackageCount.ToString();
        }

        protected override void Init(string key, string password, string accountNumber, string meterNumber, bool useProduction)
        {
            base.Init(key, password, accountNumber, meterNumber, useProduction);
            SetServiceCodes();
        }
    }
}
