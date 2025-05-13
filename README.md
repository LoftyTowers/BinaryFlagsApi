# Binary Flags API

## Overview
The Binary Flags API is a .NET 9 application designed to manage and evaluate rules using binary flags. This API provides endpoints to create, retrieve, and delete rules, as well as evaluate them against specified conditions.

## Project Structure
```
BinaryFlagsApi
├── BinaryFlagsApi.sln
├── src
│   ├── BinaryFlagsApi
│   │   ├── Controllers
│   │   │   └── RulesController.cs
│   │   ├── DTOs
│   │   │   └── RuleDto.cs
│   │   ├── Rules
│   │   │   ├── RuleEngine.cs
│   │   │   └── SampleRule.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Startup.cs
├── tests
│   ├── BinaryFlagsApi.Tests
│   │   ├── RuleEngineTests.cs
│   │   └── TestHelpers
│   │       └── MockData.cs
├── .editorconfig
├── .gitignore
└── README.md
```

## Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd BinaryFlagsApi
   ```

2. **Restore Dependencies**
   Run the following command to restore the required NuGet packages:
   ```bash
   dotnet restore
   ```

3. **Run the Application**
   To start the API, use the following command:
   ```bash
   dotnet run --project src/BinaryFlagsApi/BinaryFlagsApi.csproj
   ```

4. **Access Swagger UI**
   Once the application is running, you can access the Swagger UI at:
   ```
   http://localhost:5000/swagger
   ```

## Logging
This project uses Serilog for logging. Ensure that the logging configuration is set up in `appsettings.json` to capture logs as needed.

## Testing
Unit tests are located in the `tests/BinaryFlagsApi.Tests` directory. To run the tests, use the following command:
```bash
dotnet test
```

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.