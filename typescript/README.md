# Azure Functions Node.js/TypeScript MCP Trigger using Azure Developer CLI

This README provides instructions on how to leverage the experimental feature for the MCP Tool with Azure Functions.

## Steps to Get Started

### 1. Fork the Repository
Fork the folder in the repository `TypescriptMcpTool`.

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

### 5. Start the Azure Functions
Run the following command to start the Azure Functions:
```bash
func start
```

### 6. Debug the Code
Debug the code in debug mode using your preferred development environment.

## Verification

### Validate the MCP Tool
Use the MCP Tool Inspector to validate the MCP Tool.

1. Run the MCP Inspector using the following command:
```bash
npx @modelcontextprotocol/inspector node build/index.js
```

2. Open the following URL in your browser:
```
http://localhost:5173/
```

3. Connect to the endpoint:
```
http://localhost:7071/api/sse
```

4. Click on the list of tools and test by clicking the connect button.

Follow these steps to ensure that the MCP Tool is properly set up and functioning with Azure Functions.