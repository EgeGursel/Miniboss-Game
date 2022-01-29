using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Boss : MonoBehaviour
{
	// HEALTH
	public int maxHealth = 500;
	public int currentHealth;
	public BossHealthBar bossHealthbar;
	public bool isDead = false;

	// ANIMATIONS & SCENE MANAGEMENT
	private Light2D light2D;
	public bool bossDefeated = false;
	private SpriteRenderer spriteRenderer;
	public ParticleSystem lightParticles;
	public ParticleSystem explosionParticles;
	public ParticleSystem enrageParticles;
	public ParticleSystem hurtParticles;
	public Transform player;
	public Animator bossAnimator;
    public bool isFlipped = true;

	private void Start()
    {
		bossAnimator.SetFloat("attackSpeed", 1f);
		bossAnimator.SetFloat("walkSpeed", 1f);
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		currentHealth -= damage;
		StartCoroutine(DamageVisuals());
		bossHealthbar.SetHealth(currentHealth);

		if (currentHealth <= (maxHealth/2))
        {
			Enrage();
		}
		
		if (currentHealth <= 0)
		{
			isDead = true;
			Die();
		}
	}
	public void Die()
	{
		AfterDeath();
	}

	public void AttackShakeCamera()
    {
		CameraShake.Instance.Shake(3f, .4f);
	}

	private void AfterDeath()
    {
		ExplosionParticles();
		gameObject.SetActive(false);
	}

	private IEnumerator StartBossAnim()
    {
		GlowParticles();
		CameraShake.Instance.Shake(3f, 2f);
		yield return new WaitForSeconds(2f);
		bossAnimator.SetTrigger("Moving");
	}

	public void GlowParticles()
    {
		lightParticles.Play();
	}

	public void ExplosionParticles()
    {
		explosionParticles.transform.position = gameObject.transform.position;
		explosionParticles.Play();
    }

	public void Enrage()
    {
		EnrageVisuals();
		bossAnimator.SetFloat("attackSpeed", 2f);
		bossAnimator.SetFloat("walkSpeed", 1.5f);
		bossAnimator.GetBehaviour<Boss_Walk>().speed = 5f;
	}

	IEnumerator WaitTimeThree()
	{
		yield return new WaitForSeconds(3f);
	}


	private void EnrageVisuals()
    {
		enrageParticles.Play();
		light2D.color = Color.red;
		light2D.intensity = 2f;
    }

	private IEnumerator DamageVisuals()
	{
		spriteRenderer.color = Color.red;
		hurtParticles.Play();
		yield return new WaitForSeconds(0.15f);
		spriteRenderer.color = Color.white;
	}
}