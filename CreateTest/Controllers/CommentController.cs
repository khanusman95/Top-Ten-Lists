using CreateTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreateTest.Controllers
{
    public class CommentController : Controller
    {

        private myDbContext db;
        private UserManager<ApplicationUser> manager;

        public CommentController()
        {
            db = new myDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Comment()
        {
            return View(new Comment());
        }

        [HttpPost]
        public ActionResult Comment(Comment com, Int32 ListId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var currentList = db.TopTenList.Find(ListId);

            com.AspNetUser = currentUser;
            com.SharedOn = DateTime.Now;
            com.TTList = currentList;
            db.Comments.Add(com);
            db.SaveChanges();

            return RedirectToAction("Index","List", null);
        }
    }
}