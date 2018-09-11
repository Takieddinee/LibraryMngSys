using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,ErrorMessage ="Limit Branch Name to 30 char.")]
        public String Name { get; set; }

        [Required]
        public String Adress { get; set; }

        [Required]
        public String Telephone { get; set; }

        public String Description { get; set; }
        public DateTime OpenDate { get; set; }

        public virtual IEnumerable<Patron> Patrons { get; set; }
        public virtual IEnumerable<LibraryAsset> LibraryAssets { get; set; }

        public String ImageUrl { get; set; }
    }
}
