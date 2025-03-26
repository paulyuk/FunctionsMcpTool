using MetadataTestsOOP;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.Functions.Worker.Builder;

public static class HostBuilderExtensions
{
    public static McpToolBuilder ConfigureMcpTool(this IFunctionsWorkerApplicationBuilder builder, string toolName)
    {
        
        return new McpToolBuilder(builder, toolName);
    }
}

public sealed class McpToolBuilder(IFunctionsWorkerApplicationBuilder builder, string toolName)
{
    public McpToolBuilder WithProperty(string name, string type, string description)
    {
        builder.Services.Configure<ToolOptions>(toolName, o => o.AddProperty(name, type, description));

        return this;
    }
}