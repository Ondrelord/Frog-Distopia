using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Attributes/Attack")]
public class Attack : ScriptableObject
{
    [SerializeField] DamageTypes damage = new DamageTypes( 10f);
    [SerializeField] float range = 1f;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float timer = 0f;

    // Initialize.
    public void Awake()
    {
        TimeManager TM = FindObjectOfType<TimeManager>();
        if (TM != null) TM.Tick += AttackTimer_Tick;
    }

    // Cooling down.
    private void AttackTimer_Tick(object sender, System.EventArgs e)
    {
        if (timer >= 0f) timer -= Time.deltaTime;
    }

    // True if timer is 0.
    public bool Ready() => timer < 0f;

    // Sets timer to cooldown. 
    public void ResetCooldown() => timer = cooldown;

    public DamageTypes GetDamage() => damage;
    public float GetRange() => range;
    
    // Sets timer to time.
    public void SetTimer(float time) => timer = time;
    public float GetTimer() => timer;
}
