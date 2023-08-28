using Microsoft.Extensions.Logging;
using System.Text.Json;
using Weather.Helper;
using Weather.Interface;
using Weather.Models;
using Weather.Services;

namespace Weather.Implementations
{
    /// <summary>
    /// Weather service implementation for weather API.
    /// </summary>
    public class WeatherServiceWeatherApi : IWeatherApi
    {
        private readonly ILogger<WeatherServiceWeatherApi> _logger;
        private readonly BaseWebService _baseWebService;

        /// <summary>
        /// Constructor of weather service weather API.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="baseWebService"></param>
        public WeatherServiceWeatherApi(ILogger<WeatherServiceWeatherApi> logger,
            BaseWebService baseWebService)
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
                var response = await _baseWebService.GetAsync<WeatherApiResponse>(Constants.WEATHER_API_BASE_URL + "v1/current.json?key=" + Constants.WEATHER_API_TOKEN + "&q=London&aqi=yes");
                _logger.LogInformation(JsonSerializer.Serialize(response));
                return response.current.temp_c.ToString() + " degrees centigrade";
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTodayWeather ex : " + ex.Message);
                throw new NotImplementedException();
            }
        }
    }
}
