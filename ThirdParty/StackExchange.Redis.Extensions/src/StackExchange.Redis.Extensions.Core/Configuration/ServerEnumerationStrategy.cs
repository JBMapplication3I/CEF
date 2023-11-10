namespace StackExchange.Redis.Extensions.Core.Configuration
{
    /// <summary>A server enumeration strategy.</summary>
    public partial class ServerEnumerationStrategy
    {
        /// <summary>Values that represent mode options.</summary>
        public enum ModeOptions
        {
            /// <summary>An enum constant representing all option.</summary>
            All = 0,

            /// <summary>An enum constant representing the single option.</summary>
            Single,
        }

        /// <summary>Values that represent target role options.</summary>
        public enum TargetRoleOptions
        {
            /// <summary>An enum constant representing any option.</summary>
            Any = 0,

            /// <summary>An enum constant representing the prefer slave option.</summary>
            PreferSlave,
        }

        /// <summary>Values that represent unreachable server action options.</summary>
        public enum UnreachableServerActionOptions
        {
            /// <summary>An enum constant representing the throw option.</summary>
            Throw = 0,

            /// <summary>An enum constant representing the ignore if other available option.</summary>
            IgnoreIfOtherAvailable,
        }

        //[ConfigurationProperty("mode", IsRequired = false, DefaultValue = "All")]

        /// <summary>Specify the strategy mode.</summary>
        /// <value>Default value All.</value>
        public ModeOptions Mode { get; set; }

        /// <summary>Specify the target role.</summary>
        /// <value>Default value Any.</value>
        public TargetRoleOptions TargetRole { get; set; }

        /// <summary>Specify the unreachable server action.</summary>
        /// <value>Default value Throw.</value>
        public UnreachableServerActionOptions UnreachableServerAction { get; set; }
    }
}
