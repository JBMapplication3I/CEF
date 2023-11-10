namespace DotNetShipping.ShippingProviders
{
    using System.Threading.Tasks;

    /// <summary>Defines a standard interface for all shipping providers.</summary>
    public interface IShippingProvider
    {
        /// <summary>The name of the provider.</summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>The shipment which contains rates from the provider after calling <see cref="GetRates" />.</summary>
        /// <value>The shipment.</value>
        Shipment Shipment { get; set; }

        /// <summary>Retrieves rates from the provider.</summary>
        /// <returns>The rates.</returns>
        Task GetRates();
    }
}
