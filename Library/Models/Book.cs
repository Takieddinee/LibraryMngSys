using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book: LibraryAsset
    {
        [Required]
        public String ISBN { get; set; }

        [Required]
        public String Author { get; set; }

        [Required]
        public String DeweyIndex { get; set; }
    }
}
