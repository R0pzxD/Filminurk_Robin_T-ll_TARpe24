using Filminurk.Core.Dto.AccuWeatherDTOs;
using Filminurk.Models.AccuWeather;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;
        public AccuWeatherController(IWeatherForecastServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindWeather(AccuWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeather", new { city = model.CityName });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDTO dto = new();
            dto.CityName = city;
            _weatherForecastServices.AccuWeatherResult(dto);
            AccuWeatherViewModel vm = new();

            vm.EffectiveDate = dto.EffectiveDate;
            vm.EffectiveEpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.text = dto.text;
            vm.Category = dto.Category;
            vm.EndDate = dto.EndDate;
            vm.EndEpochDate = dto.EndEpochDate;
            vm.DailyForecastsDate = dto.DailyForecastsDate;
            vm.DailyForecastsEpochDate = dto.DailyForecastsEpochDate;

            vm.TempMinValue = dto.TempMinValue;
            vm.TempMinUnit = dto.TempMinUnit;
            vm.TempMinUnitType = dto.TempMinUnitType;
            
            vm.DayIcon = dto.DayIcon;
            vm.DayIconPhrase = dto.DayIconPhrase;
            vm.DayHasPrecipitation = dto.DayHasPrecipitation;
            vm.DayHasPrecipitationType = dto.DayHasPrecipitationType;
            vm.DayHasPrecipitationIntensity = dto.DayHasPrecipitationIntensity;

            vm.NightIcon = dto.NightIcon;
            vm.NightIconPhrase = dto.NightIconPhrase;
            vm.NightHasPrecipitation = dto.NightHasPrecipitation;
            vm.NightHasPrecipitationType = dto.NightHasPrecipitationType;
            vm.NightHasPrecipitationIntensity = dto.NightHasPrecipitationIntensity;
            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;
            return View(vm);

        }
    }
}
