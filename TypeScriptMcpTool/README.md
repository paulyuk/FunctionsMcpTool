# MCP Tool with Azure Functions

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

### 4. Debug the Code
Debug the code in debug mode using your preferred development environment.

## Verification

### Validate the MCP Tool
Use the MCP Tool Inspector to validate the MCP Tool.

1. Run the MCP Inspector using the following command:
```bash
MCpInspector
```

2. Connect to the endpoint:
```
http://localhost:7071/api/sse
```

3. Click on the list of tools and test by clicking the connect button.

Follow these steps to ensure that the MCP Tool is properly set up and functioning with Azure Functions.
