namespace MyFramework.InventorySystem.Interfaces
{
    public interface IQualitable
    {
        public float Price { get; }
        public float Quality();
    }
}