using System;
using UnityEngine;
using MyFramework.InventorySystem.Types;
using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem
{
    [Serializable]
    public abstract class Item : IItem
    {
        [SerializeField] private ItemType _type;
        public ItemType Type => _type;

        public virtual string Name => Type.Name;
        public virtual int Number => 1;
        public virtual float Price => Type.Price;
        public virtual Sprite StateImage => Type.Image;
        public virtual Sprite DragImage => Type.Image;

        public Item() 
        {
            _type = null;
        }

        public Item(ItemType itemType)
        {
            _type = itemType;
        }

        public abstract object Clone();
        public abstract float Quality();
        public abstract bool TryAlternativeAction(out IItem result);
        public abstract bool TryApply(IItem other, out IItem rest);
    }
}