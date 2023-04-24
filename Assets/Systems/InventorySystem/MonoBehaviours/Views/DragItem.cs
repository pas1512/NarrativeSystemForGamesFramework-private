using UnityEngine;
using UnityEngine.UI;
using MyFramework.InventorySystem.Items;
using static UnityEngine.EventSystems.PointerEventData;

namespace MyFramework.InventorySystem
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

        private InputButton _button;
        public InputButton button => _button;

        private void OnEnable()
        {
            _transfrom = GetComponent<RectTransform>();
        }

        public void Init(IItem itemView, InputButton button)
        {
            _dragetItem = itemView;
            _haveItem = true;
            _view.sprite = itemView.DragImage;
            _button = button;
        }

        public void Realise()
        {
            _haveItem = false;
            _dragetItem = null;
            _view.sprite = null;
            _button = default;
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