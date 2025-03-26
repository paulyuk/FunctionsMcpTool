using MetadataTestsOOP;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.Core.FunctionMetadata;
using Microsoft.Extensions.Hosting;
using static MetadataTestsOOP.ToolsInformation;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.Decorate<IFunctionMetadataProvider, CustomGenerator>();

builder.ConfigureMcpTool(GetSnippetToolName)
    .WithProperty(SnippetNamePropertyName, PropertyType, GetSnippetToolDescription);

//builder.ConfigureMcpTool(SaveSnippetToolName)
//    .WithProperty(SnippetNamePropertyName, PropertyType, GetSnippetToolDescription)
//    .WithProperty(SnippetPropertyName, PropertyType, SnippetPropertyDescription);

builder.Build().Run();




