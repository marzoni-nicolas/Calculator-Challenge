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
│   ├── Program.cs                    # Application entry point with DI setup
├── Calculator-Challenge.Tests/        # Unit test project
│   ├── CalculatorTests.cs            # Comprehensive test suite using xUnit and FluentAssertions
```

## Solution Summary

### Key Features

- **String Calculator**: Parses delimited (comma or newline separated) numeric strings and returns the sum
- **Input Handling**: 
  - Handles empty strings (returns 0)
  - Handles invalid numbers (treats as 0)
  - Supports negative numbers
  - Supports arbitrary number of inputs
- **Architecture**: 
  - Interface-based design (`ICalculator`)
  - Dependency injection using Microsoft.Extensions.DependencyInjection
- **Testing**: 
  - Unit tests with xUnit
  - Fluent assertions for readable test expectations
  - Multiple test cases covering edge cases

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