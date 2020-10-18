using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    Stop,
    Attacking,
    Moving,
    Producing
}

public class Unit : MonoBehaviour, ISelectable, IDamagable, IAttacker, IMover
{
    // General
    private Rigidbody2D rb;

    UnitState state = UnitState.Stop;

    // Selection
    [SerializeField] private GameObject selectionHighlight = null;

    // Health
    private HealthSystem healthSystem;
    [SerializeField] private HealthBar healthBar = null;
    [SerializeField] private float health = 100f;

    // Attack
    private IDamagable attackTarget;
    [SerializeField] Attack basicAttack = null;

    // Defence
    [SerializeField] Defence defence = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = new HealthSystem(health);
        healthBar.Setup(healthSystem);
        basicAttack = Instantiate(basicAttack);
    }

    // Update is called once per frame
    void Update()
    {
        //obsolete if (attackTimer >= 0) attackTimer -= Time.deltaTime;


        switch (state)
        {
            case UnitState.Stop:
                {
                    break;
                }
            case UnitState.Moving:
                {
                    Moving();
                    if (StoppedMoving())
                        SetState(UnitState.Stop);
                    break;
                }
            case UnitState.Attacking:
                {
                    Attack();
                    break;
                }
        }
    }

    // Sets state of this unit to "state". If the state is not possible for this unit, unit will keep original state.
    public void SetState(UnitState state) => this.state = state != UnitState.Producing ? state : this.state;

    public UnitState GetState() => state;

    // ISelectable -----------------------------------------------------------------------------------
    // Hides highlightning of this object. 
    public void HideHighlight() => selectionHighlight.SetActive(false);

    // Highlights this object.
    public void ShowHighlight() => selectionHighlight.SetActive(true);

    // Returns this gameObject.
    public GameObject GetGameObject() => gameObject;

    // IDamagable -----------------------------------------------------------------------------------
    // Deal damage to this object.
    public void DealDamage(DamageTypes damage) => healthSystem.Damage(defence.CalculateDamage(damage));

    // Restores healAmount of health up to a maximum.
    public void RestoreHealth(float healAmount) => healthSystem.Heal(healAmount);

    // Returns position of this object.
    public Collider2D GetCollider() => GetComponent<Collider2D>();

    // Returns current flat amount of health.
    public float GetHealth() => healthSystem.GetHealth();

    // Returns health as percentage;
    public float GetHealthPercentage() => healthSystem.GetHealthPercentage();

    // Returns true if have zero health.
    public bool IsDead() => GetHealth() == 0f;

    // IAttacker -----------------------------------------------------------------------------------
    // Attacks the last attacked target.
    public void Attack() => Attack(GetTarget());

    // Attacks target and sets as current target.
    public void Attack(IDamagable target)
    {
        if (target == null)
            return;

        if (target.IsDead())
        {
            SetState(UnitState.Stop);
            SetTarget(null);
            return;
        }

        SetTarget(target);

        if (InRange())
        {
            if (basicAttack.Ready())
            {
                target.DealDamage(basicAttack.GetDamage());
                basicAttack.ResetCooldown();
            }
        }
        else
            MoveTo(target.GetCollider().ClosestPoint(transform.position));
    }

    public bool InRange() => Vector2.Distance(transform.position , attackTarget.GetCollider().ClosestPoint(transform.position)) < basicAttack.GetRange();
    
    // Sets target as current target.
    public void SetTarget(IDamagable target)
    {
        attackTarget = target;
    }

    // Returns current target.
    public IDamagable GetTarget() => attackTarget;

    // IMover -------------------------------------------------------------------------------------------------
    // Gives move command to this object
    public void MoveTo(Vector2 position) => GetMovement().Move(position);

    // Update move loop
    public void Moving() => GetMovement().Moving();
    public bool StoppedMoving() => GetMovement().Stopped();
    
    // Returns Movement script.
    public IMovement GetMovement() => GetComponent<IMovement>();
}
