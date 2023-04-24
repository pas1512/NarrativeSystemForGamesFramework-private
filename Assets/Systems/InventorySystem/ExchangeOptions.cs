using System;
using UnityEngine;

namespace MyFramework.InventorySystem
{
    [Serializable]
    internal class ExchangeOptions
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
}
