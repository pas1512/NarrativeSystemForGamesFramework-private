using System;
using UnityEngine;
using MyFramework.InventorySystem;
using MyFramework.InventorySystem.Interfaces;

namespace Assets.Systems.InventorySystem.Slots
{
    [Serializable]
    public class ConstrainedSlot : Slot
    {
        [SerializeField] private bool _blocked = true;

        public ConstrainedSlot() { }

        public sealed override bool TryPut(IItem item)
        {
            if(_blocked)
            {
                item = null;
                return false;
            }

            return base.TryPut(item);
        }

        public sealed override bool TryApply(IItem item, out IItem rest)
        {
            if (_blocked)
            {
                rest = null;
                return false;
            }

            return base.TryApply(item, out rest);
        }

        public sealed override bool TryReplace(IItem item, out IItem rest)
        {
            if (_blocked)
            {
                rest = null;
                return false;
            }

            return base.TryReplace(item, out rest);
        }

        public sealed override bool TryTakeAll(out IItem taked)
        {
            if (_blocked)
            {
                taked = null;
                return false;
            }

            return base.TryTakeAll(out taked);
        }

        public sealed override bool TryTakePart(out IItem taked)
        {
            if (_blocked)
            {
                taked = null;
                return false;
            }

            return base.TryTakePart(out taked);
        }
    }
}
