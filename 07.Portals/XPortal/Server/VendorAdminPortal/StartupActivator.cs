namespace Clarity.Ecommerce.UI.XPortal.Server
{
    using Microsoft.Extensions.Configuration;
    using ServiceStack;

    public class StartupActivator : ModularStartupActivator
    {
        public StartupActivator(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
