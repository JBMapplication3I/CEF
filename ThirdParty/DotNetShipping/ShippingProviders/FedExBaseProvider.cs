namespace DotNetShipping.ShippingProviders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using FedExRateService;

    public abstract class FedExBaseProvider : AbstractShippingProvider
    {
        protected string _accountNumber;

        /// <summary>FedEx allows insured values for items being shipped except when utilizing SmartPost. This setting
        /// will this value to be overwritten.</summary>
        protected bool _allowInsuredValues = true;

        protected string _key;

        protected string _meterNumber;

        protected string _password;

        protected Dictionary<string, string> _serviceCodes;

        protected bool _useProduction = true;

        protected FedExBaseProvider()
        {
            // Don't allow overriding this constructor
        }

        /// <summary>Gets rates.</summary>
        /// <returns>The rates.</returns>
        public override async Task GetRates()
        {
            var request = CreateRateRequest();
            var service = new RatePortTypeClient(/*_useProduction*/);
            try
            {
                // Call the web service passing in a RateRequest and returning a RateReply
                var reply = await service.getRatesAsync(request);
                //
                if (reply.RateReply.HighestSeverity == NotificationSeverityType.SUCCESS
                    || reply.RateReply.HighestSeverity == NotificationSeverityType.NOTE
                    || reply.RateReply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    ProcessReply(reply.RateReply);
                }
                ShowNotifications(reply.RateReply);
            }
            // catch (SoapException e)
            // {
            //     Debug.WriteLine(e.Detail.InnerText);
            // }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>Gets service codes.</summary>
        /// <returns>The service codes.</returns>
        public IDictionary<string, string> GetServiceCodes()
        {
            if (_serviceCodes != null && _serviceCodes.Count > 0)
            {
                return new Dictionary<string, string>(_serviceCodes);
            }

            return null;
        }

        /// <summary>
        ///     Outputs the notifications to the debug console
        /// </summary>
        /// <param name="reply"></param>
        protected static void ShowNotifications(RateReply reply)
        {
            Debug.WriteLine("Notifications");
            for (var i = 0; i < reply.Notifications.Length; i++)
            {
                var notification = reply.Notifications[i];
                Debug.WriteLine("Notification no. {0}", i);
                Debug.WriteLine(" Severity: {0}", notification.Severity);
                Debug.WriteLine(" Code: {0}", notification.Code);
                Debug.WriteLine(" Message: {0}", notification.Message);
                Debug.WriteLine(" Source: {0}", notification.Source);
            }
        }

        /// <summary>
        ///     Creates the rate request
        /// </summary>
        /// <returns></returns>
        protected RateRequest CreateRateRequest()
        {
            // Build the RateRequest
            var request = new RateRequest
            {
                WebAuthenticationDetail = new WebAuthenticationDetail
                {
                    UserCredential = new WebAuthenticationCredential
                    {
                        Key = _key,
                        Password = _password
                    }
                },

                ClientDetail = new ClientDetail
                {
                    AccountNumber = _accountNumber,
                    MeterNumber = _meterNumber
                },

                Version = new VersionId(),

                ReturnTransitAndCommit = true,
                ReturnTransitAndCommitSpecified = true
            };

            SetShipmentDetails(request);

            return request;
        }

        /// <summary>
        ///     Processes the reply
        /// </summary>
        /// <param name="reply"></param>
        protected void ProcessReply(RateReply reply)
        {
            foreach (var rateReplyDetail in reply.RateReplyDetails)
            {
                var netCharge =
                    rateReplyDetail.RatedShipmentDetails.Max(x => x.ShipmentRateDetail.TotalNetCharge.Amount);

                var key = rateReplyDetail.ServiceType.ToString();
                var deliveryDate = rateReplyDetail.DeliveryTimestampSpecified
                    ? rateReplyDetail.DeliveryTimestamp
                    : DateTime.Now.AddDays(30);
                AddRate(key, _serviceCodes[key], netCharge, deliveryDate);
            }
        }

        /// <summary>
        ///     Sets the destination
        /// </summary>
        /// <param name="request"></param>
        protected void SetDestination(RateRequest request)
        {
            request.RequestedShipment.Recipient = new Party
            {
                Address = new Address
                {
                    StreetLines = new string[1] { "" },
                    City = "",
                    StateOrProvinceCode = "",
                    PostalCode = Shipment.DestinationAddress.PostalCode,
                    CountryCode = Shipment.DestinationAddress.CountryCode,
                    Residential = Shipment.DestinationAddress.IsResidential,
                    ResidentialSpecified =
                Shipment.DestinationAddress.IsResidential
                }
            };
        }

        /// <summary>
        ///     Sets the origin
        /// </summary>
        /// <param name="request"></param>
        protected void SetOrigin(RateRequest request)
        {
            request.RequestedShipment.Shipper = new Party
            {
                Address = new Address
                {
                    StreetLines = new string[1] { "" },
                    City = "",
                    StateOrProvinceCode = "",
                    PostalCode = Shipment.OriginAddress.PostalCode,
                    CountryCode = Shipment.OriginAddress.CountryCode,
                    Residential = Shipment.OriginAddress.IsResidential,
                    ResidentialSpecified = Shipment.OriginAddress.IsResidential
                }
            };
        }

        /// <summary>
        ///     Sets package line items
        /// </summary>
        /// <param name="request"></param>
        protected void SetPackageLineItems(RateRequest request)
        {
            request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[Shipment.PackageCount];

            var i = 0;
            foreach (var package in Shipment.Packages)
            {
                request.RequestedShipment.RequestedPackageLineItems[i] = new RequestedPackageLineItem
                {
                    SequenceNumber = (i + 1).ToString(),
                    GroupPackageCount = "1",
                    // package weight
                    Weight = new Weight
                    {
                        Units = WeightUnits.LB,
                        Value = package.RoundedWeight
                    },
                    // package dimensions
                    Dimensions = new Dimensions
                    {
                        Length =
                    package.RoundedLength.ToString(),
                        Width =
                    package.RoundedWidth.ToString(),
                        Height =
                    package.RoundedHeight.ToString(),
                        Units = LinearUnits.IN
                    }
                };

                if (_allowInsuredValues)
                {
                    // package insured value
                    request.RequestedShipment.RequestedPackageLineItems[i].InsuredValue = new Money
                    {
                        Amount = package.InsuredValue,
                        AmountSpecified = true,
                        Currency = "USD"
                    };
                }

                if (package.SignatureRequiredOnDelivery)
                {
                    var signatureOptionDetail = new SignatureOptionDetail { OptionType = SignatureOptionType.DIRECT };
                    var specialServicesRequested =
                        new PackageSpecialServicesRequested { SignatureOptionDetail = signatureOptionDetail };

                    request.RequestedShipment.RequestedPackageLineItems[i].SpecialServicesRequested =
                        specialServicesRequested;
                }

                i++;
            }
        }

        /// <summary>
        ///     Sets service codes.
        /// </summary>
        protected abstract void SetServiceCodes();

        /// <summary>
        ///     Sets shipment details
        /// </summary>
        /// <param name="request"></param>
        protected abstract void SetShipmentDetails(RateRequest request);

        protected virtual void Init(string key, string password, string accountNumber, string meterNumber, bool useProduction)
        {
            Name = "FedEx";

            _key = key;
            _password = password;
            _accountNumber = accountNumber;
            _meterNumber = meterNumber;
            _useProduction = useProduction;
        }
    }
}
