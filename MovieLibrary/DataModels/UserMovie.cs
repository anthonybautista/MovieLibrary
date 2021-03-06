using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieLibrary.DataModels
{
    public class UserMovie
    {
        public long Id { get; set; }
        [Required]
        public long Rating {get;set;}
        public DateTime RatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
