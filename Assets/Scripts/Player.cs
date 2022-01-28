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
    private bool cooldown = true;
    private bool weaponActive;

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && weaponActive)
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
        if (cooldown)
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
            StartCoroutine(CooldownCoroutine());
        }
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

    IEnumerator CooldownCoroutine()
    {
        cooldown = false;
        yield return new WaitForSeconds(attackCooldown);
        cooldown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Collectable"))
        {
            return;
        }
        string collectableName = collision.gameObject.name;

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
        infoBar.SetActive(true);
        Destroy(collision.gameObject);
        InfoBarManager.instance.SendInfoBar(collectableName);
        boss.SetActive(true);
    }
}


