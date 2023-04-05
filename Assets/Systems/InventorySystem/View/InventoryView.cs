using UnityEngine;
using ScriptsUtilities.Views.ItemsContainer;

namespace MyFramework.InventorySystem.View
{
    public class InventoryView : InfoViewContainer<SlotView, Slot> 
    {
        [SerializeField] private Inventory _inventory;

        protected override void InitInfoElement(SlotView element, Slot info)
        {
            element.Init(_inventory, info);
            base.InitInfoElement(element, info);
        }

        private void Start()
        {
            bool inited = _inventory != null && _inventory.SlotsNumber > 0;

            if (!inited)
                inited = _inventory.TryIntit();

            if (inited)
                Init(_inventory.Slots);
        }
    }
}