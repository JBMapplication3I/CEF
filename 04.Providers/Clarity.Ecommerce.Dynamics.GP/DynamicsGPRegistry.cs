namespace Clarity.Ecommerce.MicrosoftDynamicsGP
{
    using Interfaces.Workflow;
    using EFMethod.Accounts;
    using StructureMap;

    public class DynamicsGPRegistry : Registry
    {
        public DynamicsGPRegistry()
        {
            For<IAccountController>().Use<AccountController>();
        }
    }
}
