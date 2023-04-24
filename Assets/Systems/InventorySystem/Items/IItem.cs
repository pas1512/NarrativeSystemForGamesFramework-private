using System;
using MyFramework.InventorySystem.Types;

namespace MyFramework.InventorySystem.Items
{
    public interface IItem : IItemView, ICloneable, IQualitable
    {
        public ItemType Type { get; }
        public bool TryApply(IItem other, out IItem rest);
        public bool TryAlternativeAction(out IItem result);
    }
}