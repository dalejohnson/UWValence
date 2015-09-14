using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UWD2L.Valence.Models.User;


namespace Demo.Controllers
{
    public class D2LController : Controller
    {
        UWD2L.Valence.Biz.Valence d2l = new UWD2L.Valence.Biz.Valence();

        // GET: D2L
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnrollUser()
        {
            Demo.Models.D2LEnroll model = new Models.D2LEnroll();
            return View(model);    
        }

        [HttpPost]
        public ActionResult EnrollUser(Demo.Models.D2LEnroll model)
        {

           // Retrieve User Data
            UserData user;
            try
            {
                user = d2l.GetUserData(model.UserName);
                model.UserID = user.UserId;
            }
            catch (System.Exception e)
            {
                return Content("Error: " + e.ToString());
            
            }

            if (user == null )
            {
                return Content("User Not Found");

            }
           
          

           if (model.Role == "instructor")
               model.RoleID = 898;
           else
               model.RoleID = 899;


           try
           {
               d2l.EnrollUser(model.OrgUnitID, model.UserID, model.RoleID);


           }
           catch (System.Exception e1)
           { 
                return Content ("Error:  " + e1.ToString ());
           
           }

           return Content("Success!");
        }

        public ActionResult DeleteUser()
        {
            Demo.Models.D2LEnroll model = new Models.D2LEnroll();
            return View(model);
        
        }
        
        [HttpPost]
        public ActionResult DeleteUser(Demo.Models.D2LEnroll model)
        {
            UserData user;
            try
            {
                user = d2l.GetUserData(model.UserName);
                model.UserID = user.UserId;
            }
            catch (System.Exception e)
            {
                return Content("Error: " + e.ToString());

            }

            if (user == null)
            {
                return Content("User Not Found");

            }

            try
            {
                d2l.DeleteUser(model.OrgUnitID, model.UserID);
            }
            catch (System.Exception e1)
            { 
                return Content ("Error:" + e1.ToString ());
            
            }

            return Content("Success!");

        }

        public ActionResult GetFinalGrade()
        {
            Demo.Models.D2LFinalGrade model = new Models.D2LFinalGrade();
            return View(model);

        }

        [HttpPost]
        public ActionResult GetFinalGrade(Demo.Models .D2LFinalGrade model)
        {

           
            // Retrieve User Data
            UserData user;
            try
            {
                user = d2l.GetUserData(model.UserName);
            }
            catch (System.Exception e)
            {
                return Content("Error: " + e.ToString());

            }

            if (user == null)
            {
                return Content("User Not Found");

            }


           string gradeValue = d2l.GetFinalGrade(model.OrgUnitID, user.UserId);

           return Content("Success:" + gradeValue);
            
        }

    }
}