using System.Linq;
using UnityEngine;
using ScriptsUtilities.Views.ItemsContainer;
using MyFramework.InventorySystem.Items;

namespace MyFramework.InventorySystem
{
    public class Inventory: InfoContainer<Slot>
    {
        [SerializeField] private bool _inspectorEditingAllowed = true;

        public int SlotsNumber => info == null ? 0 : info.Length;
        public int EmptySlotsNumber => info == null ? 0 : info.Count(o => o.Empty);
        public int NonEmptySlotsNumber => info == null ? 0 : info.Count(o => !o.Empty);
        public float FullPrice => info == null ? 0 : info.Sum(o => o.FullPrice);

        public IItem[] GetItems() => info.Where(o => !o.Empty).Select(o => o.Item).ToArray();
        public Slot GetSlot(int id) => info[id];

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            foreach (var slot in info)
            {
                if (!_inspectorEditingAllowed)
                {
                    slot.TryTakeAll(out var removed);
                    continue;
                }

                slot.OnValidate();
            }
                
        }

#endif

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

        public IItem TryAdd(IItem[] items)
        {
            IItem rest = null;

            foreach (var item in items)
            {
                rest = item;

                do
                {
                    if (EmptySlotsNumber == 0)
                        return rest;

                    rest = TryAdd(rest);
                } while (rest != null);
            }

            return rest;
        }

        public void Clear()
        {
            if (info != null)
            {
                foreach (var slot in info)
                    slot.TryTakeAll(out var temp);
            }
        }
    }
}