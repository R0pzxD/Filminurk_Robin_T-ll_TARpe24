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
            string weatherResponse = baseUrl + $"{dto.CityName}?apikey={apikey}&metric=true";
            using (var clientWeather = new HttpClient())
            {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();
                AccuLocationRootDTO weatherRootDTO = JsonSerializer.Deserialize<AccuLocationRootDTO>(jsonWeather);
                dto.EffectiveDate = weatherRootDTO.HeadLine.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDTO.HeadLine.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.text = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate;
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDTO.Headline.MobileLink;
                dto.Link = weatherRootDTO.Headline.Link;

                dto.DailyForecastsDate = weatherRootDTO.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDTO.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDTO.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDTO.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDTO.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayHasPrecipitationType = weatherRootDTO.DailyForecasts[0].Day.PrecipitationType;

                dto.NightIcon = weatherRootDTO.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDTO.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDTO.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightHasPrecipitationType = weatherRootDTO.DailyForecasts[0].Night.PrecipitationType;


            }
            return dto;


        }
    }
}
