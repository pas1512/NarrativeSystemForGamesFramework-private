using UnityEngine;

namespace MyFramework.InventorySystem.Items
{
    public interface IItemView
    {
        public string Name { get; }
        public int Number { get; }
        public Sprite StateImage { get; }
        public Sprite DragImage { get; }
    }
}