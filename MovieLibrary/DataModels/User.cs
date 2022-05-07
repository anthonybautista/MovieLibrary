using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieLibrary.DataModels
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        [Range(16,120)]
        public long Age { get; set; }
        public string Gender { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<UserMovie> UserMovies {get;set;}
    }
}
