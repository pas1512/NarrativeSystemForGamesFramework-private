using MyFramework.InventorySystem.Interfaces;
using System;
using UnityEngine;

namespace MyFramework.InventorySystem
{
    [Serializable]
    public abstract class Item : IItem
    {
        [SerializeField] private ItemType _type;
        public ItemType Type => _type;

        public virtual string Name => Type.Name;
        public virtual int Number => 1;
        public virtual Sprite StateImage => Type.Image;
        public virtual Sprite DragImage => Type.Image;

        public abstract object Clone();
        public abstract float Quality();
        public abstract bool TryAlternativeAction(out IItem result);
        public abstract bool TryApply(IItem other, out IItem rest);
    }
}