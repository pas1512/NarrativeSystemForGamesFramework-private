using System;
using UnityEngine;

namespace ScriptsUtilities.Views.ItemsContainer
{
    public abstract class InfoContainer<I> : MonoBehaviour where I : IInfo
    {
        public event Action OnInfoChanged;

        [SerializeField] private I[] _info;
        public I[] info => _info;

        ValueMemorizer<int> _infoSize;

        protected virtual void OnValidate()
        {
            if(_infoSize == null)
                _infoSize = new ValueMemorizer<int>(-1);

            if (_infoSize.Changed(_info.Length))
                OnInfoChanged?.Invoke();
        }
    }
}