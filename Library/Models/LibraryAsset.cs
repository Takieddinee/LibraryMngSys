using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public abstract class LibraryAsset
    {
        public int Id { get; set; }

        [Required]
        public String Titel { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public decimal Cost { get; set; }
        public String ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public virtual LibraryBranch Location { get; set; }


    }
}
