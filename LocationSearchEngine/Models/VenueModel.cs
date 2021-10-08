using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocationSearchEngine.Entities;
using LocationSearchEngine.Logic;

namespace LocationSearchEngine.Models
{
    public class VenueModel
    {
            public int VenueId { get; set; }
            public List<VenueEntity> venueList { get; set; }
            public string Name { get; set; }
            public string Contact { get; set; }
            public int LocationId { get; set; }
            public int CategoryId { get; set; }
            public int Verified { get; set; }
        public string SearchText { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public string venueText { get; set; }
        public string near { get; set; }
    }
}