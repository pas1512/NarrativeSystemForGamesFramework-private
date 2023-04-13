﻿using UnityEngine;
using UnityEngine.EventSystems;
using ScriptsUtilities.Views.MouseDraged;
using MyFramework.InventorySystem.View;
using MyFramework.InventorySystem.Interfaces;

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

        public virtual bool AllowedExchange()
        {
            if (_selected == null)
                return false;

            Inventory originInventory = _slotView.owner;
            Inventory targetInventory = _selected._slotView.owner;

            return targetInventory.ExchangeAllowed(originInventory);
        }

        public override bool Init(PointerEventData.InputButton button)
        {
            if (_slotView.notInitialized &&
                _slotView.Slot.Empty)
                return false;

            IItem takedResult = null;
            bool taked = false;
            TakedType type = TakedType.None;

            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    taked = _slotView.Slot.TryTakeAll(out takedResult);
                    type = TakedType.Main;
                    break;
                case PointerEventData.InputButton.Right:
                    taked = _slotView.Slot.TryTakePart(out takedResult);
                    type = TakedType.Alternative;
                    break;
                case PointerEventData.InputButton.Middle:
                default:
                    return false;
            }

            if(taked)
            {
                if (_slotView.owner.duplicateDrag)
                    _slotView.Slot.EnforceSoft(takedResult);

                _dragElement.Init(takedResult, type);
                _dragElement.On();
            }

            return taked;
        }

        public override RectTransform GetControl()
        {
            return _dragElement.rectTransfrom;
        }

        public override void Complete()
        {
            bool exchangeAllowed = AllowedExchange();

            if (exchangeAllowed && _selected == null)
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

                if(exchangeAllowed)
                {
                    if (_selected._slotView.owner.duplicateDrag)
                        rest = null;
                    else if(_dragElement.takedType == TakedType.Main)
                        rest = _selected._slotView.Slot.EnforceHard(rest);
                    else
                        rest = _selected._slotView.Slot.EnforceSoft(rest);
                }

                if (_slotView.owner.duplicateDrag)
                    rest = null;

                if (rest != null)
                    rest = _slotView.Slot.EnforceHard(rest);

                if (rest != null)
                    rest = _slotView.owner.TryAdd(rest);

                if (rest != null)
                    GlobalInventory.AddItems(_slotView.owner.transform, new IItem[] { _dragElement.dragedItem });

                _dragElement.Realise();
                _dragElement.Off();
            }
        }
    }
}