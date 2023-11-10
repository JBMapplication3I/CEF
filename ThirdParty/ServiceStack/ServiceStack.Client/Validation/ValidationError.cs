namespace ServiceStack.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ServiceStack.Model;
    using ServiceStack.Text;

    /// <summary>
    /// The exception which is thrown when a validation error occurred.
    /// This validation is serialized in a extra clean and human-readable way by ServiceStack.
    /// </summary>
    public class ValidationError : ArgumentException, IResponseStatusConvertible
    {
        public string ErrorMessage { get; }

        public ValidationError(string errorCode)
            : this(errorCode, errorCode.SplitCamelCase())
        {
        }

        public ValidationError(ValidationErrorResult validationResult)
            : base(validationResult.ErrorMessage)
        {
            ErrorCode = validationResult.ErrorCode;
            ErrorMessage = validationResult.ErrorMessage;
            Violations = validationResult.Errors;
        }

        public ValidationError(ValidationErrorField validationError)
            : this(validationError.ErrorCode, validationError.ErrorMessage)
        {
            Violations.Add(validationError);
        }

        public ValidationError(string errorCode, string errorMessage)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Violations = new List<ValidationErrorField>();
        }

        /// <summary>
        /// Returns the first error code
        /// </summary>
        /// <value>The error code.</value>
        public string ErrorCode { get; }

        public override string Message
        {
            get
            {
                //If there is only 1 validation error than we just show the error message
                if (Violations.Count == 0)
                {
                    return ErrorMessage;
                }

                if (Violations.Count == 1)
                {
                    return ErrorMessage ?? Violations[0].ErrorMessage;
                }

                var sb = StringBuilderCache.Allocate()
                    .Append(ErrorMessage).AppendLine();
                foreach (var error in Violations)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        var fieldLabel = error.FieldName != null ? $" [{error.FieldName}]" : null;
                        sb.Append($"\n  - {error.ErrorMessage}{fieldLabel}");
                    }
                    else
                    {
                        var fieldLabel = error.FieldName != null ? ": " + error.FieldName : null;
                        sb.Append($"\n  - {error.ErrorCode}{fieldLabel}");
                    }
                }
                return StringBuilderCache.ReturnAndFree(sb);
            }
        }

        public IList<ValidationErrorField> Violations { get; private set; }

        /// <summary>
        /// Used if we need to serialize this exception to XML
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            var sb = StringBuilderCache.Allocate();
            sb.Append("<ValidationException>");
            foreach (var error in Violations)
            {
                sb.Append("<ValidationError>")
                    .Append($"<Code>{error.ErrorCode}</Code>")
                    .Append($"<Field>{error.FieldName}</Field>")
                    .Append($"<Message>{error.ErrorMessage}</Message>")
                    .Append("</ValidationError>");
            }
            sb.Append("</ValidationException>");
            return StringBuilderCache.ReturnAndFree(sb);
        }

        public static ValidationError CreateException(Enum errorCode)
        {
            return new(errorCode.ToString());
        }

        public static ValidationError CreateException(Enum errorCode, string errorMessage)
        {
            return new(errorCode.ToString(), errorMessage);
        }

        public static ValidationError CreateException(Enum errorCode, string errorMessage, string fieldName)
        {
            return CreateException(errorCode.ToString(), errorMessage, fieldName);
        }

        public static ValidationError CreateException(string errorCode)
        {
            return new(errorCode);
        }

        public static ValidationError CreateException(string errorCode, string errorMessage)
        {
            return new(errorCode, errorMessage);
        }

        public static ValidationError CreateException(string errorCode, string errorMessage, string fieldName)
        {
            var error = new ValidationErrorField(errorCode, fieldName, errorMessage);
            return new(new ValidationErrorResult(new List<ValidationErrorField> { error }));
        }

        public static ValidationError CreateException(ValidationErrorField error)
        {
            return new(error);
        }

        public static void ThrowIfNotValid(ValidationErrorResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new ValidationError(validationResult);
            }
        }

        public ResponseStatus ToResponseStatus()
        {
            return ResponseStatusUtils.CreateResponseStatus(ErrorCode, Message, Violations);
        }
    }
}