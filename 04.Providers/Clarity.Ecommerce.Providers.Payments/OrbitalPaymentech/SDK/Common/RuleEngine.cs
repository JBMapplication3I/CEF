#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Xml;
using JPMC.MSDK.Filer;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK.Common
{
    /// <summary>
    /// Summary description for RuleEngine.
    /// </summary>
    public class RuleEngine : IRuleEngine
    {
        private string name;
        private XmlDocument document;
        private CircularBuffer buffer;
        private List<Rule> rules = new List<Rule>();
        private bool matchAll = true;

        /// <summary>
        /// Creates a new RuleEngine based on the given name.
        /// </summary>
        /// <param name="name">The name of the engine to load.</param>
        public RuleEngine( string name, string homeDir, IDispatcherFactory factory )
        {
            this.name = name;
            XmlNode engineNode = null;
            var minTime = 0L;
            document = factory.Definitions.GetXmlDocument( "definitions/RuleEngine.xml", true);
            if ( document == null )
            {
                throw new FilerException( Error.RuleEngineInitFailed, "Failed to load RuleEngine.xml" );
            }
            engineNode = document.DocumentElement;
            minTime = Utils.StringToInt( Utils.GetAttributeValue( "minTime", "5000", engineNode ) );
            var ruleEngineNode = GetNode( name, document );
            if ( ruleEngineNode == null )
            {
                throw new FilerException( Error.RuleEngineInitFailed, "The specified engine could not be found." );
            }

            if ( ruleEngineNode != null )
            {
                matchAll = Utils.StringToBoolean( Utils.GetAttributeValue( "matchall", "true", ruleEngineNode ) );
            }

            foreach ( XmlNode node in ruleEngineNode.ChildNodes )
            {
                if ( matchAll && node.Name == "Option" )
                {
                    continue;
                }

                if ( node.Name != "Rule" && node.Name != "Option")
                {
                    continue;
                }

                var ruleName = Utils.GetAttributeValue( "name", null, node );
                var method = Utils.GetAttributeValue( "method", null, node );
                var pattern = Utils.GetAttributeValue( "pattern", null, node );
                pattern = ConvertEscapeCharacters( pattern );
                var endRead = false;
                long minMilliSeconds = Utils.StringToInt( Utils.GetAttributeValue( "minTime", minTime.ToString(), node ) );
                var rule = new Rule( ruleName, method, pattern );
                rule.MinMilliSeconds = minMilliSeconds;
                rule.EndReadRequired = endRead;
                var dependsNodes = node.SelectNodes( "Depends" );
                foreach ( XmlNode dependsNode in dependsNodes )
                {
                    var depends = Utils.GetTextNode( dependsNode ).Value;
                    rule.Dependencies.Add( depends );
                }
                rules.Add( rule );
            }

            ResizeBuffer();
        }

        private XmlNode GetNode( string name, XmlDocument document )
        {
            var nodes = document.GetElementsByTagName( "Engine" );
            XmlNode defaultNode = null;

            foreach ( XmlNode node in nodes )
            {
                if ( Utils.GetAttributeValue( "name", "", node ) == name )
                {
                    return node;
                }
                if ( Utils.GetAttributeValue( "name", "", node ) == "Default" )
                {
                    defaultNode = node;
                }
            }

            return defaultNode;
        }

        #region IRuleEngine Members

        /// <summary>
        /// The name of the loaded engine.
        /// </summary>
        public string Name => name;

        public bool MatchAll => matchAll;

        /// <summary>
        /// The size of the largest unsatisfied pattern.
        /// </summary>
        public int Length
        {
            get
            {
                if ( buffer == null )
                    return 0;
                return buffer.Limit;
            }
        }

        /// <summary>
        /// Returns true if all the rules are satisfied.
        /// </summary>
        public bool Satisfied
        {
            get
            {
                var matchedOne = false;
                foreach ( var rule in rules )
                {
                    if ( matchAll && !rule.Satisfied )
                    {
                        return false;
                    }
                    matchedOne = rule.Satisfied ? true : matchedOne;
                }
                return matchedOne;
            }
        }

        /// <summary>
        /// Provides access to all the rules.
        /// </summary>
        public Rule[] Rules => rules.ToArray();

        /// <summary>
        /// Returns the rule that has the given name,
        /// or null if it could not be found.
        /// </summary>
        /// <param name="name">The name of the Rule to find.</param>
        /// <returns>The Rule asked for, or null if not found.</returns>
        public Rule GetRule( string name )
        {
            foreach ( var rule in rules )
            {
                if ( rule.Name == name )
                    return rule;
            }
            return null;
        }

        /// <summary>
        /// Adds the specified Rule to the collection.
        /// </summary>
        /// <remarks>
        /// AddRule will return false if a rule by that name already exists.
        /// </remarks>
        /// <param name="rule">The rule to add.</param>
        /// <returns>True if the rule was successfully added, false if it already exists.</returns>
        public bool AddRule( Rule rule )
        {
            if ( GetRule( rule.Name ) == null )
            {
                rules.Add( rule );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds more data to the data buffer.
        /// </summary>
        /// <param name="data">The data, in bytes, to add.</param>
        public string AddData( byte[] data )
        {
            if ( buffer == null )
            {
                return null;
            }

            string terminator = null;
            foreach ( var bit in data )
            {
                buffer.Add( bit );
                terminator = ResolveRules();
            }

            return terminator;
        }

        /// <summary>
        /// Iterates through all rules and satisfies all
        /// whose dependencies are met, whose patterns have
        /// been matched, and that enough time has elapsed
        /// since the pattern was matched.
        /// </summary>
        public void SetEndRead()
        {
            var time = DateTime.Now;
            foreach ( var rule in rules )
            {
                if ( !rule.EndReadRequired || !rule.PatternMatched )
                    continue;

                var interval = (long) ( (TimeSpan) ( time - rule.TimeMatched ) ).TotalMilliseconds;
                if ( interval < rule.MinMilliSeconds )
                    continue;

                rule.Satisfied = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Iterates through all unsatisfied rules whose dependencies are met
        /// and performs the its logic to determine if its conditions are
        /// satisfied.
        /// </summary>
        /// <returns>The pattern that was matched.</returns>
        private string ResolveRules()
        {
            string terminator = null;

            foreach ( var rule in rules )
            {
                if ( rule.Satisfied )
                {
                    continue;
                }

                if ( rule.Method.ToLower() == "match" )
                {
                    if ( !DependenciesAreMet( rule ) )
                    {
                        continue;
                    }

                    var buff = Utils.ByteArrayToString( buffer.Get() );
                    if ( buff.IndexOf( rule.Pattern ) != -1 )
                    {
                        rule.PatternMatched = true;
                    }
                    if ( rule.PatternMatched && !rule.EndReadRequired )
                    {
                        rule.Satisfied = true;
                        terminator = rule.Pattern;
                    }
                }
            }

            ResizeBuffer();
            return terminator;
        }

        /// <summary>
        /// Checks if a Rule's dependencies are all satisfied.
        /// </summary>
        /// <param name="rule">The Rule whose dependencies are to be checked.</param>
        /// <returns>True if all of the Rules dependencies are satisfied, false if at least one is not.</returns>
        private bool DependenciesAreMet( Rule rule )
        {
            foreach ( var ruleName in rule.Dependencies )
            {
                var depend = GetRule( ruleName );
                if ( depend != null && !depend.Satisfied )
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the current buffer length is larger than the longest
        /// pattern of all unsatisfied rules. If it is, it resizes the
        /// circular buffer to hold the longest pattern length.
        /// </summary>
        private void ResizeBuffer()
        {
            var size = 0;
            foreach ( var rule in rules )
            {
                if ( !rule.Satisfied && rule.Pattern != null && rule.Pattern.Length > size )
                    size = rule.Pattern.Length;
            }

            if ( size == 0 )
                return;

            if ( buffer == null )
            {
                buffer = new CircularBuffer( size );
                return;
            }

            if ( size == buffer.Limit )
                return;

            var bytes = buffer.Get();
            buffer = new CircularBuffer( size );
            buffer.Add( bytes );
        }

        /// <summary>
        /// Converts text representations of escape characters into their
        /// real values.
        /// </summary>
        /// <param name="pattern">The pattern that contains escape character representations.</param>
        /// <returns>A new pattern string with all appropriate escape characters converted.</returns>
        private string ConvertEscapeCharacters( string pattern )
        {
            if ( pattern == null )
                return pattern;

            var newPattern = pattern.Replace( "\\r", "\r" );
            newPattern = newPattern.Replace( "\\n", "\n" );
            newPattern = newPattern.Replace( "\\t", "\t" );
            newPattern = newPattern.Replace( "&nbsp;", " " );
            newPattern = newPattern.Replace( "&lt;", "<" );
            newPattern = newPattern.Replace( "&gt;", ">" );
            return newPattern;
        }

        #endregion
    }
}
