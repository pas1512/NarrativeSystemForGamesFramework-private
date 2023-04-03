using UnityEngine;
using UnityEngine.UI;
using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem.DragEndDrop
{
    [RequireComponent(typeof(RectTransform))]
    public class DragItem : MonoBehaviour
    {
        [SerializeField] private Image _view;

        private GameObject _object;

        private RectTransform _transfrom;
        public RectTransform rectTransfrom => _transfrom;

        private IItem _dragetItem;
        public IItem dragedItem => _dragetItem;

        private bool _haveItem;
        public bool haveItem => _haveItem;

        private void Awake()
        {
            _transfrom = GetComponent<RectTransform>();
            _object = gameObject;
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
            _object.SetActive(true);
        }

        public void Off()
        {
            _object.SetActive(false);
        }
    }
}