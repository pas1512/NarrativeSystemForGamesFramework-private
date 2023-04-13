using UnityEngine;
using UnityEngine.UI;
using MyFramework.InventorySystem.Interfaces;
using MyFramework.InventorySystem.View;
using System;
using Assets.ScriptsUtilities.Views;

namespace MyFramework.InventorySystem.Actions
{
    [RequireComponent(typeof(RectTransform))]
    public class TradeBattons : MonoBehaviour
    {
        public enum MakeDealType
        {
            WithClose,
            Complite
        }

        [SerializeField] private InventoryView _customer;
        [SerializeField] private InventoryView _seller;
        [SerializeField] private ShopSectionView _sellSection;
        [SerializeField] private ShopSectionView _baySection;
        [SerializeField] private MessageView _quetion;
        [SerializeField] private MessageView _message;

        public void EndTradeProcess()
        {
            if (_sellSection.fullPrice < _baySection.fullPrice)
            {
                Debug.Log("Нерівноцінний обмін. Дайте більше");
                _message.Show("Нерівноцінний обмін. Дайте більше");
                return;
            }
            else ApplyDeal();
        }

        public void ApplyDeal()
        {
            IItem[] selledItems = _baySection.inventory.GetItems();
            _baySection.inventory.Clear();
            IItem rest = _customer.inventory.TryAdd(selledItems);

            if (rest != null)
                GlobalInventory.AddItems(_customer.inventory.transform, new IItem[] { rest });

            IItem[] bayedItems = _sellSection.inventory.GetItems();
            _sellSection.inventory.Clear();
            rest = _seller.inventory.TryAdd(bayedItems);

            if (rest != null)
                GlobalInventory.AddItems(_seller.inventory.transform, new IItem[] { rest });

            rest = null;
        }

        private void CencelDeal()
        {
            IItem[] selledItems = _baySection.inventory.GetItems();
            IItem rest = _customer.inventory.TryAdd(selledItems);

            if (rest != null)
                GlobalInventory.AddItems(_seller.inventory.transform, new IItem[] { rest });

            IItem[] bayedItems = _sellSection.inventory.GetItems();
            rest = _seller.inventory.TryAdd(bayedItems);

            if (rest != null)
                GlobalInventory.AddItems(_customer.inventory.transform, new IItem[] { rest });
        }
    }
}