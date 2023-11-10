// <copyright file="AddressBookFeatureSet.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class AddressBookFeatureSet
    {
        public bool enabled { get; set; }

        public bool dashboardCanAddAddresses { get; set; }

        public bool allowMakeThisMyNewDefaultBillingInCheckout { get; set; }

        public bool allowMakeThisMyNewDefaultBillingInDashboard { get; set; }

        public bool allowMakeThisMyNewDefaultShippingInCheckout { get; set; }

        public bool allowMakeThisMyNewDefaultShippingInDashboard { get; set; }
    }
}
