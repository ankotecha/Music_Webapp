using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using music.Models;


namespace music.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private dbcon db1 = new dbcon();
        public ActionResult EditSong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db1.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSong([Bind(Include = "SongId,name,type,path,artist,album,posterpath")] Song Song)
        {
            if (ModelState.IsValid)
            {
                db1.Entry(Song).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Song);
        }

        public ActionResult DeleteSong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db1.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSongConfirmed(int id)
        {
            Song Song = db1.Song.Find(id);
            string fullPath = Request.MapPath(Song.path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            fullPath = Request.MapPath(Song.posterpath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db1.Song.Remove(Song);
            db1.SaveChanges();
            return RedirectToAction("SongIndex");
        }
        public ActionResult SongIndex()
        {
           
            return View(db1.Song.ToList());
        }
        public ActionResult DetailsSong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db1.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadFile()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadFile(HttpPostedFileBase file, HttpPostedFileBase image)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Static/"), _FileName);
                    file.SaveAs(_path);

                    string _FileName1 = Path.GetFileName(image.FileName);
                    string _path1 = Path.Combine(Server.MapPath("~/Static/Poster/"), _FileName1);
                    image.SaveAs(_path1);

                    var s = new Song
                    {
                        name = Request["name"],
                        artist = Request["artist"],
                        album = Request["album"],
                        type = Request["type"],
                        path = "/Static/" + _FileName,
                        posterpath = "~/Static/Poster/" + _FileName1
                    };
                    db1.Song.Add(s);
                    db1.SaveChanges();


                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        // GET: AdminUser
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: AdminUser/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: AdminUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: AdminUser/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: AdminUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: AdminUser/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: AdminUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
