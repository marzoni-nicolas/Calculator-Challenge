# Calculator Challenge

A .NET console application that implements a string calculator as part of a coding challenge for Nimble's .NET Engineer position.

## Overview

This project demonstrates a simple calculator implementation that processes comma-separated numeric strings and returns their sum. The solution emphasizes clean architecture, dependency injection, and comprehensive unit testing.

## Project Structure

```
Calculator-Challenge/
├── Calculator-Challenge/              # Main console application
│   ├── Services/
│   │   └── Calculator.cs             # Core calculator logic with ICalculator interface
│   │   └── ApplicationService.cs     # Orchestrate the calculator workflow. Parse, Validate and Calculate. 
│   │   └── NumberParser.cs           # Parses the input into numbers.
│   │   └── NumberListValidator.cs    # Validates the list of number.
│   ├── Program.cs                    # Application entry point with DI setup
├── Calculator-Challenge.Tests/        # Unit test project. Comprehensive test suite using xUnit and FluentAssertions.
```

## Solution Summary

### Key Features

- **String Calculator**: Parses delimited numeric strings and returns the sum. Supported delimiters:
  - Comma (,)
  - New Line (\n)
  - Custom single-char delimiter (//,\n)
  - Custom multi-char delimiter (//[***]\n)
  - Multiple custom multi-char delimiter (//[***][#]\n)
- **Input Handling**: 
  - Handles empty strings (returns 0)
  - Handles invalid numbers (treats as 0)
  - Handles numbers higher than 1000 as an invalid numbers (treats as 0)
  - Do not allow negative numbers
  - Supports arbitrary number of inputs
- **Architecture**: 
  - Interface-based design (`ICalculator`)
  - Dependency injection using Microsoft.Extensions.DependencyInjection
- **Testing**: 
  - Unit tests with xUnit
  - Fluent assertions for readable test expectations
  - Multiple test cases covering edge cases
- **Custom Calculator Options supported**:
  - CLI options
  ```json
	dotnet run -- \
	Calculator:MaxAllowedValue=500 \
	Calculator:DenyNegativeNumbers=false \
	Calculator:AlternateDelimiter=;
  ```
  - appsetting.json options
- **Execution loop**:
  - The app will continue running and sking for numbers until Ctrl+C is pressed.

### Technical Stack

- **.NET 10.0**: Latest .NET framework
- **Microsoft.Extensions.DependencyInjection**: Built-in dependency injection
- **xUnit**: Testing framework
- **FluentAssertions**: Expressive assertion library

## Usage

### Running the Application

```powershell
dotnet run --project Calculator-Challenge -- "3,4"
# Output: Result: 7
```

### Running Tests

```powershell
dotnet test
```

### Example Inputs

| Input | Output |
|-------|--------|
| `"20"` | 20 |
| `"1,5000"` | 5001 |
| `"4,-3"` | 1 |
| `""` | 0 |
| `","` | 0 |
| `"5,tytyt"` | 5 |
| `"1,2,3,4,5,6,7,8,9,10,11,12"` | 78 |
| `"1\n2\n3"` | 6 |
| `"//*\n1*2*3"` | 6 |
| `"//[***]\n1***2***3"` | 6 |
| `"//[***][#]\n11***22#33"` | 6|

## Improvements
- Validate or clean repeated delimiters.
- Validate that at least one delimiter exists when header (//) exists.
- If necessary, consider adding nuget for better CLI argument support.
- Define IOperator or similar for Substraction, Multiplication and Division.
- Add Agents.MD for better Coding Agents assistance.