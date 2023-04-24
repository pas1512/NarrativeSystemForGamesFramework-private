using UnityEngine;
using ScriptsUtilities.Views.ItemsContainer;
using System.Linq;

namespace MyFramework.InventorySystem
{
    public class InventoryView : InfoViewContainer<Inventory, SlotView, Slot> 
    {
        [SerializeField] private string _id = "none";
        public string id => _id;

        [SerializeField] private bool _duplicateDrag = false;
        public bool duplicateDrag => _duplicateDrag;

        [SerializeField] private bool _selfExchangeAllowed = true;
        [SerializeField] private ExchangeType _exchangeOptions;
        [SerializeField] private ExchangeOptions[] _options;

        public Inventory inventory => container;

        internal void Init(string id, 
            ExchangeType exchangeOptions,
            ExchangeOptions[] options)
        {
            _id = id;
            _exchangeOptions = exchangeOptions;
            _options = options;
        }

        private bool ActionAllowed(ExchangeType type, bool haveInOptions)
        {
            int typeValue = (int)type;

            if (typeValue < 2)
                return typeValue == 1;
            else
                return haveInOptions ^ (typeValue == 2);
        }

        public bool ExchangeAllowed(InventoryView other)
        {
            if (_selfExchangeAllowed && _id == other._id)
                return true;

            bool otherSetedReturns = other._options.Any(o => o.SetedReturn(id));
            bool otherReturnAllowed = ActionAllowed(other._exchangeOptions,
                otherSetedReturns);

            if (!otherReturnAllowed)
                return false;

            bool setedTaking = _options.Any(o => o.SetedTaking(other.id));
            bool takingAllowed = ActionAllowed(_exchangeOptions, setedTaking);

            return takingAllowed;
        }

        protected override void InitInfoElement(SlotView element, Slot info)
        {
            element.Init(this, info);
            base.InitInfoElement(element, info);
        }
    }
}