using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPaper.Models;
namespace NewsPaper.Controllers
{
    public class HomeController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {

            Session.Add("userid",u.userID);
            return View();
        }
        
    }
}