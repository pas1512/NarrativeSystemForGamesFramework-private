using System;

namespace MyFramework.InventorySystem.Interfaces
{
    public interface IItem : IItemView, ICloneable, IQualitable
    {
        public ItemType Type { get; }
        public bool TryApply(IItem other, out IItem rest);
        public bool TryAlternativeAction(out IItem result);
    }
}