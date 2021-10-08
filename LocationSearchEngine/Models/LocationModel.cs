using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationSearchEngine.Models
{
   
        public class Rootobject
        {
            public Meta meta { get; set; }
            public Response response { get; set; }
        }

        public class Meta
        {
            public int code { get; set; }
            public string requestId { get; set; }
        }

        public class Response
        {
            public Suggestedfilters suggestedFilters { get; set; }
            public Warning warning { get; set; }
            public int suggestedRadius { get; set; }
            public string headerLocation { get; set; }
            public string headerFullLocation { get; set; }
            public string headerLocationGranularity { get; set; }
            public string query { get; set; }
            public int totalResults { get; set; }
            public Suggestedbounds suggestedBounds { get; set; }
            public Group[] groups { get; set; }
        }

        public class Suggestedfilters
        {
            public string header { get; set; }
            public Filter[] filters { get; set; }
        }

        public class Filter
        {
            public string name { get; set; }
            public string key { get; set; }
        }

        public class Warning
        {
            public string text { get; set; }
        }

        public class Suggestedbounds
        {
            public Ne ne { get; set; }
            public Sw sw { get; set; }
        }

        public class Ne
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Sw
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Group
        {
            public string type { get; set; }
            public string name { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public Reasons reasons { get; set; }
            public Venue venue { get; set; }
            public string referralId { get; set; }
        }

        public class Reasons
        {
            public int count { get; set; }
            public Item1[] items { get; set; }
        }

        public class Item1
        {
            public string summary { get; set; }
            public string type { get; set; }
            public string reasonName { get; set; }
        }

        public class Venue
        {
            public string id { get; set; }
            public string name { get; set; }
            public Contact contact { get; set; }
            public Location location { get; set; }
            public Category[] categories { get; set; }
            public bool verified { get; set; }
            public Stats stats { get; set; }
            public Delivery delivery { get; set; }
            public Beenhere beenHere { get; set; }
            public Photos photos { get; set; }
            public Herenow hereNow { get; set; }
        }

        public class Contact
        {
        }

        public class Location
        {
            public string address { get; set; }
            public string crossStreet { get; set; }
            public float lat { get; set; }
            public float lng { get; set; }
            public Labeledlatlng[] labeledLatLngs { get; set; }
            public int distance { get; set; }
            public string postalCode { get; set; }
            public string cc { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string[] formattedAddress { get; set; }
        }

        public class Labeledlatlng
        {
            public string label { get; set; }
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Stats
        {
            public int tipCount { get; set; }
            public int usersCount { get; set; }
            public int checkinsCount { get; set; }
            public int visitsCount { get; set; }
        }

        public class Delivery
        {
            public string id { get; set; }
            public string url { get; set; }
            public Provider provider { get; set; }
        }

        public class Provider
        {
            public string name { get; set; }
            public Icon icon { get; set; }
        }

        public class Icon
        {
            public string prefix { get; set; }
            public int[] sizes { get; set; }
            public string name { get; set; }
        }

        public class Beenhere
        {
            public int count { get; set; }
            public int lastCheckinExpiredAt { get; set; }
            public bool marked { get; set; }
            public int unconfirmedCount { get; set; }
        }

        public class Photos
        {
            public int count { get; set; }
            public object[] groups { get; set; }
        }

        public class Herenow
        {
            public int count { get; set; }
            public string summary { get; set; }
            public object[] groups { get; set; }
        }

        public class Category
        {
            public string id { get; set; }
            public string name { get; set; }
            public string pluralName { get; set; }
            public string shortName { get; set; }
            public Icon1 icon { get; set; }
            public bool primary { get; set; }
        }

        public class Icon1
        {
            public string prefix { get; set; }
            public string suffix { get; set; }
        }


    public class LocationModel
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string CrossStreet { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public bool Verified { get; set; }


    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string PuralName { get; set; }
        public string ShortName { get; set; }
        public string Icon { get; set; }
        public bool Primary { get; set; }


    }
}