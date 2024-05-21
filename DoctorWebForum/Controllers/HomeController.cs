using DoctorWebForum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



namespace DoctorWebForum.Controllers
{
    public class HomeController : Controller
    {
        Entities db = new Entities();

        public ActionResult Index()
        {

            ViewBag.a = db.AspNetUsers.Count();
            //string userId = User.Identity.GetUserId();
            return View();

        }
        [ChildActionOnly]
        public ActionResult headerpartial()
        {
            var c = db.UserDetails.ToList();
            return PartialView("_headerpartial1", c);
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
        [Authorize]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(HttpPostedFileBase file, DoctorDetails dd)
        {
            if (ModelState.IsValid)
            {
                UserDetails ud = new UserDetails();
                ud.JoinedDate = DateTime.Now;
                ud.U_id = User.Identity.GetUserId();
                ud.Email = User.Identity.GetUserName();
                ud.U_name = dd.U_name;
                ud.F_name = dd.F_name;
                ud.DOB = dd.DOB;
                ud.City = dd.City;
                ud.country = dd.country;
                ud.Gender = dd.Gender;
                ud.Nationality = dd.Nationality;
                ud.Phone_No = dd.Phone_No;
                ud.Status = dd.Status;
                if (file != null)
                {

                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/images/"), _filename);
                    ud.u_Image = "~/images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            db.UserDetails.Add(ud);
                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                ViewBag.msg = "Record Added";
                                ModelState.Clear();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.msg = "file must be Equal  or less then 1mb";
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    ud.u_Image = "~/images/Avatar.png";
                    db.UserDetails.Add(ud);
                    if (db.SaveChanges() > 0)
                    {
                        ViewBag.msg = "Record Added";
                        ModelState.Clear();
                       
                    }
                }
                Professional_Details pd = new Professional_Details();
                pd.School = dd.School;
                pd.College = dd.College;
                pd.University = dd.University;
                pd.Experience = dd.Experience;
                pd.U_id = User.Identity.GetUserId();
                db.Professional_Details.Add(pd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
       


        [Authorize]
        public ActionResult EditDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var im = from c in db.UserDetails
                     join p in db.Professional_Details on c.U_id equals p.U_id
                     where c.U_id == id
                     select new DoctorDetails
                     {
                         U_id = c.U_id,
                         U_name = c.U_name,
                         F_name = c.F_name,
                         DOB = c.DOB,
                         Gender = c.Gender,
                         Phone_No = c.Phone_No,
                         Status = c.Status,
                         Specailization = c.Specailization,
                         u_Image = c.u_Image,
                         JoinedDate = c.JoinedDate,
                         Nationality = c.Nationality,
                         Email = c.Email,
                         City = c.City,
                         country = c.country,
                         p_id = p.p_id,
                         School = p.School,
                         College = p.College,
                         University = p.University,
                         Experience = p.Experience
                     };
                  
            


            if (im == null)
            {
                return HttpNotFound();
            }
            return View(im.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditDetails(HttpPostedFileBase file, DoctorDetails dd)
        {
            var ud = db.UserDetails.Find(dd.U_id);
            Session["imgPath"] = ud.u_Image;
            ud.U_name = dd.U_name;
            ud.F_name = dd.F_name;
            ud.Email = dd.Email;
            ud.DOB = dd.DOB;
            ud.City = dd.City;
            ud.country = dd.country;
            ud.Gender = dd.Gender;
            ud.JoinedDate = dd.JoinedDate;
            ud.Nationality = dd.Nationality;
            ud.Phone_No = dd.Phone_No;
            ud.Status = dd.Status;
            ud.Specailization = dd.Specailization;

            if (file != null)
            {

                string filename = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                string extension = Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                ud.u_Image = "~/Images/" + _filename;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (file.ContentLength <= 1000000)
                    {
                        db.Entry<UserDetails>(ud).State = EntityState.Modified;

                        string oldimgapth = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {
                            file.SaveAs(path);
                            if (System.IO.File.Exists(oldimgapth))
                            {
                                System.IO.File.Delete(oldimgapth);
                            }
                            TempData["msg"] = "data Updated";
                            return RedirectToAction("ShowUserList");
                        }
                    }

                }
            }
            else
            {

                ud.u_Image = Session["imgPath"].ToString();
                db.Entry<UserDetails>(ud).State = EntityState.Modified;
                
                if (db.SaveChanges() > 0)
                {

                    TempData["msg"] = "data Updated";
                    return RedirectToAction("ShowUserList");

                }

            }

            var pd = db.Professional_Details.Find(dd.p_id);

            pd.School = dd.School;
            pd.College = dd.College;
            pd.University = dd.University;
            pd.Experience = dd.Experience;
            db.Entry<Professional_Details>(pd).State = EntityState.Modified;
            db.SaveChanges();


            return View();
        }
        
        public ActionResult ShowUserList(string seacrhing)
        {
            var abc = from x in db.UserDetails select x;
            if (seacrhing != null)
            {
                abc = abc.Where(x => x.U_name.Contains(seacrhing) || x.City.Contains(seacrhing)
                || x.country.Contains(seacrhing) || x.Specailization.Contains(seacrhing))  ;

            }
            return View(abc.ToList());
        }

        public ActionResult viewProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var im = from c in db.UserDetails
                     join p in db.Professional_Details on c.U_id equals p.U_id
                     where c.U_id == id
                     select new DoctorDetails
                     {
                         U_id = c.U_id,
                         U_name = c.U_name,
                         F_name = c.F_name,
                         DOB = c.DOB,
                         Gender = c.Gender,
                         Phone_No = c.Phone_No,
                         Status = c.Status,
                         Specailization = c.Specailization,
                         u_Image = c.u_Image,
                         JoinedDate = c.JoinedDate,
                         Nationality = c.Nationality,
                         Email = c.Email,
                         City = c.City,
                         country = c.country,
                         p_id = p.p_id,
                         School = p.School,
                         College = p.College,
                         University = p.University,
                         Experience = p.Experience
                     };




            if (im == null)
            {
                return HttpNotFound();
            }
            return View(im.FirstOrDefault());
        }
    }
}