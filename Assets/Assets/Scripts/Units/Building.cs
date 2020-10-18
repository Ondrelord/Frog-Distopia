using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable, IDamagable
{
    UnitState state = UnitState.Stop;

    // Selection
    [SerializeField] private GameObject selectionHighlight = null;

    // Health
    HealthSystem healthSystem;
    [SerializeField] HealthBar healthBar = null;
    float health = 100f;

    // Defence
    [SerializeField] Defence defence = null;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ISelectable -----------------------------------------------------------------------------------
    public GameObject GetGameObject() => gameObject;

    public UnitState GetState() => state;

    public void HideHighlight() => selectionHighlight.SetActive(false);

    public void SetState(UnitState state) => this.state = state;

    public void ShowHighlight() => selectionHighlight.SetActive(true);

    // IDamagable -----------------------------------------------------------------------------------
    public void DealDamage(DamageTypes damage) => healthSystem.Damage(defence.CalculateDamage(damage));

    public void RestoreHealth(float healAmount) => healthSystem.Heal(healAmount);

    public Collider2D GetCollider() => GetComponent<Collider2D>();

    public float GetHealth() => healthSystem.GetHealth();

    public float GetHealthPercentage() => healthSystem.GetHealthPercentage();

    public bool IsDead() => GetHealth() == 0f;
}
