using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // COINS
    Coins coins;

    // ANIMATIONS & SCENE MANAGEMENT
    Light2D light2D;
    public GameObject sceneLoader;
    public ParticleSystem dust;
    public Animator playerAnimator;
    public GameObject uIUpgrades;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    PlayerDash playerDash;
    public CharacterController2D controller;
    public float runSpeed = 26f;
    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {       
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        light2D = GetComponent<Light2D>();
        playerDash = GetComponent<PlayerDash>();
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name); 
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);

        if (!(PlayerPrefs.GetFloat("RunSpeed") > 0))
        {
            PlayerPrefs.SetFloat("RunSpeed", 1);
        }
        if (!(PlayerPrefs.GetFloat("AttackDamage") > 0))
        {
            PlayerPrefs.SetFloat("AttackDamage", 1);
        }
        if (!(PlayerPrefs.GetFloat("Shield") > 0))
        {
            PlayerPrefs.SetFloat("Shield", 1);
        }
        uIUpgrades.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("RunSpeed");
        uIUpgrades.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("AttackDamage");
        uIUpgrades.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("Shield");

        if (PlayerPrefs.GetInt("Katana") == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        // PLAYER MOVEMENT
        horizontalMove = Input.GetAxisRaw("Horizontal") * (runSpeed * PlayerPrefs.GetFloat("RunSpeed"));
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
        if (!playerDash.isDashing)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
    public void CreateDust()
    {
        dust.Play();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= Mathf.RoundToInt(damage / PlayerPrefs.GetFloat("Shield"));
        playerAnimator.SetTrigger("Damaged");
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        light2D.color = Color.red;
        coins.AddCoins(-PlayerPrefs.GetInt("Coins"));
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            PlayerPrefs.SetInt("DashActive", 0);
        }
        sceneLoader.GetComponent<SceneLoader>().Load("DeathScene");
    }
}

