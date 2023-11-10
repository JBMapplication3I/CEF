// <copyright file="UpdateTranslationsFiles.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the update translations files runner class</summary>
namespace Clarity.Ecommerce.Tasks.UpdateTranslationsFiles.Testing
{
    using System.Threading.Tasks;
    using Mapper;
    using Xunit;

    [Trait("Category", "ScheduledTasks.UpdateTranslationsFiles")]
    public class UpdateTranslationsFilesTaskRunner
    {
        [Fact(Skip = "Only run when you need it")]
        public Task RunUpdateTranslationsFiles()
        {
            JSConfigs.CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            return new UpdateTranslationsFilesTask().ProcessAsync(null);
        }
    }
}
