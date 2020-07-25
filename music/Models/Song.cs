using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string posterpath { get; set; }
        public string name { get; set;}

        public string type { get; set; }

        public string path { get; set; }

        public string artist{ get; set; }

        public string album { set; get; }

        
    }
}