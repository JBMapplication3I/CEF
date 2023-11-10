// <copyright file="DoMockingSetupForContextRunnerContacts.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner contacts class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerContactsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Contacts
            if (DoAll || DoContacts || DoContactTable)
            {
                var index = 0;
                RawContacts = new()
                {
                    await CreateADummyContactAsync(id: ++index, key: "CONTACT-" + index, addressID: 1, email1: "james.gray@claritymis.com",    fax1: "fax1", firstName: "James",  fullName: "James T. Gray", lastName: "Gray",    middleName: "Thomas", phone1: "8009282960", phone2: "phone2", phone3: "phone3", /*shippingAddressID: null,*/ typeID: 1, website1: "website1").ConfigureAwait(false),
                    await CreateADummyContactAsync(id: ++index, key: "CONTACT-" + index, addressID: 2, email1: "james.desimas@claritymis.com", fax1: "fax1", firstName: "James",  fullName: "James DeSimas", lastName: "DeSimas", middleName: "X",      phone1: "8009282960", phone2: "phone2", phone3: "phone3", /*shippingAddressID: null,*/ typeID: 1, website1: "website1").ConfigureAwait(false),
                    await CreateADummyContactAsync(id: ++index, key: "CONTACT-" + index, addressID: 3, email1: "steven.arnold@claritymis.com", fax1: "fax1", firstName: "Steven", fullName: "Steven Arnold", lastName: "Arnold",  middleName: "A",      phone1: "8009282960", phone2: "phone2", phone3: "phone3", /*shippingAddressID: null,*/ typeID: 1, website1: "website1").ConfigureAwait(false),
                };
                await InitializeMockSetContactsAsync(mockContext, RawContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Contact Images
            if (DoAll || DoContacts || DoContactImageTable)
            {
                var index = 0;
                RawContactImages = new()
                {
                    await CreateADummyContactImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetContactImagesAsync(mockContext, RawContactImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Contact Image Types
            if (DoAll || DoContacts || DoContactImageTypeTable)
            {
                var index = 0;
                RawContactImageTypes = new()
                {
                    await CreateADummyContactImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetContactImageTypesAsync(mockContext, RawContactImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Contact Types
            if (DoAll || DoContacts || DoContactTypeTable)
            {
                var index = 0;
                RawContactTypes = new()
                {
                    await CreateADummyContactTypeAsync(id: ++index, key: "User", name: "User", desc: "desc", sortOrder: 1, displayName: "User").ConfigureAwait(false),
                    await CreateADummyContactTypeAsync(id: ++index, key: "Billing", name: "Billing", desc: "desc", sortOrder: 2, displayName: "Billing").ConfigureAwait(false),
                    await CreateADummyContactTypeAsync(id: ++index, key: "Shipping", name: "Shipping", desc: "desc", sortOrder: 3, displayName: "Shipping").ConfigureAwait(false),
                };
                await InitializeMockSetContactTypesAsync(mockContext, RawContactTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: ProfanityFilter
            if (DoAll || DoContacts || DoProfanityFilterTable)
            {
                var index = 0;
                RawProfanityFilters = new()
                {
                    await CreateADummyProfanityFilterAsync(id: ++index, key: "butt").ConfigureAwait(false),
                };
                await InitializeMockSetProfanityFiltersAsync(mockContext, RawProfanityFilters).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Referral Codes
            if (DoAll || DoContacts || DoReferralCodeTable)
            {
                var index = 0;
                RawReferralCodes = new()
                {
                    await CreateADummyReferralCodeAsync(id: ++index, key: "Active", name: "Active", desc: "desc", code: "ABCDE-1").ConfigureAwait(false),
                };
                await InitializeMockSetReferralCodesAsync(mockContext, RawReferralCodes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Referral Code Statuses
            if (DoAll || DoContacts || DoReferralCodeStatusTable)
            {
                var index = 0;
                RawReferralCodeStatuses = new()
                {
                    await CreateADummyReferralCodeStatusAsync(id: ++index, key: "Active", name: "Active", desc: "desc", sortOrder: 1, displayName: "Active").ConfigureAwait(false),
                    await CreateADummyReferralCodeStatusAsync(id: ++index, key: "Used", name: "Used", desc: "desc", sortOrder: 1, displayName: "Used").ConfigureAwait(false),
                    await CreateADummyReferralCodeStatusAsync(id: ++index, key: "Void", name: "Void", desc: "desc", sortOrder: 1, displayName: "Void").ConfigureAwait(false),
                };
                await InitializeMockSetReferralCodeStatusesAsync(mockContext, RawReferralCodeStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Referral Code Types
            if (DoAll || DoContacts || DoReferralCodeTypeTable)
            {
                var index = 0;
                RawReferralCodeTypes = new()
                {
                    await CreateADummyReferralCodeTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1,  displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetReferralCodeTypesAsync(mockContext, RawReferralCodeTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Users
            if (DoAll || DoContacts || DoUserTable)
            {
                var index = 0;
                RawUsers = new()
                {
                    await CreateADummyUserAsync(id: ++index, key: "jothay", accountID: 1, userName: "jothay", accessFailedCount: 0, email: "jothay@email.com", emailConfirmed: true, phoneNumber: "555-555-0192", phoneNumberConfirmed: true, displayName: "James Gray", useAutoPay: true).ConfigureAwait(false),
                    await CreateADummyUserAsync(id: ++index, key: "zyreal", accountID: 2, contactID: 2, userName: "zyreal", accessFailedCount: 0, email: "zyreal@email.com", emailConfirmed: true, phoneNumber: "555-555-0193", phoneNumberConfirmed: true, displayName: "James Desimas", useAutoPay: true).ConfigureAwait(false),
                    await CreateADummyUserAsync(id: ++index, key: "zandur", accountID: 3, contactID: 3, typeID: 2, userName: "zandur", accessFailedCount: 0, email: "zandur@email.com", emailConfirmed: true, phoneNumber: "555-555-0194", phoneNumberConfirmed: true, displayName: "Ken Beckstead", useAutoPay: true).ConfigureAwait(false),
                };
                await InitializeMockSetUsersAsync(mockContext, RawUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Stored Files
            if (DoAll || DoContacts || DoUserFileTable)
            {
                var index = 0;
                RawUserFiles = new()
                {
                    await CreateADummyUserFileAsync(id: ++index, key: "File-" + index, name: "An File " + index, desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetUserFilesAsync(mockContext, RawUserFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Images
            if (DoAll || DoContacts || DoUserImageTable)
            {
                var index = 0;
                RawUserImages = new()
                {
                    await CreateADummyUserImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetUserImagesAsync(mockContext, RawUserImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Image Types
            if (DoAll || DoContacts || DoUserImageTypeTable)
            {
                var index = 0;
                RawUserImageTypes = new()
                {
                    await CreateADummyUserImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetUserImageTypesAsync(mockContext, RawUserImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Online Statuses
            if (DoAll || DoContacts || DoUserOnlineStatusTable)
            {
                var index = 0;
                RawUserOnlineStatuses = new()
                {
                    await CreateADummyUserOnlineStatusAsync(id: ++index, key: "Registered", name: "Registered", desc: "desc", sortOrder: 1, displayName: "Registered").ConfigureAwait(false),
                };
                await InitializeMockSetUserOnlineStatusesAsync(mockContext, RawUserOnlineStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Product Types
            if (DoAll || DoContacts || DoUserProductTypeTable)
            {
                var index = 0;
                RawUserProductTypes = new()
                {
                    await CreateADummyUserProductTypeAsync(id: ++index, key: "USER-PRODUCT-TYPE-" + index, jsonAttributes: "{}").ConfigureAwait(false),
                };
                await InitializeMockSetUserProductTypesAsync(mockContext, RawUserProductTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Statuses
            if (DoAll || DoContacts || DoUserStatusTable)
            {
                var index = 0;
                RawUserStatuses = new()
                {
                    await CreateADummyUserStatusAsync(id: ++index, key: "Registered", name: "Registered", desc: "desc", sortOrder: 1, displayName: "Registered").ConfigureAwait(false),
                    await CreateADummyUserStatusAsync(id: ++index, key: "Email Sent", name: "Email Sent", desc: "desc", sortOrder: 2, displayName: "Email Sent").ConfigureAwait(false),
                    await CreateADummyUserStatusAsync(id: ++index, key: "Email Verified", name: "Email Verified", desc: "desc", sortOrder: 3, displayName: "Email Verified").ConfigureAwait(false),
                };
                await InitializeMockSetUserStatusesAsync(mockContext, RawUserStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Types
            if (DoAll || DoContacts || DoUserTypeTable)
            {
                var index = 0;
                RawUserTypes = new()
                {
                    await CreateADummyUserTypeAsync(id: ++index, key: "Customer", name: "Customer", desc: "desc", sortOrder: 1,  displayName: "Customer").ConfigureAwait(false),
                    await CreateADummyUserTypeAsync(id: ++index, key: "Partner / Affiliate", name: "Partner / Affiliate", desc: "desc", sortOrder: 2, displayName: "Partner / Affiliate").ConfigureAwait(false),
                    await CreateADummyUserTypeAsync(id: ++index, key: "Administrator", name: "Administrator", desc: "desc", sortOrder: 3, displayName: "Administrator").ConfigureAwait(false),
                    await CreateADummyUserTypeAsync(id: ++index, key: "No Access", name: "No Access", desc: "desc", sortOrder: 4, displayName: "No Access").ConfigureAwait(false),
                };
                await InitializeMockSetUserTypesAsync(mockContext, RawUserTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Logins
            if (DoAll || DoContacts || DoUserLoginTable)
            {
                // var index = 0;
                RawUserLogins = new()
                {
                    await CreateADummyUserLoginAsync(loginProvider: "ASP.NET Identity", providerKey: "ASP.NET Identity", userId: 1).ConfigureAwait(false),
                };
                await InitializeMockSetUserLoginsAsync(mockContext, RawUserLogins).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Roles
            if (DoAll || DoContacts || DoUserRoleTable)
            {
                RawUserRoles = new()
                {
                    await CreateADummyUserRoleAsync(id: 1, name: "CEF Global Admin").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 2, name: "CEF Affiliate Admin").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 3, name: "CEF Local Admin").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 4, name: "CEF Store Admin").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 5, name: "CEF Connect Admin").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 6, name: "CEF User").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 7, name: "Bronze Site Membership").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 8, name: "Gold Site Membership").ConfigureAwait(false),
                    await CreateADummyUserRoleAsync(id: 9, name: "Platinum Site Membership").ConfigureAwait(false),
                };
                await InitializeMockSetUserRolesAsync(mockContext, RawUserRoles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Role Users
            if (DoAll || DoContacts || DoRoleUserTable)
            {
                RawRoleUsers = new()
                {
                    await CreateADummyRoleUserAsync(id: 1, key: "role-user-1", userId: 1, roleId: 1).ConfigureAwait(false),
                    await CreateADummyRoleUserAsync(id: 2, key: "role-user-2", userId: 1, roleId: 7, startDate: CreatedDate.AddYears(-2), endDate: CreatedDate.AddYears(-1)).ConfigureAwait(false),
                    await CreateADummyRoleUserAsync(id: 3, key: "role-user-3", userId: 1, roleId: 7, startDate: CreatedDate.AddMonths(-1), endDate: CreatedDate.AddMonths(11)).ConfigureAwait(false),
                    await CreateADummyRoleUserAsync(id: 4, key: "role-user-4", userId: 1, roleId: 8, startDate: CreatedDate.AddMonths(-1), endDate: CreatedDate.AddMonths(11)).ConfigureAwait(false),
                };
                await InitializeMockSetRoleUsersAsync(mockContext, RawRoleUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Permissions
            if (DoAll || DoContacts || DoPermissionTable)
            {
                RawPermissions = new()
                {
                    await CreateADummyPermissionAsync(id: 1, "some.permission").ConfigureAwait(false),
                };
                await InitializeMockSetPermissionsAsync(mockContext, RawPermissions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: RolePermissions
            if (DoAll || DoContacts || DoRolePermissionTable)
            {
                RawRolePermissions = new()
                {
                    await CreateADummyRolePermissionAsync(roleId: 1, permissionId: 1).ConfigureAwait(false),
                };
                await InitializeMockSetRolePermissionsAsync(mockContext, RawRolePermissions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: UserClaims
            if (DoAll || DoContacts || DoUserClaimTable)
            {
                RawUserClaims = new()
                {
                    // await CreateADummyRolePermissionAsync(roleId: 1, permissionId: 1).ConfigureAwait(false),
                };
                await InitializeMockSetUserClaimsAsync(mockContext, RawUserClaims).ConfigureAwait(false);
            }
            #endregion
        }

        [System.Diagnostics.DebuggerStepThrough]
        public Task AssignSchemaMocksContactsExtrasAsync(Mock<IClarityEcommerceEntities> context, bool dirtyOnly)
        {
            return Task.CompletedTask;
            /*
            if (DoAll || DoContacts || (DoUserRoleTable && (!dirtyOnly || UserRoleDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawUserRoles == null) { throw new InvalidOperationException("Raw UserRoles was null"); }
                    await InitializeMockSetFromListNonIBaseAsync(UserRoles, RawUserRoles).ConfigureAwait(false);
                    context.Setup(m => m.Roles).Returns(() => UserRoles?.Object!);
                    context.Setup(m => m.Set<UserRole>()).Returns(() => UserRoles?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoContacts || (DoRoleUserTable && (!dirtyOnly || RoleUserDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawRoleUsers == null) { throw new InvalidOperationException("Raw RoleUsers was null"); }
                    await InitializeMockSetFromListNonIBaseAsync(RoleUsers, RawRoleUsers).ConfigureAwait(false);
                    context.Setup(m => m.RoleUsers).Returns(() => RoleUsers?.Object!);
                    context.Setup(m => m.Set<RoleUser>()).Returns(() => RoleUsers?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoContacts || (DoUserLoginTable && (!dirtyOnly || UserLoginDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawUserLogins == null) { throw new InvalidOperationException("Raw UserLogins was null"); }
                    await InitializeMockSetFromListNonIBaseAsync(UserLogins, RawUserLogins).ConfigureAwait(false);
                    context.Setup(m => m.UserLogins).Returns(() => UserLogins?.Object!);
                    context.Setup(m => m.Set<UserLogin>()).Returns(() => UserLogins?.Object!);
                })
                .ConfigureAwait(false);
            }
            */
        }

        /*
        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetUserRolesAsync(Mock<IClarityEcommerceEntities> context, List<Mock<UserRole>> data)
        {
            if (!DoAll && !DoContacts && !DoUserRoleTable) { return; }
            UserRoles ??= new Mock<DbSet<UserRole>>();
            await InitializeMockSetFromListNonIBaseAsync(UserRoles, data).ConfigureAwait(false);
            context.Setup(m => m.Roles).Returns(() => UserRoles.Object);
            context.Setup(m => m.Set<UserRole>()).Returns(() => UserRoles.Object);
            TableRecordCounts[typeof(UserRole)] = data.Count;
            if (data.Count > 0)
            {
                TableFirstRecordIDs[typeof(UserRole)] = data[0].Object.Id;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetUserLoginsAsync(Mock<IClarityEcommerceEntities> context, List<Mock<UserLogin>> data)
        {
            if (!DoAll && !DoContacts && !DoUserRoleTable) { return; }
            UserLogins ??= new Mock<DbSet<UserLogin>>();
            await InitializeMockSetFromListNonIBaseAsync(UserLogins, data).ConfigureAwait(false);
            context.Setup(m => m.UserLogins).Returns(() => UserLogins.Object);
            context.Setup(m => m.Set<UserLogin>()).Returns(() => UserLogins.Object);
            TableRecordCounts[typeof(UserLogin)] = data.Count;
        }

        /// <summary>Creates a dummy UserLogin.</summary>
        /// <returns>The new UserLogin.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<UserLogin>> CreateADummyUserLoginAsync(string? loginProvider, string? providerKey, int userId = 1)
        {
            return Task.Run(() =>
            {
                var mock = new Mock<UserLogin>();
                mock.SetupAllProperties();
                // UserLogin Properties
                mock.Object.LoginProvider = loginProvider;
                mock.Object.ProviderKey = providerKey;
                mock.Object.UserId = userId;
                // Dynamic Linking
                // Related Properties
                mock.Setup(m => m.User).Returns(() => RawUsers?.FirstOrDefault(x => x.Object.ID == mock.Object.UserId)?.Object);
                return mock;
            });
        }

        /// <summary>Creates a dummy UserRole.</summary>
        /// <returns>The new UserRole.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<UserRole>> CreateADummyUserRoleAsync(int id, string name)
        {
            return Task.Run(() =>
            {
                var mock = new Mock<UserRole>();
                mock.SetupAllProperties();
                // UserRole Properties
                mock.Object.Id = id;
                mock.Object.Name = name;
                // Dynamic Linking
                // Associated Properties
                mock.Setup(m => m.Permissions).Returns(() => RawRolePermissions?.Where(x => x.Object.RoleId == mock.Object.Id).Select(x => x.Object).ToList() ?? new List<RolePermission>());
                mock.Setup(m => m.Users).Returns(() => RawRoleUsers?.Where(x => x.Object.RoleId == mock.Object.Id).Select(x => x.Object).ToList() ?? new List<RoleUser>());
                return mock;
            });
        }

        /// <summary>Creates a dummy RoleUser.</summary>
        /// <returns>The new RoleUser.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<RoleUser>> CreateADummyRoleUserAsync(int userId, int roleId, int? groupID = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Task.Run(() =>
            {
                var mock = new Mock<RoleUser>();
                mock.SetupAllProperties();
                // RoleUser Properties
                mock.Object.UserId = userId;
                mock.Object.RoleId = roleId;
                mock.Object.GroupID = groupID;
                mock.Object.StartDate = startDate;
                mock.Object.EndDate = endDate;
                // Dynamic Linking
                // Related Properties
                mock.Setup(m => m.Role).Returns(() => RawUserRoles?.FirstOrDefault(x => x.Object.Id == mock.Object.RoleId)?.Object);
                mock.Setup(m => m.Group).Returns(() => RawGroups?.FirstOrDefault(x => x.Object.ID == mock.Object.GroupID)?.Object);
                mock.Setup(m => m.User).Returns(() => RawUsers?.FirstOrDefault(x => x.Object.Id == mock.Object.UserId)?.Object);
                return mock;
            });
        }
        */
    }
}
