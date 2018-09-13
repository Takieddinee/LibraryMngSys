using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class CheckoutService : ICheckout
    {
        private LibraryContext _context;
        public CheckoutService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Checkout checkout)
        {
            _context.Add(checkout);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);

            //revmove existing checkouts
            RemoveExistingCheckouts(assetId);
            //closse checkout history
            RemoveCheckoutHistory(assetId);
            //look for existing holds on the item
            var currentHold = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == assetId);
            if (currentHold.Any())
            {
                CheckoutToEarliestHold(assetId, currentHold);
            }

            //if holds checkout to earliesst holds else item availabe
            UpdateAssetStatus(assetId, "Available");

            _context.SaveChanges();

        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHold)
        {
            var earliestHold = currentHold.OrderBy(holds => holds.HoldPlaced).FirstOrDefault();

            var libraryCard = earliestHold.LibraryCard;
            _context.Remove(earliestHold);
            _context.SaveChanges();

            CheckOutItem(assetId, libraryCard.Id);
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
            }
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);

            _context.Update(item);
            UpdateAssetStatus(assetId, "Checked Out");

            var libraryCard = _context.LibraryCards.Include(card => card.Checkouts)
                .FirstOrDefault(card => card.Id == libraryCardId);
            var now = DateTime.Now;
            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHis = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryAsset = item,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHis);

            _context.SaveChanges();


        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int assetId)
        {
            var isCheckOut = _context.Checkouts.Where(co => co.LibraryAsset.Id == assetId).Any();
            return isCheckOut;
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int id)
        {
            return _context.Checkouts.FirstOrDefault(checkout => checkout.Id == id);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int id)
        {
            return _context.CheckoutHistories
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.Id == holdId);

            var cardId = hold?.LibraryCard.Id;

            var patron = _context.Patrons.FirstOrDefault(p => p.LibraryCard.Id == cardId);
            return patron?.FirstName + " " + patron?.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.Id == id)
                .HoldPlaced;
        }

        public IEnumerable<Hold> GetHolds(int id)
        {
            return _context.Holds
                            .Include(h => h.LibraryAsset)
                            .Where(h => h.LibraryAsset.Id == id);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts.Where(c => c.LibraryAsset.Id == assetId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {


            UpdateAssetStatus(assetId, "Available");

            RemoveExistingCheckouts(assetId);

            RemoveCheckoutHistory(assetId);


            _context.SaveChanges();

        }

        private void UpdateAssetStatus(int assetId, String v)
        {
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId
                        );

            _context.Update(item);

            item.Status = _context.Statuses.FirstOrDefault(s => s.Name == v);
        }

        private void RemoveCheckoutHistory(int assetId)
        {
            var history = _context.CheckoutHistories.FirstOrDefault(co => co.LibraryAsset.Id == assetId
                        && co.CheckedIn == null);

            if (history != null)
            {
                _context.Remove(history);
                history.CheckedIn = DateTime.Now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            var checkout = _context.Checkouts.FirstOrDefault(co => co.LibraryAsset.Id == assetId);
            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(int assetId)
        {
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId
                       );

            _context.Update(item);

            item.Status = _context.Statuses.FirstOrDefault(s => s.Name == "Lost");
            _context.SaveChanges();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;
            var asset = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);
            var card = _context.LibraryCards.FirstOrDefault(lc => lc.Id == libraryCardId);

            if (asset.Status.Name == "Available")
            {
                UpdateAssetStatus(assetId, "On Hold");
            }

            var hold = new Hold
            {
                HoldPlaced = now,
                LibraryAsset = asset,
                LibraryCard = card
            };
            _context.Add(hold);

            _context.SaveChanges();
        }

        public string getCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);

            if (checkout == null)
            {
                return "Not Checked Out";
            }
            else
            {
                var cardId = checkout.LibraryCard.Id;
                var patron = _context.Patrons
                    .Include(p => p.LibraryCard)
                    .FirstOrDefault(p => p.LibraryCard.Id == cardId);
                return patron.FirstName + " " + patron.LastName;
            }
        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkouts
                .Include(ch => ch.LibraryAsset)
                .Include(ch => ch.LibraryCard)
                .FirstOrDefault(ch => ch.LibraryAsset.Id == assetId);
        }


    }
}
