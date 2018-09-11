using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Patron
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String TelephoneNumber { get; set; }

        //public virtual LibraryCard LibraryCard { get; set; }
    }
}
