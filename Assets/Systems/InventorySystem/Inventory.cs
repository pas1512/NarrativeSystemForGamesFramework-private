using UnityEngine;
using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Properies.TypeSelector;

namespace MyFramework.InventorySystem
{
    public class Inventory: MonoBehaviour
    {
        [TypeSelector(typeof(Item)), SerializeField, SerializeReference]
        private Item[] _items;

        private Slot[] _slots;
        public Slot[] Slots => _slots;

        public int SlotsNumber => _slots == null ? 0 : _slots.Length;
        public Slot GetSlot(int id) => _slots[id];

        public bool TryIntit()
        {
            if (_items == null && _items.Length == 0)
                return false;

            _slots = new Slot[_items.Length];

            for (int i = 0; i < _items.Length; i++)
                _slots[i] = new Slot(_items[i]);

            return true;
        }

        public IItem TryAdd(IItem item)
        {
            if (item.Type == null)
                return null;

            for (int i = 0; i < SlotsNumber && item != null; i++)
            {
                if (_slots[i].TryPut(item))
                    item = null;

                else if (_slots[i].TryApply(item, out IItem rest))
                    item = rest;
            }

            return item;
        }
    }
}