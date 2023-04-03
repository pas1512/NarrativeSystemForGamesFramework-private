using System;
using UnityEngine;
using MyFramework.InventorySystem.Interfaces;

namespace MyFramework.InventorySystem
{
    public class GlobalInventory
    {
        public static event Action<Transform, IItem[]> OnItemAdded;

        public static void AddItems(Transform origin, IItem[] items)
        {
            OnItemAdded?.Invoke(origin, items);
        }
    }
}
