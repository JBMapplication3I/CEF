// Decompiled with JetBrains decompiler
// Type: Owin.IAppBuilder
// Assembly: Owin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f0ebd12fd5e55cc5
// MVID: C152461C-65C1-4F51-912C-2454A21D9BAD
// Assembly location: C:\Users\jotha\.nuget\packages\owin\1.0.0\lib\net40\Owin.dll

namespace Owin
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for application builder.</summary>
    public interface IAppBuilder
    {
        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        IDictionary<string, object> Properties { get; }

        /// <summary>Builds the given return type.</summary>
        /// <param name="returnType">Type of the return.</param>
        /// <returns>An object.</returns>
        object Build(Type returnType);

        /// <summary>Gets the new.</summary>
        /// <returns>An IAppBuilder.</returns>
        IAppBuilder New();

        /// <summary>Uses.</summary>
        /// <param name="middleware">The middleware.</param>
        /// <param name="args">      A variable-length parameters list containing arguments.</param>
        /// <returns>An IAppBuilder.</returns>
        IAppBuilder Use(object middleware, params object[] args);
    }
}
