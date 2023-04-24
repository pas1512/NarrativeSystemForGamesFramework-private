namespace MyFramework.InventorySystem.Items
{
    public interface IQualitable
    {
        public float Price { get; }
        public float Quality();
    }
}