using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace music.Models
{
    public class Playlists
    {   
     
        public String UserName { get; set; }
        [Key]
        public int Playlistid { get; set; }

        [Required(ErrorMessage ="PlayList name required")]
        public String PlaylistName { get; set; } 
    }
}