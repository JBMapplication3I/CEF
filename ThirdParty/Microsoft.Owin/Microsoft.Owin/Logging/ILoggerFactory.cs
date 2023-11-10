// <copyright file="ILoggerFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILoggerFactory interface</summary>
namespace Microsoft.Owin.Logging
{
    /// <summary>Used to create logger instances of the given name.</summary>
    public interface ILoggerFactory
    {
        /// <summary>Creates a new ILogger instance of the given name.</summary>
        /// <param name="name">.</param>
        /// <returns>An ILogger.</returns>
        ILogger Create(string name);
    }
}
