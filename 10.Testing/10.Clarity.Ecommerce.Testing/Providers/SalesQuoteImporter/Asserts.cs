// <copyright file="Asserts.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the asserts static class which contains custom xUnit asserts for convenience.</summary>
namespace Clarity.Ecommerce
{
    using System.Linq;
    using Models;
    using Xunit;

    public static class Asserts
    {
        public static void Success(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(result.ActionSucceeded, result.Messages.DefaultIfEmpty("").Aggregate((c, n) => c + "\r\n" + n));
        }

        public static void Success<T>(CEFActionResponse<T> result)
        {
            Success((CEFActionResponse)result);
            Assert.True(result.Result != null);
        }
    }
}
