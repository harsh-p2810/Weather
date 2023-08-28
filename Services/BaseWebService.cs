using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using Weather.Helper;

namespace Weather.Services
{
    /// <summary>
    /// Base web service
    /// </summary>
    public class BaseWebService
    {
        HttpClient _client;

        JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Constructor for base web service
        /// </summary>
        public BaseWebService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        /// <summary>
        /// Get async API method from URL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        /// <summary>
        /// send async method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(string url)
        {
            try
            {
             
                Uri uri = new Uri(url);
                
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                request.Headers.Add("x-rapidapi-host", "meteostat.p.rapidapi.com");
                request.Headers.Add("x-rapidapi-key", Constants.METEO_STAT_API_TOKEN);
                
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }
    }
}
