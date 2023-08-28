namespace Weather.Interface
{
    /// <summary>
    /// Interface for weather API
    /// </summary>
    public interface IWeatherApi
    {
        /// <summary>
        /// Method to get today weather
        /// </summary>
        /// <returns>type of string</returns>
        public Task<string> GetTodayWeather();
    }
}
