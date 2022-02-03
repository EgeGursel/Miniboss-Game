using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // COINS & SOULS
    PlayerPickUp playerPickUp;
    Coins coins;
    Souls souls;

    // ANIMATIONS & SCENE & DIALOGUE MANAGEMENT
    public ParticleSystem hurtPS;
    public GameObject sceneLoader;
    public ParticleSystem dust;
    public Animator playerAnimator;
    public GameObject uIUpgrades;
    private GameObject dBox;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    PlayerDash playerDash;
    public CharacterController2D controller;
    public float runSpeed = 26f;
    public float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        dBox = GameObject.FindWithTag("DBox");
        playerPickUp = GetComponent<PlayerPickUp>();
        souls = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<Souls>();
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        playerDash = GetComponent<PlayerDash>();
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name); 
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);


        if (PlayerPrefs.GetInt("Katana") == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Bow") == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (Time.timeScale == 0f || dBox.transform.localPosition.y > -130)
        {
            return;
        }
        // PLAYER MOVEMENT
        horizontalMove = Input.GetAxisRaw("Horizontal") * (runSpeed * PlayerPrefs.GetFloat("RunSpeed"));
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (gameObject.transform.position.y < -18)
        {
            Die();
        }

    }
    void FixedUpdate()
    {
        // PLAYER JUMP
        if (!playerDash.isDashing && Time.timeScale == 1f)
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
        CameraShake.Instance.Shake(2f, .16f);
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int heal)
    {
        if (currentHealth + heal <= 100)
        {
            currentHealth += heal;
        }
        else
        {
            currentHealth = 100;
        }
        playerAnimator.SetTrigger("Healed");
        healthbar.SetHealth(currentHealth);
    }
    public void Die()
    {
        hurtPS.transform.position = transform.position;
        hurtPS.Play();
        if (PlayerPrefs.GetInt("Coins") >= playerPickUp.levelCoinCount)
        {
            coins.AddCoins(-playerPickUp.levelCoinCount);
        }
        else
        {
            coins.AddCoins(-PlayerPrefs.GetInt("Coins"));
        }
        if (PlayerPrefs.GetInt("SoulFragments") >= playerPickUp.levelSoulCount)
        {
            souls.AddSouls(-playerPickUp.levelSoulCount);
        }
        else
        {
            souls.AddSouls(-PlayerPrefs.GetInt("SoulFragments"));
        }

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            PlayerPrefs.SetInt("DashActive", 0);
        }
        sceneLoader.GetComponent<SceneLoader>().Load("DeathScene");
        gameObject.SetActive(false);
    }
    private void Deleted()
    {
        uIUpgrades.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("RunSpeed");
        uIUpgrades.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("AttackDamage");
        uIUpgrades.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("Shield");
    }
}

