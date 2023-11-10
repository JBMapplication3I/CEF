// // <copyright file="IHaveMatchCodes.cs" company="clarity-ventures.com">
// // Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// // </copyright>
// // <summary>Implements the %class description% class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IHaveMatchCodes
    {
        /// <summary>Gets match code.</summary>
        /// <remarks>Match Code is similar to Hash Code except we control the properties to check for matching a Model
        /// with it's Entity.</remarks>
        /// <param name="includeID">(Optional) True to include, false to exclude the identifier.</param>
        /// <returns>The match code.</returns>
        ulong GetMatchCode(bool includeID = false);
    }
}
