// <copyright file="PersonalDetailsDisplay.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class PersonalDetailsDisplay
    {
        public bool hideAddressBookKeys { get; set; }

        public bool hideAddressBookFirstName { get; set; }

        public bool hideAddressBookLastName { get; set; }

        public bool hideAddressBookEmail { get; set; }

        public bool hideAddressBookPhone { get; set; }

        public bool hideAddressBookFax { get; set; }

        public bool hideBillingFirstName { get; set; }

        public bool hideBillingLastName { get; set; }

        public bool hideBillingEmail { get; set; }

        public bool hideBillingPhone { get; set; }

        public bool hideBillingFax { get; set; }

        public bool hideShippingFirstName { get; set; }

        public bool hideShippingLastName { get; set; }

        public bool hideShippingEmail { get; set; }

        public bool hideShippingPhone { get; set; }

        public bool hideShippingFax { get; set; }
    }
}
