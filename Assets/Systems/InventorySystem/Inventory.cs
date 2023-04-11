using UnityEngine;
using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Views.ItemsContainer;

namespace MyFramework.InventorySystem
{
    public class Inventory: InfoContainer<Slot>
    {
        public int SlotsNumber => info == null ? 0 : info.Length;
        public Slot GetSlot(int id) => info[id];

        public IItem TryAdd(IItem item)
        {
            if (item.Type == null)
                return null;

            for (int i = 0; i < SlotsNumber && item != null; i++)
            {
                if (info[i].TryPut(item))
                    item = null;

                else if (info[i].TryApply(item, out IItem rest))
                    item = rest;
            }

            return item;
        }
    }
}