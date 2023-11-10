// <copyright file="IEmailSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEmailSettings interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <summary>Interface for email settings.</summary>
    public interface IEmailSettings
    {
        /// <summary>Gets the 'from' email.</summary>
        /// <value>The 'from' email.</value>
        string? From { get; }

        /// <summary>Gets the 'to' email(s).</summary>
        /// <value>The to email(s).</value>
        string? To { get; }

        /// <summary>Gets the 'CC' email(s).</summary>
        /// <value>The 'CC' emails.</value>
        string? CC { get; }

        /// <summary>Gets the 'BCC' email(s).</summary>
        /// <value>The 'BCC' emails.</value>
        string? BCC { get; }

        /// <summary>Gets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; }

        /// <summary>Gets the full pathname of the template file.</summary>
        /// <value>The full pathname of the template file.</value>
        string? FullTemplatePath { get; }

        /// <summary>Gets the full pathname of the return file.</summary>
        /// <value>The full pathname of the return file.</value>
        string? FullReturnPath { get; }

        /// <summary>Gets a value indicating whether this Email is enabled.</summary>
        /// <value>True if enabled, false if not.</value>
        bool Enabled { get; }

        /// <summary>Loads this class' email settings.</summary>
        void Load();

        /// <summary>Queue the email to be sent.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="to">                The 'to' email(s).</param>
        /// <param name="parameters">        [Optional] Options for controlling the operation.</param>
        /// <param name="customReplacements">[Optional] The custom replacements.</param>
        /// <returns>A Task{CEFActionResponse{int}}.</returns>
        Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null);
    }
}
