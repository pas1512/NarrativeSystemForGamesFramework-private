using UnityEngine;
using UnityEngine.UI;
using ScriptsUtilities.Views.ItemViewContainer;

namespace MyFramework.InventorySystem.View
{
    public class SlotView : InfoView
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _image;
        [SerializeField] private Text _number;

        private Slot _slot;
        public Slot Slot => _slot;

        private Inventory _owner;
        public Inventory owner => _owner;

        public void Init(Inventory inventory, Slot slot)
        {
            _slot = slot;
            _owner = inventory;
            ObserTo(slot);
        }

        public bool IsEmpty()
        { 
            return _slot == null || _slot.Empty;
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