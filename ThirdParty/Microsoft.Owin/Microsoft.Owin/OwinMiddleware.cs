// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.OwinMiddleware
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.Threading.Tasks;

    /// <summary>An abstract base class for a standard middleware pattern.</summary>
    public abstract class OwinMiddleware
    {
        /// <summary>Instantiates the middleware with an optional pointer to the next component.</summary>
        /// <param name="next">.</param>
        protected OwinMiddleware(OwinMiddleware next)
        {
            Next = next;
        }

        /// <summary>The optional next component.</summary>
        /// <value>The next.</value>
        protected OwinMiddleware Next { get; set; }

        /// <summary>Process an individual request.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public abstract Task Invoke(IOwinContext context);
    }
}
