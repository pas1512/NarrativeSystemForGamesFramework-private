using MyFramework.InventorySystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace MyFramework.InventorySystem
{
    public class ShopSectionView : InventoryView
    {
        [SerializeField] private Text _number;
        [Range(0.1f, 5f), SerializeField] private float _coficient;

        public float fullPrice => container.FullPrice * _coficient;

        internal void Init(string id,
            ExchangeType exchangeOptions,
            ExchangeOptions[] options,
            float coficient)
        {
            Init(id, exchangeOptions, options);
            _coficient = coficient;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            container.OnInfoChanged += RenewPrice;

            foreach (var slot in container.info)
                slot.OnChanged += RenewPrice;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            container.OnInfoChanged -= RenewPrice;

            foreach (var slot in container.info)
                slot.OnChanged -= RenewPrice;
        }

        public void ReleaseTo(InventoryView target)
        {
            IItem[] releasedItems = inventory.GetItems();
            inventory.Clear();
            IItem rest = null;

            if (!target.duplicateDrag)
                rest = target.inventory.TryAdd(releasedItems);

            if (rest != null)
                GlobalInventory.AddItems(target.inventory.transform, new IItem[] { rest });
        }

        private void RenewPrice()
        {
            _number.text = fullPrice.ToString();
        }
    }
}