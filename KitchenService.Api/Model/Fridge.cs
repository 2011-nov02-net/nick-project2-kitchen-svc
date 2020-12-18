using System;
using System.Collections.Generic;
using System.Linq;

namespace KitchenService.Api.Model
{
    public class Fridge : Appliance
    {
        private readonly ISet<FridgeItem> _items = new HashSet<FridgeItem>();

        public IReadOnlySet<FridgeItem> Contents => new HashSet<FridgeItem>(_items);

        public Fridge()
        {
            _items.Add(new() { Id = 1, Name = "Rockstar energy drink", WeightOz = 12, Expiration = new DateTime(2021, 1, 10) });
            _items.Add(new() { Id = 2, Name = "Ham", WeightOz = 10, Expiration = new DateTime(2020, 12, 1) });
            _items.Add(new() { Id = 3, Name = "Eggs", WeightOz = 12, Expiration = new DateTime(2020, 12, 30) });
        }

        public bool AddItem(FridgeItem item)
        {
            if (item.Id == default)
            {
                item.Id = _items.Max(x => x.Id) + 1;
            }
            else if (_items.Any(x => x.Id == item.Id))
            {
                return false;
            }
            _items.Add(item);
            return true;
        }

        public ISet<FridgeItem> Clean()
        {
            var expired = _items.Where(x => x.IsExpired).ToHashSet();
            _items.ExceptWith(expired);
            return expired;
        }
    }
}
