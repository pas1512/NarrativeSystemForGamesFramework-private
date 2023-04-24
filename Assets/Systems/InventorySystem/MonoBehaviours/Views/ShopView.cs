using UnityEngine;
using Assets.ScriptsUtilities.Views;

namespace MyFramework.InventorySystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private InventoryView _customer;
        [SerializeField] private InventoryView _seller;
        [SerializeField] private ShopSectionView _sellSection;
        [SerializeField] private ShopSectionView _baySection;

        [Header("QuetionViewSection")]
        [SerializeField] private MessageView _quetion;
        [TextArea, SerializeField] private string _nonequalExchangeQuestions;

        [Header("MessageViewSection")]
        [SerializeField] private MessageView _message;
        [TextArea, SerializeField] private string _nonequalExchangeMessage;
        [TextArea, SerializeField] private string _customerNotEnoughSpace;
        [TextArea, SerializeField] private string _sellerNotEnoughSpace;

        public void ApplyDeal()
        {
            bool constrainedSize = !_customer.duplicateDrag;
            int availableSlotsNumber = _customer.inventory.EmptySlotsNumber;
            int requiredSlotsNumber = _baySection.inventory.NonEmptySlotsNumber;

            if (constrainedSize &&
                availableSlotsNumber < requiredSlotsNumber)
            {
                //¬и не можете зд≥йснити данний обм≥н через недостатнью к≥льк≥сть в≥льних м≥сць у вашому ≥нвентар≥
                _message.Show(_customerNotEnoughSpace);
                return;
            }

            constrainedSize = !_seller.duplicateDrag;
            availableSlotsNumber = _seller.inventory.EmptySlotsNumber;
            requiredSlotsNumber = _sellSection.inventory.NonEmptySlotsNumber;

            if (constrainedSize &&
                availableSlotsNumber < requiredSlotsNumber)
            {
                //¬и не можете зд≥йснити данний обм≥н через недостатнью к≥льк≥сть в≥льних м≥сць у ≥нвентар≥ продавц€
                _message.Show(_sellerNotEnoughSpace);
                return;
            }

            if (_sellSection.fullPrice < _baySection.fullPrice)
            {
                //¬и не можете зд≥йснити данний обм≥н через нер≥вноц≥нн≥сть
                _message.Show(_nonequalExchangeMessage);
                return;
            }
            else if (_sellSection.fullPrice > _baySection.fullPrice)
            {
                //¬и даЇте б≥льше. ¬и впевнен≥ що хочете зд≥йснити обм≥н?
                _quetion.Show(_nonequalExchangeQuestions, QuestionAnswerHendler);
                return;
            }
            else
            {
                _baySection.ReleaseTo(_customer);
                _sellSection.ReleaseTo(_seller);
            }
        }

        public void CancelDeal()
        {
            _baySection.ReleaseTo(_seller);
            _sellSection.ReleaseTo(_customer);
        }

        public void Show(Inventory customer, Inventory seller)
        {
            _customer.ResetContainer(customer);
            _seller.ResetContainer(seller);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            CancelDeal();
            gameObject.SetActive(false);
        }

        private void QuestionAnswerHendler(int i)
        {
            if (i == 1)
            {
                _baySection.ReleaseTo(_customer);
                _sellSection.ReleaseTo(_seller);
            }
        }
    }
}