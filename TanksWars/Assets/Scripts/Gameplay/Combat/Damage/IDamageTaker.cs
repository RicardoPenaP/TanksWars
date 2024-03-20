namespace Gameplay.Combat.Damage
{
    public interface IDamageTaker 
    {
        public void TakeDamage(int amount);
        public ulong GetClientOwnerID();
    }
}
