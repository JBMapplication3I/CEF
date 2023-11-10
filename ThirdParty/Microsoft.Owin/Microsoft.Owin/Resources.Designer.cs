// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.Resources
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        /// <summary>Manager for resource.</summary>
        private static ResourceManager resourceMan;

        /// <summary>The resource culture.</summary>
        private static CultureInfo resourceCulture;

        /// <summary>Initializes a new instance of the <see cref="Microsoft.Owin.Resources"/> class.</summary>
        internal Resources()
        {
        }

        /// <summary>Returns the cached ResourceManager instance used by this class.</summary>
        /// <value>The resource manager.</value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (Resources.resourceMan == null)
                {
                    Resources.resourceMan = new ResourceManager("Microsoft.Owin.Resources", typeof(Resources).Assembly);
                }
                return Resources.resourceMan;
            }
        }

        /// <summary>Overrides the current thread's CurrentUICulture property for all resource lookups using this
        /// strongly typed resource class.</summary>
        /// <value>The culture.</value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => Resources.resourceCulture;
            set => Resources.resourceCulture = value;
        }

        /// <summary>Looks up a localized string similar to Conversion delegate must take one parameter.</summary>
        /// <value>The exception conversion takes one parameter.</value>
        internal static string Exception_ConversionTakesOneParameter => ResourceManager.GetString(nameof(Exception_ConversionTakesOneParameter), resourceCulture);

        /// <summary>Looks up a localized string similar to The cookie key and options are larger than ChunksSize,
        /// leaving no room for data.</summary>
        /// <value>The exception cookie limit too small.</value>
        internal static string Exception_CookieLimitTooSmall => ResourceManager.GetString(nameof(Exception_CookieLimitTooSmall), resourceCulture);

        /// <summary>Looks up a localized string similar to The chunked cookie is incomplete. Only {0} of the expected
        /// {1} chunks were found, totaling {2} characters. A client size limit may have been exceeded.</summary>
        /// <value>The exception imcomplete chunked cookie.</value>
        internal static string Exception_ImcompleteChunkedCookie => ResourceManager.GetString(nameof(Exception_ImcompleteChunkedCookie), resourceCulture);

        /// <summary>Looks up a localized string similar to The type '{0}' does not match any known middleware pattern.</summary>
        /// <value>The exception middleware not supported.</value>
        internal static string Exception_MiddlewareNotSupported => ResourceManager.GetString(nameof(Exception_MiddlewareNotSupported), resourceCulture);

        /// <summary>Looks up a localized string similar to The OWIN key 'server.OnSendingHeaders' is not available for
        /// this request.</summary>
        /// <value>The exception missing on sending headers.</value>
        internal static string Exception_MissingOnSendingHeaders => ResourceManager.GetString(nameof(Exception_MissingOnSendingHeaders), resourceCulture);

        /// <summary>Looks up a localized string similar to The class '{0}' does not have a constructor taking {1}
        /// arguments.</summary>
        /// <value>The exception no constructor found.</value>
        internal static string Exception_NoConstructorFound => ResourceManager.GetString(nameof(Exception_NoConstructorFound), resourceCulture);

        /// <summary>Looks up a localized string similar to No conversion available between {0} and {1}.</summary>
        /// <value>The exception no conversion exists.</value>
        internal static string Exception_NoConversionExists => ResourceManager.GetString(nameof(Exception_NoConversionExists), resourceCulture);

        /// <summary>Looks up a localized string similar to The path must not end with a '/'.</summary>
        /// <value>The exception path must not end with slash.</value>
        internal static string Exception_PathMustNotEndWithSlash => ResourceManager.GetString(nameof(Exception_PathMustNotEndWithSlash), resourceCulture);

        /// <summary>Looks up a localized string similar to The path must start with a '/' followed by one or more
        /// characters.</summary>
        /// <value>The exception path must start with slash.</value>
        internal static string Exception_PathMustStartWithSlash => ResourceManager.GetString(nameof(Exception_PathMustStartWithSlash), resourceCulture);

        /// <summary>Looks up a localized string similar to The path is required.</summary>
        /// <value>The exception path required.</value>
        internal static string Exception_PathRequired => ResourceManager.GetString(nameof(Exception_PathRequired), resourceCulture);

        /// <summary>Looks up a localized string similar to The query string must start with a '?' unless null or empty.</summary>
        /// <value>The exception query string must start with delimiter.</value>
        internal static string Exception_QueryStringMustStartWithDelimiter => ResourceManager.GetString(nameof(Exception_QueryStringMustStartWithDelimiter), resourceCulture);
    }
}
