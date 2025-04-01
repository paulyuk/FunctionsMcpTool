import { app, InvocationContext, output } from "@azure/functions";

export async function mcpToolTrigger(context: InvocationContext): Promise<string> {
    return "Hello I am MCP Tool!";
}

app.mcpTool('mcpToolTriggerTest', {
    toolName: 'McpToolTriggerTest',
    description: 'MCP Tool With Azure Functions Description',
    // return: output.storageBlob({
    //     path: 'output/mcpOutput.txt',
    //     connection: 'blob_connection_for_storage',
    // }),
    handler: mcpToolTrigger
});
