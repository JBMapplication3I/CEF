// <copyright file="CEFConfigTesting.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration testing class</summary>
namespace Clarity.Ecommerce.Services.Testing
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using JSConfigs;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap;
    using StructureMap.Pipeline;
#endif
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Services.CEFConfigTesting")]
    public class CEFConfigTesting : XUnitLogHelper
    {
#pragma warning disable SA1648 // inheritdoc should be used with inheriting class
        /// <inheritdoc/>
        public CEFConfigTesting(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
#pragma warning restore SA1648 // inheritdoc should be used with inheriting class

        [Fact]
        public async Task Verify_CEFConfigDictionary_LoadAsync_Works()
        {
            using var childContainer = RegistryLoader.RootContainer.CreateChildContainer();
            var contextProfileName = await SetupContainerAsync(new MockingSetup { DoEventLogTable = true }, childContainer).ConfigureAwait(false);
            CEFConfigDictionary.Load();
        }

        [Fact]
        public async Task Verify_CEFConfigDictionary_GetStoreFrontCEFConfig_Works()
        {
            using var childContainer = RegistryLoader.RootContainer.CreateChildContainer();
            // Arrange
            var contextProfileName = await SetupContainerAsync(new MockingSetup { DoEventLogTable = true }, childContainer).ConfigureAwait(false);
            CEFConfigDictionary.Load();
            // Act
            var response = CEFConfigDictionary.GetStoreFrontCEFConfig(null);
            // Assert
            Assert.True(
                response.ActionSucceeded,
                response.Messages.DefaultIfEmpty("An unknown error occurred").Aggregate((c, n) => $"{c}\r\n{n}"));
            Assert.NotNull(response.Result);
            Assert.NotEqual(string.Empty, response.Result);
            this.TestOutputHelper.WriteLine(response.Result);
        }

        protected async Task<string> SetupContainerAsync(
            MockingSetup mockingSetup,
            IContainer childContainer,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "")
        {
            var contextProfileName = $"{sourceFilePath}|{memberName}";
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
            ////var mockRoleManager = new Mock<ICEFRoleManager>();
            ////mockRoleManager.Setup(m => m.Roles).Returns(() => mockingSetup.MockContext.Object.Roles);
            ////var mockUserStore = new Mock<ICEFUserStore>();
            ////var mockUserManager = new Mock<ICEFUserManager>().SetupAllProperties();
            ////mockUserManager.Setup(m => m.GetRoles(It.IsAny<int>())).Returns(
            ////    (int id) => mockingSetup.MockContext.Object.RoleUsers
            ////        .Where(q => q.UserId == id)
            ////        .Select(q => q.Role.Name)
            ////        .ToList());
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                ////x.For<ICEFRoleManager>().Use(() => mockRoleManager.Object);
                ////x.For<ICEFUserManager>().Use(() => mockUserManager.Object);
                ////x.For<ICEFUserStore>().Use(() => mockUserStore.Object);
                ////x.For<IWebClientFactory>().Use<MockWebClientFactory>();
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
            return contextProfileName;
        }
    }
}
