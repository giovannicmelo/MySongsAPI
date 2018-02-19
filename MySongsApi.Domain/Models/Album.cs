using System;
using System.Collections.Generic;

namespace MySongsApi.Domain.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime ReleasedDate { get; set; }

        public int IdBand { get; set; }
        public virtual Band Band { get; set; }

        public virtual List<Song> Songs { get; set; }
    }
}