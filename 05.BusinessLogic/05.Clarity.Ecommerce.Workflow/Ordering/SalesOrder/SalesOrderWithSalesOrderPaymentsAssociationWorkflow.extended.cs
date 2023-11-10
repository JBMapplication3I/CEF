// <copyright file="SalesOrderWithSalesOrderPaymentsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate sales order payments workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using Interfaces.Models;

    public partial class SalesOrderWithSalesOrderPaymentsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(ISalesOrderPaymentModel model)
        {
            return (model.Slave?.Amount ?? 0m) > 0;
        }

        ////protected override ISalesOrderPayment ModelToNewObject(ISalesOrderPaymentModel model, DateTime timestamp, string? contextProfileName)
        ////{
        ////    Contract.RequiresNotNull(model);
        ////    Contract.Requires<InvalidOperationException>(
        ////        model.PaymentID > 0
        ////        || !string.IsNullOrWhiteSpace(model.PaymentKey)
        ////        || !string.IsNullOrWhiteSpace(model.Payment?.CustomKey),
        ////        "Must pass either the Payment ID or Name to match against");
        ////    //
        ////    var newSalesOrderPayment = RegistryLoaderWrapper.GetInstance<ISalesOrderPayment>(contextProfileName);
        ////    newSalesOrderPayment.CustomKey = model.CustomKey;
        ////    newSalesOrderPayment.Active = true;
        ////    newSalesOrderPayment.CreatedDate = timestamp;
        ////    if (model.PaymentID > 0)
        ////    {
        ////        newSalesOrderPayment.PaymentID = model.PaymentID;
        ////    }
        ////    else
        ////    {
        ////        newSalesOrderPayment.Payment = model.Payment.CreatePaymentEntity(timestamp, timestamp);
        ////    }
        ////    //
        ////    return newSalesOrderPayment;
        ////}
    }
}
