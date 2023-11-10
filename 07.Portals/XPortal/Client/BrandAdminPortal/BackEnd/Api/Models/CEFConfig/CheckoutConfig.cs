// <copyright file="CheckoutConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CheckoutConfig
    {
        public CheckoutConfigFlags? flags { get; set; }

        public string? root { get; set; }

        public bool useRecentlyUsedAddresses { get; set; }

        public CheckoutModes mode { get; set; }

        public bool dontAllowCreateAccount { get; set; }

        public bool stepEnterByClick { get; set; }

        public bool usernameIsEmail { get; set; }

        public string? defaultPaymentMethod { get; set; }

        public PaymentSection? paymentOptions { get; set; }

        public CEFConfigCartType? cart { get; set; }

        public CheckoutStore? store { get; set; }

        public TemplateSection[]? sections { get; set; }

        public string? finalActionButtonText { get; set; }

        public PersonalDetailsDisplay? personalDetailsDisplay { get; set; }

        public bool specialInstructions { get; set; }
    }
}
