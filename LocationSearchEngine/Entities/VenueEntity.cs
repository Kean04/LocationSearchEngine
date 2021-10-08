using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationSearchEngine.Entities
{
    public class VenueEntity
    {

            public int VenueId { get; set; }
            public List<Venue> venueList { get; set; }
            public string Name { get; set; }
            public string Contact { get; set; }
            public int LocationId { get; set; }
            public int CategoryId { get; set; }
            public bool? Verified { get; set; }
        public int Farm { get; set; }
        public string Server { get; set; }
        public string Id { get; set; }
        public string Secret { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public string CaptureDate { get; set; }
    }
}