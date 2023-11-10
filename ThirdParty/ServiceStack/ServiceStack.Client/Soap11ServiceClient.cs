#if !(SL5 || XBOX || ANDROID || __IOS__  || __MAC__|| PCL || NETSTANDARD1_1 || NETSTANDARD1_6)
namespace ServiceStack
{
    using System;
    
    public class Soap11ServiceClient : WcfServiceClient
    {
        private System.ServiceModel.BasicHttpBinding binding;

        public Soap11ServiceClient(string uri)
        {
            Uri = uri.WithTrailingSlash() + "Soap11";
        }

        private System.ServiceModel.Channels.Binding BasicHttpBinding
        {
            get
            {
                if (binding == null)
                {
                    binding = new()
                    {
                        MaxReceivedMessageSize = int.MaxValue,
                        HostNameComparisonMode = System.ServiceModel.HostNameComparisonMode.StrongWildcard,
                    };
                }
                return binding;
            }
        }

        protected override System.ServiceModel.Channels.Binding Binding => BasicHttpBinding;

        protected override System.ServiceModel.Channels.MessageVersion MessageVersion => BasicHttpBinding.MessageVersion;

        public override void SetProxy(Uri proxyAddress)
        {
            var basicBinding = (System.ServiceModel.BasicHttpBinding)Binding;

            basicBinding.ProxyAddress = proxyAddress;
            basicBinding.UseDefaultWebProxy = false;
            basicBinding.BypassProxyOnLocal = false;
            return;
        }
    }
}

#endif
