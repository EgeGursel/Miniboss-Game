using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ANIMATIONS & SCENE MANAGEMENT
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public GameObject sceneLoader;
    public ParticleSystem dust;
    public Animator playerAnimator;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    public CharacterController2D controller;
    public float runSpeed = 26f;
    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name); 
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (gameObject.transform.position.y < -24)
        {
            Die();
        }

    }
    void FixedUpdate()
    {
        // PLAYER JUMP
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
    public void CreateDust()
    {
        dust.Play();
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
    public void Die()
    {
        spriteRenderer.color = Color.red;
        // USE RIGIDBODY2D AND MAKE PLAYER STOP ALL MOVEMENT
        PlayerPrefs.SetInt("Coins", 0);
        sceneLoader.GetComponent<SceneLoader>().Load("DeathScene");
    }
}

