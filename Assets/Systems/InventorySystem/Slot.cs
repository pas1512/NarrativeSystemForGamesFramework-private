using System;
using ScriptsUtilities.Views.ItemsContainer;
using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Properies.TypeSelector;
using UnityEngine;

namespace MyFramework.InventorySystem
{
    [Serializable]
    public class Slot : IInfo
    {
        public event Action OnChanged;

        [TypeSelector(typeof(Item)), SerializeField, SerializeReference]
        private IItem _item;

        public IItem Item 
        {
            get => _item;
            protected set => _item = value;
        }

        public bool Empty => _item == null;
        public IItemView View => _item;

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

        public IItem Enforce(IItem item)
        {
            IItem rest = item;

            if (TryPut(item))
                rest = null;
            else if (TryApply(item, out rest)) { }
            else if (TryReplace(item, out rest)) { }

            return rest;
        }
    }
}