using UnityEngine;
using System.Collections.Generic;
using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Properies.TypeSelector;

namespace MyFramework.InventorySystem
{
    public class Inventory: MonoBehaviour
    {
        [TypeDropdown(typeof(Item)), SerializeField, SerializeReference]
        private Item[] _items;

        private List<Slot> _slots;

        public List<Slot> Slots => _slots;
        public int SlotsNumber => _slots == null ? 0 : _slots.Count;
        public Slot GetSlot(int id) => _slots[id];

        private void Start()
        {
            if (_items == null && _items.Length == 0)
                return;

            Slot[] slotsArray = new Slot[_items.Length];

            for (int i = 0; i < _items.Length; i++)
                slotsArray[i] = new Slot(_items[i]);

            _slots = new List<Slot>(SlotsNumber);
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