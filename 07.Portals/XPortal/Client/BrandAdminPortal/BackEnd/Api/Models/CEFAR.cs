// <copyright file="CEFAR.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEFActionResponse class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>A CEF Action Response extensions class.</summary>
    public static class CEFAR
    {
        /// <summary>A T extension method that wrap in CEFActionResponse{T}.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> WrapInPassingCEFAR<T>(
            this T? result,
            params string[] messages)
        {
            return new(result, true, messages);
        }

        /// <summary>A T extension method that wrap in passing CEFActionResponse if not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">                The result to act on.</param>
        /// <param name="additionalFailMessages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> WrapInPassingCEFARIfNotNull<T>(
            this T? result,
            params string[] additionalFailMessages)
        {
            return result is null
                ? FailingCEFAR<T>(new List<string>(additionalFailMessages) { "The result was null" }.ToArray())
                : WrapInPassingCEFAR(result);
        }

        /// <summary>A T extension method that wrap in passing CEFActionResponse if not null or empty.</summary>
        /// <typeparam name="T"> Generic type parameter.</typeparam>
        /// <typeparam name="T2">Generic type parameter of the inner type.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> WrapInPassingCEFARIfNotNullOrEmpty<T, T2>(
                this T? result,
                params string[] messages)
            where T : IEnumerable<T2>
        {
            return result is null
                ? FailingCEFAR<T>("The result was null")
                : !result.Any()
                    ? FailingCEFAR<T>("The result was an empty collection")
                    : new(result, true, messages);
        }

        /// <summary>A T extension method that wrap in failing CEFActionResponse.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> WrapInFailingCEFAR<T>(
            this T? result,
            params string[] messages)
        {
            return new(result, false, messages);
        }

        /// <summary>Passing CEFActionResponse.</summary>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse.</returns>
        public static CEFActionResponse PassingCEFAR(params string[] messages)
        {
            return new(true, messages);
        }

        /// <summary>Passing CEFActionResponse.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">  The result.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{TResult}.</returns>
        public static CEFActionResponse<TResult> PassingCEFAR<TResult>(
            TResult? result,
            params string[] messages)
        {
            return new(result, true, messages);
        }

        /// <summary>Failing CEFActionResponse.</summary>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse.</returns>
        public static CEFActionResponse FailingCEFAR(params string[] messages)
        {
            return new(false, messages);
        }

        /// <summary>Failing CEFActionResponse.</summary>
        /// <param name="message"> The message.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse.</returns>
        public static CEFActionResponse FailingCEFAR(string message, params string[] messages)
        {
            var altMessages = new List<string>(messages);
            altMessages.Insert(0, message);
            return new(false, altMessages.ToArray());
        }

        /// <summary>Failing CEFActionResponse.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> FailingCEFAR<T>(params string[] messages)
        {
            return new(false, messages);
        }

        /// <summary>A CEFActionResponse{TSource} extension method that change CEFActionResponse type.</summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A CEFActionResponse{TResult}.</returns>
        public static CEFActionResponse<TResult?> ChangeCEFARType<TSource, TResult>(
                this CEFActionResponse<TSource?> result)
            where TResult : TSource
        {
            return new(
                (TResult?)result.Result,
                result.ActionSucceeded,
                result.Messages.ToArray());
        }

        /// <summary>A CEFActionResponse{List{TSource}} extension method that change CEFActionResponse list type.</summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A list of.</returns>
        public static CEFActionResponse<List<TResult?>> ChangeCEFARListType<TSource, TResult>(
                this CEFActionResponse<List<TSource?>> result)
            where TResult : TSource
        {
            return new(
                result.Result?.Cast<TResult?>().ToList(),
                result.ActionSucceeded,
                result.Messages.ToArray());
        }

        /// <summary>A CEFActionResponse extension method that change failing CEFActionResponse type.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A CEFActionResponse{T}.</returns>
        public static CEFActionResponse<T> ChangeFailingCEFARType<T>(this CEFActionResponse result)
        {
            return new(false, result.Messages.ToArray());
        }

        /// <summary>A bool extension method that converts this CEFAR to a CEFActionResponse.</summary>
        /// <param name="result">      The result to act on.</param>
        /// <param name="failMessages">A variable-length parameters list containing fail messages.</param>
        /// <returns>The given data converted to a CEFActionResponse.</returns>
        public static CEFActionResponse BoolToCEFAR(this bool result, params string[] failMessages)
        {
            return result
                ? PassingCEFAR()
                : FailingCEFAR(failMessages);
        }

        /// <summary>A bool extension method that converts this CEFAR to a CEFActionResponse.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">      The result to act on.</param>
        /// <param name="r">           The TResult to process.</param>
        /// <param name="failMessages">A variable-length parameters list containing fail messages.</param>
        /// <returns>The given data converted to a CEFActionResponse{TResult}.</returns>
        public static CEFActionResponse<TResult> BoolToCEFAR<TResult>(
            this bool result,
            TResult r,
            params string[] failMessages)
        {
            return result
                ? PassingCEFAR(r)
                : FailingCEFAR<TResult>(failMessages);
        }

        /// <summary>Aggregates several together.</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A CEFActionResponse.</returns>
        public static CEFActionResponse Aggregate<TInnerType>(
            IEnumerable<TInnerType> objects,
            Func<TInnerType, CEFActionResponse> func)
        {
            var response = new CEFActionResponse<List<TInnerType>>(true);
            foreach (var @object in objects)
            {
                var result = func(@object);
                response.ActionSucceeded &= result.ActionSucceeded;
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregate several together.</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        public static async Task<CEFActionResponse> AggregateAsync<TInnerType>(
            IEnumerable<TInnerType> objects,
            Func<TInnerType, Task<CEFActionResponse>> func)
        {
            var response = new CEFActionResponse<List<TInnerType>>(true);
            foreach (var @object in objects)
            {
                var result = await func(@object).ConfigureAwait(false);
                response.ActionSucceeded &= result.ActionSucceeded;
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregates several together.</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A list of.</returns>
        public static CEFActionResponse<List<TInnerType?>> Aggregate<TInnerType>(
            IEnumerable<TInnerType?> objects,
            Func<TInnerType?, CEFActionResponse<TInnerType?>> func)
        {
            var response = new CEFActionResponse<List<TInnerType?>>(true)
            {
                Result = new(),
            };
            foreach (var @object in objects)
            {
                var result = func(@object);
                response.ActionSucceeded &= result.ActionSucceeded;
                response.Result.Add(result.Result);
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregates several together.</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A list of.</returns>
        public static async Task<CEFActionResponse<List<TInnerType?>>> AggregateAsync<TInnerType>(
            IEnumerable<TInnerType?> objects,
            Func<TInnerType?, Task<CEFActionResponse<TInnerType?>>> func)
        {
            var response = new CEFActionResponse<List<TInnerType?>>(true)
            {
                Result = new(),
            };
            foreach (var @object in objects)
            {
                var result = await func(@object).ConfigureAwait(false);
                response.ActionSucceeded &= result.ActionSucceeded;
                response.Result.Add(result.Result);
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregates several together.</summary>
        /// <typeparam name="TInnerType"> Type of the inner type.</typeparam>
        /// <typeparam name="TResultType">Type of the result type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A list of.</returns>
        public static CEFActionResponse<List<TResultType?>> Aggregate<TInnerType, TResultType>(
            IEnumerable<TInnerType> objects,
            Func<TInnerType, CEFActionResponse<TResultType?>> func)
        {
            var response = new CEFActionResponse<List<TResultType?>>(true)
            {
                Result = new(),
            };
            foreach (var @object in objects)
            {
                var result = func(@object);
                response.ActionSucceeded &= result.ActionSucceeded;
                response.Result.Add(result.Result);
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregate several together.</summary>
        /// <typeparam name="TInnerType"> Type of the inner type.</typeparam>
        /// <typeparam name="TResultType">Type of the result type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A list of.</returns>
        public static async Task<CEFActionResponse<List<TResultType?>>> AggregateAsync<TInnerType, TResultType>(
            IEnumerable<TInnerType> objects,
            Func<TInnerType, Task<CEFActionResponse<TResultType?>>> func)
        {
            var response = new CEFActionResponse<List<TResultType?>>(true)
            {
                Result = new(),
            };
            foreach (var @object in objects)
            {
                var result = await func(@object).ConfigureAwait(false);
                response.ActionSucceeded &= result.ActionSucceeded;
                response.Result.Add(result.Result);
                if (result.Messages.Count > 0)
                {
                    response.Messages.AddRange(result.Messages);
                }
            }
            return response;
        }

        /// <summary>Aggregates the given responses.</summary>
        /// <param name="responses">A variable-length parameters list containing responses.</param>
        /// <returns>A CEFActionResponse.</returns>
        public static CEFActionResponse Aggregate(params CEFActionResponse[] responses)
        {
            var retVal = PassingCEFAR();
            foreach (var r in responses)
            {
                retVal.ActionSucceeded &= r.ActionSucceeded;
                if (r.Messages.Any())
                {
                    retVal.Messages.AddRange(r.Messages);
                }
            }
            return retVal;
        }
    }
}
