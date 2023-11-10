// <copyright file="BNGService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the BNG service class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    using System;
    using System.IO;
    using System.Net;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A bng service.</summary>
    public class BNGService
    {
        private const string StrErrorResponseCode = "response=0&response_code=420";

        /// <summary>Type of the content.</summary>
        private const string StrContentType = "application/x-www-form-urlencoded";

        /// <summary>The method.</summary>
        private const string StrMethod = "POST";

        /// <summary>Initializes a new instance of the <see cref="BNGService"/> class.</summary>
        /// <param name="url">     URL of the document.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public BNGService(string url, string username, string password)
        {
            Contract.RequiresNotNull(url);
            URL = new(url);
            Username = username;
            Password = password;
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        private Uri URL { get; }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        private string Username { get; }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        private string Password { get; }

        /// <summary>Requests the given request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A BNGGatewayResponse.</returns>
        public IPaymentResponse Request(BNGTransaction request)
        {
            Contract.RequiresNotNull(request);
            request.Username = Username;
            request.Password = Password;
            var content = request.ToRequestString();
            StreamWriter? myWriter = null;
            try
            {
                var objRequest = (HttpWebRequest)WebRequest.Create(URL);
                objRequest.Method = StrMethod;
                objRequest.ContentLength = content.Length;
                objRequest.ContentType = StrContentType;
                myWriter = new(objRequest.GetRequestStream());
                myWriter.Write(content);
                myWriter.Close();
                string result;
                var objResponse = (HttpWebResponse)objRequest.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                return BNGPaymentsProviderExtensions.ToPaymentResponse(result);
            }
            catch (Exception)
            {
                return BNGPaymentsProviderExtensions.ToPaymentResponse(StrErrorResponseCode);
            }
            finally
            {
                myWriter?.Close();
            }
        }
    }
}
