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
    public Animator playerAnimator;
    public GameObject uIUpgrades;
    private GameObject dBox;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // MOVEMENT
    PlayerDash playerDash;
    private Rigidbody2D rb;
    public CharacterController2D controller;
    public float runSpeed = 26f;
    public float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (PlayerPrefs.GetInt("PlayTheme") == 1)
        {
            AudioManager.instance.Play("theme");
            PlayerPrefs.SetInt("PlayTheme", 0);
        }
    }
    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (dBox.transform.localPosition.y > -130)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        // PLAYER MOVEMENT
        horizontalMove = Input.GetAxisRaw("Horizontal") * (runSpeed * PlayerPrefs.GetFloat("RunSpeed"));
        if (!DialogueManager.instance.isOpen)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }

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
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= Mathf.RoundToInt(damage / PlayerPrefs.GetFloat("Shield"));
        playerAnimator.SetTrigger("Damaged");
        CameraShake.Instance.Shake(2f, .16f);
        AudioManager.instance.Play("playerhurt");
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
        Instantiate(hurtPS, transform.position, transform.rotation);
        AudioManager.instance.Play("gameover");
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
            PlayerPrefs.SetInt("Katana", 0);
        }
        SceneLoader.instance.Load("DeathScene");
        gameObject.SetActive(false);
    }
}

