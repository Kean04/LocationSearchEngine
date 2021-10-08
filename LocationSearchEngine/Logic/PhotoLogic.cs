using LocationSearchEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using static LocationSearchEngine.Models.PhotoModel;

namespace LocationSearchEngine.Logic
{
    public class PhotoLogic
    {

        public List<Photo> GetPhotos(string lat, string lon, string venue)
        {
            string url = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&lat={2}9&lon={3}&per_page=3&page=1&format=json&nojsoncallback=1";
            string urlFormatted = string.Format(url,
                ConfigurationManager.AppSettings["FlickrClientId"].ToString(),
                venue,
                lat,
                lon);

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(urlFormatted);
            }

            PhotoModel.Rootobject records = JsonConvert.DeserializeObject<PhotoModel.Rootobject>(json);
            List<Photo> photoList = new List<Photo>();

            if (records.stat == "ok")
            {
                foreach (PhotoModel.Photo data in records.photos.photo)
                {
                    Photo p = new Photo
                    {
                        PhotoId = data.id,
                        Title = data.title,
                        Farm = data.farm,
                        Secret = data.secret,
                        Server = data.server
                    };

                    photoList.Add(p);
                }               
            }

            return photoList;
        }

        
        public List<PhotoDetails> PhotoDetails(string PhotoId)
        {
            string url = "https://www.flickr.com/services/rest/?method=flickr.photos.getInfo&api_key={0}&photo_id={1}&secret={2}&format=json&nojsoncallback=1";
            string urlFormatted = string.Format(url,
                ConfigurationManager.AppSettings["FlickrClientId"].ToString(),
                PhotoId,
                ConfigurationManager.AppSettings["FlickrClientSecret"].ToString());

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(urlFormatted);
            }

            RootobjectDetails records = JsonConvert.DeserializeObject<RootobjectDetails>(json);
            List<PhotoDetails> photoList = new List<PhotoDetails>();

            if (records.stat == "ok")
            {
                PhotoDetails p = new PhotoDetails
                {
                    id = records.photo.id,
                    title = records.photo.title,
                    comments = records.photo.comments,
                    dates = records.photo.dates,
                    dateuploaded = records.photo.dateuploaded,
                    description = records.photo.description,
                    editability = records.photo.editability,
                    farm = records.photo.farm,
                    geoperms = records.photo.geoperms,
                    isfavorite = records.photo.isfavorite,
                    license = records.photo.license,
                    location = records.photo.location,
                    media = records.photo.media,
                    notes = records.photo.notes,
                    originalformat = records.photo.originalformat,
                    originalsecret = records.photo.originalsecret,
                    owner = records.photo.owner,
                    people = records.photo.people,
                    publiceditability = records.photo.publiceditability,
                    rotation = records.photo.rotation,
                    safety_level = records.photo.safety_level,
                    secret = records.photo.secret,
                    server = records.photo.server,
                    tags = records.photo.tags,
                    urls = records.photo.urls,
                    usage = records.photo.usage,
                    views = records.photo.views,
                    visibility = records.photo.visibility
                };

                photoList.Add(p);

            }

            return photoList;
        }

        public void Photos(string lat, string lon, string venue, int VenueId)
        {

            string url = "";
            string urlFormatted = "";
            if (lat != null && lon != null)
            {
                url = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&lat={2}9&lon={3}&per_page=1&page=1&format=json&nojsoncallback=1";
                urlFormatted = string.Format(url,
                    ConfigurationManager.AppSettings["FlickrClientId"].ToString(),
                venue,
                lat,
                lon);
            }
            else
            {
                url = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&per_page=1&page=1&format=json&nojsoncallback=1";
                urlFormatted = string.Format(url,
                   ConfigurationManager.AppSettings["FlickrClientId"].ToString(),
                venue);
            }

            var json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(urlFormatted);
            }

            PhotoModel.Rootobject records = JsonConvert.DeserializeObject<PhotoModel.Rootobject>(json);

            if (records.stat == "ok")
            {
                foreach (PhotoModel.Photo data in records.photos.photo)
                {
                    string photourl = "http://farm{0}.staticflickr.com/{1}/{2}_{3}_n.jpg";

                    string baseFlickrUrl = string.Format(photourl,
                        data.farm,
                        data.server,
                        data.id,
                        data.secret);

                    #region Add photos to db

                    LocationsDBEntities db = new LocationsDBEntities();

                    if (!db.Photos.Any(x => x.PhotoId == data.id))
                    {
                        Photo p = new Photo
                        {
                            PhotoId = data.id,
                            Title = data.title,
                            Farm = data.farm,
                            Secret = data.secret,
                            Server = data.server,
                            VenueId = VenueId
                        };

                        db.Photos.Add(p);
                        db.SaveChanges();
                    }

                    #endregion
                }
            }
        }
    }
}
