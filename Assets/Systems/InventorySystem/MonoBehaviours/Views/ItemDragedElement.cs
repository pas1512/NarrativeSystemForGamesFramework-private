using UnityEngine;
using UnityEngine.EventSystems;
using ScriptsUtilities.Views.MouseDraged;
using MyFramework.InventorySystem.Items;
using System;
using static UnityEngine.EventSystems.PointerEventData;

namespace MyFramework.InventorySystem
{
    [RequireComponent(typeof(SlotView), typeof(RectTransform))]
    public class ItemDragedElement : DragedElement,
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        [SerializeField] private DragItem _dragElement;

        private SlotView _slotView;

        private static ItemDragedElement _selected;
        public static ItemDragedElement Selected => _selected;

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
            if (_slotView.notInitialized)
                return false;

            if (_slotView.TryTakeItem(button, out IItem taked))
            {
                _dragElement.Init(taked, button);
                _dragElement.On();
                return true;
            }

            return false;
        }

        public override RectTransform GetControl()
        {
            return _dragElement.rectTransfrom;
        }

        public override void Complete()
        {
            SlotView selectedSlot = _selected == null ? null : _selected._slotView;

            if (_dragElement.haveItem)
            {
                IItem rest = _dragElement.dragedItem;
                InputButton button = _dragElement.button;
                _slotView.MakeExchange(rest, selectedSlot, button);   
                _dragElement.Realise();
                _dragElement.Off();
            }
        }
    }
}