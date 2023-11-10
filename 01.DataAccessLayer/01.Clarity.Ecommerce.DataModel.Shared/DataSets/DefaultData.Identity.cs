// <copyright file="DefaultData.Identity.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default data class (identity portion)</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1000
#nullable enable
#pragma warning disable SA1201 // Elements should appear in the correct order
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using JetBrains.Annotations;
    using Microsoft.AspNet.Identity;
    using Utilities;

    /// <summary>An identity data.</summary>
    public partial class IdentityData
    {
        private struct SeedUser
        {
            internal string UserName;

            internal string DisplayName;

            internal string Password;

            internal string[] Roles;
        }

        private const string AdminRole = "CEF Global Administrator";
        private const string AffiliateAdminRole = "CEF Affiliate Administrator";
        private const string LocalAdminRole = "CEF Local Administrator";
        private const string StoreAdminRole = "CEF Store Administrator";
        private const string BrandAdminRole = "CEF Brand Administrator";
        private const string FranchiseAdminRole = "CEF Brand Administrator";
        private const string VendorAdminRole = "CEF Vendor Administrator";
        private const string ClarityConnectDataRole = "Clarity Connect Data";
        private const string UserRole = "CEF User";

        private static readonly string[] AllRoles =
        {
            AdminRole,
            AffiliateAdminRole,
            LocalAdminRole,
            StoreAdminRole,
            BrandAdminRole,
            FranchiseAdminRole,
            VendorAdminRole,
            ClarityConnectDataRole,
            UserRole,
        };

        private static readonly SeedUser[] SeedUsers =
        {
            new()
            {
                UserName = "Clarity",
                DisplayName = "Clarity Admin",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { AdminRole, AffiliateAdminRole, LocalAdminRole, StoreAdminRole, FranchiseAdminRole, BrandAdminRole },
            },
            new()
            {
                UserName = "Admin",
                DisplayName = "Clarity Admin",
                Password = "W$Or2zbB1DF&12NF",
                Roles = new[] { AdminRole },
            },
            new()
            {
                UserName = "ClarityStore",
                DisplayName = "Clarity Store",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { StoreAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityBrand",
                DisplayName = "Clarity Brand",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { BrandAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityFranchise",
                DisplayName = "Clarity Franchise",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { FranchiseAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityVendor",
                DisplayName = "Clarity Vendor",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { VendorAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityAffiliate",
                DisplayName = "Clarity Affiliate",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { AffiliateAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityLocalAdmin",
                DisplayName = "Clarity Local Admin",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { LocalAdminRole, UserRole },
            },
            new()
            {
                UserName = "ClarityConnect",
                DisplayName = "Clarity Connect",
                Password = "5%Emp5Yj3^62",
                Roles = new[] { ClarityConnectDataRole },
            },
            new()
            {
                UserName = "ClarityUser",
                DisplayName = "Clarity User",
                Password = "QAZXSWwsxzaq!@#$4321",
                Roles = new[] { UserRole },
            },
        };

#if ORACLE
        private readonly OracleClarityEcommerceEntities context;
#else
        private readonly ClarityEcommerceEntities context;
#endif

        /// <summary>Initializes a new instance of the <see cref="IdentityData"/> class.</summary>
        /// <param name="context">The context.</param>
#if ORACLE
        public IdentityData(OracleClarityEcommerceEntities context)
#else
        public IdentityData(ClarityEcommerceEntities context)
#endif
        {
            this.context = context;
        }

        private static IEnumerable<string> StoreAdminPerms { get; } = new List<string>
        {
            "StoreAdmin.Accounts.Accounts",
            "StoreAdmin.Accounts.Brands",
            "StoreAdmin.Accounts.Franchises",
            "StoreAdmin.Accounts.Badges",
            "StoreAdmin.Accounts.Discounts",
            "StoreAdmin.Accounts.Reviews",
            "StoreAdmin.Accounts.Roles",
            "StoreAdmin.Accounts.SiteDomains",
            "StoreAdmin.Accounts.SocialProviders",
            "StoreAdmin.Accounts.Stores",
            "StoreAdmin.Accounts.Subscriptions",
            "StoreAdmin.Accounts.Users",
            "StoreAdmin.Inventory.Attributes",
            "StoreAdmin.Inventory.Categories",
            "StoreAdmin.Inventory.Manufacturers",
            "StoreAdmin.Inventory.Products",
            "StoreAdmin.Inventory.Vendors",
            "StoreAdmin.Inventory.Warehouses",
            "StoreAdmin.Sales.Payments",
            "StoreAdmin.Sales.SalesInvoices",
            "StoreAdmin.Sales.SalesOrders",
            "StoreAdmin.Sales.SalesReturns",
            "StoreAdmin.Sales.SalesQuotes",
            "StoreAdmin.Sales.PurchaseOrders",
            "StoreAdmin.Shipping.CarrierAccounts",
            "StoreAdmin.Shipping.Packages",
            "StoreAdmin.States.States",
            "StoreAdmin.Statuses.Statuses",
            "StoreAdmin.System.Settings",
            "StoreAdmin.Types.Types",
            "StoreAdmin.Types.PricePoints",
            "StoreAdmin.Types.CustomerPriorities",
            "StoreAdmin.Types.ContactMethods",
            "Storefront.StoreDashboard.SalesOrders",
            "Storefront.StoreDashboard.SalesInvoices",
            "Storefront.StoreDashboard.SalesReturns",
            "Storefront.StoreDashboard.PurchaseOrders",
            "Storefront.StoreDashboard.SalesQuotes",
            "Products.Product.Create",
            "Products.Product.Update",
            "Products.Product.View",
            "Products.Product.Deactivate",
            "Products.Product.Reactivate",
            "Products.Product.Delete",
        };

        private static IEnumerable<string> BrandAdminPerms { get; } = new List<string>
        {
            "BrandAdmin.Accounts.Accounts",
            "BrandAdmin.Accounts.Brands",
            "BrandAdmin.Accounts.Badges",
            "BrandAdmin.Accounts.Discounts",
            "BrandAdmin.Accounts.Reviews",
            "BrandAdmin.Accounts.Roles",
            "BrandAdmin.Accounts.SiteDomains",
            "BrandAdmin.Accounts.SocialProviders",
            "BrandAdmin.Accounts.Stores",
            "BrandAdmin.Accounts.Subscriptions",
            "BrandAdmin.Accounts.Users",
            "BrandAdmin.Inventory.Attributes",
            "BrandAdmin.Inventory.Categories",
            "BrandAdmin.Inventory.Manufacturers",
            "BrandAdmin.Inventory.Products",
            "BrandAdmin.Inventory.Vendors",
            "BrandAdmin.Inventory.Warehouses",
            "BrandAdmin.Sales.Payments",
            "BrandAdmin.Sales.SalesInvoices",
            "BrandAdmin.Sales.SalesOrders",
            "BrandAdmin.Sales.SalesReturns",
            "BrandAdmin.Sales.SalesQuotes",
            "BrandAdmin.Sales.PurchaseOrders",
            "BrandAdmin.Shipping.CarrierAccounts",
            "BrandAdmin.Shipping.Packages",
            "BrandAdmin.States.States",
            "BrandAdmin.Statuses.Statuses",
            "BrandAdmin.System.Settings",
            "BrandAdmin.Types.Types",
            "BrandAdmin.Types.PricePoints",
            "BrandAdmin.Types.CustomerPriorities",
            "BrandAdmin.Types.ContactMethods",
            "Storefront.BrandDashboard.SalesOrders",
            "Storefront.BrandDashboard.SalesInvoices",
            "Storefront.BrandDashboard.SalesReturns",
            "Storefront.BrandDashboard.PurchaseOrders",
            "Storefront.BrandDashboard.SalesQuotes",
            "Products.Product.Create",
            "Products.Product.Update",
            "Products.Product.View",
            "Products.Product.Deactivate",
            "Products.Product.Reactivate",
            "Products.Product.Delete",
        };

        private static IEnumerable<string> FranchiseAdminPerms { get; } = new List<string>
        {
            "FranchiseAdmin.Accounts.Accounts",
            "FranchiseAdmin.Accounts.Franchises",
            "FranchiseAdmin.Accounts.Badges",
            "FranchiseAdmin.Accounts.Discounts",
            "FranchiseAdmin.Accounts.Reviews",
            "FranchiseAdmin.Accounts.Roles",
            "FranchiseAdmin.Accounts.SiteDomains",
            "FranchiseAdmin.Accounts.SocialProviders",
            "FranchiseAdmin.Accounts.Stores",
            "FranchiseAdmin.Accounts.Subscriptions",
            "FranchiseAdmin.Accounts.Users",
            "FranchiseAdmin.Inventory.Attributes",
            "FranchiseAdmin.Inventory.Categories",
            "FranchiseAdmin.Inventory.Manufacturers",
            "FranchiseAdmin.Inventory.Products",
            "FranchiseAdmin.Inventory.Vendors",
            "FranchiseAdmin.Inventory.Warehouses",
            "FranchiseAdmin.Sales.Payments",
            "FranchiseAdmin.Sales.SalesInvoices",
            "FranchiseAdmin.Sales.SalesOrders",
            "FranchiseAdmin.Sales.SalesReturns",
            "FranchiseAdmin.Sales.SalesQuotes",
            "FranchiseAdmin.Sales.PurchaseOrders",
            "FranchiseAdmin.Shipping.CarrierAccounts",
            "FranchiseAdmin.Shipping.Packages",
            "FranchiseAdmin.States.States",
            "FranchiseAdmin.Statuses.Statuses",
            "FranchiseAdmin.System.Settings",
            "FranchiseAdmin.Types.Types",
            "FranchiseAdmin.Types.PricePoints",
            "FranchiseAdmin.Types.CustomerPriorities",
            "FranchiseAdmin.Types.ContactMethods",
            "Storefront.FranchiseDashboard.SalesOrders",
            "Storefront.FranchiseDashboard.SalesInvoices",
            "Storefront.FranchiseDashboard.SalesReturns",
            "Storefront.FranchiseDashboard.PurchaseOrders",
            "Storefront.FranchiseDashboard.SalesQuotes",
            "Products.Product.Create",
            "Products.Product.Update",
            "Products.Product.View",
            "Products.Product.Deactivate",
            "Products.Product.Reactivate",
            "Products.Product.Delete",
        };

        private static IEnumerable<string> VendorAdminPerms { get; } = new List<string>
        {
            "VendorAdmin.Inventory.Products",
            "Products.Product.Create",
            "Products.Product.Update",
            "Products.Product.View",
            "Products.Product.Deactivate",
            "Products.Product.Reactivate",
        };

        private static IEnumerable<string> StorefrontPerms { get; } = new List<string>
        {
            "Storefront.UserDashboard.SalesOrders.View",
            "Storefront.UserDashboard.SalesOrders.Cancel",
            "Storefront.UserDashboard.SalesReturns.View",
            "Storefront.UserDashboard.SalesReturns.Cancel",
            "Storefront.UserDashboard.SalesReturns.Submit",
            "Storefront.UserDashboard.SalesInvoices.View",
            "Storefront.UserDashboard.SalesInvoices.MakePayment",
            "Storefront.UserDashboard.SalesGroups.View",
            "Storefront.UserDashboard.SalesQuotes.View",
            "Storefront.UserDashboard.SalesQuotes.Cancel",
            "Storefront.UserDashboard.SalesQuotes.Submit",
            "Storefront.UserDashboard.SampleRequests.View",
            "Storefront.UserDashboard.SampleRequests.Submit",
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
            "Storefront.UserDashboard.ShoppingLists.Delete",
            "Storefront.UserDashboard.Downloads.View",
            "Storefront.UserDashboard.Downloads.Download",
            "Storefront.UserDashboard.Uploads.View",
            "Storefront.UserDashboard.Uploads.Download",
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
            "Storefront.Checkout.UsePayPal",
            "Messaging.Message.View",
            "Messaging.Message.Create",
            "Messaging.Message.Update",
            "Messaging.Message.Deactivate",
            "Messaging.Message.Reactivate",
            "Messaging.Message.Delete",
            "Accounts.AccountContact.View",
            "Accounts.AccountContact.Create",
            "Accounts.AccountContact.Update",
            "Accounts.AccountContact.Deactivate",
            "Accounts.AccountContact.Reactivate",
            "Accounts.AccountContact.Delete",
            "Shopping.Cart.Update",
        };

        private static List<string> Permissions { get; } = new();

        [PublicAPI]
        private enum NormalPermissions
        {
            /// <summary>An enum constant representing the create option.</summary>
            [Description("Create")]
            Create,

            /// <summary>An enum constant representing the update option.</summary>
            [Description("Update")]
            Update,

            /// <summary>An enum constant representing the view option.</summary>
            [Description("View")]
            View,

            /// <summary>An enum constant representing the deactivate option.</summary>
            [Description("Deactivate")]
            Deactivate,

            /// <summary>An enum constant representing the reactivate option.</summary>
            [Description("Reactivate")]
            Reactivate,

            /// <summary>An enum constant representing the delete option.</summary>
            [Description("Delete")]
            Delete,
        }

        /// <summary>Populates this IdentityData.</summary>
        public void Populate()
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            AddIdentityUserDataAsync().Wait(30_000);
        }

        private static bool CreateRoleIfDoesntExist(RoleManager<UserRole, int> roleManager, string name)
        {
            if (roleManager.RoleExists(name))
            {
                return true;
            }
            var result = roleManager.Create(new(name));
            if (result.Succeeded)
            {
                // TODO: Logging successful role create
                return true;
            }
            var message = result.Errors.Aggregate(
                $"There were errors creating the {name} Role: ",
                (c, n) => $"{c}\r\n{n}");
            Debug.WriteLine(message);
            // TODO: Logging role create error
            return false;
        }

        private static (bool success, string message) CreateUserIfDoesntExist(
            Microsoft.AspNet.Identity2.IUserManager<User, int> userManager,
            string userName,
            string password,
            string displayName)
        {
            var timestamp = DateTime.Now;
            var existing = userManager.Users.SingleOrDefault(x => x.UserName == userName);
            if (existing != null && !userManager.HasPasswordAsync(existing.Id).Result)
            {
                var addPasswordResult1 = userManager.AddPasswordAsync(existing.Id, password).Result;
                return addPasswordResult1.Succeeded
                    ? (true, "Added a password to existing user")
                    : (false, "Unable to add a password to existing user");
            }
            if (userManager.Users.Any(x => x.UserName == userName))
            {
                return (true, "Existing user exists and already has a password");
            }
            var user = new User
            {
                UserName = userName,
                Active = true,
                CreatedDate = timestamp,
                CustomKey = userName,
                StatusID = 1,
                TypeID = 1,
                Email = $"{userName.ToLower()}@claritydemos.com",
                EmailConfirmed = true,
                PhoneNumber = "+1-800-928-8160",
                PhoneNumberConfirmed = true,
                DisplayName = displayName,
                IsApproved = true,
                Contact = new()
                {
                    CreatedDate = timestamp,
                    Active = true,
                    CustomKey = userName,
                    FirstName = displayName.Split(' ')[0],
                    LastName = displayName.Split(' ')[1],
                    FullName = displayName,
                    TypeID = 1,
                    Email1 = $"{userName.ToLower()}@claritydemos.com",
                    Phone1 = "+1-800-928-8160",
                    Website1 = "https://clarity-ventures.com/",
                    Address = new()
                    {
                        CreatedDate = timestamp,
                        Active = true,
                        CustomKey = "Clarity HQ",
                        Company = "Development Team",
                        Street1 = "6805 N Capital of TX Hwy",
                        Street2 = "Ste 312",
                        City = "Austin",
                        RegionID = 43,
                        PostalCode = "78731",
                        CountryID = 1,
                        Latitude = 30.3903669m,
                        Longitude = -97.7487979m,
                    },
                },
            };
            var result = userManager.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                var message = result.Errors.Aggregate(
                    $"There were errors creating the {userName} User: ",
                    (c, n) => $"{c}\r\n{n}");
                Debug.WriteLine(message);
                // TODO: Logging user create error
                return (false, message);
            }
            if (userManager.HasPasswordAsync(user.ID).Result)
            {
                return (true, "successfully created the user");
            }
            var addPasswordResult = userManager.AddPasswordAsync(user.ID, password).Result;
            // TODO: Logging successful user create
            return (addPasswordResult.Succeeded,
                addPasswordResult.Succeeded
                    ? string.Empty
                    : addPasswordResult.Errors.Aggregate(
                        $"There were errors creating the {userName} User while adding the password: ",
                        (c, n) => $"{c}\r\n{n}"));
        }

        private static void AssignPermissionToRoleIfNotAssigned(
            IClarityEcommerceEntities context,
            string role,
            string permission)
        {
            if (context.RolePermissions.Any(x => x.Role!.Name == role && x.Permission!.Name == permission))
            {
                return;
            }
            var roleID = context.Roles.Single(x => x.Name == role).Id;
            var permissionID = context.Permissions.SingleOrDefault(x => x.Name == permission)?.Id;
            if (Contract.CheckInvalidID(permissionID))
            {
                var p = new Permission { Name = permission };
                context.Permissions.Add(p);
                context.SaveUnitOfWork(true);
                permissionID = p.Id;
            }
            context.RolePermissions.Add(new() { RoleId = roleID, PermissionId = permissionID!.Value });
            context.SaveUnitOfWork(true);
        }

        private void AddPermissions()
        {
            // Permissions for the Admin (applicable to Administrators only)
            foreach (var adminPerm in AdminPerms)
            {
                Permissions.Add(adminPerm);
            }
            // Permissions for the Store Admins (not able to access full Admin, but a limited Store admin)
            foreach (var storeAdminPerm in StoreAdminPerms)
            {
                foreach (var perm in EnumExtensions.AsValues<NormalPermissions>())
                {
                    Permissions.Add($"{storeAdminPerm}.{perm}");
                }
            }
            // Permissions for the Brand Admins (not able to access full Admin, but a limited Brand admin)
            foreach (var brandAdminPerm in BrandAdminPerms)
            {
                foreach (var perm in EnumExtensions.AsValues<NormalPermissions>())
                {
                    Permissions.Add($"{brandAdminPerm}.{perm}");
                }
            }
            // Permissions for the Franchise Admins (not able to access full Admin, but a limited Franchise admin)
            foreach (var franchiseAdminPerm in FranchiseAdminPerms)
            {
                foreach (var perm in EnumExtensions.AsValues<NormalPermissions>())
                {
                    Permissions.Add($"{franchiseAdminPerm}.{perm}");
                }
            }
            // Permissions for the Storefront (more applicable to end users)
            foreach (var storefrontPerm in StorefrontPerms)
            {
                Permissions.Add(storefrontPerm);
            }
            foreach (var storeAdminPerm in StoreAdminPerms)
            {
                Permissions.Add(storeAdminPerm);
            }
            foreach (var brandAdminPerm in BrandAdminPerms)
            {
                Permissions.Add(brandAdminPerm);
            }
            foreach (var franchiseAdminPerm in FranchiseAdminPerms)
            {
                Permissions.Add(franchiseAdminPerm);
            }
            foreach (var vendorAdminPerm in VendorAdminPerms)
            {
                Permissions.Add(vendorAdminPerm);
            }
            // Custom Permissions
            Permissions.Add("Quoting.SalesQuote.AwardLineItem");
            Permissions.Add("Quoting.SalesQuote.InProcess");
            Permissions.Add("Quoting.SalesQuote.Processed");
            Permissions.Add("Quoting.SalesQuote.Approve");
            Permissions.Add("Quoting.SalesQuote.Reject");
            Permissions.Add("Quoting.SalesQuote.Void");
            Permissions.Add("Ordering.SalesOrder.Confirm");
            Permissions.Add("Ordering.SalesOrder.Pending");
            Permissions.Add("Ordering.SalesOrder.Hold");
            Permissions.Add("Ordering.SalesOrder.Backorder");
            Permissions.Add("Ordering.SalesOrder.Split");
            Permissions.Add("Ordering.SalesOrder.CreateInvoice");
            Permissions.Add("Ordering.SalesOrder.AddPayment");
            Permissions.Add("Ordering.SalesOrder.CreatePickTicket");
            Permissions.Add("Ordering.SalesOrder.DropShip");
            Permissions.Add("Ordering.SalesOrder.Ship");
            Permissions.Add("Ordering.SalesOrder.ReadyForPickup");
            Permissions.Add("Ordering.SalesOrder.Complete");
            Permissions.Add("Ordering.SalesOrder.Void");
            Permissions.Add("Invoicing.SalesInvoice.Void");
            Permissions.Add("Returning.SalesReturn.Confirm");
            Permissions.Add("Returning.SalesReturn.Validate");
            Permissions.Add("Returning.SalesReturn.Reject");
            Permissions.Add("Returning.SalesReturn.Ship");
            Permissions.Add("Returning.SalesReturn.AddPayment");
            Permissions.Add("Returning.SalesReturn.Complete");
            Permissions.Add("Returning.SalesReturn.Void");
            Permissions.Add("Returning.SalesReturn.Cancel");
            Permissions.Add("Returning.SalesReturn.Refund");
            Permissions.Add("Returning.SalesReturn.ManualRefund");
            Permissions.Add("Products.Product.Export");
            // Run all the permissions through checks to make sure they exist
            foreach (var permission in Permissions.Distinct())
            {
                CreateAndAddPermissionEntityIfDoesntExist(permission);
            }
            context.SaveUnitOfWork(true);
        }

        private void CreateAndAddPermissionEntityIfDoesntExist(string name)
        {
            if (context.Permissions.Any(x => x.Name == name))
            {
                return;
            }
            context.Permissions.Add(new() { Name = name });
        }

        private async Task AddIdentityUserDataAsync()
        {
            var userStore = new CEFUserStore(context);
            var roleStore = new CEFRoleStore(context);
            // Create the Managers
            var userManager = new CEFUserManager(userStore);
            var roleManager = new CEFRoleManager(roleStore);
            foreach (var role in AllRoles)
            {
                var roleResult = CreateRoleIfDoesntExist(roleManager, role);
                if (!roleResult)
                {
                    throw new($"{role} creation failed failed");
                }
            }
            // Create the Set of Permissions to Assign to Roles
            AddPermissions();
            // Assign all permissions to the AdminRole
            foreach (var permission in Permissions)
            {
                AssignPermissionToRoleIfNotAssigned(context, AdminRole, permission);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Assign all permissions to the ClarityConnectDataRole
            foreach (var permission in Permissions)
            {
                AssignPermissionToRoleIfNotAssigned(context, ClarityConnectDataRole, permission);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Assign all permissions to the StoreAdminRole
            foreach (var permission in Permissions.Where(x => x.StartsWith("StoreAdmin.") || x.StartsWith("Storefront.StoreDashboard")))
            {
                AssignPermissionToRoleIfNotAssigned(context, StoreAdminRole, permission);
            }
            // Assign all permissions to the BrandAdminRole
            foreach (var permission in Permissions.Where(x => x.StartsWith("BrandAdmin.") || x.StartsWith("Storefront.BrandDashboard")))
            {
                AssignPermissionToRoleIfNotAssigned(context, BrandAdminRole, permission);
            }
            // Assign all permissions to the FranchiseAdminRole
            foreach (var permission in Permissions.Where(x => x.StartsWith("FranchiseAdmin.") || x.StartsWith("Storefront.FranchiseDashboard")))
            {
                AssignPermissionToRoleIfNotAssigned(context, FranchiseAdminRole, permission);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Assign all permissions to the VendorAdminRole
            foreach (var permission in Permissions.Where(x => x.StartsWith("VendorAdmin.")))
            {
                AssignPermissionToRoleIfNotAssigned(context, VendorAdminRole, permission);
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Assign all permissions to the UserRole
            foreach (var permission in Permissions.Where(x => x.StartsWith("Storefront.")))
            {
                AssignPermissionToRoleIfNotAssigned(context, UserRole, permission);
            }
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Reviews.Review.Create");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.Cart.Create");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.Cart.Update");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.Cart.Delete");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.Cart.View");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.CartType.Create");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.CartType.Update");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.CartType.Delete");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Shopping.CartType.View");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Accounts.AccountContact.View");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Accounts.AccountContact.Create");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Accounts.AccountContact.Update");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Accounts.AccountContact.Deactivate");
            AssignPermissionToRoleIfNotAssigned(context, UserRole, "Accounts.AccountContact.Delete");
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            foreach (var seedUser in SeedUsers)
            {
                var (success, message) = CreateUserIfDoesntExist(
                    userManager,
                    seedUser.UserName,
                    seedUser.Password,
                    seedUser.DisplayName);
                if (!success)
                {
                    throw new($"userResult failed: {seedUser.UserName} : {message}");
                }
                foreach (var role in seedUser.Roles)
                {
                    (success, message) = await userManager.AssignRoleIfNotAssignedAsync(
                            (await userManager.Users.SingleAsync(x => x.UserName == seedUser.UserName).ConfigureAwait(false)).ID,
                            role,
                            null,
                            null)
                        .ConfigureAwait(false);
                    if (!success)
                    {
                        throw new($"assignRoleResult failed: {seedUser.UserName} : {role} : {message}");
                    }
                }
            }
        }
    }
}
