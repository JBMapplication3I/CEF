// <copyright file="JBMRoutes.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>Category and Brand Setup</summary>
    [PublicAPI,
     RequiredPermission("Products.Product.Update", "Products.ProductCategory.Create"),
     UsedInStorefront,
     Route("/Product/UpdateBrandsAndCategories", "POST",
        Summary = "Updates the product with associated brand(s) and categories.")]
    public class ProductBrandAndCategorySetup : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "ItemNumber", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? ItemNumber { get; set; }

        [ApiMember(Name = "CategoryString", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? CategoryString { get; set; }
    }

    /// <summary>UOM Setup Setup</summary>
    [PublicAPI,
     RequiredPermission("Products.Product.Update"),
     UsedInStorefront,
     Route("/Product/AddOrUpdateUOMs", "POST",
        Summary = "Adds or Updates the product's associated units of measure.")]
    public class AddOrUpdateProductUOMs : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "ProductKey", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? ProductKey { get; set; }

        [ApiMember(Name = "UOMCode", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? UOMCode { get; set; }

        [ApiMember(Name = "UOMConversion", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? UOMConversion { get; set; }

        [ApiMember(Name = "UOMName", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? UOMName { get; set; }
    }

    /// <summary>Invoices from Fusion to CEF</summary>
    [PublicAPI,
     RequiredPermission("Invoicing.SalesInvoice.Create"),
     UsedInStorefront,
     Route("/Invoicing/CreateInvoices", "POST",
        Summary = "Updates the product with associated brand(s) and categories.")]
    public class CreateInvoices : FusionInvoiceResponse, IReturn<CEFActionResponse>
    {
    }

    /// <summary>Update Order Statuses from Fusion</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Ordering/UpdateOrderStatus", "POST",
        Summary = "Updates the order's status from Fusion.")]
    public class UpdateOrderStatus : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "TransactionNumber", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? TransactionNumber { get; set; }

        [ApiMember(Name = "StatusCode", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? StatusCode { get; set; }
    }

    /// <summary>Update Order lines from Fusion</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Ordering/UpdateOrder", "POST",
        Summary = "Updates the order's status from Fusion.")]
    public class UpdateOrder : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CompareDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? CompareDate { get; set; }
    }

    /// <summary>Creating account user roles and product required roles for price lists.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Pricing/PriceListToAccountAndProductRoles", "POST",
        Summary = "Updates the order's status from Fusion.")]
    public class PriceListToAccountAndProductRoles : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "PriceListID", DataType = "long", ParameterType = "query", IsRequired = true)]
        public long? PriceListID { get; set; }

        [ApiMember(Name = "PriceListName", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? PriceListName { get; set; }

        [ApiMember(Name = "AccountKey", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? AccountKey { get; set; }
    }

    /// <summary>Setup primarty uom.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Product/UpsertPrimaryUOM", "POST",
        Summary = "Updates the items primary UOM.")]
    public class UpdatePrimaryUOM : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "ProductKey", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? ProductKey { get; set; }

        [ApiMember(Name = "UOM", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? UOM { get; set; }
    }

    /// <summary>New Accounts/Contacts to Fusion Listener.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Account/SyncNewAccountsAndContactsToFusion", "POST",
        Summary = "Updates the items primary UOM.")]
    public class SyncNewAccountsAndContactsToFusion : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "CompareDate", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? CompareDate { get; set; }
    }

    /// <summary>Updating accounts from fusion.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Accounts/UpdateFromFusion", "POST",
        Summary = "Updates Accounts from Fusion.")]
    public class UpdateAccountFromFusion : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "AccountID", DataType = "int", ParameterType = "body", IsRequired = true)]
        public int? AccountID { get; set; }

        [ApiMember(Name = "CustomKey", DataType = "string", ParameterType = "body", IsRequired = true)]
        public string? CustomKey { get; set; }
    }

    /// <summary>Grabs the user by its username.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Accounts/GetUserByUsername", "POST",
        Summary = "Grabs the user by its username.")]
    public class GetUserByUsername : IReturn<UserModel>
    {
        [ApiMember(Name = "Username", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? Username { get; set; }
    }

    /// <summary>Creates Accounts, Account Contacts, and Contacts for current accounts/users.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Accounts/AccountSites", "POST",
        Summary = "Creates Accounts, Account Contacts, and Contacts for current accounts/users.")]
    public class AccountSites : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "CustomerAccountInformation", DataType = "CustomerAccountInformation", ParameterType = "body", IsRequired = true)]
        public CustomerAccountInformation? CustomerAccountInformation { get; set; }
    }

    /// <summary>Updates the UOM's of all valid items in CEF.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Products/InventoryItem", "POST",
        Summary = "Updates the items primary UOM.")]
    public class InventoryItem : IReturn<CEFActionResponse>
    {
    }

    /// <summary>Updates the UOM's of all valid items in CEF.</summary>
    [PublicAPI,
     UsedInStorefront,
     Route("/Products/ListProductsForGoLive", "GET",
        Summary = "Updates the items primary UOM.")]
    public class ListProductsForGoLive : IReturn<List<string>>
    {
        [ApiMember(Name = "offset", DataType = "int?", ParameterType = "query", IsRequired = true)]
        public int? offset { get; set; }

        [ApiMember(Name = "limit", DataType = "int?", ParameterType = "query", IsRequired = true)]
        public int? limit { get; set; }
    }

    /// <summary>Updates the UOM's of all valid items in CEF.</summary>
    [PublicAPI,
     UsedInStorefront,
     Route("/Accounts/MissingAccountNumbers", "GET",
        Summary = "Updates the items primary UOM.")]
    public class MissingAccountNumbers : IReturn<List<int>>
    {
    }

    /// <summary>Updates the UOM's of all valid items in CEF.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Accounts/PriceLists", "GET",
        Summary = "Get the current price lists for an account.")]
    public class CurrentAccountPriceLists : IReturn<CEFActionResponse?>
    {
        [ApiMember(Name = "AccountKey", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? AccountKey { get; set; }
    }

    /// <summary>Used to read a shipment by ID.</summary>
    [PublicAPI,
     UsedInStorefront,
     Route("/Shipping/Shipment/ByID", "GET",
        Summary = "Get the current price lists for an account.")]
    public class GetShipmentByID : IReturn<ShipmentModel?>
    {
        [ApiMember(Name = "ShipmentID", DataType = "int?", ParameterType = "query", IsRequired = true)]
        public int? ShipmentID { get; set; }
    }

    /// <summary>Used to read a shipment by ID.</summary>
    [PublicAPI,
     UsedInStorefront,
     Route("/Shipping/Shipment/BySalesGroupID", "GET",
        Summary = "Get the current price lists for an account.")]
    public class GetShipmentsBySalesGroupID : IReturn<List<ShipmentModel>?>
    {
        [ApiMember(Name = "SalesGroupID", DataType = "int?", ParameterType = "query", IsRequired = true)]
        public int? SalesGroupID { get; set; }
    }

    /// <summary>Used to read a shipment by ID.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Shipping/Shipment/CreateShipments", "POST",
        Summary = "Create shipments for order.")]
    public class CreateShipmentsForOrder : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "Shipments", DataType = "ShipmentResponse", ParameterType = "body", IsRequired = true)]
        public List<JBMShipment>? Shipments { get; set; }
    }


    /// <summary>Used to read a shipment by ID.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Products/InventoryUpdate", "POST",
        Summary = "Create shipments for order.")]
    public class UpdateInventory : IReturn<CEFActionResponse>
    {
    }

    /// <summary>Used to read a shipment by ID.</summary>
    [PublicAPI,
     RequiredRole("CEF Global Administrator"),
     UsedInStorefront,
     Route("/Ordering/TryOrderAgain", "POST",
        Summary = "Retries an order to Fusion.")]
    public class TryOrderAgain : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = "SalesGroupID", DataType = "int?", ParameterType = "query", IsRequired = true)]
        public int? SalesGroupID { get; set; }
    }
}
