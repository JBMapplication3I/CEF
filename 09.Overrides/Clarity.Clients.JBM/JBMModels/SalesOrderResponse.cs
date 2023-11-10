// <copyright file="SalesOrderResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the InvoiceLinesResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SalesOrderResponse : FusionResponseBase
    {
        public List<FusionSalesOrder>? Items { get; set; }
    }
}
