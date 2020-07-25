using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using music.Models;

namespace music.Controllers
{
    public class SongsController : Controller
    {
        private dbcon db = new dbcon();


        public ActionResult getPlaylist()
        {
            //int count = (from row in db.playlists
            //             where row.UserName.ToString() == "'"+User.Identity.GetUserName()+"'"
            //             select row).Count();
            var uname = User.Identity.GetUserName();

            var count = db.playlists.FirstOrDefault(x => x.UserName == uname);
            if (count == null)
                ViewBag.Message = "You don't have any playlist please create new";

            return View(db.playlists.Where(x=>x.UserName==uname).ToList());
        }
        public ActionResult addFav()
        {

            return RedirectToAction("getSong");
        }
        public ActionResult createPlaylist()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createPlaylist(Playlists p)
        {
            if (ModelState.IsValid == false)
                return View();
            var addnew = new Playlists
            {
                UserName = User.Identity.GetUserName(),
                PlaylistName = p.PlaylistName
            };

            var ex = db.playlists.Where(x => x.UserName == addnew.UserName && x.PlaylistName == addnew.PlaylistName).Select(x => x).FirstOrDefault();

            db.playlists.Add(addnew);
            db.SaveChanges();
            return View();
        }
        public ActionResult addtoPlaylist(int id)
        {

            TempData["songid"] = id;
            var uname = User.Identity.GetUserName();
            var allplaylist = db.playlists.Where(x => x.UserName == uname).Select(x => new { x.PlaylistName });
            var selectedplaylist = db.playlists.Where(x => x.UserName == uname).Select(x => new SelectListItem
            {
                Text = x.PlaylistName,
                Value = x.PlaylistName.ToString()
            }).ToList();
            SelectList s = new SelectList(selectedplaylist.ToList());

            ViewBag.allplaylist = selectedplaylist;
            ViewBag.selectionlist = s;
            return View();
        }

        public int removefromplaylist(UserPlaylist p)
        {
            var playlistsong = new UserPlaylist
            {
                playlistid = Convert.ToInt32(Request.QueryString["PlaylistId"]),
                songid = Convert.ToInt32(Request.QueryString["SongId"])
            };
            var obj = db.userplaylists.Where(x => x.playlistid == p.playlistid && x.songid == p.songid).FirstOrDefault();
            db.userplaylists.Remove(obj);

            db.SaveChanges();
            ViewBag.obj = obj;
            ViewBag.sid = p.songid;
            ViewBag.pid = p.playlistid;
            if (ViewBag.sid == null)
                return ViewBag.sid;
            if (ViewBag.pid == null)
                return ViewBag.pid;
            return p.playlistid;
        }

        public int removeplaylist(UserPlaylist p)
        {
            var playlistsong = new UserPlaylist
            {
                playlistid = Convert.ToInt32(Request.QueryString["PlaylistId"]),

            };
            var uname = User.Identity.GetUserName();
            var obj = db.playlists.Where(x => x.Playlistid == p.playlistid && x.UserName == uname).FirstOrDefault();
            db.playlists.Remove(obj);

            db.SaveChanges();
            return p.playlistid;
        }
        [HttpPost]
        public ActionResult addtoPlaylist(FormCollection form)
        {




            String uname = User.Identity.GetUserName();
            String pname = form["Playlists"].ToString();
            if (pname != null)
            {
                //S["pname"] = pname;
                Session["uname"] = uname;
                //  return RedirectToAction("errord");
            }

            var id = db.playlists.Where(x => x.UserName == uname && x.PlaylistName == pname).Select(x => x.Playlistid).First();
            //  Console.WriteLine(id);
            int id2 = Convert.ToInt32(id); //changes
            int sid = Convert.ToInt32(TempData["songid"]);                               //    var id2 = id.ToString();
            var item = new UserPlaylist
            {
                songid = sid,

                //playlistid = Int32.Parse(id)
                playlistid = id2 //changes
            };

            var existiong = db.userplaylists.Where(x => x.songid == item.songid && x.playlistid == item.playlistid).FirstOrDefault();
            if (existiong == null)
            {

                db.userplaylists.Add(item);
                db.SaveChanges();
                return RedirectToAction("DisplaySong");
            }
                TempData["Message"] = "Song is already added in this playlist";
               return RedirectToAction("DisplaySong");

           


            

        }

        public int SongHistory()
        {
            string user = User.Identity.GetUserName();
            SongHistory s = new SongHistory();
            s.songId = int.Parse(Request.QueryString["SongId"]);
            s.user = user;
            db.sh.Add(s);
            db.SaveChanges();
            return s.songId;
        }
        public ActionResult errord()
        {
            return View();
        }
        public ActionResult TrendingSong()
        {
            string user = User.Identity.GetUserName();
            int count = db.sh.Where(x => x.user == user).Select(x=>x.songId).Distinct().Count();
             
            var res = new List<int>();
            if (count > 4)
                res=db.sh.Where(x=>x.user == user).Select(x => x.songId).Distinct().Take(4).ToList();
            else
                res= db.sh.Where(x => x.user == user).Select(x => x.songId).Distinct().ToList();
            List<Song> l = new List<Song>();
            foreach(var item in res)
            {
                var a = db.Song.Where(x => x.SongId == item).FirstOrDefault();
                if (a != null)
                    l.Add(a);
            }
            ViewBag.Trending = l;
            return View();
        }
        public ActionResult viewplaylist()
        {
            var uname = User.Identity.GetUserName();
            var x = db.playlists.Where(x1 => x1.UserName == uname).Select(x1 => x1.Playlistid);

            //         var query = db.playlists   
            //.Join(db.userplaylists, 
            //   Playlists=> Playlists.Playlistid,    
            //   meta => meta.playlistid,   
            //   (pid, psong) => new { Post = pid, Meta = psong }) // selection
            //.Where(postAndMeta => postAndMeta.Post.Playlistid == );
            return View();
        }




        public ActionResult createplay()
        {
            return View();
        }

        public ActionResult viewsingleplaylist(int id)
        {
            var x1 = db.userplaylists.Where(x => x.playlistid == id).Select(x => x).ToList();
            List<Song> s = new List<Song>();

            foreach (var item in x1)
            {
                var adds = db.Song.Where(x => x.SongId == item.songid).FirstOrDefault();
                if (adds != null)
                    s.Add(adds);
            }
            ViewBag.s = s;
            ViewBag.x1 = x1;
            return View();

        }
        
        //[HttpGet]
        //public ActionResult Search()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Search(String searchString)
        //{
        //    var song = from s in db.Songs
        //               select s;
        //    //  var searchString = Request.QueryString[""];
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        //courses = courses.Where(x => x.CourseName == searchString);
        //        ViewBag.Songs = song.Where(x => x.name.Contains(searchString) || x.artist.Contains(searchString)).ToList();
        //        //  ViewBag.Songs = 

        //    }
        //    else
        //    {
        //        ViewBag.Songs = song;
        //    }

        //    return View();
        //}


        public ActionResult DisplaySong()
        {
            //Console.WriteLine(System.IO.File.Exists("~/Models/dbcon.cs"));
            ////if(System.IO.File.Exists("~/Models/dbcon.cs"))
            //    ViewBag.s = Server.MapPath("~/App_Data/file_example_MP3_700KB.mp3");

            //ViewBag.Song = db.Song;
            string user = User.Identity.GetUserName();
            if (user == "")
            {

                ViewBag.Songs = db.Song;
            }
            else
            {
                List<Song> s = db.Song.ToList();
                List<FavouriteSong> f = (db.favouritesong.Where(x => x.user == user)).ToList();
                // var id1 = Convert.ToInt32(id);
                // Console.WriteLine(id1);
                // ViewBag.songs = db.Song;

                List<Song> l = new List<Song>();

                // var id= db.Song.Select(x => x.SongId).ToList();


                //int flag = 0

                foreach (var item in f)
                {
                    // var id1 = Convert.ToInt32(f.SongId);
                    Song f1 = db.Song.Where(x => x.SongId == item.SongId).FirstOrDefault();
                    if (f1 != null)
                    {
                        //flag = 1;
                        l.Add(f1);
                    }
                    //else

                }

                var l1 = s.Except(l);

                Console.WriteLine(l);
                // if (flag == 1)
                ViewBag.songs = l1;
            }
            var a = db.favouritesong.Where(x => x.user == user).Select(x => x.SongId).ToList();
            List<Song> l3 = new List<Song>();
            foreach (var i in a)
            {
                var b = db.Song.Where(x => x.SongId == i).FirstOrDefault();
                l3.Add(b);
            }
            ViewBag.Favourite =l3;
            return View();

        }
        public ActionResult Album(string album)
        {
            //  var a = db.Song.Where(x => x.album == album);
            ViewBag.album = album;
            string user = User.Identity.GetUserName();
            if (user == "")
            {

                ViewBag.songs = db.Song.Where(x => x.album == album);
            }
            else
            {
                List<Song> s = db.Song.Where(x => x.album == album).ToList();
                List<FavouriteSong> f = (db.favouritesong.Where(x => x.user == user)).ToList();
                //    // var id1 = Convert.ToInt32(id);
                //    // Console.WriteLine(id1);
                //    // ViewBag.songs = db.Song;

                List<Song> l = new List<Song>();

                //    // var id= db.Song.Select(x => x.SongId).ToList();


                //    //int flag = 0;

                foreach (var item in f)
                {
                    //        // var id1 = Convert.ToInt32(f.SongId);
                    Song f1 = db.Song.Where(x => x.SongId == item.SongId && x.album == album).FirstOrDefault();
                    if (f1 != null)
                    {
                        //flag = 1;
                        l.Add(f1);
                    }
                    //else

                }

                var l1 = s.Except(l);

                Console.WriteLine(l);
                // if (flag == 1)
                ViewBag.songs = l1;

            }
            // ViewBag.songs = db.Song.Where(x => x.album == album);
            var a = db.favouritesong.Where(x => x.user == user).Select(x => x.SongId).ToList();
            List<Song> l3 = new List<Song>();
            foreach (var i in a)
            {
                var b = db.Song.Where(x => x.SongId == i && x.album == album).FirstOrDefault();
                if (b != null)
                    l3.Add(b);
            }
         //   if (l3.Count == 0)
           //     ViewBag.Favourite = null;
            //else
                 ViewBag.Favourite = l3;
            return View();
        }

        public ActionResult Artist(string artist)
        {
            //  var a = db.Song.Where(x => x.album == album);
            ViewBag.artist = artist;
            string user = User.Identity.GetUserName();
            if (user == "")
            {

                ViewBag.songs = db.Song.Where(x => x.artist == artist);
            }
            else
            {
                List<Song> s = db.Song.Where(x => x.artist == artist).ToList();
                List<FavouriteSong> f = (db.favouritesong.Where(x => x.user == user)).ToList();
                //    // var id1 = Convert.ToInt32(id);
                //    // Console.WriteLine(id1);
                //    // ViewBag.songs = db.Song;

                List<Song> l = new List<Song>();

                //    // var id= db.Song.Select(x => x.SongId).ToList();


                //    //int flag = 0;

                foreach (var item in f)
                {
                    //        // var id1 = Convert.ToInt32(f.SongId);
                    Song f1 = db.Song.Where(x => x.SongId == item.SongId && x.artist == artist).FirstOrDefault();
                    if (f1 != null)
                    {
                        //flag = 1;
                        l.Add(f1);
                    }
                    //else

                }

                var l1 = s.Except(l);

                Console.WriteLine(l);
                // if (flag == 1)
                ViewBag.songs = l1;

            }
            // ViewBag.songs = db.Song.Where(x => x.album == album);
            var a = db.favouritesong.Where(x => x.user == user).Select(x => x.SongId).ToList();
            List<Song> l3 = new List<Song>();
            foreach (var i in a)
            {
                var b = db.Song.Where(x => x.SongId == i && x.artist== artist).FirstOrDefault();
                if (b != null)
                    l3.Add(b);
            }
            //   if (l3.Count == 0)
            //     ViewBag.Favourite = null;
            //else
            ViewBag.Favourite = l3;
            return View();
        }

        public ActionResult Search(string searchString)
        {
            var Song = from w in db.Song
                       select w;
            string user = User.Identity.GetUserName();
            //  var searchString = Request.QueryString[""];
            if (!string.IsNullOrEmpty(searchString))
            {
                if (user == "")
                {

                    //courses = courses.Where(x => x.CourseName == searchString);
                    ViewBag.Songs = Song.Where(x => x.name.Contains(searchString) || x.album.Contains(searchString) || x.artist.Contains(searchString)).ToList();



                }


                else
                {
                    List<Song> s = db.Song.Where(x => x.name.Contains(searchString) || x.album.Contains(searchString) || x.artist.Contains(searchString)).ToList();
                    List<FavouriteSong> f = (db.favouritesong.Where(x => x.user == user)).ToList();

                    List<Song> l = new List<Song>();
                    foreach (var item in f)
                    {
                        // var id1 = Convert.ToInt32(f.SongId);
                        Song f1 = db.Song.Where(x => (x.SongId == item.SongId) && (x.album.Contains(searchString) || x.name.Contains(searchString) || x.artist.Contains(searchString))).FirstOrDefault();
                        if (f1 != null)
                        {
                            //flag = 1;
                            l.Add(f1);
                        }
                        //else

                    }

                    var l1 = s.Except(l);

                    Console.WriteLine(l);
                    // if (flag == 1)
                    ViewBag.songs = l1;

                    var a = db.favouritesong.Where(x => x.user == user).Select(x => x.SongId).ToList();
                    List<Song> l3 = new List<Song>();
                    foreach (var i in a)
                    {
                        var b = db.Song.Where(x => x.SongId == i && (x.name.Contains(searchString) || x.artist.Contains(searchString) || x.album.Contains(searchString))).FirstOrDefault();
                        if(b!=null)
                        l3.Add(b);
                    }
                    ViewBag.Favourite = l3;
                }
            }
            else
            {

                ViewBag.Songs = Song;

            }
            return View("DisplaySong");
        
        }
        public int AddToFavourite(FavouriteSong fs)
        {
            FavouriteSong f = new FavouriteSong();
           // f.SongName = fs.SongName;
            f.SongId = fs.SongId;

            f.SongName = db.Song.Where(x => x.SongId == f.SongId).Select(x => x.name).FirstOrDefault();
            f.user = User.Identity.GetUserName();
            if (f.user == "")
                return 0;
                var f1 = db.favouritesong.Where(x => x.SongId == f.SongId && x.user == f.user).FirstOrDefault();
                Console.WriteLine(f1);
                if (f1 == null)
                {
                    db.favouritesong.Add(f);
                    db.SaveChanges();
                    return f.SongId;

                }

                return 0;
            


           
           
                //return RedirectToAction("/Account/Login");
            
}

        public int RemoveFavourite(FavouriteSong fs)
        {
            FavouriteSong f = new FavouriteSong();
          //  f.SongName = fs.SongName;
            f.SongId = fs.SongId;
            f.user = User.Identity.GetUserName();

            var f1 = db.favouritesong.Where(x => x.SongId == f.SongId && x.user == f.user).FirstOrDefault();

            db.favouritesong.Remove(f1);
            db.SaveChanges();
            return f.SongId;
           // return RedirectToAction("DisplaySong");

        }
        [Authorize]
        public ActionResult DisplayFavouriteSong()
        {
           
            string user = User.Identity.GetUserName();
            
                ViewBag.FavouriteSong = db.favouritesong.Where(x => x.user == user).ToList();

                return View();
          
          //  return RedirectToAction("Login", "Account");
            
        }

        // GET: Songs
        public ActionResult Index()
        {
            ViewBag.songs = db.Song.ToList();
            return View();
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SongId,name,type,path,artist,album")] Song Song)
        {
            if (ModelState.IsValid)
            {
                db.Song.Add(Song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SongId,name,type,path,artist")] Song Song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song Song = db.Song.Find(id);
            if (Song == null)
            {
                return HttpNotFound();
            }
            return View(Song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song Song = db.Song.Find(id);
            db.Song.Remove(Song);
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
