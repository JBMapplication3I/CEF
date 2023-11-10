// <copyright file="IntegratedPipelineExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the integrated pipeline extensions class</summary>
namespace Microsoft.Owin.Extensions
{
    using System;

    /// <summary>Extension methods used to indicate at which stage in the integrated pipeline prior middleware should
    /// run.</summary>
    public static class IntegratedPipelineExtensions
    {
        /// <summary>The integrated pipeline stage marker.</summary>
        private const string IntegratedPipelineStageMarker = "integratedpipeline.StageMarker";

        /// <summary>Call after other middleware to specify when they should run in the integrated pipeline.</summary>
        /// <param name="app">      The IAppBuilder.</param>
        /// <param name="stageName">The name of the integrated pipeline in which to run.</param>
        /// <returns>The original IAppBuilder for chaining.</returns>
        public static global::Owin.IAppBuilder UseStageMarker(this global::Owin.IAppBuilder app, string stageName)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (app.Properties.TryGetValue("integratedpipeline.StageMarker", out var obj))
            {
                ((Action<global::Owin.IAppBuilder, string>)obj)(app, stageName);
            }
            return app;
        }

        /// <summary>Call after other middleware to specify when they should run in the integrated pipeline.</summary>
        /// <param name="app">  The IAppBuilder.</param>
        /// <param name="stage">The stage of the integrated pipeline in which to run.</param>
        /// <returns>The original IAppBuilder for chaining.</returns>
        public static global::Owin.IAppBuilder UseStageMarker(this global::Owin.IAppBuilder app, global::Owin.PipelineStage stage)
        {
            return app.UseStageMarker(stage.ToString());
        }
    }
}
