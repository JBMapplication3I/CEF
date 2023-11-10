namespace ServiceStack.FluentValidation.Internal
{
    using System.Collections.Generic;

    /// <summary>
    /// Assists in the construction of validation messages.
    /// </summary>
    public class MessageFormatter
    {
        private readonly Dictionary<string, object> placeholderValues = new(2);
        private object[] additionalArguments = new object[0];
        private bool shouldUseAdditionalArgs;

        /// <summary>
        /// Default Property Name placeholder.
        /// </summary>
        public const string PropertyName = "PropertyName";

        /// <summary>
        /// Default Property Value placeholder.
        /// </summary>
        public const string PropertyValue = "PropertyValue";

        /// <summary>
        /// Adds a value for a validation message placeholder.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MessageFormatter AppendArgument(string name, object value)
        {
            placeholderValues[name] = value;
            return this;
        }

        /// <summary>
        /// Appends a property name to the message.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns></returns>
        public MessageFormatter AppendPropertyName(string name)
        {
            return AppendArgument(PropertyName, name);
        }

        /// <summary>
        /// Appends a property value to the message.
        /// </summary>
        /// <param name="value">The value of the property</param>
        /// <returns></returns>
        public MessageFormatter AppendPropertyValue(object value)
        {
            return AppendArgument(PropertyValue, value);
        }

        /// <summary>
        /// Adds additional arguments to the message for use with standard string placeholders.
        /// </summary>
        /// <param name="additionalArgs">Additional arguments</param>
        /// <returns></returns>
        public MessageFormatter AppendAdditionalArguments(params object[] additionalArgs)
        {
            additionalArguments = additionalArgs;
            shouldUseAdditionalArgs = additionalArguments?.Length > 0;
            return this;
        }

        /// <summary>
        /// Constructs the final message from the specified template.
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>The message with placeholders replaced with their appropriate values</returns>
        public string BuildMessage(string messageTemplate)
        {

            var result = messageTemplate;

            foreach (var pair in placeholderValues)
            {
                result = ReplacePlaceholderWithValue(result, pair.Key, pair.Value);
            }

            if (shouldUseAdditionalArgs)
            {
                return string.Format(result, additionalArguments);
            }
            return result;
        }

        /// <summary>
        /// Additional arguments to use
        /// </summary>
        public object[] AdditionalArguments => additionalArguments;

        /// <summary>
		/// Additional placeholder values
		/// </summary>
		public Dictionary<string, object> PlaceholderValues => placeholderValues;

        private static string ReplacePlaceholderWithValue(string template, string key, object value)
        {
            var placeholder = GetPlaceholder(key);
            return template.Replace(placeholder, value == null ? null : value.ToString());
        }

        private static string GetPlaceholder(string key)
        {
            // Performance: String concat causes much overhead when not needed. Concatting constants results in constants being compiled.
            switch (key)
            {
                case PropertyName:
                    return "{" + PropertyName + "}";
                case PropertyValue:
                    return "{" + PropertyValue + "}";
                default:
                    return "{" + key + "}";
            }
        }
    }
}