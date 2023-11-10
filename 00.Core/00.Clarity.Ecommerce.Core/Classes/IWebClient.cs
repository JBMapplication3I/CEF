// <copyright file="IWebClient.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWebClient interface</summary>
namespace System.Net
{
    using System;
    using Threading.Tasks;

    /// <summary>Required methods (subset of `System.Net.WebClient` methods).</summary>
    /// <remarks>This interface is used for mocking.</remarks>
    public interface IWebClient : IDisposable
    {
        /// <summary>Gets or sets the base address.</summary>
        /// <value>The base address.</value>
        string BaseAddress { get; set; }

        /// <summary>Gets or sets the headers.</summary>
        /// <value>The headers.</value>
        WebHeaderCollection Headers { get; set; }

        /// <summary>Downloads the data described by address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A byte[].</returns>
        byte[] DownloadData(Uri address);

        /// <summary>Uploads a data.</summary>
        /// <param name="address">The address.</param>
        /// <param name="data">   The data.</param>
        /// <returns>A byte[].</returns>
        byte[] UploadData(Uri address, byte[] data);

        /// <summary>Downloads the string described by address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A string.</returns>
        string DownloadString(Uri address);

        /// <summary>Downloads the string described by address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A string.</returns>
        string DownloadString(string address);

        /// <summary>Downloads the string task described by address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A Task{string}.</returns>
        Task<string> DownloadStringTaskAsync(string address);

        /// <summary>Downloads the string task described by address.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A Task{string}.</returns>
        Task<string> DownloadStringTaskAsync(Uri address);

        /// <summary>Uploads a string.</summary>
        /// <param name="address">The address.</param>
        /// <param name="content">The content.</param>
        /// <returns>A string.</returns>
        string UploadString(Uri address, string content);

        /// <summary>Uploads a string.</summary>
        /// <param name="address">The address.</param>
        /// <param name="content">The content.</param>
        /// <returns>A string.</returns>
        string UploadString(string address, string content);

        /// <summary>Uploads a string.</summary>
        /// <param name="address">The address.</param>
        /// <param name="content">The content.</param>
        /// <returns>A Task{string}.</returns>
        Task<string> UploadStringTaskAsync(string address, string content);

        /// <summary>Uploads a string.</summary>
        /// <param name="address">The address.</param>
        /// <param name="content">The content.</param>
        /// <returns>A Task{string}.</returns>
        Task<string> UploadStringTaskAsync(Uri address, string content);
    }
}
