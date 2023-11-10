#if !NETSTANDARD1_6

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace ServiceStack.Html.AntiXsrf
{
    internal sealed class AntiForgeryConfigWrapper : IAntiForgeryConfig
    {
        public IAntiForgeryAdditionalDataProvider AdditionalDataProvider => AntiForgeryConfig.AdditionalDataProvider;

        public string CookieName => AntiForgeryConfig.CookieName;

        public string FormFieldName => AntiForgeryConfig.AntiForgeryTokenFieldName;

        public bool RequireSSL => AntiForgeryConfig.RequireSsl;

        public bool SuppressIdentityHeuristicChecks => AntiForgeryConfig.SuppressIdentityHeuristicChecks;

        public string UniqueClaimTypeIdentifier => AntiForgeryConfig.UniqueClaimTypeIdentifier;
    }
}

#endif
