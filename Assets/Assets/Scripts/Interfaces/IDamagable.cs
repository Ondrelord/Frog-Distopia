using UnityEngine;

public interface IDamagable
{
    void DealDamage(DamageTypes damage);
    void RestoreHealth(float healAmount);

    Collider2D GetCollider();

    float GetHealth();
    float GetHealthPercentage();
    bool IsDead();

}