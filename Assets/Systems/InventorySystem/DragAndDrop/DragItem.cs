using UnityEngine;
using UnityEngine.UI;
using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem.DragEndDrop
{
    [RequireComponent(typeof(RectTransform))]
    public class DragItem : MonoBehaviour
    {
        [SerializeField] private Image _view;

        private RectTransform _transfrom;
        public RectTransform rectTransfrom => _transfrom;

        private IItem _dragetItem;
        public IItem dragedItem => _dragetItem;

        private bool _haveItem;
        public bool haveItem => _haveItem;

        private void OnEnable()
        {
            _transfrom = GetComponent<RectTransform>();
        }

        public void Init(IItem itemView)
        {
            _dragetItem = itemView;
            _haveItem = true;
            _view.sprite = itemView.DragImage;
        }

        public void Realise()
        {
            _haveItem = false;
            _dragetItem = null;
            _view.sprite = null;
        }

        public void On()
        {
            gameObject.SetActive(true);
        }

        public void Off()
        {
            gameObject.SetActive(false);
        }
    }
}