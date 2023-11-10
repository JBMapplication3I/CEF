#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Configurator;


namespace JPMC.MSDK.Comm
{

    ///
    ///
    ///
    /// Title: TCP Batch module interface
    ///
    /// Description: This interface defines how the comm manager will
    /// interact with the batch module
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna, RameshKumar Bhaskharan
    /// @version 1.0
    ///

    /// <summary>
    /// This interface defines how the comm manager will
    /// interact with the batch module
    /// </summary>
    public interface ICommModule
{
       CommArgs CompleteTransaction( CommArgs outmsg );

       ConfigurationData Config { get; }

       void Close();
}
}
