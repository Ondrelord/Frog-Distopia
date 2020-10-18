using UnityEngine;

// Holds damage types for attacks and defences. With basic operations.
[System.Serializable]
public struct DamageTypes
{
    public float physical;
    public float magical;

    public DamageTypes(float physical, float magical = 0f)
    {
        this.physical = physical;
        this.magical = magical;
    }

    // Sums types together.
    public float Sum() => physical + magical;

    // Substracts resistances from damages. Cannot go bellow zero.
    public static DamageTypes operator -(DamageTypes a, DamageTypes b)
    {
        return new DamageTypes(
            a.physical - b.physical < 0 ? 0f : a.physical - b.physical,
            a.magical - b.magical < 0 ? 0f : a.magical - b.magical
            );
    }

    public static DamageTypes operator +(DamageTypes a, DamageTypes b) => new DamageTypes(a.physical + b.physical, a.magical + b.magical);
    public static DamageTypes operator *(DamageTypes a, DamageTypes b)
    {
        return new DamageTypes(
            a.physical * b.physical < 0 ? 0f : a.physical * b.physical,
            a.magical * b.magical < 0 ? 0f : a.magical * b.magical
            );
    }
}
