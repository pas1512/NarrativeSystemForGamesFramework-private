using System;
using ScriptsUtilities.Views.ItemsContainer;
using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Properies.TypeSelector;
using UnityEngine;
using ScriptsUtilities;
using Unity.VisualScripting.Antlr3.Runtime;

namespace MyFramework.InventorySystem
{
    [Serializable]
    public class Slot : IInfo
    {
        public event Action OnChanged;

        [TypeSelector(typeof(Item)), SerializeField, SerializeReference]
        private IItem _item;
        private ValueMemorizer<int> _itemCache; 

        public IItem Item 
        {
            get => _item;
            protected set => _item = value;
        }

        public bool Empty => _item == null;
        public IItemView View => _item;
        public float FullPrice => _item == null ? 0 : _item.Price;

        public Slot(IItem item = null)
        {
            if(item.Type != null)
                _item = item;
        }

        public virtual bool TryPut(IItem item)
        {
            if (Empty)
            {
                Item = item;
                OnChanged?.Invoke();
                return true;
            }

            return false;
        }

        public virtual bool TryApply(IItem item, out IItem rest)
        {
            if (_item.TryApply(item, out rest))
            {
                OnChanged?.Invoke();
                return true;
            }

            return false;
        }

        public virtual bool TryReplace(IItem item, out IItem rest)
        {
            rest = _item;

            if (Empty)
                return false;

            _item = item; 
            OnChanged?.Invoke();

            return true;
        }

        public virtual bool TryTakePart(out IItem taked)
        {
            taked = _item;

            if (Empty)
                return false;

            if (_item.TryAlternativeAction(out taked))
            {
                OnChanged?.Invoke();
                return true;
            }

            return false;
        }

        public virtual bool TryTakeAll(out IItem taked)
        {
            taked = _item;

            if (Empty)
                return false;

            _item = null;
            OnChanged?.Invoke();
            return true;
        }

        public IItem EnforceHard(IItem item)
        {
            IItem rest = item;

            if (TryPut(item))
                rest = null;

            else if (!TryApply(item, out rest))
                TryReplace(item, out rest);

            return rest;
        }

        public IItem EnforceSoft(IItem item)
        {
            if (TryPut(item))
                return null;

            TryApply(item, out IItem rest);

            return rest;
        }

        public virtual void OnValidate()
        {
            if (_item == null)
                return;

            if (_item.Type == null || _item.Number <= 0)
            {
                _item = null;
                return;
            }

            if (_itemCache == null)
                _itemCache = new ValueMemorizer<int>(int.MinValue);

            if(_itemCache.Changed(_item.GetHashCode()))
                OnChanged?.Invoke();
        }
    }
}