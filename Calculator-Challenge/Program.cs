using Calculator_Challenge.Options;
using Calculator_Challenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddCommandLine(args)
    .Build();

var options = new CalculatorOptions();
configuration.GetSection("Calculator").Bind(options);

var services = new ServiceCollection()
    .AddSingleton<ICalculatorOptions>(options)
    .AddSingleton<ICalculator, Calculator>()
    .AddSingleton<INumberParser, NumberParser>()
    .AddSingleton<INumberListValidator, NumberListValidator>()
    .AddSingleton<IApplicationService, ApplicationService>()
    .BuildServiceProvider();

var app = services.GetRequiredService<IApplicationService>();


Console.WriteLine("Calculator started. Press Ctrl+C to exit.");

while (true)
{
    Console.Write("Enter the input string:");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    var result = app.Calculate(input);

    Console.WriteLine($"{string.Join(result.separator, result.numbers)}={result.total}");
}