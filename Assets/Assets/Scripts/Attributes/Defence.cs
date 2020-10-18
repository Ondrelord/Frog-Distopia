using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Defence", menuName ="Attributes/Defence")]
public class Defence : ScriptableObject
{
    [SerializeField] DamageTypes resistances = new DamageTypes(2f);

    public DamageTypes GetResistances() => resistances;
    public float CalculateDamage(DamageTypes damage) => (damage-resistances).Sum();
}
