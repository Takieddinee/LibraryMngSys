using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class PatronDetailModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public int LibraryCardId { get; set; }
        public String Adress { get; set; }
        public DateTime MemberSince { get; set; }
        public String Telephone { get; set; }
        public String HomeLibraryBranch { get; set; }
        public decimal OverdueFees { get; set; }
        public IEnumerable<Checkout> AssetsCheckouts { get; set; }
        public IEnumerable<CheckoutHistory> checkoutHistories { get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
