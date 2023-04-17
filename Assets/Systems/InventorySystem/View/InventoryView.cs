using ScriptsUtilities.Views.ItemsContainer;

namespace MyFramework.InventorySystem.View
{
    public class InventoryView : InfoViewContainer<Inventory, SlotView, Slot> 
    {
        public Inventory inventory => container;

        protected override void InitInfoElement(SlotView element, Slot info)
        {
            element.Init(container, info);
            base.InitInfoElement(element, info);
        }
    }
}