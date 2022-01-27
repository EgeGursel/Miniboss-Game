using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	// HEALTH
	public int maxHealth = 500;
	public int currentHealth;
	public BossHealthBar bossHealthbar;

	// ANIMATIONS & SCENE MANAGEMENT
	public ParticleSystem lightParticles;
	public Transform player;
	public Animator bossAnimator;
    public bool isFlipped = true;
	public GameObject sceneLoader;

	private void Start()
    {
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
		bossHealthbar.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		StartCoroutine(AfterDeath());
		gameObject.SetActive(false);
	}

	public void AttackShakeCamera()
    {
		CameraShake.Instance.Shake(3f, .4f);
	}

	IEnumerator AfterDeath()
    {
		// ADD DEATH ANIMATION & DESTROY BOSS HERE INSTEAD OF Die()
		yield return new WaitForSeconds(0);
	}

	IEnumerator StartBossAnim()
    {
		GlowParticles();
		CameraShake.Instance.Shake(3f, 2.4f);
		yield return new WaitForSeconds(3f);
		bossAnimator.SetTrigger("Moving");
	}

	public void GlowParticles()
    {
		lightParticles.Play();
	}
}