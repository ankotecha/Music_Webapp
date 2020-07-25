using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace music.Models
{
    public class FavouriteSong
    {
        public int FavouriteSongId { get; set; }


        public string user { get; set; }
        //[ForeignKey("UserName")]
        //public virtual ApplicationUser a{ get; set; }
        public int SongId { get; set; }


        [ForeignKey("SongId")]
        public virtual Song s { get; set; }
        public string SongName { get; set; }
    }
}