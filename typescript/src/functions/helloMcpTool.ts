import { app, InvocationContext, output } from "@azure/functions";

export async function mcpToolHello(context: InvocationContext): Promise<string> {
    return "Hello I am MCP Tool!";
}

app.mcpTool('hello', {
    toolName: 'hello',
    description: 'Simple hello world MCP Tool that responses with a hello message.',
    handler: mcpToolHello
});

// TODO: Swapnil could you please implement these to match logic in dotnet/SnippetTool.cs?
// app.mcpTool('save_snippet', {
//     toolName: 'save_snippet',
//     description: 'MCP Tool that saves code snippet from user to blob storage',
//     return: output.storageBlob({
//         path: 'snippets/mcpOutput.txt',
//         connection: 'AzureWebJobsStorage',
//     }),
//     handler: mcpToolTrigger
// });

// app.mcpTool('get_snippet', {
//     toolName: 'get_snippet',
//     description: 'MCP Tool that retreives code snippet from blob storage by name',
//     handler: mcpToolTrigger
// });
