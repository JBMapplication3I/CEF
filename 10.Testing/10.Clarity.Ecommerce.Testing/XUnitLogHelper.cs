// <copyright file="XUnitLogHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the unit log helper class</summary>
namespace Clarity.Ecommerce.Testing
{
    using Xunit.Abstractions;

    /// <summary>A unit log helper.</summary>
    public abstract class XUnitLogHelper
    {
        /// <summary>The test output helper.</summary>
        protected readonly ITestOutputHelper TestOutputHelper;

        /// <summary>Initializes a new instance of the <see cref="XUnitLogHelper" /> class.</summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        protected XUnitLogHelper(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }
    }
}
