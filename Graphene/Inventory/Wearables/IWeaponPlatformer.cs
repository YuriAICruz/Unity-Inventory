namespace Graphene.Inventory.Wearables
{
    public interface IWeaponPlatformer
    {
        void Use(float delay, float duration);
        void SetEnabled(float delay, float duration);
    }
}