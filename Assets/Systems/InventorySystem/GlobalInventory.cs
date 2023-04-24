using System;
using UnityEngine;
using MyFramework.InventorySystem.Items;

namespace MyFramework.InventorySystem
{
    public class GlobalInventory
    {
        public static event Action<Transform, IItem[]> OnItemAdded;

        public static void AddItems(Transform origin, params IItem[] items)
        {
            OnItemAdded?.Invoke(origin, items);
        }
    }
}
