﻿#if !NETSTANDARD1_6

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;

namespace ServiceStack.Html.AntiXsrf
{
    // Represents the security token for the Anti-XSRF system.
    // The token is a random 128-bit value that correlates the session with the request body.
    internal sealed class AntiForgeryToken
    {
        internal const int SecurityTokenBitLength = 128;
        internal const int ClaimUidBitLength = 256;

        private string _additionalData;
        private BinaryBlob _securityToken;
        private string _username;

        public string AdditionalData
        {
            get => _additionalData ?? string.Empty;
            set => _additionalData = value;
        }

        public BinaryBlob ClaimUid { get; set; }

        public bool IsSessionToken { get; set; }

        public BinaryBlob SecurityToken
        {
            get
            {
                _securityToken ??= new(SecurityTokenBitLength);
                return _securityToken;
            }
            set => _securityToken = value;
        }

        public string Username
        {
            get => _username ?? string.Empty;
            set => _username = value;
        }
    }
}

#endif
