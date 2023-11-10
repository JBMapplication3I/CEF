// <copyright file="CEFAR.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEFActionResponse class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary>A CEF Action Response extensions class.</summary>
    public static class CEFAR
    {
        /// <summary>A T extension method that wrap in <see cref="CEFActionResponse{T}"/>.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T?> WrapInPassingCEFAR<T>(this T? result, params string[] messages)
        {
            return new(result, true, messages);
        }

        /// <summary>A T extension method that wrap in passing <see cref="CEFActionResponse{T}"/> if not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">                The result to act on.</param>
        /// <param name="additionalFailMessages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T> WrapInPassingCEFARIfNotNull<T>(this T? result, params string[] additionalFailMessages)
        {
            return result == null
                ? FailingCEFAR<T>(new List<string>(additionalFailMessages) { "The result was null" }.ToArray())
                : result.WrapInPassingCEFAR()!;
        }

        /// <summary>A <see cref="Task{T}"/> extension method that await and wrap result in passing
        /// <see cref="CEFActionResponse{T}"/> if not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="task">                  The task to act on.</param>
        /// <param name="additionalFailMessages">A variable-length parameters list containing additional fail messages.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_T}"/>.</returns>
        public static async Task<CEFActionResponse<T?>> AwaitAndWrapResultInPassingCEFARIfNotNullAsync<T>(
            this Task<T?> task,
            params string[] additionalFailMessages)
        {
            var result = await task;
            return (result == null
                ? FailingCEFAR<T?>(new List<string>(additionalFailMessages) { "The result was null" }.ToArray())
                : result.WrapInPassingCEFAR()!)!;
        }

        /// <summary>A <see cref="Task{T}"/> extension method that await and wrap result in passing
        /// <see cref="CEFActionResponse{T}"/> if not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="task">                  The task to act on.</param>
        /// <param name="additionalFailMessages">A variable-length parameters list containing additional fail messages.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_T}"/>.</returns>
        public static async Task<CEFActionResponse<T>> AwaitAndWrapResultInPassingCEFARAsync<T>(
            this Task<T?> task,
            params string[] additionalFailMessages)
        {
            var result = await task;
            return (result == null
                ? FailingCEFAR<T>(new List<string>(additionalFailMessages) { "The result was null" }.ToArray())
                : result.WrapInPassingCEFAR()!)!;
        }

        /// <summary>A T extension method that wrap in passing <see cref="CEFActionResponse{T}"/> if not null or empty.</summary>
        /// <typeparam name="T"> Generic type parameter.</typeparam>
        /// <typeparam name="T2">Generic type parameter of the inner type.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T> WrapInPassingCEFARIfNotNullOrEmpty<T, T2>(
                this T? result,
                params string[] messages)
            where T : IEnumerable<T2>
        {
            return result == null
                ? FailingCEFAR<T>("The result was null")
                : !result.Any()
                    ? FailingCEFAR<T>("The result was an empty collection")
                    : new(result, true, messages);
        }

        /// <summary>A T extension method that wrap in failing <see cref="CEFActionResponse"/>.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T?> WrapInFailingCEFAR<T>(this T? result, params string[] messages)
        {
            return new(result, false, messages);
        }

        /// <summary>Passing <see cref="CEFActionResponse"/>.</summary>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public static CEFActionResponse PassingCEFAR(params string[] messages)
        {
            return new(true, messages);
        }

        /// <summary>Passing <see cref="CEFActionResponse{T}"/>.</summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="result">  The result.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T?> PassingCEFAR<T>(T? result, params string[] messages)
        {
            return new(result, true, messages);
        }

        /// <summary>Failing <see cref="CEFActionResponse"/>.</summary>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public static CEFActionResponse FailingCEFAR(params string?[]? messages)
        {
            return new(false, messages);
        }

        /// <summary>Failing <see cref="CEFActionResponse"/>.</summary>
        /// <param name="message"> The message.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public static CEFActionResponse FailingCEFAR(string? message, params string[] messages)
        {
            var altMessages = new List<string?>(messages);
            altMessages.Insert(0, message);
            return new(false, altMessages.ToArray());
        }

        /// <summary>Failing <see cref="CEFActionResponse{T}"/>.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T> FailingCEFAR<T>(params string?[]? messages)
        {
            return new(false, messages?.ToArray() ?? Array.Empty<string>());
        }

        /// <summary>A CEFActionResponse{TSource} extension method that change CEFActionResponse type.</summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A <see cref="CEFActionResponse{TResult}"/>.</returns>
        public static CEFActionResponse<TResult> ChangeCEFARType<TSource, TResult>(
                this CEFActionResponse<TSource> result)
            where TResult : TSource
        {
            return new(
                (TResult)result.Result!,
                result.ActionSucceeded,
                result.Messages.ToArray());
        }

        /// <summary>A CEFActionResponse{List{TSource}} extension method that change CEFActionResponse list type.</summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A <see cref="CEFActionResponse{List_TResult}"/>.</returns>
        public static CEFActionResponse<List<TResult>> ChangeCEFARListType<TSource, TResult>(
                this CEFActionResponse<List<TSource>> result)
            where TResult : TSource
        {
            return new(
                result.Result?.Cast<TResult>().ToList(),
                result.ActionSucceeded,
                result.Messages.ToArray());
        }

        /// <summary>A CEFActionResponse extension method that change failing CEFActionResponse type.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T> ChangeFailingCEFARType<T>(this CEFActionResponse result)
        {
            return new(false, result.Messages.ToArray());
        }

        /// <summary>A bool extension method that converts the value to a <see cref="CEFActionResponse"/>.</summary>
        /// <param name="result">      The result to act on.</param>
        /// <param name="failMessages">A variable-length parameters list containing fail messages.</param>
        /// <returns>The given data converted to a <see cref="CEFActionResponse"/>.</returns>
        public static CEFActionResponse BoolToCEFAR(this bool result, params string?[]? failMessages)
        {
            return result
                ? PassingCEFAR()
                : FailingCEFAR(failMessages);
        }

        /// <summary>A bool extension method that converts this CEFAR to a <see cref="CEFActionResponse{T}"/>.</summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="result">      The result to act on.</param>
        /// <param name="r">           The TResult to process.</param>
        /// <param name="failMessages">A variable-length parameters list containing fail messages.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        public static CEFActionResponse<T?> BoolToCEFAR<T>(
            this bool result,
            T? r,
            params string[] failMessages)
        {
            return result
                ? PassingCEFAR(r)
                : FailingCEFAR<T?>(failMessages);
        }

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection).</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection).</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection and the results output to a list).</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="CEFActionResponse{List_TInnerType}"/>.</returns>
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection and the results output to a list).</summary>
        /// <typeparam name="TInnerType">Type of the inner type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="CEFActionResponse{List_TInnerType}"/>.</returns>
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection and the results output to a list).</summary>
        /// <typeparam name="TInnerType"> Type of the inner type.</typeparam>
        /// <typeparam name="TResultType">Type of the result type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="CEFActionResponse{List_TInnerType}"/>.</returns>
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection and the results output to a list).</summary>
        /// <typeparam name="TInnerType"> Type of the inner type.</typeparam>
        /// <typeparam name="TResultType">Type of the result type.</typeparam>
        /// <param name="objects">The objects.</param>
        /// <param name="func">   The function.</param>
        /// <returns>A <see cref="CEFActionResponse{List_TInnerType}"/>.</returns>
        public static async Task<CEFActionResponse<List<TResultType?>>> AggregateAsync<TInnerType, TResultType>(
            IEnumerable<TInnerType?> objects,
            Func<TInnerType?, Task<CEFActionResponse<TResultType?>>> func)
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

        /// <summary>Aggregates several <see cref="CEFActionResponse"/> together (booleans are and'd together and the
        /// messages are unioned to a single collection).</summary>
        /// <param name="responses">A variable-length parameters list containing responses.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public static CEFActionResponse Aggregate(params CEFActionResponse[] responses)
        {
            var retVal = PassingCEFAR();
            foreach (var r in responses)
            {
                retVal.ActionSucceeded &= r.ActionSucceeded;
                if (r.Messages?.Any() == true)
                {
                    retVal.Messages.AddRange(r.Messages);
                }
            }
            return retVal;
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an
        /// <see cref="InvalidOperationException"/> if failed, with the first message in the list as the exception
        /// message.</summary>
        /// <param name="cefar">The <see cref="CEFActionResponse"/> to check.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        [Pure]
        public static CEFActionResponse ThrowIfFailed(this CEFActionResponse cefar)
        {
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            throw new InvalidOperationException(
                cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred");
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an
        /// <see cref="InvalidOperationException"/> if failed, with the first message in the list as the exception
        /// message.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="cefar">The <see cref="CEFActionResponse{T}"/> to check.</param>
        /// <returns>A <see cref="CEFActionResponse{T}"/>.</returns>
        [Pure]
        public static CEFActionResponse<T> ThrowIfFailed<T>(this CEFActionResponse<T> cefar)
        {
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            throw new InvalidOperationException(
                cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred");
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an
        /// <see cref="InvalidOperationException"/> if failed, with the first message in the list as the exception
        /// message.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="task">The task which returns a <see cref="CEFActionResponse{T}"/> to check.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_T}"/>.</returns>
        [Pure]
        public static async Task<CEFActionResponse<T>> AwaitAndThrowIfFailedAsync<T>(this Task<CEFActionResponse<T>> task)
        {
            var cefar = await task;
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            throw new InvalidOperationException(
                cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred");
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an TException if failed, with
        /// the first message in the list as the exception message.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="cefar">The <see cref="CEFActionResponse"/> to check.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        [Pure]
        public static CEFActionResponse ThrowIfFailed<TException>(this CEFActionResponse cefar)
            where TException : Exception
        {
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            var message = cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred";
            if (typeof(TException) == typeof(ArgumentNullException))
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message, message)!;
            }
            throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an TException if failed, with
        /// the first message in the list as the exception message.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="task">The <see cref="CEFActionResponse"/> to check.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        [Pure]
        public static async Task<CEFActionResponse> AwaitAndThrowIfFailedAsync<TException>(
                this Task<CEFActionResponse> task)
            where TException : Exception
        {
            var cefar = await task;
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            var message = cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred";
            if (typeof(TException) == typeof(ArgumentNullException))
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message, message)!;
            }
            throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an TException if failed, with
        /// the first message in the list as the exception message.</summary>
        /// <typeparam name="T">         Generic type parameter.</typeparam>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="cefar">The <see cref="CEFActionResponse"/> to check.</param>
        /// <returns>A CEFActionResponse.</returns>
        [Pure]
        public static CEFActionResponse<T> ThrowIfFailed<T, TException>(this CEFActionResponse<T> cefar)
            where TException : Exception
        {
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            var message = cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred";
            if (typeof(TException) == typeof(ArgumentNullException))
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message, message)!;
            }
            throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }

        /// <summary>A <see cref="CEFActionResponse"/> extension method that will throw an TException if failed, with
        /// the first message in the list as the exception message.</summary>
        /// <typeparam name="T">         Generic type parameter.</typeparam>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="task">The <see cref="Task{CEFActionResponse_T}"/> to check.</param>
        /// <returns>A CEFActionResponse.</returns>
        [Pure]
        public static async Task<CEFActionResponse<T>> AwaitAndThrowIfFailedAsync<T, TException>(
                this Task<CEFActionResponse<T>> task)
            where TException : Exception
        {
            var cefar = await task;
            if (cefar.ActionSucceeded)
            {
                return cefar;
            }
            var message = cefar.Messages.FirstOrDefault() ?? "An unknown error has occurred";
            if (typeof(TException) == typeof(ArgumentNullException))
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message, message)!;
            }
            throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }
    }
}
