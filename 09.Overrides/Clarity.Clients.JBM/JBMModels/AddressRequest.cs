// <copyright file="AddressRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    public class JBMAddressRequest
    {
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? AddressType { get; set; }

        public string? PartySiteName { get; set; }
    }
}