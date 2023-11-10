using System;
using System.Collections.Generic;

namespace JPMC.MSDK.Common
{
	/// <summary>
	/// Summary description for Rule.
	/// </summary>
	public class Rule
	{
		private bool satisfied;
		private string name;
		private string method;
		private string pattern;
		private List<string> depends = new List<string>();
		private bool endRead;
		private bool patternMatched;
		private long minMilliSeconds = 5000;
		private DateTime timeMatched = DateTime.Now;

		/// <summary>
		/// Constructor. Creates a new Rule with the supplied data.
		/// </summary>
		/// <param name="name">The name of the rule.</param>
		/// <param name="method">The type of logic to apply to it.</param>
		/// <param name="pattern">The pattern used for it.</param>
		public Rule(string name, string method, string pattern)
		{
			this.name = name;
			this.method = method;
			this.pattern = pattern;
		}

		/// <summary>
		/// The name of the rule. This is used for identification purposes.
		/// </summary>
		public string Name
		{
			get => name;
            set => name = value;
        }

		/// <summary>
		/// The name of the rule. This is used for identification purposes.
		/// </summary>
		public bool EndReadRequired
		{
			get => endRead;
            set => endRead = value;
        }

		/// <summary>
		/// The name of the rule. This is used for identification purposes.
		/// </summary>
		public bool PatternMatched
		{
			get => patternMatched;
            set => patternMatched = value;
        }

		/// <summary>
		/// The name of the rule. This is used for identification purposes.
		/// </summary>
		public DateTime TimeMatched
		{
			get => timeMatched;
            set => timeMatched = value;
        }

		/// <summary>
		/// The name of the rule. This is used for identification purposes.
		/// </summary>
		public long MinMilliSeconds
		{
			get => minMilliSeconds;
            set => minMilliSeconds = value;
        }

		/// <summary>
		/// Identifies the type of logic to be applied to the rule.
		/// </summary>
		/// <remarks>
		/// Currently, the only two methods are as follows:
		/// 1. Match (will perform a pattern matching algorithm)
		/// 2. Manual (has no logic, but depends on having its 
		///    Satisfied property set.
		/// </remarks>
		public string Method => method;

        /// <summary>
		/// The pattern used in pattern matching.
		/// </summary>
		public string Pattern => pattern;

        /// <summary>
		/// Returns true if the rule has been satisfied, false if it has not.
		/// </summary>
		public bool Satisfied
		{
			get => satisfied;
            set => satisfied = value;
        }

		/// <summary>
		/// The list of dependent rule names.
		/// </summary>
		public List<string> Dependencies => depends;
    }
}
