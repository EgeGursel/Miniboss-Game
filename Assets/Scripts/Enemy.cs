using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private State currentState;

    [SerializeField]
    private int
        maxHealth;
    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        knockbackDuration;
    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private ParticleSystem blood;
    private int
        currentHealth,
        facingDirection,
        damageDirection;
    private float
        knockbackStartTime;
    private Vector2
        movement;
    private bool
        groundDetected,
        wallDetected;

    public GameObject alive;
    public Animator aliveAnim;
    public Rigidbody2D aliveRB;

    public Transform attackArea;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public int attackDamage = 30;
    void Start()
    {
        currentHealth = maxHealth; 
        alive = transform.Find("Alive").gameObject;
        aliveAnim = alive.GetComponent<Animator>();
        aliveRB = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }
    // MOVEMENT
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }
    
    private void Update()
    {
        AttackPlayer();
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }
    // WALKING STATE
    private void EnterWalkingState()
    {

    }
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }
    private void ExitWalkingState()
    {

    }
    // KNOCKBACK STATE
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }
    private void UpdateKnockbackState()
    {
        if (Time.time > knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Walking); 
        }
    }
    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }
    // DEAD STATE
    private void EnterDeadState()
    {
        // SPAWN BLOOD PARTICLES & CHUNKS
        blood.Play();

        Destroy(gameObject);
    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }

    // OTHER FUNCTIONS
    private void AttackPlayer()
    {
        // DETECT ENEMIES IN RANGE OF ATTACK
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackArea.position, attackRange, playerLayer);

        // DAMAGE ENEMIES
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().TakeDamage(attackDamage);
        }
        return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void Damage(int damage)
    {
        currentHealth -= damage;
        // ADD HURT ANIMATION
        if (currentHealth > 0)
        {
            SwitchState(State.Knockback);
        }
        if (currentHealth < 0)
        {
            SwitchState(State.Dead);
        }
    }
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
    }
}
