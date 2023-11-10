// <copyright file="SampleData.SalesInvoices.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data sales invoices class</summary>
// ReSharper disable MultipleSpaces, PossibleInvalidOperationException
#pragma warning disable format
#nullable enable
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private SalesInvoiceItem CreateSalesInvoiceItem(DateTime createdDate, decimal corePrice, decimal soldPrice, decimal quantity, string productKey)
        {
            return new()
            {
                ProductID = context.Products.Where(c => c.CustomKey == productKey).Select(c => c.ID).First(),
                Name = context.Products.Where(c => c.CustomKey == productKey).Select(c => c.Name).First(),
                Quantity = quantity,
                Sku = productKey,
                UnitCorePrice = corePrice,
                UnitSoldPrice = soldPrice,
                CreatedDate = createdDate,
                Active = true,
            };
        }

        private SalesInvoice CreateSalesInvoice(DateTime createdDate, string orderKey, string productKey, decimal quantity, string statusKey, string stateKey, string? storeName)
        {
            var corePrice = context.Products.Where(c => c.CustomKey == productKey).Select(c => c.PriceBase).First() ?? 0m;
            var soldPrice = context.Products.Where(c => c.CustomKey == productKey).Select(c => c.PriceSale).First() ?? 0m;
            var subtotal = soldPrice * quantity;
            var shippingPercentageForRate = 0.09m;
            var taxRate = 0.0825m;
            var total = Math.Round(subtotal + Math.Round(subtotal * shippingPercentageForRate, 2) + Math.Round(subtotal * taxRate, 2), 2);
            var account = context.Accounts.First(c => c.CustomKey == "ACCT-1121");
            var user = context.Users.First(c => c.CustomKey == "USER-0001");
            var contact = CreateContact(createdDate);
            var statusID = context.SalesInvoiceStatuses.Where(sos => sos.CustomKey == statusKey).Select(sos => sos.ID).First();
            var stateID = context.SalesInvoiceStates.Where(sos => sos.CustomKey == stateKey).Select(sos => sos.ID).First();
            var typeID = context.SalesInvoiceTypes.Where(sot => sot.Name == "General").Select(sot => sot.ID).First();
            var storeID = context.Stores.Where(c => c.Name == storeName).Select(c => (int?)c.ID).FirstOrDefault();
            var entity = new SalesInvoice
            {
                StatusID = statusID,
                StateID = stateID,
                TypeID = typeID,
                CustomKey = orderKey,
                SubtotalItems = Math.Round(subtotal, 2),
                SubtotalShipping = Math.Round(subtotal * shippingPercentageForRate, 2),
                SubtotalHandling = 0.00m,
                SubtotalFees = 0.00m,
                SubtotalDiscounts = 0.00m,
                SubtotalTaxes = Math.Round(subtotal * taxRate, 2),
                Total = total,
                BalanceDue = total,
                CreatedDate = createdDate,
                UpdatedDate = null,
                Active = true,
                AccountID = account.ID,
                UserID = user.ID,
                ShippingSameAsBilling = true,
                BillingContact = contact,
                ShippingContact = contact,
                SalesItems = new[]
                {
                    CreateSalesInvoiceItem(createdDate, corePrice, soldPrice, quantity, productKey),
                },
                StoreID = storeID,
            };
            return entity;
        }

        private void AddSampleSalesInvoices(DateTime createdDate)
        {
            if (context?.SalesInvoices == null)
            {
                return;
            }
            if (context.SalesInvoices.Any())
            {
                return;
            }
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1000", productKey: "432957",       quantity: 02, statusKey: "UNPAID", stateKey: "WORK",    storeName: null));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1001", productKey: "6542456",      quantity: 03, statusKey: "UNPAID", stateKey: "WORK",    storeName: null));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1002", productKey: "CBENM09",      quantity: 05, statusKey: "UNPAID", stateKey: "WORK",    storeName: null));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1003", productKey: "343731",       quantity: 10, statusKey: "PAID",   stateKey: "HISTORY", storeName: "Bob's Auto Shop"));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1004", productKey: "M3659",        quantity: 01, statusKey: "PAID",   stateKey: "HISTORY", storeName: "Bob's Auto Shop"));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1005", productKey: "403100000000", quantity: 08, statusKey: "UNPAID", stateKey: "WORK",    storeName: "Jane's Antique Store"));
            context.SalesInvoices.Add(CreateSalesInvoice(createdDate: createdDate, orderKey: "SAI-1006", productKey: "AC4.1.5",      quantity: 07, statusKey: "VOID",   stateKey: "HISTORY", storeName: "Jane's Antique Store"));
            context.SaveUnitOfWork(true);
        }
    }
}
