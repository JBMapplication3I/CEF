// <copyright file="WeChatChattingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the we chat chatting provider tests class</summary>
namespace Clarity.Ecommerce.Testing.Providers.Chatting.WeChat
{
    using System.Linq;
    using Ecommerce.Providers.Chatting.WeChatInt;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Chatting.WeChat")]
    public class WeChatChattingProviderTests : XUnitLogHelper
    {
        /// <summary>Initializes a new instance of the <see cref="XUnitLogHelper" /> class.</summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        public WeChatChattingProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetToken_CompletesWithoutError()
        {
            var provider = new WeChatChattingProvider();
            var result = provider.GetToken();
            Assert.True(result.ActionSucceeded, result.Messages?.Aggregate((c, n) => c + "\r\n" + n));
            Assert.NotNull(result.Result);
            Assert.NotEmpty(result.Result);
            TestOutputHelper.WriteLine(result.Result);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_PostMessage_CompletesWithoutError()
        {
            var provider = new WeChatChattingProvider();
            var result = provider.PostMessage("2", "1", "test");
            Assert.True(result.ActionSucceeded, result.Messages?.Aggregate((c, n) => c + "\r\n" + n));
        }
    }
}
