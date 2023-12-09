using Vidly.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.ViewModels
{
    public class MoviesViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        [Range(1, 20)]
        public byte? NumberInStock { get; set; }
        [Required]
        public int? GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public MoviesViewModel()
        {
            Id = 0;
        }
        public MoviesViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;

        }
    }
}