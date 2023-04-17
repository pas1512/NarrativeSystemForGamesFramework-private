using UnityEngine;

namespace MyFramework.InventorySystem.Types
{
    public class ItemType : ScriptableObject
    {
        [SerializeField] private Sprite _image;
        public Sprite Image => _image;

        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private float _price;
        public float Price => _price;

        [SerializeField] private float _preciousness;
        public float Preciousness => _preciousness;
    }
}