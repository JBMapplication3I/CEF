// <copyright file="201903182059034_Overhaul2019Q1b.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201903182059034 overhaul 2019 q 1b class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    // ReSharper disable once InconsistentNaming
    public partial class Overhaul2019Q1b : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Accounts.AccountTerm", "ID", "Accounts.Account");
            DropForeignKey("Contacts.Opportunities", "AccountID", "Accounts.Account");
            DropForeignKey("Contacts.Opportunities", "ClientProjectContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Opportunities", "OFTDeliveryContactID", "Contacts.Contact");
            DropForeignKey("Contacts.Opportunities", "PrimaryContactID", "Contacts.Contact");
            DropForeignKey("Ordering.SalesOrderItemShipment", "MasterID", "Ordering.SalesOrderItem");
            DropForeignKey("Ordering.SalesOrderItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Sampling.SampleRequestItemShipment", "MasterID", "Sampling.SampleRequestItem");
            DropForeignKey("Sampling.SampleRequestItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Invoicing.SalesInvoiceItemShipment", "MasterID", "Invoicing.SalesInvoiceItem");
            DropForeignKey("Invoicing.SalesInvoiceItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Returning.SalesReturnItemShipment", "MasterID", "Returning.SalesReturnItem");
            DropForeignKey("Returning.SalesReturnItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Quoting.SalesQuoteItemShipment", "MasterID", "Quoting.SalesQuoteItem");
            DropForeignKey("Quoting.SalesQuoteItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Purchasing.PurchaseOrderItemShipment", "MasterID", "Purchasing.PurchaseOrderItem");
            DropForeignKey("Purchasing.PurchaseOrderItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Shopping.CartItemShipment", "MasterID", "Shopping.CartItem");
            DropForeignKey("Shopping.CartItemShipment", "SlaveID", "Shipping.Shipment");
            DropForeignKey("Vendors.ShipVia", "ShipCarrierID", "Shipping.ShipCarrier");
            DropForeignKey("Vendors.Term", "CreatedByUserID", "Contacts.User");
            DropForeignKey("Vendors.Term", "UpdatedByUserID", "Contacts.User");
            DropForeignKey("Vendors.Term", "VendorTermID", "Vendors.VendorTerm");
            DropIndex("Accounts.AccountTerm", new[] { "ID" });
            DropIndex("Accounts.AccountTerm", new[] { "CustomKey" });
            DropIndex("Accounts.AccountTerm", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountTerm", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountTerm", new[] { "Active" });
            DropIndex("Accounts.AccountTerm", new[] { "Hash" });
            DropIndex("Accounts.AccountTerm", new[] { "Name" });
            DropIndex("Contacts.CustomerPriority", new[] { "ID" });
            DropIndex("Contacts.CustomerPriority", new[] { "CustomKey" });
            DropIndex("Contacts.CustomerPriority", new[] { "CreatedDate" });
            DropIndex("Contacts.CustomerPriority", new[] { "UpdatedDate" });
            DropIndex("Contacts.CustomerPriority", new[] { "Active" });
            DropIndex("Contacts.CustomerPriority", new[] { "Hash" });
            DropIndex("Contacts.CustomerPriority", new[] { "Name" });
            DropIndex("Contacts.CustomerPriority", new[] { "DisplayName" });
            DropIndex("Contacts.CustomerPriority", new[] { "SortOrder" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "ID" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "CustomKey" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "CreatedDate" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "UpdatedDate" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Active" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Hash" });
            DropIndex("Purchasing.FreeOnBoard", new[] { "Name" });
            DropIndex("Contacts.Opportunities", new[] { "ID" });
            DropIndex("Contacts.Opportunities", new[] { "CustomKey" });
            DropIndex("Contacts.Opportunities", new[] { "CreatedDate" });
            DropIndex("Contacts.Opportunities", new[] { "UpdatedDate" });
            DropIndex("Contacts.Opportunities", new[] { "Active" });
            DropIndex("Contacts.Opportunities", new[] { "Hash" });
            DropIndex("Contacts.Opportunities", new[] { "AccountID" });
            DropIndex("Contacts.Opportunities", new[] { "PrimaryContactID" });
            DropIndex("Contacts.Opportunities", new[] { "ClientProjectContactID" });
            DropIndex("Contacts.Opportunities", new[] { "OFTDeliveryContactID" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "ID" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "CreatedDate" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "Active" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "MasterID" });
            DropIndex("Ordering.SalesOrderItemShipment", new[] { "SlaveID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "ID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "CustomKey" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "CreatedDate" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "UpdatedDate" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "Active" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "Hash" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "MasterID" });
            DropIndex("Sampling.SampleRequestItemShipment", new[] { "SlaveID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "ID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "CustomKey" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "CreatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "UpdatedDate" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "Active" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "Hash" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "MasterID" });
            DropIndex("Invoicing.SalesInvoiceItemShipment", new[] { "SlaveID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "ID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "CustomKey" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "CreatedDate" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "UpdatedDate" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "Active" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "Hash" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "MasterID" });
            DropIndex("Returning.SalesReturnItemShipment", new[] { "SlaveID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "ID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "CustomKey" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "CreatedDate" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "UpdatedDate" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "Active" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "Hash" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "MasterID" });
            DropIndex("Quoting.SalesQuoteItemShipment", new[] { "SlaveID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "ID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "CustomKey" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "CreatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "UpdatedDate" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "Active" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "Hash" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "MasterID" });
            DropIndex("Purchasing.PurchaseOrderItemShipment", new[] { "SlaveID" });
            DropIndex("Shopping.CartItemShipment", new[] { "ID" });
            DropIndex("Shopping.CartItemShipment", new[] { "CustomKey" });
            DropIndex("Shopping.CartItemShipment", new[] { "CreatedDate" });
            DropIndex("Shopping.CartItemShipment", new[] { "UpdatedDate" });
            DropIndex("Shopping.CartItemShipment", new[] { "Active" });
            DropIndex("Shopping.CartItemShipment", new[] { "Hash" });
            DropIndex("Shopping.CartItemShipment", new[] { "MasterID" });
            DropIndex("Shopping.CartItemShipment", new[] { "SlaveID" });
            DropIndex("Vendors.ShipVia", new[] { "ID" });
            DropIndex("Vendors.ShipVia", new[] { "CustomKey" });
            DropIndex("Vendors.ShipVia", new[] { "CreatedDate" });
            DropIndex("Vendors.ShipVia", new[] { "UpdatedDate" });
            DropIndex("Vendors.ShipVia", new[] { "Active" });
            DropIndex("Vendors.ShipVia", new[] { "Hash" });
            DropIndex("Vendors.ShipVia", new[] { "ShipCarrierID" });
            DropIndex("Vendors.Term", new[] { "ID" });
            DropIndex("Vendors.Term", new[] { "CustomKey" });
            DropIndex("Vendors.Term", new[] { "CreatedDate" });
            DropIndex("Vendors.Term", new[] { "UpdatedDate" });
            DropIndex("Vendors.Term", new[] { "Active" });
            DropIndex("Vendors.Term", new[] { "Hash" });
            DropIndex("Vendors.Term", new[] { "VendorTermID" });
            DropIndex("Vendors.Term", new[] { "CreatedByUserID" });
            DropIndex("Vendors.Term", new[] { "UpdatedByUserID" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "ID" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "CustomKey" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "CreatedDate" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "UpdatedDate" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "Active" });
            DropIndex("Shipping.UPSEndOfDay", new[] { "Hash" });
            DropTable("Accounts.AccountTerm");
            DropTable("Contacts.CustomerPriority");
            DropTable("Purchasing.FreeOnBoard");
            DropTable("Contacts.Opportunities");
            DropTable("Ordering.SalesOrderItemShipment");
            DropTable("Sampling.SampleRequestItemShipment");
            DropTable("Invoicing.SalesInvoiceItemShipment");
            DropTable("Returning.SalesReturnItemShipment");
            DropTable("Quoting.SalesQuoteItemShipment");
            DropTable("Purchasing.PurchaseOrderItemShipment");
            DropTable("Shopping.CartItemShipment");
            DropTable("Vendors.ShipVia");
            DropTable("Vendors.Term");
            DropTable("Shipping.UPSEndOfDay");
        }

        public override void Down()
        {
            CreateTable(
                "Shipping.UPSEndOfDay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        ShipToEmailAddress = c.String(),
                        ThirdPartyReceiverPostalCode = c.String(),
                        ShipToCustomerID = c.String(),
                        ShipToCompanyorName = c.String(),
                        ShipToAttention = c.String(),
                        ShipToAddress1 = c.String(),
                        ShipToAddress2 = c.String(),
                        ShipToAddress3 = c.String(),
                        ShipToCountryTerritory = c.String(),
                        ShipToPostalCode = c.String(),
                        ShipToCityorTown = c.String(),
                        ShipToStateProvinceCounty = c.String(),
                        ShipToTelephone = c.String(),
                        ShipToFaxNumber = c.String(),
                        ShipToReceiverUPSAccountNumber = c.String(),
                        ShipToResidentialIndicator = c.String(),
                        ShipFromCompanyorName = c.String(),
                        ShipFromAddress1 = c.String(),
                        ShipFromAttention = c.String(),
                        ShipFromAddress2 = c.String(),
                        ShipFromAddress3 = c.String(),
                        ShipFromCountryTerritory = c.String(),
                        ShipFromPostalCode = c.String(),
                        ShipFromCityorTown = c.String(),
                        ShipFromStateProvinceCounty = c.String(),
                        ShipFromTelephone = c.String(),
                        ShipFromFaxNumber = c.String(),
                        ShipFromEmailAddress = c.String(),
                        ShipFromUPSAccountNumber = c.String(),
                        ThirdPartyCustomerID = c.String(),
                        ThirdPartyCompanyorName = c.String(),
                        ThirdPartyAttention = c.String(),
                        ThirdPartyAddress1 = c.String(),
                        ThirdPartyAddress2 = c.String(),
                        ThirdPartyAddress3 = c.String(),
                        ThirdPartyCountryTerritory = c.String(),
                        ThirdPartyCityorTown = c.String(),
                        ThirdPartyStateProvinceCounty = c.String(),
                        ThirdPartyTelephone = c.String(),
                        ThirdPartyFaxNumber = c.String(),
                        ThirdPartyUPSAccountNumber = c.String(),
                        InternationalDocumentationInvoiceImporterSameAsShipTo = c.String(),
                        InternationalDocumentationInvoiceTermsOfSale = c.String(),
                        InternationalDocumentationInvoiceReasonForExport = c.String(),
                        InternationalDocumentationInvoiceDeclarationStatement = c.String(),
                        InternationalDocumentationInvoiceAdditionalComments = c.String(),
                        InternationalDocumentationInvoiceLineTotal = c.String(),
                        InternationalDocumentationTotalInvoiceAmount = c.String(),
                        InternationalDocumentationCustomsValueTotal = c.String(),
                        InternationalDocumentationDutiescost = c.String(),
                        InternationalDocumentationTaxesandFeescost = c.String(),
                        InternationalDocumentationTotalDutiesandTaxes = c.String(),
                        GoodsPartNumber = c.String(),
                        GoodsDescriptionOfGood = c.String(),
                        GoodsInvNAFTACN22TariffCode = c.String(),
                        GoodsDDTCQuantity = c.String(),
                        GoodsDDTCUnitofMeasure = c.String(),
                        ImporterCustomerID = c.String(),
                        ImporterCompanyorName = c.String(),
                        ImporterAttention = c.String(),
                        ImporterAddress1 = c.String(),
                        ImporterAddress2 = c.String(),
                        ImporterAddress3 = c.String(),
                        ImporterCountryTerritory = c.String(),
                        ImporterCityorTown = c.String(),
                        ImporterStateProvinceCounty = c.String(),
                        ImporterPostalCode = c.String(),
                        ImporterTelephone = c.String(),
                        ImporterUPSAccountNumber = c.String(),
                        UltimateConsigneeCustomerID = c.String(),
                        UltimateConsigneeCompanyorName = c.String(),
                        UltimateConsigneeAttention = c.String(),
                        UltimateConsigneeAddress1 = c.String(),
                        UltimateConsigneeAddress2 = c.String(),
                        UltimateConsigneeAddress3 = c.String(),
                        UltimateConsigneeCountryTerritory = c.String(),
                        UltimateConsigneeCityorTown = c.String(),
                        UltimateConsigneeStateProvinceCounty = c.String(),
                        UltimateConsigneePostalCode = c.String(),
                        UltimateConsigneeTelephone = c.String(),
                        UltimateConsigneeTaxIDNumber = c.String(),
                        ProducerCustomerID = c.String(),
                        ProducerCompanyorName = c.String(),
                        ProducerAttention = c.String(),
                        ProducerAddress1 = c.String(),
                        ProducerAddress2 = c.String(),
                        ProducerAddress3 = c.String(),
                        ProducerCountryTerritory = c.String(),
                        ProducerCityorTown = c.String(),
                        ProducerStateProvinceCounty = c.String(),
                        ProducerPostalCode = c.String(),
                        ProducerTelephone = c.String(),
                        ProducerTaxIDNumber = c.String(),
                        GoodsInvNAFTACOCN22CountryTerritoryOfOrigin = c.String(),
                        GoodsInvoiceCN22Units = c.String(),
                        GoodsInvoiceCN22UnitOfMeasure = c.String(),
                        GoodsInvoiceSEDCN22UnitPrice = c.String(),
                        GoodsCurrencyCode = c.String(),
                        GoodsInvoiceNetWeight = c.String(),
                        GoodsSEDCOGrossWeight = c.String(),
                        GoodsSEDCOLBSorKgs = c.String(),
                        GoodsSEDPrintGoodOnSED = c.String(),
                        GoodsInvoiceSEDCOMarksAndNumbers = c.String(),
                        GoodsSEDScheduleBNumber = c.String(),
                        GoodsSEDScheduleBUnits1 = c.String(),
                        GoodsSEDScheduleBUnits2 = c.String(),
                        GoodsSEDUnitofMeasure1 = c.String(),
                        GoodsSEDUnitofMeasure2 = c.String(),
                        GoodsSEDECCN = c.String(),
                        GoodsDFM = c.String(),
                        GoodsSEDException = c.String(),
                        GoodsSEDLicenseNumber = c.String(),
                        GoodsSEDLicenseExpirationDate = c.String(),
                        GoodsSEDExportCode = c.String(),
                        GoodsDDTCRegistrationNumber = c.String(),
                        GoodsDDTCSignificantMilitaryEquipmentIndicator = c.String(),
                        GoodsDDTCEligiblePartyCertificationIndicator = c.String(),
                        GoodsDDTCUSMLCategoryCode = c.String(),
                        GoodsDDTCLicenseLineNumber = c.String(),
                        GoodsNAFTAPrintGoodOnNAFTA = c.String(),
                        GoodsNAFTAPreferenceCriterion = c.String(),
                        GoodsNAFTAProducer = c.String(),
                        GoodsNAFTAMultipleCountriesTerritoriesofOrigin = c.String(),
                        GoodsNAFTANetCostMethod = c.String(),
                        GoodsNAFTANetCostPeriodStartDate = c.String(),
                        GoodsNAFTANetCostPeriodEndDate = c.String(),
                        GoodsCOPrintGoodOnCO = c.String(),
                        GoodsCONumberOfPackagesContainingGood = c.String(),
                        GoodsSLIPrintGoodOnSLI = c.String(),
                        InternationalDocumentationInvoiceCN22CurrencyCode = c.String(),
                        InternationalDocumentationInvoiceDiscount = c.String(),
                        InternationalDocumentationInvoiceChargesFreight = c.String(),
                        InternationalDocumentationInvoiceChargesInsurance = c.String(),
                        InternationalDocumentationInvoiceChargesOther = c.String(),
                        InternationalDocumentationCustomsValueCurrencyCode = c.String(),
                        InternationalDocumentationCustomsValueOrigin = c.String(),
                        InternationalDocumentationSEDCode = c.String(),
                        InternationalDocumentationSEDPartiesToTransaction = c.String(),
                        InternationalDocumentationSEDUltimateConsigneeSameAsShipTo = c.String(),
                        InternationalDocumentationSEDCountryTerritoryOfUltimateDestination = c.String(),
                        InternationalDocumentationSEDAESTransactionNumber = c.String(),
                        InternationalDocumentationSEDAESTransactionNumberType = c.String(),
                        InternationalDocumentationSEDRoutedTransaction = c.String(),
                        InternationalDocumentationSEDCurrencyConversion = c.String(),
                        InternationalDocumentationCOUPSPrepareCO = c.String(),
                        InternationalDocumentationCOOwnerOrAgent = c.String(),
                        InternationalDocumentationNAFTAProducer = c.String(),
                        InternationalDocumentationNAFTABlanketPeriodStart = c.String(),
                        InternationalDocumentationNAFTABlanketPeriodEnd = c.String(),
                        InternationalDocumentationMovementReferenceNumberMRN = c.String(),
                        InternationalDocumentationCN22GoodsType = c.String(),
                        InternationalDocumentationCN22GoodsTypeOtherDescription = c.String(),
                        InternationalDocumentationRequestDutiesandTaxes = c.String(),
                        InternationalDocumentationVATcost = c.String(),
                        ThirdPartyReceiverCustomerID = c.String(),
                        ThirdPartyReceiverCompanyorName = c.String(),
                        ThirdPartyReceiverAttention = c.String(),
                        ThirdPartyReceiverAddress1 = c.String(),
                        ThirdPartyReceiverAddress2 = c.String(),
                        ThirdPartyReceiverAddress3 = c.String(),
                        ThirdPartyReceiverCountryTerritory = c.String(),
                        PackageUSPSWeight = c.String(),
                        ThirdPartyReceiverCityorTown = c.String(),
                        ThirdPartyReceiverStateProvinceCounty = c.String(),
                        ThirdPartyReceiverTelephone = c.String(),
                        ThirdPartyReceiverFaxNumber = c.String(),
                        ThirdPartyReceiverUPSAccountNumber = c.String(),
                        PackageVoidIndicator = c.String(),
                        PackagePackageReferenceCharge = c.String(),
                        PackagePackagePublishedCharge = c.String(),
                        PackagePackageType = c.String(),
                        PackageWeight = c.String(),
                        PackageDeliveryConfirmationNegotiatedRatesCharge = c.String(),
                        PackageNonmachinableNegotiatedRatesCharge = c.String(),
                        PackageNonDDUNegotiatedRatesCharge = c.String(),
                        PackageTrackingNumber = c.String(),
                        PackageLargePackageIndicator = c.String(),
                        PackageReference1 = c.String(),
                        PackageReference2 = c.String(),
                        PackageAdditionalHandlingOption = c.String(),
                        PackagePackageNegotiatedRatesCharge = c.Decimal(precision: 18, scale: 2),
                        PackageAdditionalHandlingNegotiatedRatesCharge = c.String(),
                        PackageDeliveryConfirmationSignatureRequired = c.String(),
                        PackageDeclaredValueOption = c.String(),
                        PackageDeclaredValueShipperPaid = c.String(),
                        PackageDeclaredValueAmount = c.String(),
                        PackageDeliveryConfirmationOption = c.String(),
                        PackageDeliveryConfirmationReferenceCharge = c.String(),
                        PackageMerchandiseDescriptionforPackage = c.String(),
                        PackageLength = c.String(),
                        PackageWidth = c.String(),
                        PackageHeight = c.String(),
                        ShipmentInformationServiceType = c.String(),
                        ShipmentInformationPageNumber = c.String(),
                        ShipmentInformationShipmentID = c.String(),
                        ShipmentInformationActualWeight = c.String(),
                        ShipmentInformationDimensionalWeight = c.String(),
                        ShipmentInformationBillableWeight = c.String(),
                        ShipmentInformationBillTransportationTo = c.String(),
                        ShipmentInformationBillDutyandTaxTo = c.String(),
                        ShipmentInformationTransitTime = c.String(),
                        ShipmentInformationSaturdayDeliveryNegotiatedRatesCharge = c.String(),
                        ShipmentInformationNumberofPackages = c.String(),
                        ShipmentInformationSaturdayPickupOption = c.String(),
                        ShipmentInformationCollectiondate = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Vendors.Term",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        AccountNumber = c.String(maxLength: 200, unicode: false),
                        Notes = c.String(maxLength: 500, unicode: false),
                        VendorTermID = c.Int(nullable: false),
                        CreatedByUserID = c.Int(nullable: false),
                        UpdatedByUserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Vendors.ShipVia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        AccountNumber = c.String(maxLength: 200, unicode: false),
                        Notes = c.String(maxLength: 500, unicode: false),
                        ShipCarrierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Shopping.CartItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Purchasing.PurchaseOrderItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Quoting.SalesQuoteItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Returning.SalesReturnItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Invoicing.SalesInvoiceItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Sampling.SampleRequestItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Ordering.SalesOrderItemShipment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 4),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.Opportunities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        StageName = c.String(maxLength: 100),
                        Type = c.String(maxLength: 100),
                        DeliveryInstructions = c.String(maxLength: 300),
                        PurchaseOrder = c.String(maxLength: 50),
                        CloseDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProbabilityOfClose = c.Decimal(precision: 18, scale: 4),
                        ProbabilityOfWin = c.Decimal(precision: 18, scale: 4),
                        AccountID = c.Int(nullable: false),
                        PrimaryContactID = c.Int(),
                        ClientProjectContactID = c.Int(),
                        OFTDeliveryContactID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Purchasing.FreeOnBoard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Contacts.CustomerPriority",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Accounts.AccountTerm",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Uid = c.Guid(nullable: false),
                        DaysDiscountAvailable = c.Int(),
                        DaysUntilDue = c.Int(),
                        DiscountCalculation = c.String(),
                        DiscountDateBasedOn = c.String(),
                        DueDateBasedOn = c.String(),
                        IsDiscountCalculatedOnFreight = c.Boolean(),
                        IsDiscountCalculatedOnMiscellaneous = c.Boolean(),
                        IsDiscountCalculatedOnSalePurchase = c.Boolean(),
                        IsDiscountCalculatedOnTax = c.Boolean(),
                        IsDiscountCalculatedOnTradeDiscount = c.Boolean(),
                        ModifiedBy = c.String(),
                        UseGracePeriods = c.Boolean(),
                        UseValueAddedTax = c.Boolean(),
                        AccountKey = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Shipping.UPSEndOfDay", "Hash");
            CreateIndex("Shipping.UPSEndOfDay", "Active");
            CreateIndex("Shipping.UPSEndOfDay", "UpdatedDate");
            CreateIndex("Shipping.UPSEndOfDay", "CreatedDate");
            CreateIndex("Shipping.UPSEndOfDay", "CustomKey");
            CreateIndex("Shipping.UPSEndOfDay", "ID");
            CreateIndex("Vendors.Term", "UpdatedByUserID");
            CreateIndex("Vendors.Term", "CreatedByUserID");
            CreateIndex("Vendors.Term", "VendorTermID");
            CreateIndex("Vendors.Term", "Hash");
            CreateIndex("Vendors.Term", "Active");
            CreateIndex("Vendors.Term", "UpdatedDate");
            CreateIndex("Vendors.Term", "CreatedDate");
            CreateIndex("Vendors.Term", "CustomKey");
            CreateIndex("Vendors.Term", "ID");
            CreateIndex("Vendors.ShipVia", "ShipCarrierID");
            CreateIndex("Vendors.ShipVia", "Hash");
            CreateIndex("Vendors.ShipVia", "Active");
            CreateIndex("Vendors.ShipVia", "UpdatedDate");
            CreateIndex("Vendors.ShipVia", "CreatedDate");
            CreateIndex("Vendors.ShipVia", "CustomKey");
            CreateIndex("Vendors.ShipVia", "ID");
            CreateIndex("Shopping.CartItemShipment", "SlaveID");
            CreateIndex("Shopping.CartItemShipment", "MasterID");
            CreateIndex("Shopping.CartItemShipment", "Hash");
            CreateIndex("Shopping.CartItemShipment", "Active");
            CreateIndex("Shopping.CartItemShipment", "UpdatedDate");
            CreateIndex("Shopping.CartItemShipment", "CreatedDate");
            CreateIndex("Shopping.CartItemShipment", "CustomKey");
            CreateIndex("Shopping.CartItemShipment", "ID");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "SlaveID");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "MasterID");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "Hash");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "Active");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "UpdatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "CreatedDate");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "CustomKey");
            CreateIndex("Purchasing.PurchaseOrderItemShipment", "ID");
            CreateIndex("Quoting.SalesQuoteItemShipment", "SlaveID");
            CreateIndex("Quoting.SalesQuoteItemShipment", "MasterID");
            CreateIndex("Quoting.SalesQuoteItemShipment", "Hash");
            CreateIndex("Quoting.SalesQuoteItemShipment", "Active");
            CreateIndex("Quoting.SalesQuoteItemShipment", "UpdatedDate");
            CreateIndex("Quoting.SalesQuoteItemShipment", "CreatedDate");
            CreateIndex("Quoting.SalesQuoteItemShipment", "CustomKey");
            CreateIndex("Quoting.SalesQuoteItemShipment", "ID");
            CreateIndex("Returning.SalesReturnItemShipment", "SlaveID");
            CreateIndex("Returning.SalesReturnItemShipment", "MasterID");
            CreateIndex("Returning.SalesReturnItemShipment", "Hash");
            CreateIndex("Returning.SalesReturnItemShipment", "Active");
            CreateIndex("Returning.SalesReturnItemShipment", "UpdatedDate");
            CreateIndex("Returning.SalesReturnItemShipment", "CreatedDate");
            CreateIndex("Returning.SalesReturnItemShipment", "CustomKey");
            CreateIndex("Returning.SalesReturnItemShipment", "ID");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "SlaveID");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "MasterID");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "Hash");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "Active");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "UpdatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "CreatedDate");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "CustomKey");
            CreateIndex("Invoicing.SalesInvoiceItemShipment", "ID");
            CreateIndex("Sampling.SampleRequestItemShipment", "SlaveID");
            CreateIndex("Sampling.SampleRequestItemShipment", "MasterID");
            CreateIndex("Sampling.SampleRequestItemShipment", "Hash");
            CreateIndex("Sampling.SampleRequestItemShipment", "Active");
            CreateIndex("Sampling.SampleRequestItemShipment", "UpdatedDate");
            CreateIndex("Sampling.SampleRequestItemShipment", "CreatedDate");
            CreateIndex("Sampling.SampleRequestItemShipment", "CustomKey");
            CreateIndex("Sampling.SampleRequestItemShipment", "ID");
            CreateIndex("Ordering.SalesOrderItemShipment", "SlaveID");
            CreateIndex("Ordering.SalesOrderItemShipment", "MasterID");
            CreateIndex("Ordering.SalesOrderItemShipment", "Hash");
            CreateIndex("Ordering.SalesOrderItemShipment", "Active");
            CreateIndex("Ordering.SalesOrderItemShipment", "UpdatedDate");
            CreateIndex("Ordering.SalesOrderItemShipment", "CreatedDate");
            CreateIndex("Ordering.SalesOrderItemShipment", "CustomKey");
            CreateIndex("Ordering.SalesOrderItemShipment", "ID");
            CreateIndex("Contacts.Opportunities", "OFTDeliveryContactID");
            CreateIndex("Contacts.Opportunities", "ClientProjectContactID");
            CreateIndex("Contacts.Opportunities", "PrimaryContactID");
            CreateIndex("Contacts.Opportunities", "AccountID");
            CreateIndex("Contacts.Opportunities", "Hash");
            CreateIndex("Contacts.Opportunities", "Active");
            CreateIndex("Contacts.Opportunities", "UpdatedDate");
            CreateIndex("Contacts.Opportunities", "CreatedDate");
            CreateIndex("Contacts.Opportunities", "CustomKey");
            CreateIndex("Contacts.Opportunities", "ID");
            CreateIndex("Purchasing.FreeOnBoard", "Name");
            CreateIndex("Purchasing.FreeOnBoard", "Hash");
            CreateIndex("Purchasing.FreeOnBoard", "Active");
            CreateIndex("Purchasing.FreeOnBoard", "UpdatedDate");
            CreateIndex("Purchasing.FreeOnBoard", "CreatedDate");
            CreateIndex("Purchasing.FreeOnBoard", "CustomKey");
            CreateIndex("Purchasing.FreeOnBoard", "ID");
            CreateIndex("Contacts.CustomerPriority", "SortOrder");
            CreateIndex("Contacts.CustomerPriority", "DisplayName");
            CreateIndex("Contacts.CustomerPriority", "Name");
            CreateIndex("Contacts.CustomerPriority", "Hash");
            CreateIndex("Contacts.CustomerPriority", "Active");
            CreateIndex("Contacts.CustomerPriority", "UpdatedDate");
            CreateIndex("Contacts.CustomerPriority", "CreatedDate");
            CreateIndex("Contacts.CustomerPriority", "CustomKey");
            CreateIndex("Contacts.CustomerPriority", "ID");
            CreateIndex("Accounts.AccountTerm", "Name");
            CreateIndex("Accounts.AccountTerm", "Hash");
            CreateIndex("Accounts.AccountTerm", "Active");
            CreateIndex("Accounts.AccountTerm", "UpdatedDate");
            CreateIndex("Accounts.AccountTerm", "CreatedDate");
            CreateIndex("Accounts.AccountTerm", "CustomKey");
            CreateIndex("Accounts.AccountTerm", "ID");
            AddForeignKey("Vendors.Term", "VendorTermID", "Vendors.VendorTerm", "ID", cascadeDelete: true);
            AddForeignKey("Vendors.Term", "UpdatedByUserID", "Contacts.User", "ID");
            AddForeignKey("Vendors.Term", "CreatedByUserID", "Contacts.User", "ID", cascadeDelete: true);
            AddForeignKey("Vendors.ShipVia", "ShipCarrierID", "Shipping.ShipCarrier", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Shopping.CartItemShipment", "MasterID", "Shopping.CartItem", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Purchasing.PurchaseOrderItemShipment", "MasterID", "Purchasing.PurchaseOrderItem", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Quoting.SalesQuoteItemShipment", "MasterID", "Quoting.SalesQuoteItem", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Returning.SalesReturnItemShipment", "MasterID", "Returning.SalesReturnItem", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Invoicing.SalesInvoiceItemShipment", "MasterID", "Invoicing.SalesInvoiceItem", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Sampling.SampleRequestItemShipment", "MasterID", "Sampling.SampleRequestItem", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItemShipment", "SlaveID", "Shipping.Shipment", "ID", cascadeDelete: true);
            AddForeignKey("Ordering.SalesOrderItemShipment", "MasterID", "Ordering.SalesOrderItem", "ID", cascadeDelete: true);
            AddForeignKey("Contacts.Opportunities", "PrimaryContactID", "Contacts.Contact", "ID");
            AddForeignKey("Contacts.Opportunities", "OFTDeliveryContactID", "Contacts.Contact", "ID");
            AddForeignKey("Contacts.Opportunities", "ClientProjectContactID", "Contacts.Contact", "ID");
            AddForeignKey("Contacts.Opportunities", "AccountID", "Accounts.Account", "ID", cascadeDelete: true);
            AddForeignKey("Accounts.AccountTerm", "ID", "Accounts.Account", "ID");
        }
    }
}
