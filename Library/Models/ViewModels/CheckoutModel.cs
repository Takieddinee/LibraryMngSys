using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class CheckoutModel
    {
        public String LibraryCardId { get; set; }
        public String Title { get; set; }
        public int AssetId { get; set; }
        public String ImageUrl { get; set; }
        public int HoldCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
