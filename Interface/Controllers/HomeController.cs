using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessLayer;
using Data.MapObjects;
using Interface.Logging;

namespace Interface.Controllers
{
    [LogAction]
    public class HomeController : Controller
    {
        public HomeController(ProcessModel processModel)
        {
            _processModel = processModel;
            if (System.Web.HttpContext.Current.Session["ApplicationSettings"] == null)
            {
                var appSettings = _processModel.GetApplicationSettings();
                System.Web.HttpContext.Current.Session["ApplicationSettings"] = appSettings
                    .ToDictionary(x => x.SettingName, x => x.SettingValue);
            }
        }
        
        public ActionResult Index()
        {
            ViewBag.AppSettings = System.Web.HttpContext.Current.Session["ApplicationSettings"];
            return View();
        }
        
        public JsonResult GetWeatherForecast(Coordinates coords)
        {
            var coordsWereValid = _processModel.TryGetWeatherForCoordinates(coords, out var weatherForecast);

            if (!coordsWereValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                Response.StatusDescription = $"Coordinates: {coords.LatitudeRoundedToString}, {coords.LongitudeRoundedToString} are invalid.";
                return new JsonResult();
            }
            
            return Json(weatherForecast, JsonRequestBehavior.AllowGet); 
        }

        private readonly ProcessModel _processModel;
    }
}