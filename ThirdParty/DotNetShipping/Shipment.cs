namespace DotNetShipping
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///     Summary description for Shipment.
    /// </summary>
    public class Shipment
    {
        public readonly Address DestinationAddress;

        public readonly Address OriginAddress;

        public ReadOnlyCollection<Package> Packages;

        public ICollection<IRateAdjuster> RateAdjusters;

        public Shipment(Address originAddress, Address destinationAddress, List<Package> packages)
        {
            OriginAddress = originAddress;
            DestinationAddress = destinationAddress;
            Packages = packages.AsReadOnly();
            Rates = new List<Rate>();
            ServerErrors = new List<USPSError>();
        }

        public int PackageCount => Packages.Count;

        public List<Rate> Rates { get; }

        public List<USPSError> ServerErrors { get; }

        public decimal TotalPackageWeight
        {
            get { return Packages.Sum(x => x.Weight); }
        }
    }
}
