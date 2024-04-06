using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using login_page_for_murad_project.Models;

namespace login_page_for_murad_project.Controllers
{
    public class UserPanelController : Controller
    {
        DbConext db = new DbConext();
        // GET: UserPanel
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string email)
        {
            var row=db.GetUsers().Where(model=>model.Email==email).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Login(UsersModels um)
        {
            var data=db.GetUsers().Where(model=>model.Email==um.Email && model.Password==um.Password).FirstOrDefault();
            if (data!=null)
            {
                Session["uid"] = um.Email;
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.Showmsg = "Invalid Email or Password!";
                ModelState.Clear();
            }
            return View();
        }
        public ActionResult Welcome()
        {
            var row = db.GetById(Session["uid"].ToString());
            return View(row);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UsersModels um)
        {
            db.Add(um);
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var data = db.GetUsers().Where(x=>x.Id==id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(UsersModels um)
        {
            db.Update(um);
            return RedirectToAction("Welcome");
        }
    }
}