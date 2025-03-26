using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using static MetadataTestsOOP.ToolsInformation;

namespace MetadataTestsOOP;
public class SnippetsTool(ILogger<SnippetsTool> logger)
{
    private const string BlobPath = "snippets/{mcptoolargs." + SnippetNamePropertyName + "}.json";

    [Function(nameof(GetSnippet))]
    public object GetSnippet(
        [McpToolTrigger(GetSnippetToolName, GetSnippetToolDescription)] string context,
        [McpToolProperty(SnippetNamePropertyName, PropertyType, SnippetNamePropertyDescription)] string name,
        [BlobInput(BlobPath)] string snippetContent)
    {
        return snippetContent;
    }

    [Function(nameof(SaveSnippet))]
    [BlobOutput(BlobPath)]
    public string SaveSnippet(
        [McpToolTrigger(SaveSnippetToolName, SaveSnippetToolDescription)] ToolInvocationContext context,
        [McpToolProperty(SnippetNamePropertyName, PropertyType, SnippetNamePropertyDescription)] string name,
        [McpToolProperty(SnippetPropertyName, PropertyType, SnippetPropertyDescription)] string snippet)
    {
        return snippet;
    }
}