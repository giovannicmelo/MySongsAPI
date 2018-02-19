using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySongsApi.API.DTO
{
    public class BandDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field 'Name' is required")]
        [StringLength(maximumLength: 150, MinimumLength = 2, ErrorMessage = "'Name' must have between 2 and 150 characters")]
        public string Name { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "'Genre' couldn't have over 50 characters")]
        public string Genre { get; set; }
        [StringLength(maximumLength: 500, ErrorMessage = "'Name' couldn't have over 500 characters")]
        public string Biography { get; set; }

        public List<AlbumDTO> Albums { get; set; }
    }
}