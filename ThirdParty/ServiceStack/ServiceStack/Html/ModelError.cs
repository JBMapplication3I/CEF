using System;

namespace ServiceStack.Html
{
#if !NETSTANDARD1_6
	[Serializable]
#endif
	public class ModelError
	{
		public ModelError(Exception exception)
			: this(exception, null /* errorMessage */)
		{
		}

		public ModelError(Exception exception, string errorMessage)
			: this(errorMessage)
		{
			Exception = exception ?? throw new ArgumentNullException(nameof(exception));
		}

		public ModelError(string errorMessage)
		{
			ErrorMessage = errorMessage ?? string.Empty;
		}

		public Exception Exception
		{
			get;
			private set;
		}

		public string ErrorMessage
		{
			get;
			private set;
		}
	}
}
