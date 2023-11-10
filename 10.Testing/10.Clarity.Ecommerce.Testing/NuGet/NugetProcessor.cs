// <copyright file="NugetProcessor.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the nuget processor class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Microsoft.Build.Construction;
    using Microsoft.Build.Evaluation;
    using MoreLinq.Extensions;
    using ServiceStack;
    using Testing;
    using Xunit;

    [Trait("Category", "NugetProcessor")]
    public class NugetProcessor : XUnitLogHelper
    {
        public NugetProcessor(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public async Task CreateNuspecFilesForAllProjectsInSolutionAsync()
        {
            var path = Globals.CEFRootPath + "Clarity.Ecommerce.All.sln";
            var solution = SolutionFile.Parse(path);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd");
            var serializer = new XmlSerializer(typeof(Package));
            await solution.ProjectsInOrder.ForEachAsync(
                8,
                async project =>
                {
                    if (project.ProjectType == SolutionProjectType.SolutionFolder
                        || project.ProjectName.EndsWith("Testing")
                        || project.RelativePath.Contains("10.Connect"))
                    {
                        return;
                    }
                    // TestOutputHelper.WriteLine("=== " + project.ProjectName);
                    var projectFile = new Project(project.AbsolutePath);
                    var nuspecObject = new Package();
                    var references = projectFile.GetItems("Reference");
                    foreach (var reference in references.DistinctBy(x => x.EvaluatedInclude))
                    {
                        // TestOutputHelper.WriteLine(reference.EvaluatedInclude);
                        nuspecObject.metadata.frameworkAssemblies.Add(new()
                        {
                            assemblyName = reference.EvaluatedInclude,
                        });
                    }
                    var dependencies = projectFile.GetItems("PackageReference");
                    foreach (var dependency in dependencies.DistinctBy(x => x.EvaluatedInclude))
                    {
                        // TestOutputHelper.WriteLine(dependency.EvaluatedInclude);
                        nuspecObject.metadata.dependencies.Add(new Dependency
                        {
                            id = dependency.EvaluatedInclude,
                            version = $"[{dependency.GetMetadataValue("Version")}]",
                        });
                    }
                    nuspecObject.files.Add(new() { src = "bin\\$configuration$\\$id$.dll" });
                    nuspecObject.files.Add(new() { src = "bin\\$configuration$\\$id$.pdb" });
                    if (projectFile.Properties.Any(x => x.Name == "DocumentationFile"))
                    {
                        nuspecObject.files.Add(new() { src = "bin\\$configuration$\\$id$.xml" });
                    }
                    // TestOutputHelper.WriteLine("===");
                    using var writer = new StringWriterWithEncoding(Encoding.UTF8);
                    serializer.Serialize(writer, nuspecObject, ns);
                    using var fileStream = new FileStream(project.AbsolutePath.Replace("csproj", "nuspec"), FileMode.Create);
                    var buffer = writer.ToString().ToUtf8Bytes();
#if NET5_0_OR_GREATER
                    await fileStream.WriteAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
#else
                    await fileStream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
#endif
                    await fileStream.FlushAsync().ConfigureAwait(false);
                }).ConfigureAwait(false);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task CreatePackagesAsync()
        {
            var solutionPath = Globals.CEFRootPath + "Clarity.Ecommerce.All.sln";
            var solution = SolutionFile.Parse(solutionPath);
            return solution.ProjectsInOrder.ForEachAsync(
                8,
#pragma warning disable 1998
                async project =>
#pragma warning restore 1998
                {
                    if (project.ProjectType == SolutionProjectType.SolutionFolder
                        || project.ProjectName.EndsWith("Testing")
                        || project.RelativePath.Contains("10.Connect"))
                    {
                        return;
                    }
                    var specPath = project.AbsolutePath.Replace("csproj", "nuspec");
                    var processInfo = new ProcessStartInfo(
                        @"C:\Users\jotha\.nuget\.nuget\NuGet.exe",
                        $@"pack {project.AbsolutePath}")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Path.GetDirectoryName(specPath) ?? throw new InvalidOperationException(),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                    };
                    var process = Process.Start(processInfo);
                    process?.WaitForExit();
                });
        }
    }
}
