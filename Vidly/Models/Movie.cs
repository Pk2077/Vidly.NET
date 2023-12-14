using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }
        [StringLength(255)]
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}