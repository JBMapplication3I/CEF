// <copyright file="ISurveyProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISurveyProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Surveys
{
    using System.Threading.Tasks;
    using Models;
    using Providers;

    /// <summary>Interface for survey provider base.</summary>
    public interface ISurveyProviderBase : IProviderBase
    {
        /// <summary>Creates event survey.</summary>
        /// <param name="evt">               The event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task CreateEventSurveyAsync(ICalendarEventModel evt, string? contextProfileName);
    }
}
