using System;
using UnityEngine;

namespace ScriptsUtilities.Views.ItemsContainer
{
    public abstract class InfoContainer<InfoType> : MonoBehaviour where InfoType : class, IInfo
    {
        public event Action OnInfoChanged;

        [SerializeField] private InfoType[] _info;
        public InfoType[] info => _info;

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