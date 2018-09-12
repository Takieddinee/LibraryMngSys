using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class AssetDetailModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String AuthorOrDirector { get; set; }
        public String Type { get; set; }
        public int Year { get; set; }
        public String ISBN { get; set; }
        public String DeweyCallNumber { get; set; }
        public String Status { get; set; }
        public decimal Cost { get; set; }
        public String  CurrentLocation { get; set; }
        public String ImageUrl { get; set; }
        public String PatronName { get; set; }
        public Checkout LatestCheckout { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistories { get; set; }
        public IEnumerable<AssetHoldModel> CurrentHold { get; set; }

    }

    public class AssetHoldModel
    {
        public String PatronName { get; set; }
        public String HoldPlaced { get; set; }
    }
}
