using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.DataModels
{
    public class Occupation
    {
        public long Id { get; set; }
        [Required] 
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
