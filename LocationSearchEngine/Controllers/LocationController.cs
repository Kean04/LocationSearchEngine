using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LocationSearchEngine.Logic;
using LocationSearchEngine.Models;

namespace LocationSearchEngine.Controllers
{
    public class LocationController : ApiController
    {
        [HttpGet]
        [Route("api/Location/GetVenues")]
        public async Task<List<Location>> GetVenues(string Lat, string Lon, string Venue)
        {
            LocationLogic lLogic = new LocationLogic();
            List<Location> list = new List<Location>();
            list = lLogic.GetLocations(Lat, Lon, Venue);

            return list;
        }

        [HttpGet]
        [Route("api/Location/GetPhotos")]
        public async Task<List<Photo>> GetPhotos(string Lat, string Lon, string Venue)
        {

            List<Photo> list = new List<Photo>();
            PhotoLogic photo = new PhotoLogic();

            list = photo.GetPhotos(Lat, Lon, Venue);
            return list;
        }

        [HttpGet]
        [Route("api/Location/GetPhotoDetails")]
        public async Task<List<PhotoModel.PhotoDetails>> GetPhotoDetails(string PhotoId)
        {

            List<PhotoModel.PhotoDetails> list = new List<PhotoModel.PhotoDetails>();
            PhotoLogic photo = new PhotoLogic();

            list = photo.PhotoDetails(PhotoId);
            return list;
        }

    }
}
