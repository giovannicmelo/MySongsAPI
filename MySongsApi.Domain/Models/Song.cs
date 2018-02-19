using System;
using System.Collections.Generic;
using System.Text;

namespace MySongsApi.Domain.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public int IdAlbum { get; set; }
        public virtual Album Album { get; set; }
    }
}
