using UnityEngine;

namespace MyFramework.InventorySystem.Types
{
    public sealed class NonExistent : ItemType 
    {
        public new string Name => "нічого";

        public static NonExistent CreateInstance()
        {
            return CreateInstance<NonExistent>();
        }
    }
}