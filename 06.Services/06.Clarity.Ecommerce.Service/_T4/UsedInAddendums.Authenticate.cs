// <copyright file="UsedInAddendums.Authenticate.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the used in addendums classes</summary>
// ReSharper disable MissingXmlDoc
#pragma warning disable SA1134 // Attributes should not share line
#pragma warning disable SA1502 // Element should not be on a single line
#pragma warning disable SA1516 // Elements should be separated by blank line
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    [Authenticate] public partial class GetAddressBook { }
    [Authenticate] public partial class GetAddresses { }
    [Authenticate] public partial class GetManufacturerProducts { }
    [Authenticate] public partial class GetPaymentByID { }
    [Authenticate] public partial class GetPaymentByKey { }
    [Authenticate] public partial class GetPayments { }
    [Authenticate] public partial class GetSampleRequestByID { }
    [Authenticate] public partial class GetSampleRequestByKey { }
    [Authenticate] public partial class GetSampleRequests { }
    [Authenticate] public partial class GetSubscriptionByID { }
    [Authenticate] public partial class GetSubscriptionByKey { }
    [Authenticate] public partial class GetSubscriptions { }
    [Authenticate] public partial class GetUserByID { }
    [Authenticate] public partial class GetUserByKey { }
    [Authenticate] public partial class GetUsers { }
    [Authenticate] public partial class GetVendorProducts { }
}
