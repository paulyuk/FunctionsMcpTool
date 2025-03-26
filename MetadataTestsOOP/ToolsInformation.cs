using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Azure.Functions.Worker.Core.FunctionMetadata;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Options;

namespace MetadataTestsOOP;

internal sealed class ToolsInformation
{
    public const string SaveSnippetToolName = "savesnippet";
    public const string SaveSnippetToolDescription = "Saves a code snippet into your snippet collection.";
    public const string GetSnippetToolName = "getsnippets";
    public const string GetSnippetToolDescription = "Gets code snippets from your snippet collection.";
    public const string SnippetNamePropertyName = "snippetname";
    public const string SnippetPropertyName = "snippet";
    public const string SnippetNamePropertyDescription = "The name of the snippet.";
    public const string SnippetPropertyDescription = "The code snippet.";
    public const string PropertyType = "string";
}

public class CustomGenerator(IFunctionMetadataProvider inner, IOptionsSnapshot<ToolOptions> toolOptionsSnapshot)
    : IFunctionMetadataProvider
{
    private static readonly Regex EntryPointRegex = new(@"^(?<typename>.*)\.(?<methodname>\S*)$");

    public async Task<ImmutableArray<IFunctionMetadata>> GetFunctionMetadataAsync(string directory)
    {
        var metadata = await inner.GetFunctionMetadataAsync(directory);

        foreach (var function in metadata)
        {
            if (function.RawBindings is null
                || function.Name is null)
            {
                continue;
            }

            for (int i = 0; i < function.RawBindings.Count; i++)
            {
                var binding = function.RawBindings[i];
                if (!binding.Contains("mcpToolTrigger"))
                {
                    continue;
                }

                var node = JsonNode.Parse(binding);

                if (node is not JsonObject jsonObject)
                {
                    continue;
                }

                if (jsonObject.TryGetPropertyValue("type", out var triggerType)
                    && triggerType?.ToString() == "mcpToolTrigger"
                    && jsonObject.TryGetPropertyValue("toolName", out var toolName)
                    && GetToolProperties(toolName?.ToString(), function, out var toolProperties))
                {
                    jsonObject["toolProperties"] = GetPropertiesJson(function.Name, toolProperties);

                    function.RawBindings[i] = jsonObject.ToJsonString();

                    break;
                }
            }
        }

        return metadata;
    }

    private bool GetToolProperties(string? toolName, IFunctionMetadata functionMetadata, [NotNullWhen(true)] out List<ToolProperty>? toolProperties)
    {
        toolProperties = null;

        // Get from configured options first:
        var toolOptions = toolOptionsSnapshot.Get(toolName);

        if (toolOptions.Properties.Count != 0)
        {
            toolProperties = toolOptions.Properties;
            return true;
        }

        return TryGetPropertiesFromAttributes(functionMetadata, ref toolProperties);
    }

    private static bool TryGetPropertiesFromAttributes(IFunctionMetadata functionMetadata, ref List<ToolProperty>? toolProperties)
    {
        // Fallback to attributes:
        var match = EntryPointRegex.Match(functionMetadata.EntryPoint ?? string.Empty);
                    
        if (!match.Success)
        {
            return false;
        }

        var typeName = match.Groups["typename"].Value;
        var methodName = match.Groups["methodname"].Value;

        var type = Type.GetType(typeName);
        if (type is null)
        {
            return false;
        }

        var method = type.GetMethod(methodName);

        var properties = new List<ToolProperty>();
        foreach (var parameter in method.GetParameters())
        {
            var toolAttribute = parameter.GetCustomAttribute<McpToolPropertyAttribute>();

            if (toolAttribute is null)
            {
                continue;
            }

            properties.Add(new ToolProperty(toolAttribute.PropertyName, toolAttribute.PropertyType, toolAttribute.Description));
        }

        toolProperties = properties;
        return true;
    }

    private static JsonNode? GetPropertiesJson(string functionName, List<ToolProperty> properties)
    {
        return JsonSerializer.Serialize(properties);
    }

   
}

public class ToolOptions
{
    public void AddProperty(string name, string type, string description)
    {
        Properties.Add(new ToolProperty(name, type, description));
    }

    public required List<ToolProperty> Properties { get; set; } = [];
}

public class ToolProperty(string name, string type, string? description)
{
    [JsonPropertyName("propertyName")]
    public string Name { get; set; } = name;

    [JsonPropertyName("propertyType")]
    public string Type { get; set; } = type;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = description;
}
