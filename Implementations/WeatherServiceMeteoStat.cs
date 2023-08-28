using Microsoft.Extensions.Logging;
using System.Text.Json;
using Weather.Helper;
using Weather.Interface;
using Weather.Models;
using Weather.Services;

namespace Weather.Implementations
{
    /// <summary>
    /// Weather service for meteo stat API.
    /// </summary>
    public class WeatherServiceMeteoStat : IWeatherApi
    {
        private readonly ILogger<WeatherServiceMeteoStat> _logger;
        private readonly BaseWebService _baseWebService;

        public WeatherServiceMeteoStat(ILogger<WeatherServiceMeteoStat> logger, BaseWebService baseWebService)
        {
            _logger = logger;
            _baseWebService = baseWebService;
        }
        /// <summary>
        /// Get today weather from weather API service.
        /// </summary>
        /// <returns>type of string</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetTodayWeather()
        {
            try
            {
                _logger.LogInformation("GetTodayWeather start");
                var responses = await _baseWebService.SendAsync<MeteoStatResponse>(Constants.METEO_STAT_API_BASE_URL + "stations/hourly?station=10637&start=2023-08-27&end=2023-08-28&tz=Europe%2FBerlin");
                _logger.LogInformation(JsonSerializer.Serialize(responses));
                return responses.data.FirstOrDefault().temp + " degrees centigrade";
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTodayWeather ex : " + ex.Message);
                throw new NotImplementedException();
            }
        }
    }
}
