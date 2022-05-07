using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.DataModels
{
    public class Movie
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required] 
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
        public virtual ICollection<UserMovie> UserMovies {get;set;}
        
    }
}
