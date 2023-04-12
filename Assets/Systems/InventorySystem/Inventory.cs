using MyFramework.InventorySystem.Interfaces;
using ScriptsUtilities.Views.ItemsContainer;
using System;
using System.Linq;
using UnityEngine;

namespace MyFramework.InventorySystem
{
    public class Inventory: InfoContainer<Slot>
    {
        private enum ExchangeType
        {
            Free,
            ReadOnly,
            AllowByOptions,
            ForbidByOptions
        }

        [Serializable]
        private class ExchangeOptions
        {
            private enum ExchangeState
            {
                Both,
                ReturnsOnly,
                TakingOnly
            }

            [SerializeField] private ExchangeState _state = ExchangeState.Both;
            [SerializeField] private string _ids;

            public bool SetedReturn(string otherId)
            {
                if (_state == ExchangeState.Both ||
                    _state == ExchangeState.ReturnsOnly)
                    return _ids == otherId;

                return false;
            }

            public bool SetedTaking(string otherId)
            {
                if (_state == ExchangeState.Both ||
                    _state == ExchangeState.TakingOnly)
                    return _ids == otherId;

                return false;
            }
        }

        [SerializeField] private bool _slotEditingAllowed = true;

        [SerializeField] private string _id = "none";
        public string id => _id;

        [SerializeField] private ExchangeType _exchangeOptions;
        [SerializeField] private ExchangeOptions[] _options;

        public int SlotsNumber => info == null ? 0 : info.Length;
        public Slot GetSlot(int id) => info[id];

        public bool ExchangeAllowed(Inventory other)
        {
            if (_id == other._id)
                return true;

            bool otherReturnAllowed = false;
            bool otherSetedReturns = other._options.Any(o => o.SetedReturn(id));

            switch (other._exchangeOptions)
            {
                case ExchangeType.Free:
                    otherReturnAllowed = true;
                    break;
                case ExchangeType.ReadOnly:
                    otherReturnAllowed = false;
                    break;
                case ExchangeType.AllowByOptions:
                    otherReturnAllowed = otherSetedReturns;
                    break;
                case ExchangeType.ForbidByOptions:
                    otherReturnAllowed = !otherSetedReturns;
                    break;
            }

            if (!otherReturnAllowed)
                return false;

            bool takingAllowed = false;
            bool setedTaking = _options.Any(o => o.SetedTaking(other.id));

            switch (_exchangeOptions)
            {
                case ExchangeType.Free:
                    takingAllowed = true;
                    break;
                case ExchangeType.ReadOnly:
                    takingAllowed = false;
                    break;
                case ExchangeType.AllowByOptions:
                    takingAllowed = setedTaking;
                    break;
                case ExchangeType.ForbidByOptions:
                    takingAllowed = !setedTaking;
                    break;
            }

            return takingAllowed;
        }

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            foreach (var slot in info)
            {
                if (!_slotEditingAllowed)
                {
                    slot.TryTakeAll(out var removed);
                    continue;
                }

                slot.OnValidate();
            }
                
        }

#endif

        public IItem TryAdd(IItem item)
        {
            if (item.Type == null)
                return null;

            for (int i = 0; i < SlotsNumber && item != null; i++)
            {
                if (info[i].TryPut(item))
                    item = null;

                else if (info[i].TryApply(item, out IItem rest))
                    item = rest;
            }

            return item;
        }
    }
}