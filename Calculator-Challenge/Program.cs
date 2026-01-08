using Calculator_Challenge.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<ICalculator, Calculator>()
    .AddSingleton<INumberParser, NumberParser>()
    .BuildServiceProvider();

var calculator = services.GetRequiredService<ICalculator>();
var parser = services.GetRequiredService<INumberParser>();


// Expect the expression as the first argument
if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- \"3,4\"");
    return;
}

var numbers = parser.Parse(args[0]);

var result = calculator.Add(numbers);
Console.WriteLine($"Result: {result}");