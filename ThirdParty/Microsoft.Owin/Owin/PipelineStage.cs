// <copyright file="PipelineStage.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pipeline stage class</summary>
namespace Owin
{
    /// <summary>An ordered list of known Asp.Net integrated pipeline stages. More details on the ASP.NET integrated
    /// pipeline can be found at http://msdn.microsoft.com/en-us/library/system.web.httpapplication.aspx.</summary>
    public enum PipelineStage
    {
        /// <summary>
        ///     Corresponds to the AuthenticateRequest stage of the ASP.NET integrated pipeline.
        /// </summary>
        Authenticate,

        /// <summary>
        ///     Corresponds to the PostAuthenticateRequest stage of the ASP.NET integrated pipeline.
        /// </summary>
        PostAuthenticate,

        /// <summary>
        ///     Corresponds to the AuthorizeRequest stage of the ASP.NET integrated pipeline.
        /// </summary>
        Authorize,

        /// <summary>
        ///     Corresponds to the PostAuthorizeRequest stage of the ASP.NET integrated pipeline.
        /// </summary>
        PostAuthorize,

        /// <summary>
        ///     Corresponds to the ResolveRequestCache stage of the ASP.NET integrated pipeline.
        /// </summary>
        ResolveCache,

        /// <summary>
        ///     Corresponds to the PostResolveRequestCache stage of the ASP.NET integrated pipeline.
        /// </summary>
        PostResolveCache,

        /// <summary>
        ///     Corresponds to the MapRequestHandler stage of the ASP.NET integrated pipeline.
        /// </summary>
        MapHandler,

        /// <summary>
        ///     Corresponds to the PostMapRequestHandler stage of the ASP.NET integrated pipeline.
        /// </summary>
        PostMapHandler,

        /// <summary>
        ///     Corresponds to the AcquireRequestState stage of the ASP.NET integrated pipeline.
        /// </summary>
        AcquireState,

        /// <summary>
        ///     Corresponds to the PostAcquireRequestState stage of the ASP.NET integrated pipeline.
        /// </summary>
        PostAcquireState,

        /// <summary>
        ///     Corresponds to the PreRequestHandlerExecute stage of the ASP.NET integrated pipeline.
        /// </summary>
        PreHandlerExecute,
    }
}
