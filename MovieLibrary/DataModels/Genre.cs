using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.DataModels
{
    public class Genre
    {
        public long Id { get; set; }
        [Required] 
        [MaxLength(20)]
        public string Name { get; set; }
        
        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
    }
}
