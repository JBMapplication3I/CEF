// <copyright file="SurveyProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the survey provider base class</summary>
namespace Clarity.Ecommerce.Providers.Surveys
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Surveys;

    /// <summary>A survey provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISurveyProviderBase"/>
    public abstract class SurveyProviderBase : ProviderBase, ISurveyProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Survey;

        /// <inheritdoc/>
        public abstract Task CreateEventSurveyAsync(ICalendarEventModel evt, string contextProfileName);
    }
}
