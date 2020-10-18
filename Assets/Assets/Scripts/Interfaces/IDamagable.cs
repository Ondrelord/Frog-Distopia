using UnityEngine;

public interface IDamagable
{
    void DealDamage(DamageTypes damage);
    void RestoreHealth(float healAmount);

    Vector2 GetPosition();

    float GetHealth();
    float GetHealthPercentage();
    bool IsDead();

}