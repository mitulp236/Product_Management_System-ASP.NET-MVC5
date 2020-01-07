using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo_project.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;


namespace demo_project.Controllers
{
    public class AuthController : Controller
    {
        Contex db = new Contex();
        // GET: Auth
        public ActionResult Login()
        {
            if(Session["email"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        public ActionResult Signup()
        {

            ViewBag.title = "signup";
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            ViewBag.title = "signup";
            if (ModelState.IsValid)
            {
                var output = db.users.FirstOrDefault(m => m.Email == user.Email);
                if(output == null){
                    db.users.Add(user);
                    db.SaveChanges();
                    ViewBag.Message = user.Name + " Successfully Registested";
                    TempData["message"] = user.Name + " Successfully Registested";
                    ViewBag.danger = false;
                    return RedirectToAction("Login", "Auth");
                    //return View();
                }
                ViewBag.Message = user.Email +" Already Exist ! ";
                TempData["message"] = user.Email +" Already Exist ! ";
                ViewBag.danger = true;
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            ViewBag.title = "login";
            if(user.Email == null)
            {
                TempData["message"] = "Wrong Email and Password !";
                ViewBag.message = "Wrong Email and Password ! ";
                ViewBag.danger = true;
                return View();
            }
            if(user.Password == null)
            {
                TempData["message"] = "Wrong Email and Password ! ";
                ViewBag.message = "Wrong Email and Password ! ";
                ViewBag.danger = true;
                return View();
            }
            var output = db.users.FirstOrDefault(m => m.Email == user.Email & m.Password == user.Password);
            if (output != null)
            {
                Session["email"] = user.Email;
                User u = db.users.FirstOrDefault(m => m.Email == user.Email);
                Session["user"] = u.Name;
                //TempData["message"] = "successfully logged in !";
                //ViewBag.message = "successfully logged in !";
                ViewBag.danger = false;
                return RedirectToAction("Index", "Dashboard");
            }
            TempData["message"] = "Wrong Email and Password !";
            ViewBag.message = "Wrong Email and Password ! ";
            ViewBag.danger = true;
            return View();
  
        }

        public ActionResult Logout()
        {
            Session.Clear(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }


    }
}