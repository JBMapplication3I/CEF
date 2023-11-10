namespace ServiceStack
{
#if !(SL5 || XBOX || ANDROID || __IOS__ || __MAC__ || PCL || NETSTANDARD1_1 || NETSTANDARD1_6)
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using ServiceStack.Text;

    public class Soap12ServiceClient : WcfServiceClient
    {
        public Soap12ServiceClient(string uri)
        {
            Uri = uri.WithTrailingSlash() + "Soap12";
            StoreCookies = true;
        }

        private WSHttpBinding binding;

        private Binding WsHttpBinding
        {
            get
            {
                if (binding == null)
                {
                    binding = new()
                    {
                        MaxReceivedMessageSize = int.MaxValue,
                        HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                        MaxBufferPoolSize = 524288,
                        Security = { Mode = SecurityMode.None },
                    };
                    // CCB Custom
                    // Yes, you need this to manage cookies yourself.  Seems counterintutive, but set to true,
                    // it only means that the framework will manage cookie propagation for the same call, which is
                    // not what we want.
                    if (StoreCookies)
                    {
                        binding.AllowCookies = false;
                    }
                }
                return binding;
            }
        }

        protected override Binding Binding => WsHttpBinding;

        protected override MessageVersion MessageVersion => MessageVersion.Default;

        public override void SetProxy(Uri proxyAddress)
        {
            var wsHttpBinding = (WSHttpBinding)Binding;

            wsHttpBinding.ProxyAddress = proxyAddress;
            wsHttpBinding.UseDefaultWebProxy = false;
            wsHttpBinding.BypassProxyOnLocal = false;
            return;
        }
    }
}
#endif