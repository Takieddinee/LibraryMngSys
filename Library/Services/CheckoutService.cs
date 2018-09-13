using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Services
{
    public class CheckoutService : ICheckout
    {
            
        public void Add(Checkout checkout)
        {
            throw new NotImplementedException();
        }

        public void CheckInItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Checkout> GetAll()
        {
            throw new NotImplementedException();
        }

        public Checkout GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int id)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hold> GetHolds(int id)
        {
            throw new NotImplementedException();
        }

        public void MarkFound(int assetId)
        {
            throw new NotImplementedException();
        }

        public void MarkLost(int assetId)
        {
            throw new NotImplementedException();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }
    }
}
