using UnityEngine;
using UnityEngine.UI;

namespace MyFramework.InventorySystem.View
{
    public class ShopSectionView : InventoryView
    {
        [SerializeField] private Text _number;

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

        private void RenewPrice()
        {
            _number.text = container.FullPrice.ToString();
        }
    }
}