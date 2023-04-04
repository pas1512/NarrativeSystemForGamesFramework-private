using UnityEngine;
using UnityEngine.EventSystems;
using ScriptsUtilities.Views.MouseDraged;
using MyFramework.InventorySystem.Interfaces;
using MyFramework.InventorySystem.View;

namespace MyFramework.InventorySystem.DragEndDrop
{
    [RequireComponent(typeof(SlotView), typeof(RectTransform))]
    public class ItemDragElement : DragedElement,
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        [SerializeField] private DragItem _dragElement;

        private static ItemDragElement _selected;
        public static ItemDragElement Selected => _selected;

        private SlotView _slotView;

        private void Start()
        {
            _slotView = GetComponent<SlotView>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _selected = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_selected == this)
                _selected = null;
        }

        public override bool Init(PointerEventData.InputButton button)
        {
            if (_slotView.notInitialized &&
                _slotView.Slot.Empty)
                return false;

            IItem takedResult = null;

            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    _slotView.Slot.TryTakeAll(out takedResult);
                    break;
                case PointerEventData.InputButton.Right:
                    _slotView.Slot.TryTakePart(out takedResult);
                    break;
                case PointerEventData.InputButton.Middle:
                default:
                    return false;
            }

            _dragElement.Init(takedResult);
            _dragElement.On();
            return true;
        }

        public override RectTransform GetControl()
        {
            return _dragElement.rectTransfrom;
        }

        public override void Complete()
        {
            if(_selected == null)
            {
                GlobalInventory.AddItems(_slotView.owner.transform, new IItem[] { _dragElement.dragedItem });
                _dragElement.Realise();
                _dragElement.Off();
                return;
            }

            IItem rest = null;

            if (_dragElement.haveItem)
            {
                rest = _dragElement.dragedItem;
                rest = _selected._slotView.Slot.Enforce(rest);

                if (rest != null)
                    rest = _slotView.Slot.Enforce(rest);
                else return;

                if (rest != null)
                    rest = _slotView.owner.TryAdd(rest);
                else return;

                if (rest != null)
                    GlobalInventory.AddItems(_slotView.owner.transform, new IItem[] { _dragElement.dragedItem });
                else return;
            }

            _dragElement.Realise();
            _dragElement.Off();
        }
    }
}