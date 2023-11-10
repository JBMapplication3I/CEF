// <copyright file="SampleData.SalesOrders.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample data. sales orders class</summary>
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
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public partial class SampleData
    {
        private SalesOrderItem CreateSalesOrderItem(DateTime createdDate, decimal corePrice, decimal soldPrice, decimal quantity, string productKey)
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

        private Address CreateAddress(DateTime createdDate)
        {
            return new()
            {
                Company = "HQ",
                CustomKey = null,
                Street1 = "300 N Lamar",
                Street2 = "Apt. 1111",
                Street3 = null,
                City = "Austin",
                RegionID = context.Regions.Where(r => r.Name == "Texas").Select(r => r.ID).First(),
                RegionCustom = string.Empty,
                CountryID = context.Countries.Where(c => c.Name == "United States of America").Select(c => c.ID).First(),
                CountryCustom = string.Empty,
                PostalCode = "78703",
                Latitude = null,
                Longitude = null,
                CreatedDate = createdDate,
                Active = true,
            };
        }

        private Contact CreateContact(DateTime createdDate)
        {
            return new()
            {
                CreatedDate = createdDate,
                Active = true,
                Address = CreateAddress(createdDate),
                Email1 = "john.smith@email.com",
                Phone1 = "1-555-343-4322",
                Fax1 = "1-555-533-1234",
                FullName = "John Smith",
                FirstName = "John",
                LastName = "Smith",
                TypeID = context.ContactTypes.First(x => x.Name == "Billing").ID,
            };
        }

        private SalesOrder CreateSalesOrder(DateTime createdDate, string orderKey, string? invoiceKey, string productKey, decimal quantity, string? paymentKey, string? invoicePaymentKey, string statusKey, string stateKey, string? storeName)
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
            var statusID = context.SalesOrderStatuses.Where(sos => sos.CustomKey == statusKey).Select(sos => sos.ID).First();
            var stateID = context.SalesOrderStates.Where(sos => sos.CustomKey == stateKey).Select(sos => sos.ID).First();
            var typeID = context.SalesOrderTypes.Where(sot => sot.Name == "Web").Select(sot => sot.ID).First();
            var invStatusID = context.SalesInvoiceStatuses.Where(c => c.Name == "Unpaid").Select(c => c.ID).First();
            var invStateID = context.SalesInvoiceStates.Where(c => c.Name == "Work").Select(c => c.ID).First();
            var invTypeID = context.SalesInvoiceTypes.Where(c => c.Name == "General").Select(c => c.ID).First();
            var storeID = context.Stores.Where(c => c.Name == storeName).Select(c => (int?)c.ID).FirstOrDefault();
            var entity = new SalesOrder
            {
                StatusID = statusID,
                StateID = stateID,
                TypeID = typeID,
                CustomKey = orderKey,
                PurchaseOrderNumber = null,
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
                StoreID = storeID,
                SalesItems = new[]
                {
                    CreateSalesOrderItem(createdDate, corePrice, soldPrice, quantity, productKey),
                },
            };
            var pmtMethodID = context.PaymentMethods.Where(c => c.Name == "Credit Card").Select(c => c.ID).First();
            var pmtStatusID = context.PaymentStatuses.Where(c => c.Name == "Payment Received").Select(c => c.ID).First();
            var pmtTypeID = context.PaymentTypes.Where(c => c.Name == "Mastercard").Select(c => c.ID).First();
            if (!string.IsNullOrWhiteSpace(paymentKey))
            {
                entity.PaymentTransactionID = "ABCD123456";
                entity.BalanceDue -= total;
                entity.SalesOrderPayments = new List<SalesOrderPayment>
                {
                    new()
                    {
                        CreatedDate = createdDate,
                        Active = true,
                        Slave = new()
                        {
                            CreatedDate = createdDate,
                            Active = true,
                            Amount = total,
                            CustomKey = paymentKey,
                            AuthCode = "Auth123456",
                            Authorized = true,
                            AuthDate = createdDate,
                            BillingContact = contact,
                            CardMask = "&bull;",
                            ExpirationMonth = 12,
                            ExpirationYear = 2020,
                            ExternalPaymentID = "ABCD123456",
                            Last4CardDigits = "1111",
                            Received = true,
                            ReceivedDate = createdDate,
                            StatusID = pmtStatusID,
                            PaymentMethodID = pmtMethodID,
                            TypeID = pmtTypeID,
                            TransactionNumber = "ABCD123456",
                            StatusDate = createdDate,
                            Response = "{some:json}",
                            ExternalCustomerID = "USER-0001",
                        },
                    },
                };
            }
            if (!string.IsNullOrWhiteSpace(invoiceKey))
            {
                entity.AssociatedSalesInvoices = new[]
                {
                    new SalesOrderSalesInvoice
                    {
                        CreatedDate = createdDate,
                        Active = true,
                        Slave = new()
                        {
                            StatusID = invStatusID,
                            StateID = invStateID,
                            TypeID = invTypeID,
                            CustomKey = invoiceKey,
                            DueDate = createdDate,
                            SubtotalItems = Math.Round(subtotal, 2),
                            SubtotalShipping = Math.Round(subtotal * shippingPercentageForRate, 2),
                            SubtotalHandling = 0.00m,
                            SubtotalFees = 0.00m,
                            SubtotalDiscounts = 0.00m,
                            SubtotalTaxes = Math.Round(subtotal * taxRate, 2),
                            Total = Math.Round(subtotal + Math.Round(subtotal * shippingPercentageForRate, 2) + Math.Round(subtotal * taxRate, 2), 2),
                            BalanceDue = Math.Round(subtotal + Math.Round(subtotal * shippingPercentageForRate, 2) + Math.Round(subtotal * taxRate, 2), 2),
                            CreatedDate = createdDate,
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
                        },
                    },
                };
                if (!string.IsNullOrWhiteSpace(invoicePaymentKey))
                {
                    entity.PaymentTransactionID = "ABCD123456";
                    entity.AssociatedSalesInvoices.First().Slave!.BalanceDue = 0.00m;
                    entity.AssociatedSalesInvoices.First().Slave!.SalesInvoicePayments = new List<SalesInvoicePayment>
                    {
                        new()
                        {
                            CreatedDate = createdDate,
                            Active = true,
                            Slave = new()
                            {
                                CreatedDate = createdDate,
                                Active = true,
                                Amount = total,
                                CustomKey = invoicePaymentKey,
                                AuthCode = "Auth123456",
                                Authorized = true,
                                AuthDate = createdDate,
                                BillingContact = contact,
                                CardMask = "*",
                                ExpirationMonth = 12,
                                ExpirationYear = 2020,
                                ExternalPaymentID = "ABCD123456",
                                Last4CardDigits = "1111",
                                Received = true,
                                ReceivedDate = createdDate,
                                StatusID = pmtStatusID,
                                PaymentMethodID = pmtMethodID,
                                TypeID = pmtTypeID,
                                TransactionNumber = "ABCD123456",
                                StatusDate = createdDate,
                                Response = "{some:json}",
                                ExternalCustomerID = "USER-0001",
                            },
                        },
                    };
                }
            }
            return entity;
        }

        private void AddSampleSalesOrders(DateTime createdDate)
        {
            if (context?.SalesOrders == null)
            {
                return;
            }
            if (context.SalesOrders.Any())
            {
                return;
            }
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1000", invoiceKey: "INV-1000", productKey: "432957",       quantity: 02, statusKey: "Pending",                  stateKey: "WORK",    paymentKey: "PAYMENT-1000", invoicePaymentKey: null,           storeName: null));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1001", invoiceKey: "INV-1001", productKey: "6542456",      quantity: 03, statusKey: "Partial Payment Received", stateKey: "WORK",    paymentKey: "PAYMENT-1001", invoicePaymentKey: null,           storeName: null));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1002", invoiceKey: "INV-1002", productKey: "CBENM09",      quantity: 05, statusKey: "Full Payment Received",    stateKey: "WORK",    paymentKey: "PAYMENT-1002", invoicePaymentKey: null,           storeName: null));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1003", invoiceKey: "INV-1003", productKey: "343731",       quantity: 10, statusKey: "Processing",               stateKey: "WORK",    paymentKey: "PAYMENT-1003", invoicePaymentKey: null,           storeName: "Bob's Auto Shop"));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1004", invoiceKey: "INV-1004", productKey: "M3659",        quantity: 01, statusKey: "Shipped",                  stateKey: "WORK",    paymentKey: null,           invoicePaymentKey: "PAYMENT-1004", storeName: "Bob's Auto Shop"));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1005", invoiceKey: "INV-1005", productKey: "403100000000", quantity: 08, statusKey: "Completed",                stateKey: "HISTORY", paymentKey: null,           invoicePaymentKey: "PAYMENT-1005", storeName: "Jane's Antique Store"));
            context.SalesOrders.Add(CreateSalesOrder(createdDate: createdDate, orderKey: "SO-1006", invoiceKey: null,       productKey: "AC4.1.5",      quantity: 07, statusKey: "Pending",                  stateKey: "WORK",    paymentKey: null,           invoicePaymentKey: null,           storeName: "Jane's Antique Store"));
            context.SaveUnitOfWork(true);
        }
    }
}
