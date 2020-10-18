using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable, IDamagable, IProducer
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

    // Produce
    [SerializeField] Producer producer = null;
    [SerializeField] Transform spawnTransform = null;


    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthBar.Setup(healthSystem);

        GetProducer().CreateProduct += CreateProduct;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case UnitState.Stop:
                {
                    break;
                }
            case UnitState.Producing:
                {
                    Produce();
                    break;
                }
        }
    }

    // ISelectable -----------------------------------------------------------------------------------
    public GameObject GetGameObject() => gameObject;

    public UnitState GetState() => state;

    public void HideHighlight() => selectionHighlight.SetActive(false);

    public void SetState(UnitState state)
    {
        if (state != UnitState.Moving && state != UnitState.Attacking)
            this.state = state;
    }

    public void ShowHighlight() => selectionHighlight.SetActive(true);

    // IDamagable -----------------------------------------------------------------------------------
    public void DealDamage(DamageTypes damage) => healthSystem.Damage(defence.CalculateDamage(damage));

    public void RestoreHealth(float healAmount) => healthSystem.Heal(healAmount);

    public Collider2D GetCollider() => GetComponent<Collider2D>();

    public float GetHealth() => healthSystem.GetHealth();

    public float GetHealthPercentage() => healthSystem.GetHealthPercentage();

    public bool IsDead() => GetHealth() == 0f;

    // IProducer ------------------------------------------------------------------------------

    public Producer GetProducer() => producer;

    public void Produce()
    {
        GetProducer().Producing();
        if (!GetProducer().Working())
            SetState(UnitState.Stop);
    }

    public void StartProducing(int index)
    {
        GetProducer().Produce(index);
        SetState(UnitState.Producing);
    }

    public void CreateProduct(object sender, EventArgs e) => Instantiate(GetProducer().GetDoneProduct().GetResult(), spawnTransform.position, Quaternion.identity);
}
