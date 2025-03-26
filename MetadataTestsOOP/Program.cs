using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using static FunctionsSnippetTool.ToolsInformation;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.EnableMcpToolMetadata();
builder.ConfigureMcpTool(GetSnippetToolName)
    .WithProperty(SnippetNamePropertyName, PropertyType, SnippetNamePropertyDescription);

builder.Build().Run();