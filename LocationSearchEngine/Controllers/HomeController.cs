using LocationSearchEngine.Logic;
using LocationSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationSearchEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard(string SearchText = null)
        {
            VenueModel model = new VenueModel();

         

            VenueLogic venLogic = new VenueLogic();
            var userId = Convert.ToInt32(Session["UserId"]);
            model.venueList = venLogic.GetAllVenuesPerUser(userId).ToList();

            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = SearchText.Trim().ToLower();
                model.venueList = model.venueList.Where(
                    a =>
                        a.Name.ToLower().Contains(SearchText)||
                        a.Address.ToLower().Contains(SearchText)||
                        a.CaptureDate.ToLower().Contains(SearchText)

                        ).ToList();
            }

            return View(model);

        }

        [HttpGet]
        public ActionResult GetNewLocation()
        {
          
            return View();

        }

        [HttpPost]
        public ActionResult GetNewLocation(VenueModel model)
        {           
            LocationLogic locationLogic = new LocationLogic();
            var userId = Convert.ToInt32(Session["UserId"]);
            locationLogic.Locations(model.latitude, model.longitude,model.near, model.venueText, userId);

            return RedirectToAction("Dashboard","Home",model);

        }


    }
}
