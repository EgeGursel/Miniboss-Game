using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Boss : MonoBehaviour
{
	// HEALTH
	public int maxHealth = 800;
	public int currentHealth;
	public BossHealthBar bossHealthbar;
	private bool isEnraged = false;

	// ANIMATIONS & SCENE MANAGEMENT
	private Light2D light2D;
	public ParticleSystem lightParticles;
	public ParticleSystem enrageParticles;
	private Transform player;
	private Animator animator;
    public bool isFlipped = true;
	public Transform keyPrefab;

	private void Start()
    {
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator.SetFloat("walkSpeed", PlayerPrefs.GetFloat("RunSpeed"));
		light2D = GetComponent<Light2D>();
		currentHealth = maxHealth;
		bossHealthbar.gameObject.SetActive(true);
		bossHealthbar.SetMaxHealth(currentHealth);
		StartCoroutine(StartBossAnim());
	}
    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
	public void Damage(int damage)
	{
		animator.SetTrigger("Damaged");
		currentHealth -= damage;
		bossHealthbar.SetHealth(currentHealth);

		if (currentHealth <= (maxHealth/2) && !isEnraged)
        {
			Enrage();
			isEnraged = true;
		}
		
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		Instantiate(keyPrefab, transform.position, transform.rotation);
		EndingManager.instance.BossDead();
		gameObject.SetActive(false);
	}

	public void AttackShakeCamera()
    {
		CameraShake.Instance.Shake(3f, .4f);
	}

	private IEnumerator StartBossAnim()
    {
		lightParticles.Play();
		CameraShake.Instance.Shake(3f, 2f);
		yield return new WaitForSeconds(2f);
		animator.SetTrigger("Moving");
	}
	public void Enrage()
    {
		EnrageVisuals();
		animator.SetFloat("attackSpeed", animator.GetFloat("attackSpeed")+2);
		animator.SetFloat("walkSpeed", animator.GetFloat("walkSpeed")+1.5f);
		animator.GetBehaviour<Boss_Walk>().speed *= 1.5f;
	}
	private void EnrageVisuals()
    {
		enrageParticles.Play();
		light2D.color = Color.red;
		light2D.intensity = 2.5f;
    }
}