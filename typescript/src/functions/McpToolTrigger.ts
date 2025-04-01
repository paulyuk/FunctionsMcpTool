import { app, InvocationContext, output } from "@azure/functions";

export async function mcpToolHello(context: InvocationContext): Promise<string> {
    return "Hello I am MCP Tool!";
}

app.mcpTool('hello', {
    toolName: 'hello',
    description: 'Simple hello world MCP Tool',
    handler: mcpToolHello
});


// app.mcpTool('save-snippet', {
//     toolName: 'save-snippet',
//     description: 'MCP Tool that saves code snippet from user to blob storage',
//     return: output.storageBlob({
//         path: 'snippets/mcpOutput.txt',
//         connection: 'AzureWebJobsStorage',
//     }),
//     handler: mcpToolTrigger
// });

// app.mcpTool('save-snippet', {
//     toolName: 'save-snippet',
//     description: 'MCP Tool that saves code snippet from user to blob storage',
//     handler: mcpToolTrigger
// });
