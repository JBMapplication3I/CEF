#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Common
{
    /// <summary>
    /// Summary description for IRuleEngine.
    /// </summary>
    public interface IRuleEngine
    {
        /// <summary>
        /// The name of the loaded engine.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The size of the largest unsatisfied pattern.
        /// </summary>
        int Length { get; }
        /// <summary>
        /// Returns true if all the rules are satisfied.
        /// </summary>
        bool Satisfied { get; }
        /// <summary>
        /// Provides access to all the rules.
        /// </summary>
        Rule[] Rules { get; }
        /// <summary>
        /// Returns the rule that has the given name,
        /// or null if it could not be found.
        /// </summary>
        /// <param name="name">The name of the Rule to find.</param>
        /// <returns>The Rule asked for, or null if not found.</returns>
        Rule GetRule( string name );
        /// <summary>
        /// Adds the specified Rule to the collection.
        /// </summary>
        /// <remarks>
        /// AddRule will return false if a rule by that name already exists.
        /// </remarks>
        /// <param name="rule">The rule to add.</param>
        /// <returns>True if the rule was successfully added, false if it already exists.</returns>
        bool AddRule( Rule rule );
        /// <summary>
        /// Adds more data to the data buffer.
        /// </summary>
        /// <param name="data">The data, in bytes, to add.</param>
        /// <returns>The file terminator that it used to match a success.</returns>
        string AddData( byte[] data );
        /// <summary>
        /// Iterates through all rules and satisfies all
        /// whose dependencies are met, whose patterns have
        /// been matched, and that enough time has elapsed
        /// since the pattern was matched.
        /// </summary>
        void SetEndRead();

        /// <summary>
        /// Specifies whether all the rules for the engine must be
        /// satisfied for the entire engine to be satisfied.
        /// </summary>
        bool MatchAll { get; }
    }
}
