using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // WEAPON & ITEMS & ANIMATIONS & SCENE & INFO BAR MANAGEMENT
    public GameObject infoBar;
    public GameObject boss;
    public GameObject sceneLoader;
    public GameObject weapon;
    public ParticleSystem dust;
    public Animator playerAnimator;
    public Animator weaponAnimator;

    // INVENTORY
    //private Inventory inventory;
    // public PickUp pickUp;
    // public GameObject effect;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    public CharacterController2D controller;
    public float runSpeed = 26f;
    float horizontalMove = 0f;
    bool jump = false;

    // ATTACK
    public Transform attackArea;
    public float attackRange = 0.55f;
    public LayerMask enemyLayer;
    public int attackDamage = 30;
    public float attackCooldown = 0.4f;
    private bool attackCD = true;
    private bool weaponActive;

    // DASH
    private Rigidbody2D rb;
    private KeyCode lastKeyCode;
    private bool dashCD = true;
    private bool dashActive = false;
    private float InitialTouch;
    private float touchDelay = 0.3f;
    bool isDashing = false;
    public float dashDistance = 15f;
    private float dashCooldown = 0.5f;


    private void Start()
    {
        // inventory = GetComponent<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
    }
    private void Update()
    {
        // PLAYER MOVEMENT
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && weaponActive)
        {
            Attack();
        }

        if (dashActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time < InitialTouch + touchDelay && lastKeyCode == KeyCode.A)
                {
                    PlayerDash(-1);
                }
                lastKeyCode = KeyCode.A;
                InitialTouch = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time < InitialTouch + touchDelay && lastKeyCode == KeyCode.D)
                {
                    PlayerDash(1);
                }
                lastKeyCode = KeyCode.D;
                InitialTouch = Time.time;
            }
        }

        if (gameObject.transform.position.y < -24)
        {
            Die();
        }
        
    }
    void FixedUpdate()
    {
        // PLAYER JUMP
        if (!isDashing)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
    public void CreateDust()
    {
        dust.Play();
    }
    void Attack()
    {
        if (attackCD)
        {
            // DETECT ENEMIES IN RANGE OF ATTACK
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayer);

            // DAMAGE ENEMIES
            foreach (Collider2D enemy in hitEnemies)
            {
                try
                {
                    enemy.GetComponent<Enemy>().Damage(attackDamage);
                }
                catch
                {
                    enemy.GetComponent<Boss>().Damage(attackDamage);
                }
            }
            weaponAnimator.SetTrigger("Attack");
            CameraShake.Instance.Shake(2f, .16f);
            StartCoroutine(AttackCooldown());
        }
    }
    void PlayerDash(int direction)
    {
        if (dashCD)
        {
            StartCoroutine(Dash(direction));
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator Dash(float direction)
    {
        isDashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(10f);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerAnimator.SetTrigger("Damaged");
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        sceneLoader.GetComponent<SceneLoader>().Load("DeathScene");
    }

    IEnumerator AttackCooldown()
    {
        attackCD = false;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = true;
    }
    IEnumerator DashCooldown()
    {
        dashCD = false;
        yield return new WaitForSeconds(dashCooldown);
        dashCD = true;
    }

    // COLLECTABLES
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Collectable"))
        {
            return;
        }

        string collectableName = collision.gameObject.name;

        // NON-INVENTORY COLLECTABLES
        if (collectableName == "Dash Power up")
        {
            dashActive = true;
        }

        // INVENTORY COLLECTABLES
        else
        {
            if (collectableName == "Katana")
            {
                boss.SetActive(true);
            }

            // IMPLEMENT INVENTORY GUI HERE!!!

            foreach (Transform child in transform)
            {
                if (collectableName == child.name)
                {
                    child.gameObject.SetActive(true);
                    weaponActive = true;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        infoBar.SetActive(true);
        Destroy(collision.gameObject);
        InfoBarManager.instance.SendInfoBar(collectableName);
    }
    public void BossDied()
    {
        CameraShake.Instance.Shake(4f, 1f);
    }
}


