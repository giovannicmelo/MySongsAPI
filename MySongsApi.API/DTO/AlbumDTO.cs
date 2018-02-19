using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySongsApi.API.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field 'Name' is required")]
        [StringLength(maximumLength: 150, MinimumLength = 2, ErrorMessage = "'Name' must have between 2 and 150 characters")]
        public string Name { get; set; }
        [StringLength(maximumLength: 500, ErrorMessage = "'Image' couldn't have over 500 characters")]
        public string Image { get; set; }
        public DateTime ReleasedDate { get; set; }
        [Required(ErrorMessage = "The field 'IdBand' is required")]
        public int IdBand { get; set; }

        public List<SongDTO> Songs { get; set; }
    }
}