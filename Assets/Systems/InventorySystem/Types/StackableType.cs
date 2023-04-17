using UnityEngine;

namespace MyFramework.InventorySystem.Types
{
    public class StackableType : ItemType
    {
        [SerializeField] private int _maxNumber;
        public int MaxNumber => _maxNumber;
    }
}