using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ICheckout
    {
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int id);
        void Add(Checkout checkout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);
        IEnumerable<CheckoutHistory> GetCheckoutHistories(int id);
        Checkout GetLatestCheckout(int assetId);
        String getCurrentCheckoutPatron(int assetId);
        bool IsCheckedOut(int assetId);

        void PlaceHold(int assetId, int libraryCardId);
        String GetCurrentHoldPatronName(int holdId);
        DateTime GetCurrentHoldPlaced(int id);
        IEnumerable<Hold> GetHolds(int id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);

    }
}
