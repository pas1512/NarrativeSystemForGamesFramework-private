using UnityEngine;
using System.Collections.Generic;
using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem
{
    public class Inventory: MonoBehaviour
    {
        private List<Slot> _slots;

        public List<Slot> Slots => _slots;
        public int SlotsNumber => _slots.Count;
        public Slot GetSlot(int id) => _slots[id];

        private void Start()
        {
            Slot[] slotsArray = new Slot[SlotsNumber];

            for (int i = 0; i < SlotsNumber; i++)
                slotsArray[i] = new Slot();

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