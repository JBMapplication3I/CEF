// <copyright file="SalesInvoiceWithSalesInvoicePaymentsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate sales invoice payments workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class SalesInvoiceWithSalesInvoicePaymentsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(ISalesInvoicePaymentModel model)
        {
            return (model.Slave?.Amount ?? 0m) > 0;
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            ISalesInvoicePayment newEntity,
            ISalesInvoicePaymentModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            ////Contract.Requires<InvalidOperationException>(
            ////    model.SlaveID > 0
            ////    || !string.IsNullOrWhiteSpace(model.SlaveKey)
            ////    || !string.IsNullOrWhiteSpace(model.Slave?.CustomKey),
            ////    "Must pass either the Payment ID or Name to match against");
            if (model.SlaveID > 0)
            {
                newEntity.SlaveID = model.SlaveID;
            }
            else
            {
                model.Slave!.Active = true;
                newEntity.Slave = (Payment)model.Slave.CreatePaymentEntity(timestamp, context.ContextProfileName);
            }
            return Task.CompletedTask;
        }

        ////protected override ISalesInvoicePayment ModelToNewObject(ISalesInvoicePaymentModel model, DateTime timestamp, string? contextProfileName)
        ////{
        ////    Contract.RequiresNotNull(model);
        ////    //Contract.Requires<InvalidOperationException>(
        ////    //    model.SlaveID > 0
        ////    //    || !string.IsNullOrWhiteSpace(model.SlaveKey)
        ////    //    || !string.IsNullOrWhiteSpace(model.Slave?.CustomKey),
        ////    //    "Must pass either the Payment ID or Name to match against");
        ////    //
        ////    var newSalesInvoicePayment = RegistryLoaderWrapper.GetInstance<ISalesInvoicePayment>(contextProfileName);
        ////    newSalesInvoicePayment.CustomKey = model.CustomKey;
        ////    newSalesInvoicePayment.Active = true;
        ////    newSalesInvoicePayment.CreatedDate = timestamp;
        ////    if (model.SlaveID > 0)
        ////    {
        ////        newSalesInvoicePayment.PaymentID = model.SlaveID;
        ////    }
        ////    else
        ////    {
        ////        model.Slave.Active = true;
        ////        newSalesInvoicePayment.Payment = model.Slave.CreatePaymentEntity(timestamp, timestamp, contextProfileName);
        ////    }
        ////    //
        ////    return newSalesInvoicePayment;
        ////}
    }
}
