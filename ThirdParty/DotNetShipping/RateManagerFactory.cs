namespace DotNetShipping
{
    using System;
    using System.Linq;
    using System.Reflection;
    using ShippingProviders;

    public class RateManagerFactory
    {
        /// <summary>Builds a Rate Manager and adds the providers.</summary>
        /// <returns>A RateManager.</returns>
        public static RateManager Build()
        {
            var providers = Assembly.GetAssembly(typeof(IShippingProvider))
                .GetTypes()
                .Where(x => x.BaseType == typeof(AbstractShippingProvider) && !x.IsAbstract);
            var rateManager = new RateManager();
            foreach (var provider in providers)
            {
                if (Activator.CreateInstance(provider) is not IShippingProvider instance)
                {
                    continue;
                }
                rateManager.AddProvider(instance);
            }
            return rateManager;
        }
    }
}
