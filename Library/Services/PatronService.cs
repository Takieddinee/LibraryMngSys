using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class PatronService : IPatron
    {

        private LibraryContext _context;

        public PatronService(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public void Add(Patron patron)
        {
            _context.Add(patron);
            _context.SaveChanges();
        }

        public Patron Get(int id)
        {
            return _context.Patrons
                .Include(p => p.LibraryCard)
                .Include(p => p.HomeLibraryBranch)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
                .Include(p => p.LibraryCard)
                .Include(p => p.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int PatronId)
        {
            var CardId = Get(PatronId).LibraryCard.Id;
            return _context.CheckoutHistories.Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == CardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int PatronId)
        {
            var CardId = Get(PatronId).LibraryCard.Id;
            return _context.Checkouts.Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == CardId);

        }

        public IEnumerable<Hold> GetHolds(int PatronId)
        {
            var CardId = Get(PatronId).LibraryCard.Id;
            return _context.Holds.Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.Id == CardId)
                .OrderByDescending(h => h.HoldPlaced);

        }
    }
}
