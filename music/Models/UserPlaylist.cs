using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace music.Models
{
    public class UserPlaylist
    {
        [Key]
        public int primaryid { get; set; }
        public int playlistid { get; set; }
        public int songid { get; set; }

    }
}