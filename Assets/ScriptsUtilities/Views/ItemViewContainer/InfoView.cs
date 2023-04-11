using UnityEngine;
using ScriptsUtilities.Views.ItemsContainer;

namespace ScriptsUtilities.Views.ItemViewContainer
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class InfoView : MonoBehaviour
    {
        private IInfo _observable;
        private bool _observing;

        public bool observing => _observing;
        public bool notObserving => !_observing;
        public bool initialized => _observable != null;
        public bool notInitialized => _observable == null;

        protected virtual void OnEnable() => SetObserve();
        protected virtual void OnDisable() => UnsetObserve();
        protected virtual void OnDestroy() => UnsetObserve();

        public virtual bool IsObserved(IInfo itemInfo)
        {
            return _observing && _observable == itemInfo;
        }

        public virtual void ObserTo(IInfo itemInfo)
        {
            if (IsObserved(itemInfo))
                return;

            _observable = itemInfo;
            SetObserve();
        }

        protected abstract void UpdateInfo();

        private void SetObserve()
        {
            if (initialized && notObserving)
            {
                _observable.OnChanged += UpdateInfo;
                _observing = true;
                UpdateInfo();
            }
        }

        private void UnsetObserve()
        {
            if (initialized && observing)
            {
                _observable.OnChanged -= UpdateInfo;
                _observing = false;
            }
        }
    }
}