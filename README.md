# Binary Flags API

## Overview
The Binary Flags API is a .NET 9 application designed to manage and evaluate rules using binary flags. This API provides endpoints to process payments and evaluate fraud rules using a binary flag system.

## Project Structure
```
BinaryFlagsApi
├── BinaryFlagsApi.sln
├── src
│   ├── BinaryFlagsApi
│   │   ├── Controllers
│   │   │   └── PaymentsController.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   ├── Core
│   │   ├── DTO's
│   │   │   ├── PaymentDto.cs
│   │   │   ├── FuturePaymentDto.cs
│   │   │   └── ImmediatePaymentDto.cs
│   │   ├── Enums
│   │   │   └── FraudRuleFlags.cs
│   ├── BinaryFlagRulesService
│   │   ├── FraudRuleEngine.cs
│   │   └── Rules
│   │       ├── BaseFraudRule.cs
│   │       ├── Rule1.cs
│   │       ├── Rule2.cs
│   │       └── Rule10.cs
├── tests
│   ├── BinaryFlagsApi.Tests
│   │   ├── FraudRuleEngineTests.cs
│   │   └── TestHelpers
│   │       └── MockData.cs
├── .editorconfig
├── .gitignore
└── README.md
```

## Setup Instructions

### 1. Clone the Repository
```bash
git clone <repository-url>
cd BinaryFlagsApi
```

### 2. Restore Dependencies
Run the following command to restore the required NuGet packages:
```bash
dotnet restore
```

### 3. Run the Application
To start the API, use the following command:
```bash
dotnet run --project src/BinaryFlagsApi/BinaryFlagsApi.csproj
```

### 4. Access Swagger UI
Once the application is running, you can access the Swagger UI at:
```
http://localhost:5000/swagger
```

## Logging
This project uses Serilog for logging. Logs are written to both the console and a rolling file (`logs/log-.txt`). Ensure that the logging configuration is set up in `appsettings.json` to capture logs as needed.

## Testing
Unit tests are located in the `tests/BinaryFlagsApi.Tests` directory. To run the tests, use the following command:
```bash
dotnet test
```

## Key Features
- **Binary Flag System**: Uses the `FraudRuleFlags` enum to represent rules as binary flags.
- **Polymorphic Payment Processing**: Accepts abstract `PaymentDto` objects and processes derived types like `FuturePaymentDto` and `ImmediatePaymentDto`.
- **Fraud Rule Engine**: Evaluates fraud rules using the `FraudRuleEngine` and custom rule classes.
- **Swagger Integration**: Provides an interactive API documentation and testing interface.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
