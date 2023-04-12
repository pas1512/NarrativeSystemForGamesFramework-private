using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem.Slots
{
    public class GenericSlot<T>: Slot where T : IItem
    {
        public bool Admits(IItem item) => item is T;
        public bool NotAdmits(IItem item) => !(item is T);

        public GenericSlot() { }

        public GenericSlot(T item)
        {
            Item = item;
        }

        public sealed override bool TryPut(IItem item)
        {
            if (NotAdmits(item))
                return false;

            return base.TryPut(item);
        }

        public sealed override bool TryApply(IItem item, out IItem rest)
        {
            if (NotAdmits(item))
            {
                rest = item;
                return false;
            }

            return base.TryApply(item, out rest);
        }

        public sealed override bool TryReplace(IItem item, out IItem rest)
        {
            if (NotAdmits(item))
            {
                rest = item;
                return false;
            }

            return base.TryReplace(item, out rest);
        }

        public sealed override bool TryTakeAll(out IItem taked)
        {
            return base.TryTakeAll(out taked);
        }

        public sealed override bool TryTakePart(out IItem taked)
        {
            return base.TryTakePart(out taked);
        }
    }
}