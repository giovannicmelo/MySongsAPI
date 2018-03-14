using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySongsApi.API.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field 'Name' is required")]
        [StringLength(maximumLength: 150, MinimumLength = 2, ErrorMessage = "'Name' must have between 2 and 150 characters")]
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Album { get; set; }

        [Required(ErrorMessage = "The field 'IdAlbum' is required")]
        public int IdAlbum { get; set; }
    }
}