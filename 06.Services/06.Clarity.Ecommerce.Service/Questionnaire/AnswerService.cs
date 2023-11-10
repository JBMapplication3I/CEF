// <copyright file="AnswerService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AnswerService class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Questionnaire/Answers/CreateMany", "POST",
            Summary = "Securely creates many answers, ensures that the user ID on the answers is the current user.")]
    public partial class SecureCreateAnswers : IReturn<CEFActionResponse>
    {
        public List<AnswerModel> Answers { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Questionnaire/CurrentUser/HasAnswersForBrandQuestionnaire", "GET",
            Summary = "Checks if the current user has answered the given brand's questionnaire.")]
    public partial class CheckIfQuestionnaireIsAnswered : IAmFilterableByBrandSearchModel, IReturn<CEFActionResponse<bool>>
    {
        #region IAmFilterableByBrandSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Brand ID For Search, Note: This will be overridden on data calls automatically")]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        public bool? BrandIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Key for objects")]
        public string? BrandKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Name for objects")]
        public string? BrandName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store which uses this category")]
        public int? BrandCategoryID { get; set; }
        #endregion
    }

    public partial class AnswerService
    {
        public async Task<object?> Post(SecureCreateAnswers request)
        {
            return await Workflows.Answers.SecureCreateAnswersAsync(
                    request.Answers
                        .Cast<IAnswerModel>()
                        .ToList(),
                    CurrentUserIDOrThrow401,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(CheckIfQuestionnaireIsAnswered request)
        {
            return await Workflows.Answers.CheckIfUserHasAnsweredBrandQuestionnaireAsync(
                    CurrentUserIDOrThrow401,
                    request.BrandID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
