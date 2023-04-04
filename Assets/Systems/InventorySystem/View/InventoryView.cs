using ScriptsUtilities.Views.ItemsContainer;
using UnityEngine;

namespace MyFramework.InventorySystem.View
{
    public class InventoryView : InfoViewContainer 
    {
        [SerializeField] private Inventory _inventory;

        private void Start()
        {
            if(_inventory != null && _inventory.SlotsNumber > 0)
                Init(_inventory.Slots.ToArray());
        }
    }
}