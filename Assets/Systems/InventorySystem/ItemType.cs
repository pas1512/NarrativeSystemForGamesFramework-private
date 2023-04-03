using UnityEngine;

namespace MyFramework.InventorySystem
{
    public class ItemType : ScriptableObject
    {
        [SerializeField] private Sprite _image;
        public Sprite Image => _image;

        [SerializeField] private string _name;
        public string Name => _name;
    }
}