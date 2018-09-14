using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IPatron
    {
        Patron Get(int id);
        IEnumerable<Patron> GetAll();
        void Add(Patron patron);
        IEnumerable<CheckoutHistory> GetCheckoutHistories(int PatronId);
        IEnumerable<Hold> GetHolds(int PatronId);
        IEnumerable<Checkout> GetCheckouts(int PatronId);


    }
}
