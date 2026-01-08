using Calculator_Challenge.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<ICalculator, Calculator>()
    .BuildServiceProvider();

var calculator = services.GetRequiredService<ICalculator>();

// Expect the expression as the first argument
if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- \"3,4\"");
    return;
}

var result = calculator.Add(args[0]);
Console.WriteLine($"Result: {result}");