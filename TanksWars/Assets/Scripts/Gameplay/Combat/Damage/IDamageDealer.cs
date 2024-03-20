namespace Gameplay.Combat.Damage
{
    public interface IDamageDealer
    {
        public void DealDamage(IDamageTaker damageTaker);
        public void SetDamageOwnerId(ulong ownerId);
    }
}
