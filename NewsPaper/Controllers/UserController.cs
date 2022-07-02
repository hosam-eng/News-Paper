using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPaper.Models;
namespace NewsPaper.Controllers
{
    public class UserController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: User
        public ActionResult login()
        {
            if (Request.Cookies["news"] != null)
            {
                Session["userid"] = Request.Cookies["news"].Values["id"];
                return RedirectToAction("profile", new { id= Session["userid"] });
            }
            return View();
        }
        [HttpPost]
        public ActionResult login(User u,bool remember)
        {
          User user=db.Users.Where(n => n.name == u.name).FirstOrDefault();
            if (user!= null && user.password == u.password)
            {
                if (remember)
                {
                    HttpCookie co = new HttpCookie("news");
                    co.Values.Add("id", user.userID.ToString());
                    co.Values.Add("name", user.name);
                    co.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(co);
                }
                Session.Add("userid", user.userID);
                return RedirectToAction("profile", new {id=user.userID});
            }
            else
            {
                ViewBag.mess = "username or password invalid";
                return View();
            }
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(User u)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(u);
                db.SaveChanges();
                ViewBag.id = u.userID;
                Session.Add("userid", ViewBag.id);
                return RedirectToAction("profile", new {id=ViewBag.id });
            }
            else
            {
                return View();
            }
        }

        public ActionResult profile(int id)
        {
            ViewBag.id = id;
           List<News> news = db.News.Where(n=>n.userID==id).ToList();
            return View(news);
        }
        public ActionResult edit(int id)
        {
            User u = db.Users.Where(n => n.userID == id).FirstOrDefault();
            return View(u);
        }
        [HttpPost]
        public ActionResult edit(User u)
        {
            User user = db.Users.Where(n => n.userID == u.userID).FirstOrDefault();
            user.name = u.name;
            user.email = u.email;
            user.Confirm_password = user.password;
            db.SaveChanges();
            return RedirectToAction("profile", new {id=u.userID});
        }
        public ActionResult ch_pass(int id)
        {
            User u= db.Users.Where(n => n.userID == id).FirstOrDefault();
            return View(u);
        }
        [HttpPost]
        public ActionResult ch_pass(User u)
        {
            
            int id = (int)Session["userid"];
            User user = db.Users.Where(n => n.userID == id).FirstOrDefault();
            if (user.password == Request.Form["oldpass"])
            {
                user.userID = user.userID;
                user.name = user.name;
                user.email = user.email;
                user.password = u.password;
                user.Confirm_password = user.password;
                db.SaveChanges();
                ViewBag.success = "success";
                return View("ch_pass");
            }
            else
            {
                ViewBag.error = "not valid";
                return View("ch_pass");
            }
        }
        public ActionResult logout()
        {
            HttpCookie c = new HttpCookie("news");
            c.Expires = DateTime.Now.AddDays(-30);
            Response.Cookies.Add(c);
            Session["userid"] = null;
            return RedirectToAction("login");
        }
        public ActionResult check(string email,int? id)
        {
            if (id == null)
            {
                User u = db.Users.Where(n => n.email == email).FirstOrDefault();
                if (u == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}