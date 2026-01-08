using Calculator_Challenge.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<ICalculator, Calculator>()
    .AddSingleton<INumberParser, NumberParser>()
    .AddSingleton<INumberListValidator, NumberListValidator>()
    .BuildServiceProvider();

var calculator = services.GetRequiredService<ICalculator>();
var parser = services.GetRequiredService<INumberParser>();
var validator = services.GetRequiredService<INumberListValidator>();


// Expect the expression as the first argument
if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- \"3,4\"");
    return;
}

var numbers = parser.Parse(args[0]);

validator.Validate(numbers);

var result = calculator.Add(numbers);
Console.WriteLine($"Result: {result}");