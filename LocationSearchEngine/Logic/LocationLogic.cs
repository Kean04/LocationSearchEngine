using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Xml.Serialization;
using LocationSearchEngine.Models;
using Newtonsoft.Json;

namespace LocationSearchEngine.Logic
{
    public class LocationLogic
    {

        public List<Location> GetLocations(string lat, string lon, string venue)
        {
            string url = "https://api.foursquare.com/v2/venues/explore?client_id={0}&client_secret={1}&v=20180323&limit=1&ll={2},{3}&query={4}";
            string urlFormatted = string.Format(url,
                ConfigurationManager.AppSettings["ClientId"].ToString(),
                ConfigurationManager.AppSettings["ClientSecret"].ToString(),
                lat,
                lon,
                venue);

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(urlFormatted);
            }

            Rootobject records = JsonConvert.DeserializeObject<Rootobject>(json);
            List<Location> locationList = new List<Location>();

            foreach (var item in records.response.groups)
            {
                foreach (var item2 in item.items)
                {
                    Location loc = new Location
                    {
                        Address = item2.venue.location.address,
                        CC = item2.venue.location.cc,
                        City = item2.venue.location.city,
                        Country = item2.venue.location.country,
                        CrossStreet = item2.venue.location.crossStreet,
                        Distance = item2.venue.location.distance,
                        FormattedAddress = item2.venue.location.formattedAddress.ToString(),
                        LabeledLatLngs = item2.venue.location.labeledLatLngs.ToString(),
                        Lattitude = item2.venue.location.lat.ToString(),
                        Longitude = item2.venue.location.lng.ToString(),
                        PostalCode = item2.venue.location.postalCode,
                        State = item2.venue.location.state
                    };

                    locationList.Add(loc);

                }
            }

            return locationList;
        }


        public List<Location> Locations(string lat, string lon, string near, string venue, int userid)
        {
            string url = "";
            string urlFormatted = "";
            if (lat != null && lon != null)
            {
                url = "https://api.foursquare.com/v2/venues/explore?client_id={0}&client_secret={1}&v=20180323&limit=1&ll={2},{3}&query={4}";
                urlFormatted = string.Format(url,
                   ConfigurationManager.AppSettings["ClientId"].ToString(),
                   ConfigurationManager.AppSettings["ClientSecret"].ToString(),
                   lat,
                   lon,
                   venue);
            }
            else
            {
                url = "https://api.foursquare.com/v2/venues/explore?client_id={0}&client_secret={1}&near={2}&v=20180323&limit=1&query={3}";
                urlFormatted = string.Format(url,
                   ConfigurationManager.AppSettings["ClientId"].ToString(),
                   ConfigurationManager.AppSettings["ClientSecret"].ToString(),
                   near,
                   venue);
            }

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(urlFormatted);
            }

            Rootobject records = JsonConvert.DeserializeObject<Rootobject>(json);
            List<Location> locationList = new List<Location>();

            LocationsDBEntities db = new LocationsDBEntities();

            foreach (var item in records.response.groups)
            {
                foreach (var item2 in item.items)
                {

                    #region Add Locations, Categories and Venues to db

                    Location loc = new Location
                    {
                        Address = item2.venue.location.address,
                        CC = item2.venue.location.cc,
                        City = item2.venue.location.city,
                        Country = item2.venue.location.country,
                        CrossStreet = item2.venue.location.crossStreet,
                        Distance = item2.venue.location.distance,
                        FormattedAddress = string.Concat(item2.venue.location.formattedAddress),
                        LabeledLatLngs = latlongFormat(item2.venue.location.labeledLatLngs),
                        Lattitude = item2.venue.location.lat.ToString(),
                        Longitude = item2.venue.location.lng.ToString(),
                        PostalCode = item2.venue.location.postalCode,
                        State = item2.venue.location.state
                    };
                    locationList.Add(loc);
                    db.Locations.Add(loc);
                    db.SaveChanges();

                    Category cat = new Category
                    {
                        Name = item2.venue.categories[0].name,
                        Icon = item2.venue.categories[0].icon.ToString(),
                        Primary = item2.venue.categories[0].primary,
                        PuralName = item2.venue.categories[0].pluralName,
                        ShortName = item2.venue.categories[0].shortName,
                        Id = item2.venue.categories[0].id
                    };

                    db.Categories.Add(cat);
                    db.SaveChanges();

                    Venue ven = new Venue
                    {
                        Name = item2.venue.name,
                        Id = item2.venue.id,
                        Verified = item2.venue.verified,
                        Contact = item2.venue.contact.ToString(),
                        LocationId = loc.LocationId,
                        CategoryId = cat.CategoryId,
                        UserId = userid,
                        CaptureDate = DateTime.Now

                    };

                    db.Venues.Add(ven);
                    db.SaveChanges();
                    #endregion

                    #region Get Photos per location
                    PhotoLogic image = new PhotoLogic();

                    image.Photos(lat, lon, venue, ven.VenueId);
                    #endregion

                }
            }

            return locationList;
        }

        public string latlongFormat(LocationSearchEngine.Models.Labeledlatlng[] latlon)
        {
            string latlonFormatted = "";
            for (int k = 0; k < latlon.Length; k++)
            {
                latlonFormatted = latlon[k].label + "," + latlon[k].lat + "," + latlon[k].lng;
            }
            return latlonFormatted;
        }

    }
}