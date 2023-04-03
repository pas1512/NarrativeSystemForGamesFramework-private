using UnityEngine;

namespace MyFramework.InventorySystem.Interfaces
{
    public interface IItemView
    {
        public string Name { get; }
        public int Number { get; }
        public Sprite StateImage { get; }
        public Sprite DragImage { get; }
    }
}