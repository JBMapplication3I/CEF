// <copyright file="PaymentSection.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class PaymentSection
    {
        public bool creditCard { get; set; }

        public bool invoice { get; set; }

        public bool payPal { get; set; }
    }
}
