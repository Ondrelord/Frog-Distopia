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
        switch (state)
        {
            case UnitState.Stop:
                {
                    break;
                }
            case UnitState.Producing:
                {
                    Producing();
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


    // Producer Placeholder ------------------------------------------------------------------------------
    
    // products that can be created
    public Product[] products;

    // currently producing
    Product producing = null;
    // products to be produced
    Queue<Product> productQueue = new Queue<Product>();
    [SerializeField] int productMaxQueueLenght = 5;

    // progress of current production
    float producingProgress;
    [SerializeField] UnityEngine.UI.Image producingProgressBar = null;
    // position where new objects are spawned
    [SerializeField] Transform spawnTransform = null;

    // Command to Start producing (or enqueue if already producing).
    public void Produce(Product product)
    {
        if (producing == null)
        {
            producing = product;
            SetState(UnitState.Producing);
        }
        else if (productQueue.Count < productMaxQueueLenght)
            productQueue.Enqueue(product);

    }

    // Update loop function. Continues producing until no products are in queue.
    public void Producing()
    {
        // contiue in production
        if (producing != null)
        {
            if (producingProgress < producing.GetTimeToProduce())
            {
                producingProgress += Time.deltaTime;
                producingProgressBar.fillAmount = producingProgress / producing.GetTimeToProduce();
            }
            else
            {
                // create product
                Instantiate(producing.GetResult(), spawnTransform.position, Quaternion.identity);
                // reset progress
                producingProgress = 0f;
                producingProgressBar.fillAmount = 0;
                // next in queue
                if (productQueue.Count != 0)
                    producing = productQueue.Dequeue();
                else
                {
                    producing = null;
                    SetState(UnitState.Stop);
                }
            }
        }
        else
            SetState(UnitState.Stop);
    }





}
