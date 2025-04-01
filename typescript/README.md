# Azure Functions Node.js/TypeScript MCP Trigger using Azure Developer CLI

This README provides instructions on how to leverage the experimental feature for the MCP Tool with Azure Functions.

## Steps to Get Started

### 1. Fork and Clone the Repository
Fork the folder in the repository `FunctionsMcpTool`.

Then change to the `typescript` folder in a new terminal window
```shell
cd typescript
```

### 2. Install Required Extensions
Navigate to the forked folder and execute the following command to install the required extensions for MCP Tool support:
```bash
func extensions install
```

### 3. Install Dependencies
Run the following command to install the necessary dependencies. Note that `@azure/functions` is changed to support the feature version `4.7.0-beta.0-alpha.20250331.2`.
```bash
npm install
```

### 4. Build the Project
Run the following command to build the project:
```bash
npm run build
```

### 5. Start Azurite

An Azure Storage Emulator is needed for this particular sample because we will save and get snippets from blob storage. 

```shell
docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 \
    mcr.microsoft.com/azure-storage/azurite
```

>**Note** if you use Azurite coming from VS Code extension you need to run `Azurite: Start` now or you will see errors.

### 6. Start the Azure Functions
Run the following command to start the Azure Functions:
```bash
func start
```

Note by default this will use the webhooks route: `/runtime/webhooks/mcp/sse`.  Later we will use this in Azure to set the key on client/host calls: `/runtime/webhooks/mcp/sse?code=<system_key>`

### 6. Debug the Code
Debug the code in debug mode using your preferred development environment.

## Use the MCP server from within a client/host

### VS Code - Copilot Edits

1. **Add MCP Server** from command palette and add URL to your running Function app's SSE endpoint:
    ```shell
    http://0.0.0.0:7071/runtime/webhooks/mcp/sse
    ```
1. **List MCP Servers** from command palette and start the server
1. In Copilot chat agent mode enter a prompt to trigger the tool, e.g., select some code and enter this prompt

    ```plaintext
    Say Hello
    ```

    ```plaintext
    // NYI - but ETA is today
    Save this snippet as snippet1 
    ```

    ```plaintext
    // NYI - but ETA is today
    Retrieve snippet1 and apply to newFile.ts
    ```
1. When prompted to run the tool, consent by clicking **Continue**

1. When you're done, press Ctrl+C in the terminal window to stop the `func.exe` host process.

### MCP Inspector

1. In a **new terminal window**, install and run MCP Inspector

    ```shell
    npx @modelcontextprotocol/inspector node build/index.js
    ```

1. CTRL click to load the MCP Inspector web app from the URL displayed by the app (e.g. http://0.0.0.0:5173/#resources)
1. Set the transport type to `SSE` 
1. Set the URL to your running Function app's SSE endpoint and **Connect**:
    ```shell
    http://0.0.0.0:7071/runtime/webhooks/mcp/sse
    ```
1. **List Tools**.  Click on a tool and **Run Tool**.  

## Deploy to Azure

Run this [azd](https://aka.ms/azd) command to provision the function app, with any required Azure resources, and deploy your code:

```shell
azd up
```

>**Using key based auth**
> This function requires a system key by default which can be obtained from the [portal](https://learn.microsoft.com/en-us/azure/azure-functions/function-keys-how-to?tabs=azure-portal), and then update the URL in your host/client to be:
> `https://<funcappname>.azurewebsites.net/runtime/webhooks/mcp/sse?code=<systemkey_for_mcp_extension>`
> via command line you can call `az functionapp keys list --resource-group <resource_group> --name <function_app_name>`
> Additionally, [EasyAuth](https://learn.microsoft.com/en-us/azure/app-service/overview-authentication-authorization) can be used to set up your favorite OAuth provider including Entra.  

You can opt-in to a VNet being used in the sample. To do so, do this before `azd up`

```bash
azd env set VNET_ENABLED true
```
After publish completes successfully, `azd` provides you with the URL endpoints of your new functions, but without the function key values required to access the endpoints. To obtain these same endpoints along with the **required function keys**, see [Invoke the function on Azure](https://learn.microsoft.com/azure/azure-functions/create-first-function-azure-developer-cli?pivots=programming-language-dotnet#invoke-the-function-on-azure)

## Redeploy your code

You can run the `azd up` command as many times as you need to both provision your Azure resources and deploy code updates to your function app.

>[!NOTE]
>Deployed code files are always overwritten by the latest deployment package.

## Clean up resources

When you're done working with your function app and related resources, you can use this command to delete the function app and its related resources from Azure and avoid incurring any further costs:

```shell
azd down
```

