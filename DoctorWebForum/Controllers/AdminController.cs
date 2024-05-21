using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebForum.Models;
using PagedList;
using PagedList.Mvc;
namespace DoctorWebForum.Controllers
 


{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult UserList()
        {
            var abc = db.UserDetails.ToList();
            return View(abc);
        }
        public ActionResult QueryList(string seacrhing, int? page)
        {
            var abc = from x in db.Query_Post select x;
            if (seacrhing != null)
            {
                abc = abc.Where(x => x.UserDetails.U_name.Contains(seacrhing)
                || x.tags.Contains(seacrhing) || x.question.Contains(seacrhing));


            }
            return View(abc.ToList().ToPagedList(page ?? 1, 3));
        }
        public ActionResult deleteAccount(string id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //string currentImage = Request.MapPath(UserDetails.u_Image);

            db.DeleteAccount(id);


                //if (System.IO.File.Exists(currentImage))
                //{
                //    System.IO.File.Delete(currentImage);
                //}
                TempData["msg"] = "Data Delete";
                return RedirectToAction("UserList");
           
        }
        public ActionResult DeleteQuery(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteQuery(id);
            return RedirectToAction("QueryList", "Admin");
        }
        public ActionResult Deletereply(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteReply(id);
            return RedirectToAction("QueryList", "Admin");
        }
    }
   
}