// <copyright file="DatabaseErrorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the database error tests class</summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace Clarity.Ecommerce.DataModel.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Moq;
    using StructureMap;
    using StructureMap.Pipeline;
    using Utilities;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "DataModel.DatabaseErrors")]
    public class DatabaseErrorTests : XUnitLogHelper
    {
        public DatabaseErrorTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_InvalidOperationException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some invalid operation exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_InvalidOperationException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChanges()).Throws(new InvalidOperationException(Message));
            var exception = Assert.Throws<InvalidOperationException>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal(Message, exception.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_DbEntityValidationException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some db entity validation exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            // using var db = new ClarityEcommerceEntities();
            // db.Database.Connection.ConnectionString = "Server=(localdb)\\;";
            // var entity = new Product();
            // db.Set<Product>().Add(entity);
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_DbEntityValidationException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChanges()).Throws(new DbEntityValidationException(
                Message,
                new List<DbEntityValidationResult>
                {
                    // new(db.ChangeTracker.Entries<Product>().First(), new List<DbValidationError> { new("ID", "Not a valid ID") }),
                }));
            var exception = Assert.Throws<DbEntityValidationException>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal("None", exception.Message);
            // Assert.False(true, "Need to add EntityValidationErrors which have 1+ ValidationErrors which are PropertyName+ErrorMessage");
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_DbUpdateException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some db update operation exception";
            const string MessageInner = "Some validation failure";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_DbUpdateException_TheCatch_ReadsTheCorrectDataAsync));
            var exi = new UpdateException(
                MessageInner,
                null,
                new List<ObjectStateEntry>
                {
                    //((IObjectContextAdapter)mockContext.Object).ObjectContext.ObjectStateManager.GetObjectStateEntry(new Product()),
                });
            var ex = new DbUpdateException(Message, exi);
            mockContext.Setup(m => m.SaveChanges()).Throws(ex);
            var exception = Assert.Throws<DbUpdateException>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal(Message, exception.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_EntityCommandExecutionException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some entity command execution exception";
            const string MessageInner = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_EntityCommandExecutionException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChanges()).Throws(new EntityCommandExecutionException(Message, new(MessageInner)));
            var exception = Assert.Throws<EntityCommandExecutionException>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal(Message, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(MessageInner, exception.InnerException!.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_EntityException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some entity exception";
            const string MessageInner = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_EntityException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChanges()).Throws(new EntityException(Message, new(MessageInner)));
            var exception = Assert.Throws<EntityException>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal(Message, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(MessageInner, exception.InnerException!.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChanges_Exception_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChanges_Exception_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChanges()).Throws(new Exception(Message));
            var exception = Assert.Throws<Exception>(() => mockContext.Object.SaveUnitOfWork());
            Assert.Equal(Message, exception.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_InvalidOperationException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some invalid operation exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_InvalidOperationException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new InvalidOperationException(Message));
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal(Message, exception.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_DbEntityValidationException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some db entity validation exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            // using var db = new ClarityEcommerceEntities();
            // db.Database.Connection.ConnectionString = "Server=(localdb)\\;";
            // var entity = new Product();
            // db.Set<Product>().Add(entity);
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_DbEntityValidationException_TheCatch_ReadsTheCorrectDataAsync));
            // var mockDbEntityEntry = new Mock<DbEntityEntry<Product>>(null).Object;
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new DbEntityValidationException(
                Message,
                new List<DbEntityValidationResult>
                {
                    // new(mockDbEntityEntry, new List<DbValidationError> { new("ID", "Not a valid ID") }),
                }));
            var exception = await Assert.ThrowsAsync<DbEntityValidationException>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal("None", exception.Message);
            // Assert.False(true, "Need to add EntityValidationErrors which have 1+ ValidationErrors which are PropertyName+ErrorMessage");
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_DbUpdateException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some db update operation exception";
            const string MessageInner = "Some validation failure";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_DbUpdateException_TheCatch_ReadsTheCorrectDataAsync));
            var exi = new UpdateException(
                MessageInner,
                null,
                new List<ObjectStateEntry>
                {
                    //((IObjectContextAdapter)mockContext.Object).ObjectContext.ObjectStateManager.GetObjectStateEntry(new Product()),
                });
            var ex = new DbUpdateException(Message, exi);
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(ex);
            var exception = await Assert.ThrowsAsync<DbUpdateException>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal(Message, exception.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_EntityCommandExecutionException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some entity command execution exception";
            const string MessageInner = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_EntityCommandExecutionException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new EntityCommandExecutionException(Message, new(MessageInner)));
            var exception = await Assert.ThrowsAsync<EntityCommandExecutionException>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal(Message, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(MessageInner, exception.InnerException!.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_EntityException_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some entity exception";
            const string MessageInner = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_EntityException_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new EntityException(Message, new(MessageInner)));
            var exception = await Assert.ThrowsAsync<EntityException>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal(Message, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(MessageInner, exception.InnerException!.Message);
        }

        [Fact]
        public async Task Verify_WhenDbThrowsOnSaveChangesAsync_Exception_TheCatch_ReadsTheCorrectDataAsync()
        {
            const string Message = "Some general exception";
            var mockContext = new Mock<IClarityEcommerceEntities>();
            mockContext.Setup(x => x.ContextProfileName).Returns(nameof(Verify_WhenDbThrowsOnSaveChangesAsync_Exception_TheCatch_ReadsTheCorrectDataAsync));
            mockContext.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new(Message));
            var exception = await Assert.ThrowsAsync<Exception>(() => mockContext.Object.SaveUnitOfWorkAsync()).ConfigureAwait(false);
            Assert.Equal(Message, exception.Message);
        }

        /* This is just to make PoC calls
        [Fact]
        public async Task Verify_SettingACollectionPropertyAndGettingIt_Works_Async()
        {
            // PoC for exercising explicit implementations manually
            var accountImage = new AccountImage();
            var accountToAssign = new Account();
            Assert.Equal(1, ((IAmFilterableByAccount)accountImage).AccountID = 1);
            Assert.Equal(1, ((IAmFilterableByAccount)accountImage).AccountID);
            Assert.Equal(accountToAssign, ((IAmFilterableByAccount)accountImage).Account = accountToAssign);
            Assert.Equal(accountToAssign, ((IAmFilterableByAccount)accountImage).Account);
            // Find explicit implementations on a class dynamically with reflection
            TestOutputHelper.WriteLine("Find explicits:");
            var explicitProperties = typeof(AccountImage)
                .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(prop => new { prop, getAccessor = prop.GetGetMethod(true) })
                .Where(t => t.getAccessor.IsFinal && t.getAccessor.IsPrivate)
                .Select(t => t.prop);
            foreach (var p in explicitProperties)
            {
                var name = p.Name;
                TestOutputHelper.WriteLine(name);
                TestOutputHelper.WriteLine(name[(name.LastIndexOf('.') + 1)..]);
            }
            // Find virtual collections
            TestOutputHelper.WriteLine("Find virtual collections:");
            var collectionProperties = typeof(Account)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(prop => prop.PropertyType.Name == "ICollection`1" && prop.Name != "Permissions");
            foreach (var p in collectionProperties)
            {
                var name = p.Name;
                TestOutputHelper.WriteLine(name);
                TestOutputHelper.WriteLine(p.PropertyType.GetGenericArguments()[0].Name);
            }
        }
        */

        [Fact]
        public async Task Verify_CEFUserManager_Works_Async()
        {
            // Arrange
            var contextProfileName = GenContextProfileName("Verify_Get_ByID_Should_ReturnAModelWithFullMap");
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountTable = true,
                    DoAccountUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                };
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                mockingSetup.Users!.Object.First().Id = 1;
                ////using var userStore = new CEFUserStore(mockingSetup.MockContext.Object);
                var mockUserStore = new Mock<ICEFUserStore>();
                mockUserStore.Setup(m => m.Context).Returns(mockingSetup.MockContext.Object);
                mockUserStore.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
                    .Returns(() => Task.FromResult(mockingSetup.Users!.Object.First()));
                mockUserStore.Setup(m => m.GetRolesAsync(It.IsAny<User>()))
                    .Returns(() => Task.FromResult<IList<string>>(
                        mockingSetup.RoleUsers!.Object.Where(x => x.UserId == 1).Select(x => x.Role!.Name).ToList()));
                using CEFUserManager userManager = new(mockUserStore.Object);
                Assert.NotNull(userManager.CEFStore);
                const int userID = 1;
                const string username = "Jothay";
                const int accountID = 1;
                const string roleName = "CEF Affiliate Admin";
                const string existingRoleName = "CEF Affiliate Admin";
                const string permissionName = "Some Permission";
                const string twoFactorProvider = "Some Two-Factor Provider";
                const string token = "Some Two-Factor Provider Token";
                RoleForUserModel roleForUser = new() { UserId = 1, RoleId = 1, };
                RoleForAccountModel roleForAccount = new() { AccountId = 1, RoleId = 1, };
                // Act/Assert
                var resultN1 = await userManager.FindByIdAsync(userID).ConfigureAwait(false);
                Assert.NotNull(resultN1);
                // Role Management
                {
                    var result16a = await userManager.AddRoleToUserAsync(userID, roleName, startDate: null, endDate: null).ConfigureAwait(false);
                    Assert.NotNull(result16a);
                    var result17a = await userManager.AddRoleToAccountAsync(accountID, roleName, startDate: null, endDate: null).ConfigureAwait(false);
                    Assert.NotNull(result17a);
                    var result16b = await userManager.AddRoleToUserAsync(userID, existingRoleName, startDate: null, endDate: null).ConfigureAwait(false);
                    Assert.NotNull(result16b);
                    var result17b = await userManager.AddRoleToAccountAsync(accountID, existingRoleName, startDate: null, endDate: null).ConfigureAwait(false);
                    Assert.NotNull(result17b);
                    var result18 = await userManager.UserHasRoleAsync(userID, roleName).ConfigureAwait(false);
                    Assert.True(result18);
                    var result19 = await userManager.AccountHasRoleAsync(accountID, roleName).ConfigureAwait(false);
                    Assert.True(result19);
                    var result14 = await userManager.UpdateRoleAsync(roleForUser).ConfigureAwait(false);
                    Assert.NotNull(result14);
                    var result15 = await userManager.UpdateRoleAsync(roleForAccount).ConfigureAwait(false);
                    Assert.NotNull(result15);
                    var result04 = await userManager.GetRoleNamesForUserAsync(userID).ConfigureAwait(false);
                    Assert.NotNull(result04);
                    var result05 = await userManager.GetRoleNamesForAccountAsync(accountID).ConfigureAwait(false);
                    Assert.NotNull(result05);
                    var result06 = await userManager.GetRolesForUserAsync(userID).ConfigureAwait(false);
                    Assert.NotNull(result06);
                    var result07 = await userManager.GetRolesForAccountAsync(accountID).ConfigureAwait(false);
                    Assert.NotNull(result07);
                    var result01a = await userManager.GetUserRolesAsync(userID).ConfigureAwait(false);
                    Assert.NotNull(result01a);
                    var result01b = await userManager.GetAccountRolesAsync(accountID).ConfigureAwait(false);
                    Assert.NotNull(result01b);
                    var result08 = await userManager.GetPermissionNamesForUserAsync(userID).ConfigureAwait(false);
                    Assert.NotNull(result08);
                    var result09 = await userManager.GetPermissionNamesForAccountAsync(accountID).ConfigureAwait(false);
                    Assert.NotNull(result09);
                    var result10 = await userManager.UserHasPermissionAsync(userID, permissionName).ConfigureAwait(false);
                    Assert.False(result10);
                    var result11 = await userManager.AccountHasPermissionAsync(accountID, permissionName).ConfigureAwait(false);
                    Assert.False(result11);
                    var result20 = await userManager.RemoveRoleFromUserAsync(userID, roleName).ConfigureAwait(false);
                    Assert.NotNull(result20);
                    var result21 = await userManager.RemoveRoleFromAccountAsync(accountID, roleName).ConfigureAwait(false);
                    Assert.NotNull(result21);
                }
                // Two-Factor Auth
                if (false)
#pragma warning disable CS0162
                {
                    var result02 = await userManager.GetTwoFactorEnabledAsync(userID).ConfigureAwait(false);
                    Assert.True(result02);
                    var result03 = await userManager.GetTwoFactorEnabledAsync(username).ConfigureAwait(false);
                    Assert.True(result03);
                    var result12 = await userManager.NotifyTwoFactorTokenAsync(userID, twoFactorProvider, token).ConfigureAwait(false);
                    Assert.NotNull(result12);
                    var result13 = await userManager.NotifyTwoFactorTokenAsync(username, usePhone: true).ConfigureAwait(false);
                    Assert.True(result13);
                    var result00 = await userManager.GenOTPAsync(userID, usePhone: true).ConfigureAwait(false);
                    Assert.NotNull(result00);
                }
#pragma warning restore CS0162
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [DebuggerStepThrough]
        private async Task DoSetupAsync(IContainer childContainer, MockingSetup mockingSetup, string contextProfileName)
        {
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            RegistryLoader.RootContainer.Configure(
                x => x.For<ILogger>().UseInstance(
                    new ObjectInstance(new Logger
                    {
                        ExtraLogger = s =>
                        {
                            try
                            {
                                TestOutputHelper.WriteLine(s);
                            }
                            catch
                            {
                                // Do nothing
                            }
                        },
                    })));
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
        }

        [DebuggerStepThrough]
        private string GenContextProfileName(string functionName)
        {
            return new StringBuilder().Append(GetType().Name).Append('|').Append(functionName).ToString();
        }
    }
}
