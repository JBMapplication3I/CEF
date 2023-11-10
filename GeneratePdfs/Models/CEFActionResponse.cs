namespace GeneratePdfs.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class CEFActionResponse<TResult> : CEFActionResponse
    {
        public CEFActionResponse() { }
        public CEFActionResponse(CEFActionResponse response) : this()
        {
            Messages = response.Messages;
            ActionSucceeded = response.ActionSucceeded;
        }
        public CEFActionResponse(CEFActionResponse response, TResult result) : this(response) { Result = result; }
        public CEFActionResponse(bool actionSucceeded) : base(actionSucceeded) { }
        public CEFActionResponse(bool actionSucceeded, params string[] messages) : base(actionSucceeded, messages) { }
        public CEFActionResponse(TResult result, bool actionSucceeded) : base(actionSucceeded) { Result = result; }
        public CEFActionResponse(TResult result, bool actionSucceeded, params string[] messages) : base(actionSucceeded, messages) { Result = result; }
        public TResult? Result { get; set; }
    }

    public class CEFActionResponse
    {
        public CEFActionResponse()
        {
            Messages = new List<string>();
        }
        public CEFActionResponse(bool actionSucceeded)
        {
            ActionSucceeded = actionSucceeded;
            Messages = new List<string>();
        }
        public CEFActionResponse(bool actionSucceeded, params string[] messages)
        {
            ActionSucceeded = actionSucceeded;
            Messages = messages?.ToList();
        }
        public bool ActionSucceeded { get; set; }
        public List<string>? Messages { get; set; }
    }

    public static class CEFAR
    {
        /// <summary>A T extension method that wrap in CEFActionResponse{T}.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">  The result to act on.</param>
        /// <param name="messages">A variable-length parameters list containing messages.</param>
        /// <returns>A CEFActionResponse{T}</returns>
        public static CEFActionResponse<T> WrapInPassingCEFAR<T>(this T result, params string[] messages)
        {
            return new CEFActionResponse<T>(result, true, messages);
        }

        public static CEFActionResponse<T> WrapInPassingCEFARIfNotNull<T>(this T result, params string[] failMessages)
        {
            return result == null
                ? FailingCEFAR<T>(failMessages)
                : result.WrapInPassingCEFAR();
        }

        public static CEFActionResponse<T> WrapInFailingCEFAR<T>(this T result, params string[] messages)
        {
            return new CEFActionResponse<T>(result, false, messages);
        }

        public static CEFActionResponse PassingCEFAR(params string[] messages)
        {
            return new CEFActionResponse(true, messages);
        }

        public static CEFActionResponse<TResult> PassingCEFAR<TResult>(TResult result, params string[] messages)
        {
            return new CEFActionResponse<TResult>(result, true, messages);
        }

        public static CEFActionResponse FailingCEFAR(params string[] messages)
        {
            return new CEFActionResponse(false, messages);
        }

        public static CEFActionResponse<T> FailingCEFAR<T>(params string[] messages)
        {
            return new CEFActionResponse<T>(false, messages);
        }
    }
}
