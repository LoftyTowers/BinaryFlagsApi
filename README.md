# 🛡️ BinaryFlagsApi

A modular, test-driven fraud rules engine built in .NET 9.0. It processes various payment types (e.g. immediate, future, standing orders) using a binary flag-based rule execution system. Rules are configurable via `appsettings.json`, and new rules can be added without modifying core logic.

---

## 📦 Features

- ✅ Binary `[Flags]` enum for rule activation
- ⚙️ Polymorphic deserialization of `PaymentDto` using custom JSON converters
- 🔁 Rule configuration driven from `appsettings.json`
- 🧪 xUnit test coverage across DTOs, logic, and controllers
- 📊 Logging via `Serilog`
- 🔍 Swagger for API exploration
- 💡 Extensible rule system (`Rule1` through `Rule10`)

---

## 🗂️ Project Structure

```
BinaryFlagsApi/
├── src/
│   ├── Core                    # Shared enums, models, DTOs
│   ├── BinaryFlagRulesService # Rule logic + engine
│   └── BinaryFlagsApi         # Main API with Swagger + Controllers
├── tests/
│   ├── Core.Tests
│   ├── BinaryFlagRulesService.Tests
│   └── BinaryFlagsApi.Tests
```

---

## 🧪 Run Tests

```bash
dotnet test
```

Covers:

- ✅ Rule execution (`Rule1`–`Rule10`)
- ✅ FraudRuleEngine logic
- ✅ Payment polymorphism + JSON handling
- ✅ Controller integration
- ✅ Enum extension and flag behavior

---

## 🔧 Configuration

Defined in `appsettings.json`:

```json
"FraudRules": {
  "ImmediatePayment": "Rule1,Rule4,Rule5",
  "FuturePayment": "Rule2,Rule3,Rule6",
  "StandardOrder": "Rule7,Rule8,Rule9,Rule10"
}
```

Each entry maps a payment type to a bitmask combination of rule flags. The `PaymentRuleFactory` uses this to inject `RulesToRun` into the DTO before execution.

---

## ▶️ Run Locally

```bash
dotnet run --project src/BinaryFlagsApi/BinaryFlagsApi.csproj
```

Then visit:

```
https://localhost:5001/swagger
```

---

## 🧩 Extending

To add a new fraud rule:

1. Create a new class `Rule11.cs` implementing `BaseFraudRule<Rule11>`
2. Define a new flag in `FraudRuleFlags`
3. Add to `appsettings.json` where relevant
4. Write a unit test in `RuleTests.cs`

---

## 📜 License

MIT — free to use and modify. Contributions welcome.

---

## 🙌 Credits

- Built with [.NET 9](https://dotnet.microsoft.com/)
- Testing with [xUnit](https://xunit.net/), [Moq](https://github.com/moq/moq4), [FluentAssertions](https://fluentassertions.com/)
- API documentation via [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
