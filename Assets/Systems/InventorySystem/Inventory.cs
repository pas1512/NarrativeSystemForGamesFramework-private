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
            ReadOnly,
            Free,
            ForbidByOptions,
            AllowByOptions
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

        [SerializeField] private bool _inspectorEditingAllowed = true;

        [SerializeField] private string _id = "none";
        public string id => _id;

        [SerializeField] private bool _duplicateDrag = false;
        public bool duplicateDrag => _duplicateDrag;

        [SerializeField] private bool _selfExchangeAllowed = true;
        [SerializeField] private ExchangeType _exchangeOptions;
        [SerializeField] private ExchangeOptions[] _options;

        public int SlotsNumber => info == null ? 0 : info.Length;
        public float FullPrice => info == null ? 0 : info.Sum(o => o.FullPrice);
        public Slot GetSlot(int id) => info[id];


        public bool ExchangeAllowed(Inventory other)
        {
            if (_selfExchangeAllowed && _id == other._id)
                return true;

            bool otherSetedReturns = other._options.Any(o => o.SetedReturn(id));
            bool otherReturnAllowed = ActionAllowed(other._exchangeOptions,
                otherSetedReturns);

            if (!otherReturnAllowed)
                return false;

            bool setedTaking = _options.Any(o => o.SetedTaking(other.id));
            bool takingAllowed = ActionAllowed(_exchangeOptions, setedTaking);

            return takingAllowed;
        }

        private bool ActionAllowed(ExchangeType type, bool haveInOptions)
        {
            int typeValue = (int)type;

            if (typeValue < 2)
                return typeValue == 1;
            else
                return haveInOptions ^ (typeValue == 2);
        }


#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            foreach (var slot in info)
            {
                if (!_inspectorEditingAllowed)
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