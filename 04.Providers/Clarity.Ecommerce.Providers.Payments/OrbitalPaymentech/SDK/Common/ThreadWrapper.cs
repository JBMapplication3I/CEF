#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Threading;

namespace JPMC.MSDK.Common
{
    ///
    ///
    /// Title: Thread Wrappper
    ///
    /// Description: A wrapper for the Thread class
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 1.0
    ///

    /// <summary>
    /// A wrapper for the Thread class. This allows all methods to be virtual and therefore able to be
    /// overridden in a derived stub class during testing.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
public class ThreadWrapper : WrapperBase<ThreadWrapper>
{
    private Thread thread;

    /// <summary>
    /// We make this public only because it is required for Type argument in the generic class
    /// WrapperFactory. In reality we want everyone to use the factory (GetInstance) so this class
    /// can be replaced with a stub when testing.
    /// </summary>
    public ThreadWrapper()
    {
    }

    public virtual void Initialize( Thread obj )
    {
        if (obj != null)
        {
            thread = obj;
        }
    }

    public virtual void Initialize( ThreadStart st )
    {
        thread = new Thread( st );
    }

    public virtual bool IsBackground
    {

        set
        {
            if (thread == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            thread.IsBackground = value;
        }
    }

    public virtual string Name
    {
        get
        {
            if (thread == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            return thread.Name;
        }
        set => thread.Name = value;
    }

    public virtual int ManagedThreadId
    {
        get
        {
            if (thread == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            return thread.ManagedThreadId;
        }
    }

    public virtual bool IsEqual( Thread threadObj )
    {
        if (thread == null)
        {
            throw new ObjectDisposedException("Object not properly initialized");
        }
        return thread == threadObj;
    }

    public virtual void Start()
    {
        if (thread == null)
        {
            throw new ObjectDisposedException("Object not properly initialized");
        }
        thread.Start();
    }
}
}

