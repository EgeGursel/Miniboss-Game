using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // WEAPON & ANIMATIONS
    public GameObject weapon;
    public ParticleSystem dust;
    public Animator playerAnimator;
    public Animator weaponAnimator;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    // ATTACK
    public Transform attackArea;
    public float attackRange = 1.05f;
    public LayerMask enemyLayer;
    public int attackDamage = 30;

    private void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }
    void FixedUpdate()
    {
        // MOVE CHARACTER
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
    public void CreateDust()
    {
        dust.Play();
    }
    void Attack()
    {
        // DETECT ENEMIES IN RANGE OF ATTACK
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayer);

        // DAMAGE ENEMIES
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
        weaponAnimator.SetTrigger("Attack");
        CameraShake.Instance.Shake(2f, .16f);
        return;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
}

