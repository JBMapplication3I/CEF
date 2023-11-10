// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.OwinStartupAttribute
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;

    /// <summary>Used to mark which class in an assembly should be used for automatic startup.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class OwinStartupAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="OwinStartupAttribute" /> class.</summary>
        /// <param name="startupType">The startup class.</param>
        public OwinStartupAttribute(Type startupType)
            : this(string.Empty, startupType, string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwinStartupAttribute" /> class.</summary>
        /// <param name="friendlyName">A non-default configuration, e.g. staging.</param>
        /// <param name="startupType"> The startup class.</param>
        public OwinStartupAttribute(string friendlyName, Type startupType)
            : this(friendlyName, startupType, string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwinStartupAttribute" /> class.</summary>
        /// <param name="startupType">The startup class.</param>
        /// <param name="methodName"> Specifies which method to call.</param>
        public OwinStartupAttribute(Type startupType, string methodName)
            : this(string.Empty, startupType, methodName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwinStartupAttribute" /> class.</summary>
        /// <param name="friendlyName">A non-default configuration, e.g. staging.</param>
        /// <param name="startupType"> The startup class.</param>
        /// <param name="methodName">  Specifies which method to call.</param>
        public OwinStartupAttribute(string friendlyName, Type startupType, string methodName)
        {
            FriendlyName = friendlyName ?? throw new ArgumentNullException(nameof(friendlyName));
            StartupType = startupType ?? throw new ArgumentNullException(nameof(startupType));
            MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
        }

        /// <summary>A non-default configuration if any. e.g. Staging.</summary>
        /// <value>The name of the friendly.</value>
        public string FriendlyName { get; }

        /// <summary>The name of the configuration method.</summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; }

        /// <summary>The startup class.</summary>
        /// <value>The type of the startup.</value>
        public Type StartupType { get; }
    }
}
