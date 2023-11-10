// <copyright file="JsonSerializer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements helper methods for use with JsonSerializer</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using Models;
    using Newtonsoft.Json;

    /// <summary>A JSON serializer.</summary>
    public static class JsonSerializer
    {
        /// <summary>Gets serialized string.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>The serialized string.</returns>
        public static string GetSerializedString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>Deserialize response.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="tempResponse">The temporary response.</param>
        /// <returns>A T.</returns>
        public static T DeserializeResponse<T>(PayTraceResponse tempResponse)
        {
            var returnObject = default(T);
            if (tempResponse.JsonResponse != null)
            {
                // Parse JSON data into C# obj
                returnObject = JsonConvert.DeserializeObject<T>(tempResponse.JsonResponse);
            }
            return returnObject!;
        }

        /// <summary>Assign error.</summary>
        /// <param name="tempResponse"> The temporary response.</param>
        /// <param name="basicResponse">The basic response.</param>
        public static void AssignError(PayTraceResponse tempResponse, PayTraceBasicResponse basicResponse)
        {
            basicResponse.HttpErrorMessage = tempResponse.ErrorMessage;
        }
    }
}
