/*
 * AvaTax API Client Library
 *
 * (c) 2004-2019 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Genevieve Conty
 * @author Greg Hester
 */
namespace Avalara.AvaTax.RestClient
{
    /// <summary>The type of data contained in this batch.</summary>
    public enum BatchType
    {
        /// <summary>An enum constant representing the ava cert update option.</summary>
        AvaCertUpdate = 0,

        /// <summary>An enum constant representing the ava cert update all option.</summary>
        AvaCertUpdateAll = 1,

        /// <summary>An enum constant representing the batch maintenance option.</summary>
        BatchMaintenance = 2,

        /// <summary>An enum constant representing the company location import option.</summary>
        CompanyLocationImport = 3,

        /// <summary>An enum constant representing the document import option.</summary>
        DocumentImport = 4,

        /// <summary>An enum constant representing the exempt cert import option.</summary>
        ExemptCertImport = 5,

        /// <summary>An enum constant representing the item import option.</summary>
        ItemImport = 6,

        /// <summary>An enum constant representing the sales audit export option.</summary>
        SalesAuditExport = 7,

        /// <summary>An enum constant representing the sstp test deck import option.</summary>
        SstpTestDeckImport = 8,

        /// <summary>An enum constant representing the tax rule import option.</summary>
        TaxRuleImport = 9,

        /// <summary>
        /// This batch type represents tax transaction data being uploaded to AvaTax. Each line in the batch represents a single transaction
        ///  or a line in a multi-line transaction. For reference, see http://developer.avalara.com/blog/2016/10/24/batch-transaction-upload-in-rest-v2
        /// </summary>
        TransactionImport = 10,

        /// <summary>An enum constant representing the upc bulk import option.</summary>
        UPCBulkImport = 11,

        /// <summary>An enum constant representing the upc validation import option.</summary>
        UPCValidationImport = 12,
    }
}
