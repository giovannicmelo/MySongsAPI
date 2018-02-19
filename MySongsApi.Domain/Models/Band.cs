using System;
using System.Collections.Generic;
using System.Text;

namespace MySongsApi.Domain.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Biography { get; set; }

        public virtual List<Album> Albums { get; set; }
    }
}
