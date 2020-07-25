using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music.Models
{
    public class SongHistory
    {
        public int SongHistoryId { get; set; }

        public string user { get; set; }
        public int songId { get; set; }
    }
}