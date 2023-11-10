// <copyright file="RolesManagement.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the roles management class</summary>
namespace Clarity.Ecommerce.SeedDatabase
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using DataModel;
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity;
    using Utilities;
    using Xunit;

    [Trait("Category", "Seed Database.RolesManagement")]
    public class RolesManagement
    {
        //private const string UserRole = "User";
        private const string AssistantManagerRole = "Assistant Manager";
        private const string BrandManagerRole = "Brand Manager";
        private const string RegionalManagerRole = "Regional Manager";
        private const string StoreAdministratorRole = "Store Administrator";
        private const string StoreManagerRole = "Store Manager";
        private const string CashierRole = "Cashier";

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertUserRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, CashierRole);
                var permissions = new List<string>
                {
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                    "Ordering.SalesOrder.ReadyForPickup",
                };

                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();
                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, CashierRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertAssistantManagerRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, AssistantManagerRole);
                var permissions = new List<string>
                {
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Ordering.SalesOrder.View",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Admin.Sales.SalesReturns.Create",
                    "Admin.Sales.SalesReturns.Update",
                    "Admin.Sales.SalesReturns.View",
                    "Returning.SalesReturn.Create",
                    "Returning.SalesReturn.Update",
                    "Returning.SalesReturn.View",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                    "Ordering.SalesOrder.ReadyForPickup",
                };

                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();

                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, AssistantManagerRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertStoreManagerRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, StoreManagerRole);
                var permissions = new List<string>
                {
                    "Admin.Accounts.Discounts.Create",
                    "Admin.Accounts.Discounts.Update",
                    "Admin.Accounts.Discounts.View",
                    "Admin.Accounts.Discounts.Deactivate",
                    "Admin.Accounts.Discounts.Reactivate",
                    "Admin.Accounts.Users.Create",
                    "Admin.Accounts.Users.Update",
                    "Admin.Accounts.Users.View",
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.Payments.View",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Accounts.Account.Create",
                    "Accounts.Account.Update",
                    "Accounts.Account.View",
                    "Accounts.Account.Deactivate",
                    "Accounts.Account.Reactivate",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Categories.Category.View",
                    "Contacts.Contact.View",
                    "Discounts.Discount.Create",
                    "Discounts.Discount.Update",
                    "Discounts.Discount.View",
                    "Discounts.Discount.Deactivate",
                    "Discounts.Discount.Reactivate",
                    "Geography.Address.Update",
                    "Geography.Address.View",
                    "Geography.Address.Deactivate",
                    "Geography.Address.Reactivate",
                    "Geography.Country.View",
                    "Geography.Region.View",
                    "Geography.ZipCode.View",
                    "Globalization.Language.View",
                    "Globalization.UiKey.View",
                    "Globalization.UiTranslation.View",
                    "Inventory.InventoryLocation.View",
                    "Inventory.InventoryLocationSection.View",
                    "Ordering.SalesOrder.Update",
                    "Ordering.SalesOrder.View",
                    "Ordering.SalesOrderItem.Create",
                    "Ordering.SalesOrderItem.Update",
                    "Ordering.SalesOrderItem.View",
                    "Ordering.SalesOrderItem.Deactivate",
                    "Ordering.SalesOrderItem.Reactivate",
                    "Payments.Payment.View",
                    "Products.Product.View",
                    "Products.ProductInventoryLocationSection.View",
                    "Products.ProductType.View",
                    "Reporting.Report.Create",
                    "Reporting.Report.Update",
                    "Reporting.Report.View",
                    "Reporting.Report.Deactivate",
                    "Reporting.Report.Reactivate",
                    "Reporting.ReportType.Create",
                    "Reporting.ReportType.Update",
                    "Reporting.ReportType.View",
                    "Reporting.ReportType.Deactivate",
                    "Reporting.ReportType.Reactivate",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Admin.System.ReportDesigner.View",
                    "Admin.System.ReportLoader.View",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Ordering.SalesOrder.Void",
                    "Admin.Sales.SalesReturns.Create",
                    "Admin.Sales.SalesReturns.Update",
                    "Admin.Sales.SalesReturns.View",
                    "Returning.SalesReturn.Create",
                    "Returning.SalesReturn.Update",
                    "Returning.SalesReturn.View",
                    "Returning.SalesReturnSalesOrder.Create",
                    "Returning.SalesReturnSalesOrder.Update",
                    "Returning.SalesReturnSalesOrder.View",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "StoreAdmin.Sales.SalesReturns",
                    "Returning.SalesReturn.Confirm",
                    "Returning.SalesReturn.Ship",
                    "Returning.SalesReturn.AddPayment",
                    "Returning.SalesReturn.Complete",
                    "Returning.SalesReturn.Void",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                    "Ordering.SalesOrder.ReadyForPickup",
                };
                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();

                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, StoreManagerRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertBrandManagerRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, BrandManagerRole);
                var permissions = new List<string>
                {
                    "Admin.Accounts.Discounts.Create",
                    "Admin.Accounts.Discounts.Update",
                    "Admin.Accounts.Discounts.View",
                    "Admin.Accounts.Discounts.Deactivate",
                    "Admin.Accounts.Discounts.Reactivate",
                    "Admin.Accounts.Stores.View",
                    "Admin.Accounts.Stores.Deactivate",
                    "Admin.Accounts.Stores.Reactivate",
                    "Admin.Accounts.Users.Create",
                    "Admin.Accounts.Users.Update",
                    "Admin.Accounts.Users.View",
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.Payments.View",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Accounts.Account.Create",
                    "Accounts.Account.Update",
                    "Accounts.Account.View",
                    "Accounts.Account.Deactivate",
                    "Accounts.Account.Reactivate",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Categories.Category.View",
                    "Contacts.Contact.View",
                    "Discounts.Discount.Create",
                    "Discounts.Discount.Update",
                    "Discounts.Discount.View",
                    "Discounts.Discount.Deactivate",
                    "Discounts.Discount.Reactivate",
                    "Geography.Address.Update",
                    "Geography.Address.View",
                    "Geography.Address.Deactivate",
                    "Geography.Address.Reactivate",
                    "Geography.Country.View",
                    "Geography.Region.View",
                    "Geography.ZipCode.View",
                    "Globalization.Language.View",
                    "Globalization.UiKey.View",
                    "Globalization.UiTranslation.View",
                    "Inventory.InventoryLocation.View",
                    "Inventory.InventoryLocationSection.View",
                    "Ordering.SalesOrder.Update",
                    "Ordering.SalesOrder.View",
                    "Ordering.SalesOrderItem.Create",
                    "Ordering.SalesOrderItem.Update",
                    "Ordering.SalesOrderItem.View",
                    "Ordering.SalesOrderItem.Deactivate",
                    "Ordering.SalesOrderItem.Reactivate",
                    "Payments.Payment.View",
                    "Products.Product.View",
                    "Products.Product.Deactivate",
                    "Products.Product.Reactivate",
                    "Products.ProductInventoryLocationSection.View",
                    "Products.ProductType.View",
                    "Reporting.Report.Create",
                    "Reporting.Report.Update",
                    "Reporting.Report.View",
                    "Reporting.Report.Deactivate",
                    "Reporting.Report.Reactivate",
                    "Reporting.ReportType.Create",
                    "Reporting.ReportType.Update",
                    "Reporting.ReportType.View",
                    "Reporting.ReportType.Deactivate",
                    "Reporting.ReportType.Reactivate",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Admin.System.ReportDesigner.View",
                    "Admin.System.ReportLoader.View",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Ordering.SalesOrder.Void",
                    "Admin.Sales.SalesReturns.Create",
                    "Admin.Sales.SalesReturns.Update",
                    "Admin.Sales.SalesReturns.View",
                    "Returning.SalesReturn.Create",
                    "Returning.SalesReturn.Update",
                    "Returning.SalesReturn.View",
                    "Returning.SalesReturnSalesOrder.Create",
                    "Returning.SalesReturnSalesOrder.Update",
                    "Returning.SalesReturnSalesOrder.View",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "StoreAdmin.Sales.SalesReturns",
                    "Returning.SalesReturn.Confirm",
                    "Returning.SalesReturn.Ship",
                    "Returning.SalesReturn.AddPayment",
                    "Returning.SalesReturn.Complete",
                    "Returning.SalesReturn.Void",
                    "Admin.Stores.Stores.Deactivate",
                    "Admin.Stores.Stores.Reactivate",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                };

                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();

                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, BrandManagerRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertRegionalManagerRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, RegionalManagerRole);
                var permissions = new List<string>
                {
                    "Admin.Accounts.Accounts.Create",
                    "Admin.Accounts.Accounts.Update",
                    "Admin.Accounts.Accounts.View",
                    "Admin.Accounts.Accounts.Deactivate",
                    "Admin.Accounts.Accounts.Reactivate",
                    "Admin.Accounts.Brands.View",
                    "Admin.Accounts.Discounts.Create",
                    "Admin.Accounts.Discounts.Update",
                    "Admin.Accounts.Discounts.View",
                    "Admin.Accounts.Discounts.Deactivate",
                    "Admin.Accounts.Discounts.Reactivate",
                    "Admin.Accounts.Stores.View",
                    "Admin.Accounts.Stores.Deactivate",
                    "Admin.Accounts.Stores.Reactivate",
                    "Admin.Accounts.Users.Create",
                    "Admin.Accounts.Users.Update",
                    "Admin.Accounts.Users.View",
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.Payments.View",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Accounts.Account.Create",
                    "Accounts.Account.Update",
                    "Accounts.Account.View",
                    "Accounts.Account.Deactivate",
                    "Accounts.Account.Reactivate",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Categories.Category.View",
                    "Contacts.Contact.View",
                    "Discounts.Discount.Create",
                    "Discounts.Discount.Update",
                    "Discounts.Discount.View",
                    "Discounts.Discount.Deactivate",
                    "Discounts.Discount.Reactivate",
                    "Geography.Address.Update",
                    "Geography.Address.View",
                    "Geography.Address.Deactivate",
                    "Geography.Address.Reactivate",
                    "Geography.Country.View",
                    "Geography.Region.View",
                    "Geography.ZipCode.View",
                    "Globalization.Language.View",
                    "Globalization.UiKey.View",
                    "Globalization.UiTranslation.View",
                    "Inventory.InventoryLocation.View",
                    "Inventory.InventoryLocationSection.View",
                    "Ordering.SalesOrder.Update",
                    "Ordering.SalesOrder.View",
                    "Ordering.SalesOrderItem.Create",
                    "Ordering.SalesOrderItem.Update",
                    "Ordering.SalesOrderItem.View",
                    "Ordering.SalesOrderItem.Deactivate",
                    "Ordering.SalesOrderItem.Reactivate",
                    "Payments.Payment.View",
                    "Products.Product.View",
                    "Products.Product.Deactivate",
                    "Products.Product.Reactivate",
                    "Products.ProductInventoryLocationSection.View",
                    "Products.ProductType.View",
                    "Reporting.Report.Create",
                    "Reporting.Report.Update",
                    "Reporting.Report.View",
                    "Reporting.Report.Deactivate",
                    "Reporting.Report.Reactivate",
                    "Reporting.ReportType.Create",
                    "Reporting.ReportType.Update",
                    "Reporting.ReportType.View",
                    "Reporting.ReportType.Deactivate",
                    "Reporting.ReportType.Reactivate",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Admin.System.ReportDesigner.View",
                    "Admin.System.ReportLoader.View",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Ordering.SalesOrder.Void",
                    "Admin.Sales.SalesReturns.Create",
                    "Admin.Sales.SalesReturns.Update",
                    "Admin.Sales.SalesReturns.View",
                    "Returning.SalesReturn.Create",
                    "Returning.SalesReturn.Update",
                    "Returning.SalesReturn.View",
                    "Returning.SalesReturnSalesOrder.Create",
                    "Returning.SalesReturnSalesOrder.Update",
                    "Returning.SalesReturnSalesOrder.View",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "StoreAdmin.Sales.SalesReturns",
                    "Returning.SalesReturn.Confirm",
                    "Returning.SalesReturn.Ship",
                    "Returning.SalesReturn.AddPayment",
                    "Returning.SalesReturn.Complete",
                    "Returning.SalesReturn.Void",
                    "Admin.Stores.Stores.Deactivate",
                    "Admin.Stores.Stores.Reactivate",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                };
                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();

                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, RegionalManagerRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void InsertStoreAdministratorRole()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var roleStore = new CEFRoleStore(context);
                var roleManager = new RoleManager<UserRole, int>(roleStore);
                CreateRoleIfDoesntExist(roleManager, StoreAdministratorRole);
                var permissions = new List<string>
                {
                    "Admin.Accounts.Accounts.Create",
                    "Admin.Accounts.Accounts.Update",
                    "Admin.Accounts.Accounts.View",
                    "Admin.Accounts.Accounts.Deactivate",
                    "Admin.Accounts.Accounts.Reactivate",
                    "Admin.Accounts.Brands.View",
                    "Admin.Accounts.Discounts.Create",
                    "Admin.Accounts.Discounts.Update",
                    "Admin.Accounts.Discounts.View",
                    "Admin.Accounts.Discounts.Deactivate",
                    "Admin.Accounts.Discounts.Reactivate",
                    "Admin.Accounts.Stores.View",
                    "Admin.Accounts.Stores.Deactivate",
                    "Admin.Accounts.Stores.Reactivate",
                    "Admin.Accounts.Users.Create",
                    "Admin.Accounts.Users.Update",
                    "Admin.Accounts.Users.View",
                    "Admin.Accounts.Users.Deactivate",
                    "Admin.Accounts.Users.Reactivate",
                    "Admin.Inventory.Categories.View",
                    "Admin.Inventory.Manufacturers.View",
                    "Admin.Inventory.Products.View",
                    "Admin.Inventory.Vendors.View",
                    "Admin.Inventory.Warehouses.Update",
                    "Admin.Sales.Payments.Create",
                    "Admin.Sales.Payments.Update",
                    "Admin.Sales.Payments.View",
                    "Admin.Sales.Payments.Deactivate",
                    "Admin.Sales.Payments.Reactivate",
                    "Admin.Sales.SalesOrders.Create",
                    "Admin.Sales.SalesOrders.Update",
                    "Admin.Sales.SalesOrders.View",
                    "Admin.Shipping.CarrierAccounts.View",
                    "Admin.Shipping.Packages.View",
                    "Admin.System.Settings.View",
                    "Admin.System.UiKeys.Create",
                    "Admin.System.UiKeys.Update",
                    "Admin.System.UiKeys.View",
                    "Admin.System.UiKeys.Deactivate",
                    "Admin.System.UiKeys.Reactivate",
                    "Admin.System.UiTranslations.Create",
                    "Admin.System.UiTranslations.Update",
                    "Admin.System.UiTranslations.View",
                    "Admin.System.UiTranslations.Deactivate",
                    "Admin.System.UiTranslations.Reactivate",
                    "Admin.States.States.View",
                    "Admin.Statuses.Statuses.View",
                    "Admin.Types.Types.View",
                    "Accounts.Account.Create",
                    "Accounts.Account.Update",
                    "Accounts.Account.View",
                    "Accounts.Account.Deactivate",
                    "Accounts.Account.Reactivate",
                    "Accounts.AccountContact.Create",
                    "Accounts.AccountContact.Update",
                    "Accounts.AccountContact.View",
                    "Accounts.AccountContact.Deactivate",
                    "Accounts.AccountContact.Reactivate",
                    "Categories.Category.View",
                    "Contacts.Contact.View",
                    "Discounts.Discount.Create",
                    "Discounts.Discount.Update",
                    "Discounts.Discount.View",
                    "Discounts.Discount.Deactivate",
                    "Discounts.Discount.Reactivate",
                    "Geography.Address.Update",
                    "Geography.Address.View",
                    "Geography.Address.Deactivate",
                    "Geography.Address.Reactivate",
                    "Geography.Country.View",
                    "Geography.Region.View",
                    "Geography.ZipCode.View",
                    "Globalization.Language.Create",
                    "Globalization.Language.Update",
                    "Globalization.Language.View",
                    "Globalization.Language.Deactivate",
                    "Globalization.Language.Reactivate",
                    "Globalization.UiKey.Create",
                    "Globalization.UiKey.Update",
                    "Globalization.UiKey.View",
                    "Globalization.UiKey.Deactivate",
                    "Globalization.UiKey.Reactivate",
                    "Globalization.UiTranslation.Create",
                    "Globalization.UiTranslation.Update",
                    "Globalization.UiTranslation.View",
                    "Globalization.UiTranslation.Deactivate",
                    "Globalization.UiTranslation.Reactivate",
                    "Inventory.InventoryLocation.View",
                    "Inventory.InventoryLocationSection.View",
                    "Ordering.SalesOrder.Update",
                    "Ordering.SalesOrder.View",
                    "Ordering.SalesOrderItem.Create",
                    "Ordering.SalesOrderItem.Update",
                    "Ordering.SalesOrderItem.View",
                    "Ordering.SalesOrderItem.Deactivate",
                    "Ordering.SalesOrderItem.Reactivate",
                    "Payments.Payment.View",
                    "Products.Product.View",
                    "Products.Product.Deactivate",
                    "Products.Product.Reactivate",
                    "Products.ProductInventoryLocationSection.View",
                    "Products.ProductType.View",
                    "Reporting.Report.Create",
                    "Reporting.Report.Update",
                    "Reporting.Report.View",
                    "Reporting.Report.Deactivate",
                    "Reporting.Report.Reactivate",
                    "Reporting.ReportType.Create",
                    "Reporting.ReportType.Update",
                    "Reporting.ReportType.View",
                    "Reporting.ReportType.Deactivate",
                    "Reporting.ReportType.Reactivate",
                    "Tracking.PageView.Create",
                    "Storefront.UserDashboard.AddressBook.View",
                    "Storefront.UserDashboard.AddressBook.AddTo",
                    "Storefront.UserDashboard.AddressBook.Update",
                    "Storefront.UserDashboard.AddressBook.RemoveFrom",
                    "Storefront.UserDashboard.Wallet.View",
                    "Storefront.UserDashboard.Wallet.AddTo",
                    "Storefront.UserDashboard.Wallet.Update",
                    "Storefront.UserDashboard.Wallet.RemoveFrom",
                    "Storefront.UserDashboard.WishList.View",
                    "Storefront.UserDashboard.WishList.AddTo",
                    "Storefront.UserDashboard.WishList.RemoveFrom",
                    "Storefront.UserDashboard.NotifyMeList.View",
                    "Storefront.UserDashboard.NotifyMeList.AddTo",
                    "Storefront.UserDashboard.NotifyMeList.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.View",
                    "Storefront.UserDashboard.ShoppingLists.Create",
                    "Storefront.UserDashboard.ShoppingLists.AddTo",
                    "Storefront.UserDashboard.ShoppingLists.RemoveFrom",
                    "Storefront.UserDashboard.ShoppingLists.Deactivate",
                    "Storefront.UserDashboard.ShoppingLists.Reactivate",
                    "Storefront.Checkout.Initiate",
                    "Storefront.Checkout.UseAddressBook",
                    "Storefront.Checkout.UseNewAddressForBilling",
                    "Storefront.Checkout.UseNewAddressForShipping",
                    "Storefront.Checkout.UseWallet",
                    "Storefront.Checkout.UseCreditCards",
                    "Storefront.Checkout.UseNewCreditCard",
                    "Storefront.Checkout.UseEChecks",
                    "Storefront.Checkout.UseNewECheck",
                    "Storefront.Checkout.UsePONumbers",
                    "Admin.System.API.View",
                    "Admin.System.ScheduledTasks.View",
                    "Admin.System.ReportDesigner.View",
                    "Admin.System.ReportLoader.View",
                    "Ordering.SalesOrder.Confirm",
                    "Ordering.SalesOrder.Backorder",
                    "Ordering.SalesOrder.Split",
                    "Ordering.SalesOrder.CreateInvoice",
                    "Ordering.SalesOrder.AddPayment",
                    "Ordering.SalesOrder.CreatePickTicket",
                    "Ordering.SalesOrder.DropShip",
                    "Ordering.SalesOrder.Ship",
                    "Ordering.SalesOrder.Complete",
                    "Ordering.SalesOrder.Void",
                    "Admin.Sales.SalesReturns.Create",
                    "Admin.Sales.SalesReturns.Update",
                    "Admin.Sales.SalesReturns.View",
                    "Returning.SalesReturn.Create",
                    "Returning.SalesReturn.Update",
                    "Returning.SalesReturn.View",
                    "Returning.SalesReturn.Deactivate",
                    "Returning.SalesReturn.Reactivate",
                    "Returning.SalesReturnSalesOrder.Create",
                    "Returning.SalesReturnSalesOrder.Update",
                    "Returning.SalesReturnSalesOrder.View",
                    "Storefront.UserDashboard.SalesReturns.View",
                    "Storefront.UserDashboard.SalesReturns.Cancel",
                    "StoreAdmin.Sales.SalesReturns",
                    "Returning.SalesReturn.Confirm",
                    "Returning.SalesReturn.Ship",
                    "Returning.SalesReturn.AddPayment",
                    "Returning.SalesReturn.Complete",
                    "Returning.SalesReturn.Void",
                    "Admin.Stores.Brands.View",
                    "Admin.Stores.Stores.Deactivate",
                    "Admin.Stores.Stores.Reactivate",
                    "Storefront.UserDashboard.SalesReturns.Submit",
                    "Admin.Applications.ScheduledTasks.View",
                    "Admin.Applications.ReportDesigner.View",
                    "Admin.Applications.ReportLoader.View",
                };

                // Run all the permissions through checks to make sure they exist
                foreach (var permission in permissions.Distinct())
                {
                    if (context.Permissions.Any(x => x.Name == permission)) { continue; }
                    context.Permissions.Add(new Permission { Name = permission });
                }
                context.SaveUnitOfWork();

                foreach (var permission in permissions)
                {
                    AssignPermissionToRoleIfNotAssigned(context, StoreAdministratorRole, permission);
                }
            }
        }

        [Fact(Skip = "Comment out the Skip when you need to seed the database")]
        public void AppendCEFStoreAdminPermissions()
        {
            var perms = new[]
            {
                "Products.Product.Create",
                "Products.Product.Update",
                "Products.Product.View",
                "Products.Product.Deactivate",
                "Products.Product.Reactivate",
                "Products.Product.Delete",
            };
            using (var context = new ClarityEcommerceEntities())
            {
                var addedAnything = false;
                foreach (var perm in perms)
                {
                    if (context.Permissions.AsNoTracking().Any(x => x.Name == perm)) { continue; }
                    context.Permissions.Add(new Permission { Name = perm });
                    addedAnything = true;
                }
                if (addedAnything)
                {
                    context.SaveUnitOfWork();
                }
                addedAnything = false;
                var existingPerms = context.Permissions
                    .AsNoTracking()
                    .Where(x => perms.Contains(x.Name))
                    .Select(x => x.Id);
                var storeAdminRoleId = context.Roles
                    .AsNoTracking()
                    .Select(x => new { x.Id, x.Name })
                    .Single(x => x.Name == "CEF Store Administrator")
                    .Id;
                foreach (var perm in existingPerms)
                {
                    var alreadyAppended = context.RolePermissions
                        .AsNoTracking()
                        .Any(x => x.RoleId == storeAdminRoleId && existingPerms.Contains(x.PermissionId));
                    if (alreadyAppended) { continue; }
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = storeAdminRoleId,
                        PermissionId = perm,
                    });
                    addedAnything = true;
                }
                if (addedAnything)
                {
                    context.SaveUnitOfWork();
                }
            }
        }

        private static void CreateRoleIfDoesntExist(RoleManager<UserRole, int> roleManager, string name)
        {
            if (roleManager.RoleExists(name))
            {
                return;
            }
            var result = roleManager.Create(new UserRole(name));
            if (result.Succeeded)
            {
                // TODO: Logging successful role create
                return;
            }
            var message = result.Errors.Aggregate($"There were errors creating the {name} Role: ", (c, n) => c + string.Empty + n);
            Debug.WriteLine(message);
            // TODO: Logging role create error
        }

        private static void AssignPermissionToRoleIfNotAssigned(IClarityEcommerceEntities context, string role, string permission)
        {
            if (context.RolePermissions.Any(x => x.Role!.Name == role && x.Permission!.Name == permission)) { return; }
            var roleID = context.Roles.Single(x => x.Name == role).Id;
            var permissionID = context.Permissions.Single(x => x.Name == permission).Id;
            context.RolePermissions.Add(new RolePermission { RoleId = roleID, PermissionId = permissionID });
            context.SaveUnitOfWork();
        }
    }
}
