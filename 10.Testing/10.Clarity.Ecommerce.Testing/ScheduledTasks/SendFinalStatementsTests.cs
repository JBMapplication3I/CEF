// <copyright file="SendFinalStatementWithFinalPaymentReminderAndPdfInsertTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the send final statement with final payment request and pdf inserts test class</summary>
namespace Clarity.Ecommerce.Tasks.SendFinalStatementWithFinalPaymentReminderAndPdfInsert.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ScheduledTasks.SendFinalStatementWithFinalPRandPDFInserts")]
    public class SendFinalStatementWithFinalPaymentReminderAndPdfInsertTests
    {
        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Sending_FinalStatementWithFinalPaymentReminderAndPdfInsert_Works()
        {
            return new SendFinalStatementsTask().ProcessAsync(null);
        }
    }
}
