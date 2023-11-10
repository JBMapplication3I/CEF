// <copyright file="TypescriptCodeGenerator.Service.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typescript code generator. service class</summary>
namespace ServiceStack.CodeGenerator.TypeScript
{
    using System.CodeDom.Compiler;
    using System.IO;

    public partial class TypescriptCodeGenerator
    {
        private void WriteServiceClassForReact(IndentedTextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("/** Exposes access to the ServiceStack routes */");
            foreach (var folder in serviceStackRoutes)
            {
                writer.WriteLine($"import {{ {folder.Key} }} from \"./cvApi.{folder.Key}\";");
            }
            writer.WriteLine();
            writer.WriteLine("export class CEFAPI {");
            writer.Indent++;
            foreach (var folder in serviceStackRoutes)
            {
                writer.Write(folder.Key.ToCamelCase());
                writer.Write(": ");
                writer.Write(folder.Key);
                writer.WriteLine(";");
            }
            writer.WriteLine();
            writer.WriteLine("constructor() {");
            writer.Indent++;
            foreach (var folder in serviceStackRoutes)
            {
                writer.Write("this.");
                writer.Write(folder.Key.ToCamelCase());
                writer.Write(" = new ");
                writer.Write(folder.Key);
                writer.WriteLine("();");
            }
            writer.Indent--;
            writer.WriteLine("}");
            writer.Indent--;
            writer.WriteLine("}");
        }

        private void WriteServiceInterface(IndentedTextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine("/** Exposes access to the ServiceStack routes */");
            writer.Write("export interface I");
            writer.Write(serviceName);
            writer.Write(" {");
            writer.WriteLine();
            writer.Indent++;
            writer.WriteLine("$http: ng.IHttpService;");
            writer.WriteLine("rootUrl: string;");
            writer.WriteLine("// Routes");
            foreach (var folder in serviceStackRoutes)
            {
                writer.Write(folder.Key.ToCamelCase());
                writer.Write(": ");
                writer.Write(folder.Key);
                writer.Write(";");
                writer.WriteLine();
            }
            writer.Indent--;
            writer.WriteLine("}");
        }

        private void WriteServiceClass(IndentedTextWriter mainWriter)
        {
            mainWriter.WriteLine();
            mainWriter.WriteLine("/** Exposes access to the ServiceStack routes */");
            mainWriter.Write("export class ");
            mainWriter.Write(serviceName);
            mainWriter.Write(" implements I");
            mainWriter.Write(serviceName);
            mainWriter.Write(" {");
            mainWriter.WriteLine();
            mainWriter.Indent++;
            mainWriter.WriteLine("authentication2: Authentication2;");
            using var constructorWriter = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 1 };
            constructorWriter.WriteLine();
            constructorWriter.Indent++;
            constructorWriter.WriteLine("constructor($http: ng.IHttpService, rootUrl: string) {");
            constructorWriter.Indent++;
            constructorWriter.WriteLine("this._$http = $http;");
            constructorWriter.WriteLine("this.rootUrl = rootUrl;");
            constructorWriter.WriteLine("this.authentication2 = new Authentication2(this);");
            foreach (var routeRoot in serviceStackRoutes)
            {
                constructorWriter.Write("this.");
                constructorWriter.Write(routeRoot.Key.ToCamelCase());
                constructorWriter.Write(" = new ");
                constructorWriter.Write(routeRoot.Key);
                constructorWriter.Write("(this);");
                constructorWriter.WriteLine();
                // This route property on our service
                // someRoute: ISomeRoute;
                mainWriter.Write(routeRoot.Key.ToCamelCase());
                mainWriter.Write(" : ");
                mainWriter.Write(routeRoot.Key);
                mainWriter.Write(";");
                mainWriter.WriteLine();
            }
            constructorWriter.Indent--;
            constructorWriter.WriteLine("}");
            mainWriter.WriteLine(constructorWriter.InnerWriter);
            mainWriter.WriteLine("private _$http: ng.IHttpService;");
            mainWriter.WriteLine("get $http(): ng.IHttpService { return this._$http; }");
            mainWriter.WriteLine("private _rootUrl: string;");
            mainWriter.WriteLine("set rootUrl(value) { this._rootUrl = value; }");
            mainWriter.WriteLine("get rootUrl():string {");
            mainWriter.Indent++;
            mainWriter.WriteLine("// Remove trailing slash from URL if present");
            mainWriter.WriteLine("return this._rootUrl.substr(-1) !== \"/\"");
            mainWriter.Indent++;
            mainWriter.WriteLine("? this._rootUrl");
            mainWriter.WriteLine(": this._rootUrl.substr(0, this._rootUrl.length - 1);");
            mainWriter.Indent--;
            mainWriter.Indent--;
            mainWriter.WriteLine("}");
            // Close the class
            mainWriter.Indent--;
            mainWriter.WriteLine("}");
        }
    }
}
