using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPaper.Models;
namespace NewsPaper.Controllers
{
    public class NewsController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: News
        [HandleError]
        public ActionResult show_news()
        {
            List<Catigory> cat= db.Catigories.ToList();
            SelectList s = new SelectList(cat, "catID", "name");
            List<News> news= db.News.OrderByDescending(n=>n.datehire).ToList();
            ViewBag.news = news;
            return View(s);
        }
        public ActionResult select_news(int id)
        {
            List<News> news = db.News.Where(n => n.catID == id).ToList();
                ViewBag.news = news;
                return PartialView();
            
        }
        public ActionResult details(int id)
        {
           News news=db.News.Where(n => n.newsID == id).FirstOrDefault();
            return View(news);
        }
        public ActionResult detail(int id)
        {
            News news = db.News.Where(n => n.newsID == id).FirstOrDefault();
            ViewBag.news = news;
            return PartialView();
        }
        public ActionResult create()
        {
            List<Catigory> cat = db.Catigories.ToList();
            SelectList c = new SelectList(cat, "catID", "name");
            ViewBag.cat = c;
            return View();
        }
        [HttpPost]
        public ActionResult create(News n,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                photo.SaveAs(Server.MapPath("~/attach/" + photo.FileName));
                n.photo = photo.FileName;
                n.userID =int.Parse(Session["userid"].ToString());
                db.News.Add(n);
                db.SaveChanges();
                return RedirectToAction("profile", "User", new {id=Session["userid"] });
            }
            else
            {
                return View();
            }
        }
        public ActionResult edit_news(int id)
        {
            List<Catigory> cat = db.Catigories.ToList();
            SelectList st = new SelectList(cat, "catID", "name");
            ViewBag.st = st;
            News news= db.News.Where(n => n.newsID == id).FirstOrDefault();
            return View(news);
        }
        [HttpPost]
        public ActionResult edit_news(News n, HttpPostedFileBase photo)
        {
            photo.SaveAs(Server.MapPath("~/attach/" + photo.FileName));
            n.photo = photo.FileName;
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("profile", "User", new { id = Session["userid"] });
        }

    }
}