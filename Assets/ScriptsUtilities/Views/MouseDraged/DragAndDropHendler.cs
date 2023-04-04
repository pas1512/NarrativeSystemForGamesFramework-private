using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace ScriptsUtilities.Views.MouseDraged
{
    public class DragAndDropHendler : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        [SerializeField] private DragedElement _dragedElement;
        [SerializeField] private bool _saveOffset = true;
        [SerializeField] private bool _clapedByScreen = true;

        private bool _dragStarted;
        private bool _isInited => _dragedElement != null;
        private RectTransform _transform;
        private Vector2 _dragOffset = Vector2.zero;
        private (Vector2 upper, Vector2 lower) _clampedValues;

        private void Start()
        {
            if(_dragedElement == null)
                _dragedElement = GetComponent<DragedElement>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if(_isInited)
            {
                _dragStarted = _dragedElement.Init(eventData.button);

                if (!_dragStarted)
                    return;

                _transform = _dragedElement.GetControl();
                _dragOffset = (Vector2)_transform.position - eventData.position;
                Vector2 size = _transform.rect.size * _transform.localScale * 0.5f;
                Vector2 upperClampedValues = Screen.safeArea.size - size;
                Vector2 lowerClampedValues = size;

                _clampedValues = (upperClampedValues, lowerClampedValues);
                OnDrag(eventData);
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (_isInited && _dragStarted)
            {
                Vector2 newPosition = eventData.position;

                if (_saveOffset)
                    newPosition += _dragOffset;

                if (_clapedByScreen)
                {
                    newPosition.x = Mathf.Clamp(newPosition.x, _clampedValues.lower.x, _clampedValues.upper.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, _clampedValues.lower.y, _clampedValues.upper.y);
                }

                _transform.position = newPosition;
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData) 
        {
            if (_isInited && _dragStarted)
            {
                _dragedElement.Complete();
                _transform = null;
                _dragStarted = false;
            }
        }
    }
}