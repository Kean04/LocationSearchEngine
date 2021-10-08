using LocationSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationSearchEngine.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Login(int UserId = 0)
        {
            UserModel userModel = new UserModel();

            return View(userModel);
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (LocationsDBEntities db = new LocationsDBEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).SingleOrDefault();

                if (userDetails == null)
                {
                    ViewBag.ErrorMessage = "Wrong Username or password.";

                    return View("Login", model);
                }
                else {
                    Session["UserId"] = userDetails.UserId;
                }
            }
                UserModel userModel = new UserModel();

            return RedirectToAction("Dashboard","Home");
        }

     
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }


        [HttpGet]
        public ActionResult AddUser(int UserId = 0)
        {
            UserModel userModel = new UserModel();

            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            using (LocationsDBEntities db = new LocationsDBEntities())
            {
                if (db.Users.Any(x => x.Username == model.Username))
                {
                    ViewBag.DuplicateMessage = "This username already exists";
                    return View("AddUser", model);
                }

                User u = new User { 
                Username = model.Username,
                Password = model.Password,
                IsAdmin = false
                };


                db.Users.Add(u);
                db.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMessage = "User Account has been created.";

            return View("AddUser", new UserModel());
        }
    }
}