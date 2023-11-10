// <copyright file="NotificationResultState.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the notification result state class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    /// <summary>Values that represent notification result states.</summary>
    public enum NotificationResultState
    {
        /// <summary>
        ///     Continue with normal processing.
        /// </summary>
        Continue,

        /// <summary>
        ///     Discontinue processing the request in the current middleware and pass control to the next one.
        /// </summary>
        Skipped,

        /// <summary>
        ///     Discontinue all processing for this request.
        /// </summary>
        HandledResponse,
    }
}
