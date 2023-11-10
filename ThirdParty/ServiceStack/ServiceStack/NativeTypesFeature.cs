using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;
using ServiceStack.NativeTypes;

namespace ServiceStack
{
    public class NativeTypesFeature : IPlugin
    {
        public MetadataTypesConfig MetadataTypesConfig { get; set; }

        public static bool DisableTokenVerification { get; set; }

        public NativeTypesFeature()
        {
            MetadataTypesConfig = new()
            {
                AddDefaultXmlNamespace = HostConfig.DefaultWsdlNamespace,
                ExportAttributes = new()
                {
                    typeof(FlagsAttribute),
                    typeof(ApiAttribute),
                    typeof(ApiResponseAttribute),
                    typeof(ApiMemberAttribute),
                    typeof(StringLengthAttribute),
                    typeof(IgnoreAttribute),
                    typeof(IgnoreDataMemberAttribute),
                    typeof(MetaAttribute),
                    typeof(RequiredAttribute),
                    typeof(ReferencesAttribute),
                    typeof(StringLengthAttribute),
                    typeof(AutoQueryViewerAttribute),
                    typeof(AutoQueryViewerFieldAttribute),
                },
                ExportTypes = new()
                {
                    typeof(IGet),
                    typeof(IPost),
                    typeof(IPut),
                    typeof(IDelete),
                    typeof(IPatch),
                },
                IgnoreTypes = new()
                {
                },
                IgnoreTypesInNamespaces = new()
                {
                    "ServiceStack",
                    "ServiceStack.Auth",
                    "ServiceStack.Caching",
                    "ServiceStack.Configuration",
                    "ServiceStack.Data",
                    "ServiceStack.IO",
                    "ServiceStack.Logging",
                    "ServiceStack.Messaging",
                    "ServiceStack.Model",
                    "ServiceStack.Redis",
                    "ServiceStack.Web",
                    "ServiceStack.Admin",
                    "ServiceStack.NativeTypes",
                    "ServiceStack.Api.Swagger",
                },
                DefaultNamespaces = new()
                {
                    "System",
                    "System.Collections",
                    "System.Collections.Generic",
                    "System.Runtime.Serialization",
                    "ServiceStack",
                    "ServiceStack.DataAnnotations",
                },
            };
        }

        public void Register(IAppHost appHost)
        {
            appHost.Register<INativeTypesMetadata>(
                new NativeTypesMetadata(appHost.Metadata, MetadataTypesConfig));

            appHost.RegisterService<NativeTypesService>();
        }
    }
}