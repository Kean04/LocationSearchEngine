using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocationSearchEngine.Entities;
using LocationSearchEngine.Models;

namespace LocationSearchEngine.Logic
{
    public class VenueLogic
    {
        public List<VenueEntity> GetAllVenuesPerUser(int UserId)
        {
            LocationsDBEntities db = new LocationsDBEntities();
            var list = (from v in db.Venues
                        join  p in db.Photos on v.VenueId equals p.VenueId
                        join l in db.Locations on v.LocationId equals l.LocationId
                        where v.UserId == UserId
                        select new VenueEntity { 
                            Id = p.PhotoId,
                            Farm = p.Farm,
                            Server = p.Server,
                            Secret = p.Secret,
                            Name = v.Name,
                            Contact = v.Contact,
                            Verified = v.Verified,
                            ImagePath = "http://farm"+p.Farm+ ".staticflickr.com/" + p.Server + "/" + p.PhotoId + "_" + p.Secret + "_n.jpg",
                            Address = l.FormattedAddress,
                            CaptureDate = v.CaptureDate.ToString()
                        }).OrderByDescending(x=>x.CaptureDate).ToList();

            return list;
        }



    }
}