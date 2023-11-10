#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Adapters
{
    public enum TransactionType
    {
        NONE,
        Start,
        Next,
        End,
        Cancel,
        Open,
        Delete,
        HeartBeat,
        Passthru,

        // Orbital types (corresponds to Orbital template names)
        AccountUpdater,
        Inquiry,
        NewOrder,
        EndOfDay,
        FlexCache,
        Profile,
        Reversal,
        MarkForCapture,
        SafetechFraudAnalysis
    }
}
