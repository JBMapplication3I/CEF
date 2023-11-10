// <copyright file="CEFCacheProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF cache provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Cache.Testing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using JSConfigs;
    using Models;
    using StackExchange.Redis;
    using Xunit;

    [Trait("Category", "Providers.Caching")]
    public class CEFCacheProviderTests
    {
        [Fact]
        public async Task Verify_CEFCache_Async()
        {
            CEFConfigDictionary.Load();
            const string Key = "Verify_CEFCache";
            const string Value = "TestValue";
            // Skip Redis Test if not Enabled
            var cache = (await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false))!;
            // Confirm Key Initially Empty
            // NOTE: This test fails a lot because the build server redis runs act like they are too busy. Adding a
            // retry helper isntance to get around this so it doesn't have to repeat the tests as often
            await RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(() => cache.RemoveAsync(Key, false)).ConfigureAwait(false);
            var cachedValue = await cache.GetAsync<string>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
            // Confirm Key Value Added
            await cache.AddAsync(Key, Value, false).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<string>(Key, false).ConfigureAwait(false);
            Assert.Equal(Value, cachedValue);
            // Confirm Key Removed
            await cache.RemoveAsync(Key, false).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<string>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
            // Confirm Key Value Added and Expires
            await cache.AddAsync(Key, Value, false, timeToLive: TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<string>(Key, false).ConfigureAwait(false);
            Assert.Equal(Value, cachedValue);
            Thread.Sleep(4000);
            cachedValue = await cache.GetAsync<string>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
        }

        [Fact]
        public async Task Verify_CEFCache_RemoveByPatternAsync()
        {
            var keys = new[]
            {
                "1XXXX|P:123|XXXX",
                "2XXXX|P:122|XXXX",
                "3XXXX|P:123|XXXX",
                "4XXXX|P:122|XXXX",
                "5XXXX|P:122|XXXX",
            };
            const string Value = "TestValue";
            const string Pattern = "*122*";
            var cache = (await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false))!;
            string cachedValue;
            // Confirm Key Initially Empty
            foreach (var key in keys)
            {
                // NOTE: This test fails a lot because the build server redis runs act like they are too busy. Adding a
                // retry helper isntance to get around this so it doesn't have to repeat the tests as often
                await RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(() => cache.RemoveAsync(key, false)).ConfigureAwait(false);
                cachedValue = await cache.GetAsync<string>(key, false).ConfigureAwait(false);
                Assert.Null(cachedValue);
            }
            // Confirm Key Value Added
            foreach (var key in keys)
            {
                await cache.AddAsync(key, Value, false).ConfigureAwait(false);
                cachedValue = await cache.GetAsync<string>(key, false).ConfigureAwait(false);
                Assert.Equal(Value, cachedValue);
            }
            // Remove Keys by Pattern
            await cache.RemoveByPatternAsync(Pattern, false).ConfigureAwait(false);
            // Confirm P:122 Keys Removed
            foreach (var key in keys)
            {
                cachedValue = await cache.GetAsync<string>(key, false).ConfigureAwait(false);
                if (key.Contains("P:122"))
                {
                    Assert.Null(cachedValue);
                }
                else
                {
                    Assert.NotNull(cachedValue);
                }
            }
            // Remove Keys
            foreach (var key in keys)
            {
                await cache.RemoveAsync(key, false).ConfigureAwait(false);
            }
        }

        [Fact]
        public async Task Verify_CEFCache_ObjectsAsync()
        {
            const string Key = "Verify_CEFCache_Object";
            var value = new UserModel
            {
                ID = 1,
                CustomKey = "User1",
                DisplayName = "Test User",
                Account = new AccountModel
                {
                    ID = 1,
                    CustomKey = "Acct1"
                },
            };
            var cache = (await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false))!;
            // Confirm Key Initially Empty
            // NOTE: This test fails a lot because the build server redis runs act like they are too busy. Adding a
            // retry helper isntance to get around this so it doesn't have to repeat the tests as often
            await RetryHelper.RetryOnExceptionAsync<RedisConnectionException>(() => cache.RemoveAsync(Key, false)).ConfigureAwait(false);
            var cachedValue = await cache.GetAsync<UserModel>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
            // Confirm Key Value Added
            await cache.AddAsync(Key, value, false).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<UserModel>(Key, false).ConfigureAwait(false);
            Assert.True(QuickUserModelCompare(value, cachedValue));
            // Confirm Key Removed
            await cache.RemoveAsync(Key, false).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<UserModel>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
            // Confirm Key Value Added and Expires
            await cache.AddAsync(Key, value, false, TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            cachedValue = await cache.GetAsync<UserModel>(Key, false).ConfigureAwait(false);
            Assert.True(QuickUserModelCompare(value, cachedValue));
            Thread.Sleep(6_000);
            cachedValue = await cache.GetAsync<UserModel>(Key, false).ConfigureAwait(false);
            Assert.Null(cachedValue);
        }

        private static bool QuickUserModelCompare(UserModel user1, UserModel user2)
        {
            return user1 != null
                && user2 != null
                && user1.ID == user2.ID
                && user1.CustomKey == user2.CustomKey
                && user1.DisplayName == user2.DisplayName
                && user1.Account?.ID == user2.Account?.ID
                && user1.Account?.CustomKey == user2.Account?.CustomKey;
        }
    }
}
