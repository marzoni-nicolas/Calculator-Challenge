using Calculator_Challenge.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<ICalculator, Calculator>()
    .AddSingleton<INumberParser, NumberParser>()
    .AddSingleton<INumberListValidator, NumberListValidator>()
    .AddSingleton<IApplicationService, ApplicationService>()
    .BuildServiceProvider();

var app = services.GetRequiredService<IApplicationService>();

// Expect the expression as the first argument
if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run -- \"3,4\"");
    return;
}

var result = app.Calculate(args[0]);

Console.WriteLine($"Result: {result}");