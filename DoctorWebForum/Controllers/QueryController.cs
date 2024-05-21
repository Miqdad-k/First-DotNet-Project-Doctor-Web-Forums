using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebForum.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace DoctorWebForum.Controllers
{
    public class QueryController : Controller
    {
        // GET: Qeury
        Entities db = new Entities();
        [Authorize]
        public ActionResult Index(string seacrhing ,int? page)
        {
            ViewBag.query = db.Query_Post.Count(); 
            var abc = from x in db.Query_Post select x;
            if (seacrhing != null)
            {
                abc = abc.Where(x => x.UserDetails.U_name.Contains(seacrhing)
                || x.tags.Contains(seacrhing) || x.question.Contains(seacrhing));


            }
            return View(abc.ToList().ToPagedList(page ?? 1, 3)
);
        }
        public ActionResult createQuery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createQuery(Query_Post Qp)
        {
            if (ModelState.IsValid)
            {
                Qp.U_id = User.Identity.GetUserId();
                Qp.CreateOn = DateTime.Now;
                db.Query_Post.Add(Qp);
                if (db.SaveChanges() > 0)
                {
                    ViewBag.msg = "Record Added";
                    ModelState.Clear();
                    return RedirectToAction("Index","Query");
                }
            }
            return View();
        }
        public ActionResult EditQuery(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var im = db.Query_Post.Find(id);



            if (im == null)
            {
                return HttpNotFound();
            }
            return View(im);
        }
        [HttpPost]
        public ActionResult EditQuery(Query_Post Qp)
        {
            Qp.U_id = User.Identity.GetUserId();
            Qp.CreateOn = DateTime.Now;
            db.Entry(Qp).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                TempData["msg"] = "data Updated";
                return RedirectToAction("Index", "Query");
            }
            return View(Qp);
        }

        public ActionResult DetailqueryPost(int id)
        {
            
            var im = db.Query_Post.Find(id);
            return View(im);
        }
        [HttpPost]
        public ActionResult PostReply(string reply,int CID)
        {
            
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Query_Reply r = new Query_Reply();
            r.Reply = reply;
            r.q_id = CID;
            r.U_id = userid.ToString();
            r.CreateOn = DateTime.Now;
            db.Query_Reply.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index", "Query");
        }
    }
}