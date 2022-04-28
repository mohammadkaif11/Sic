using Sic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sic.Controllers
{
    public class HomeController : Controller
    {

        private readonly SicEntities db=new SicEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create( user obj )
        {

            if (ModelState.IsValid)
            {
              db.users.Add(obj);
              int a=db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.msg = "User Register successfully";
                    return View();
                }
            }
            ViewBag.msg = "User Register Not successfully";

            return View();
        }

        public ActionResult Get()
        {
            var data = db.users.ToList();
            return View(data);
        }


        public ActionResult Edit(int id)
        {
            var user = db.users.Find(id);   
            return  View(user); 
        }

        public JsonResult Allow(int id)
        {
            
                        var user =db.users.Find(id);    
                        if (user != null)
                        {
                           onboarded onboarded =  new onboarded();
                            onboarded.useremail = user.Email;
                            db.onboardeds.Add(onboarded);
                            db.SaveChanges();
                            return Json("success", JsonRequestBehavior.AllowGet);

                        }
                        return Json("failure", JsonRequestBehavior.AllowGet);
            

        }

        public JsonResult NotAllow(int id)
        {
            var user = db.users.Find(id);
            if (user != null)
            {
                rejected rejected = new rejected();
                rejected.Useremail= user.Email;
                db.rejecteds.Add(rejected);
                db.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("failure", JsonRequestBehavior.AllowGet);

        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}