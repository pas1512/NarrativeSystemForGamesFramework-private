using UnityEngine;
using UnityEngine.UI;
using ScriptsUtilities.Views.ItemViewContainer;
using MyFramework.InventorySystem.Items;
using static UnityEngine.EventSystems.PointerEventData;

namespace MyFramework.InventorySystem
{
    public class SlotView : InfoView<Slot>
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _image;
        [SerializeField] private Text _number;

        private Slot _slot;
        private InventoryView _owner;

        public void Init(InventoryView owner, Slot slot)
        {
            _slot = slot;
            _owner = owner;
        }

        public bool IsEmpty()
        { 
            return _slot == null || _slot.Empty;
        }

        public bool TryTakeItem(InputButton button, out IItem item)
        {
            if (_slot.Empty)
            {
                item = null;
                return false;
            }

            bool taked = false;

            switch (button)
            {
                case InputButton.Left:
                    taked = _slot.TryTakeAll(out item);
                    break;
                case InputButton.Right:
                    taked = _slot.TryTakePart(out item);
                    break;
                default:
                    item = null;
                    return false;
            }

            if(taked && _owner.duplicateDrag)
                _slot.EnforceSoft((IItem)item.Clone());

            return taked;
        }

        public bool CanExchange(SlotView other)
        {
            if(other == null)
                return false;

            return _owner.ExchangeAllowed(other._owner);
        }

        public void MakeExchange(IItem item, SlotView origin, InputButton button)
        {
            IItem rest = item;

            bool originIsNotNull = origin != null;

            if (CanExchange(origin))
            {
                if (origin._owner.duplicateDrag)
                    rest = null;
                else if (button == InputButton.Left)
                    rest = origin._slot.EnforceHard(rest);
                else if (button == InputButton.Right)
                    rest = origin._slot.EnforceSoft(rest);
            }

            if (_owner.duplicateDrag)
                return;

            if(originIsNotNull)
            {
                if (rest != null)
                    rest = _slot.EnforceHard(rest);

                if (rest != null)
                    rest = _owner.inventory.TryAdd(rest);
            }

            if (rest != null)
                GlobalInventory.AddItems(_owner.inventory.transform, rest);
        }

        protected sealed override void UpdateInfo()
        {
            if (IsEmpty())
            {
                _image.enabled = false;
                _name.enabled = false;
                _number.enabled = false;
            }
            else
            {
                _image.sprite = _slot.View.StateImage;
                _image.enabled = true;

                _name.text = _slot.View.Name;
                _name.enabled = true;

                if (_slot.View.Number > 1)
                {
                    _number.text = _slot.View.Number.ToString();
                    _number.enabled = true;
                }
                else
                {
                    _number.enabled = false;
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _slot = null;
            _name = null;
            _image = null;
            _number = null;
        }
    }
}