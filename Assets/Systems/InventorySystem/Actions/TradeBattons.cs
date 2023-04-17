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
            bool constrainedSize = !_customer.inventory.duplicateDrag;
            int availableSlotsNumber = _customer.inventory.EmptySlotsNumber;
            int requiredSlotsNumber = _baySection.inventory.NonEmptySlotsNumber;
            
            if (constrainedSize &&
                availableSlotsNumber < requiredSlotsNumber)
            {
                string messageString = "�� �� ������ �������� ������ ���� �����" +
                    " ����������� ������� ������ ���� � ������ ��������";
                Debug.Log(messageString);
                _message.Show(messageString);
                return;
            }

            constrainedSize = !_seller.inventory.duplicateDrag;
            availableSlotsNumber = _seller.inventory.EmptySlotsNumber;
            requiredSlotsNumber = _sellSection.inventory.NonEmptySlotsNumber;

            if (constrainedSize &&
                availableSlotsNumber < requiredSlotsNumber)
            {
                string messageString = "�� �� ������ �������� ������ ���� �����" +
                    " ����������� ������� ������ ���� � �������� ��������";
                Debug.Log(messageString);
                _message.Show(messageString);
                return;
            }

            if (_sellSection.fullPrice < _baySection.fullPrice)
            {
                Debug.Log("�� �� ������ �������� ������ ���� ����� �������������");
                _message.Show("�� �� ������ �������� ������ ���� ����� �������������");
                return;
            }
            else if(_sellSection.fullPrice > _baySection.fullPrice)
            {
                string quetionString = "�� ���� �����. �� ������� �� ������ �������� ����?";
                Debug.Log(quetionString);
                _quetion.Show(quetionString, QuestionAnswerHendler);
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

        private void QuestionAnswerHendler(int i)
        {
            if (i == 1)
                ApplyDeal();
        }
    }
}