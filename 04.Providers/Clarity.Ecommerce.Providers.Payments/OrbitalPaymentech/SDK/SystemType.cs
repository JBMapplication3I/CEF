namespace JPMC.MSDK
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Specifies the type of system to be used for communication.
    /// </summary>
    [ComVisible( true )]
    public enum SystemType
    {
        /// <summary>
        /// For Salem processing.
        /// </summary>
        SLM,
        /// <summary>
        /// For TCS processing.
        /// </summary>
        TCS,
        /// <summary>
        /// For HCS processing.
        /// </summary>
        HCS,
        /// <summary>
        /// For ETS processing.
        /// </summary>
        ETS,
        /// <summary>
        /// For Orbital processing.
        /// </summary>
        ORB
    }
}
