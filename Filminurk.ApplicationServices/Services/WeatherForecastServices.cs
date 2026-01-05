using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.Dto.AccuWeatherDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDTO> AccuWeatherResult(AccuLocationWeatherResultDTO dto)
        {
            string apikey = Enviroment.accuweatherkey;
            var baseUrl = "https://dataservice.accuweather.com/indices/v1/daily/1day/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );
                var response = await httpClient.GetAsync($"{dto.CityCode}?apikey={apikey}&details=true");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                List<AccuCityCodeRootDTO> weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDTO>>(jsonResponse);
                dto.CityName = weatherData[0].LocalizedName;
                dto.CityCode = weatherData[0].LocalizedName;
            }


        }
    }
}
