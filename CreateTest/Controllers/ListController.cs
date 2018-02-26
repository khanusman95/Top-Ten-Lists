using CreateTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreateTest.Controllers
{
    public class ListController : Controller
    {
        private myDbContext db;
        private UserManager<ApplicationUser> manager;

        public ListController()
        {
            db = new myDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public ActionResult Index()
        {
            return View(db.TopTenList.ToList());
        }

        // GET: List
        public ActionResult Create()
        {            
            return View(new TopTenList());
        }

        [HttpPost]
        public ActionResult Create(TopTenList lmodel, IEnumerable<ListItem> items, IEnumerable<HttpPostedFileBase> file1, HttpPostedFileBase file)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            
            if (ModelState.IsValid)
            {
                lmodel.ListItems = items;
                lmodel.AspNetUser = currentUser;

                using (var br = new BinaryReader(file.InputStream))
                {
                    var data = br.ReadBytes(file.ContentLength);
                    lmodel.Image = data;
                }

                    db.TopTenList.Add(lmodel);

                var imageList = new List<object>();
                foreach (var j in file1)
                {
                    using (var br = new BinaryReader(j.InputStream))
                    {
                        var data = br.ReadBytes(j.ContentLength);
                        imageList.Add(data);
                    }
                }
                var count = 0;
                foreach (var i in items)
                {
                    i.Image = (byte[])imageList[count];
                    i.TTList = lmodel;
                    db.List_Items.Add(i);
                    count++;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(lmodel);
        }       

        public ActionResult Details(int? id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var obj = db.UserRatedLists.Where(m => m.List.ID == id && m.User.Id == currentUser.Id).ToList();

            List<object> myModel = new List<object>();

            myModel.Add(db.TopTenList.Where(m => m.ID == id).ToList());
            myModel.Add(db.List_Items.Where(m => m.TTList.ID == id).OrderByDescending(m => m.Rating).ToList());
            myModel.Add(obj);
            myModel.Add(db.Comments.Where(m => m.TTList.ID == id).ToList());

            return View(myModel);
        }
        //----------------------------------------------------------------
        [HttpPost]
        public ActionResult Comment(int ListID, string text)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.AspNetUser = currentUser;
                comment.CommentContent = text;
                comment.SharedOn = DateTime.Now;
                comment.isPublished = true;
                comment.TTList.ID = ListID;

                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("ListView");
        }

        public ActionResult Rate(int? itemid, string userid)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            Rated rated = new Rated();
            var listItem = db.List_Items.Find(itemid);
            var obj = db.RatedList.Where(r => r.ListItem.ID == (int)itemid && r.User.Id == currentUser.Id).ToList();

            if(obj.Count == 0)
            {
                ListUserHasRated luhr = new ListUserHasRated();                

                rated.ListItem = listItem;
                rated.User = currentUser;
                listItem.Rating += 1;
                luhr.List = db.TopTenList.Find(listItem.TTList.ID);
                luhr.User = currentUser;
                db.UserRatedLists.Add(luhr);
                db.RatedList.Add(rated);
                db.SaveChanges();
                return Redirect("Index");
            }
            else
            {
                return View(obj);
            }

            
        }

        public bool userHasVotedOnThisList (int? ListId)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var list = db.TopTenList.Find(ListId);

            var obj = db.UserRatedLists.Where(r => r.User == currentUser && r.List == list).ToList();
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        


        ///-------------------------------------------------------------------

    }
}