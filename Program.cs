using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Weather.Implementations;
using Weather.Interface;
using Weather.Services;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
         .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
         .CreateLogger();

        var services = new ServiceCollection()
            .AddLogging(l => l.AddSerilog())
            .AddScoped<IWeatherApi, WeatherServiceMeteoStat>()
            .AddScoped<IWeatherApi, WeatherServiceWeatherApi>()
            .AddSingleton<BaseWebService>()
            .BuildServiceProvider();

        var weatherAPIService = services.GetServices<IWeatherApi>().FirstOrDefault(x => x.GetType() == typeof(WeatherServiceWeatherApi));
        var meteorAPIService = services.GetServices<IWeatherApi>().FirstOrDefault(x => x.GetType() == typeof(WeatherServiceMeteoStat));

        Console.WriteLine("Current temperature (weatherapi) - " + await weatherAPIService.GetTodayWeather());
        Console.WriteLine("Current temperature (meteostat) - " + await meteorAPIService.GetTodayWeather());
        Console.ReadLine();
    }
}
